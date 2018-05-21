using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DBConnect;
using DBConnect.DBModels;

namespace PFDBSite.Views.DM.Bestiary_
{
    public class CreateModel : PageModel
    {
        private readonly DBConnect.PFDBContext _context;

        public CreateModel(DBConnect.PFDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["Type"] = new SelectList(_context.BestiaryType, "BestiaryTypeId", "Name");
            return Page();
        }

        [BindProperty]
        public Bestiary Bestiary { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Bestiary.Add(Bestiary);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}