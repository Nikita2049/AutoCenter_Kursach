using AutoCenterBusinessLogic.BindingModel;
using AutoCenterBusinessLogic.HelperModels;
using AutoCenterBusinessLogic.Interfaces;
using AutoCenterBusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace AutoCenterBusinessLogic.BusinessLogic
{
    public class ReportLogic
    {
        private readonly IOrderLogic orderLogic;
        private readonly IRequestLogic requestLogic;
        private readonly ICarLogic carLogic;
        private readonly ISpareLogic spareLogic;

        public ReportLogic(IRequestLogic requestLogic, ICarLogic carLogic, IOrderLogic orderLogic, ISpareLogic spareLogic)
        {
            this.requestLogic = requestLogic;
            this.carLogic = carLogic;
            this.orderLogic = orderLogic;
            this.spareLogic = spareLogic;
        }

        public List<ReportSpareRequestViewModel> GetSpareRequest()
        {
            var requests = requestLogic.Read(null);
            var list = new List<ReportSpareRequestViewModel>();

            foreach (var request in requests)
            {
                foreach (var rec in request.SpareRequests)
                {
                    var record = new ReportSpareRequestViewModel
                    {
                        RequestName = request.RequestName,
                        DateCreate = request.DateCreate,
                        SpareName = rec.Value.Item1,
                        Count = rec.Value.Item2
                    };
                    list.Add(record);
                }
            }
            return list;
        }

        public List<OrderViewModel> GetClientOrders(int id)
        {
            var orders = orderLogic.Read(null);
            var list = new List<OrderViewModel>();
            foreach (var order in orders)
            {
                if (order.ClientId == id)
                {
                    var record = new OrderViewModel
                    {
                        Id = order.Id,
                        ClientFIO = order.ClientFIO,
                        DateCreate = order.DateCreate,
                        Price = order.Price,
                        Status = order.Status,
                        OrderCars = order.OrderCars
                    };

                    list.Add(record);
                }
            }
            return list;
        }

        public List<CarViewModel> GetOrderCars(OrderViewModel order)
        {
            var cars = new List<CarViewModel>();

            foreach (var car in order.OrderCars)
            {
                cars.Add(carLogic.Read(new CarBindingModel
                {
                    Id = car.CarId
                }).FirstOrDefault());
            }
            return cars;
        }

        public List<CarViewModel> GetCars()
        {
            return carLogic.Read(null);
        }

        public List<IGrouping<DateTime, RequestViewModel>> GetRequests(ReportBindingModel model)
        {
            var cl = requestLogic.Read(new RequestBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo,

            })
            .GroupBy(rec => rec.DateCreate.Date)
            .OrderBy(recG => recG.Key)
            .ToList();
            return cl;
        }

        //ворд админ
        public void SaveSpareRequestsToWordlFile(string fileName, string email)
        {
            string title = "Заявки";
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = fileName,
                Title = title,
                SpareRequests = GetSpareRequest()
            });
            SendMail(email, fileName, title);
        }

        //ексель админ
        public void SaveSpareRequestsToExcellFile(string fileName, string email)
        {
            string title = "Заявки";
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = fileName,
                Title = title,
                SpareRequests = GetSpareRequest()
            });
            SendMail(email, fileName, title);
        }

        //ворд клиент
        public void SaveOrderToWordFile(string fileName, OrderViewModel order, string email)
        {
            string title = "Список автомобилей по заказу №" + order.Id;
            SaveToWord.CreateDoc(new WordInfoAvto
            {
                FileName = fileName,
                Title = title,
                Cars = GetOrderCars(order)
            });
            SendMail(email, fileName, title);
        }

        //ексель клиент
        public void SaveOrderToExcelFile(string fileName, OrderViewModel order, string email)
        {
            string title = "Список автомобилей по заказу №" + order.Id;
            SaveToExcel.CreateDoc(new ExcelInfoAvto
            {
                FileName = fileName,
                Title = title,
                Cars = GetOrderCars(order)
            });
            SendMail(email, fileName, title);
        }

        //пдф админ
        public void SaveCarsRequestsToPdfFile(ReportBindingModel req, string email)
        {
            string title = "Машины и Заявки в период c " + req.DateFrom.ToString() + " по " + req.DateTo.ToString();
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = req.FileName,
                Title = title,
                Cars = carLogic.Read(null).ToList(),
                Requests = GetRequests(req)
            });
            SendMail(email, req.FileName, title);
        }

        //пдф клиент
        public void SaveCarsSparesToPdfFile(string fileName, int id, string email)
        {
            string title = "Список заказов";
            SaveToPdf.CreateDoc(new PdfInfoClient
            {
                FileName = fileName,
                Title = title,
                Orders = GetClientOrders(id),
                Cars = GetCars()
            });
            SendMail(email, fileName, title);
        }

        public void SendMail(string email, string fileName, string subject)
        {
            MailAddress from = new MailAddress("labwork15kafis@gmail.com", "Автосалон Корыто");
            MailAddress to = new MailAddress(email);
            MailMessage m = new MailMessage(from, to)
            {
                Subject = subject
            };
            m.Attachments.Add(new Attachment(fileName));
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("labwork15kafis@gmail.com", "passlab15"),
                EnableSsl = true
            };
            smtp.Send(m);
        }
    }
}