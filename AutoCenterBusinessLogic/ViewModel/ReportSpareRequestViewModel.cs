using System;
using System.Collections.Generic;
using System.Text;

namespace AutoCenterBusinessLogic.ViewModel
{
    public class ReportSpareRequestViewModel
    {
        public string RequestName { get; set; }

        public DateTime DateCreate { get; set; }

        public string SpareName { get; set; }

        public int Count { get; set; }

        public List<Tuple<string, int>> Spares { get; set; }
    }
}