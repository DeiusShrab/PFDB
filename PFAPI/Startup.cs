using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DBConnect;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace PFAPI
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
#if !DEBUG
      services.Configure<MvcOptions>(options =>
      {
        options.Filters.Add(new RequireHttpsAttribute());
      });
#endif
      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
          options.TokenValidationParameters = new TokenValidationParameters
          {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = PFConfig.JWT_ISSUER,
            ValidAudience = PFConfig.JWT_ISSUER,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(PFConfig.JWT_KEY))
          };
        });

      services.AddMvc();

      services.AddDbContext<PFDBContext>(options =>
                options.UseSqlServer($"Server={PFConfig.DB_ADDR};Database={PFConfig.DB_DB};User Id={PFConfig.DB_USER};Password={PFConfig.DB_PASS}"));

      ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

      using (var context = PFDAL.GetContext())
      {
        context.Database.Migrate();
      }
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
#if !DEBUG
      var options = new RewriteOptions()
                        .AddRedirectToHttps();

      app.UseRewriter(options);
#endif
      app.UseAuthentication();

      //app.UseMvc();

      Console.WriteLine(PFConfig.APP_MODE);
    }
  }
}
