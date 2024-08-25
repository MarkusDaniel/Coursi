using System;
using System.Collections.Generic;

namespace Tanfolyam_nyilvántartás
{
    public partial class Tanfolyam
    {
        public int TanfolyamId { get; set; }
        public string Nev { get; set; } = null!;
        public DateTime KezdetDatuma { get; set; }
        public DateTime? VegzesDatuma { get; set; }
        public decimal KoltsegPerFo { get; set; }
        public bool? Aktiv { get; set; }
        public int? TanarId { get; set; }

        public virtual Tanar? Tanar { get; set; }
    }
}
