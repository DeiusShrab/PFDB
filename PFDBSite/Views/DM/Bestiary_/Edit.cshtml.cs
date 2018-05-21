using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DBConnect;
using DBConnect.DBModels;

namespace PFDBSite.Views.DM.Bestiary_
{
    public class EditModel : PageModel
    {
        private readonly DBConnect.PFDBContext _context;

        public EditModel(DBConnect.PFDBContext context)
        {
            _context = context;
        }

        [BindProperty]
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
           ViewData["Type"] = new SelectList(_context.BestiaryType, "BestiaryTypeId", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Bestiary).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BestiaryExists(Bestiary.BestiaryId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BestiaryExists(int id)
        {
            return _context.Bestiary.Any(e => e.BestiaryId == id);
        }
    }
}
