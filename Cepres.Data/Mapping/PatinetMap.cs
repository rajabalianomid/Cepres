using Cepres.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cepres.Data.Mapping
{
    public class PatinetMap : EntityTypeConfiguration<Patient>
    {
        public override void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable(nameof(Patient));
            builder.HasKey(pk => pk.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(200);

            base.Configure(builder);
        }
    }
}
