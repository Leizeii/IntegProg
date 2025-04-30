using RestaurantManagementApp.Model;
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

namespace RestaurantManagementApp.View
{
    public partial class frmProductView : Form
    {
        public frmProductView()
        {
            InitializeComponent();
        }

        public void GetData()
        {
            string qry = "SELECT pID, pName, pPrice, CategoryID, c.catName FROM Products p INNER JOIN Category c ON c.catID = p.CategoryID WHERE pName LIKE '%" + txtSearch.Text + "%'";
            ListBox lb = new ListBox();
            lb.Items.Add(dgvid);
            lb.Items.Add(dgvName);
            lb.Items.Add(dgvPrice);
            lb.Items.Add(dgvcatID);
            lb.Items.Add(dgvCat);

            MainClass.LoadData(qry, dgvProduct, lb);
        }
        
        private void frmProductView_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //frmProductAdd frm = new frmProductAdd();
            //frm.ShowDialog();
            MainClass.BlurBackground(new frmProductAdd());
            GetData();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvProduct.CurrentCell.OwningColumn.Name == "dgvedit")
            {
                frmProductAdd frm = new frmProductAdd();
                frm.id = Convert.ToInt32(dgvProduct.CurrentRow.Cells["dgvid"].Value);
                frm.cID = Convert.ToInt32(dgvProduct.CurrentRow.Cells["dgvcatID"].Value);

                //frm.ShowDialog();
                MainClass.BlurBackground(frm);
                GetData();
            }
            if (dgvProduct.CurrentCell.OwningColumn.Name == "dgvdel")
            {
                if (MessageBox.Show("Are you sure you want to delete this record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string qry = "DELETE FROM Products WHERE pID = @id";
                    Hashtable ht = new Hashtable();
                    ht.Add("@id", Convert.ToInt32(dgvProduct.CurrentRow.Cells["dgvid"].Value));
                    if (MainClass.SQL(qry, ht) > 0)
                    {
                        MessageBox.Show("Product Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GetData();
                    }
                }
            }
        }
    }
}
