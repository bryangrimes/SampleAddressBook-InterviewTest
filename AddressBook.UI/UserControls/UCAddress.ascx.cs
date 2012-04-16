using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AddressBook.BLL.Entities;

namespace AddressBook.UI.UserControls
{
    public partial class UCAddress : System.Web.UI.UserControl
    {
        public int ControlId { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblAddressHeader.Text = String.Format("Address {0}", ControlId + 1);
        }
     
    }
}