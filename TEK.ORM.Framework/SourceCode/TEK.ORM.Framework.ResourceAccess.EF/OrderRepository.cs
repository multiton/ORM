using System;

using TEK.ORM.Framework.Entity;
using TEK.ORM.Framework.ResourceAccess.Contract;

namespace TEK.ORM.Framework.ResourceAccess.EF
{
	public class OrderRepository : IOrderRepository
	{
		public readonly IRepository<OrderHeader> HeaderRepository;
		public readonly IRepository<Product> ProductRepository;
		public readonly IRepository<Company> SupplierRepository;

		public OrderRepository(
			IRepository<OrderHeader> headerRepository,
			IRepository<Product> productRepository,
			IRepository<Company> supplierRepository)
		{
			this.HeaderRepository = headerRepository;
			this.ProductRepository = productRepository;
			this.SupplierRepository = supplierRepository;
		}

		public OrderHeader CreateOrder(OrderHeader order)
		{
			if (order == null) throw new ArgumentNullException(nameof(order));

			this.SupplierRepository.Attach(order.Supplier);

			foreach (var item in order.OdrerItems)
			{
				this.ProductRepository.Attach(item.Product);
			}

			return this.HeaderRepository.Add(order);
		}

		public int SaveChanges()
		{
			return this.HeaderRepository.SaveChanges();
		}
	}
}