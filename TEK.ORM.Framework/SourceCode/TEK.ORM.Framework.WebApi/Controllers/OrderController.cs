using System.Collections.Generic;

using TEK.ORM.Framework.Entity;
using TEK.ORM.Framework.BusinessLogic.Contract;

namespace TEK.ORM.Framework.WebApi.Controllers
{
    public class OrderController : BaseController
	{
		private readonly IDataService<OrderHeader> orderDataService;

		public OrderController(IDataService<OrderHeader> dataService)
		{
			this.orderDataService = dataService;
		}

		public IEnumerable<OrderHeader> Get()
        {
			return orderDataService.Find(x => x.Number != string.Empty, null);
        }
    }
}