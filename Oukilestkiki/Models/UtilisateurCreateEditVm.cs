using System.Collections.Generic;

namespace Oukilestkiki.Models
{
    public class UtilisateurCreateEditVm
    {
        public Utilisateur Utilisateur { get; set; }
        public List<Role> ListeRoles { get; set; }
        public bool IsInscription { get; set; }
    }
}