using AddressBook.BLL.Entities;
using AddressBook.BLL.Repositories;
using AddressBook.DAL.DataRepositories;
using NUnit.Framework;

namespace AddressBook.Tests.BLLTests
{
    [TestFixture]
    class Fixture_UserCreate
    {

        [Test]
        public void can_create_user_record()
        {
            var userData = new UserDataRepository();
            var repo = new UserAddressRepo(userData);

            var user = new User {Email = "test@bll.com", FirstName = "Test", LastName = "McTest"};

            var createdUser = repo.CreateUser(user);
            Assert.IsNotNull(createdUser);
            Assert.IsNotNull(createdUser.Id);
        }

    }
}
