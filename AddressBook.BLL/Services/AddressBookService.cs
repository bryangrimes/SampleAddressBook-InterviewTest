using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using AddressBook.BLL.Entities;
using AddressBook.BLL.Repositories;
using System.Linq.Dynamic;

namespace AddressBook.BLL.Services
{
    // There are no addresses without users.
    // While the repo stuff is there to talk to the DAL, the top level here only wants to dishout a User obj with properties
    // one of whish is a LIST of addresses and another a LIST of tags.
    public class AddressBookService : IAddressBookService
    {
        private readonly IUserAddressRepo userRepo;
        private readonly IAddressRepo addrRepo;

        public AddressBookService(IUserAddressRepo uRepo, IAddressRepo aRepo)
        {
            userRepo = uRepo;
            addrRepo = aRepo;
        }

        public IList<User> GetAllUsers()
        {
            return userRepo.GetAllUsers();
        }

        public User GetUser(int id)
        {
            return userRepo.GetSingleUser(id);
        }

        public User CreateUser(User user)
        {
            return userRepo.CreateUser(user);
        }

        public void DeleteUser(User user)
        {
            userRepo.DeleteUser(user);
        }

        public User UpdateUser(User user)
        {
            return userRepo.UpdateUser(user);
        }

        public Address CreateAddress(Address address)
        {
            return addrRepo.CreateAddress(address);
        }

        public IList<User> GetUsersByFilter(string field, string op, string value)
        {
            var users =  userRepo.GetAllUsers().AsQueryable();

            IList<User> returnUsers;

            switch (op)
            {
                case "eq":
                    returnUsers = users.Where(@String.Format("{0} = \"{1}\"", field, value)).ToList();
                    break;
                case "ne":
                    returnUsers = users.Where(String.Format("{0} <> \"{1}\"", field, value)).ToList();
                    break;
                default:
                    returnUsers = CreateWhereSearch(users, field, value, op);
                    break;   
            }

            return returnUsers;
        }

        private static IList<User> CreateWhereSearch(IEnumerable<User> input, string field, string value, string op)
        {
            switch (field)
            {
                case "LastName":
                    switch (op)
                    {
                        case "sw":
                            return FooStartsWith(input, c => c.LastName, value);
                        case "ew":
                            return FooEndsWith(input, c => c.LastName, value);
                        case "co":
                            return FooContains(input, c => c.LastName, value);
                        default:
                            throw new ApplicationException("Filter contains no valid operator.");
                    }
                case "FirstName":
                    switch (op)
                    {
                        case "sw":
                            return FooStartsWith(input, c => c.FirstName, value);
                        case "ew":
                            return FooEndsWith(input, c => c.FirstName, value);
                        case "co":
                            return FooContains(input, c => c.FirstName, value);
                        default:
                            throw new ApplicationException("Filter contains no valid operator.");
                    }
                case "Email":
                    switch (op)
                    {
                        case "sw":
                            return FooStartsWith(input, c => c.Email, value);
                        case "ew":
                            return FooEndsWith(input, c => c.Email, value);
                        case "co":
                            return FooContains(input, c => c.Email, value);
                        default:
                            throw new ApplicationException("Filter contains no valid operator.");
                    }
            }
             return null;
        }

        private static IList<User> FooStartsWith(IEnumerable<User> input, Func<User, string> searchExpr , string searchText)
        {
         return input.Where(c => searchExpr(c).StartsWith(searchText, StringComparison.CurrentCultureIgnoreCase)).ToList();
        }

        private static IList<User> FooEndsWith(IEnumerable<User> input, Func<User, string> searchExpr , string searchText)
        {
         return input.Where(c => searchExpr(c).EndsWith(searchText, StringComparison.CurrentCultureIgnoreCase)).ToList();
        }

        private static IList<User> FooContains(IEnumerable<User> input, Func<User, string> searchExpr , string searchText)
        {
         return input.Where(c => searchExpr(c).Contains(searchText)).ToList();
        }

    }
}
