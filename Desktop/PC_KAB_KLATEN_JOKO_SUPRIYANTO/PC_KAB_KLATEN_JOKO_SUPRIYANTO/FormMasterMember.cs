using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PC_KAB_KLATEN_JOKO_SUPRIYANTO
{
    public partial class FormMasterMember : Form, IFormBasic
    {
        private FormState formState;
        IMandhegParkingSystemDataContext context;

        public FormState FormState
        {
            get { return formState; }
            set { 
                formState = value;
                switch (value)
                {
                    case FormState.Default:
                        btnInsert.Enabled = true;
                        btnUpdate.Enabled = false;
                        btnDelete.Enabled = false;
                        btnSubmit.Enabled = false;
                        btnCancel.Enabled = false;

                        PopulateDate();
                        ClearForm();
                        DisableForm();
                        break;
                    case FormState.Select:
                        btnInsert.Enabled = false;
                        btnUpdate.Enabled = true;
                        btnDelete.Enabled = true;
                        btnSubmit.Enabled = true;
                        btnCancel.Enabled = true;

                        DisableForm();
                        break;
                    case FormState.Insert:
                        btnInsert.Enabled = false;
                        btnUpdate.Enabled = false;
                        btnDelete.Enabled = false;
                        btnSubmit.Enabled = true;
                        btnCancel.Enabled = true;

                        ClearForm();
                        EnableForm();
                        break;
                    case FormState.Update:
                        btnInsert.Enabled = false;
                        btnUpdate.Enabled = false;
                        btnDelete.Enabled = false;
                        btnSubmit.Enabled = true;
                        btnCancel.Enabled = true;

                        EnableForm();
                        break;
                    default:
                        break;
                }
            }
        }

        public string title => "Master Member";

        int current_id = 0;

        public FormMasterMember()
        {
            InitializeComponent();
            //MessageBox.Show(DateTime.Now.ToString("yyyy/MM/dd"));

            FormClosing += (o, e) => Program.GetInstanceOf(typeof(MainForm)).Show();
            //current_id = new TextBox();

            this.context = new MandhegParkingSystemDataContext();

            cbxGender.Items.Add("Male");
            cbxGender.Items.Add("Female");
            cbxGender.SelectedIndex = 0;

            cbxMemberType.DataSource = context.Memberships.Where(x => x.name != "Non Member").Select(x => x.name).ToList();


            FormState = FormState.Default;


            dataGridView1.CellClick += (o, e) =>
            {
                foreach (Control item in Controls)
                {
                    if (item != dataGridView1) item.DataBindings.Clear();
                }

                if (e.RowIndex > 0)
                {
                    current_id = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                }

                FormState = FormState.Select;
                txtName.DataBindings.Add("Text", dataGridView1.DataSource, "name");
                cbxMemberType.DataBindings.Add("Text", dataGridView1.DataSource, "Membership_Name");
                txtEmail.DataBindings.Add("Text", dataGridView1.DataSource, "email");
                txtPhone.DataBindings.Add("Text", dataGridView1.DataSource, "phone_number");
                txtAddresss.DataBindings.Add("Text", dataGridView1.DataSource, "address");
                dtpDateOfBirth.DataBindings.Add("Text", dataGridView1.DataSource, "date_of_birth");
                cbxGender.DataBindings.Add("Text", dataGridView1.DataSource, "gender");
            };
        }

        private void ClearForm()
        {
            foreach (Control item in Controls)
            {
                if (item.GetType() == typeof(TextBox)) item.Text = "";
                else if (item.GetType() == typeof(DateTimePicker)) ((DateTimePicker)item).Value = DateTime.Now;
                else if (item.GetType() == typeof(ComboBox)) ((ComboBox)item).SelectedIndex = 0;
            }
        }

        private void DisableForm()
        {
            foreach (Control item in Controls)
            {
                if (item.GetType() == typeof(TextBox)) item.Enabled = false;
                else if (item.GetType() == typeof(DateTimePicker)) ((DateTimePicker)item).Enabled = false;
                else if (item.GetType() == typeof(ComboBox)) ((ComboBox)item).Enabled = false;
            }
        }

        private void EnableForm()
        {
            foreach (Control item in Controls)
            {
                if (item.GetType() == typeof(TextBox)) item.Enabled = true;
                else if (item.GetType() == typeof(DateTimePicker)) ((DateTimePicker)item).Enabled = true;
                else if (item.GetType() == typeof(ComboBox)) ((ComboBox)item).Enabled = true;
            }
        }

        private void PopulateDate()
        {
            dataGridView1.DataSource = context.Members.Join(context.Memberships, m => m.membership_id, t => t.id, (m, t) => new { m.id, m.name, m.email, Membership_Name =  t.name, m.phone_number, m.address, m.date_of_birth, m.gender }). ToList();
        }

        

        private void btnInsert_Click(object sender, EventArgs e)
        {
            FormState = FormState.Insert;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            FormState = FormState.Update;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FormState = FormState.Default;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(FormState == FormState.Select)
            {
                if(MessageBox.Show("Are you sure want to delete this data?", $"Mandheg Parking System - {title}", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var member = context.Members.Where(x => x.id == current_id).FirstOrDefault();
                    var vehicle = context.Vehicles.Where(x => x.member_id == member.id).FirstOrDefault();
                    var parkingdata = context.ParkingDatas.Where(x => x.vehicle_id == vehicle.id).FirstOrDefault();

                    ((MandhegParkingSystemDataContext)context).Members.DeleteOnSubmit(member);
                    ((MandhegParkingSystemDataContext)context).Vehicles.DeleteOnSubmit(vehicle);
                    if(parkingdata != null) ((MandhegParkingSystemDataContext)context).ParkingDatas.DeleteOnSubmit(parkingdata);
                    ((MandhegParkingSystemDataContext)context).SubmitChanges();

                    MessageBox.Show("Success delete data.", $"Mandheg Parking System - {title}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FormState = FormState.Default;
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtName.Text) || 
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text) ||
                string.IsNullOrWhiteSpace(txtAddresss.Text))
            {
                MessageBox.Show("Please fill all form first!", $"Mandheg Parking System - {title}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MailAddress mail;
            try
            {
                mail = new MailAddress(txtEmail.Text); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, $"Mandheg Parking System - {title}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (FormState.Equals(FormState.Insert))
                {
                    Member member = new Member();
                    member.name = txtName.Text;
                    member.membership_id = cbxMemberType.SelectedIndex + 1;
                    member.email = txtEmail.Text;
                    member.phone_number = txtPhone.Text;
                    member.address = txtAddresss.Text;
                    member.date_of_birth = DateTime.Parse( DateTime.Parse(dtpDateOfBirth.Value.ToString()).ToString("yyyy/MM/dd"));
                    member.gender = cbxGender.SelectedItem.ToString();

                    context.Members.InsertOnSubmit(member);
                    ((MandhegParkingSystemDataContext)context).SubmitChanges();

                    MessageBox.Show("Success insert data.", $"Mandheg Parking System - {title}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FormState = FormState.Default;
                }
                else if (FormState.Equals(FormState.Update))
                {
                    Member member = context.Members.Where(x => x.id == current_id).FirstOrDefault();
                    member.name = txtName.Text;
                    member.membership_id = cbxMemberType.SelectedIndex + 1;
                    member.email = txtEmail.Text;
                    member.phone_number = txtPhone.Text;
                    member.address = txtAddresss.Text;
                    member.date_of_birth = dtpDateOfBirth.Value;
                    member.gender = cbxGender.SelectedValue.ToString();

                    ((MandhegParkingSystemDataContext)context).SubmitChanges();

                    MessageBox.Show("Success update data.", $"Mandheg Parking System - {title}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FormState = FormState.Default;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, $"Mandheg Parking System - {title}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FormState = FormState.Default;
            }

        }
    }
}
