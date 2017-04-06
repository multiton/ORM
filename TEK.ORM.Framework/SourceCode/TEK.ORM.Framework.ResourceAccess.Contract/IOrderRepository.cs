using TEK.ORM.Framework.Entity;

namespace TEK.ORM.Framework.ResourceAccess.Contract
{
	public interface IOrderRepository : IRepository
	{
		OrderHeader CreateOrder(OrderHeader order);

		int SaveChanges();
	}
}