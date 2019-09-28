using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Configure<TokenManagement>(Configuration.GetSection("tokenManagement"));//EKLENDİ
            var token = Configuration.GetSection("tokenManagement").Get<TokenManagement>();//EKLENDİ
            byte[] secret = Encoding.ASCII.GetBytes(token.Secret);//EKLENDİ

            services.AddScoped<IAuthenticationService, TokenAuthenticationService>();//EKLENDİ
            services.AddScoped<IUserManagementService, UserManagement>();//EKLENDİ

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x=> {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secret),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                }); //services.AddAuthentication kısmından itibaren EKLENDİ.
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication(); // yoksa bile EKLENMELİDİR.
            app.UseCors(x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()); //EKLENDİ(API tüketmek için herşeye izin verir.)

            app.UseMvc();
        }
    }
}
