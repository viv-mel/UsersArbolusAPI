using Microsoft.AspNetCore.Mvc;
using UsersArbolusAPI.Models;
using UsersArbolusAPI.Services;

namespace UsersArbolusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpGet(Name = "GetUsers")]
        public async Task<UserList> Get()
        {
            return await usersService.GetUsers();
        }
    }
}
