using AutoCenterDatabaseImplement.Models;
using AutoCenterBusinessLogic.BindingModel;
using AutoCenterBusinessLogic.Interfaces;
using AutoCenterBusinessLogic.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoCenterDatabaseImplement.Implements
{
    public class RequestLogic : IRequestLogic
    {
        public void CreateOrUpdate(RequestBindingModel model)
        {
            using (var context = new KorytoDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Request element = context.Requests.FirstOrDefault(rec => rec.RequestName == model.RequestName && rec.Id != model.Id);
                        if (element != null)
                        {
                            throw new Exception("Уже есть заявка с таким названием");
                        }
                        if (model.Id.HasValue)
                        {
                            element = context.Requests.FirstOrDefault(rec => rec.Id == model.Id);
                            if (element == null)
                            {
                                throw new Exception("Элемент не найден");
                            }
                        }
                        else
                        {
                            element = new Request();
                            context.Requests.Add(element);
                        }
                        element.RequestName = model.RequestName;
                        element.DateCreate = model.DateCreate;

                        context.SaveChanges();

                        if (model.Id.HasValue)
                        {
                            var SpareRequests = context.SpareRequests.Where(rec
                           => rec.RequestId == model.Id.Value).ToList();
                            context.SpareRequests.RemoveRange(SpareRequests.Where(rec =>
                            !model.SpareRequests.ContainsKey(rec.SpareId)).ToList());
                            context.SaveChanges();
                            foreach (var updateSpare in SpareRequests)
                            {
                                updateSpare.Count = model.SpareRequests[updateSpare.SpareId].Item2;

                                model.SpareRequests.Remove(updateSpare.SpareId);
                            }
                            context.SaveChanges();
                        }
                        foreach (var pc in model.SpareRequests)
                        {
                            context.SpareRequests.Add(new SpareRequest
                            {
                                RequestId = element.Id,
                                SpareId = pc.Key,
                                Count = pc.Value.Item2
                            });
                            context.SaveChanges();
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Delete(RequestBindingModel model)
        {
            using (var context = new KorytoDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.SpareRequests.RemoveRange(context.SpareRequests.Where(rec => rec.RequestId == model.Id));
                        Request element = context.Requests.FirstOrDefault(rec => rec.Id == model.Id);
                        if (element != null)
                        {
                            context.Requests.Remove(element);
                            context.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Элемент не найден");
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public List<RequestViewModel> Read(RequestBindingModel model)
        {
            using (var context = new KorytoDatabase())
            {
                return context.Requests
                 .Where(rec => model == null || rec.Id == model.Id
                  || (model.DateFrom.HasValue && model.DateTo.HasValue && rec.DateCreate >= model.DateFrom && rec.DateCreate <= model.DateTo))
                 .ToList()
                 .Select(rec => new RequestViewModel
                 {
                     Id = rec.Id,
                     RequestName = rec.RequestName,
                     DateCreate = rec.DateCreate,
                     SpareRequests = context.SpareRequests
                        .Include(recPC => recPC.Spare)
                        .Where(recPC => recPC.RequestId == rec.Id)
                        .ToDictionary(recPC => recPC.SpareId, recPC =>
                             (recPC.Spare?.SpareName, recPC.Count))
                 })
                 .ToList();
            }
        }
    }
}