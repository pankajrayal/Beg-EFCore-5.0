using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EFCore5WebApp.Core.Entities;

namespace EFCore5WebApp.Pages.Contacts {
    public class IndexModel : PageModel {
        private readonly EFCore5WebApp.DAL.AppDbContext _context;

        public IndexModel(EFCore5WebApp.DAL.AppDbContext context) {
            _context = context;
        }

        public IList<Person> Person { get;set; }

        public async Task OnGetAsync() {
            Person = await _context.Persons.ToListAsync();
        }
    }
}