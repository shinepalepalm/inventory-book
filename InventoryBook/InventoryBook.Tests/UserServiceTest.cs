using InventoryBook.BLL.Services.Implementations;
using InventoryBook.DAL.Repositories.Interfaces;
using Moq;
using Xunit;

namespace InventoryBook.Tests
{
    public class UserServiceTest
    {
        private const int Id = 1;
        private const string Login = "login";
        private readonly UserService _userService;
        private readonly Mock<IUserRepository> _userRepositoryMock;

        public UserServiceTest()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userService = new UserService(_userRepositoryMock.Object);
        }

        [Fact]
        public void Get_ShouldReturnNull_WhenUserDoesNotExist()
        {
            _userRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(() => null);

            var userViewModel = _userService.Get(Id);

            Assert.Null(userViewModel);
        }

        [Fact]
        public void IsLoginExistsForUpdate_ShouldReturnFalse_WhenLoginDoesNotExistForUpdate()
        {
            _userRepositoryMock.Setup(x => x.IsLoginExistsForUpdate(It.IsAny<int>(), It.IsAny<string>())).Returns(false);

            var result = _userService.IsLoginExistsForUpdate(Id, Login);

            Assert.False(result);
        }

        [Fact]
        public void IsLoginExistsForUpdate_ShouldReturnTrue_WhenLoginExistsForUpdate()
        {
            _userRepositoryMock.Setup(x => x.IsLoginExistsForUpdate(Id, Login)).Returns(true);

            var result = _userService.IsLoginExistsForUpdate(Id, Login);

            Assert.True(result);
        }
    }
}
