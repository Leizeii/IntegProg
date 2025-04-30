using RestaurantManagementApp.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantManagementApp.Model
{
    public partial class frmReports : Form
    {
        public frmReports()
        {
            InitializeComponent();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            string qry = @"SELECT * FROM Products";

            SqlCommand cmd = new SqlCommand(qry, MainClass.conn);
            MainClass.conn.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            MainClass.conn.Close();

            frmPrint frm = new frmPrint();
            rptMenu cr = new rptMenu();

            cr.SetDataSource(dt);
            frm.crystalReportViewer1.ReportSource = cr;
            frm.crystalReportViewer1.Refresh();
            frm.Show();
        }
        
        private void btnStaff_Click(object sender, EventArgs e)
        {
            string qry = @"SELECT * FROM Staff";

            SqlCommand cmd = new SqlCommand(qry, MainClass.conn);
            MainClass.conn.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            MainClass.conn.Close();

            frmPrint frm = new frmPrint();
            rptStaffList cr = new rptStaffList();

            cr.SetDataSource(dt);
            frm.crystalReportViewer1.ReportSource = cr;
            frm.crystalReportViewer1.Refresh();
            frm.Show();
        }

        private void btnSalesCat_Click(object sender, EventArgs e)
        {
            frmSaleByCategory frm = new frmSaleByCategory();
            frm.ShowDialog();
        }
    }
}
