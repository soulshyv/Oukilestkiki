using BO;

namespace Oukilestkiki.ViewModels
{
    public class Animal
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Couleur { get; set; }
        public int Age { get; set; }
        public TypeAnimal Type { get; set; }
        public Utilisateur Maitre { get; set; }
    }
}