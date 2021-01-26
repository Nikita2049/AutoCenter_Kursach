using AutoCenterBusinessLogic.BindingModel;
using AutoCenterBusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoCenterBusinessLogic.Interfaces
{
    public interface ICarLogic
    {
        List<CarViewModel> Read(CarBindingModel model);

        void CreateOrUpdate(CarBindingModel model);

        void Delete(CarBindingModel model);
    }
}