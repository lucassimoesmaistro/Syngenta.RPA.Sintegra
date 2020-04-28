using System;
using System.Collections.Generic;
using System.Text;

namespace Syngenta.Sintegra.Application.InputFiles.Models
{
    public class CustomersColletionModel
    {
        [ExcelColumn("Customer")]
        public string CustomerId { get; set; }
        [ExcelColumn("Name 1")]
        public string Name { get; set; }
        [ExcelColumn("Street")]
        public string Street { get; set; }
        [ExcelColumn("House Number")]
        public string HouseNumber { get; set; }
        [ExcelColumn("District")]
        public string District { get; set; }
        [ExcelColumn("Postal Code")]
        public string PostalCode { get; set; }
        [ExcelColumn("City")]
        public string City { get; set; }
        [ExcelColumn("Country")]
        public string Country { get; set; }
        [ExcelColumn("Region")]
        public string Region { get; set; }
        [ExcelColumn("Tax Number 1")]
        public string CNPJ { get; set; }
        [ExcelColumn("Tax Number 2")]
        public string CPF { get; set; }
        [ExcelColumn("Tax Number 3")]
        public string InscricaoEstadual { get; set; }
    }
}
