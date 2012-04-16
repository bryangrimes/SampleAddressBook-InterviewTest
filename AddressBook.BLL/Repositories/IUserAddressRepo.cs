using System.Collections.Generic;
using AddressBook.BLL.Entities;
using AddressBook.BLL.Services;

namespace AddressBook.BLL.Repositories
{
    public interface IUserAddressRepo
    {
        IList<User> GetAllUsers();
        User GetSingleUser(int id);
        User UpdateUser(User obj);
        User CreateUser(User obj);
        void DeleteUser(User obj);
        void MarkAsFavorite(User obj);
        void RemoveAsFavorite(User obj);
    }
}
