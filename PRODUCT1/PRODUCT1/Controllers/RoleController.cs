using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PRODUCT1.Controllers
{
    [Authorize(Roles = SD.Role_Admin)]

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
            // เรียกใช้ Method GetAll จาก Service เพื่อแสดงสินค้า
            var result = await _roleService.GetAll();
            // จากนั้นส่งค่าไปยังหน้า View 
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
            // เรียกใช้ Method Add จาก Service เพื่อเพิ่มสินค้า
            var result = await _roleService.Add(roleDto);

            // จากนั้นส่งค่าไปยังหน้า View 

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
