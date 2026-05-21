using Domain.Entities.OrderModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configrations
{
    public class DeliveryMethodConfigrations : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.ToTable("DeliveryMethods");
            builder.Property(D => D.Price)
                   .HasColumnType("decimal(8,2)");

            builder.Property(D => D.ShortName)
                   .HasColumnType("varchar")
                   .HasMaxLength(50) ;

            builder.Property(D => D.Description)
                   .HasColumnType("nvarchar")
                   .HasMaxLength(100);

            builder.Property(D => D.DeliveryTime)
                   .HasColumnType("nvarchar")
                   .HasMaxLength(50) ;
            builder.Ignore(D => D.Name);
        }
    }
}
