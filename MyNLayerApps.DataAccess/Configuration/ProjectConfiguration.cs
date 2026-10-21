using Microsoft.EntityFrameworkCore;
using MyNLayerApps.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyNLayerApps.DataAccess.Configuration
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(p => p.Description)
                .HasMaxLength(500);
            builder.Property(p => p.StartDate)
                .IsRequired();
            builder.Property(p => p.EndDate)
                .IsRequired();
            builder.Property(p => p.Budget)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
        }
    }
}
