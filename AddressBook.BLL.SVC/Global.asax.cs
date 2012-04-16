using System;
using System.Web;
using Castle.Windsor;
using AddressBook.BLL.Components;

namespace AddressBook.BLL.SVC
{
    public class Global : HttpApplication
    {

        public static WindsorContainer Container { get; set; }

        public override void Init()
        {
            base.Init();

            InitializeCastle();
        }

        private static void InitializeCastle()
        {
            // container stuff
            Container = new WindsorContainer();

            var reg = new ComponentRegister();
            reg.GetComponents(Container);
        }

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            
        }
    }
}