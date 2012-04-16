using System.Collections.Generic;
using AddressBook.DAL.Domain;

namespace AddressBook.BLL.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
        public bool Favorite { get; set; }
        public bool FavUpdated { get; set; }
        public IList<Address> Addresses { get; set; }
        public IList<Tag> Tags { get; set; }

        // this is used to translate data objects to business obects.  
        // Allows the BLL to only care about the data it needs to use thus not all data needs to flow from DAL to the BLL 
        // and possibly to the UI to worry about.
        public static User SetEntity(UserData userData)
        {
            if (userData != null)
            {
                return new User
                           {
                               Addresses = Address.SetEntity(userData.Addresses),
                               Tags = Tag.SetEntity(userData.Tags),
                                Email = userData.Email,
                                FirstName = userData.FirstName,
                                Id = userData.Id,
                                LastName = userData.LastName,
                                Notes = userData.Notes,
                                Favorite = userData.Favorite,
                };
            }

            return null;
        }

        // opposite...take the BLL obj and set it to a DAL obj for work in that level...
        public static UserData ToData(User user)
        {
            return new UserData
            {
                Addresses = Address.ToData(user.Addresses),
                Tags = Tag.ToData(user.Tags),
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                Notes = user.Notes,
                Favorite = user.Favorite
            };
        }
    }
}
