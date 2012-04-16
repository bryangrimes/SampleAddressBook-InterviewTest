using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddressBook.DAL.Domain
{
    public class TagData : BaseData
    {
        public virtual string Name { get; set; }
        public virtual int WhatId { get; set; } // FK to tables that use this one.
    }
}
