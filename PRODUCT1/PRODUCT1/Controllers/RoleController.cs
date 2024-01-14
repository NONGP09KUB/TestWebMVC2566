using Microsoft.AspNetCore.Mvc;

namespace PRODUCT1.Controllers
{
    public class RoleController : Controller
    {
        
        private readonly IRoleService _roleService;


        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        //================================================= แสดงผล =============================
        public async Task<IActionResult> Index()
        {
            var result = await _roleService.GetAll();
            return View(result);
        }
        //================================================= แสดงผล =============================



        //================================================= สร้าง =============================

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleDto roleDto)
        {
            var result = await _roleService.Add(roleDto);


            return RedirectToAction(nameof(Index));
        }
        //================================================= สร้าง =============================

        //================================================= แก้ไข =============================

        public async Task<IActionResult> Edit(string name)
        {
            var result = await _roleService.Find(name);


            var roleUpdate = new RoleUpdateDto { Name = result.Name };


            return View(roleUpdate);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RoleUpdateDto roleUpdateDto)
        {
            var result = await _roleService.Update(roleUpdateDto);


            return RedirectToAction(nameof(Index));
        }

        //================================================= แก้ไข =============================

        //================================================= ลบ =============================

        public async Task<IActionResult> Delete(string name)
        {
            var result = await _roleService.Delete(name);


            return RedirectToAction(nameof(Index));
        }
        //================================================= ลบ =============================


    }

}
