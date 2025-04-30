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
    public partial class frmCategoryAdd : Form
    {
        public frmCategoryAdd()
        {
            InitializeComponent();
        }

        public int id = 0;

        private void btnSave_Click(object sender, EventArgs e)
        {
            string qry = "";

            if(id == 0) // INSERT
            {
                qry = "INSERT INTO Category VALUES(@Name)";
            }
            else // UPDATE
            {
                qry = "UPDATE Category SET catName = @Name WHERE catID = @id";
            }

            Hashtable ht = new Hashtable();
            ht.Add("@id", id);
            ht.Add("@Name", txtName.Text);

            if (MainClass.SQL(qry, ht) > 0)
            {
                MessageBox.Show("Category Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                id = 0;
                txtName.Clear();
                txtName.Focus();
            }
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
