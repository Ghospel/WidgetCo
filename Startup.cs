using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WidgetAndCo.DAL;

namespace WidgetAndCo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddOptions();

            services.Configure<StorageConfig>(Configuration.GetSection("StorageConfig"));

            services.AddDbContext<OrderContext>(opt =>
                                               opt.UseCosmos("AccountEndpoint=https://clouddatabases.documents.azure.com:443/;AccountKey=Vl3li8iKwoRoI5kzgqIaC6NuV64BRBPgD8M99AHFImKbvvtqZkJQpgmutQzu2ODaiIFrWFtpMO8FvxHvNL5Wzg==", "WidgetCo"));
            services.AddDbContext<ProductContext>(opt =>
                                               opt.UseCosmos("AccountEndpoint=https://clouddatabases.documents.azure.com:443/;AccountKey=Vl3li8iKwoRoI5kzgqIaC6NuV64BRBPgD8M99AHFImKbvvtqZkJQpgmutQzu2ODaiIFrWFtpMO8FvxHvNL5Wzg==", "WidgetCo"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
