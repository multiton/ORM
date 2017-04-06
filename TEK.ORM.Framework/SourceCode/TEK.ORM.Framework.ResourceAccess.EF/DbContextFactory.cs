using System;
using System.Collections.Generic;

using TEK.ORM.Framework.Entity;

namespace TEK.ORM.Framework.ResourceAccess.EF
{
	public interface IDbContextFactory
	{
		DbContextBase Instance { get; }
	}

	public class DbContextFactory<TEntity> : IDbContextFactory, IDisposable
	{
		private readonly Dictionary<Type, Type> conextEntityMap = new Dictionary<Type, Type>
		{
			{ typeof(BaseDataEntity), typeof(DataContext) },
			{ typeof(BaseAuditEntity), typeof(AuditContext) }
		};

		private DbContextBase instance;

		public DbContextBase Instance
		{
			get
			{
				if (this.instance != null) return this.instance;

				var contextType = this.GetDataContextType();

				this.instance = Activator.CreateInstance(contextType) as DbContextBase;

				return this.instance;
			}
		}

		private Type GetDataContextType()
		{
			foreach (var map in this.conextEntityMap)
			{
				if (typeof(TEntity).IsSubclassOf(map.Key))
				{
					return map.Value;
				}
			}

			throw new InvalidOperationException($"Entity [{typeof(TEntity).FullName}] not mapped to Context");
		}

		public void Dispose()
		{
			this.instance?.Dispose();
		}
	}
}