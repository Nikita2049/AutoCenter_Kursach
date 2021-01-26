using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoCenterWeb.Models
{
    public class CreateOrderModel
    {
        public Dictionary<int, int> OrderCars { get; set; }
    }
}