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
    public partial class frmCategoryView : Form
    {
        public frmCategoryView()
        {
            InitializeComponent();
        }

        public void GetData()
        {
            string qry = "SELECT * FROM Category WHERE catName LIKE '%"+ txtSearch.Text +"%'";
            ListBox lb = new ListBox();
            lb.Items.Add(dgvid);
            lb.Items.Add(dgvName);

            MainClass.LoadData(qry, dgvCategory, lb);
        }

        private void frmCategoryView_Load(object sender, EventArgs e)
        {
            GetData();
        }
        
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //frmCategoryAdd frm = new frmCategoryAdd();
            //frm.ShowDialog();
            MainClass.BlurBackground(new frmCategoryAdd());
            GetData();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void dgvCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCategory.CurrentCell.OwningColumn.Name == "dgvedit")
            {
                frmCategoryAdd frm = new frmCategoryAdd();
                frm.id = Convert.ToInt32(dgvCategory.CurrentRow.Cells["dgvid"].Value);
                frm.txtName.Text = Convert.ToString(dgvCategory.CurrentRow.Cells["dgvName"].Value);
                //frm.ShowDialog();
                MainClass.BlurBackground(frm);
                GetData();
            } 
            if (dgvCategory.CurrentCell.OwningColumn.Name == "dgvdel")
            {
                if (MessageBox.Show("Are you sure you want to delete this record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string qry = "DELETE FROM Category WHERE catID = @id";
                    Hashtable ht = new Hashtable();
                    ht.Add("@id", Convert.ToInt32(dgvCategory.CurrentRow.Cells["dgvid"].Value));
                    if (MainClass.SQL(qry, ht) > 0)
                    {
                        MessageBox.Show("Category Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GetData();
                    }
                }
            }
        }
    }
}
