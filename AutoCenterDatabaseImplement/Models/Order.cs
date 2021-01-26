using AutoCenterBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AutoCenterDatabaseImplement.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        [Required]
        public DateTime DateCreate { get; set; }

        public DateTime? DateImplement { get; set; }

        [ForeignKey("OrderId")]
        public virtual List<OrderCar> OrderCars { get; set; }

        public Client Client { get; set; }
    }
}