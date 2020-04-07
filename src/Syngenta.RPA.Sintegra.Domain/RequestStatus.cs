using System;
using System.Collections.Generic;
using System.Text;

namespace Syngenta.RPA.Sintegra.Domain
{
    public enum RequestStatus
    {
        Draft = 0,
        RegisteredItems = 1,
        Processing = 2,
        Processed = 3,
        PartiallyProcessed = 4,
        CommunicationFailure = 10
    }
}