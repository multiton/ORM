using Castle.Windsor;
using Castle.MicroKernel.ModelBuilder;

using TEK.ORM.Framework.IoC.Installers;

namespace TEK.ORM.Framework.IoC
{
	public static class BootStrapper
	{
		public static IWindsorContainer Configure()
		{
			return BootStrapper.Configure(null, null);
		}

		public static IWindsorContainer Configure(IWindsorContainer container)
		{
			return BootStrapper.Configure(container, null);
		}

		public static IWindsorContainer Configure(
			IWindsorContainer container,
			IContributeComponentModelConstruction contributor)
		{
			container = container ?? new WindsorContainer();

			if (contributor != null)
			{
				container.Kernel.ComponentModelBuilder.AddContributor(contributor);
			}

			container
				.Install(new DbContextInstaller())
				.Install(new RepositoryInstaller())
				.Install(new DataServiceInstaller())
				.Install(new DataComponentInstaller());

			return container;
		}
	}
}