using InventoryBook.Common.ViewModels;
using System.Collections.Generic;

namespace InventoryBook.BLL.Services.Interfaces
{
    public interface IUserService
    {
        public void Delete(int id);

        public void Edit(UserViewModel user);

        public bool IsLoginExistsForUpdate(int id, string login);

        public UserViewModel Get(int id);

        public IEnumerable<UserViewModel> GetAll();
    }
}
