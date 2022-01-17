using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaultNETClient
{
    public  class VaultOptions
    {
        public string BaseUrl { get; set; }
        public string Token { get; set; }
        public string SecretsPath { get; set; }
    }
}
