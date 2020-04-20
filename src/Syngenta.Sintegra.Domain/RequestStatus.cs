using System;
using System.Collections.Generic;
using System.Text;

namespace Syngenta.Sintegra.Domain
{
    public enum RequestStatus
    {
        Draft = 0,

        RegisteredItems = 5,
        
        Processed = 10,

        CommunicationFailure = 15,

        OutputFileGenerated = 20
    }
}