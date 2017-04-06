using System;

namespace TEK.ORM.Framework.ResourceAccess.Contract
{
	public abstract class UnitOfWorkBase : IUnitOfWork
	{
		protected virtual IDisposable DataContext { get; set; }

		protected UnitOfWorkBase(IDisposable dataContext)
		{
			this.DataContext = dataContext;
		}

		public abstract int Save();

		public abstract void Commit();

		public abstract void BeginTransaction();

		#region Dispose
		private bool disposed = false;

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					this.DataContext.Dispose();
				}
			}
			this.disposed = true;
		}
		#endregion
	}
}