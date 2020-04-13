using AutoMapper;
using Syngenta.RPA.Sintegra.Application.InputFiles.Models;
using Syngenta.RPA.Sintegra.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Syngenta.RPA.Sintegra.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<CustomersColletionModel, RequestItem>()
                .ConstructUsing(c =>
                    new RequestItem(c.CustomerId, c.Street, c.HouseNumber, c.District, c.PostalCode, c.City, c.Country, c.Region, c.CNPJ, c.CPF, c.InscricaoEstadual));
        }
    }
}
