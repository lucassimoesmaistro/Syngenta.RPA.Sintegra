using Syngenta.Common.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Syngenta.RPA.Sintegra.Domain
{
    public class RequestItem : Entity
    {
        public Guid? RequestId { get; private set; }
        public string CustomerId { get; private set; }
        public string CustomerName { get; private set; }
        public string CustomerStreet { get; private set; }
        public string CustomerHouseNumber { get; private set; }
        public string CustomerDistrict { get; private set; }
        public string CustomerPostalCode { get; private set; }
        public string CustomerCity { get; private set; }
        public string CustomerCountry { get; private set; }
        public string CustomerRegion { get; private set; }
        public string CustomerCNPJ { get; private set; }
        public string CustomerCPF { get; private set; }
        public string CustomerInscricaoEstadual { get; private set; }
        public RequestItemStatus RequestItemStatus { get; private set; }

        private readonly List<ChangeLog> _changeLogs;
        public IReadOnlyCollection<ChangeLog> ChangeLogs => _changeLogs;

        public Request Request { get; set; }
        
        public RequestItem(string customerId, string customerStreet, string customerHouseNumber, string customerDistrict, string customerPostalCode, string customerCity, string customerCountry, string customerRegion, string customerCNPJ, string customerCPF, string customerInscricaoEstadual)
        {
            CustomerId = customerId;
            CustomerStreet = customerStreet;
            CustomerHouseNumber = customerHouseNumber;
            CustomerDistrict = customerDistrict;
            CustomerPostalCode = customerPostalCode;
            CustomerCity = customerCity;
            CustomerCountry = customerCountry;
            CustomerRegion = customerRegion;
            CustomerCNPJ = customerCNPJ;
            CustomerCPF = customerCPF;
            CustomerInscricaoEstadual = customerInscricaoEstadual;
            _changeLogs = new List<ChangeLog>();
        }

        protected RequestItem()
        {
            _changeLogs = new List<ChangeLog>();
        }
        internal void ConnectToRequest(Guid requestId)
        {
            RequestId = requestId;
        }
    }
}
