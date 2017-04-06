using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TEK.ORM.Framework.IoC;
using TEK.ORM.Framework.Entity;
using TEK.ORM.Framework.ResourceAccess.Contract;

namespace TEK.ORM.Framework.ResourceAccess.EF.UnitTest
{
	[TestClass]
	public class OrderRepositoryTest
	{
		[TestMethod]
		public void CreateOrder()
		{
			using (var container = BootStrapper.Configure())
			{
				var repository = container.Resolve<IOrderRepository>();

				repository.CreateOrder(new OrderHeader
				{
					Number = "150",
					Supplier = new Company { Id = 1 },
					OdrerItems = new HashSet<OrderItem>
					{
						new OrderItem { Quantity = 3, Price = 3.62m, Product = new Product { Id = 1 } },
						new OrderItem { Quantity = 5, Price = 4.12m, Product = new Product { Id = 2 } }
					}
				});

				var count = repository.SaveChanges();
				Assert.AreEqual(count, 3);
			}
		}

		[TestMethod]
		public void AddOrderItem()
		{
			var item = new OrderItem { Quantity = 3, Price = 2500.00m };

			using (var container = BootStrapper.Configure())
			{
				var repository = container.Resolve<IRepository<OrderItem>>();

				repository.Set(item, () => item.Product, 1);
				repository.Set(item, () => item.Order, 4);

				repository.Add(item);
				repository.SaveChanges();
			}
		}

		[TestMethod]
		public void CreateOrderItem()
		{
			var item = new OrderItem
			{
				Quantity = 3,
				Price = 2500.00m,
				Product = new Product { Id = 1 },
				Order = new OrderHeader { Id = 4 }
			};

			using (var container = BootStrapper.Configure())
			{
				var repository = container.Resolve<IRepository<OrderItem>>();

				repository.Attach(item.Product);
				repository.Attach(item.Order);

				repository.Add(item);
				repository.SaveChanges();
			}
		}
	}
}