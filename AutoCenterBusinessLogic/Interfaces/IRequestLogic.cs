using AutoCenterBusinessLogic.BindingModel;
using AutoCenterBusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoCenterBusinessLogic.Interfaces
{
    public interface IRequestLogic
    {
        List<RequestViewModel> Read(RequestBindingModel model);

        void CreateOrUpdate(RequestBindingModel model);

        void Delete(RequestBindingModel model);
    }
}