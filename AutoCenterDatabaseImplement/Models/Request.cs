using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AutoCenterDatabaseImplement.Models
{
    public class Request
    {
        public int Id { get; set; }

        [Required]
        public string RequestName { get; set; }

        [Required]
        public DateTime DateCreate { get; set; }

        [ForeignKey("RequestId")]
        public virtual List<SpareRequest> SpareRequests { get; set; }
    }
}