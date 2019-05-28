using Oukilestkiki.Enums;

namespace BO
{
    public class Message
    {
        public int Id { get; set; }
        public string Contenu { get; set; }
        public TypeMessageEnum Type { get; set; }
        public virtual Utilisateur Destinataire { get; set; }
        public virtual Recherche Recherche { get; set; }
    }
}