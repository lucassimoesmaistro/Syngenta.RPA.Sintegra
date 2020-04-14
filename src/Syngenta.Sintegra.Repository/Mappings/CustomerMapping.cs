using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Syngenta.Sintegra.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Syngenta.Sintegra.Repository.Mappings
{
    public class CustomerMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);
                                 
            builder.ToTable("Customers");
        }
    }
}
