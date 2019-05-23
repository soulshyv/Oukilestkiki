using System.Collections.Generic;

namespace Oukilestkiki.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public Animal Animal { get; set; }
        public List<Recherche> Recherches { get; set; }
    }
}