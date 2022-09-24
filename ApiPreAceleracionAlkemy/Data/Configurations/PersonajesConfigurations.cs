using ApiPreAceleracionAlkemy.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiPreAceleracionAlkemy.Data.Configurations
{
    public class PersonajesConfigurations : IEntityTypeConfiguration<Personaje>
    {
        public void Configure(EntityTypeBuilder<Personaje> builder)
        {
            builder.Property(p => p.Nombre).HasColumnType("varchar(300)").IsRequired();
            builder.Property(p => p.Historia).HasColumnType("varchar(1500)");
        }
    }
}
