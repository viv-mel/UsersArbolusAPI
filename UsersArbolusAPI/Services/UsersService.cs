using System.Net.Http;
using System.Text.Json;
using UsersArbolusAPI.Models;

namespace UsersArbolusAPI.Services
{
    public class UsersService : IUsersService
    {
        const string url = "https://2q2woep105.execute-api.eu-west-1.amazonaws.com/napptilus/oompa-loompas?page=1";
        private readonly IHttpClientFactory httpClientFactory;

        public UsersService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        //private readonly HttpClient httpClient;

        //public UsersService(HttpClient httpClient)
        //{
        //    this.httpClient = httpClient;
        //}

        public async Task<UserList> GetUsers()
        {
            var httpClient = httpClientFactory.CreateClient(); // TODO HttpClient with pattern and Polly library
            using HttpResponseMessage response = await httpClient.GetAsync(url);

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
