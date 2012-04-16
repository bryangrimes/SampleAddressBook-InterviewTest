using System;
using System.Collections.Generic;
using AddressBook.DAL.Domain;

namespace AddressBook.DAL.DataRepositories
{
    public class TagDataRepository : BaseRepository<TagData>, ITagDataRepository
    {
        public TagData SelectById(int id)
        {
            return SelectSingleInternalById(id);
        }

        public IList<TagData> SelectALL()
        {
            return SelectMultipleInternalALL();
        }

        public IList<TagData> SelectMultipleById(IList<int> ids)
        {
            return SelectMultipleInternalById(ids);
        }

        public IList<TagData> SelectMultipleByFieldAndValue(string field, string value)
        {
            return SelectMultipleInternalByFieldAndValue(field, value);
        }

        public IList<TagData> SelectMultipleByFieldAndValue(string filter)
        {
            throw new NotImplementedException();
        }

        public TagData Update(TagData obj)
        {
            return UpdateRecord(obj);
        }

        public TagData Add(TagData obj)
        {
            return AddRecord(obj);
        }

        public void Delete(TagData obj)
        {
            DeleteRecord(obj);
        }
    }
}
