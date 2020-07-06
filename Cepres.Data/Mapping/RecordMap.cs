using Cepres.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cepres.Data.Mapping
{
    public class RecordMap : EntityTypeConfiguration<Record>
    {
        public override void Configure(EntityTypeBuilder<Record> builder)
        {
            builder.ToTable(nameof(Record));
            builder.HasKey(pk => pk.Id);
            builder.Property(p => p.DiseaseName).HasMaxLength(200);
            builder.HasOne(p => p.Patient).WithMany(w => w.Records).HasForeignKey(fk => fk.PatientId);

            base.Configure(builder);
        }
    }
}
