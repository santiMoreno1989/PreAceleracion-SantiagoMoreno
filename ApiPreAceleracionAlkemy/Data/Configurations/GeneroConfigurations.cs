using ApiPreAceleracionAlkemy.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiPreAceleracionAlkemy.Data.Configurations
{
    public class GeneroConfigurations : IEntityTypeConfiguration<Genero>
    {
        public void Configure(EntityTypeBuilder<Genero> builder)
        {
            builder.Property(g => g.Nombre).HasMaxLength(400).IsRequired();
        }
    }
}
