using System;
using System.Collections.Generic;
using System.Text;

namespace Syngenta.Sintegra.Domain
{
    public enum RequestItemStatus
    {
        Registered = 1,
        Checked = 2,
        UpdatedRegistry = 3,
        CommunicationFailure = 10
    }
}
