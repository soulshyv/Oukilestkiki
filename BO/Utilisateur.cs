using System.Collections.Generic;
using Oukilestkiki.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace BO
{
    public class Utilisateur
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Nom { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Prenom { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 6)]
        public string Mail { get; set; }
        public bool Suspendu { get; set; } = false;
        public virtual Role Role { get; set; }
        public List<Message> Messages { get; set; }
    }
}