using ApiCadastroReact.Data.Map;
using ApiCadastroReact.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCadastroReact.Data
{
    public class UsuariosDB : DbContext
    {
        public UsuariosDB(DbContextOptions<UsuariosDB> options) : base(options)
        {
        }

        public DbSet<UsuarioModels> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
