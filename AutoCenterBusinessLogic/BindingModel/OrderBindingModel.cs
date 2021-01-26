using AutoCenterBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AutoCenterBusinessLogic.BindingModel
{
    [DataContract]
    public class OrderBindingModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public int ClientId { get; set; }
        [DataMember]
        public int? CarId { get; set; }
        [DataMember]
        public int Price { get; set; }
        [DataMember]
        public OrderStatus Status { get; set; }
        [DataMember]
        public DateTime DateCreate { get; set; }
        [DataMember]
        public DateTime? DateFrom { get; set; }
        [DataMember]
        public DateTime? DateTo { get; set; }
        [DataMember]
        public DateTime? DateImplement { get; set; }
        public List<OrderCarBindingModel> OrderCars { get; set; }
    }
}