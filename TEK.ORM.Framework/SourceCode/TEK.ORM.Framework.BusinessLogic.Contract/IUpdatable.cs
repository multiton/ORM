namespace TEK.ORM.Framework.BusinessLogic.Contract
{
	public interface IUpdatable<TEntity> where TEntity : class
	{
		TEntity Update(TEntity entity);
	}
}