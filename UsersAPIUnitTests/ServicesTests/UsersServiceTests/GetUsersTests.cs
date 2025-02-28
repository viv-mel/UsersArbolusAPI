using AutoFixture;
using FluentAssertions;

namespace UsersAPIUnitTests.ServicesTests.UsersServiceTests
{
    public class GetUsersTests : BaseUserServiceTests
    {     

        [Fact]
        public async Task HappyPath()
        {
            // Arrange
            var page = fixture.Create<int>();

            MockSuccessfulResponse(page);

            // Act
            await target.GetUsers(page);

            // Asserts
            autoMocker.VerifyAll();
        }

        [Fact]
        public async Task WhenSuccessfulThenExpectedResultIsReturned()
        {
            // Arrange
            var page = fixture.Create<int>();
            var expectedTotal = 1;
            var expectedUri = new Uri($"{UriStr}?page={page}", UriKind.Relative);
            var expectedUser = userBuilder.WithSampleData().Create();

            using HttpResponseMessage response = new(System.Net.HttpStatusCode.OK);
            response.Content = new StringContent(GetRealJsonResponse(page, expectedTotal, expectedUser));

            MockHttpClient(expectedUri, response);

            // Act
            var result = await target.GetUsers(page);

            // Asserts
            result.Should().NotBeNull();
            result.Current.Should().Be(page);
            result.Total.Should().Be(expectedTotal);

            var userResults = result.Results;
            userResults.Should().NotBeNull();
            userResults.Count.Should().Be(expectedTotal);

            var user = userResults.First();
            user.Id.Should().Be(expectedUser.Id);
            user.FirstName.Should().Be(expectedUser.FirstName);
            user.LastName.Should().Be(expectedUser.LastName);
            user.Age.Should().Be(expectedUser.Age);
            user.Gender.Should().Be(expectedUser.Gender);
            user.Country.Should().Be(expectedUser.Country);
            user.Height.Should().Be(expectedUser.Height);
            user.Image.Should().Be(expectedUser.Image);
            user.Profession.Should().Be(expectedUser.Profession);
            user.Email.Should().Be(expectedUser.Email);

            var userFavorite = user.Favorite;
            userFavorite.Should().NotBeNull();
            userFavorite.Food.Should().Be(expectedUser.Favorite!.Food);
            userFavorite.Color.Should().Be(expectedUser.Favorite.Color);
        }

        [Fact]
        public void WhenNonSuccessfulResponseThenExceptionIsThrown()
        {
            // Arrange
            using HttpResponseMessage response = new(System.Net.HttpStatusCode.Forbidden);

            MockHttpClient(new Uri(UriStr, UriKind.Relative), response);

            // Act
            Func<Task> act = async () => await target.GetUsers(page);

            // Asserts
            act.Should().ThrowAsync<HttpRequestException>();
        }

        
        [Fact]
        public void WhenTaskIsCancelledThenExceptionIsThrown()
        {
            // Arrange
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();

            using HttpResponseMessage response = new(System.Net.HttpStatusCode.OK);

            MockHttpClient(new Uri(UriStr, UriKind.Relative), response);

            // Act
            Func<Task> act = async () => await target.GetUsers(page, cancellationTokenSource.Token);

            // Asserts
            act.Should().ThrowAsync<TaskCanceledException>();
        }
    }
}
