using ApiCadastroReact.Data;
using ApiCadastroReact.Repository;
using ApiCadastroReact.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiCadastroReact.Api
{
    public static class BuilderExtension
    {
        public static void AddConfig(this WebApplicationBuilder builder)
        {
            ConfigApi.ConnectionString = builder.Configuration.GetConnectionString("AppDbConnectionString") ?? string.Empty;
        }

        public static void AddDoc(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(x =>
            {
                x.CustomSchemaIds(n=>n.FullName);
            });
        }

        public static void AddBanco(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<UsuariosDB>(
                x => x.UseMySql(ConfigApi.ConnectionString, ServerVersion.AutoDetect(ConfigApi.ConnectionString)));
        }

        public static void AddServicos(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
        }

        public static void AddCorsConexao(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost", policy =>
                {
                    policy.WithOrigins("http://localhost:5173", "http://127.0.0.1:5500")
                        .SetIsOriginAllowed(isOriginAllowed: _ => true)
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }
    }
}
