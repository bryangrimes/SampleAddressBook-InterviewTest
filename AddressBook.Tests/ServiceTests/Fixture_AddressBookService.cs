
using System;
using System.Collections.Generic;
using AddressBook.BLL.Entities;
using AddressBook.BLL.Repositories;
using AddressBook.BLL.Services;
using AddressBook.DAL.DataRepositories;
using NUnit.Framework;
using System.Linq;

namespace AddressBook.Tests.ServiceTests
{
    [TestFixture]
    class Fixture_AddressBookService
    {
        private readonly UserAddressRepo svcRepo;
        private readonly AddressRepo addrRepo;
        private AddressBookService svc;

        private readonly User testUser = new User {Email = "test@bll.com", FirstName = "Test", LastName = "McTest"};

        public Fixture_AddressBookService()
        {
            svcRepo = new UserAddressRepo(new UserDataRepository());
            addrRepo = new AddressRepo(new AddressDataRepository(), new TagDataRepository());
        }

        [Test]
        public void service_can_create_a_user()
        {
            svc = new AddressBookService(svcRepo, addrRepo);
            var created = svc.CreateUser(testUser);
            Assert.IsNotNull(created);
            Assert.IsTrue(created.Id > 0);
        }

        [Test]
        public void service_can_create_a_user_with_address()
        {
            svc = new AddressBookService(svcRepo, addrRepo);
            var user = svc.CreateUser(testUser);
            user.Addresses.Add(new Address { City = "test", UserId = user.Id, State = "PA", Street1 = "111 Elm" });

            var created = svc.UpdateUser(user);

            Assert.IsNotNull(created);
            Assert.IsTrue(created.Id > 0);
            Assert.IsNotNull(created.Addresses);
            Assert.AreEqual(user.Id, created.Addresses[0].UserId);
        }

        [Test]
        public void service_can_update_address()
        {
            svc = new AddressBookService(svcRepo, addrRepo);
            var user = svc.CreateUser(testUser);

            user.Addresses.Add(new Address { City = "test", UserId = user.Id, State = "PA", Street1 = "111 Elm" });
            var created = svc.UpdateUser(user);

            var oldVal = created.Addresses[0].City;

            created.Addresses[0].City = "Philly";
            var updated = svc.UpdateUser(created);

            Assert.AreNotEqual(oldVal, updated.Addresses[0].City);

        }

        [Test]
        public void service_can_update_address_collection()
        {
            svc = new AddressBookService(svcRepo, addrRepo);
            var user = svc.CreateUser(testUser);

            var tagSimulate = "flyers cup";

            var addr = new Address
            {
                Street1 = "100 Test Street",
                Street2 = "10th Fl",
                City = "Phila",
                State = "PA",
                Zip = "19147",
                Type = "Governmental" 
            };

            if (tagSimulate != String.Empty)
            {
                var tags = tagSimulate.Split(' ');
                IList<Tag> newTags = tags.Select(tag => new Tag { Name = tag }).ToList();
                addr.Tags = newTags;
            }

            user.Addresses.Add(addr);
           
            var created = svc.UpdateUser(user);

            Assert.IsTrue(created.Addresses.Count > 0);

            var oldVal = created.Addresses[0].City;

            Assert.AreEqual("Phila", oldVal);

            created.Addresses[0].City = "Philly";
            var updated = svc.UpdateUser(created);

            Assert.AreEqual("Philly", updated.Addresses[0].City);
            Assert.AreNotEqual(oldVal, updated.Addresses[0].City);

        }

        [Test]
        public void service_can_delete_address()
        {
            svc = new AddressBookService(svcRepo, addrRepo);
            var user = svc.CreateUser(testUser);
            user.Addresses.Add(new Address { City = "test", UserId = user.Id, State = "PA", Street1 = "111 Elm" });

            // added db via user update.
            var updatedUser = svc.UpdateUser(user);
            Assert.IsTrue(updatedUser.Addresses.Count > 0);

            // remove one from the list, and assert the count = 0 after the re-select of the user
            updatedUser.Addresses.Remove(updatedUser.Addresses[0]);

            var final = svc.UpdateUser(updatedUser);

            Assert.IsTrue(final.Addresses.Count == 0);

            // select user back out and verify addresses are 0
            //var userAgain = svc.GetUser(user.Id);

            //Assert.IsTrue(userAgain.Addresses.Count == 0);
        }

        [Test]
        public void service_can_update_user()
        {
            svc = new AddressBookService(svcRepo, addrRepo);
            var user = svc.CreateUser(testUser);

            var oldVal = user.Email;

            user.Email = "updated@user.com";
            var updatedUser = svc.UpdateUser(user);

            Assert.AreNotEqual(oldVal, updatedUser.Email);
        }

        [Test]
        public void service_can_delete_user()
        {
            svc = new AddressBookService(svcRepo, addrRepo);
            var user = svc.CreateUser(testUser);

            var id = user.Id;

            svc.DeleteUser(user);

            // reselect and validate it's null
            var deletedUser = svc.GetUser(id);
            Assert.IsNull(deletedUser);
        }

