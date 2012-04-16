using AddressBook.DAL.Domain;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace AddressBook.Tests
{
    [TestFixture]
    public class Fixture_CreateSchema
    {
        [Test]
        public void Create_Schema()
        {
            var cfg = new Configuration();
            cfg.Configure();
            cfg.AddAssembly(typeof(AddressData).Assembly);
            new SchemaExport(cfg).Execute(false, true, false);
        }
    }
}
