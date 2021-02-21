using InventoryBook.Common.Enums;
using InventoryBook.Common.Models;
using System.Collections.Generic;

namespace InventoryBook.DAL.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public void Delete(int id);

        public void Update(User user);

        public void Register(string login, byte[] hash, byte[] salt, Role role);

        public bool IsLoginExists(string login);

        public bool IsLoginExistsForUpdate(int id, string login);

        public byte[] GetHashByLogin(string login);

        public byte[] GetSaltByLogin(string login);

        public User Login(string login, byte[] passwordHash);

        public User Get(int id);

        public IEnumerable<User> GetAll();
    }
}
