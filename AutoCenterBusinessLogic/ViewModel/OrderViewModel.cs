using AutoCenterBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace AutoCenterBusinessLogic.ViewModel
{
    [DataContract]
    public class OrderViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int ClientId { get; set; }

        [DataMember]
        [DisplayName("ФИО Клиента")]
        public string ClientFIO { get; set; }

        [DataMember]
        [DisplayName("Название машины")]
        public string CarName { get; set; }

        [DataMember]
        [DisplayName("Сумма")]
        public int Price { get; set; }

        [DataMember]
        [DisplayName("Статус заказа")]
        public OrderStatus Status { get; set; }

        [DataMember]
        [DisplayName("Дата создания заказа")]
        public DateTime DateCreate { get; set; }

        [DataMember]
        [DisplayName("Дата завершения заказа")]
        public DateTime? DateImplement { get; set; }

        public List<OrderCarViewModel> OrderCars { get; set; }
    }
}