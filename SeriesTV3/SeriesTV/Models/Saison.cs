using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SeriesTV.Models
{
    [Table("Saison", Schema = "Series")]
    [Index("Num", "SerieId", Name = "UC_Saison_Num", IsUnique = true)]
    public partial class Saison
    {
        [Key]
        [Column("SaisonID")]
        public int SaisonId { get; set; }
        public int Num { get; set; }
        public int? NbEpisodes { get; set; }
        [Column("SerieID")]
        public int SerieId { get; set; }

        [ForeignKey("SerieId")]
        [InverseProperty("Saisons")]
        public virtual Serie Serie { get; set; } = null!;
    }
}
