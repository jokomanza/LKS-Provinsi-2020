using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PC_KAB_KLATEN_JOKO_SUPRIYANTO
{
    public partial class FormLogin : Form, IFormBasic
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        public string title { get => "Form Login"; }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please fill all form first!", $"Mandheg Parking System - {title}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var employee = Program.GetContext().Employees.Where(x => x.email == txtID.Text && x.password == getHash(txtPassword.Text)).FirstOrDefault();
            if (employee is null)
            {
                MessageBox.Show("User id and password wrong!", $"Mandheg Parking System - {title}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MainForm main = new MainForm();
             main.Employee = employee;
            main.Show();

            this.Hide();
            txtID.Text = "";
            txtPassword.Text = "";
        }

        public string getHash(string target)
        {
            StringBuilder result = new StringBuilder();
            using(SHA256 hash = SHA256Managed.Create())
            {
                Encoding encoding = Encoding.UTF8;
                Byte[] has =  hash.ComputeHash(encoding.GetBytes(target));

                foreach (var item in has)
                {
                    result.Append(item.ToString("x2"));
                }
            }
            return result.ToString();
        }
    }
}
