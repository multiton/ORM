using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

using TEK.ORM.Framework.Entity;
using TEK.ORM.Framework.Entity.Validate;

namespace TEK.ORM.Framework.ResourceAccess.EF
{
	public class AuditContext : DbContextBase
	{
		protected override void OnConfiguring(DbContextOptionsBuilder builder)
		{
			if (builder.IsConfigured) return;

			builder
				.UseSqlServer("Server=MSC-PK01HMG;Database=EF7PocAudit;Integrated Security=false;User Id=sa;password=stalker45")
				.ConfigureWarnings(x => x.Ignore(RelationalEventId.AmbientTransactionWarning));
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<AuditRecord>()
				.Property(b => b.ActionDate)
				.HasDefaultValueSql("GETDATE()");
		}

		protected override IList<ValidationError> Validate()
		{
			var entities = this.ChangeTracker.Entries().Where(x =>
				x.State == EntityState.Added || x.State == EntityState.Modified);

			var validationErrors = new List<ValidationError>();

			foreach (var entityEntry in entities)
			{
				var validateError = Validator.Validate(entityEntry.Entity);

				if (validateError != null)
				{
					validationErrors.Add(validateError);
				}
			}

			return validationErrors;
		}
	}
}