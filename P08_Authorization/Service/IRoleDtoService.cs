using P08_Authorization.Models;

namespace P08_Authorization.Service
{
    public interface IRoleDtoService
    {
        Task<List<IdentityRole>> Get();
        Task<bool> Add(RoleDto roleDto);
        Task<bool> Delete(string name);
        Task<bool> Update(RoleUpdateDto roleUpdateDto);
        Task<IdentityRole> Find(string name); 

    }
}
