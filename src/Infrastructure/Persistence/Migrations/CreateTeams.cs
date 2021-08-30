using System;
using FluentMigrator;

namespace Infrastructure.Persistence.Migrations
{
    [AutoVersioningMigration(1, "Create TeamTable")]
    public class CreateTeams : Migration
    {
        public override void Up()
        {
            Create.Table("TestTable")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString(255).NotNullable().WithDefaultValue("Anonymous");
        }

        public override void Down()
        {
            Delete.Table("TestTable");
        }
    }
}