namespace TEK.ORM.Framework.ResourceAccess.Contract
{
    public interface IDeletable<TEntity> where TEntity : class
    {
        TEntity Delete(TEntity entity);
    }
}