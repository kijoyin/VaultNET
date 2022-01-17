using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaultNETClient
{
    public class VaultConfigurationSource : IConfigurationSource
    {
        private readonly VaultClient _vaultClient;

        public VaultConfigurationSource(VaultClient vaultClient) =>
        _vaultClient = vaultClient;
        public IConfigurationProvider Build(IConfigurationBuilder builder) =>
        new VaultConfigurationProvider(_vaultClient);
    }
}
