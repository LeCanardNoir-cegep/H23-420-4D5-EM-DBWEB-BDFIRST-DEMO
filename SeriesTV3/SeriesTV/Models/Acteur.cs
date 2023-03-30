using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SeriesTV.Models
{
    [Table("Acteur", Schema = "Acteurs")]
    public partial class Acteur
    {
        public Acteur()
        {
            ActeurSeries = new HashSet<ActeurSerie>();
            AuditActeurSeries = new HashSet<AuditActeurSerie>();
        }

        [Key]
        [Column("ActeurID")]
        public int ActeurId { get; set; }
        [StringLength(50)]
        public string Prenom { get; set; } = null!;
        [StringLength(50)]
        public string Nom { get; set; } = null!;
        [Column(TypeName = "date")]
        public DateTime DateNaissance { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateDeces { get; set; }

        [InverseProperty("Acteur")]
        public virtual ICollection<ActeurSerie> ActeurSeries { get; set; }
        [InverseProperty("Acteur")]
        public virtual ICollection<AuditActeurSerie> AuditActeurSeries { get; set; }
    }
}
