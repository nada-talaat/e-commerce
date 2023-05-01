using Ecommerce1.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Ecommerce1
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public void Configure(IApplicationBuilder app)
        {
           
        }
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<EcommerceContext>();
            //for the authentication:
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecKey"]))
                };
            });

        }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}
