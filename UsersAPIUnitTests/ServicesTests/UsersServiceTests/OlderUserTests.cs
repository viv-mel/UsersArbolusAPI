using AutoFixture;
using FluentAssertions;
using UsersArbolusAPI.Models;

namespace UsersAPIUnitTests.ServicesTests.UsersServiceTests
{
    public class OlderUserTests : BaseUserServiceTests
    {
        [Fact]
        public async Task HappyPath()
        {
            // Arrange
            var usersCount = 3;
            UserList userList = new()
            {
                Results = fixture.Build<User>()
                .With(u => u.Gender, Gender.Female)
                .CreateMany(usersCount)
                .ToList()
            };
            var olderUser = userList.Results.MaxBy(u => u.Age);

            MockSuccessfulUserList(userList);

            // Act
            var result = await target.GetOlderUser(page);

            // Asserts
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(olderUser);
            autoMocker.VerifyAll();
        }
    }
}
