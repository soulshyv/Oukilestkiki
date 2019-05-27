using System;
using System.Collections.Generic;
using Oukilestkiki.ViewModels;

namespace BO
{
    public class Recherche
    {
        public int Id { get; set; }
        public string Localisation { get; set; }
        public DateTime DerniereApparition { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; } = true;
        public virtual Animal Animal { get; set; }
        public virtual List<Photo> Photos { get; set; }
    }
}