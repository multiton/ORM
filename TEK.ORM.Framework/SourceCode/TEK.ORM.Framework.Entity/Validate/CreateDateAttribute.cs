using System;
using System.ComponentModel.DataAnnotations;

namespace TEK.ORM.Framework.Entity.Validate
{
	public class CreateDateAttribute :  ValidationAttribute
	{
		public override bool IsValid(object value)
		{
			return Convert.ToDateTime(value).Date != DateTime.MinValue.Date;
		}
	}
}