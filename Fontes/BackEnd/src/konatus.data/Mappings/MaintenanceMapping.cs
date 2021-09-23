using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using konatus.business.Models;

namespace konatus.data.Mappings
{
    public class MaintenanceMapping : IEntityTypeConfiguration<MaintenanceModel>
    {
        public void Configure(EntityTypeBuilder<MaintenanceModel> builder)
        {
            builder.ToTable("Maintenances");
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
                .IsRequired();
            builder.Property(p => p.Description)
                .HasColumnType("varchar(250)")
                .HasMaxLength(150)
                .IsRequired();
            builder.Property(p => p.UserId)
                .IsRequired();
            builder.Property(p => p.StatusMaintenance)
                           .HasColumnType("int")
                           .HasConversion<int>()
                           .IsRequired();
        }
    }
}