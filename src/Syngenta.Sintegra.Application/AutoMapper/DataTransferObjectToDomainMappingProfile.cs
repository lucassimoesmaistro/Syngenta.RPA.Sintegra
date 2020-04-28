
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
                    string.IsNullOrEmpty(dto.dt_Situacao) ?
                         new Customer(dto.nm_Empresa,
                                      dto.ds_Logradouro,
                                      dto.nr_Logradouro,
                                      dto.nm_Bairro,
                                      dto.nr_CEP,
                                      dto.nm_Municipio,
                                      "BR",
                                      dto.sg_UFLocalizacao,
                                      dto.nr_CNPJ,
                                      string.Empty,
                                      dto.nr_InscricaoEstadual,
                                      dto.ds_SituacaoContribuinte,
                                      dto.tp_IE)
                            : //else
                        new Customer(dto.nm_Empresa,
                                      dto.ds_Logradouro,
                                      dto.nr_Logradouro,
                                      dto.nm_Bairro,
                                      dto.nr_CEP,
                                      dto.nm_Municipio,
                                      "BR",
                                      dto.sg_UFLocalizacao,
                                      dto.nr_CNPJ,
                                      string.Empty,
                                      dto.nr_InscricaoEstadual,
                                      dto.ds_SituacaoContribuinte,
                                      dto.tp_IE,
                                      new DateTime(int.Parse(dto.dt_Situacao.Substring(6,4)),
                                                   int.Parse(dto.dt_Situacao.Substring(3, 2)),
                                                   int.Parse(dto.dt_Situacao.Substring(0, 2))))
                 );
        }
    }
}
