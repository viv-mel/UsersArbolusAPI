using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.AutoMock;
using Moq.Protected;
using System.Text.Json;
using UsersAPIUnitTests.Builders;
using UsersArbolusAPI.Models;
using UsersArbolusAPI.Options;
using UsersArbolusAPI.Services;

namespace UsersAPIUnitTests.ServicesTests.UsersServiceTests
{
    public class BaseUserServiceTests
    {
        public const string ConfigSectionName = "ArbolusApiConfig";
        public const string UriStr = "/napptilus/oompa-loompas";
        protected int page = 1;

        public readonly UserBuilder userBuilder = new();
        public readonly Fixture fixture = new();
        public readonly AutoMocker autoMocker = new();
        public readonly UsersService target;


        public BaseUserServiceTests()
        {
            autoMocker.Use(Options.Create(fixture.Create<ArbolusApiOptions>()));
            target = autoMocker.CreateInstance<UsersService>();
            target.Should().BeAssignableTo<IUsersService>();
        }
        
        protected void MockHttpClient(Uri uri, HttpResponseMessage response)
        {
            // Creating http handler mock
            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            // Set up the SendAsync protected method.
            httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(m => m.Method == HttpMethod.Get && m.RequestUri != null && m.RequestUri.ToString().Contains(uri.ToString())),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response)
                .Verifiable();

            var httpClient = new HttpClient(httpMessageHandlerMock.Object)
            {
                BaseAddress = fixture.Create<Uri>()
            };

            autoMocker.Setup<IHttpClientFactory, HttpClient>(c => c.CreateClient(ConfigSectionName))
                .Returns(httpClient)
                .Verifiable();
        }

        protected void MockSuccessfulUserList(UserList userList)
        {
            var uri = new Uri($"{UriStr}?page={page}", UriKind.Relative);

            HttpResponseMessage response = new(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(userList))
            };

            MockHttpClient(uri, response);
        }

        protected void MockSuccessfulResponse(int page)
        {
            var expectedTotal = 1;
            var expectedUri = new Uri($"{UriStr}?page={page}", UriKind.Relative);
            var expectedUser = userBuilder.WithSampleData().Create();

            HttpResponseMessage response = new(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(GetRealJsonResponse(page, expectedTotal, expectedUser))
            };

            MockHttpClient(expectedUri, response);
        }

        protected static string GetRealJsonResponse(int page, int total, User u) =>
            $"{{\"current\": {page}, \"total\": {total}, \"results\": [{{\"first_name\": \"{u.FirstName}\", \"last_name\": \"{u.LastName}\", " +
                $"\"favorite\": {{\"color\": \"{u.Favorite!.Color}\", \"food\": \"{u.Favorite.Food}\", \"random_string\": \"{u.Favorite.RandomString}\", " +
                $"\"song\": \"{u.Favorite.Song}\"}}, \"gender\": \"{u.Gender}\", \"image\": \"{u.Image}\", \"profession\": \"{u.Profession}\", " +
                $"\"email\": \"{u.Email}\", \"age\": {u.Age}, \"country\": \"{u.Country}\", \"height\": {u.Height}, \"id\": {u.Id}}}]}}";
    }
}
