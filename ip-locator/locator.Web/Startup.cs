using AutoMapper;
using locator.Infrastructure.EntityFramework;
using locator.Infrastructure.Mapper;
using locator.Infrastructure.Repositories;
using locator.Infrastructure.Repositories.Interfaces;
using locator.Web.Services;
using locator.Web.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace locator.Web
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

            //All services for DI
            services.AddScoped<ILocalizationRepository, LocalizationRepository>();
            
            //HttpClient for services which will need to send http requests
            services.AddHttpClient<IIpStackService ,IpStackService>(); //also register IpStackService

            //Database provider
            services.AddDbContext<LocatorContext>(opts =>
                opts.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), 
                opts => opts.MigrationsAssembly("locator.Infrastructure"))
            );            

            //AutoMapper
            var mappingConfig = new MapperConfiguration(mc => 
            {
                mc.AddProfile(new MappingProfile());
            });
            services.AddSingleton(mappingConfig.CreateMapper());

            // services.AddMvc().AddViewComponentsAsServices();

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
