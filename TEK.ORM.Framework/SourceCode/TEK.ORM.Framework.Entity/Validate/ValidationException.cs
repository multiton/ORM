using System;
using System.Text;
using System.Collections.Generic;

namespace TEK.ORM.Framework.Entity.Validate
{
	public class ValidationException : Exception
	{
		public readonly IEnumerable<ValidationError> ValidationErrors;

		public ValidationException(IEnumerable<ValidationError> validationErrors)
		{
			this.ValidationErrors = validationErrors;
		}

		public virtual string GetErrorMessage()
		{
			if (this.ValidationErrors == null) return string.Empty;

			var errorMessage = new StringBuilder("\n\n");

			foreach (var validationError in this.ValidationErrors)
			{
				errorMessage
					.AppendFormat("[{0}]:", validationError.EntityName)
					.AppendLine();

				foreach (var err in validationError.ValidationResults)
				{
					errorMessage.Append("-").AppendLine(err.ErrorMessage);
				}
			}

			return errorMessage.AppendLine().ToString();
		}

		public override string ToString()
		{
			return $"{this.GetErrorMessage()} {base.ToString()}";
		}
	}
}