using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using provider.InfraStructure.ActionFilter;
using provider.InfraStructure.Log;
using provider.InfraStructure.Middleware;
using provider.InfraStructure.Service;
using provider.Model.LineBot;
using System;

namespace provider
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
            services.AddCors(options =>
            {
                options.AddPolicy("AllowHostList",
                    policy =>
                    {
                        policy.WithOrigins("*")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });
            services.AddHttpClient<GitHubService>();
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(RequestLogActionFilter));
                options.Filters.Add(typeof(ResponseLogActionFilter));
            });
            services.AddSingleton<LineBotConfig, LineBotConfig>((s) => new LineBotConfig()
            {
                ChannelAccessToken = Environment.GetEnvironmentVariable("LineChannelAccessToken"),
                ChannelSecret = Environment.GetEnvironmentVariable("LineChannelSecret")
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "provider", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "provider v1"));
            }

            app.UseHttpsRedirection();
            app.UseMiddleware<RequestLogMiddleware>();
            app.UseRouting();
            app.UseCors("AllowHostList");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default",
                   pattern: "{controller=Home}/{action=Index}");
                endpoints.MapControllers();
            });
        }
    }
}
