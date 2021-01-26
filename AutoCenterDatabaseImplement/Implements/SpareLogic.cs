using AutoCenterDatabaseImplement.Models;
using AutoCenterBusinessLogic.BindingModel;
using AutoCenterBusinessLogic.Interfaces;
using AutoCenterBusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoCenterDatabaseImplement.Implements
{
    public class SpareLogic : ISpareLogic
    {
        public void CreateOrUpdate(SpareBindingModel model)
        {
            using (var context = new KorytoDatabase())
            {
                Spare element = context.Spares.FirstOrDefault(rec => rec.SpareName == model.SpareName && rec.Id != model.Id);

                if (element != null)
                {
                    throw new Exception("Уже есть деталь с таким названием");
                }

                if (model.Id.HasValue)
                {
                    element = context.Spares.FirstOrDefault(rec => rec.Id == model.Id);

                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Spare();
                    context.Spares.Add(element);
                }
                element.SpareName = model.SpareName;
                element.Price = model.Price;
                element.TotalAmount = model.TotalAmount;

                context.SaveChanges();
            }
        }

        public void Delete(SpareBindingModel model)
        {
            using (var context = new KorytoDatabase())
            {
                Spare element = context.Spares.FirstOrDefault(rec => rec.Id == model.Id);

                if (element != null)
                {
                    context.Spares.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public List<SpareViewModel> Read(SpareBindingModel model)
        {
            using (var context = new KorytoDatabase())
            {
                return context.Spares
                .Where(rec => model == null || rec.Id == model.Id)
                .Select(rec => new SpareViewModel
                {
                    Id = rec.Id,
                    SpareName = rec.SpareName,
                    Price = rec.Price,
                    TotalAmount = rec.TotalAmount
                })
                .ToList();
            }
        }
    }
}