        [Test]
        public void can_mark_user_as_fav_but_not_address()
        {
            svc = new AddressBookService(svcRepo, addrRepo);
            var user = svc.CreateUser(testUser);

            user.Addresses.Add(new Address { City = "test", UserId = user.Id, State = "PA", Street1 = "111 Elm" });
            user = svc.UpdateUser(user);

            user.Favorite = true;
            var updatedUser = svc.UpdateUser(user);

            Assert.AreEqual(user.Favorite, updatedUser.Favorite);

            foreach (var a in updatedUser.Addresses)
            {
                Assert.IsTrue(!a.Favorite);
            }

        }

        [Test]
        public void can_mark_user_as_fav_and_an_address()
        {
            svc = new AddressBookService(svcRepo, addrRepo);
            var user = svc.CreateUser(testUser);

            user.Addresses.Add(new Address { City = "test", UserId = user.Id, State = "PA", Street1 = "111 Elm" });
            user = svc.UpdateUser(user);

            user.Addresses[0].Favorite = true;
            user.Favorite = true;
            var updatedUser = svc.UpdateUser(user);

            Assert.AreEqual(user.Favorite, updatedUser.Favorite);

            foreach (var a in updatedUser.Addresses)
            {
                Assert.IsTrue(a.Favorite);
            }
        }

        [Test]
        public void can_mark_address_as_fav_but_not_user()
        {

            svc = new AddressBookService(svcRepo, addrRepo);
            var user = svc.CreateUser(testUser);

            user.Addresses.Add(new Address { City = "test", UserId = user.Id, State = "PA", Street1 = "111 Elm" });
            user = svc.UpdateUser(user);

            user.Addresses[0].Favorite = true;
  
            foreach (var a in svc.UpdateUser(user).Addresses)
            {
                Assert.IsTrue(a.Favorite);
            }
        }

        [Test]
        public void can_create_one_user_tag()
        {
            svc = new AddressBookService(svcRepo, addrRepo);
            var user = svc.CreateUser(testUser);
            user.Tags.Add(new Tag { Name = "tag test", WhatId = user.Id });

            // update the user with the tag prop, and assert it's there after save
            var updatedUser = svc.UpdateUser(user);

            Assert.IsTrue(updatedUser.Tags != null);
            Assert.AreEqual(user.Tags[0].Name, updatedUser.Tags[0].Name);
        }

        [Test]
        public void can_create_more_than_one_user_tag()
        {
            svc = new AddressBookService(svcRepo, addrRepo);
            var user = svc.CreateUser(testUser);
            user.Tags.Add(new Tag { Name = "hammels", WhatId = user.Id });
            user.Tags.Add(new Tag { Name = "lee", WhatId = user.Id });
            user.Tags.Add(new Tag { Name = "halladay", WhatId = user.Id });

            // update the user with the tag prop, and assert it's there after save
            var updatedUser = svc.UpdateUser(user);

            Assert.IsTrue(updatedUser.Tags != null);
            Assert.IsTrue(updatedUser.Tags.Count == 3);
        }

        [Test]
        public void can_delete_one_user_tag()
        {
            svc = new AddressBookService(svcRepo, addrRepo);
            var user = svc.CreateUser(testUser);
            user.Tags.Add(new Tag { Name = "hammels", WhatId = user.Id });
            user.Tags.Add(new Tag { Name = "lee", WhatId = user.Id });
            user.Tags.Add(new Tag { Name = "halladay", WhatId = user.Id });

            // update the user with the tag prop, and assert it's there after save
            var updatedUser = svc.UpdateUser(user);
            var assertCount = updatedUser.Tags.Count;

            var toRemove = updatedUser.Tags.Where(x => x.Id > 2).First();

            updatedUser.Tags.Remove(toRemove);

            var finalUpdate = svc.UpdateUser(updatedUser);

            Assert.IsTrue(finalUpdate.Tags != null);
            Assert.IsTrue(finalUpdate.Tags.Count < assertCount);
        }

        [Test]
        public void can_delete_more_than_one_user_tag()
        {
            svc = new AddressBookService(svcRepo, addrRepo);
            var user = svc.CreateUser(testUser);
            user.Tags.Add(new Tag { Name = "halladay", WhatId = user.Id });
            user.Tags.Add(new Tag { Name = "halladay", WhatId = user.Id });
            user.Tags.Add(new Tag { Name = "halladay", WhatId = user.Id });

            // update the user with the tag prop, and assert it's there after save
            var updatedUser = svc.UpdateUser(user);
            var assertCount = updatedUser.Tags.Count;

            foreach (var tag in updatedUser.Tags.Take(2).ToList())
                updatedUser.Tags.Remove(tag);

            var finalUpdate = svc.UpdateUser(updatedUser);

            Assert.IsTrue(finalUpdate.Tags != null);
            Assert.IsTrue(finalUpdate.Tags.Count < assertCount);
        }

