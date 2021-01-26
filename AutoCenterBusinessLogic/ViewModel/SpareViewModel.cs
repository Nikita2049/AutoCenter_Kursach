using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace AutoCenterBusinessLogic.ViewModel
{
    [DataContract]
    public class SpareViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [DisplayName("Название детали")]
        public string SpareName { get; set; }

        [DataMember]
        [DisplayName("Цена детали")]
        public int Price { get; set; }

        [DataMember]
        [DisplayName("Количество детали")]
        public int TotalAmount { get; set; }
    }
}