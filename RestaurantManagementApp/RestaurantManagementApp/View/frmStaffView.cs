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
    public partial class frmStaffView : Form
    {
        public frmStaffView()
        {
            InitializeComponent();
        }

        public void GetData()
        {
            string qry = "SELECT * FROM Staff WHERE sName LIKE '%" + txtSearch.Text + "%'";
            ListBox lb = new ListBox();
            lb.Items.Add(dgvid);
            lb.Items.Add(dgvName);
            lb.Items.Add(dgvPhone);
            lb.Items.Add(dgvRole);

            MainClass.LoadData(qry, dgvStaff, lb);
        }

        private void frmStaffView_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //frmStaffAdd frm = new frmStaffAdd();
            //frm.ShowDialog();
            MainClass.BlurBackground(new frmStaffAdd());
            GetData();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void dgvStaff_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvStaff.CurrentCell.OwningColumn.Name == "dgvedit")
            {
                frmStaffAdd frm = new frmStaffAdd();
                frm.id = Convert.ToInt32(dgvStaff.CurrentRow.Cells["dgvid"].Value);
                frm.txtName.Text = Convert.ToString(dgvStaff.CurrentRow.Cells["dgvName"].Value);
                frm.txtPhone.Text = Convert.ToString(dgvStaff.CurrentRow.Cells["dgvPhone"].Value);
                frm.cbxRole.Text = Convert.ToString(dgvStaff.CurrentRow.Cells["dgvRole"].Value);
                //frm.ShowDialog();
                MainClass.BlurBackground(frm);
                GetData();
            }
            if (dgvStaff.CurrentCell.OwningColumn.Name == "dgvdel")
            {
                if (MessageBox.Show("Are you sure you want to delete this record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string qry = "DELETE FROM Staff WHERE staffID = @id";
                    Hashtable ht = new Hashtable();
                    ht.Add("@id", Convert.ToInt32(dgvStaff.CurrentRow.Cells["dgvid"].Value));
                    if (MainClass.SQL(qry, ht) > 0)
                    {
                        MessageBox.Show("Staff Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GetData();
                    }
                }
            }
        }
    }
}
