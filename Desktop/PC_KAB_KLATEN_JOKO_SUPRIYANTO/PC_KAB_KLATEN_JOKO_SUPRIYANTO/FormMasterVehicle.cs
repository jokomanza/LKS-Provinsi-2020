using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PC_KAB_KLATEN_JOKO_SUPRIYANTO
{
    public partial class FormMasterVehicle : Form, IFormBasic
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

        public string title => "Master Vehicle";
        int current_id;
        TextBox member_id;

        public FormMasterVehicle()
        {
            InitializeComponent();
            FormClosing += (o, e) => Program.GetInstanceOf(typeof(MainForm)).Show();
            member_id = new TextBox();

            txtFilter.TextChanged += (o, e) =>
            {
                PopulateDate(txtFilter.Text);
            };

            this.context = new MandhegParkingSystemDataContext();

            cbxFilterby.Items.Add("Owner Name");
            cbxFilterby.Items.Add("License Plate");
            cbxFilterby.SelectedIndex = 0;

            cbxVehicleType.DataSource = context.VehicleTypes.ToList();
            cbxVehicleType.DisplayMember = "name";
            cbxVehicleType.ValueMember = "id";


            FormState = FormState.Default;
            txtOwner.TextChanged += (o, e) => member_id.Text = txtOwner.Text;


            dataGridView1.CellClick += (o, e) =>
            {
                member_id.DataBindings.Clear();
                foreach (Control item in Controls)
                {
                    if (item != dataGridView1) item.DataBindings.Clear();
                }
                if(e.RowIndex > 0)
                {
                    current_id = int.Parse( dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                }
                
                FormState = FormState.Select;
                member_id.DataBindings.Add("Text", dataGridView1.DataSource, "member_id");
                txtPlate.DataBindings.Add("Text", dataGridView1.DataSource, "license_plate");
                cbxVehicleType.DataBindings.Add("SelectedValue", dataGridView1.DataSource, "type_id");
                txtOwner.DataBindings.Add("Text", dataGridView1.DataSource, "member_id");
                txtNotes.DataBindings.Add("Text", dataGridView1.DataSource, "notes");

                FormState = FormState.Select;
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
                if (item.GetType() == typeof(TextBox) && item != txtFilter) item.Enabled = false;
                else if (item.GetType() == typeof(DateTimePicker)) ((DateTimePicker)item).Enabled = false;
                else if (item.GetType() == typeof(ComboBox) && item != cbxFilterby) ((ComboBox)item).Enabled = false;
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

        private void PopulateDate(string filter = "")
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                dataGridView1.DataSource = context.Vehicles.Select(m =>  new { m.id, type_id = m.vehicle_type_id, m.member_id, m.license_plate, m.notes }). ToList();
            }
            else
            {
                dataGridView1.DataSource = context.Vehicles.Where(x =>  SqlMethods.Like(x.license_plate, $"%{filter}%")).Select(m => new { m.id, type_id = m.vehicle_type_id, m.member_id, m.license_plate, m.notes }).ToList();
            }
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
            if (FormState == FormState.Select)
            {
                try
                {
                    if (MessageBox.Show("Are you sure want to delete this data?", $"Mandheg Parking System - {title}", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        var taret = context.Vehicles.Where(x => x.id == current_id).FirstOrDefault();
                        ((MandhegParkingSystemDataContext)context).Vehicles.DeleteOnSubmit(taret);
                        ((MandhegParkingSystemDataContext)context).SubmitChanges();

                        MessageBox.Show("Success delete data.", $"Mandheg Parking System - {title}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FormState = FormState.Default;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtPlate.Text) || 
                string.IsNullOrWhiteSpace(txtOwner.Text) ||
                string.IsNullOrWhiteSpace(txtNotes.Text))
            {
                MessageBox.Show("Please fill all form first!", $"Mandheg Parking System - {title}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (FormState.Equals(FormState.Insert))
            {
                Vehicle member = new Vehicle();
                member.vehicle_type_id = int.Parse( cbxVehicleType.SelectedValue.ToString());
                member.member_id = int.Parse( member_id.Text);
                member.license_plate = txtPlate.Text;
                member.notes = txtNotes.Text;

                context.Vehicles.InsertOnSubmit(member);
                ((MandhegParkingSystemDataContext)context).SubmitChanges();

                MessageBox.Show("Success insert data.", $"Mandheg Parking System - {title}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FormState = FormState.Default;
            }
            else if (FormState.Equals(FormState.Update))
            {
                Vehicle member = context.Vehicles.Where(x => x.id == current_id).FirstOrDefault();
                member.vehicle_type_id = int.Parse(cbxVehicleType.SelectedValue.ToString());
                member.member_id = int.Parse(member_id.Text);
                member.license_plate = txtPlate.Text;
                member.notes = txtNotes.Text;

                ((MandhegParkingSystemDataContext)context).SubmitChanges();

                MessageBox.Show("Success update data.", $"Mandheg Parking System - {title}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FormState = FormState.Default;
            }
        }
    }
}
