﻿using Microsoft.Extensions.DependencyInjection;

namespace Syngenta.Sintegra.Bootstrapper
{
    public static class AntiCorruptionLayerInitializer
    {

        public static IServiceCollection AddAntiCorruptionLayer(this IServiceCollection services)
        {

            //services.AddTransient<IAbcfarmaServicoWeb, AbcfarmaServicoWeb>();

            return services;
        }
    }
}
