
using Microsoft.EntityFrameworkCore;
using P08_Authorization.Areas.Identity.Data;
using P08_Authorization.Models;

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


        //____________________________________________________________________________________

        public async Task<bool> Add(RoleDto roleDto)
        {
            var role =   new IdentityRole
            {
                Name = roleDto.Name,
                NormalizedName = _roleManager.NormalizeKey(roleDto.Name)
            };
           var result =  await _roleManager.CreateAsync(role);
            if (!result.Succeeded) return false;
            return true;
        }

        public async Task<bool> Delete(string name)
        {
            var role = await Find(name);
            if (role == null) return false;

            var resoult = await _roleManager.DeleteAsync(role);
            if (!resoult.Succeeded) return false;
            return true;
            //return resoult.Succeeded;
           
        }

        public async Task<IdentityRole> Find(string name)
        {
           return await _roleManager.FindByNameAsync(name);
        }
     
        public async Task<List<IdentityRole>> Get()
        {
           return await _roleManager.Roles.ToListAsync();   
        }
       
        public async Task<bool> Update(RoleUpdateDto roleUpdateDto)
        {
            var role = await Find(roleUpdateDto.Name);
            if (role == null) return false;
            // เป็นค่าที่อัพเดต
            role.Name = roleUpdateDto.NameUpdate;
            role.NormalizedName = _roleManager.NormalizeKey(roleUpdateDto.NameUpdate);
            
            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded) return false;
            return true;
        }
        //____________________________________________________________________________________

    }
}
