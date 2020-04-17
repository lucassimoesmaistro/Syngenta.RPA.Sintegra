using AutoMapper;
using Syngenta.Sintegra.Application.InputFiles.Models;
using Syngenta.Sintegra.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Syngenta.Sintegra.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<CustomersColletionModel, RequestItem>()
                .ConstructUsing(c =>
                    new RequestItem(c.CustomerId, c.Name, c.Street, c.HouseNumber, c.District, c.PostalCode, c.City, c.Country, c.Region, c.CNPJ, c.CPF, c.InscricaoEstadual));
        }
    }
}
