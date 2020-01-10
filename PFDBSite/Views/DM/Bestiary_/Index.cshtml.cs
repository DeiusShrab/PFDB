using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DBConnect;
using PFDBCommon.DBModels;

namespace PFDBSite.Views.DM.Bestiary_
{
    public class IndexModel : PageModel
    {
        private readonly DBConnect.PFDBContext _context;

        public IndexModel(DBConnect.PFDBContext context)
        {
            _context = context;
        }

        public IList<Bestiary> Bestiary { get;set; }

        public async Task OnGetAsync()
        {
            Bestiary = await _context.Bestiary
                .Include(b => b.BestiaryType).ToListAsync();
        }
    }
}
