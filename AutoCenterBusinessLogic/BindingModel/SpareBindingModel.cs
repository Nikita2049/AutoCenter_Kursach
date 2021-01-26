using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AutoCenterBusinessLogic.BindingModel
{
    [DataContract]
    public class SpareBindingModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public string SpareName { get; set; }
        [DataMember]
        public int Price { get; set; }
        [DataMember]
        public int TotalAmount { get; set; }
    }
}