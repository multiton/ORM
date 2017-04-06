using System;
using System.ComponentModel.DataAnnotations;
using TEK.ORM.Framework.Entity.Validate;

namespace TEK.ORM.Framework.Entity
{
	public abstract class TrackedEntity : BaseDataEntity
	{
		//[Required]
		//[CreateDate]
		//public DateTime DateCreated { get; set; }

		//[Required]
		//[MinLength(1)]
		//[MaxLength(64)]
		//public string UserCreated { get; set; }

		//public DateTime? DateModified { get; set; }

		//[MaxLength(64)]
		//public string UserModified { get; set; }
	}
}