using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using BO;

namespace Oukilestkiki.ViewModels
{
    public class RechercheCreateEditViewModel
    {
        public Recherche Recherche { get; set; }

        [DisplayName("Photos")]
        public HttpPostedFileBase[] ImageFiles { get; set; }
    }
}