using System.Collections.Generic;

using TEK.ORM.Framework.Entity;
using TEK.ORM.Framework.BusinessLogic.Contract;
using TEK.ORM.Framework.BusinessLogic.Contract.DataComponents;

namespace TEK.ORM.Framework.App
{
	public static class OrderBuilder
	{
		public static void AddSingleObjectGraph(IOrderComponent orderDataComponent)
		{
			orderDataComponent.CreateOrder(new OrderHeader
			{
				Number = "1",
				Supplier = new Company { Id = 1 },
				OdrerItems = new HashSet<OrderItem>
				{
					new OrderItem { Quantity = 3, Price = 2500.00m, Product = new Product { Id = 1 } },
					new OrderItem { Quantity = 4, Price = 1580.95m, Product = new Product { Id = 2 } }
				}
			});
		}

		public static void AddSingleObjectId(IDataService<OrderHeader> dataService)
		{
			var item = new OrderItem { Quantity = 2, Price = 2500.15m };

			dataService.Set(item, () => item.Product  , 1);

			var order = new OrderHeader
			{
				Number = "100",
				OdrerItems = new HashSet<OrderItem> { item }
			};

			dataService.Set(order, () => order.Supplier, 1);

			dataService.Add(order);
		}
	}
}