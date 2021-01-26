using AutoCenterBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoCenterWeb.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateImplement { get; set; }
        public int Price { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderCarModel> OrderCars { get; set; }
    }
}