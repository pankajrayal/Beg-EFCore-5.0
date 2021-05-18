using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EFCore5WebApp.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using EFCore5WebApp.DAL;
using Microsoft.AspNetCore.Identity;

namespace EFCore5WebApp.Pages.Contacts {
    [Authorize(Roles = PageAccessRoles.AdminOnly)]
    public class DeleteModel : SecuredPageModel {
        private readonly AppDbContext _context;

        public DeleteModel(AppDbContext context, SignInManager<IdentityUser> signInManager,
           UserManager<IdentityUser> userManager) : base(context, signInManager, userManager) {
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