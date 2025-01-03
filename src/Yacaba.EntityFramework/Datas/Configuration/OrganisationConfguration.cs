using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yacaba.Domain.Models;

namespace Yacaba.EntityFramework.Datas.Configuration {
    public class OrganisationConfguration : IEntityTypeConfiguration<Organisation> {
        public void Configure(EntityTypeBuilder<Organisation> builder) {
            builder.ToTable("ORGANISATIONS");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd().HasColumnName("id_organisation_pk");
            builder.Property(p => p.Name).HasMaxLength(200).HasColumnName("name");
            builder.Property(p => p.Image).HasColumnName("image");
            builder.Property(p => p.IsOffical).HasColumnName("is_official");

            builder.HasIndex(p => p.Name).IsUnique(true).HasDatabaseName("ix_organisation_name");

        }
    }
}
