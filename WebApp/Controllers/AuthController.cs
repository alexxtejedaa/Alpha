using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

// Enstaka kommentarer för förtydligande när AI har varit till hjälp. 

namespace WebApp.Controllers
{
    [Authorize]                // Alla actions kräver inloggning om inte annat anges
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]        // Tillåter även oinloggade
        public IActionResult Register() => View();

        [HttpPost]
        [AllowAnonymous]        // Tillåter även oinloggade
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                    ModelState.AddModelError("", err.Description);
                return View(model);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            return LocalRedirect("/projects");
        }

        [HttpGet]
        [AllowAnonymous]        // Tillåt även oinloggade
        public IActionResult Login() => View();

        [HttpPost]
        [AllowAnonymous]        // Tillåt även oinloggade
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _signInManager.PasswordSignInAsync(
                model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }

            return LocalRedirect("/projects");
        }

        [HttpPost]
        [Authorize]             // Kräver inloggning
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Auth");
        }
    }
}
