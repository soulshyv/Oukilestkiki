using System.Web;
using BO;
using Oukilestkiki.ViewModels;

namespace Oukilestkiki.Models
{
    public class PhotoRechercheModel
    {
        public HttpPostedFileBase Photo { get; set; }
        public Recherche Recherche { get; set; }
        public Animal Animal { get; set; }
    }
}