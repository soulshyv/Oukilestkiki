using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Oukilestkiki.Enums;

namespace BO
{
    public class Message
    {
        public int Id { get; set; }
        public string Contenu { get; set; }
        public TypeMessageEnum Type { get; set; }
        [DisplayName("Date d'envoi")]
        public DateTime DateEnvoi { get; set; }
        public virtual Utilisateur Expediteur { get; set; }
        public virtual Utilisateur Destinataire { get; set; }
        public virtual Recherche Recherche { get; set; }
        [DisplayName("Pièces jointes")]
        public virtual List<Photo> PiecesJointes { get; set; }
    }
}