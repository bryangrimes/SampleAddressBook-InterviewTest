using AddressBook.BLL.Repositories;
using AddressBook.BLL.Services;
using AddressBook.DAL.DataRepositories;
using NUnit.Framework;

namespace AddressBook.Tests
{
    [TestFixture]
    class Fixture_LiveData
    {

         [Test]
         public void can_select_users_from_database_with_live_data()
         {
            AddressBookService svc = new AddressBookService(new UserAddressRepo(new UserDataRepository()), new AddressRepo(new AddressDataRepository(), new TagDataRepository()));
             var users = svc.GetAllUsers();
             Assert.IsTrue(users.Count > 0);
         }
    }
}
