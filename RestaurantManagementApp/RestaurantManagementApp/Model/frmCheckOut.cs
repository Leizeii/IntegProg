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
    public partial class frmCheckOut : Form
    {
        public frmCheckOut()
        {
            InitializeComponent();
        }

        public double amt;
        public int MainID = 0;

        private void txtReceived_TextChanged(object sender, EventArgs e)
        {
            double amt = 0;
            double receipt = 0;
            double change = 0;

            double.TryParse(txtBillAmount.Text, out amt);
            double.TryParse(txtReceived.Text, out receipt);
            if (receipt > 0)
            {
                change = receipt - amt;
                txtChange.Text = change.ToString();
            }
            else
            {
                txtChange.Text = "0.00";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string qry = @"UPDATE tblMain SET status = 'Paid', total = @total, received = @rec, change = @change
                           WHERE MainID = @id";

            Hashtable ht = new Hashtable();
            ht.Add("@id", MainID);
            ht.Add("@total", txtBillAmount.Text);
            ht.Add("@rec", txtReceived.Text);
            ht.Add("@change", txtChange.Text);
            

            if (MainClass.SQL(qry, ht) > 0)
            {
                MessageBox.Show("Payment Completed", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Payment Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmCheckOut_Load(object sender, EventArgs e)
        {
            txtBillAmount.Text = amt.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
