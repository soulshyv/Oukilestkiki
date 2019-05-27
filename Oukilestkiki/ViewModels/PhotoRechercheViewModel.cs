using System.Collections.Generic;
using System.Web;
using BO;

namespace Oukilestkiki.ViewModels
{
    public class PhotoRechercheViewModel
    {
        public HttpPostedFileBase[] Photos { get; set; }
        public Recherche Recherche { get; set; }
        public Animal Animal { get; set; }
    }
}