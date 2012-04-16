using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AddressBook.BLL.Entities;
using AddressBook.DAL.Domain;

namespace AddressBook.BLL.Entities
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }
        public int WhatId { get; set; }

        public static Tag SetEntity(TagData tagData)
        {
            return new Tag
                       {
                          Id = tagData.Id,
                          Name = tagData.Name,
                          WhatId = tagData.WhatId
                       };
        }

        public static IList<Tag> SetEntity(IList<TagData> tagData)
        {
            return tagData != null
                       ? tagData.Select(t => new Tag {Id = t.Id, Name = t.Name, WhatId = t.WhatId}).ToList()
                       : new List<Tag>();
        }

        // to data objects
        public static TagData ToData(Tag tag)
        {
            return new TagData
            {
                Id = tag.Id,
                Name = tag.Name,
                WhatId = tag.WhatId
            };
        }

        public static IList<TagData> ToData(IList<Tag> tags)
        {
            return tags != null ? tags.Select(t => new TagData { Id = t.Id, Name = t.Name, WhatId = t.WhatId }).ToList() 
                                : new List<TagData>();
        }
    }
}
