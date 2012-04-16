using System.Collections.Generic;
using System.Linq;
using AddressBook.DAL.Access;
using AddressBook.DAL.Domain;
using NHibernate;
using NHibernate.Criterion;

namespace AddressBook.DAL.DataRepositories
{
    public abstract class BaseRepository<T> where T : BaseData
    {

        // VOIDS
        protected internal virtual T AddRecord(T obj)
        {
            // create session/trans and commit.
            using (ISession session = NBHelper.OpenSession())
            using (ITransaction tran = session.BeginTransaction())
            {
                session.Save(obj);
                tran.Commit();
                session.Flush();
            }

            return obj;
        }

        protected internal virtual void DeleteRecord(T obj)
        {
            // create session/trans and commit.
            using (ISession session = NBHelper.OpenSession())
            using (ITransaction tran = session.BeginTransaction())
            {
                session.Delete(obj);
                tran.Commit();
                session.Flush();
            }
        }

        protected internal virtual T UpdateRecord(T obj)
        {
            // create session/trans and commit.
            using (ISession session = NBHelper.OpenSession())
            using (ITransaction tran = session.BeginTransaction())
            {
                session.Update(obj);
                tran.Commit();
                session.Flush();
            }

            return obj;
        }

        // RETURNS
        protected T SelectSingleInternalById(int Id)
        {

            // create session/trans and commit.
            using (ISession session = NBHelper.OpenSession())
            using (ITransaction tran = session.BeginTransaction())
            {
                var ret = session.Get<T>(Id);
                tran.Commit();
                session.Flush();
                return ret;
            }
        }

        protected IList<T> SelectMultipleInternalALL()
        {
            // create session/trans and commit.
            using (ISession session = NBHelper.OpenSession())
            using (ITransaction tran = session.BeginTransaction())
            {
                var ret = session.CreateCriteria<T>().List<T>();
                tran.Commit();
                session.Flush();
                return ret;
            }
        }

        protected IList<T> SelectMultipleInternalById(IEnumerable<int> ids)
        {
            // create session/trans and commit.
            using (ISession session = NBHelper.OpenSession())
            using (ITransaction tran = session.BeginTransaction())
            {
                var ret =  session.CreateCriteria<T>().Add(Restrictions.In("Id", ids.ToList())).List<T>();
                tran.Commit();
                session.Flush();
                return ret;
            }
        }

        protected IList<T> SelectMultipleInternalByFieldAndValue(string field, string value)
        {
            // create session/trans and commit.
            using (ISession session = NBHelper.OpenSession())
            using (ITransaction tran = session.BeginTransaction())
            {
                var ret = session.CreateCriteria<T>().Add(Restrictions.Eq(field, value)).List<T>();
                tran.Commit();
                session.Flush();
                return ret;
            }
        }
    }
}
