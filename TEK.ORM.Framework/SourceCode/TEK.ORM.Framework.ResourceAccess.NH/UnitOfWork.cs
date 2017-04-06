using System;
using NHibernate;

using TEK.ORM.Framework.ResourceAccess.Contract;

namespace TEK.ORM.Framework.ResourceAccess.NH
{
	public class UnitOfWork : UnitOfWorkBase
	{
		public ISession Session { get; private set; }

		private ITransaction transaction;

		static UnitOfWork()
		{
			// Initialise singleton instance of ISessionFactory, static constructors are only executed
			// once during the application lifetime - the first time the UnitOfWork class is used.
			//
			//sessionFactory = Fluently.Configure()
			//	.Database(MsSqlConfiguration.MsSql2008.ConnectionString(x => x.FromConnectionStringWithKey("DataContext")))
			//	.Mappings(x => x.AutoMappings.Add(AutoMap.AssemblyOf<Product>(new AutomappingConfiguration()).UseOverridesFromAssemblyOf<Product>()))
			//	.ExposeConfiguration(config => new SchemaUpdate(config).Execute(false, true))
			//	.BuildSessionFactory();
		}

		public UnitOfWork(ISessionFactory sessionFactory) : base (sessionFactory)
		{
			this.Session = sessionFactory.OpenSession();
		}

		public override void BeginTransaction()
		{
			if (this.transaction != null)
			{
				throw new InvalidOperationException("A transactional process is already ongoing");
			}

			this.transaction = this.Session.BeginTransaction();
		}

		public override void Commit()
		{
			if (this.transaction == null || (!this.transaction.IsActive))
			{
				throw new InvalidOperationException("Cannot commit to inactive transaction.");
			}

			try
			{
				this.transaction.Commit();
			}
			catch
			{
				this.transaction.Rollback();
				throw;
			}
			finally
			{
				this.Session.Close();
			}
		}

		public void Rollback()
		{
			try
			{
				if (this.transaction != null && this.transaction.IsActive)
				{
					this.transaction.Rollback();
				}
			}
			finally
			{
				this.Session.Close();
			}
		}

		public override int Save()
		{
			this.Session.Flush();
			return 0;
		}

		public void CancelChanges()
		{
			this.Session.Clear();
		}
	}
}