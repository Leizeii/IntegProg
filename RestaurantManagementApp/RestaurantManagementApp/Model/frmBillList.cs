using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using RestaurantManagementApp.Reports;

namespace RestaurantManagementApp.Model
{
    public partial class frmBillList : Form
    {
        public frmBillList()
        {
            InitializeComponent();
        }

        public int MainID = 0;

        private void frmBillList_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            string qry = @"SELECT MainID, TableName, WaiterName, orderType, status, total FROM tblMain
                         WHERE status <> 'Pending'";
            ListBox lb = new ListBox();
            lb.Items.Add(dgvid);
            lb.Items.Add(dgvtbl);
            lb.Items.Add(dgvWaiter);
            lb.Items.Add(dgvType);
            lb.Items.Add(dgvStatus);
            lb.Items.Add(dgvTotal);

            MainClass.LoadData(qry, dgvBillList, lb);
        }

        private void dgvBillList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // For Serial No.

            int count = 0;

            foreach (DataGridViewRow row in dgvBillList.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
        }

        private void dgvBillList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvBillList.CurrentCell.OwningColumn.Name == "dgvedit")
            {
                MainID = Convert.ToInt32(dgvBillList.CurrentRow.Cells["dgvid"].Value);
                this.Close();
            }

            if (dgvBillList.CurrentCell.OwningColumn.Name == "dgvdel") 
            {
                // Prints the bill
                MainID = Convert.ToInt32(dgvBillList.CurrentRow.Cells["dgvid"].Value);
                string qry = @"SELECT * FROM tblMain m INNER JOIN 
                               tblDetails d on d.MainID = m.MainID
                               INNER JOIN Products p on p.pID = d.proID
                               WHERE m.MainID = "+ MainID +"";

                SqlCommand cmd = new SqlCommand(qry, MainClass.conn);
                MainClass.conn.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                MainClass.conn.Close();

                frmPrint frm = new frmPrint();
                rptBill cr = new rptBill();

                cr.SetDataSource(dt);
                frm.crystalReportViewer1.ReportSource = cr;
                frm.crystalReportViewer1.Refresh();
                frm.Show();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
