using System.ComponentModel.DataAnnotations;

namespace TEK.ORM.Framework.Entity
{
	public class OrderItem : TrackedEntity
	{
		[Required]
		public int Quantity { get; set; }

		[Required]
		public decimal Price { get; set; }

		public virtual Product Product { get; set; }

		public virtual OrderHeader Order { get; set; }
	}
}