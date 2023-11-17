using Microsoft.AspNetCore.Mvc;

namespace KompInvest.Controllers
{
    public class Member : Controller
    {
        public IActionResult Dashboard()
        {
            // Ensure the user is authenticated. If not, redirect to login page.
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account"); // Assuming 'Account' controller has a 'Login' action.
            }

            // Retrieve member-specific data here, such as investment details.
            // For demonstration purposes, we'll just return the view.

            return View();
        }


    }
}
