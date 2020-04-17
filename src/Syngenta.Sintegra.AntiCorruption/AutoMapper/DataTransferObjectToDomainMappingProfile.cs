using AutoMapper;
using Syngenta.Sintegra.AntiCorruption.DTO;
using Syngenta.Sintegra.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Syngenta.Sintegra.AntiCorruption.AutoMapper
{
    public class DataTransferObjectToDomainMappingProfile : Profile
    {
        public DataTransferObjectToDomainMappingProfile()
        {
            //CreateMap<SintegraNacionalResponseDTO, Customer>()
            //    .ConstructUsing(dto =>
            //        new Customer(dto.Response.Output..CustomerId, c.Name, c.Street, c.HouseNumber, c.District, c.PostalCode, c.City, c.Country, c.Region, c.CNPJ, c.CPF, c.InscricaoEstadual));
        }
    }
}
