using InventoryBook.BLL.Services.Interfaces;
using InventoryBook.Common;
using InventoryBook.Common.ViewModels;
using InventoryBook.DAL.Repositories.Interfaces;
using System.Linq;

namespace InventoryBook.BLL.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICryptographerService _cryptographerService;

        public AccountService(IUserRepository userRepository, ICryptographerService cryptographerService)
        {
            _userRepository = userRepository;
            _cryptographerService = cryptographerService;
        }

        public bool IsLoginExists(string login) => _userRepository.IsLoginExists(login);

        public void Register(RegisterViewModel registerViewModel)
        {
            var passwordHash = _cryptographerService.Encrypt(registerViewModel.Password, out byte[] passwordSalt);
            _userRepository.Register(registerViewModel.Login, passwordHash, passwordSalt, registerViewModel.Role);
        }

        public UserViewModel Login(LoginViewModel loginViewModel) 
        {
            UserViewModel userViewModel = null;

            if (IsLoginExists(loginViewModel.Login))
            {
                var userHash = _cryptographerService.Encrypt(loginViewModel.Password, _userRepository.GetSaltByLogin(loginViewModel.Login));
                var dbHash = _userRepository.GetHashByLogin(loginViewModel.Login);

                if (userHash.SequenceEqual(dbHash))
                {
                    userViewModel = _userRepository.Login(loginViewModel.Login, userHash).ToUserViewModel();
                }
            }

            return userViewModel;
        } 
    }
}
