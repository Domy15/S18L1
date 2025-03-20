using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using S18L1.Models;
using S18L1.ViewModels;

namespace S18L1.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            var authenticated = User.Identity.IsAuthenticated;
            if (authenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);

            if (user == null)
            {
                return View();
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, true, false);

            if (!signInResult.Succeeded) 
            {
                return View();
            }

            var roles = await _signInManager.UserManager.GetRolesAsync(user);

            var claims = new List<Claim>();

            foreach (var role in roles)
            {
                var roleClaim = new Claim(ClaimTypes.Role, role);
                claims.Add(roleClaim);
            }

            claims.Add(new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"));

            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            var authenticated = User.Identity.IsAuthenticated;
            if (authenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var newUser = new ApplicationUser()
            {
                Email = registerViewModel.Email,
                UserName = registerViewModel.Email,
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                BirthDate = registerViewModel.BirthDate
            };

            var result = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if (!result.Succeeded)
            {
                return View();
            }

            var user = await _userManager.FindByEmailAsync(newUser.Email);

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            await _userManager.AddToRoleAsync(user, "Student");

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
