using System.Web;
using BO;

namespace Oukilestkiki.ViewModels
{
    public class RechercheCreateEditViewModel
    {
        public Recherche Recherche { get; set; }
        public HttpPostedFileBase[] Photos { get; set; }
    }
}