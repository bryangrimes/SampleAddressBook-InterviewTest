using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AddressBook.DAL.Access;
using AddressBook.DAL.Domain;
using AddressBook.DAL.DataRepositories;
using NHibernate;
using NUnit.Framework;

namespace AddressBook.Tests.PersistanceTests
{
    [TestFixture]
    class Fixture_TagDataRepo
    {
        [SetUp]
        public void SetupContext()
        {
            CreateInitialData();
        }

        private readonly UserData[] _users = new[]
        { new UserData {Email = "bryan@test.com", FirstName = "Bryan", LastName = "Grimes", Notes = "this is a unit test User"},
          new UserData {Email = "parker@test.com", FirstName = "Parker", LastName = "Grimes", Notes = "this is another unit test User"},
          new UserData {Email = "avery@test.com", FirstName = "Avery", LastName = "Grimes", Notes = "this is another unit test User"} 
        };

        private readonly AddressData[] _addrs = new[]
        { 
            new AddressData{Street1 = "111 Elm Street", Street2 = "Suite 200", City = "Philadelphia", State = "PA", Zip = "19107", Type = "Business"},
            new AddressData{Street1 = "222 W Spruce Street", Street2 = "", City = "Beverly Hills", State = "CA", Zip = "90210", Type = "Residence"},
            new AddressData{Street1 = "1274 S Ridge Ave", Street2 = "Apt A", City = "New York", State = "NY", Zip = "010101", Type = "Residence"},
            new AddressData{Street1 = "1234 E Reed St", Street2 = "Bldg 5", City = "Philadelphia", State = "PA", Zip = "19147", Type = "Business"},
            new AddressData{Street1 = "1523 s 131th St", Street2 = "", City = "Philadelphia", State = "PA", Zip = "19147", Type = "Residence"},
        };

        private void CreateInitialData()
        {
            using (ISession session = NBHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                foreach (var u in _users)
                    session.Save(u); transaction.Commit();       
            }
            
            using (ISession session = NBHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                foreach (var a in _addrs)
                    session.Save(a); transaction.Commit();
            }  
        }

        [Test]
        public void can_create_tag_on_user()
        {
            var repo = new TagDataRepository();

            var userRepo = new UserDataRepository();
            var selectUser = userRepo.SelectById(1);

            Assert.IsNotNull(selectUser);

            TagData tag = new TagData {Name = "philles", WhatId = selectUser.Id};
            repo.Add(tag);

             using (ISession session = NBHelper.OpenSession())
            {
                var result = session.Get<TagData>(tag.Id);
                Assert.IsNotNull(result);
                Assert.AreNotSame(tag, result);
                Assert.AreEqual(tag.WhatId, result.WhatId);
                Assert.IsNotNull(result.Id);
            }
        }
    }
}
