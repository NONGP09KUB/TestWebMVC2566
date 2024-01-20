using Microsoft.AspNetCore.Identity;
using PRODUCT1.Models;

namespace PRODUCT1.Service
{
    public interface IRoleService
    {


        Task<List<IdentityRole>> GetAll();
        Task<bool> Add(RoleDto roleDto);
        Task<bool> Update(RoleUpdateDto roleUpdateDto);
        Task<bool> Delete(string name);
        Task<IdentityRole> Find(string name);


    }
}
