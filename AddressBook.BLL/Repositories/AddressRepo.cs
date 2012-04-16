using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AddressBook.BLL.Entities;
using AddressBook.DAL.DataRepositories;
using AddressBook.Util.Aspects.Tracing;
using Castle.Core;

namespace AddressBook.BLL.Repositories
{
    [Interceptor(typeof(TraceAspect))]
    public class AddressRepo : IAddressRepo
    {
        private readonly IAddressDataRepository addrDataRepo;
       // private readonly ITagDataRepository tagDataRepo;
        public AddressRepo(IAddressDataRepository aDataRepo, ITagDataRepository tDataRepo)
        {
            addrDataRepo = aDataRepo;
           // tagDataRepo = tDataRepo;
        }

        public IList<Address> GetUserAddresses(int id)
        {
            return Address.SetEntity(addrDataRepo.SelectMultipleByFieldAndValue("UserId", id.ToString()));
        }

        public Address GetSingleAddress(int id)
        {
            return Address.SetEntity(addrDataRepo.SelectById(id));
        }

        public Address CreateAddress(Address obj)
        {
            return Address.SetEntity(addrDataRepo.Add(Address.ToData(obj)));
        }

        public Address UpdateAddress(Address obj)
        {
            return Address.SetEntity(addrDataRepo.Update(Address.ToData(obj)));
        }

        public User DeleteAddress(User user)
        {
            var addresses = user.Addresses;
            var toDelete = addresses.Where(a => a.ToDelete = true);

            foreach (var a in toDelete)
            {
                addrDataRepo.Delete(addrDataRepo.SelectById(a.Id));
            }

            return user;
        }
        
        public void MarkAsFavorite(Address obj)
        {
            obj.Favorite = true;
            addrDataRepo.MarkAsFavorite(Address.ToData(obj));
        }

        public void RemoveAsFavorite(Address obj)
        {
            obj.Favorite = false;

            addrDataRepo.RemoveAsFavorite(Address.ToData(obj));
        }

        public void MarkAsFavorite(int id)
        {
            // get the Address obj and then update.
            var addr = addrDataRepo.SelectById(id);
            addr.Favorite = true;

            addrDataRepo.MarkAsFavorite(addr);
        }

        public void RemoveAsFavorite(int id)
        {
            var addr = addrDataRepo.SelectById(id);
            addr.Favorite = false;

            addrDataRepo.RemoveAsFavorite(addr);
        }

    }
}
