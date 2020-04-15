using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Syngenta.Sintegra.AntiCorruption
{
    public class ConfigurationManager : IConfigurationManager
    {
        private readonly IConfiguration _configuration;

        public ConfigurationManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetValue(string node)
        {
            return _configuration[node];
        }
    }
}
