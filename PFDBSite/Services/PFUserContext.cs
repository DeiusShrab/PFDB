using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBConnect;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PFDBSite.Models;

namespace PFDBSite.Services
{
  public class PFUserContext : IdentityDbContext<ApplicationUser>
  {
    public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }

    public PFUserContext() : base() { }

    public PFUserContext(DbContextOptions<PFUserContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        optionsBuilder.UseSqlServer($"Server={PFConfig.UDB_ADDR};Database={PFConfig.UDB_DB};User Id={PFConfig.UDB_USER};Password={PFConfig.UDB_PASS}");
      }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      //modelBuilder.Entity<ApplicationUser>(entity =>
      //{
      //  entity.HasKey(e => e.Id);
      //});
    }
  }
}
