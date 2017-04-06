namespace TEK.ORM.Framework.BusinessLogic.Contract
{
	public interface IDeletable<TEntity> where TEntity : class
	{
		TEntity Delete(TEntity entity);
	}
}