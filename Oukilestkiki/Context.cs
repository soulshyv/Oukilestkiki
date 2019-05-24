using System.Data.Entity;
using BO;
using Oukilestkiki.ViewModels;

namespace Oukilestkiki
{
    public class Context : DbContext
    {
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Animal> Animaux { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Recherche> Recherches { get; set; }
        public DbSet<TypeAnimal> TypeAnimaux { get; set; }
    }
}