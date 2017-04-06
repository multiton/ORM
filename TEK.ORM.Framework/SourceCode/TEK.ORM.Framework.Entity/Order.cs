using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TEK.ORM.Framework.Entity
{
	public class OrderHeader : TrackedEntity
	{
		[Required]
		[StringLength(255)]
		public string Number { get; set; }

		[Required]
		public virtual Company Supplier { get; set; }

		public virtual ICollection<OrderItem> OdrerItems { get; set; }
	}
}