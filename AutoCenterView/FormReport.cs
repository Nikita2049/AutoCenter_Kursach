using AutoCenterBusinessLogic.BindingModel;
using AutoCenterBusinessLogic.BusinessLogic;
using AutoCenterBusinessLogic.ViewModel;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace AutoCenterView
{
    public partial class FormReport : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly ReportLogic logic;
        public FormReport(ReportLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void FormReport_Load(object sender, EventArgs e)
        {

            this.reportViewer.RefreshReport();
        }

        public void LoadData()
        {
            if (dateTimePickerFrom.Value.Date >= dateTimePickerTo.Value.Date)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                ReportParameter parameter = new ReportParameter("ReportParameterPeriod", "c " + dateTimePickerFrom.Value.ToShortDateString() + " по " + dateTimePickerTo.Value.ToShortDateString());
                reportViewer.LocalReport.SetParameters(parameter);

                var requests = logic.GetRequests(new ReportBindingModel { DateFrom = dateTimePickerFrom.Value.Date, DateTo = dateTimePickerTo.Value.Date });
                var cars = logic.GetCars();
                List<ReportCarsViewModel> dataSourceCars = new List<ReportCarsViewModel>();
                foreach (var car in cars)
                {
                    dataSourceCars.Add(new ReportCarsViewModel { CarName = car.CarName, FullPrice = car.FullPrice, Price = car.Price, Year = car.Year });
                }
                List<ReportRequestsViewModel> dataSourceRequests = new List<ReportRequestsViewModel>();
                foreach (var reqs in requests)
                {
                    foreach (var req in reqs)
                    {
                        dataSourceRequests.Add(new ReportRequestsViewModel { RequestName = req.RequestName, DateCreate = req.DateCreate });
                    }
                }
                ReportDataSource sourceCars = new ReportDataSource("DataSetCars", dataSourceCars);
                ReportDataSource sourceRequests = new ReportDataSource("DataSetRequests", dataSourceRequests);
                reportViewer.LocalReport.DataSources.Add(sourceCars);
                reportViewer.LocalReport.DataSources.Add(sourceRequests);
                reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
            this.reportViewer.RefreshReport();
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog { Filter = "pdf|*.pdf" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        logic.SaveCarsRequestsToPdfFile(new ReportBindingModel
                        {
                            FileName = dialog.FileName,
                            DateFrom = dateTimePickerFrom.Value,
                            DateTo = dateTimePickerTo.Value
                        }, "mr.alodov@mail.ru");

                        MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}