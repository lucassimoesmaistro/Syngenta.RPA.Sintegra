using System;
using System.Collections.Generic;
using System.Text;

namespace Syngenta.Sintegra.AntiCorruption
{
    public interface IConfigurationManager
    {
        string GetValue(string node);
    }
}
