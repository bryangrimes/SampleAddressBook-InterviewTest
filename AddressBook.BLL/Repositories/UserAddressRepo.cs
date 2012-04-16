
using System.Collections.Generic;
using System.Linq;
using AddressBook.BLL.Entities;
using AddressBook.Util.Aspects.Tracing;
using AddressBook.DAL.DataRepositories;
using Castle.Core;


namespace AddressBook.BLL.Repositories
{
    [Interceptor(typeof(TraceAspect))]
    public class UserAddressRepo : IUserAddressRepo
    {
        private readonly IUserDataRepository userDataRepo;

        public UserAddressRepo(IUserDataRepository uDataRepo)
        {
            userDataRepo = uDataRepo;
        }

        public IList<User> GetAllUsers()
        {
            return userDataRepo.SelectALL().Select(user => User.SetEntity(user)).ToList();

            //return users;
            //.Select(user => User.SetEntity(user)).ToList();

            //return null;
        }

       /*public IList<User> GetUsersByFilter(string filter)
        {
            //return userDataRepo.SelectMultipleByFieldAndValue(filter).Select(user => User.SetEntity(user)).ToList());
            
        }*/

        public User GetSingleUser(int id)
        {
            return User.SetEntity(userDataRepo.SelectById(id));
        }

        public User CreateUser(User obj)
        {
            return User.SetEntity(userDataRepo.Add(User.ToData(obj)));
        }

        public User UpdateUser(User obj)
        {
            return User.SetEntity(userDataRepo.Update(User.ToData(obj)));
        }

        public void DeleteUser(User obj)
        {
            userDataRepo.Delete(User.ToData(obj));
        }

        public void MarkAsFavorite(User obj)
        {
            userDataRepo.MarkAsFavorite(User.ToData(obj));
        }

        public void RemoveAsFavorite(User obj)
        {
            userDataRepo.RemoveAsFavorite(User.ToData(obj));
        }

    }
}
