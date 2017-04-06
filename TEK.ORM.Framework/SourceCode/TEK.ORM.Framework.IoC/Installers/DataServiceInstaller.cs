using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;

using TEK.ORM.Framework.BusinessLogic;
using TEK.ORM.Framework.BusinessLogic.Contract;

namespace TEK.ORM.Framework.IoC.Installers
{
    internal class DataServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
			container.Register(Component
				.For<IAccessLogger>()
				.ImplementedBy<AccessLogger>()
				.LifeStyle.Transient);

			container.Register(Component
				.For(typeof(IDataService<>))
				.ImplementedBy(typeof(DataService<>))
				.LifeStyle.Transient);
		}
    }
}