using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Business.Common
{
    public class EmailMessage
    {
        public EmailMessage(bool isBodyHtml)
        {
            this.IsBodyHtml = isBodyHtml;
            ToUsersEmail = new List<string>();

        }

        public bool IsBodyHtml { get; set; }
        public string FromUserEmail { get; set; }
        public List<string> ToUsersEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
