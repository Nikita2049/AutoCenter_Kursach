using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AutoCenterBusinessLogic.BindingModel
{
    [DataContract]
    public class CarBindingModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public string CarName { get; set; }
        [DataMember]
        public int Price { get; set; }
        [DataMember]
        public int FullPrice { get; set; }
        [DataMember]
        public int Year { get; set; }
        public Dictionary<int, (string, int, int)> CarSpares { get; set; }
    }
}