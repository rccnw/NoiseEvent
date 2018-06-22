using Microsoft.Azure.KeyVault;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.AppConfiguration;



namespace ApplicationCore.Utilities
{


    // https://docs.microsoft.com/en-us/azure/key-vault/key-vault-get-started
    // https://docs.microsoft.com/en-us/azure/key-vault/key-vault-use-from-web-application
    //  A URI to a secret in an Azure Key Vault
    //  A Client ID and a Client Secret for a web application registered with Azure Active Directory that has access to your Key Vault

    //  https://NoiseEventKeyVault.vault.azure.net/keys/SqlDbPassword 

    //  and use 


    // https://noiseeventkeyvault.vault.azure.net/secrets/SqlDbPassword/e30c4d5f6fbb48bcb994a7bcfe86b234
    //  to get this specific version.


    public class KeyVaultHelper : IKeyVaultHelper
    {
        private readonly IOptions<AppSettings> _appSettings;
        private readonly string _clientId;
        private readonly string _clientSecret;

        public KeyVaultHelper(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
            _clientId = _appSettings.Value.ClientIdKeyVaultAccess;
            _clientSecret = _appSettings.Value.ClientSecretKeyVaultAccess;
        }


        public KeyVaultHelper(string clientId, string clientSecret)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
        }


        public string GetSecret(string secretKey)
        {
            var keyVaultClient = new KeyVaultClient(GetToken);

            return keyVaultClient.GetSecretAsync(secretKey).Result.Value;
        }

        public async Task<string> GetSecret(string vault, string secretKey)
        {
            var keyVaultClient = new KeyVaultClient(GetToken);

            var secret = await keyVaultClient.GetSecretAsync(vault, secretKey);
            return secret.Value;
        }

        private async Task<string> GetToken(string authority, string resource, string scope)
        {
            var authContext = new AuthenticationContext(authority);
            ClientCredential clientCred = new ClientCredential(_clientId, _clientSecret);
            AuthenticationResult result = await authContext.AcquireTokenAsync(resource, clientCred);

            if (result == null)
                throw new InvalidOperationException("Failed to obtain the token");

            return result.AccessToken;
        }
    }
    public interface IKeyVaultHelper
    {
        Task<string> GetSecret(string vault, string secretKey);
        string GetSecret(string secretKey);
    }
}
