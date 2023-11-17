using KompInvest.Data;
using KompInvest.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace KompInvest.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<AccountController> logger)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                bool isAjaxRequest = HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";

                if (ModelState.IsValid)
                {
                    var user = new IdentityUser { UserName = model.Username, Email = model.Email };
                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("IsApproved", "false"));
                        _logger.LogInformation($"User registered: {user.UserName}");

                        if (isAjaxRequest)
                        {
                            return Json(new { status = "success", message = "Registration successful. Awaiting admin approval." });
                        }
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                        if (isAjaxRequest)
                        {
                            return Json(new { status = "failure", message = "Registration failed." });
                        }
                    }
                }
                else
                {
                    if (isAjaxRequest)
                    {
                        return Json(new { status = "failure", message = "Invalid model state." });
                    }
                }

                return View(model);
            }
            catch (InvalidOperationException iex)
            {
                _logger.LogError($"InvalidOperationException occurred: {iex.Message}");
                return View("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred during registration: {ex.Message}, Username: {model.Username}");
                return View("Error");
            }
        }





        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    if (HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    {
                        // Return JSON object with redirect URL for AJAX requests
                        return Json(new { redirect = Url.Action("Index", "Home") });
                    }
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            if (HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                // Handle AJAX request errors here, if needed
                return Json(new { error = "Invalid login attempt." });
            }

            return View(model);
        }

    }
}

