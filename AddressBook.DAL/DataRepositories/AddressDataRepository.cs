using System;
using System.Collections.Generic;
using AddressBook.DAL.Domain;

namespace AddressBook.DAL.DataRepositories
{
    public class AddressDataRepository : BaseRepository<AddressData>, IAddressDataRepository
    {
        public AddressData SelectById(int id)
        {
            return SelectSingleInternalById(id);
        }

        public IList<AddressData> SelectALL()
        {
            return SelectMultipleInternalALL();
        }

        public IList<AddressData> SelectMultipleById(IList<int> ids)
        {
            return SelectMultipleInternalById(ids);
        }

        public IList<AddressData> SelectMultipleByFieldAndValue(string field, string value)
        {
            return SelectMultipleInternalByFieldAndValue(field, value);
        }

        public IList<AddressData> SelectMultipleByFieldAndValue(string filter)
        {
            throw new NotImplementedException();
        }

        public AddressData Add(AddressData obj)
        {
           return AddRecord(obj);
        }

        public void Delete(AddressData obj)
        {
            DeleteRecord(obj);
        }

        public AddressData Update(AddressData obj)
        {
            return UpdateRecord(obj);
        }

       /* public IEnumerable<Address> GetByZip(string zip)
        {
            return SelectMultipleInternalByFieldAndValue("Zip", zip);
        }*/

        public IEnumerable<AddressData> GetByFuzzyStreetCityZip(string street, string city, string zip)
        {
            /* // use only fields populated.
             string q = "from Address a where ";
             string s = (street == String.Empty ? "a.street like '%:street%'" : String.Empty);
             string c = (city == String.Empty ? "a.city like :'%city%'" : String.Empty);
             string z = (city == String.Empty ? "a.zip like :'%zip%'" : String.Empty);

             if (s != String.Empty)
             {
                
             }

             using (ISession session = NBHelper.OpenSession())
             {
                 var query =
                     session.CreateQuery(
                         "from Address a where a.street like '%:street%' or a.city like :'%city%' or a.zip like :'%zip%'");
                     query.SetString()
             }
                // return session.CreateCriteria<Address>().Add(Restrictions.Eq("Zip", zip)).List<Address>();
             * */
            return null;
         }
            

        public void MarkAsFavorite(AddressData obj)
        {
            obj.Favorite = true;
            UpdateRecord(obj);
        }

        public void RemoveAsFavorite(AddressData obj)
        {
            obj.Favorite = false;
            UpdateRecord(obj);
        }
    }
}
