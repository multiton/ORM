using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TEK.ORM.Framework.Entity
{
	public abstract class IdentifiedEntity<TId> where TId : struct
    {
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public TId Id { get; set; }

		public virtual bool IsNew => EqualityComparer<TId>.Default.Equals(this.Id, default(TId));
    }
}