using System;

using Castle.Windsor;

namespace AddressBook.UI
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

       public IContainerAccessor accessor;

        protected override void  OnInit(EventArgs e)
        {
           // IContainerAccessor 
            accessor = Context.ApplicationInstance as IContainerAccessor;

            if (accessor == null || accessor.Container == null)
                throw new InvalidOperationException("Castle not configured properly");

            Session.Add("Container", accessor);
        }
    }
}
