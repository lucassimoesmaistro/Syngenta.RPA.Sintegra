using Syngenta.Sintegra.Application.InputFiles.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Syngenta.Sintegra.Application.OutputFiles.Models
{
    public class OutputFileColumns
    {
        [ExcelColumn("Customer")]
        public string CustomerId { get; set; }

        [ExcelColumn("Name 1")]
        public string Name { get; set; }
        [ExcelColumn("Name Sintegra")]
        public string NameSintegra { get; set; }

        [ExcelColumn("Street")]
        public string Street { get; set; }
        [ExcelColumn("Street Sintegra")]
        public string StreetSintegra { get; set; }

        [ExcelColumn("House Number")]
        public string HouseNumber { get; set; }
        [ExcelColumn("House Number Sintegra")]
        public string HouseNumberSintegra { get; set; }

        [ExcelColumn("District")]
        public string District { get; set; }
        [ExcelColumn("District Sintegra")]
        public string DistrictSintegra { get; set; }

        [ExcelColumn("Postal Code")]
        public string PostalCode { get; set; }
        [ExcelColumn("Postal Code Sintegra")]
        public string PostalCodeSintegra { get; set; }

        [ExcelColumn("City")]
        public string City { get; set; }
        [ExcelColumn("City Sintegra")]
        public string CitySintegra { get; set; }

        [ExcelColumn("Country")]
        public string Country { get; set; }
        [ExcelColumn("Country Sintegra")]
        public string CountrySintegra { get; set; }

        [ExcelColumn("Region")]
        public string Region { get; set; }
        [ExcelColumn("Region Sintegra")]
        public string RegionSintegra { get; set; }

        [ExcelColumn("CNPJ")]
        public string CNPJ { get; set; }

        [ExcelColumn("CPF")]
        public string CPF { get; set; }

        [ExcelColumn("I.E")]
        public string InscricaoEstadual { get; set; }
        [ExcelColumn("I.E. Sintegra")]
        public string InscricaoEstadualSintegra { get; set; }
    }
}
