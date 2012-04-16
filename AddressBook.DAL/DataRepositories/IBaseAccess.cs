using System.Collections.Generic;
using AddressBook.DAL.Domain;

namespace AddressBook.DAL.DataRepositories
{
    public interface IBaseAccess<T> where T : BaseData
    {
        T SelectById(int id);
        IList<T> SelectALL();
        IList<T> SelectMultipleById(IList<int> ids);
        IList<T> SelectMultipleByFieldAndValue(string field, string value);
        IList<T> SelectMultipleByFieldAndValue(string filter);
        T Update(T obj);
        T Add(T obj);
        
        // voids
        void Delete(T obj);
        
    }
}
