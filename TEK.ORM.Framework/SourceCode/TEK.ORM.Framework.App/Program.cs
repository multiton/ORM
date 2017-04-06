using TEK.ORM.Framework.IoC;
using TEK.ORM.Framework.BusinessLogic.Contract.DataComponents;

namespace TEK.ORM.Framework.App
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var container = BootStrapper.Configure())
			{
				var orderDataComponent = container.Resolve<IOrderComponent>();

				OrderBuilder.AddSingleObjectGraph(orderDataComponent);
			}
		}
	}
}