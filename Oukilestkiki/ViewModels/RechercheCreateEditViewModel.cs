using BO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;

namespace Oukilestkiki.ViewModels
{
    public class RechercheCreateEditViewModel
    {
        public Recherche Recherche { get; set; }

        [DisplayName("Photos pour la recherche")]
        public HttpPostedFileBase[] ImageFilesRecherche { get; set; }

        [DisplayName("Photos de l'animal")]
        public HttpPostedFileBase[] ImageFilesAnimal { get; set; }

        public List<TypeAnimal> Types { get; set; }

        public int TypeId { get; set; }
    }
}