using BlazorEcommerce.Client.Services.UserService;

namespace BlazorEcommerce.Client.ViewModels
{
    public class UserVM
    {
		private readonly IUserService _userService;
		private readonly IAddressService _addressService;

		 
		public UserVM(IUserService userService)
        {	
			_userService = userService;
			
		}
        
        public string Email { get; set; } = string.Empty;

		public string DateCreated { get; set; } = string.Empty;

		public string Role { get; set; } = string.Empty;



		public string firstname { get; set; } = string.Empty;
		public string lastname { get; set; } = string.Empty;
		public string city { get; set; } = string.Empty;
		public string street { get; set; } = string.Empty;

		public string state { get; set; } = string.Empty;

		public string zipcode { get; set; } = string.Empty;

		public string country { get; set; } = string.Empty;
	}
}
