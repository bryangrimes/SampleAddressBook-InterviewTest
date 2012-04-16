using System.Collections;
using System.Collections.Generic;
using AddressBook.BLL.Entities;
using AddressBook.BLL.Repositories;
using AddressBook.DAL.DataRepositories;
using NUnit.Framework;

namespace AddressBook.Tests.BLLTests
{
    [TestFixture]
    class Fixture_AddressCreate
    {
        private User _user;
        private UserAddressRepo _userRepo;

        [TestFixtureSetUp]
        public void  create_user_for_test()
        {
            var userData = new UserDataRepository();
            _userRepo = new UserAddressRepo(userData);
            _user = _userRepo.CreateUser(new User { Email = "test@bll.com", FirstName = "Test", LastName = "McTest" });
        }

        [Test]
        public void can_create_address_record()
        {       

            var userData = new AddressDataRepository();
            var tagData = new TagDataRepository();
            //var repo = new AddressRepo(userData, tagData);

            //IList<Address> lstAddr = new List<Address> };

            _user.Addresses.Add(new Address {City = "test", UserId = _user.Id});

            //var createdAdddress = repo.CreateAddress(address);
            var updatedUser = _userRepo.UpdateUser(_user);


            Assert.IsNotNull(updatedUser.Addresses);
            Assert.IsNotNull(updatedUser.Addresses[0].Id);
            Assert.IsFalse(updatedUser.Addresses[0].Favorite);
        }

    }
}
