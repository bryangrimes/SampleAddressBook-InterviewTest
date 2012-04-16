using System.Collections.Generic;
using AddressBook.BLL.Entities;
using AddressBook.BLL.Services;

namespace AddressBook.BLL.Repositories
{
    public interface IAddressRepo
    {
        IList<Address> GetUserAddresses(int id);
        Address GetSingleAddress(int id);
        Address UpdateAddress(Address obj);
        Address CreateAddress(Address obj);
        //void DeleteAddress(Address obj);
        //User DeleteAddress(int id);
        User DeleteAddress(User user);
         
        void MarkAsFavorite(Address obj);
        void RemoveAsFavorite(Address obj);
        void MarkAsFavorite(int id);
        void RemoveAsFavorite(int id);
    }
}
