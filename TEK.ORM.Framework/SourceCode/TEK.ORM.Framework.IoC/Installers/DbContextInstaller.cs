using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;

using Microsoft.EntityFrameworkCore;
using TEK.ORM.Framework.ResourceAccess.EF;

namespace TEK.ORM.Framework.IoC.Installers
{
    internal class DbContextInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
			container.Register
			(
				Component.For<IDbContextFactory>().ImplementedBy(typeof(DbContextFactory<>)).LifeStyle.Singleton,
				Component.For<DbContext>().ImplementedBy<DataContext>().LifeStyle.Singleton
			);
		}
    }
}