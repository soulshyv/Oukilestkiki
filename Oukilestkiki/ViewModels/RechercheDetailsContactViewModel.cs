using System.ComponentModel;
using System.Web;
using BO;

namespace Oukilestkiki.ViewModels
{
    public class RechercheDetailsContactViewModel
    {
        public Recherche Recherche { get; set; }

        public string ContenuMessage { get; set; }

        [DisplayName("Pièces jointes")]
        public HttpPostedFileBase[] PiecesJointes { get; set; }

        // Pour les labels
        public Message Message { get; set; }
    }
}