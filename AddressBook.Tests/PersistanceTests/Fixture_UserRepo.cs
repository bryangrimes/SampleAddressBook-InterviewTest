using AddressBook.DAL.Access;
using AddressBook.DAL.Domain;
using AddressBook.DAL.DataRepositories;
using NHibernate;
using NUnit.Framework;

namespace AddressBook.Tests.PersistanceTests
{
    [TestFixture]
    class Fixture_UserRepo
    {

        [SetUp]
        public void SetupContext()
        {
            CreateInitialData();
        }

        private readonly UserData[] _users = new[]
        { new UserData {Email = "bryan@test.com", FirstName = "Bryan", LastName = "Grimes", Notes = "this is a unit test User"},
          new UserData {Email = "parker@test.com", FirstName = "Parker", LastName = "Grimes", Notes = "this is another unit test User"} ,
          new UserData {Email = "avery@test.com", FirstName = "Avery", LastName = "Grimes", Notes = "this is another unit test User"} 
        };

        private void CreateInitialData()
        {
            using (ISession session = NBHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                foreach (var u in _users)
                    session.Save(u); transaction.Commit();
            }
        }

        [Test]
        public void can_create_new_record()
        {
            var repo = new UserDataRepository();
            UserData user = new UserData { Email = "unit@test.com", FirstName = "Unit", LastName = "test", Notes = "Unit test user" };           
            repo.Add(user);
            
            using (ISession session = NBHelper.OpenSession())
            {
                var result = session.Get<UserData>(user.Id);
                Assert.IsNotNull(result);
                Assert.AreNotSame(user, result);
                Assert.AreEqual(user.Email, result.Email);
                Assert.IsNotNull(result.Id);
            }
        }

        [Test]
        public void mark_as_fav()
        {
            var user = _users[0];
            user.Favorite = true;

            var repo = new UserDataRepository();
            repo.MarkAsFavorite(user);

            // get it back out and verify
            using (ISession session = NBHelper.OpenSession())
            {
                var result = session.Get<UserData>(user.Id);
                Assert.AreEqual(true, result.Favorite);
            }

        }

        [Test]
        public void unmark_as_fav()
        {
            // first mark as fav then unmark it...to be complete.
            var user = _users[0];
            user.Favorite = true;

            var repo = new UserDataRepository();
            repo.MarkAsFavorite(user);

            // get it back out and verify
            using (ISession session = NBHelper.OpenSession())
            {
                var result = session.Get<UserData>(user.Id);
                Assert.AreEqual(true, result.Favorite);
            }

            repo = new UserDataRepository();
            repo.RemoveAsFavorite(user);

            user.Favorite = false;

            // get it back out and verify
            using (ISession session = NBHelper.OpenSession())
            {
                var result = session.Get<UserData>(user.Id);
                Assert.AreEqual(false, result.Favorite);
            }
        }
    }
}
