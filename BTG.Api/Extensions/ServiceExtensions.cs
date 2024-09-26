using Asp.Versioning;

namespace BTG.Api.Extensions
{
    public static class ServiceExtensions
    {
        /**
         * Se crea un método de extensión para agregar la versión de la API.
         * se definen las versiones de la API y se configura para que asuma la versión por defecto.
         * y se reporten las versiones de la API.
         */
        public static void AddApiVersioningExtension(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
        }
    }
}
