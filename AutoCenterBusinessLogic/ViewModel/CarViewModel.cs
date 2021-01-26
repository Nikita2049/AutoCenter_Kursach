using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace AutoCenterBusinessLogic.ViewModel
{
    [DataContract]
    public class CarViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [DisplayName("Название автомобиля")]
        public string CarName { get; set; }

        [DataMember]
        [DisplayName("Цена автомобиля без комплектации")]
        public int Price { get; set; }

        [DataMember]
        [DisplayName("Цена автомобиля")]
        public int FullPrice { get; set; }

        [DataMember]
        [DisplayName("Год выпуска")]
        public int Year { get; set; }

        public Dictionary<int, (string, int, int)> CarSpares { get; set; }
    }
}