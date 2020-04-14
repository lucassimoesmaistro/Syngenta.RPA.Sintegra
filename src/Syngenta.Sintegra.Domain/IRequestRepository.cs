using Syngenta.Common.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Syngenta.Sintegra.Domain
{
    public interface IRequestRepository : IRepository<Request>
    {

        void Add(Request request);

    }
}
