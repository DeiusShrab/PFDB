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
    public class DetailsModel : PageModel
    {
        private readonly DBConnect.PFDBContext _context;

        public DetailsModel(DBConnect.PFDBContext context)
        {
            _context = context;
        }

        public Bestiary Bestiary { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Bestiary = await _context.Bestiary
                .Include(b => b.BestiaryType).SingleOrDefaultAsync(m => m.BestiaryId == id);

            if (Bestiary == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
