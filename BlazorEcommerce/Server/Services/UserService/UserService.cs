namespace BlazorEcommerce.Server.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context) 
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<User>>> GetUserList()
        {
            var response = new ServiceResponse<List<User>>();

            response.Data = await _context.Users
                        .Include(x => x.Address)
                        .Where(r => r.Role != "Admin").ToListAsync();
            response.Message = "Successfully got the Users ";


            return response;
         }
    }
}
