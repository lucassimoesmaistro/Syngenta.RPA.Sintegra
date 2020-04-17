
using AutoMapper;
using Syngenta.Common.DomainObjects.DTO;
using Syngenta.Sintegra.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Syngenta.Sintegra.Application.AutoMapper
{
    public class DataTransferObjectToDomainMappingProfile : Profile
    {
        public DataTransferObjectToDomainMappingProfile()
        {
            CreateMap<Output, Customer>()
                .ConstructUsing(dto =>
                    new Customer(dto.nm_Empresa, dto.ds_Logradouro, dto.nr_Logradouro, dto.nm_Bairro, dto.nr_CEP, dto.nm_Municipio, "BR", dto.sg_UFLocalizacao, dto.nr_CNPJ, string.Empty, dto.nr_InscricaoEstadual));
        }
    }
}
