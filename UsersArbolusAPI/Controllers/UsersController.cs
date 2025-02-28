using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using UsersArbolusAPI.Models;
using UsersArbolusAPI.Services;

namespace UsersArbolusAPI.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpGet("get-users")]
        public async Task<UserList> Get([FromQuery] int page = 1, CancellationToken cancellationToken = default)
        {
            return await usersService.GetUsers(page, cancellationToken);
        }

        [HttpGet("get-older-user")]
        public async Task<User?> GetOlderUser([FromQuery] int page = 1, CancellationToken cancellationToken = default)
        {
            return await usersService.GetOlderUser(page, cancellationToken);
        }

        [HttpGet("get-users-by-gender/{gender}")]
        public async Task<IEnumerable<User>> GetUsersByGender([FromRoute] string gender, [FromQuery] int page = 1, CancellationToken cancellationToken = default)
        {
            return await usersService.GetUsersByGender(gender, page, cancellationToken);
        }
    }
}
