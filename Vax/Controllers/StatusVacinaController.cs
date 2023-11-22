using Microsoft.AspNetCore.Mvc;
using Vax.Data;

namespace Vax.Controllers
{
    public class StatusVacinaController : Controller
    {
        private readonly DataContext _context;
    
        public StatusVacinaController(DataContext context)
        {
            _context = context;
        }

    }
}
