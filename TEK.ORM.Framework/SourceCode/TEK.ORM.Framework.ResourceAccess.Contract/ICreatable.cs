using System;
using System.Linq.Expressions;
using System.Collections.Generic;

using TEK.ORM.Framework.Entity;

namespace TEK.ORM.Framework.ResourceAccess.Contract
{
    public interface ICreatable<TEntity> where TEntity : class
    {
        TEntity Add(TEntity entity);

		BaseEntity Attach(BaseEntity entity);

		ICollection<TEntity> Upsert(TEntity[] entities);

		void Set<TValue, T>(object entity, Expression<Func<T>> propertyExpression, TValue propertyValue);
	}
}