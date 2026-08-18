using Microsoft.AspNetCore.Mvc;
using InstallmentAdminPanel.Models;

namespace InstallmentAdminPanel.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // TODO: verify credentials against AdminUser store (hashed password),
            // sign in with cookie authentication, then redirect.
            // Example:
            // var claims = new List<Claim> { new(ClaimTypes.Name, model.Username), new(ClaimTypes.Role, "Owner") };
            // await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            //     new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)));

            return RedirectToAction("Index", "Dashboard");
        }

        public IActionResult Logout()
        {
            // TODO: await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}
