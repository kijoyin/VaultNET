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
        public static IConfigurationBuilder AddVaultConfiguration(this IConfigurationBuilder builder)
        {
            var tempConfig = builder.Build();
            var vaultConfig =
                tempConfig.GetSection("vault");
            var vaultOptions = new VaultOptions();
            vaultOptions.BaseUrl = tempConfig.GetSection("vault:baseUrl")?.Value ?? throw new ArgumentNullException($"BaseUrl cannot be empty");
            vaultOptions.Token = tempConfig.GetSection("vault:token")?.Value?? throw new ArgumentNullException($"Token cannot be empty");
            vaultOptions.SecretsPath = tempConfig.GetSection("vault:secretsPath")?.Value?? throw new ArgumentNullException($"secretsPath cannot be empty");
            var vaultClient = new VaultClient(vaultOptions);

            return builder.Add(new VaultConfigurationSource(vaultClient));
        }
    }
}
