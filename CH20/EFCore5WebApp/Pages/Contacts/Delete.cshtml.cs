﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EFCore5WebApp.Core.Entities;

namespace EFCore5WebApp.Pages.Contacts {
    public class DeleteModel : PageModel {
        private readonly EFCore5WebApp.DAL.AppDbContext _context;

        public DeleteModel(EFCore5WebApp.DAL.AppDbContext context) {
            _context = context;
        }

        [BindProperty]
        public Person Person { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id) {
            if (id == null) {
                return NotFound();
            }

            Person = await _context.Persons.Include(nameof(Person.Addresses))
                .SingleOrDefaultAsync(m => m.Id == id);

            if (Person == null) {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id) {
            if (id == null) {
                return NotFound();
            }

            Person = await _context.Persons.FindAsync(id);

            if (Person != null) {
                _context.Persons.Remove(Person);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}