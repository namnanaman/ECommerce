using BlazorEcommerce.Client.ViewModels;
using BlazorEcommerce.Shared;
using System.Net.Http.Json;

namespace BlazorEcommerce.Client.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly HttpClient _http;

        public UserService(HttpClient http)
        {
            _http = http;
        }
        public List<UserVM> userVMs { get; set; } = new List<UserVM>();


        public async Task GetUsers()
        {
            //var x = await _http.GetFromJsonAsync<ServiceResponse<List<User>>>("api/user/get");

            //List<User> userList = new List<User>();
            

            //userList = x.Data;


            //foreach(var user in userList)
            //{

            //    UserVM userVM = new UserVM();

            //    userVM.FirstName = user.Address.FirstName;
            //    userVM.LastName = user.Address.LastName;
            //    userVM.Email = user.Email;
            //    userVM.Street = user.Address.Street;
                
            //    userVM.City = user.Address.City;
            //    userVM.State = user.Address.State;
            //    userVM.Country = user.Address.Country;
            //    userVM.ZipCode = user.Address.Zip;

            //    userVMs.Add(userVM);

            //}



        }

      
    }
}
