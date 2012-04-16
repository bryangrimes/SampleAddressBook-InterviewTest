
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AddressBook.DAL.Access;
using AddressBook.DAL.Domain;
using AddressBook.DAL.DataRepositories;
using NHibernate;
using NUnit.Framework;

namespace AddressBook.Tests.PersistanceTests
{
    [TestFixture]
    class Fixture_AddressRepo
    {
        [SetUp]
        public void SetupContext()
        {
            CreateInitialData();
        }

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
                foreach (var a in _addrs)
                    session.Save(a); transaction.Commit();
            }
        }

        [Test]
        public void can_create_new_record()
        {
            var repo = new AddressDataRepository();
            AddressData addr = new AddressData{Street1 = "XXX N Test Steet", Street2 = "", City = "Philadelphia", State = "PA", Zip = "19147", Type = "TEST"};
            repo.Add(addr);

            using (ISession session = NBHelper.OpenSession())
            {
                var result = session.Get<AddressData>(addr.Id);
                Assert.IsNotNull(result);
                Assert.AreNotSame(addr, result);
                Assert.AreEqual(addr.Street1, result.Street1);
            }
        }

        [Test]
        public void can_update_record()
        {
            var addr = _addrs[0];
            addr.Type = "Updated By TestFixture";

            var repo = new AddressDataRepository();
            repo.Update(addr);

            // get it back out and verify
            using (ISession session = NBHelper.OpenSession())
            {
                var result = session.Get<AddressData>(addr.Id);
                Assert.AreEqual(addr.Type, result.Type);
            }
        }

        [Test]
        public void can_delete_record()
        {
            var addr = _addrs[0];
            var repo = new AddressDataRepository();
            repo.Delete(addr);

            // get it back out and verify...that it's not there...!
            using (ISession session = NBHelper.OpenSession())
            {
                var result = session.Get<AddressData>(addr.Id);
                Assert.IsNull(result);
            }
        }

        [Test]
        public void can_get_by_single_id()
        {
            var repo = new AddressDataRepository();
            var result = repo.SelectById(_addrs[2].Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(_addrs[2].Id, result.Id);
        }

        [Test]
        public void can_get_by_multiple_ids()
        {
            // throw the ids into a list, and then pull the data back
            IList<int> ids = _addrs.Select(a => a.Id).ToList();

            var repo = new AddressDataRepository();
            var results = repo.SelectMultipleById(ids);        
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count() == ids.Count);
        }

        [Test]
        public void can_get_all()
        {
            var repo = new AddressDataRepository();
            Assert.IsNotNull(repo.SelectALL());
        }

        [Test]
        public void can_get_by_field_and_value()
        {
            var repo = new AddressDataRepository();
            var results = repo.SelectMultipleByFieldAndValue("Type", "Residence");

            foreach (var a in results)
                Assert.AreEqual("Residence", a.Type);
        }

        [Test]
        public void can_get_by_zip()
        {
            var repo = new AddressDataRepository();
            var results = repo.SelectMultipleByFieldAndValue("Zip","19147");

            foreach (var a in results)
                Assert.AreEqual("19147", a.Zip);
        }

        [Test]
        public void can_get_by_fuzzy_match()
        {
               
        }

        [Test]
        public void mark_as_fav()
        {
            var addr = _addrs[0];
            addr.Favorite = true;

            var repo = new AddressDataRepository();
            repo.MarkAsFavorite(addr);
           
            // get it back out and verify
            using (ISession session = NBHelper.OpenSession())
            {
                var result = session.Get<AddressData>(addr.Id);
                Assert.AreEqual(true, result.Favorite);
            }

        }

        [Test]
        public void unmark_as_fav()
        {
            // first mark as fav then unmark it...to be complete.
            var addr = _addrs[0];
            addr.Favorite = true;

            var repo = new AddressDataRepository();
            repo.MarkAsFavorite(addr);

            // get it back out and verify
            using (ISession session = NBHelper.OpenSession())
            {
                var result = session.Get<AddressData>(addr.Id);
                Assert.AreEqual(true, result.Favorite);
            }

            repo = new AddressDataRepository();
            repo.RemoveAsFavorite(addr);

            addr.Favorite = false;

            // get it back out and verify
            using (ISession session = NBHelper.OpenSession())
            {
                var result = session.Get<AddressData>(addr.Id);
                Assert.AreEqual(false, result.Favorite);
            }
        }
    }
}
