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
    public partial class FormPayment : Form
    {

        IMandhegParkingSystemDataContext context;

        public FormPayment()
        {
            context = Program.GetContext();

            InitializeComponent();

            FormClosing += (o, e) => Program.GetInstanceOf(typeof(MainForm)).Show();
            dtpInTime.Format = DateTimePickerFormat.Time;
            dtpOutTime.Format = DateTimePickerFormat.Time;
            cbxMemberType.DataSource = context.Memberships.ToList();
            cbxMemberType.DisplayMember = "name";
            cbxMemberType.ValueMember = "id";
            cbxMemberType.SelectedIndex = 0;

            cbxVehicleType.DataSource = context.VehicleTypes.ToList();
            cbxVehicleType.DisplayMember = "name";
            cbxVehicleType.ValueMember = "id";
            cbxVehicleType.SelectedIndex = 0;


            var P = context.Vehicles.Where(x => x.license_plate == txtPlate.Text).FirstOrDefault();
            if (P is null)
            {
                tbxOwner.Text = "";
                tbxOwner.Enabled = true;
                cbxMemberType.Enabled = true;
                cbxMemberType.SelectedItem = "No Member";
                cbxVehicleType.Enabled = true;
                cbxVehicleType.SelectedIndex = 0;
            }
            else
            {
                var owner = context.Members.Where(x => x.id == context.Vehicles.Where(y => y.license_plate == P.license_plate).FirstOrDefault().id).FirstOrDefault();
                tbxOwner.Text = owner.name;
                tbxOwner.Enabled = false;
                cbxMemberType.Enabled = false;
                cbxMemberType.SelectedValue = owner.membership_id;
                cbxVehicleType.Enabled = false;
                cbxVehicleType.SelectedValue = P.vehicle_type_id;
            }



            txtPlate.TextChanged += (o, e) =>
            {
                var plate = context.Vehicles.Where(x => x.license_plate == txtPlate.Text).FirstOrDefault();
                if(plate is null)
                {
                    tbxOwner.Text = "";
                    tbxOwner.Enabled = true;
                    cbxMemberType.Enabled = true;
                    cbxMemberType.SelectedItem = "No Member";
                    cbxVehicleType.Enabled = true;
                    cbxVehicleType.SelectedIndex = 0;
                }
                else
                {
                    var owner = context.Members.Where(x => x.id == context.Vehicles.Where(y => y.license_plate == plate.license_plate).FirstOrDefault().id).FirstOrDefault();
                    tbxOwner.Text = owner.name;
                    tbxOwner.Enabled = false;
                    cbxMemberType.Enabled = false;
                    cbxMemberType.SelectedValue = owner.membership_id;
                    cbxVehicleType.Enabled = false;
                    cbxVehicleType.SelectedValue = plate.vehicle_type_id;
                }
            };

            txtPlate.Text = "";


        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            
            ParkingData payment = new ParkingData();
            payment.license_plate = txtPlate.Text;
            payment.vehicle_id = int.Parse(cbxVehicleType.SelectedValue.ToString());
            payment.employee_id = ((MainForm)Program.GetInstanceOf(typeof(MainForm))).Employee.id;

        }
    }
}
