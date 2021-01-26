using System;
using System.Collections.Generic;
using System.Text;

namespace AutoCenterDatabaseImplement.Models
{
    public class OrderCar
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int CarId { get; set; }

        public int Count { get; set; }

        public virtual Order Order { get; set; }

        public virtual Car Car { get; set; }
    }
}