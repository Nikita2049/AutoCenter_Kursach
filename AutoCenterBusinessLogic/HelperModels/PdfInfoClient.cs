using AutoCenterBusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoCenterBusinessLogic.HelperModels
{
    class PdfInfoClient
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public List<OrderViewModel> Orders { get; set; }

        public List<CarViewModel> Cars { get; set; }
    }
}