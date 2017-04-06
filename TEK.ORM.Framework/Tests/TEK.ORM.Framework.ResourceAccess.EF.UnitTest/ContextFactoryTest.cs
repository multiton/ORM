using TEK.ORM.Framework.Entity;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TEK.ORM.Framework.ResourceAccess.EF.UnitTest
{
	[TestClass]
	public class ContextFactoryTest
	{
		[TestMethod]
		public void CreateContextTest()
		{
			var ctx = new DbContextFactory<OrderHeader>().Instance;

			Assert.IsNotNull(ctx);
		}
	}
}