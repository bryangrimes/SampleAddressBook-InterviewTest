
using System.Collections.Generic;
using AddressBook.DAL.Domain;

namespace AddressBook.DAL.DataRepositories
{
    public interface IAddressDataRepository : IBaseAccess<AddressData>
    {
        //IEnumerable<Address> GetByZip(string zip);
        IEnumerable<AddressData> GetByFuzzyStreetCityZip(string sreet, string city, string zip);
        void MarkAsFavorite(AddressData obj);
        void RemoveAsFavorite(AddressData obj);
    }
}
