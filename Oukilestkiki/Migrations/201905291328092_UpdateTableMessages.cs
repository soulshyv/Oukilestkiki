namespace Oukilestkiki.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableMessages : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "Destinataire_Id", "dbo.Utilisateurs");
            AddColumn("dbo.Messages", "DateEnvoi", c => c.DateTime(nullable: false));
            AddColumn("dbo.Messages", "Expediteur_Id", c => c.Int());
            AddColumn("dbo.Messages", "Utilisateur_Id", c => c.Int());
            CreateIndex("dbo.Messages", "Expediteur_Id");
            CreateIndex("dbo.Messages", "Utilisateur_Id");
            AddForeignKey("dbo.Messages", "Expediteur_Id", "dbo.Utilisateurs", "Id");
            AddForeignKey("dbo.Messages", "Utilisateur_Id", "dbo.Utilisateurs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "Utilisateur_Id", "dbo.Utilisateurs");
            DropForeignKey("dbo.Messages", "Expediteur_Id", "dbo.Utilisateurs");
            DropIndex("dbo.Messages", new[] { "Utilisateur_Id" });
            DropIndex("dbo.Messages", new[] { "Expediteur_Id" });
            DropColumn("dbo.Messages", "Utilisateur_Id");
            DropColumn("dbo.Messages", "Expediteur_Id");
            DropColumn("dbo.Messages", "DateEnvoi");
            AddForeignKey("dbo.Messages", "Destinataire_Id", "dbo.Utilisateurs", "Id");
        }
    }
}
