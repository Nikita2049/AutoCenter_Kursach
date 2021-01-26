using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AutoCenterDatabaseImplement.Models
{
    public class Spare
    {
        public int Id { get; set; }

        [Required]
        public string SpareName { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int TotalAmount { get; set; }

        [ForeignKey("SpareId")]
        public virtual List<CarSpare> CarSpares { get; set; }

        [ForeignKey("SpareId")]
        public virtual List<SpareRequest> SpareRequests { get; set; }
    }
}