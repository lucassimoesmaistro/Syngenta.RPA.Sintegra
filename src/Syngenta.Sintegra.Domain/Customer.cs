using Syngenta.Common.DomainObjects;
using System;

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
        public string SituacaoDoContribuinte { get; private set; }
        public string TipoIE { get; private set; }
        public DateTime? DataDaSituação { get; private set; }

        protected Customer() { }

        public Customer(string customerId, string name, string street, string houseNumber, string district, string postalCode, string city, string country, string region, string cnpj, string cpf, string inscricaoEstadual, string situacaoDoContribuinte, string tipoIE, DateTime? dataDaSituação)
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
            SituacaoDoContribuinte = situacaoDoContribuinte;
            TipoIE = tipoIE;
            DataDaSituação = dataDaSituação;
        }
        public Customer(string name, string street, string houseNumber, string district, string postalCode, string city, string country, string region, string cnpj, string cpf, string inscricaoEstadual, string situacaoDoContribuinte, string tipoIE, DateTime? dataDaSituação)
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
            SituacaoDoContribuinte = situacaoDoContribuinte;
            TipoIE = tipoIE;
            DataDaSituação = dataDaSituação;
        }
        public Customer(string name, string street, string houseNumber, string district, string postalCode, string city, string country, string region, string cnpj, string cpf, string inscricaoEstadual, string situacaoDoContribuinte, string tipoIE)
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
            SituacaoDoContribuinte = situacaoDoContribuinte;
            TipoIE = tipoIE;
        }
    }
}
