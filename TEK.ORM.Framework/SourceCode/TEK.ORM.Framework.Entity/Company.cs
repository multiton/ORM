using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TEK.ORM.Framework.Entity
{
	public class Company : TrackedEntity
	{
		[Required]
		[StringLength(255)]
		public string Name { get; set; }

        public virtual ICollection<OrderHeader> Orders { get; set; }
    }
}