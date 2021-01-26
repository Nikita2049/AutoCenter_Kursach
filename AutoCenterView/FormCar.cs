using AutoCenterBusinessLogic.BindingModel;
using AutoCenterBusinessLogic.Interfaces;
using AutoCenterBusinessLogic.ViewModel;
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
    public partial class FormCar : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }

        private readonly ICarLogic logic;

        private int? id;

        private Dictionary<int, (string, int, int)> carSpares;

        public FormCar(ICarLogic logic)
        {
            InitializeComponent();
            dataGridView.Columns.Add("Id", "Id");
            dataGridView.Columns.Add("DettailName", "Деталь");
            dataGridView.Columns.Add("Count", "Количество");
            dataGridView.Columns.Add("Price", "Цена");
            dataGridView.Columns[0].Visible = false;
            dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.logic = logic;
        }

        private void FormCar_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    CarViewModel view = logic.Read(new CarBindingModel { Id = id.Value })?[0];
                    if (view != null)
                    {
                        textBoxName.Text = view.CarName;
                        textBoxPriceCar.Text = view.Price.ToString();
                        textBoxPrice.Text = view.FullPrice.ToString();
                        textBoxYear.Text = view.Year.ToString();
                        carSpares = view.CarSpares;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                carSpares = new Dictionary<int, (string, int, int)>();
            }
        }

        private void LoadData()
        {
            int Sum = 0;
            try
            {
                if (carSpares != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var pc in carSpares)
                    {
                        dataGridView.Rows.Add(new object[] { pc.Key, pc.Value.Item1, pc.Value.Item2, pc.Value.Item3 });
                        Sum += pc.Value.Item3;
                    }
                    Sum += Convert.ToInt32(textBoxPriceCar.Text);
                    textBoxPrice.Text = Sum.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormCarSpare>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (carSpares.ContainsKey(form.Id))
                {
                    carSpares[form.Id] = (form.SpareName, form.Count, form.PlusSum);
                }
                else
                {
                    carSpares.Add(form.Id, (form.SpareName, form.Count, form.PlusSum));
                }
                LoadData();
            }
        }

        private void ButtonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormCarSpare>();
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                form.Id = id;
                form.Count = carSpares[id].Item2;
                form.PlusSum = carSpares[id].Item3;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    carSpares[form.Id] = (form.SpareName, form.Count, form.PlusSum);
                    LoadData();
                }
            }
        }

        private void ButtonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        carSpares.Remove(Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void ButtonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                logic.CreateOrUpdate(new CarBindingModel
                {
                    Id = id,
                    CarName = textBoxName.Text,
                    Price = Convert.ToInt32(textBoxPriceCar.Text),
                    FullPrice = Convert.ToInt32(textBoxPrice.Text),
                    Year = Convert.ToInt32(textBoxYear.Text),
                    CarSpares = carSpares
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void textBoxPriceCar_TextChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}