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
    public partial class FormFlawRequest : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

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

        public FormFlawRequest(ISpareLogic logic)
        {
            InitializeComponent();
            List<SpareViewModel> list = logic.Read(null);
            if (list != null)
            {
                comboBoxSpare.DisplayMember = "SpareName";
                comboBoxSpare.ValueMember = "Id";
                comboBoxSpare.DataSource = list;
                comboBoxSpare.SelectedItem = null;
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