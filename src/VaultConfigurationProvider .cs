using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaultNETClient
{
    public class VaultConfigurationProvider: ConfigurationProvider
    {
        private readonly VaultClient _vaultClient;
        public VaultConfigurationProvider(VaultClient vaultClient)
        {
            _vaultClient = vaultClient;
        }
        public override void Load()
        {
            Data = _vaultClient.GetSecrets().GetAwaiter().GetResult() ;
        }
    }
}
