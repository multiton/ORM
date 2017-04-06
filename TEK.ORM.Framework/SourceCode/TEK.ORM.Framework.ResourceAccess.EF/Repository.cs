using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using TEK.ORM.Framework.Entity;
using TEK.ORM.Framework.ResourceAccess.Contract;

namespace TEK.ORM.Framework.ResourceAccess.EF
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
	{
		private readonly DbContext dbContext;
		private readonly DbSet<TEntity> entitySet;

		public Repository(DbContext dbContext)
		{
			this.dbContext = dbContext;
			this.entitySet = dbContext.Set<TEntity>();
		}

		public TEntity Add(TEntity entity)
		{
			return this.entitySet.Add(entity).Entity;
		}

		public BaseEntity Attach(BaseEntity entity)
		{
			return this.dbContext.Attach(entity).Entity;
		}

		public ICollection<TEntity> Upsert(TEntity[] entities)
		{
			if (entities == null || entities.Length == 0)
			{
				return null;
			}

			var createdEntities = new List<TEntity>();

			using (var transaction = this.dbContext.Database.BeginTransaction())
			{
				foreach (var entity in entities)
				{
					if (entity.IsNew)
					{
						createdEntities.Add(this.Add(entity));
					}
					else
					{
						this.Update(entity);
					}

					this.SaveChanges();
				}

				transaction.Commit();

				// Dont need to try/catch and call transaction.Rollback(), because 'using'
				// statement does auto-rollback on-dispose if transaction is not committed
			}

			return createdEntities;
		}

		public virtual TEntity Delete(params object[] keyValues)
		{
			if (keyValues == null || keyValues.Length == 0) return null;

			var entityToDelete = this.entitySet.Find(keyValues);

			if (entityToDelete == null) return null;

			return this.Delete(entityToDelete);
		}

		public TEntity Delete(TEntity entity)
		{
			if (entity == null) return null;

			if (this.dbContext.Entry(entity).State == EntityState.Detached)
			{
				this.entitySet.Attach(entity);
			}

			return this.entitySet.Remove(entity).Entity;
		}

		public TEntity Get(Expression<Func<TEntity, bool>> predicate)
		{
			return this.Get(predicate, null);
		}

		public virtual IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters)
		{
			if (string.IsNullOrEmpty(query) || parameters == null || parameters.Length == 0)
			{
				return null;
			}

			return this.entitySet.FromSql(query, parameters).ToList();
		}

		public TEntity Get(
			Expression<Func<TEntity, bool>> predicate,
			params Expression<Func<TEntity, object>>[] include)
		{
			IQueryable<TEntity> query = this.entitySet;

			if (include != null && include.Any())
			{
				foreach (var includeExpression in include)
				{
					query = query.Include(includeExpression);
				}
			}

			return query.FirstOrDefault(predicate);
		}

		public TEntity Get(params object[] keyValues)
		{
			var entity = this.entitySet.Find(keyValues);
			return entity;
		}

		public IEnumerable<TEntity> Find(
			Expression<Func<TEntity, bool>> predicate,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
		{
			return this.Find(predicate, orderBy, (Expression<Func<TEntity, object>>)null);
		}

		public IEnumerable<TEntity> Find(
			Expression<Func<TEntity, bool>> predicate,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
			params Expression<Func<TEntity, object>>[] include)
		{
			IQueryable<TEntity> query = this.entitySet;

			if (predicate != null)
			{
				query = query.Where(predicate);
			}

			if (orderBy != null)
			{
				query = orderBy(query);
			}

			if (include != null && include.Any())
			{
				foreach (var includeExpression in include)
				{
					if (include != null && include[0] != null)
					{
						query = query.Include(includeExpression);
					}
				}
			}

			return query;		// ??? .ToList() !!! ???
		}

		public TEntity Update(TEntity entity)
		{
			this.dbContext.Entry(entity).State = EntityState.Modified;
			return entity;
		}

		public void Set<TValue, T>(object entity, Expression<Func<T>> propertyExpression, TValue propertyValue)
		{
			var propertyName = $"{((MemberExpression)propertyExpression.Body).Member.Name}Id";

			this.dbContext.Entry(entity).Property(propertyName).CurrentValue = propertyValue;
		}
		public int SaveChanges()
		{
			return this.dbContext.SaveChanges();
		}

		public Task<int> SaveChangesAsync()
		{
			return this.dbContext.SaveChangesAsync();
		}
	}
}