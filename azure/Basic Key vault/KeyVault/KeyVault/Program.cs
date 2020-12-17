using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyVault
{
    class Program
    {
        static void Main(string[] args)
        {
            string keyVaultUrl = "https://demovault2090.vault.azure.net/";
            var client = new SecretClient(vaultUri: new Uri(keyVaultUrl), credential: new DefaultAzureCredential());
                        
            KeyVaultSecret secret = client.GetSecret("newpassword");
            Console.WriteLine(secret.Value);
            Console.ReadKey();
            
        }
    }
}
