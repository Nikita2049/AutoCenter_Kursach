using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoCenterDatabaseImplement.Models;
using AutoCenterBusinessLogic.Interfaces;
using Unity;

namespace AutoCenterView
{
    public partial class FormStatistic : Form
    {
        [Dependency] public new IUnityContainer Container { get; set; }
        private readonly ICarLogic logic;

        public FormStatistic(ICarLogic logic)
        {
            this.logic = logic;
            InitializeComponent();
        }

        private void FormStatistic_Load(object sender, EventArgs e)
        {
            var list = logic.Read(null);

            foreach (var car in list)
            {
                int countSpare = 0;
                var Spares = car.CarSpares.Values;
                foreach (var Spare in Spares)
                {
                    countSpare += Spare.Item3;
                }

                this.chart1.Series["Цена деталей"].Points.AddXY(car.CarName, countSpare);
            }
        }
    }
}