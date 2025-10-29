using Kebab_Simulator.Core.Domain;
using Kebab_Simulator.Core.Domain.Dto;
using Kebab_Simulator.Core.Domain.Serviceinterface;
using Kebab_Simulator.Core.ServiceInterface;
using Kebab_Simulator.Data;
using Kebab_Simulator.Models;
using Kebab_Simulator.Models.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Kebab_Simulator.Controllers
{
    public class AccountsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _SignInManager;
        private readonly KebabSimulatorContext _context;
        private readonly IEmailsServices _emailServices;

        public AccountsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, KebabSimulatorContext context, IEmailsServices emailsservices)
        {
            _userManager = userManager;
            _SignInManager = signInManager;
            _context = context;
            _emailServices = emailsservices;
        }

        [HttpGet]
        public async Task<IActionResult> AddPassword()
        {
            var user = await _userManager.GetUserAsync(User);
            var userHasPassword = await _userManager.HasPasswordAsync(user);
            if (userHasPassword)
            {
                RedirectToAction("ChangePassword");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPassword(AddPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var result = await _userManager.AddPasswordAsync(user, model.NewPassword);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View();
                }
                await _SignInManager.RefreshSignInAsync(user);
                return View("AddPasswordConfirmation");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login");
                }
                var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View();
                }
                await _SignInManager.RefreshSignInAsync(user);
                return View("ChangePasswordConfirmation");
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null && await _userManager.IsEmailConfirmedAsync(user))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var passwordResetLink = Url.Action("ResetPassword", "Accounts", new { email = model.Email, token = token }, Request.Scheme);

                    return View("ForgotPasswordConfirmation");
                }
                return View("ForgotPasswordConfirmation");
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword()
        {
            var user = await _userManager.GetUserAsync(User);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (token == null || user.Email == null)
            {
                ModelState.AddModelError("", "Invalid password reset token");
            }
            var model = new ResetPasswordViewModel
            {
                Token = token,
                Email = user.Email
            };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        if (await _userManager.IsLockedOutAsync(user))
                        {
                            await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow);
                        }
                        await _SignInManager.SignOutAsync();
                        await _userManager.DeleteAsync(user);
                        return RedirectToAction("ResetPasswordConfirmation", "Accounts");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return RedirectToAction("ResetPasswordConfirmation", "Accounts");
                }
                await _userManager.DeleteAsync(user);
                return RedirectToAction("ResetPasswordConfirmation", "Accounts");
            }
            return RedirectToAction("ResetPasswordConfirmation", "Accounts");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    City = model.City,
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Player");

                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var confirmationLink = Url.Action(
                        "ConfirmEmail",
                        "Accounts",
                        new { userId = user.Id, token = token },
                        Request.Scheme
                    );

                    EmailTokenDto newsignup = new()
                    {
                        Token = token,
                        Body = $"Thank you for signing up! Please confirm your email by clicking here: {confirmationLink}",
                        Subject = "Confirm your email",
                        To = user.Email
                    };
                    _emailServices.SendEmailToken(newsignup, token);

                    if (_SignInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    {
                        return RedirectToAction("ListUsers", "Administrations");
                    }

                    return View("VerifyEmail");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }


        [HttpGet]
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null) { return RedirectToAction("Index", "Home"); }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"The user with id of {userId} is not valid";
                return View("NotFound");
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                ViewBag.ErrorTitle = "Email confirmed";
                ViewBag.ErrorMessage = "Your email has been confirmed successfully.";
                return View();
            }

            ViewBag.ErrorTitle = "Email cannot be confirmed";
            ViewBag.ErrorMessage = $"The users email, with userid of {userId}, cannot be confirmed.";
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string? returnURL)
        {
            LoginViewModel vm = new()
            {
                ReturnURL = returnURL,
            };
            return View(vm);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnURL)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null && !user.EmailConfirmed && (await _userManager.CheckPasswordAsync(user, model.Password)))
                {
                    ModelState.AddModelError(string.Empty, "Your email hasn't been confirmed yet. Please check your Email spam folders.");
                    return View(model);
                }

                var result = await _SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true);
                if (result.Succeeded)
                {
                    if (await _userManager.IsInRoleAsync(user, "Admin"))
                    {
                        return RedirectToAction("Index", "AdminDashboard");
                    }
                    else if (await _userManager.IsInRoleAsync(user, "Player"))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                if (result.IsLockedOut)
                {
                    return View("AccountLocked");
                }
                ModelState.AddModelError("", "Invalid Login Attempt, please contact admin.");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
