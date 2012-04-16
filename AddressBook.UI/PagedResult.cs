using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AddressBook.BLL.Entities;

namespace AddressBook.UI
{
    public class PagedResult<T> where T : BaseEntity
    {
        public int Total { get; set; }
        public List<T> Rows { get; set; }
    }
}
