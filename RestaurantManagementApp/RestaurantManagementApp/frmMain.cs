using RestaurantManagementApp.Model;
using RestaurantManagementApp.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantManagementApp
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        // For accessing frm main
        static frmMain _obj;
        public static frmMain Instance
        {
            get
            {
                if (_obj == null)
                {
                    _obj = new frmMain();
                }
                return _obj;
            }
        }

        // Method to add Controls in Main Form
        public static void AddControls(Form f, Guna.UI2.WinForms.Guna2Panel centerPanel)
        {
            centerPanel.Controls.Clear();
            f.Dock = DockStyle.Fill;
            f.TopLevel = false;
            centerPanel.Controls.Add(f);
            f.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            lblUser.Text = MainClass.USER;
            _obj = this;
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            AddControls(new frmHome(), centerPanel);
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            AddControls(new frmCategoryView(), centerPanel);
        }

        private void btnTable_Click(object sender, EventArgs e)
        {
            AddControls(new frmTableView(), centerPanel);
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            AddControls(new frmStaffView(), centerPanel);
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            AddControls(new frmProductView(), centerPanel);
        }

        private void btnPOS_Click(object sender, EventArgs e)
        {
            frmPOS frm = new frmPOS();
            frm.Show();
        }

        private void btnKitchen_Click(object sender, EventArgs e)
        {
            AddControls(new frmKitchenView(), centerPanel);
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            AddControls(new frmReports(), centerPanel);
        }
    }
}
