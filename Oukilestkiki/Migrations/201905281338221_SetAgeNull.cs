namespace Oukilestkiki.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetAgeNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Animals", "Age", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Animals", "Age", c => c.Int(nullable: false));
        }
    }
}
