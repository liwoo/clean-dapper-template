using FluentMigrator;

namespace Infrastructure.Persistence.Migrations
{
    [AutoVersioningMigration(1, "Create TeamTable")]
    public class CreateTeams : Migration
    {
        public override void Up()
        {
            if (!Schema.Table("teams").Exists())
            {
                Create.Table("teams")
                    .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                    .WithColumn("name").AsString(255).NotNullable()
                    .WithColumn("position").AsInt32().NotNullable()
                    .WithColumn("home_kit_color").AsString(255).NotNullable()
                    .WithColumn("stadium").AsString(255).NotNullable()
                    .WithColumn("city").AsString(255).NotNullable();
            }
        }

        public override void Down()
        {
            Delete.Table("teams");
        }
    }
}