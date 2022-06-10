using CoreNRF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreNRF.Data
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("Location");
            builder.HasKey(e => e.Id);

        }

    }
    public class NFConfiguration : IEntityTypeConfiguration<NF>
    {
        public void Configure(EntityTypeBuilder<NF> builder)
        {
            builder.ToTable("NF");
            builder.HasKey(e => e.Id);
            builder.HasMany(e => e.Services).WithOne(e => e.Nf).HasForeignKey(e => e.NfId).OnDelete(DeleteBehavior.Cascade);
        }

    }
    public class ServicesConfiguration : IEntityTypeConfiguration<Services>
    {
        public void Configure(EntityTypeBuilder<Services> builder)
        {
            builder.ToTable("Services");
            builder.HasKey(e => e.Id);

        }

    }
}
