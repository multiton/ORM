using System;
using System.Linq;
using System.Collections.Generic;
using cm = System.ComponentModel.DataAnnotations;

namespace TEK.ORM.Framework.Entity.Validate
{
	public class Validator
	{
		public static ValidationError Validate(object entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			var results = new List<cm.ValidationResult>();

			if (cm.Validator.TryValidateObject(entity, new cm.ValidationContext(entity), results, true))
			{
				return null;
			}

			return new ValidationError(
				entity.GetType().Name,
				results.Where(x => x != cm.ValidationResult.Success));
		}
	}
}