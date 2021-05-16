using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EFCore5WebApp.Core.Entities;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace EFCore5WebApp.Pages.Contacts {
    public class CreateModel : PageModel {
        private readonly EFCore5WebApp.DAL.AppDbContext _context;
        [BindProperty(SupportsGet = true)]
        public List<SelectListItem> States { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<SelectListItem> Countries { get; set; }

        public CreateModel(EFCore5WebApp.DAL.AppDbContext context) {
            _context = context;
        }

        public IActionResult OnGet() {
            Person.Addresses.Add(new Address());
            
            States = _context.Lookups.Where(x => x.LookUpType == LookUpType.State)
                .Select(x => new SelectListItem { Text = x.Description, Value = x.Code }).ToList();
            Countries = _context.Lookups.Where(x => x.LookUpType == LookUpType.Country)
                .Select(x => new SelectListItem { Text = x.Description, Value = x.Code }).ToList();

            States.Insert(0, new SelectListItem { Text = "Select an item", Value = string.Empty });
            Countries.Insert(0, new SelectListItem { Text = "Select an item", Value = string.Empty });

            return Page();
        }

        [BindProperty (SupportsGet = true)]
        public Person Person { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) {
                return Page();
            }
            Person.CreatedOn = DateTime.Now;
            _context.Persons.Add(Person);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}