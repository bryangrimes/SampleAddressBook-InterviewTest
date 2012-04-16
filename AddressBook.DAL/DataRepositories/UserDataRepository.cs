using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AddressBook.DAL.Domain;

namespace AddressBook.DAL.DataRepositories
{
    public class UserDataRepository : BaseRepository<UserData>, IUserDataRepository
    {
        public UserData SelectById(int id)
        {
            return SelectSingleInternalById(id);
        }

        public IList<UserData> SelectALL()
        {
            return SelectMultipleInternalALL();
        }

        public IList<UserData> SelectMultipleById(IList<int> ids)
        {
            return SelectMultipleById(ids);
        }

        public IList<UserData> SelectMultipleByFieldAndValue(string field, string value)
        {
            return SelectMultipleInternalByFieldAndValue(field, value);
        }

        public IList<UserData> SelectMultipleByFieldAndValue(string filter)
        {
            throw new NotImplementedException();
        }

        public UserData Add(UserData obj)
        {
            return AddRecord(obj);
        }

        public void Delete(UserData obj)
        {
            DeleteRecord(obj);
        }

        public UserData Update(UserData obj)
        {
            return UpdateRecord(obj);
        }

        public void MarkAsFavorite(UserData obj)
        {
            obj.Favorite = true;
            UpdateRecord(obj);
        }

        public void RemoveAsFavorite(UserData obj)
        {
            obj.Favorite = false;
            UpdateRecord(obj);
        }
    }
}
