namespace TEK.ORM.Framework.BusinessLogic.Contract
{
	public interface IAccessLogger
	{
		void LogOperationResult(int changesCount, string entityTypeName);

		void LogOperationResult(int changesCount, string operation, object entity);

		void LogOperationResult(object[] entities);

		string FormatEntityTypeName(string entityTypeName);
	}
}