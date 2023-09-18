using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Linq;
using System.Threading.Tasks;



namespace KompInvest.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SendGridClient _sendGridClient;

        public AdminController(UserManager<IdentityUser> userManager, SendGridClient sendGridClient)
        {
            _userManager = userManager;
            _sendGridClient = sendGridClient;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            var pendingUsers = users.Where(u => _userManager.GetClaimsAsync(u).Result.Any(c => c.Type == "IsApproved" && c.Value == "false")).ToList();
            return View(pendingUsers);
        }

        private async Task SendEmailAsync(string email, string subject, string message)
        {
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("admin@example.com", "Admin"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));
            await _sendGridClient.SendEmailAsync(msg);
        }

        public async Task<IActionResult> ApproveUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.RemoveClaimAsync(user, new System.Security.Claims.Claim("IsApproved", "false"));
            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("IsApproved", "true"));
            await SendEmailAsync(user.Email, "Account Approved", "Your account has been approved by the admin.");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RejectUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.DeleteAsync(user);
            await SendEmailAsync(user.Email, "Account Rejected", "Your account has been rejected by the admin.");
            return RedirectToAction("Index");
        }
    }
}