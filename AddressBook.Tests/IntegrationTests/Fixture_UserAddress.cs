using AddressBook.DAL.Access;
using AddressBook.DAL.Domain;
using AddressBook.DAL.DataRepositories;
using NHibernate;
using NUnit.Framework;

namespace AddressBook.Tests.IntegrationTests
{
    [TestFixture]
    class Fixture_UserAddress
    {
        /*
         * The point here is to get past the tests of did User A create, or Address B create.
         * 
         * Context is needed.  
         * 1- Create a User, then create addresses related to that User.
         * 2- Delete the User and see the addresses delete.
         * 
        */

        [Test]
        public void can_create_new_user_with_address()
        {
            var userRepo = new UserDataRepository();
            var user = new UserData { Email = "unit@test.com", FirstName = "Unit", LastName = "test", Notes = "Unit test user" };
            userRepo.Add(user);

            UserData returnUser;

            using (ISession session = NBHelper.OpenSession())
            {
                returnUser = session.Get<UserData>(user.Id);
                Assert.IsNotNull(returnUser);
                Assert.IsNotNull(returnUser.Id);
            }

            var addrRepo = new AddressDataRepository();
            // with the User, create an address.
            var addr = new AddressData {City = "Testville", State="NJ", Street1 = "Test1", Street2 = "Test2", UserId = returnUser.Id};
            addrRepo.Add(addr);

            using (ISession session = NBHelper.OpenSession())
            {
                var result = session.Get<AddressData>(addr.Id);
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);
                Assert.AreEqual(returnUser.Id, result.UserId);
            }
        }

        [Test]
        public void should_delete_user_and_addresses()
        {
            var userRepo = new UserDataRepository();
            var user = new UserData { Email = "unit@test.com", FirstName = "Unit", LastName = "test", Notes = "Unit test user" };
            userRepo.Add(user);

            UserData returnUser;

            using (ISession session = NBHelper.OpenSession())
                returnUser = session.Get<UserData>(user.Id);

            var addrRepo = new AddressDataRepository();
            var addr = new AddressData { City = "Testville", State = "NJ", Street1 = "Test1", Street2 = "Test2", UserId = returnUser.Id };
            addrRepo.Add(addr);

            AddressData returnAddress;

            using (ISession session = NBHelper.OpenSession())
                returnAddress = session.Get<AddressData>(addr.Id);


            // delete the User
            userRepo.Delete(returnUser);
            using (ISession session = NBHelper.OpenSession())
            {
                var result = session.Get<UserData>(returnUser.Id);
                Assert.IsNull(result);
            }


        }

        [Test]
        public void can_select_user_straight_up()
        {
            
        }
    }
}
