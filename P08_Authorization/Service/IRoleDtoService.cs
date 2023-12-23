namespace P08_Authorization.Service
{
    public interface IRoleDtoService
    {
        Task<List<IdentityRole>> Get();
    }
}
