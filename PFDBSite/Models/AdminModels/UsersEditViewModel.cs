using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PFDBSite.Models.AdminModels
{
  public class UsersEditViewModel
  {
    public ApplicationUser User { get; set; }
    public List<Tuple<IdentityRole, bool>> Roles { get; set; }
  }
}
