using System;
using System.Collections;
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
    public partial class frmStaffAdd : Form
    {
        public frmStaffAdd()
        {
            InitializeComponent();
        }

        public int id = 0;

        private void btnSave_Click(object sender, EventArgs e)
        {
            string qry = "";

            if (id == 0) // INSERT
            {
                qry = "INSERT INTO Staff VALUES(@Name, @Phone, @Role)";
            }
            else // UPDATE
            {
                qry = "UPDATE Staff SET sName = @Name, sPhone = @Phone, sRole = @Role WHERE staffID = @id";
            }

            Hashtable ht = new Hashtable();
            ht.Add("@id", id);
            ht.Add("@Name", txtName.Text);
            ht.Add("@Phone", txtPhone.Text);
            ht.Add("@Role", cbxRole.Text);

            if (MainClass.SQL(qry, ht) > 0)
            {
                MessageBox.Show("Staff Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                id = 0;
                txtName.Clear();
                txtPhone.Clear();
                cbxRole.SelectedIndex = -1;
                txtName.Focus();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
