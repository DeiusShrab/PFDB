using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PFDBSite.Models.ManageViewModels
{
  public class ManageRolesViewModel
  {
    public List<ApplicationUser> Users { get; set; }
    public List<Tuple<IdentityRole, bool>> Roles { get; set; }
  }
}
