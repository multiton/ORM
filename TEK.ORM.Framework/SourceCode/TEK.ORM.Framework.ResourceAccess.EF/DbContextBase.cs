using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using TEK.ORM.Framework.Entity.Validate;

namespace TEK.ORM.Framework.ResourceAccess.EF
{
	public abstract class DbContextBase : DbContext
	{
		public override int SaveChanges(bool acceptAllChangesOnSuccess)
		{
			this.ChangeTracker.DetectChanges();

			var validationErrors = this.Validate();

			if (validationErrors.Any())
			{
				throw new ValidationException(validationErrors);
			}

			// Set AutoDetectChangesEnabled = false, before call base SaveChanges
			// is for performance reasons, to avoid calling DetectChanges() again.
			ChangeTracker.AutoDetectChangesEnabled = false;
			var result = base.SaveChanges(acceptAllChangesOnSuccess);
			ChangeTracker.AutoDetectChangesEnabled = true;

			return result;
		}

		public override Task<int> SaveChangesAsync(
			bool acceptAllChangesOnSuccess,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			this.ChangeTracker.DetectChanges();

			// ToDo: refactor validation for asynchronous scenario

			var validationErrors = this.Validate();

			if (validationErrors.Any())
			{
				throw new ValidationException(validationErrors);
			}

			return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}

		protected abstract IList<ValidationError> Validate();
	}
}