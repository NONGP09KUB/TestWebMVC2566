using Microsoft.AspNetCore.Mvc;
using P08_Authorization.Service;

namespace P08_Authorization.Controllers
{
    public class RoleDtoController : Controller
    {
        private readonly IRoleDtoService _roleService;
        public RoleDtoController(IRoleDtoService roleService )
        {
            _roleService = roleService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
