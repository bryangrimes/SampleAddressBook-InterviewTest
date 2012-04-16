using System;
using System.Web;
using AddressBook.BLL.Components;
using Castle.Windsor;

namespace AddressBook.UI
{
    public class Global : HttpApplication, IContainerAccessor
    {

        //public static WindsorContainer Container { get; set; }
        private static IWindsorContainer _container;

        public override void Init()
        {
            base.Init();

            InitializeCastle();
        }

        private static void InitializeCastle()
        {
            // container stuff
            _container = new WindsorContainer();

            var reg = new ComponentRegister();
            reg.GetComponents(_container);
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

        IWindsorContainer IContainerAccessor.Container { get { return _container; } }
    }
}