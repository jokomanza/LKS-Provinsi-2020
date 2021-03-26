using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PC_KAB_KLATEN_JOKO_SUPRIYANTO
{
    public partial class MainForm : Form
    {
        private Employee employee;

        public Employee Employee
        {
            get { return employee; }
            set { 
                employee = value;
                lblWelcome.Text = $"Welcome, {employee.name}";
            }
        }

        public MainForm()
        {
            InitializeComponent();

            FormClosing += (o, e) => Program.GetInstanceOf(typeof(FormLogin)).Show();

            Timer timer = new Timer();
            timer.Tick += (o, e) => lblDate.Text = DateTime.Now.ToString();
            timer.Start();
        }

        private void btnMasterMember_Click(object sender, EventArgs e)
        {
            var manageMember = Program.GetInstanceOf(typeof(FormMasterMember));
            manageMember.Show();
            this.Hide();
        }

        private void btnMasterVehicle_Click(object sender, EventArgs e)
        {
            var manageVehicle = Program.GetInstanceOf(typeof(FormMasterVehicle));
            manageVehicle.Show();
            this.Hide();
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            var form = Program.GetInstanceOf(typeof(FormPayment));
            form.Show();
            this.Hide();
        }
    }
}
