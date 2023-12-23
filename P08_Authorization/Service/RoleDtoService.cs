
using Microsoft.EntityFrameworkCore;
using P08_Authorization.Areas.Identity.Data;

namespace P08_Authorization.Service
{
    public class RoleDtoService : IRoleDtoService
    {
   // ################ // ต้องไปที่หน้า Programe AddRoles<IdentityRole>() ถึงจะรันได้ #####################################
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<MyUser> _userManager;
        public RoleDtoService(RoleManager<IdentityRole> roleManager , UserManager<MyUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<List<IdentityRole>> Get()
        {
            return await _roleManager.Roles.ToListAsync();
        }
    }
}
