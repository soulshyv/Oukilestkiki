namespace Oukilestkiki.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Animals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Couleur = c.String(),
                        Age = c.Int(nullable: false),
                        Type_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TypeAnimals", t => t.Type_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.TypeAnimals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Libelle = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Contenu = c.String(),
                        Type = c.Int(nullable: false),
                        Destinataire_Id = c.Int(),
                        Recherche_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Utilisateurs", t => t.Destinataire_Id)
                .ForeignKey("dbo.Recherches", t => t.Recherche_Id)
                .Index(t => t.Destinataire_Id)
                .Index(t => t.Recherche_Id);
            
            CreateTable(
                "dbo.Utilisateurs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Prenom = c.String(),
                        Password = c.String(),
                        Mail = c.String(),
                        Suspendu = c.Boolean(nullable: false),
                        Role_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.Role_Id)
                .Index(t => t.Role_Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Libelle = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Recherches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Localisation = c.String(),
                        DerniereApparition = c.DateTime(nullable: false),
                        Description = c.String(),
                        Active = c.Boolean(nullable: false),
                        Animal_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Animals", t => t.Animal_Id)
                .Index(t => t.Animal_Id);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        FilePath = c.String(),
                        Animal_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Animals", t => t.Animal_Id)
                .Index(t => t.Animal_Id);
            
            CreateTable(
                "dbo.PhotoRecherches",
                c => new
                    {
                        Photo_Id = c.Int(nullable: false),
                        Recherche_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Photo_Id, t.Recherche_Id })
                .ForeignKey("dbo.Photos", t => t.Photo_Id, cascadeDelete: true)
                .ForeignKey("dbo.Recherches", t => t.Recherche_Id, cascadeDelete: true)
                .Index(t => t.Photo_Id)
                .Index(t => t.Recherche_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "Recherche_Id", "dbo.Recherches");
            DropForeignKey("dbo.PhotoRecherches", "Recherche_Id", "dbo.Recherches");
            DropForeignKey("dbo.PhotoRecherches", "Photo_Id", "dbo.Photos");
            DropForeignKey("dbo.Photos", "Animal_Id", "dbo.Animals");
            DropForeignKey("dbo.Recherches", "Animal_Id", "dbo.Animals");
            DropForeignKey("dbo.Utilisateurs", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.Messages", "Destinataire_Id", "dbo.Utilisateurs");
            DropForeignKey("dbo.Animals", "Type_Id", "dbo.TypeAnimals");
            DropIndex("dbo.PhotoRecherches", new[] { "Recherche_Id" });
            DropIndex("dbo.PhotoRecherches", new[] { "Photo_Id" });
            DropIndex("dbo.Photos", new[] { "Animal_Id" });
            DropIndex("dbo.Recherches", new[] { "Animal_Id" });
            DropIndex("dbo.Utilisateurs", new[] { "Role_Id" });
            DropIndex("dbo.Messages", new[] { "Recherche_Id" });
            DropIndex("dbo.Messages", new[] { "Destinataire_Id" });
            DropIndex("dbo.Animals", new[] { "Type_Id" });
            DropTable("dbo.PhotoRecherches");
            DropTable("dbo.Photos");
            DropTable("dbo.Recherches");
            DropTable("dbo.Roles");
            DropTable("dbo.Utilisateurs");
            DropTable("dbo.Messages");
            DropTable("dbo.TypeAnimals");
            DropTable("dbo.Animals");
        }
    }
}
