using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Syngenta.Sintegra.Application.AutoMapper
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<ViewModelToDomainMappingProfile>();
            });
        }
    }
}
