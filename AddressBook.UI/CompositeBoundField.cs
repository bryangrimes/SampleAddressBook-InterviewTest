﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AddressBook.UI
{
    public class CompositeBoundField : BoundField
    {
        protected override object GetValue(Control controlContainer)
        {
            object item = DataBinder.GetDataItem(controlContainer);
            return DataBinder.Eval(item, DataField);
        }
    }
}
