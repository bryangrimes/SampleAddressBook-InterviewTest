using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AddressBook.DAL.Domain;

namespace AddressBook.BLL.Entities
{
    public class Address : BaseEntity
    {
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Type { get; set; }
        public bool Favorite { get; set; }
        public int UserId { get; set; }
        public bool ToDelete { get; set; }
        public bool FavUpdated { get; set; }
        public IList<Tag> Tags { get; set; }
        public string TagString { get; set; }

        // this is used to translate data objects to business obects.  
        // Allows the BLL to only care about the data it needs to use thus not all data needs to flow from DAL to the BLL 
        // and possibly to the UI to worry about.
        public static Address SetEntity(AddressData addrData)
        {
            return new Address
                       {
                           Tags = Tag.SetEntity(addrData.Tags),
                           City = addrData.City,
                           Favorite = addrData.Favorite,
                           Id = addrData.Id,
                           Street1 = addrData.Street1,
                           Street2 = addrData.Street2,
                           State = addrData.State,
                           Type = addrData.Type,
                           UserId = addrData.UserId,
                           Zip = addrData.Zip
                       };
        }

        // translate the collection of user addresses to list of business objects.
        // again, any fields in the data layer that this layer doesn't care about wouldn't be mapped.
        public static IList<Address> SetEntity(IList<AddressData> addrData)
        {
            // only do this is there ARE addresses for the user...
            return addrData != null
                       ? addrData.Select(a => new Address
                                                  {
                                                      City = a.City,
                                                      Favorite = a.Favorite,
                                                      Id = a.Id,
                                                      Street1 = a.Street1,
                                                      Street2 = a.Street2,
                                                      State = a.State,
                                                      Type = a.Type,
                                                      UserId = a.UserId,
                                                      Zip = a.Zip,
                                                      Tags = Tag.SetEntity(a.Tags)
                                                  }).ToList()
                       : new List<Address>();
        }

        // to data objects
        public static AddressData ToData(Address addr)
        {
            return new AddressData
            {
                Tags = Tag.ToData(addr.Tags),
                City = addr.City,
                Favorite = addr.Favorite,
                Id = addr.Id,
                Street1 = addr.Street1,
                Street2 = addr.Street2,
                State = addr.State,
                Type = addr.Type,
                UserId = addr.UserId,
                Zip = addr.Zip
            };
        }

        public static IList<AddressData> ToData(IList<Address> addr)
        {
            // only do this is there ARE addresses for the user...
            return addr != null
                       ? addr.Select(a => new AddressData
                                              {
                                                  City = a.City,
                                                  Favorite = a.Favorite,
                                                  Id = a.Id,
                                                  Street1 = a.Street1,
                                                  Street2 = a.Street2,
                                                  State = a.State,
                                                  Type = a.Type,
                                                  UserId = a.UserId,
                                                  Zip = a.Zip,
                                                  Tags = Tag.ToData(a.Tags)
                                              }).ToList()
                       : new List<AddressData>();
        }
    }
}
