using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yacaba.Domain.Models;

namespace Yacaba.EntityFramework.Datas.Configuration {
    public class WallConfguration : IEntityTypeConfiguration<Wall> {
        public void Configure(EntityTypeBuilder<Wall> builder) {
            builder.ToTable("WALLS");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd().HasColumnName("id_wall_pk");
            builder.Property(p => p.Name).HasMaxLength(200).HasColumnName("name");
            builder.Property(p => p.Image).HasColumnName("image");
            builder.Property(p => p.WallType).HasColumnName("wall_type");
            builder.Property(p => p.Angle).HasColumnName("angle");
            builder.Property(p => p.GymId).HasColumnName("id_gym_fk");

            builder.HasIndex(p => new { p.GymId, p.Name }).IsUnique(true).HasDatabaseName("ix_wall_name");

            builder
                .HasOne(p => p.Gym)
                .WithMany(p => p.Walls)
                .HasForeignKey(p => p.GymId)
                .HasConstraintName("fk_wall_gym")
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
