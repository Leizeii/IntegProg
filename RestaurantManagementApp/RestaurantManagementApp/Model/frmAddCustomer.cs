using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantManagementApp.Model
{
    public partial class frmAddCustomer : Form
    {
        public frmAddCustomer()
        {
            InitializeComponent();
        }

        public string orderType = "";
        public int driverID = 0;
        public string cusName = "";
        public int mainID = 0;

        private void frmAddCustomer_Load(object sender, EventArgs e)
        {
            if (orderType == "Take Out")
            {
                lblDriver.Visible = false;
                cbxDriver.Visible = false;
            }

            string qry = "SELECT staffID 'id', sName 'name' FROM Staff WHERE sRole LIKE 'Driver'";
            MainClass.CBFill(qry, cbxDriver);

            if (mainID > 0)
            {
                cbxDriver.SelectedValue = driverID;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbxDriver_SelectedIndexChanged(object sender, EventArgs e)
        {
            driverID = Convert.ToInt32(cbxDriver.SelectedValue);
        }
    }
}
