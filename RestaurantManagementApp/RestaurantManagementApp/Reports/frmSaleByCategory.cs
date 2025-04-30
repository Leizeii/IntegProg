using RestaurantManagementApp.Model;
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

namespace RestaurantManagementApp.Reports
{
    public partial class frmSaleByCategory : Form
    {
        public frmSaleByCategory()
        {
            InitializeComponent();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            string qry = @"SELECT * FROM tblMain m
                           INNER JOIN tblDetails d ON m.MainID = d.MainID
                           INNER JOIN Products p ON p.pID = d.proID
                           INNER JOIN Category c ON c.catID = p.CategoryID
                           WHERE m.aDate BETWEEN @sdate AND @edate";

            SqlCommand cmd = new SqlCommand(qry, MainClass.conn);
            cmd.Parameters.AddWithValue("@sdate", Convert.ToDateTime(dtpStart.Value).Date);
            cmd.Parameters.AddWithValue("@edate", Convert.ToDateTime(dtpEnd.Value).Date);
            MainClass.conn.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            MainClass.conn.Close();

            frmPrint frm = new frmPrint();
            rptSaleByCategory cr = new rptSaleByCategory();

            cr.SetDataSource(dt);
            frm.crystalReportViewer1.ReportSource = cr;
            frm.crystalReportViewer1.Refresh();
            frm.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
