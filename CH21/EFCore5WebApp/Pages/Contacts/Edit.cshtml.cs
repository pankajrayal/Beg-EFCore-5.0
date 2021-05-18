﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EFCore5WebApp.Core.Entities;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using EFCore5WebApp.DAL;

namespace EFCore5WebApp.Pages.Contacts {
    [Authorize(Roles = PageAccessRoles.AdminOnly)]
    public class EditModel : SecuredPageModel {
        private readonly EFCore5WebApp.DAL.AppDbContext _context;
        [BindProperty(SupportsGet = true)]
        public List<SelectListItem> States { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<SelectListItem> Countries { get; set; }
        public EditModel(AppDbContext context, SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager) : base(context, signInManager, userManager)
        {
            _context = context;
        }

        [BindProperty]
        public Person Person { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id) {
            if (id == null) {
                return NotFound();
            }

            Person = await _context.Persons.Include("Addresses").FirstOrDefaultAsync(m => m.Id == id);

            if (Person == null) {
                return NotFound();
            }

            var lookups = _context.Lookups.Where(x => new List<LookUpType> 
                { LookUpType.State, LookUpType.State }.Contains(x.LookUpType)).ToList();

            States = lookups.Where(x => x.LookUpType == LookUpType.State).Select(x =>
                new SelectListItem { Text = x.Description, Value = x.Code }).ToList();
            //Countries = lookups.Where(x => x.LookUpType == LookUpType.Country).Select(x =>
            //    new SelectListItem { Text = x.Description, Value = x.Code }).ToList();
            Countries = _context.Lookups.Where(x => x.LookUpType == LookUpType.Country)
                .Select(x => new SelectListItem { Text = x.Description, Value = x.Code }).ToList();

            States.Insert(0, new SelectListItem { Text = "Select an item", Value = string.Empty });
            Countries.Insert(0, new SelectListItem { Text = "Select an item", Value = string.Empty });

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) {
                return Page();
            }

            _context.Attach(Person).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!PersonExists(Person.Id)) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PersonExists(int id) {
            return _context.Persons.Any(e => e.Id == id);
        }
    }
}