        [Test]
        public void can_create_one_address_tag()
        {
            svc = new AddressBookService(svcRepo, addrRepo);
            var user = svc.CreateUser(testUser);
            user.Addresses.Add(new Address { City = "test", UserId = user.Id, State = "PA", Street1 = "111 Elm" });
            user = svc.UpdateUser(user);

            user.Addresses[0].Tags.Add(new Tag { Name = "addr1", WhatId = user.Addresses[0].Id });

            var updatedUser = svc.UpdateUser(user);

            Assert.IsTrue(updatedUser.Addresses[0].Tags != null);
            Assert.AreEqual(user.Addresses[0].Tags[0].Name, updatedUser.Addresses[0].Tags[0].Name);
        }

        [Test]
        public void can_create_more_than_one_address_tag()
        {
            svc = new AddressBookService(svcRepo, addrRepo);
            var user = svc.CreateUser(testUser);
            user.Addresses.Add(new Address { City = "test", UserId = user.Id, State = "PA", Street1 = "111 Elm" });
            user = svc.UpdateUser(user);

            user.Addresses[0].Tags.Add(new Tag { Name = "addr1", WhatId = user.Id });
            user.Addresses[0].Tags.Add(new Tag { Name = "addr2", WhatId = user.Id });
            user.Addresses[0].Tags.Add(new Tag { Name = "addr2", WhatId = user.Id });

            // update the user with the tag prop, and assert it's there after save
            var updatedUser = svc.UpdateUser(user);

            Assert.IsTrue(user.Addresses[0].Tags != null);
            Assert.IsTrue(user.Addresses[0].Tags.Count == 3);
        }

        [Test]
        public void can_delete_one_address_tag()
        {
            svc = new AddressBookService(svcRepo, addrRepo);
            var user = svc.CreateUser(testUser);
            user.Addresses.Add(new Address { City = "test", UserId = user.Id, State = "PA", Street1 = "111 Elm" });
            user = svc.UpdateUser(user);

            user.Addresses[0].Tags.Add(new Tag { Name = "addr555", WhatId = user.Id });

            // update the user with the tag prop, and assert it's there after save
            var updatedUser = svc.UpdateUser(user);
            Assert.IsTrue(updatedUser.Addresses[0].Tags.Count > 0);

            updatedUser.Addresses[0].Tags.RemoveAt(0);

            var finalUpdate = svc.UpdateUser(updatedUser);

            Assert.IsTrue(finalUpdate.Addresses[0].Tags.Count == 0);
        }

        [Test]
        public void can_delete_more_than_one_address_tag()
        {
            svc = new AddressBookService(svcRepo, addrRepo);
            var user = svc.CreateUser(testUser);
            user.Addresses.Add(new Address { City = "test", UserId = user.Id, State = "PA", Street1 = "111 Elm" });
            user = svc.UpdateUser(user);

            user.Addresses[0].Tags.Add(new Tag { Name = "addr3", WhatId = user.Id });
            user.Addresses[0].Tags.Add(new Tag { Name = "addr3", WhatId = user.Id });
            user.Addresses[0].Tags.Add(new Tag { Name = "addr3", WhatId = user.Id });

            // update the user with the tag prop, and assert it's there after save
            var updatedUser = svc.UpdateUser(user);
            var assertCount = updatedUser.Addresses[0].Tags.Count;

            foreach (var tag in updatedUser.Addresses[0].Tags.Take(2).ToList())
                updatedUser.Addresses[0].Tags.Remove(tag);

            var finalUpdate = svc.UpdateUser(updatedUser);

            Assert.IsTrue(finalUpdate.Addresses[0].Tags != null);
            Assert.IsTrue(finalUpdate.Addresses[0].Tags.Count < assertCount);
        }

        [Test]
        public void can_filter_users_with_equals()
        {
            svc = new AddressBookService(svcRepo, addrRepo);
            var users = svc.GetUsersByFilter("FirstName", "eq", "Test");
            Assert.IsNotNull(users);
            Assert.IsTrue(users.Count>0);
        }

        [Test]
        public void can_filter_users_with_not_equals()
        {
            svc = new AddressBookService(svcRepo, addrRepo);
            var users = svc.GetUsersByFilter("LastName", "ne", "Fluffy");
            Assert.IsNotNull(users);
            Assert.IsTrue(users.Count > 0);
        }

        [Test]
        public void can_filter_users_with_contains()
        {
            svc = new AddressBookService(svcRepo, addrRepo);
            var users = svc.GetUsersByFilter("Email", "co", "test@");
            Assert.IsNotNull(users);
            Assert.IsTrue(users.Count > 0);
        }

        [Test]
        public void can_filter_users_with_starts_with()
        {
            svc = new AddressBookService(svcRepo, addrRepo);
            var users = svc.GetUsersByFilter("FirstName", "sw", "Te");
            Assert.IsNotNull(users);
            Assert.IsTrue(users.Count > 0);
        }

        [Test]
        public void can_filter_users_with_ends_with()
        {
            svc = new AddressBookService(svcRepo, addrRepo);
            var users = svc.GetUsersByFilter("Email", "ew", ".com");
            Assert.IsNotNull(users);
            Assert.IsTrue(users.Count > 0);
        }

    }
}
