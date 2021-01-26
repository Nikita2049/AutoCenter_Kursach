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
    public class OrderLogic : IOrderLogic
    {
        public void CreateOrUpdate(OrderBindingModel model)
        {
            using (var context = new KorytoDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Order element = model.Id.HasValue ? null : new Order();
                        if (model.Id.HasValue)
                        {
                            element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                            if (element == null)
                            {
                                throw new Exception("Элемент не найден");
                            }
                            element.ClientId = model.ClientId;
                            element.DateCreate = model.DateCreate;
                            element.DateImplement = model.DateImplement;
                            element.Price = model.Price;
                            element.Status = model.Status;
                            context.SaveChanges();
                        }
                        else
                        {
                            element.ClientId = model.ClientId;
                            element.DateCreate = model.DateCreate;
                            element.DateImplement = model.DateImplement;
                            element.Price = model.Price;
                            element.Status = model.Status;
                            context.Orders.Add(element);
                            context.SaveChanges();
                            var groupCars = model.OrderCars
                               .GroupBy(rec => rec.CarId)
                               .Select(rec => new
                               {
                                   CarId = rec.Key,
                                   Count = rec.Sum(r => r.Count)
                               });

                            foreach (var groupCar in groupCars)
                            {
                                context.OrderCars.Add(new OrderCar
                                {
                                    OrderId = element.Id,
                                    CarId = groupCar.CarId,
                                    Count = groupCar.Count
                                });
                                context.SaveChanges();
                            }
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

        public void Delete(OrderBindingModel model)
        {
            using (var context = new KorytoDatabase())
            {
                Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id.Value);

                if (element != null)
                {
                    context.Orders.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            using (var context = new KorytoDatabase())
            {
                List<OrderViewModel> result = new List<OrderViewModel>();

                if (model != null)
                {
                    if ((model.DateTo != null) && (model.DateFrom != null))
                    {
                        result.AddRange(context.Orders
                        .Where(rec => (rec.Id == model.Id || rec.ClientId == model.ClientId) && (rec.DateCreate >= model.DateFrom && rec.DateCreate <= model.DateTo))
                        .Select(rec => CreateViewModel(rec)));
                    }
                    else
                    {
                        result.AddRange(context.Orders
                        .Where(rec => rec.Id == model.Id || rec.ClientId == model.ClientId)
                        .Select(rec => CreateViewModel(rec)));
                    }
                }
                else
                {
                    result.AddRange(context.Orders.Select(rec => CreateViewModel(rec)));
                }
                return result;
            }
        }

        static private OrderViewModel CreateViewModel(Order order)
        {
            using (var context = new KorytoDatabase())
            {
                var cars = context.OrderCars
                    .Where(rec => rec.OrderId == order.Id)
                    .Include(rec => rec.Car)
                    .Select(rec => new OrderCarViewModel
                    {
                        Id = rec.Id,
                        OrderId = rec.OrderId,
                        CarId = rec.CarId,
                        CarName = rec.Car.CarName,
                        Count = rec.Count
                    }).ToList();

                foreach (var car in cars)
                {
                    var carData = context.Cars.Where(rec => rec.Id == car.CarId).FirstOrDefault();

                    if (carData != null)
                    {
                        car.CarName = carData.CarName;
                        car.Year = carData.Year;
                        car.Price = carData.Price;
                    }
                }

                return new OrderViewModel
                {
                    Id = order.Id,
                    ClientId = order.ClientId,
                    ClientFIO = context.Clients.Where(rec => rec.Id == order.ClientId).Select(rec => rec.ClientFIO).FirstOrDefault(),
                    DateCreate = order.DateCreate,
                    DateImplement = order.DateImplement,
                    Status = order.Status,
                    Price = order.Price,
                    OrderCars = cars
                };
            }
        }
    }
}