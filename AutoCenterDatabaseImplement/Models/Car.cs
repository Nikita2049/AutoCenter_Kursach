using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AutoCenterDatabaseImplement.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required]
        public string CarName { get; set; }

        [Required]
        public int Price { get; set; }

        public int FullPrice { get; set; }

        [Required]
        public int Year { get; set; }

        [ForeignKey("CarId")]
        public virtual List<CarSpare> CarSpares { get; set; }
    }
}