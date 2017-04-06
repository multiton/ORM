using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace TEK.ORM.Framework.ResourceAccess.Contract
{
    public interface IRetrievable<TEntity> where TEntity : class
    {
	    TEntity Get(params object[] keyValues);

		TEntity Get(Expression<Func<TEntity, bool>> predicate);

	    TEntity Get(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] include);

		IEnumerable<TEntity> Find(
            Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);

	    IEnumerable<TEntity> Find(
		    Expression<Func<TEntity, bool>> predicate,
		    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
		    params Expression<Func<TEntity, object>>[] include);
	}
}