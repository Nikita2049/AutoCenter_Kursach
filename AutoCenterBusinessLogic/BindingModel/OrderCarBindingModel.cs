using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AutoCenterBusinessLogic.BindingModel
{
    [DataContract]
    public class OrderCarBindingModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public int CarId { get; set; }
        [DataMember]
        public int ClientId { get; set; }
        [DataMember]
        public int? OrderId { get; set; }
        [DataMember]
        public int Count { get; set; }
        [DataMember]
        public int Price { get; set; }
    }
}