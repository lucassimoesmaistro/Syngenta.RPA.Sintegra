using Syngenta.Common.DomainObjects;

namespace Syngenta.Sintegra.Domain
{
    public class Customer : Entity, IAggregateRoot
    {

        public string CustomerId { get; private set; }
        public string Name { get; private set; }
        public string Street { get; private set; }
        public string HouseNumber { get; private set; }
        public string District { get; private set; }
        public string PostalCode { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }
        public string Region { get; private set; }
        public string CNPJ { get; private set; }
        public string CPF { get; private set; }
        public string InscricaoEstadual { get; private set; }

        protected Customer() { }

        public Customer(string customerId, string name, string street, string houseNumber, string district, string postalCode, string city, string country, string region, string cnpj, string cpf, string inscricaoEstadual)
        {
            CustomerId = customerId;
            Name = name;
            Street = street;
            HouseNumber = houseNumber;
            District = district;
            PostalCode = postalCode;
            City = city;
            Country = country;
            Region = region;
            CNPJ = cnpj;
            CPF = cpf;
            InscricaoEstadual = inscricaoEstadual;
        }
        public Customer(string name, string street, string houseNumber, string district, string postalCode, string city, string country, string region, string cnpj, string cpf, string inscricaoEstadual)
        {
            Name = name;
            Street = street;
            HouseNumber = houseNumber;
            District = district;
            PostalCode = postalCode;
            City = city;
            Country = country;
            Region = region;
            CNPJ = cnpj;
            CPF = cpf;
            InscricaoEstadual = inscricaoEstadual;
        }
    }
}
