using System.Linq;
using System.Text;
using System.Threading;
using Castle.Core.Logging;

using TEK.ORM.Framework.BusinessLogic.Contract;

namespace TEK.ORM.Framework.BusinessLogic
{
	public class AccessLogger : IAccessLogger
	{
		public ILogger Logger { get; set; }

		public AccessLogger()
		{
			this.Logger = NullLogger.Instance;
		}

		public void LogOperationResult(int changesCount, string entityTypeName)
		{
			Logger.InfoFormat("{0} records of type: {1} retrieved by user: {2}",
				changesCount,
				FormatEntityTypeName(entityTypeName),
				Thread.CurrentPrincipal.Identity.Name);
		}

		public void LogOperationResult(int changesCount, string operation, object entity)
		{
			Logger.InfoFormat(
				"{0} entities {1} by user: {2}\nRootType: {3}, details: {4}",
				changesCount,
				operation,
				Thread.CurrentPrincipal.Identity.Name,
				FormatEntityTypeName(entity.GetType().Name),
				entity.ToString());
		}

		public void LogOperationResult(object[] entities)
		{
			var logMessage = new StringBuilder();

			logMessage.AppendFormat("{0} entities RootType {1} modified by user: {2}. Details:",
				entities.Length,
				FormatEntityTypeName(entities[0].GetType().Name),
				Thread.CurrentPrincipal.Identity.Name);

			foreach (var entity in entities)
			{
				logMessage.Append(entity);
			}

			Logger.Info(logMessage.ToString());
		}

		public string FormatEntityTypeName(string entityTypeName)
		{
			if (entityTypeName.Contains("_"))
			{
				entityTypeName = entityTypeName.Split('_').First();
			}
			return entityTypeName;
		}
	}
}