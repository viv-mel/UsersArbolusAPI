using AutoFixture;
using FluentAssertions;
using UsersArbolusAPI.Models;

namespace UsersAPIUnitTests.ServicesTests.UsersServiceTests
{
    public class GetUsersByGenderTests : BaseUserServiceTests
    {
        const int females = 2;
        const int males = 2;
        const int others = 0;
        readonly UserList userList = new();

        public GetUsersByGenderTests()
        {
            userList.Results.AddRange(fixture.Build<User>().With(u => u.Gender, Gender.Female).CreateMany(females));
            userList.Results.AddRange(fixture.Build<User>().With(u => u.Gender, Gender.Male).CreateMany(males));
            userList.Total = userList.Results.Count;
        }

        [Theory]
        [InlineData(Gender.Female)]
        [InlineData(Gender.Male)]
        [InlineData(Gender.Other)]
        public async Task HappyPath(string gender)
        {
            // Arrange
            MockSuccessfulResponse(page);

            // Act
            await target.GetUsersByGender(gender, page);

            // Asserts
            autoMocker.VerifyAll();
        }

        [Theory]
        [InlineData(Gender.Female, females)]
        [InlineData(Gender.Male, males)]
        [InlineData(Gender.Other, others)]
        public async Task WhenUsersWithSpecificGenderExistThenUsersAreReturned(string gender, int expectedCount)
        {
            // Arrange
            MockSuccessfulUserList(userList);

            // Act
            var result = await target.GetUsersByGender(gender, page);

            // Asserts
            result.Count().Should().Be(expectedCount);
            result.Any(u => u.Gender != gender).Should().BeFalse();
        }
    }
}
