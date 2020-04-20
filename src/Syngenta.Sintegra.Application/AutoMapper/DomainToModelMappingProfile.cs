using AutoMapper;
using Syngenta.Sintegra.Application.OutputFiles.Models;
using Syngenta.Sintegra.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Syngenta.Sintegra.Application.AutoMapper
{
    public class DomainToModelMappingProfile : Profile
    {
        public DomainToModelMappingProfile()
        {
            CreateMap<RequestItem, OutputFileColumns>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CustomerName))
                .ForMember(dest => dest.NameSintegra,
                           opt => opt.MapFrom(src =>
                            src.ChangeLogs.Where(w => w.FieldName.Equals("Name")).FirstOrDefault() != null ?
                                src.ChangeLogs.Where(w => w.FieldName.Equals("Name")).FirstOrDefault().NewValue : ""))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.CustomerStreet))
                .ForMember(dest => dest.StreetSintegra,
                           opt => opt.MapFrom(src =>
                            src.ChangeLogs.Where(w => w.FieldName.Equals("Street")).FirstOrDefault() != null ?
                                src.ChangeLogs.Where(w => w.FieldName.Equals("Street")).FirstOrDefault().NewValue : ""))
                .ForMember(dest => dest.HouseNumber, opt => opt.MapFrom(src => src.CustomerHouseNumber))
                .ForMember(dest => dest.HouseNumberSintegra,
                           opt => opt.MapFrom(src =>
                            src.ChangeLogs.Where(w => w.FieldName.Equals("HouseNumber")).FirstOrDefault() != null ?
                                src.ChangeLogs.Where(w => w.FieldName.Equals("HouseNumber")).FirstOrDefault().NewValue : ""))
                .ForMember(dest => dest.District, opt => opt.MapFrom(src => src.CustomerDistrict))
                .ForMember(dest => dest.DistrictSintegra,
                           opt => opt.MapFrom(src =>
                            src.ChangeLogs.Where(w => w.FieldName.Equals("District")).FirstOrDefault() != null ?
                                src.ChangeLogs.Where(w => w.FieldName.Equals("District")).FirstOrDefault().NewValue : ""))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.CustomerPostalCode))
                .ForMember(dest => dest.PostalCodeSintegra,
                           opt => opt.MapFrom(src =>
                            src.ChangeLogs.Where(w => w.FieldName.Equals("PostalCode")).FirstOrDefault() != null ?
                                src.ChangeLogs.Where(w => w.FieldName.Equals("PostalCode")).FirstOrDefault().NewValue : ""))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.CustomerCity))
                .ForMember(dest => dest.CitySintegra,
                           opt => opt.MapFrom(src =>
                            src.ChangeLogs.Where(w => w.FieldName.Equals("City")).FirstOrDefault() != null ?
                                src.ChangeLogs.Where(w => w.FieldName.Equals("City")).FirstOrDefault().NewValue : ""))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.CustomerCountry))
                .ForMember(dest => dest.CountrySintegra,
                           opt => opt.MapFrom(src =>
                            src.ChangeLogs.Where(w => w.FieldName.Equals("Country")).FirstOrDefault() != null ?
                                src.ChangeLogs.Where(w => w.FieldName.Equals("Country")).FirstOrDefault().NewValue : ""))
                .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.CustomerRegion))
                .ForMember(dest => dest.RegionSintegra,
                           opt => opt.MapFrom(src =>
                            src.ChangeLogs.Where(w => w.FieldName.Equals("Region")).FirstOrDefault() != null ?
                                src.ChangeLogs.Where(w => w.FieldName.Equals("Region")).FirstOrDefault().NewValue : ""))
                .ForMember(dest => dest.InscricaoEstadual, opt => opt.MapFrom(src => src.CustomerInscricaoEstadual))
                .ForMember(dest => dest.InscricaoEstadualSintegra,
                           opt => opt.MapFrom(src =>
                            src.ChangeLogs.Where(w => w.FieldName.Equals("InscricaoEstadual")).FirstOrDefault() != null ?
                                src.ChangeLogs.Where(w => w.FieldName.Equals("InscricaoEstadual")).FirstOrDefault().NewValue : ""))
                .ForMember(dest => dest.CNPJ, opt => opt.MapFrom(src => src.CustomerCNPJ))
                .ForMember(dest => dest.CPF, opt => opt.MapFrom(src => src.CustomerCPF));

        }
    }
}
