﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShopTARgv23.ApplicationServices.Services;
using ShopTARgv23.Core.Domain;
using ShopTARgv23.Core.Dto;
using ShopTARgv23.Core.ServiceInterface;
using ShopTARgv23.Models;
using ShopTARgv23.Models.Accounts;
using ShopTARgv23.Models.Emails;
using System.Diagnostics;
using System.Net.Mail;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ShopTARgv23.Controllers
{
    public class AccountsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailsServices _emailsServices;

        public AccountsController
            (
                UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IEmailsServices emailsServices
                

            )
        {
            _emailsServices = emailsServices;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = vm.Email,
                    Email = vm.Email,
                    City = vm.City,
                    FirstName = vm.FirstName,
                };

                var result = await _userManager.CreateAsync(user, vm.Password);

                if (result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Accounts", new {userId = user.Id, token = token}, Request.Scheme);

                    //if (_signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    //{
                    //    return RedirectToAction("ListUsers", "Administrations");
                    //}

                    EmailTokenDto newsignup = new();
                    newsignup.Token = token;
                    newsignup.Body = "Please registrate your account by: <a href\"{confirmationLink}\">clicking here </a>;";
                    newsignup.Subject = "CRUD registrartion";
                    newsignup.To = user.Email;

                    _emailsServices.SendEmailToken(newsignup, token);
                    List<string> errordatas =
                        [
                        "Area", "Accounts",
                        "Issue","Success",
                        "StatusMessage", "Registration Success",
                        "AcredOn", $"{vm.Email}",
                        "CreatedAccountData", $"{vm.Email}\n{vm.City}\n[password hidden]\n[password hidden]"
                        ];




                    ViewBag.ErrorData = errordatas;
                    ViewBag.ErrorTitle = "Registration succesful";
                    ViewBag.ErrorMessage = "Before you can Login, please confirm your " +
                        "email, by clicking on the confirmation link we have emailed you";

                    return View("ConfirmEmailMessage");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null )
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"The user with is of {userId} is not valid";
                return View("NotFound");
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            List<string> errordatas =
                        [
                        "Area", "Accounts",
                        "Issue","Success",
                        "StatusMessage", "Registration Success",
                        "AcredOn", $"{user.Email}",
                        "CreatedAccountData", $"{user.Email}\n{user.City}\n[password hidden]\n[password hidden]"
                        ];
            if (result.Succeeded)
            {
                errordatas =
                    [
                        "Area", "Accounts",
                        "Issue","Success",
                        "StatusMessage", "Registration Success",
                        "AcredOn", $"{user.Email}",
                        "CreatedAccountData", $"{user.Email}\n{user.City}\n[password hidden]\n[password hidden]"
                        ];
                ViewBag.ErrorDatas = errordatas;
                return View();
            }

            ViewBag.ErrorDatas = errordatas;
            ViewBag.ErrorTitle = "Email cannot be confirmed";
            ViewBag.ErrorMessage = $"The users email, with userdid of {userId}, cannot be confirmed";
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string? returnUrl)
        {
            LoginViewModel vm = new()
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            return View(vm);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl)
        {
            model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null && !user.EmailConfirmed &&
                    (await _userManager.CheckPasswordAsync(user, model.Password)))
                {
                    ModelState.AddModelError(string.Empty, "Email not confirmed yet");
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        //ApplicationUser applicationUser = new();

                        //model.FirstName = applicationUser.FirstName;

                        return RedirectToAction("Index", "Home");
                    }
                }

                if (result.IsLockedOut)
                {
                    return View("AccountLocked");
                }

                ModelState.AddModelError("", "Invalid Login Attempt");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
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

                    if (result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }

                        return View();
                    }

                    await _signInManager.RefreshSignInAsync(user);
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

         [HttpGet]
         [AllowAnonymous]

         public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
         {
            if (!ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null && await _userManager.IsEmailConfirmedAsync(user))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var passwordResetLink = Url.Action("ResetPassword", "Account", new {email = model.Email, token = token}, Request.Scheme);

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

        public IActionResult SendEmail(EmailsViewModel vm)
        {
            
            string customMessage = "Confirm email";

            var dto = new EmailDto()
            {
                To = vm.To,
                Subject = vm.Subject,
                Body = $"{customMessage}\n\n{vm.Body}", 
            };

            
            _emailsServices.SendEmail(dto);

            
            return View();
        }



    }
}
