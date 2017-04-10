using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using CF.Model;
using CF.Data;
using CF.Business.Common;

namespace CF.Web.App_Start
{
    public class ApplicationUserManager : UserManager<User, long>
    {
        public ApplicationUserManager(IUserStore<User, long> store)
            : base(store)
        {
            var dataProtectorProvider = Startup.DataProtectionProvider;
            var dataProtector = dataProtectorProvider.Create("ASP.NET Identity");

            this.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<User, long>
            {
                MessageFormat = "Your security code is: {0}"
            });

            this.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<User, long>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is: {0}"
            });

            this.UserTokenProvider = new DataProtectorTokenProvider<User, long>(dataProtector)
            {
                TokenLifespan = TimeSpan.FromHours(24),
            };
            this.EmailService = new EmailService();
            this.SmsService = new SmsService();

        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new CustomUserStore(context.Get<MyDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<User, long>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug in here.
            manager.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<User, long>
            {
                MessageFormat = "Your security code is: {0}"
            });
            manager.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<User, long>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is: {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = Startup.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<User, long>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {


            var email = new EmailMessage(true)
            {
                ToUsersEmail = new List<string>() { message.Destination },
                FromUserEmail = "",
                IsBodyHtml = true,
                Subject = "Reset Password",
                Body = message.Body
            };

            var result = await SMTPEmailManager.SendEmailAsync(email);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your sms service here to send a text message.
            return Task.FromResult(0);
        }
    }

    public class ApplicationSignInManager : SignInManager<User, long>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager) :
            base(userManager, authenticationManager)
        { }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}