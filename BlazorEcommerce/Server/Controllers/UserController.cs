using BlazorEcommerce.Server.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUserService _userService;

        public UserController(IUserService userService) 
        {
            
            _userService = userService;
        }


        [HttpGet("get")]
        public async Task<ActionResult<ServiceResponse<List<User>>>> GetUserList()
        {
            var result = await _userService.GetUserList();
            return Ok(result);
        }


    }
}
