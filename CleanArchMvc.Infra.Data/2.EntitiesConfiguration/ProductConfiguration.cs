using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CleanArchMvc.Domain.Entities;

//aula 37
namespace CleanArchMvc.Infra.Data._2.EntitiesConfiguration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);  
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Price).HasPrecision(10, 2);
            builder.HasOne(e => e.Category).WithMany(e => e.Products)
                .HasForeignKey(e => e.CategoryId);
            
        }
    }
}
