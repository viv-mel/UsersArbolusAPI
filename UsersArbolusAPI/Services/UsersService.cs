using System.Text.Json;
using UsersArbolusAPI.Models;
using UsersArbolusAPI.Options;

namespace UsersArbolusAPI.Services
{
    public class UsersService : IUsersService
    {
        private readonly ILogger logger;
        private readonly IHttpClientFactory httpClientFactory;

        public UsersService(ILogger<UsersService> logger, IHttpClientFactory httpClientFactory)
        {
            this.logger = logger;
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<UserList> GetUsers(int page, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                // End the task
                logger.LogWarning("Task cancelled");
                cancellationToken.ThrowIfCancellationRequested();
            }

            var uri = new Uri($"/napptilus/oompa-loompas?page={page}", UriKind.Relative);
            var httpClient = httpClientFactory.CreateClient(ArbolusApiOptions.SectionName);

            using HttpResponseMessage response = await httpClient.GetAsync(uri, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                logger.LogError("HttpError {HttpError} when calling url {FailedUrl} ", response.StatusCode, uri);
                response.EnsureSuccessStatusCode();
            }

            var jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);
            return JsonSerializer.Deserialize<UserList>(jsonResponse) ?? new();
        }

        public async Task<User?> GetOlderUser(int page, CancellationToken cancellationToken = default)
            => (await GetUsers(page, cancellationToken)).Results.MaxBy(u => u.Age);

        public async Task<IEnumerable<User>> GetUsersByGender(string gender, int page, CancellationToken cancellationToken = default)
            => (await GetUsers(page, cancellationToken)).Results.Where(u => u.Gender == gender);
    }
}
