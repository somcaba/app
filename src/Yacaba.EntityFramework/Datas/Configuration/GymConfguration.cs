using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yacaba.Domain.Models;

namespace Yacaba.EntityFramework.Datas.Configuration {
    public class GymConfguration : IEntityTypeConfiguration<Gym> {
        public void Configure(EntityTypeBuilder<Gym> builder) {
            builder.ToTable("GYMS");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd().HasColumnName("id_gym_pk");
            builder.Property(p => p.Name).HasMaxLength(200).HasColumnName("name");
            builder.Property(p => p.Image).HasColumnName("image");
            builder.Property(p => p.Contact).HasColumnName("contact");
            builder.Property(p => p.IsOffical).HasColumnName("is_official");
            builder.Property(p => p.OrganisationId).HasColumnName("id_organisation_fk");

            builder.HasIndex(p => p.Name).IsUnique(true).HasDatabaseName("ix_gym_name");

            builder.OwnsOne(p => p.Address, addressBuilder => {
                addressBuilder.Property(p => p.Line1).HasMaxLength(200).HasColumnName("address_line1");
                addressBuilder.Property(p => p.Line2).HasMaxLength(200).HasColumnName("address_line2");
                addressBuilder.Property(p => p.Line3).HasMaxLength(200).HasColumnName("address_line3");
                addressBuilder.Property(p => p.PostalCode).HasMaxLength(5).HasColumnName("address_npa");
                addressBuilder.Property(p => p.Locality).HasColumnName("address_locality");
                addressBuilder.Property(p => p.CountryCode).HasMaxLength(2).HasColumnName("address_coutry");
            });
            builder.OwnsOne(p => p.Location, locationBuilder => {
                locationBuilder.Property(p => p.Latitude).HasColumnName("location_latitude");
                locationBuilder.Property(p => p.Longitude).HasColumnName("location_longitude");
            });

            builder
                .HasOne(p => p.Organisation)
                .WithMany(p => p.Gyms)
                .HasForeignKey(p => p.OrganisationId)
                .HasConstraintName("fk_gym_organisation")
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
