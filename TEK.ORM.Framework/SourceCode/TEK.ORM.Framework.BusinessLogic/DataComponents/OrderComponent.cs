using System;

using TEK.ORM.Framework.Entity;
using TEK.ORM.Framework.ResourceAccess.Contract;
using TEK.ORM.Framework.BusinessLogic.Contract.DataComponents;

namespace TEK.ORM.Framework.BusinessLogic.DataComponents
{
	public class OrderComponent : IOrderComponent, IDisposable
	{
		public readonly IUnitOfWork UnitOfWork;
		public readonly IOrderRepository OrderRepository;

		public OrderComponent(IUnitOfWork unitOfWork, IOrderRepository orderRepository)
		{
			this.UnitOfWork = unitOfWork;
			this.OrderRepository = orderRepository;
		}

		public OrderHeader CreateOrder(OrderHeader order)
		{
			if (order == null) throw new ArgumentNullException(nameof(order));

			this.OrderRepository.CreateOrder(order);
			this.UnitOfWork.Save();

			return order;
		}

		public void Dispose()
		{
			this.UnitOfWork?.Dispose();
		}
	}
}