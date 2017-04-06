// See: https://docs.microsoft.com/en-us/ef/core/saving/transactions
using System.Transactions;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using TEK.ORM.Framework.Entity;

namespace TEK.ORM.Framework.ResourceAccess.EF.UnitTest
{
	[TestClass]
	public class TwoPhaseCommit
	{
		[TestMethod]
		public void CreateCompanyAudit()
		{
			using (var transactionScope = new TransactionScope())
			{
				using (var dataContext = new DataContext())
				{
					using (var dataTransaction = dataContext.Database.BeginTransaction())
					{
						dataContext.Set<Company>().Add(new Company { Name = "Sookin & Son" });

						dataContext.SaveChanges();
						dataTransaction.Commit();
					}
				}

				using (var auditContext = new AuditContext())
				{
					using (var auditTransaction = auditContext.Database.BeginTransaction())
					{
						auditContext.Set<AuditRecord>().Add(new AuditRecord
						{
							ActionName = "Test", UserLoginName = "JDoe"
						});

						auditContext.SaveChanges();
						auditTransaction.Commit();
					}
				}

				 transactionScope.Complete();
			}
		}
	}
}