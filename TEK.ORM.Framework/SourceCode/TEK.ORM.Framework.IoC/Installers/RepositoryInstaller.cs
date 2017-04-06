using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;

using TEK.ORM.Framework.ResourceAccess.EF;
using TEK.ORM.Framework.ResourceAccess.Contract;

namespace TEK.ORM.Framework.IoC.Installers
{
    internal class RepositoryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
			container.Register(Component
				.For(typeof(IRepository<>))
				.ImplementedBy(typeof(Repository<>))
				.LifeStyle.Transient);

			container.Register(Classes
				.FromAssemblyContaining<OrderRepository>()
				.BasedOn<IRepository>()
				.WithService.FromInterface()
				.LifestyleTransient());
		}
    }
}