using CF.Business.Common;
using CF.Data;
using CF.Model;
using CF.ViewModel.Account;
using CF.Web.App_Start;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CF.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        #region Member Variable

        private readonly ICFUow uow;
        //private readonly CurrentUser currentUser;
        private User user;

        #endregion

        #region Constructor

        public AccountController(ICFUow uow, ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            this.uow = uow;
            UserManager = userManager;
            SignInManager = signInManager;
        }

        #endregion

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationSignInManager _signInManager;
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { _signInManager = value; }
        }

        [AllowAnonymous]
        // GET: Account
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                user = await UserManager.FindAsync(model.UserName, model.Password);

                var p = Request.Params;

                if (user != null && user.IsActive)
                {
                    //if (!await UserManager.IsEmailConfirmedAsync(user.Id))
                    //{
                    //    string callbackUrl = await SendEmailConfirmationTokenAsync(user.Id, "Confirm your account-Resend");
                    //    ViewBag.errorMessage = "You must have a confirmed email to log on. "
                    //                                  + "The confirmation token has been resent to your email account.";
                    //    return View("Error");
                    //}

                    await SignInAsync(user, model.RememberMe);
                    HttpCookie cookie = new HttpCookie("User", model.UserName);
                    cookie.Expires = DateTime.Now.AddDays(31);
                    Response.Cookies.Add(cookie);

                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            var pwh = new PasswordHasher();

            return View(model);
        }

        public async Task<ActionResult> ConfirmEmail(long userId, string code)
        {
            if (userId == default(long) || code == null)
            {
                return View("Error");
            }

            IdentityResult result = await UserManager.ConfirmEmailAsync(userId, code);
            if (result.Succeeded)
            {
                return View("ConfirmEmail");
            }
            else
            {
                AddErrors(result);
                return View();
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId<long>());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login", "Account");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.UserName);
                if (user == null || !user.IsActive)
                {
                    ModelState.AddModelError("", "The user either does not exist.");
                    return View();
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                string body = string.Format(EmailConstant.PassowrdResetEmail, callbackUrl);
                await UserManager.SendEmailAsync(user.Id, "Reset Password", body);
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            if (code == null)
            {
                return View("Error");
            }
            return View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.UserName);
                if (user == null || !user.IsActive)
                {
                    ModelState.AddModelError("", "No user found.");
                    return View();
                }
                IdentityResult result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("ResetPasswordConfirmation", "Account");
                }
                else
                {
                    AddErrors(result);
                    return View();
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ChangePassowrd
        public ActionResult ChangePassowrd(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
            message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
            : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
            : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
            : message == ManageMessageId.Error ? "An error has occurred."
            : "";

            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("ChangePassowrd");
            return View();
        }


        //POST: /Account/ChangePassowrd
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassowrd(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("ChangePassowrd");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId<long>(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        var user = await UserManager.FindByIdAsync(User.Identity.GetUserId<long>());
                        await SignInAsync(user, isPersistent: false);
                        return RedirectToAction("ChangePassowrd", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId<long>(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("ChangePassowrd", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // GET: /Account/Profile
        public ActionResult Profile()
        {
            return View();
        }


        #region Private Methods

        private async Task<string> SendEmailConfirmationTokenAsync(long userID, string subject)
        {
            string code = await UserManager.GenerateEmailConfirmationTokenAsync(userID);
            var callbackUrl = Url.Action("ConfirmEmail", "Account",
               new { userId = userID, code = code }, protocol: Request.Url.Scheme);
            await UserManager.SendEmailAsync(userID, subject,
               "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

            return callbackUrl;
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(User user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl) && (returnUrl != null && returnUrl.Trim() != "/"))
            {
                return Redirect(returnUrl);
            }
            else
            {
                //if (user.Roles.Any(r => r.Role.Name == RoleNames.SUPERADMIN || r.Role.Name == RoleNames.HOD))
                //    return RedirectToAction("Index", "LevelPerformanceAnalysis");
                //else if (user.Roles.Any(r => r.Role.Name == RoleNames.TEACHER))
                //    return RedirectToAction("Index", "ClassPerformanceAnalysis");
                //return RedirectToAction("Index", "Users");

                return RedirectToAction("Index", "Home");
            }
        }

        #endregion
    }
}