using ApiCadastroReact.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiCadastroReact.Data.Map
{
    public class UsuarioMap : IEntityTypeConfiguration<UsuarioModels>
    {
        public void Configure(EntityTypeBuilder<UsuarioModels> builder)
        {
            builder.HasKey(x =>x.Id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Idade).IsRequired();
            builder.Property(x => x.Email).IsRequired().HasMaxLength(50);

            builder.HasIndex(x => x.Email).IsUnique();
        }
    }
}
