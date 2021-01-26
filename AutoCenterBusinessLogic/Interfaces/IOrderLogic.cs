using AutoCenterBusinessLogic.BindingModel;
using AutoCenterBusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoCenterBusinessLogic.Interfaces
{
    public interface IOrderLogic
    {
        List<OrderViewModel> Read(OrderBindingModel model);

        void CreateOrUpdate(OrderBindingModel model);

        void Delete(OrderBindingModel model);
    }
}