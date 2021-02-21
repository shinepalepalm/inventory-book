using InventoryBook.Common.ViewModels;

namespace InventoryBook.BLL.Services.Interfaces
{
    public interface IAccountService
    {
        public bool IsLoginExists(string login);

        public void Register(RegisterViewModel registerViewModel);

        public UserViewModel Login(LoginViewModel loginViewModel);
    }
}
