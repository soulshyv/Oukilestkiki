using BO;

namespace Oukilestkiki.ViewModels
{
    public class Animal
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Couleur { get; set; }
        public int Age { get; set; }
        public virtual TypeAnimal Type { get; set; }
        public virtual Utilisateur Maitre { get; set; }
    }
}