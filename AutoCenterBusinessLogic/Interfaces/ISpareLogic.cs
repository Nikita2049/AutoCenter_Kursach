using AutoCenterBusinessLogic.BindingModel;
using AutoCenterBusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoCenterBusinessLogic.Interfaces
{
    public interface ISpareLogic
    {
        List<SpareViewModel> Read(SpareBindingModel model);

        void CreateOrUpdate(SpareBindingModel model);

        void Delete(SpareBindingModel model);
    }
}