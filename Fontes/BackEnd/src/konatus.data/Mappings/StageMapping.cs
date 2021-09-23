using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using konatus.business.Models;
using konatus.business.Enums;

namespace konatus.data.Mappings
{
    public class StageMapping : IEntityTypeConfiguration<StageModel>
    {
        public void Configure(EntityTypeBuilder<StageModel> builder)
        {
            builder.ToTable("Stages");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.CreateDate)
                .HasDefaultValue(DateTime.Now)
                .IsRequired();
            builder.Property(p => p.Status)
                .HasColumnType("int")
                .HasConversion<int>()
                .IsRequired();
            builder.Property(p => p.Id)
                .HasColumnType("varchar(150)")
                .HasMaxLength(150)
                .IsRequired();
            builder.Property(p => p.MaintenanceId)
                .IsRequired();
            builder.Property(p => p.Description)
                .HasColumnType("varchar(250)")
                .HasMaxLength(250)
                .IsRequired();
            builder.Property(p => p.StatusStage)
                .HasColumnType("int")
                .HasConversion<int>()
                .IsRequired();
            builder.Property(p => p.Type)
                .HasColumnType("int")
                .HasConversion<int>()
                .IsRequired();
            builder.Property(p => p.Value)
               .HasColumnType("varchar(100000)")
               .HasMaxLength(100000)
               .IsRequired();
        }
    }
}