using UsersArbolusAPI.Models;

namespace UsersArbolusAPI.Services
{
    public interface IUsersService
    {
        Task<UserList> GetUsers();
    }
}
