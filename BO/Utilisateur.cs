using System.Collections.Generic;
using Oukilestkiki.Models;

namespace BO
{
    public class Utilisateur
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public bool Suspendu { get; set; } = false;
        public virtual Role Role { get; set; }
        public List<Message> Messages { get; set; }
    }
}