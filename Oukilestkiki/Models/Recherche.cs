using System;
using System.Collections.Generic;

namespace Oukilestkiki.Models
{
    public class Recherche
    {
        public int Id { get; set; }
        public string Localisation { get; set; }
        public DateTime DerniereApparition { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public Animal Animal { get; set; }
        public List<Photo> Photos { get; set; }
    }
}