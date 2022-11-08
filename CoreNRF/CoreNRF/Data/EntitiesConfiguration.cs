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
    public class NFServicesConfiguration : IEntityTypeConfiguration<NFServices>
    {
        public void Configure(EntityTypeBuilder<NFServices> builder)
        {
            builder.ToTable("NFServices");
            builder.HasKey(e => e.Id);
            builder.HasOne(e=> e.Service).WithMany(e=> e.NFService).HasForeignKey(e => e.ServiceId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(e => e.NF).WithMany(e => e.NFServices).HasForeignKey(e => e.NFId).OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class ServicesConfiguration : IEntityTypeConfiguration<Models.Services>
    {
        public void Configure(EntityTypeBuilder<Models.Services> builder)
        {
            builder.ToTable("Services");
            builder.HasKey(e => e.Id);

        }
    }
    public class PortalConfiguration : IEntityTypeConfiguration<Models.Portal>
    {
        public void Configure(EntityTypeBuilder<Models.Portal> builder)
        {
            builder.ToTable("Portal");
            builder.HasKey(e => e.Id);
        }
    }
    public class PortalNFConfiguration : IEntityTypeConfiguration<Models.PortalNF>
    {
        public void Configure(EntityTypeBuilder<Models.PortalNF> builder)
        {
            builder.ToTable("PortalNF");
            builder.HasKey(e => e.Id);
            builder.HasOne(e=> e.Portal).WithMany(e=> e.PortalNFs).HasForeignKey(e => e.PortalId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(e => e.NF).WithMany(e => e.PortalNFs).HasForeignKey(e => e.NFId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
