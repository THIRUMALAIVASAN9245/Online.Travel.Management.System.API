namespace Online.Travel.AuthService.API
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Swashbuckle.AspNetCore.Swagger;
    using global::System;
    using global::System.Reflection;
    using AutoMapper;
    using MediatR;
    using Online.Travel.AuthService.API.Entities;
    using Online.Travel.AuthService.API.Entities.Repository;
    using Online.Travel.AuthService.API.Infrastructure;
    using Online.Travel.AuthService.API.Contract;

    ///<Summary>
    /// Startup class
    ///</Summary>
    public partial class Startup
    {
        ///<Summary>
        /// Startup class constructor
        ///</Summary>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        ///<Summary>
        /// Configuration
        ///</Summary>
        public IConfiguration Configuration { get; }

        ///<Summary>
        /// ConfigureServices method
        ///</Summary>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.AddMvc();
            services.AddMediatR();

            services.AddAutoMapper(Assembly.GetAssembly(typeof(ContractMappingProfile)));
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();

            string defaultConnectionString = Environment.GetEnvironmentVariable("UserInfoDb") ??
                                             Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AuthServiceDbContext>(options => options.UseSqlServer(defaultConnectionString));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new Info
                {
                    Version = "v1.0",
                    Title = "UserInfo API"
                });
            });
        }
      
        ///<Summary>
        /// Configure method
        ///</Summary>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");

            // MovieCruiser api's ui view 
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "UserInfo API v1.0");
            });

            //app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "api/{controller}/{action}/{id?}");
            });
        }
    }
}

