using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AutoCenterDatabaseImplement.Models
{
    public class CarSpare
    {
        public int Id { get; set; }

        public int CarId { get; set; }

        public int SpareId { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public int Sum { get; set; }

        public virtual Spare Spare { get; set; }

        public virtual Car Car { get; set; }
    }
}