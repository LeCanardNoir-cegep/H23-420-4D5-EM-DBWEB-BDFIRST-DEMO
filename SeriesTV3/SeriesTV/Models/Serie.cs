using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SeriesTV.Models
{
    [Table("Serie", Schema = "Series")]
    public partial class Serie
    {
        public Serie()
        {
            ActeurSeries = new HashSet<ActeurSerie>();
            AuditActeurSeries = new HashSet<AuditActeurSerie>();
            Saisons = new HashSet<Saison>();
        }

        [Key]
        [Column("SerieID")]
        public int SerieId { get; set; }
        [StringLength(100)]
        public string Nom { get; set; } = null!;
        public int AnneeDebut { get; set; }
        public int? AnneeFin { get; set; }

        [InverseProperty("Serie")]
        public virtual ICollection<ActeurSerie> ActeurSeries { get; set; }
        [InverseProperty("Serie")]
        public virtual ICollection<AuditActeurSerie> AuditActeurSeries { get; set; }
        [InverseProperty("Serie")]
        public virtual ICollection<Saison> Saisons { get; set; }
    }
}
