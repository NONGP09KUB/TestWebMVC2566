using Microsoft.AspNetCore.Mvc;
using P08_Authorization.Models;
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

        public async Task<IActionResult> Delete(string name)
        {
            await _roleService.Delete(name);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Index()
        {
            var result = await _roleService.Get();
            return View(result);
        }
        
        public async Task< IActionResult> Edit(string name)
        {
            var result = await _roleService.Find(name);
            if (result == null) { return RedirectToAction(nameof(Index)); }
            var roleupdate = new RoleUpdateDto { Name = result.Name };
            return View(roleupdate);  
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RoleUpdateDto roleUpdateDto)
        {
           await _roleService.Update(roleUpdateDto);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleDto roleDto)
        {
            await _roleService.Add(roleDto);
            return RedirectToAction(nameof(Index));
        }

       


    }
}
