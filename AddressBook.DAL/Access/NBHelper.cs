using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AddressBook.DAL.Domain;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace AddressBook.DAL.Access
{
    public class NBHelper
    {
        private static ISessionFactory _sessionFactory;
       
        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    var configuration = new Configuration();
                    configuration.Configure();
                    configuration.AddAssembly(Assembly.GetCallingAssembly());
                    //new SchemaExport(configuration).Execute(false, true, false);
                    _sessionFactory = configuration.BuildSessionFactory();

                }
                return _sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
