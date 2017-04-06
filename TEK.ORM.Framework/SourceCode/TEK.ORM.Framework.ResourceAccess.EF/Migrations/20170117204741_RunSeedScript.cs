using System;
using System.IO;

using Microsoft.EntityFrameworkCore.Migrations;

namespace TEK.ORM.Framework.ResourceAccess.EF.Migrations
{
    public partial class RunSeedScript : Migration
    {
		#if (!RELEASE)
		protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(File.ReadAllText
			(
				Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Seed\Seed.sql")
			));
		}
		#endif

        protected override void Down(MigrationBuilder migrationBuilder) { }
    }
}