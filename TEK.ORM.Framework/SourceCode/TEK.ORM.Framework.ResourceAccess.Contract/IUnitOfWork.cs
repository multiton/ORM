using System;

namespace TEK.ORM.Framework.ResourceAccess.Contract
{
	public interface IUnitOfWork : IDisposable
	{
		int Save();

		void Commit();
	}
}