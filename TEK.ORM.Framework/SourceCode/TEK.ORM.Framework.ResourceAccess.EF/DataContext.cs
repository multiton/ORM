using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using TEK.ORM.Framework.Entity;
using TEK.ORM.Framework.Entity.Validate;

namespace TEK.ORM.Framework.ResourceAccess.EF
{
	public class DataContext : DbContextBase
	{	
		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Company>().HasIndex(c => c.Name).IsUnique();
			builder.Entity<Product>().HasIndex(c => c.Name).IsUnique();
			builder.Entity<OrderHeader>().HasIndex(c => c.Number).IsUnique();

			foreach (var entityType in builder.Model.GetEntityTypes()
				.Where(e => typeof(TrackedEntity).IsAssignableFrom(e.ClrType)))
			{
				builder.Entity(entityType.ClrType).Property<DateTime>("DateCreated")
					.HasDefaultValueSql("GETDATE()")
					.IsRequired();

				builder.Entity(entityType.ClrType).Property<DateTime?>("DateModified");

				builder.Entity(entityType.ClrType).Property<string>("UserCreated")
					.HasMaxLength(64)
					.IsRequired();

				builder.Entity(entityType.ClrType).Property<string>("UserModified")
					.HasMaxLength(64);
			}

			base.OnModelCreating(builder);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder builder)
		{
			if (builder.IsConfigured) return;

            builder.UseSqlServer("Server=huron;Database=EF7Poc;Integrated Security=false;uid=sa;password=stalker45");
		}

		protected override IList<ValidationError> Validate()
		{
			var utcNow = DateTime.UtcNow;

			var userName = GetAuthenticatedUserName();

			var entities = this.ChangeTracker.Entries().Where(x => 
				x.State == EntityState.Added || x.State == EntityState.Modified);

			var validationErrors = new List<ValidationError>();

			foreach (var entityEntry in entities)
			{
				SetTrackingInfo(entityEntry, userName, utcNow);

				var validateError = Validator.Validate(entityEntry.Entity);

				if (validateError != null)
				{
					validationErrors.Add(validateError);
				}
			}

			return validationErrors;
		}

		private static void SetTrackingInfo(EntityEntry entityEntry, string userName, DateTime utcNow)
		{
			if (!(entityEntry.Entity is TrackedEntity)) return;

			if (entityEntry.State == EntityState.Added)
			{
				entityEntry.Property("DateCreated").CurrentValue = utcNow;
				entityEntry.Property("UserCreated").CurrentValue = userName;
			}
			else
			{
				entityEntry.Property("DateModified").CurrentValue = utcNow;
				entityEntry.Property("UserModified").CurrentValue = userName;
			}
		}

		private static string GetAuthenticatedUserName()
		{
			var userName = Thread.CurrentPrincipal.Identity.Name;

			if (string.IsNullOrEmpty(userName))
			{
				userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
			}

			return userName;
		}
	}
}