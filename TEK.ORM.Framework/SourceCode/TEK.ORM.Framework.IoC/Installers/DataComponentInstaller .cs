using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;

using TEK.ORM.Framework.BusinessLogic.DataComponents;
using TEK.ORM.Framework.BusinessLogic.Contract.DataComponents;

namespace TEK.ORM.Framework.IoC.Installers
{
	public class DataComponentInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(Classes
				.FromAssemblyContaining<OrderComponent>()
				.BasedOn<IDataComponent>()
				.WithService.FromInterface()
				.LifestyleTransient());
		}
	}
}