using Microsoft.Extensions.DependencyInjection;
using Syngenta.RPA.Sintegra.Bootstrapper;
using System;

namespace Syngenta.RPA.Sintegra.ScheduledService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
               .AddRepository()
               .AddAntiCorruptionLayer()
               .AddDomain()
               .AddApplication()
               .BuildServiceProvider();


        }
    }
}
