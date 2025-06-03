namespace ApiCadastroReact.Api
{
    public static class AppExtension
    {
        public static void ConfigAmbiente(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
