using AutoCenterBusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoCenterBusinessLogic.HelperModels
{
    class WordInfoAvto
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public List<CarViewModel> Cars { get; set; }
    }
}