using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBConnect;
using Microsoft.EntityFrameworkCore;
using PFDBSite.Models;

namespace PFDBSite.Services
{
  public class PFUserContext : DbContext
  {
    public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        optionsBuilder.UseSqlServer($"Server={PFConfig.UDB_ADDR};Database={PFConfig.UDB_DB};User Id={PFConfig.UDB_USER};Password={PFConfig.UDB_PASS}");
      }
    }
  }
}
