using AutoCenterBusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoCenterBusinessLogic.HelperModels
{
    class PdfInfo
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public List<CarViewModel> Cars { get; set; }

        public List<IGrouping<DateTime, RequestViewModel>> Requests { get; set; }
    }
}