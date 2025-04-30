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
    public partial class frmWaiterSelect : Form
    {
        public frmWaiterSelect()
        {
            InitializeComponent();
        }

        public string waiterName;

        private void frmWaiterSelect_Load(object sender, EventArgs e)
        {
            string qry = "SELECT * FROM Staff WHERE sRole LIKE 'Waiter'";
            SqlCommand cmd = new SqlCommand(qry, MainClass.conn);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                Guna.UI2.WinForms.Guna2Button btn = new Guna.UI2.WinForms.Guna2Button();
                btn.Text = row["sName"].ToString();
                btn.Width = 150;
                btn.Height = 50;
                btn.FillColor = Color.FromArgb(241, 85, 126);
                btn.HoverState.FillColor = Color.FromArgb(50, 55, 89);


                //Event for Click
                btn.Click += new EventHandler(btn_Click);

                //Add the button to the panel
                flowLayoutPanel1.Controls.Add(btn);
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            waiterName = (sender as Guna.UI2.WinForms.Guna2Button).Text.ToString();
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
