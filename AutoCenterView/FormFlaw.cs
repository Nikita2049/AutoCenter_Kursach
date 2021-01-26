using AutoCenterDatabaseImplement;
using AutoCenterDatabaseImplement.Implements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoCenterView
{
    public partial class FormFlaw : Form
    {
        public List<Tuple<string, int>> Raz = new List<Tuple<string, int>>();


        public FormFlaw()
        {
            InitializeComponent();

            using (var context = new KorytoDatabase())
            {
                var orders = context.Orders.Where(rec => rec.Status == AutoCenterBusinessLogic.Enums.OrderStatus.Ожидает_поставки_запчастей).ToList();
                if (orders != null)
                {
                    foreach (var order in orders)
                    {
                        var ordercars = context.OrderCars.Where(rec => rec.OrderId == order.Id);
                        foreach (var car in ordercars)
                        {
                            if (car.Count > 0)
                            {
                                var carSpares = context.CarSpares.Where(rec => rec.CarId == car.CarId);
                                foreach (var det in carSpares)
                                {

                                    var detcount = context.Spares.FirstOrDefault(rec => rec.Id == det.SpareId);
                                    if ((det.Count * car.Count) > detcount.TotalAmount)
                                    {
                                        int raznica = (det.Count * car.Count) - detcount.TotalAmount;
                                        Raz.Add(new Tuple<string, int>(detcount.SpareName, raznica));

                                    }
                                }
                            }
                        }
                    }
                }
            }
            dataGridView.Columns.AddRange(
                new DataGridViewTextBoxColumn() { Name = "clmName", HeaderText = "Деталь", DataPropertyName = "Item1" },
                new DataGridViewTextBoxColumn() { Name = "clmAmount", HeaderText = "Требуемое количество", DataPropertyName = "Item2" }
            );
            dataGridView.DataSource = Raz;
            // textBox1.Text = Raz[0].Item1;
        }
    }
}