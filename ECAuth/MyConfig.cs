using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECAuth
{
    public class MyConfig
    {
        public string URLHomeExternalApp { get; set; }
        public string MailActivo { get; set; }
        public string MailToPruebas { get; set; }
        public string MailServer { get; set; }
        public string MailPort { get; set; }
        public string MailFrom { get; set; }
        public string MailUser { get; set; }
        public string MailPass { get; set; }
        public string MailSSL { get; set; }
        public string EsEntornoPruebas { get; set; }
        public string ResetPasswordExpiration { get; set; }
        public string OldPassCheck { get; set; }
    }
}
