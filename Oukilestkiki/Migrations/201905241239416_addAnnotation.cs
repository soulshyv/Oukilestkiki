namespace Oukilestkiki.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAnnotation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Utilisateurs", "Nom", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Utilisateurs", "Prenom", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Utilisateurs", "Password", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Utilisateurs", "Mail", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.TypeAnimals", "Libelle", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TypeAnimals", "Libelle", c => c.String());
            AlterColumn("dbo.Utilisateurs", "Mail", c => c.String());
            AlterColumn("dbo.Utilisateurs", "Password", c => c.String());
            AlterColumn("dbo.Utilisateurs", "Prenom", c => c.String());
            AlterColumn("dbo.Utilisateurs", "Nom", c => c.String());
        }
    }
}
