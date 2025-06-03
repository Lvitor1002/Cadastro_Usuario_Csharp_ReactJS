
using ApiCadastroReact.Api;

namespace ApiCadastroReact
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.AddConfig();
            builder.AddDoc();
            builder.AddBanco();
            builder.AddServicos();
            builder.AddCorsConexao();
            builder.Services.AddAuthorization();


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.ConfigAmbiente();
            }

            app.UseHttpsRedirection();
            
            //Permite que a api seja chamada de algumas origens 
            app.UseCors("AllowLocalhost");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
