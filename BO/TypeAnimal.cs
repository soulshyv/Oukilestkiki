using System.ComponentModel.DataAnnotations;

 namespace Oukilestkiki.ViewModels
{
    public class TypeAnimal
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Libelle { get; set; }
    }
}