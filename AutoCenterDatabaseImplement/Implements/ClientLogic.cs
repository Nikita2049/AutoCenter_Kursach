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
    public class ClientLogic : IClientLogic
    {
        public void CreateOrUpdate(ClientBindingModel model)
        {
            using (var context = new KorytoDatabase())
            {
                Client element = model.Id.HasValue ? null : new Client();

                if (model.Id.HasValue)
                {
                    element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);

                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Client();
                    context.Clients.Add(element);
                }
                element.ClientFIO = model.ClientFIO;
                element.Password = model.Password;
                element.Mail = model.Mail == null ? element.Mail : model.Mail;
                element.Login = model.Login == null ? element.Login : model.Login;
                context.SaveChanges();
            }
        }

        public void Delete(ClientBindingModel model)
        {
            using (var context = new KorytoDatabase())
            {
                Client element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);

                if (element != null)
                {
                    context.Clients.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public List<ClientViewModel> Read(ClientBindingModel model)
        {
            using (var context = new KorytoDatabase())
            {
                return context.Clients
                .Where(rec => model == null
                   || (rec.Id == model.Id)
                   || (rec.Login == model.Login || rec.Mail == model.Mail)
                       && (model.Password == null || rec.Password == model.Password))
                .Select(rec => new ClientViewModel
                {
                    Id = rec.Id,
                    ClientFIO = rec.ClientFIO,
                    Mail = rec.Mail,
                    Login = rec.Login,
                    Password = rec.Password
                })
                .ToList();
            }
        }
    }
}