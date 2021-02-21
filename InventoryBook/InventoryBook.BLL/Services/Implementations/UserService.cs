using InventoryBook.BLL.Services.Interfaces;
using InventoryBook.Common.ViewModels;
using InventoryBook.DAL.Repositories.Interfaces;
using System.Collections.Generic;
using InventoryBook.Common;

namespace InventoryBook.BLL.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) => _userRepository = userRepository;

        public void Delete(int id) => _userRepository.Delete(id);

        public void Edit(UserViewModel userViewModel) => _userRepository.Update(userViewModel.ToUser());

        public bool IsLoginExistsForUpdate(int id, string login) => _userRepository.IsLoginExistsForUpdate(id, login);

        public UserViewModel Get(int id) => _userRepository.Get(id)?.ToUserViewModel();

        public IEnumerable<UserViewModel> GetAll() => _userRepository.GetAll().ToUserViewModelList();
    }
}
