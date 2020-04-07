using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Syngenta.RPA.Sintegra.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Syngenta.RPA.Sintegra.Repository.Mappings
{
    public class RequestItemMapping : IEntityTypeConfiguration<RequestItem>
    {
        public void Configure(EntityTypeBuilder<RequestItem> builder)
        {
            builder.HasKey(c => c.Id);


            builder.Property(c => c.CustomerCNPJ)
                .HasColumnType("varchar(14)");

            builder.Property(c => c.CustomerCPF)
                .HasColumnType("varchar(11)");


            builder.HasOne(c => c.Request)
                .WithMany(c => c.RequestItems);

            builder.ToTable("RequestItems");
        }
    }
}
