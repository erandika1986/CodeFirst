using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Business.Common
{
    public static class Constants
    {
        // Constants related to the email notifications
        public static bool EnableSsl = bool.Parse(ConfigurationManager.AppSettings["EnableSsl"]);
        public static string SmtpServerEmail = ConfigurationManager.AppSettings["SMTPServerEmail"];
        public static string SmtpServerEmailPassword = ConfigurationManager.AppSettings["SMTPServerEmailPassword"];
        public static bool NetworkCredentialIsRequired = bool.Parse(ConfigurationManager.AppSettings["NetworkCredentialIsRequired"]);
        public static int SmtpServerPort = int.Parse(ConfigurationManager.AppSettings["SMTPServerPort"]);
        public static string SmtpServer = ConfigurationManager.AppSettings["SMTPServer"];
        public static string IsGridEmailEnabled = ConfigurationManager.AppSettings["IsGridEmailEnabled"];

    }
}
