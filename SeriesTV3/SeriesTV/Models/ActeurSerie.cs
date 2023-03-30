using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SeriesTV.Models
{
    [Table("ActeurSerie", Schema = "Series")]
    [Index("ActeurId", "SerieId", Name = "UC_ActeurSerie_ActeurEtSerie", IsUnique = true)]
    public partial class ActeurSerie
    {
        [Key]
        [Column("ActeurSerieID")]
        public int ActeurSerieId { get; set; }
        [Column("ActeurID")]
        public int ActeurId { get; set; }
        [Column("SerieID")]
        public int SerieId { get; set; }

        [ForeignKey("ActeurId")]
        [InverseProperty("ActeurSeries")]
        public virtual Acteur Acteur { get; set; } = null!;
        [ForeignKey("SerieId")]
        [InverseProperty("ActeurSeries")]
        public virtual Serie Serie { get; set; } = null!;
    }
}
