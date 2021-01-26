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
    public partial class FormCarSpare : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ISpareLogic logicP;

        public int Id
        {
            get { return Convert.ToInt32(comboBoxSpare.SelectedValue); }
            set { comboBoxSpare.SelectedValue = value; }
        }

        public string SpareName { get { return comboBoxSpare.Text; } }

        public int Count
        {
            get { return Convert.ToInt32(textBoxCount.Text); }
            set { textBoxCount.Text = value.ToString(); }
        }

        public int PlusSum;

        public FormCarSpare(ISpareLogic logicP)
        {
            InitializeComponent();
            this.logicP = logicP;
        }

        private void FormCarSpare_Load(object sender, EventArgs e)
        {
            try
            {
                var list = logicP.Read(null);
                if (list != null)
                {
                    comboBoxSpare.DisplayMember = "SpareName";
                    comboBoxSpare.ValueMember = "Id";
                    comboBoxSpare.DataSource = list;
                    comboBoxSpare.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TextBoxCount_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void ComboBoxSpare_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void CalcSum()
        {
            if (comboBoxSpare.SelectedValue != null && !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(comboBoxSpare.SelectedValue);
                    SpareViewModel Spare = logicP.Read(new SpareBindingModel { Id = id })?[0];
                    int count = Convert.ToInt32(textBoxCount.Text);
                    int sum = count * Spare?.Price ?? 0;
                    textBoxPlusSum.Text = sum.ToString();
                    PlusSum = sum;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxSpare.SelectedValue == null)
            {
                MessageBox.Show("Выберите деталь", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}