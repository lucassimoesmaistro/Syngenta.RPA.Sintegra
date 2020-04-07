using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Syngenta.RPA.Sintegra.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Syngenta.RPA.Sintegra.Repository.Mappings
{
    public class ChangeLogMapping : IEntityTypeConfiguration<ChangeLog>
    {
        public void Configure(EntityTypeBuilder<ChangeLog> builder)
        {
            builder.HasKey(c => c.Id);


            builder.Property(c => c.FieldName)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(c => c.NewValue)
                .IsRequired()
                .HasColumnType("varchar(100)");


            builder.HasOne(c => c.RequestItem)
                .WithMany(c => c.ChangeLogs);

            builder.ToTable("ChangeLogs");
        }
    }
}
