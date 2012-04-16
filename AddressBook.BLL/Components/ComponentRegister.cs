
using AddressBook.BLL.Repositories;
using AddressBook.BLL.Services;
using AddressBook.DAL.DataRepositories;
using AddressBook.Util.Aspects.Tracing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace AddressBook.BLL.Components
{
    public class ComponentRegister
    {
        public virtual void GetComponents(IWindsorContainer container)
        {
            // could do the assembly load, but do this for now...
            container.Register(
                Component.For<IAddressBookService>().ImplementedBy<AddressBookService>(),
                Component.For<IUserAddressRepo>().ImplementedBy<UserAddressRepo>(),
                Component.For<IAddressRepo>().ImplementedBy<AddressRepo>(),
                Component.For<IUserDataRepository>().ImplementedBy<UserDataRepository>(),
                Component.For<IAddressDataRepository>().ImplementedBy<AddressDataRepository>(),
                Component.For<ITagDataRepository>().ImplementedBy<TagDataRepository>(),
                   Component.For<TraceAspect>()
               );
        }
    }
}
