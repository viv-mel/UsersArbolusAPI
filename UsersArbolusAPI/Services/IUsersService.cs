using UsersArbolusAPI.Models;

namespace UsersArbolusAPI.Services
{
    public interface IUsersService
    {
        Task<UserList> GetUsers(int page, CancellationToken cancellationToken = default);
        Task<User?> GetOlderUser(int page, CancellationToken cancellationToken = default);
        Task<IEnumerable<User>> GetUsersByGender(string gender, int page, CancellationToken cancellationToken = default);
    }
}
