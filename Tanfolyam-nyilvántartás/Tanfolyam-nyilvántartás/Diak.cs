using System;
using System.Collections.Generic;

namespace Tanfolyam_nyilvántartás
{
    public partial class Diak
    {
        public int DiakId { get; set; }
        public string Nev { get; set; } = null!;
        public string? SzamlazasiNev { get; set; }
        public string? SzamlazasiCim { get; set; }
    }
}
