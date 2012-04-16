using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AddressBook.BLL.Entities;

namespace AddressBook.BLL.Services
{
    public interface IAddressBookService
    {
        IList<User> GetAllUsers();
        User GetUser(int id);
        User CreateUser(User user);
        void DeleteUser(User user);
        User UpdateUser(User user);

        Address CreateAddress(Address address);

        IList<User>GetUsersByFilter(string field, string op, string value);

        //void MarkAddressFavorite(int id);
        //void MarkAddressNotFavorite(int id);
    }
}
