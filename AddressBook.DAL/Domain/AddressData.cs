using System.Collections.Generic;

namespace AddressBook.DAL.Domain
{
    public class AddressData : BaseData
    {
        public virtual string Street1 { get; set; }
        public virtual string Street2 { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string Zip { get; set; }
        public virtual string Type { get; set; }
        public virtual bool Favorite { get; set; }
        public virtual int UserId { get; set; }
        public virtual IList<TagData> Tags { get; set; }
    }
}
