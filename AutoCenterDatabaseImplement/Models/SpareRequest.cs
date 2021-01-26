using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AutoCenterDatabaseImplement.Models
{
    public class SpareRequest
    {
        public int Id { get; set; }

        public int RequestId { get; set; }

        public int SpareId { get; set; }

        [Required]
        public int Count { get; set; }

        public virtual Spare Spare { get; set; }

        public virtual Request Request { get; set; }
    }
}