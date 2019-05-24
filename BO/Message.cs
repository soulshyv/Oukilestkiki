using BO;
using Oukilestkiki.Enums;

namespace Oukilestkiki.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Contenu { get; set; }
        public TypeMessageEnum Type { get; set; }
        public Utilisateur Destinataire { get; set; }
        public Recherche Recherche { get; set; }
    }
}