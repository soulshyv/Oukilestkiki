namespace Oukilestkiki.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAnimalMaitre : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Animals", "Maitre_Id", c => c.Int());
            CreateIndex("dbo.Animals", "Maitre_Id");
            AddForeignKey("dbo.Animals", "Maitre_Id", "dbo.Utilisateurs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Animals", "Maitre_Id", "dbo.Utilisateurs");
            DropIndex("dbo.Animals", new[] { "Maitre_Id" });
            DropColumn("dbo.Animals", "Maitre_Id");
        }
    }
}
