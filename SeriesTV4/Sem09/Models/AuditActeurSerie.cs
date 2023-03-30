using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Sem09.Models
{
    [Table("AuditActeurSerie", Schema = "Series")]
    public partial class AuditActeurSerie
    {
        [Key]
        [Column("AuditActeurSerieID")]
        public int AuditActeurSerieId { get; set; }
        [StringLength(6)]
        [Unicode(false)]
        public string Action { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime DateAction { get; set; }
        [Column("ActeurID")]
        public int ActeurId { get; set; }
        [Column("SerieID")]
        public int SerieId { get; set; }

        [ForeignKey("ActeurId")]
        [InverseProperty("AuditActeurSeries")]
        public virtual Acteur Acteur { get; set; } = null!;
        [ForeignKey("SerieId")]
        [InverseProperty("AuditActeurSeries")]
        public virtual Serie Serie { get; set; } = null!;
    }
}
