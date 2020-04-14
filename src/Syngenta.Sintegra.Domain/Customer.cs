using Syngenta.Common.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Syngenta.Sintegra.Domain
{
    public class Customer : Entity, IAggregateRoot
    {
        public string Street { get; private set; }
        public string HouseNumber { get; private set; }
        public string District { get; private set; }
        public string PostalCode { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }
        public string Region { get; private set; }
        public string CNPJ { get; private set; }
        public string CPF { get; private set; }
        public DateTime Timestamp { get; private set; }
    }
}
