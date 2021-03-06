﻿using DBConnect;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PFDBSite.Models;
using PFDBSite.Services;
using System.Text;

namespace PFDBSite
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
      services.AddDbContext<PFUserContext>(options =>
                options.UseSqlServer($"Server={PFConfig.UDB_ADDR};Database={PFConfig.UDB_DB};User Id={PFConfig.UDB_USER};Password={PFConfig.UDB_PASS}"));

      // DEBUG
      var a = PFConfig.UDB_ADDR;
      var b = PFConfig.UDB_DB;
      var c = PFConfig.UDB_USER;
      var d = PFConfig.UDB_PASS;

      services.AddIdentity<ApplicationUser, IdentityRole>()
          .AddEntityFrameworkStores<PFUserContext>()
          .AddDefaultTokenProviders();

      services.Configure<IdentityOptions>(options =>
      {
        // Password settings
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.Password.RequiredUniqueChars = 4;

        // Lockout settings
        options.Lockout.DefaultLockoutTimeSpan = System.TimeSpan.FromMinutes(30);
        options.Lockout.MaxFailedAccessAttempts = 10;
        options.Lockout.AllowedForNewUsers = true;

        // User settings
        options.User.RequireUniqueEmail = true;
      });

      services.ConfigureApplicationCookie(options =>
      {
        // Cookie settings
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = System.TimeSpan.FromMinutes(360);
        // If the LoginPath isn't set, ASP.NET Core defaults 
        // the path to /Account/Login.
        options.LoginPath = "/Account/Login";
        // If the AccessDeniedPath isn't set, ASP.NET Core defaults 
        // the path to /Account/AccessDenied.
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.SlidingExpiration = true;
      });

      // Add application services.
      services.AddTransient<IEmailSender, EmailSender>();

      services.AddMvc()
              .AddSessionStateTempDataProvider();

      services.AddSession();

      services.AddSignalR();

      services.AddAuthentication(options =>
      {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
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
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
      }

      app.UseForwardedHeaders(new ForwardedHeadersOptions
      {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
      });
#if !DEBUG
      var options = new RewriteOptions()
                        .AddRedirectToHttps();
      app.UseRewriter(options);
#endif
      app.UseStaticFiles();

      app.UseAuthentication();

      app.UseSession();

      app.UseMvc(routes =>
      {
        routes.MapRoute(
                  name: "default",
                  template: "{controller=Pathfinder}/{action=Index}/{id?}");
      });

      app.UseSignalR(routes =>
      {
        routes.MapHub<LiveDisplayHub>("/livedisplayhub");
      });
    }
  }
}
