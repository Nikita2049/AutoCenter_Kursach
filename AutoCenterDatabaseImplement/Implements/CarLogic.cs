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
    public class CarLogic : ICarLogic
    {
        public void CreateOrUpdate(CarBindingModel model)
        {
            using (var context = new KorytoDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Car element = context.Cars.FirstOrDefault(rec => rec.CarName == model.CarName && rec.Id != model.Id);
                        if (element != null)
                        {
                            throw new Exception("Уже есть автомобиль с таким названием");
                        }
                        if (model.Id.HasValue)
                        {
                            element = context.Cars.FirstOrDefault(rec => rec.Id == model.Id);
                            if (element == null)
                            {
                                throw new Exception("Элемент не найден");
                            }
                        }
                        else
                        {
                            element = new Car();
                            context.Cars.Add(element);
                        }
                        element.CarName = model.CarName;
                        element.Price = model.Price;
                        element.FullPrice = model.FullPrice;
                        element.Year = model.Year;

                        context.SaveChanges();

                        if (model.Id.HasValue)
                        {
                            var CarSpares = context.CarSpares.Where(rec
                           => rec.CarId == model.Id.Value).ToList();
                            context.CarSpares.RemoveRange(CarSpares.Where(rec =>
                            !model.CarSpares.ContainsKey(rec.SpareId)).ToList());
                            context.SaveChanges();
                            foreach (var updateSpare in CarSpares)
                            {
                                updateSpare.Count =
                               model.CarSpares[updateSpare.SpareId].Item2;

                                model.CarSpares.Remove(updateSpare.SpareId);
                            }
                            context.SaveChanges();
                        }
                        foreach (var pc in model.CarSpares)
                        {
                            context.CarSpares.Add(new CarSpare
                            {
                                CarId = element.Id,
                                SpareId = pc.Key,
                                Count = pc.Value.Item2,
                                Sum = pc.Value.Item3
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

        public void Delete(CarBindingModel model)
        {
            using (var context = new KorytoDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.CarSpares.RemoveRange(context.CarSpares.Where(rec =>
                        rec.CarId == model.Id));
                        Car element = context.Cars.FirstOrDefault(rec => rec.Id == model.Id);
                        if (element != null)
                        {
                            context.Cars.Remove(element);
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

        public List<CarViewModel> Read(CarBindingModel model)
        {
            using (var context = new KorytoDatabase())
            {
                return context.Cars
                .Where(rec => model == null || rec.Id == model.Id)
                .ToList()
                .Select(rec => new CarViewModel
                {
                    Id = rec.Id,
                    CarName = rec.CarName,
                    Price = rec.Price,
                    FullPrice = rec.FullPrice,
                    Year = rec.Year,
                    CarSpares = context.CarSpares
                        .Include(recPC => recPC.Spare)
                        .Where(recPC => recPC.CarId == rec.Id)
                        .ToDictionary(recPC => recPC.SpareId, recPC =>
                             (recPC.Spare?.SpareName, recPC.Count, recPC.Sum))
                })
                .ToList();
            }
        }
    }
}