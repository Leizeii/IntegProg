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
    public partial class frmTableView : Form
    {
        public frmTableView()
        {
            InitializeComponent();
        }

        public void GetData()
        {
            string qry = "SELECT * FROM Tables WHERE tName LIKE '%" + txtSearch.Text + "%'";
            ListBox lb = new ListBox();
            lb.Items.Add(dgvid);
            lb.Items.Add(dgvName);

            MainClass.LoadData(qry, dgvTable, lb);
        }

        private void frmTableView_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            //frmTableAdd frm = new frmTableAdd();
            //frm.ShowDialog();
            MainClass.BlurBackground(new frmTableAdd());
            GetData();
        }

        private void txtSearch_TextChanged_1(object sender, EventArgs e)
        {
            GetData();
        }

        private void dgvTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvTable.CurrentCell.OwningColumn.Name == "dgvedit")
            {
                frmCategoryAdd frm = new frmCategoryAdd();
                frm.id = Convert.ToInt32(dgvTable.CurrentRow.Cells["dgvid"].Value);
                frm.txtName.Text = Convert.ToString(dgvTable.CurrentRow.Cells["dgvName"].Value);
                //frm.ShowDialog();
                MainClass.BlurBackground(frm);
                GetData();
            }
            if (dgvTable.CurrentCell.OwningColumn.Name == "dgvdel")
            {
                if (MessageBox.Show("Are you sure you want to delete this record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string qry = "DELETE FROM Tables WHERE tID = @id";
                    Hashtable ht = new Hashtable();
                    ht.Add("@id", Convert.ToInt32(dgvTable.CurrentRow.Cells["dgvid"].Value));
                    if (MainClass.SQL(qry, ht) > 0)
                    {
                        MessageBox.Show("Table Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GetData();
                    }
                }
            }
        }
    }
}
