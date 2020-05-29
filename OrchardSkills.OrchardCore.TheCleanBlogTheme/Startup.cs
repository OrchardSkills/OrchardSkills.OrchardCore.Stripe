using OrchardCore.Modules;
using OrchardCore.ResourceManagement;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using OrchardCore.Setup;
using OrchardCore.Themes.TheCleanBlogTheme.Services;
using Stripe;
using System;

namespace OrchardCore.Themes.TheCleanBlogTheme
{
    public class Startup : StartupBase
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public override void ConfigureServices(IServiceCollection serviceCollection)
        {
            ConfigureStripe();
            serviceCollection.AddSignalR();
            serviceCollection.AddSetup();
            serviceCollection.AddScoped<IResourceManifestProvider, ResourceManifest>();
            serviceCollection.AddScoped<ICardPaymentService, CardPaymentService>();
        }

        private void ConfigureStripe()
        {
            var stripeApiKey = Configuration["Stripe:ApiKey"];

            StripeConfiguration.SetApiKey(stripeApiKey);
        }

        public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {

            builder.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapHub<ChatHub>("/chatHub");
            });
        }

    }
}
