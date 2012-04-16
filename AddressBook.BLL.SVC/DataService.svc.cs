
using System.Collections.Generic;
using System.Web.Services;
using AddressBook.BLL.Entities;
using AddressBook.BLL.Repositories;

namespace AddressBook.BLL.SVC
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class DataService : System.Web.Services.WebService
    {
        private readonly IUserRepo userRepo;
        private readonly IAddressRepo addressRepo;

        public DataService(IUserRepo uRepo, IAddressRepo aRepo)
        {
            userRepo = uRepo;
            addressRepo = aRepo;
        }

        [WebMethod]
        public IList<User> GetAllUsers()
        {
            var users = userRepo.GetAllUsers();
            foreach (var u in users)
            {
                // with each user, get the addresses they might have and assign to the User's property.
                u.Addresses = addressRepo.GetUserAddresses(u.Id);
            }

            return users;
        }

        [WebMethod]
        public User GetUser(int id)
        {
            return userRepo.GetSingleUser(id);
        }

        [WebMethod]
        public User CreateUser(User user)
        {
            return userRepo.CreateUser(user);
        }

        [WebMethod]
        public User CreateAddress(User user, Address address)
        {
            var createdAddr = addressRepo.CreateAddress(address);
            user.Addresses.Add(createdAddr);
            return user;
        }

        [WebMethod]
        public void DeleteUser(User user)
        {
            userRepo.DeleteUser(user);
        }

        [WebMethod]
        public void DeleteAddress(User user)
        {
            addressRepo.DeleteAddress(user);
        }

        [WebMethod]
        public User UpdateAddress(User user)
        {
            foreach (var addr in user.Addresses)
            {
                addressRepo.UpdateAddress(addr);
            }

            return user;
        }

        [WebMethod]
        public User UpdateUser(User user)
        {
            return userRepo.UpdateUser(user);
        }
    }
}
