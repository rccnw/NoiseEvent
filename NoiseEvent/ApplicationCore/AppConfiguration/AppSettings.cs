using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.AppConfiguration
{

    public class AppSettings
    {
        public string Setting1 { get; set; }
        public string Setting2 { get; set; }

        public string KeyVaultClientId { get; set; }
        public string KeyVaultSecret { get; set; }

        public string ClientIdKeyVaultAccess { get; set; }
        public string ClientSecretKeyVaultAccess { get; set; }


        public string dbpass { get; set; }
        public string dbuser { get; set; }
        public string dbname { get; set; }

        public string SqlDbPassword { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
