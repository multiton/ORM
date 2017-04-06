namespace TEK.ORM.Framework.Entity
{
	public abstract class BaseEntity : IdentifiedEntity<int> { }

	public abstract class BaseDataEntity : BaseEntity { }

	public abstract class BaseAuditEntity : BaseEntity { }
}