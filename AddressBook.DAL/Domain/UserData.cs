using System.Collections.Generic;

namespace AddressBook.DAL.Domain
{
    public class UserData : BaseData
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual string Notes { get; set; }
        public virtual bool Favorite { get; set; }
        public virtual IList<AddressData> Addresses { get; set; }
        public virtual IList<TagData> Tags { get; set; }
    }
}
