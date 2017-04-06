using System;

using TEK.ORM.Framework.ResourceAccess.Contract;

namespace TEK.ORM.Framework.ResourceAccess.EF
{
	public abstract class UnitOfWork : UnitOfWorkBase
	{
		protected new DataContext DataContext { get; set; }

		protected UnitOfWork(IDisposable dataContext) : base (dataContext)
		{
		}

		public override void Commit()
		{
			this.DataContext.Database.CommitTransaction();
		}

		public override void BeginTransaction()
		{
			if (this.DataContext.Database.CurrentTransaction == null)
			{
				this.DataContext.Database.BeginTransaction();
			}
		}

		public override int Save()
		{
			return this.DataContext.SaveChanges();
		}
	}
}