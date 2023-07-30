using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CleanArchMvc.Domain.Entities;

//aula 37 - 12 minutos
namespace CleanArchMvc.Infra.Data._2.EntitiesConfiguration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(t => t.Id);
            //builder.Property(t => t.Id).ValueGeneratedOnAdd();
            //builder.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            //builder.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);


            //[DatabaseGenerated(DatabaseGeneratedOption.None)]
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();

            //builder.HasData(
            //    new Category(1, "Material Escolar"),
            //    new Category(2, "Eletrônicos"),
            //    new Category(3, "Acessórios")
            //);
        }
    }
}
