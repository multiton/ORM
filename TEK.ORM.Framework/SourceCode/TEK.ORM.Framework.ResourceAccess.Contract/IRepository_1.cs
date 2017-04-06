using System.Threading.Tasks;

namespace TEK.ORM.Framework.ResourceAccess.Contract
{
	public interface IRepository<TEntity> :
		ICreatable<TEntity>, IRetrievable<TEntity>, IUpdatable<TEntity>, IDeletable<TEntity>
		where TEntity : class
	{
		int SaveChanges();

		Task<int> SaveChangesAsync();
	}
}