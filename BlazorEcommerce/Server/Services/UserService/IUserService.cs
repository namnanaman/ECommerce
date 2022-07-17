namespace BlazorEcommerce.Server.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<List<User>>> GetUserList();
    }
}
