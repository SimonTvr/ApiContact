using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ApiContact
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Cette méthode est utilisée pour ajouter des services au conteneur.
        public void ConfigureServices(IServiceCollection services)
{
    services.AddCors(options =>
    {
        options.AddPolicy("AllowSpecificOrigin",
            builder => builder.WithOrigins("http://localhost:4200"));
    });
    
    // Autres configurations de services
}
        // Cette méthode est utilisée pour configurer le pipeline de requête HTTP.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    // Autres configurations de l'application

    app.UseCors("AllowSpecificOrigin");

    // Autres middlewares
}

    }
}
