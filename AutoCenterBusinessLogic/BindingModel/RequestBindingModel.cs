using System;
using System.Collections.Generic;
using System.Text;

namespace AutoCenterBusinessLogic.BindingModel
{
    public class RequestBindingModel
    {
        public int? Id { get; set; }
        public int? SpareId { get; set; }
        public string RequestName { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public DateTime DateCreate { get; set; }
        public Dictionary<int, (string, int)> SpareRequests { get; set; }
    }
}