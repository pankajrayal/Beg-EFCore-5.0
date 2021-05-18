using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EFCore5WebApp.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using EFCore5WebApp.DAL;
using Microsoft.AspNetCore.Identity;

namespace EFCore5WebApp.Pages.Contacts {
    [Authorize(Roles = PageAccessRoles.AllAccess)]
    public class IndexModel : SecuredPageModel {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context, SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager):base(context, signInManager, userManager) {
            _context = context;
        }

        public IList<Person> Person { get;set; }

        public async Task OnGetAsync() {
            Person = await _context.Persons.ToListAsync();
        }
    }
}