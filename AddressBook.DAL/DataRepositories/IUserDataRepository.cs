
using AddressBook.DAL.Domain;

namespace AddressBook.DAL.DataRepositories
{
    public interface IUserDataRepository : IBaseAccess<UserData>
    {
        void MarkAsFavorite(UserData obj);
        void RemoveAsFavorite(UserData obj);
    }
}
