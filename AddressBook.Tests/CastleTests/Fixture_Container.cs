using AddressBook.BLL.Entities;
using AddressBook.BLL.Services;
using Castle.Windsor;
using NUnit.Framework;
using AddressBook.BLL.Components;

namespace AddressBook.Tests.CastleTests
{
    [TestFixture]
    class Fixture_Container
    {
      private IWindsorContainer _container;

        [TestFixtureSetUp]
        public void CreateContainer()
        {
            _container = new WindsorContainer();

            var reg = new ComponentRegister();
            reg.GetComponents(_container);
        }

        [Test]
        public void should_create_container_with_at_least_one_type()
        {
            var svc = _container.Resolve<IAddressBookService>();
            Assert.IsNotNull(svc);
        }

        // just one sanity test on the container.
        [Test]
        public void should_be_able_to_get_user_repo_and_create_user_from_container()
        {
            var svc = _container.Resolve<IAddressBookService>();
            var created = svc.CreateUser(new User{Email = "bryan@bryan.com", FirstName = "bryan", Notes = "testing"});
            Assert.IsNotNull(created);
            Assert.IsTrue(created.Id > 0);
        }
    }
}
