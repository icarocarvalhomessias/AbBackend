using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Rate)
            .IsRequired()
            .HasColumnType("decimal(3,2)");

        builder.Property(r => r.Count)
            .IsRequired();

        builder.ToTable("Ratings");
    }
}
