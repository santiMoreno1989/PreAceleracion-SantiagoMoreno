using ApiPreAceleracionAlkemy.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiPreAceleracionAlkemy.Data.Configurations
{
    public class PeliculasConfigurations : IEntityTypeConfiguration<Pelicula>
    {
        public void Configure(EntityTypeBuilder<Pelicula> builder)
        {
            builder.Property(p => p.Titulo).HasMaxLength(200).IsRequired();
        }
    }
}
