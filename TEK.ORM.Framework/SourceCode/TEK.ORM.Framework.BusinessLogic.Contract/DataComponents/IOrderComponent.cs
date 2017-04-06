using TEK.ORM.Framework.Entity;

namespace TEK.ORM.Framework.BusinessLogic.Contract.DataComponents
{
	public interface IOrderComponent : IDataComponent
	{
		OrderHeader CreateOrder(OrderHeader order);
	}
}