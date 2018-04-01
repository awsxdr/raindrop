namespace Raindrop.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using Raindrop.Domain.AggregateRoots;
    using Raindrop.Identity;
    using Raindrop.Models;

    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<UserAccount> _userManager;
        private readonly SignInManager<UserAccount> _signInManager;

        public AccountController(
            UserManager<UserAccount> userManager,
            SignInManager<UserAccount> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index() =>
            Redirect("/");

        [HttpGet]
        public IActionResult Login() =>
            View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login([FromForm] LoginViewModel loginDetails, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if(ModelState.IsValid)
            {
                var result =
                    _signInManager.PasswordSignInAsync(loginDetails.Username, loginDetails.Password, loginDetails.Remember, false);

                if (result.IsCompletedSuccessfully)
                    return Redirect("/");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Setup()
        {
            var result = await _userManager.CreateAsync(
                new UserAccount("admin", null),
                "z4&%B1rY@.");

            return Redirect("/");
        }
    }
}