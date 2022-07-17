using BlazorEcommerce.Client.ViewModels;

namespace BlazorEcommerce.Client.Services.UserService
{
    public interface IUserService
    {
        List<UserVM> userVMs { get; set; }

        Task GetUsers();


    }
}
