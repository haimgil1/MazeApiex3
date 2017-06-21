namespace AP2ex3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Wins", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "Losses", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Losses");
            DropColumn("dbo.Users", "Wins");
        }
    }
}
