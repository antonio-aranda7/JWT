using AutoMapper;
using Business;
using CompanyEmployees.Extensions;
using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JWT
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
            services.AddAutoMapper(typeof(Startup));
            //services.AddSingleton(mapper);

            services.ConfigureCors();
            /*services.AddDbContext<DutchContext>(option =>
                option.UseSqlServer(Configuration.GetConnectionString("sqlConnection")));*/
            services.ConfigureSqlContext(Configuration);//Call using CompanyEmployees.Extensions;
            //services.ConfigureRepositoryManager();
            services.ConfigureCors();
            services.ConfigureRepositoryManager();//Dependecy Inyection on Repository MAnager
            //services.AddAutoMapper(MappingProfile/*typeof(Startup)*/);
            

            var secretKey = Configuration.GetSection("Settings").GetSection("SecretKey").ToString();
            var keyBytes = Encoding.UTF8.GetBytes(secretKey);

            services.AddAuthentication(config => {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;



            }).AddJwtBearer(config => {
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    //ValidAudience= Configuration["JWT:"],
                    //ValidateIssuerSigningKey = true,
                    //IssuerSigningKey = "",//new SymmetricSecurityKey(keyBytes),

                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JWT:validIssuer"],
                    ValidAudience = Configuration["JWT:validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(Configuration.GetSection("JWT:securityKey").Value))


                };

            });

            /*services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       ;
            }));*/

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            //app.UseStaticFiles();
            app.UseCors("MyPolicy");
            app.UseRouting();
             
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
