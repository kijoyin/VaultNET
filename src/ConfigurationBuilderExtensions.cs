using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaultNETClient
{
    public static class ConfigurationBuilderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="environment">Add the environment name to append the secretsPath with it eg mangoose/<environment></param>
        /// <returns></returns>
        public static IConfigurationBuilder AddVaultConfiguration(this IConfigurationBuilder builder,string? environment=null)
        {
            var tempConfig = builder.Build();
            var vaultConfig =
                tempConfig.GetSection("vault");
            var vaultOptions = new VaultOptions();
            vaultOptions.BaseUrl = tempConfig.GetSection("vault:baseUrl")?.Value ?? throw new ArgumentNullException($"BaseUrl cannot be empty");
            vaultOptions.Token = tempConfig.GetSection("vault:token")?.Value?? throw new ArgumentNullException($"Token cannot be empty");
            vaultOptions.SecretsPath = tempConfig.GetSection("vault:secretsPath")?.Value?? throw new ArgumentNullException($"secretsPath cannot be empty");
            if(environment !=null)
            {
                vaultOptions.SecretsPath = $"{vaultOptions.SecretsPath}/{environment}";
            }
            var vaultClient = new VaultClient(vaultOptions);

            return builder.Add(new VaultConfigurationSource(vaultClient));
        }
    }
}
