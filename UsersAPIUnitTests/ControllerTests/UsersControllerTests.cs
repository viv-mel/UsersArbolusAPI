using AutoFixture;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using UsersArbolusAPI.Controllers;
using UsersArbolusAPI.Models;
using UsersArbolusAPI.Services;

namespace UsersAPIUnitTests.ControllerTests
{
    public class UsersControllerTests
    {
        public readonly Fixture fixture = new();
        public readonly AutoMocker autoMocker = new();
        private readonly UsersController target;

        public UsersControllerTests()
        {
            target = autoMocker.CreateInstance<UsersController>();
        }

        [Fact]
        public async Task HappyPath()
        {
            // Arrange
            var page = 2;
            var expectedResult = fixture.Build<UserList>()
                .With(r => r.Results, fixture.CreateMany<User>().ToList())
                .Create();

            autoMocker.Setup<IUsersService, Task<UserList>>(s => s.GetUsers(page, default))
                .ReturnsAsync(expectedResult)
                .Verifiable();

            // Act
            var result = await target.Get(page);

            // Asserts
            autoMocker.VerifyAll();
            result.Should().Be(expectedResult);
        }
    }
}
