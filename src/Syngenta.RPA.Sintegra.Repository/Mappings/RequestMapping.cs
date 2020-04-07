using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Syngenta.RPA.Sintegra.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Syngenta.RPA.Sintegra.Repository.Mappings
{
    public class RequestMapping : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasMany(c => c.RequestItems)
                .WithOne(c => c.Request)
                .HasForeignKey(c => c.RequestId);

            builder.ToTable("RequestVerification");
        }
    }
}
