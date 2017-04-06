using System;
using System.ComponentModel.DataAnnotations;

namespace TEK.ORM.Framework.Entity
{
	public class AuditRecord : BaseAuditEntity
	{
		[Required]
		[MinLength(3)]
		[MaxLength(55)]
		public string UserLoginName { get; set; }

		[Required]
		[MinLength(3)]
		[MaxLength(255)]
		public string ActionName { get; set; }

		[Required]
		public DateTime ActionDate { get; set; }
	}
}