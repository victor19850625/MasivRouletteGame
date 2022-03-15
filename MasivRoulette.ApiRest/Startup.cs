using MasivRoulette.DataAccess;
using MasivRoulette.DataAccess.ConnectionStrings.Interfaces;
using MasivRoulette.DataAccess.Interfaces;
using MasivRoulette.Entities.BindingModelsConfiguration;
using MasivRoulette.Services;
using MasivRoulette.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;

namespace MasivRoulette.ApiRest
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
            var connectionStringsSection = Configuration.GetSection("ConnectionStrings");
            services.Configure<ConnectionStringsBindingModel>(connectionStringsSection);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAd"));
            services.AddTransient<IConnectionDac, ConnectionDac>();
            services.AddTransient<IUserDac, UserDac>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRouletteDac, RouletteDac>();
            services.AddTransient<IRouletteService, RouletteService>();
            services.AddTransient<IOpeningDac, OpeningDac>();
            services.AddTransient<IOpeningService, OpeningService>();
            services.AddTransient<IBetDac, BetDac>();
            services.AddTransient<IBetService, BetService>();

            services.AddControllers()
                .AddNewtonsoftJson();
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MasivRoulette.ApiRest", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
            }

            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MasivRoulette.ApiRest v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
            app.UseDefaultFiles();
        }
    }
}
