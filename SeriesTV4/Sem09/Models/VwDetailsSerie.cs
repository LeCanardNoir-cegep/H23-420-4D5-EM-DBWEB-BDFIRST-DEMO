using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Sem09.Models
{
    [Keyless]
    public partial class VwDetailsSerie
    {
        [Column("SerieID")]
        public int SerieId { get; set; }
        [StringLength(100)]
        public string Nom { get; set; } = null!;
        public int AnneeDebut { get; set; }
        public int? AnneeFin { get; set; }
        public int? NbActeurs { get; set; }
        public int? NbEpisodesTotal { get; set; }
        public int? NbSaisons { get; set; }
    }
}
