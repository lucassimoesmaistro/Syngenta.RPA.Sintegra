using Syngenta.Common.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Syngenta.RPA.Sintegra.Domain
{
    public class Request : Entity, IAggregateRoot
    {
        public DateTime Timestamp { get; private set; }
        public string FileName { get; private set; }
        public RequestStatus RequestStatus { get; private set; }

        private readonly List<RequestItem> _requestItems;
        public IReadOnlyCollection<RequestItem> RequestItems => _requestItems;

        public Request(string fileName)
        {
            Timestamp = DateTime.Now;
            FileName = fileName;
            _requestItems = new List<RequestItem>();
        }

        protected Request()
        {
            _requestItems = new List<RequestItem>();
        }
    }

    
}
