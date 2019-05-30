using System.Collections.Generic;
using Oukilestkiki.ViewModels;

namespace BO
{
    public class Photo
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public virtual Animal Animal { get; set; }
        public virtual List<Recherche> Recherches { get; set; }
        public virtual Message Message { get; set; }
    }
}