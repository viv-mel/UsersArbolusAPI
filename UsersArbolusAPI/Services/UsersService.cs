using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using UsersArbolusAPI.Models;
using UsersArbolusAPI.Options;

namespace UsersArbolusAPI.Services
{
    public class UsersService : IUsersService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public UsersService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<UserList> GetUsers()
        {
            var uri = new Uri($"/napptilus/oompa-loompas?page=1", UriKind.Relative);
            var httpClient = httpClientFactory.CreateClient(ArbolusApiOptions.SectionName);

            using HttpResponseMessage response = await httpClient.GetAsync(uri);

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<UserList>(jsonResponse);


            //// Older user
            //var olderUser = result.Results.MaxBy(u => u.Age);

            //// Split list by gender 
            //result.Results.Where(u )

            //    // TODO Unit tests

            return result;
        }
    }
}
