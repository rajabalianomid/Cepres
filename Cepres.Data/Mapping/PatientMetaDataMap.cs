using Cepres.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cepres.Data.Mapping
{
    public class PatientMetaDataMap : EntityTypeConfiguration<PatientMetaData>
    {
        public override void Configure(EntityTypeBuilder<PatientMetaData> builder)
        {
            builder.ToTable(nameof(PatientMetaData));
            builder.HasKey(pk => pk.Id);
            builder.Property(p => p.Key).IsRequired().HasMaxLength(200);
            builder.Property(p => p.Value).IsRequired().HasMaxLength(200);
            builder.HasOne(p => p.Patient).WithMany(w => w.PatientMetaDatas).HasForeignKey(fk => fk.PatientId);
            builder.HasIndex(hi => new { hi.Key, hi.Value, hi.PatientId }).IsUnique().HasName("UK_PatientMetaData_Key_Value_PatientId");

            base.Configure(builder);
        }
    }
}
