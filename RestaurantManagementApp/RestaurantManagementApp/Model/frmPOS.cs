using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace RestaurantManagementApp.Model
{
    public partial class frmPOS : Form
    {
        public frmPOS()
        {
            InitializeComponent();
        }

        public int MainID = 0;
        public string OrderType;
        public int driverID = 0;
        public string customerName = "";
        public string customerPhone = "";

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPOS_Load(object sender, EventArgs e)
        {
            dgvPOS.BorderStyle = BorderStyle.FixedSingle;
            AddCategory();

            ProductPanel.Controls.Clear();
            LoadProducts();
        }

        private void AddCategory()
        {
            string qry = "SELECT * FROM Category";
            SqlCommand cmd = new SqlCommand(qry, MainClass.conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            CategoryPanel.Controls.Clear();

            // Add "All Categories" button first
            Guna.UI2.WinForms.Guna2Button allBtn = new Guna.UI2.WinForms.Guna2Button();
            allBtn.FillColor = Color.FromArgb(50, 55, 89);
            allBtn.Size = new Size(161, 45);
            allBtn.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            allBtn.Text = "ALL CATEGORIES";
            allBtn.Click += new EventHandler(btn_Click);
            CategoryPanel.Controls.Add(allBtn);

            // Add other category buttons
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Guna.UI2.WinForms.Guna2Button btn = new Guna.UI2.WinForms.Guna2Button();
                    btn.FillColor = Color.FromArgb(50, 55, 89);
                    btn.Size = new Size(161, 45);
                    btn.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
                    btn.Text = row["catName"].ToString();

                    //Event for Click
                    btn.Click += new EventHandler(btn_Click);

                    CategoryPanel.Controls.Add(btn);
                }
            }
        }
        // Store the last selected category
        private string lastSelectedCategory = "ALL CATEGORIES";

        private void btn_Click(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2Button btn = (Guna.UI2.WinForms.Guna2Button)sender;
            string selectedCategory = btn.Text;


            if (selectedCategory == "ALL CATEGORIES")
            {
                // Store that "ALL CATEGORIES" was selected
                lastSelectedCategory = "ALL CATEGORIES";

                // Show all products
                foreach (var item in ProductPanel.Controls)
                {
                    var pro = (ucProduct)item;
                    pro.Visible = true; // Show all
                }
            }
            else
            {
                // Store the selected category
                lastSelectedCategory = selectedCategory;

                // Show only products of the selected category
                foreach (var item in ProductPanel.Controls)
                {
                    var pro = (ucProduct)item;
                    pro.Visible = pro.PCategory.Equals(selectedCategory, StringComparison.OrdinalIgnoreCase);
                }
            }
        }

        private void AddItems(string id, String proID, string name, string cat, string price, Image pimage)
        {
            var w = new ucProduct()
            {
                PName = name,
                PPrice = price,
                PCategory = cat,
                PImage = pimage,
                id = Convert.ToInt32(proID),
            };

            ProductPanel.Controls.Add(w);

            w.onSelect += (ss, ee) =>
            {
                var wdg = (ucProduct)ss;

                foreach (DataGridViewRow item in dgvPOS.Rows)
                {
                    // This is to check if the item already exists in the dgvPOS, and if it does, it will update the quantity and amount
                    if (Convert.ToInt32(item.Cells["dgvproID"].Value) == wdg.id)
                    {
                        item.Cells["dgvQty"].Value = int.Parse(item.Cells["dgvQty"].Value.ToString()) + 1;
                        item.Cells["dgvAmount"].Value = int.Parse(item.Cells["dgvQty"].Value.ToString()) *
                                                        double.Parse(item.Cells["dgvPrice"].Value.ToString());
                        GetTotal();
                        return;
                    }
                }
                // This is to add new item to the dgvPOS
                dgvPOS.Rows.Add(new object[] {0, 0, wdg.id, wdg.PName, 1, wdg.PPrice, wdg.PPrice});
                GetTotal();
            };   
        }

        // Getting product from Database          
        private void LoadProducts()
        {
            string qry = "SELECT * FROM Products INNER JOIN Category ON catID = CategoryID";
            SqlCommand cmd = new SqlCommand(qry, MainClass.conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow item in dt.Rows)
            {
                Byte[] imageArray = (Byte[])(item["pImage"]);
                byte[] imageByteArray = imageArray;

                AddItems("0", item["pID"].ToString(), item["pName"].ToString(), item["catName"].ToString(),
                    item["pPrice"].ToString(), Image.FromStream(new MemoryStream(imageArray)));
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // If search box is cleared, show all products regardless of selected category
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                // If no category was selected, show all products
                if (string.IsNullOrEmpty(lastSelectedCategory) || lastSelectedCategory == "ALL CATEGORIES")
                {
                    foreach (var item in ProductPanel.Controls)
                    {
                        var pro = (ucProduct)item;
                        pro.Visible = true; // Show all products
                    }
                }
                else
                {
                    // If a category is selected, show only products from that category
                    foreach (var item in ProductPanel.Controls)
                    {
                        var pro = (ucProduct)item;
                        pro.Visible = pro.PCategory.Equals(lastSelectedCategory, StringComparison.OrdinalIgnoreCase);
                    }
                }
            }
            else
            {
                // If there's text in the search box, show only the products that match the search
                foreach (var item in ProductPanel.Controls)
                {
                    var pro = (ucProduct)item;
                    pro.Visible = pro.PName.ToLower().Contains(txtSearch.Text.Trim().ToLower());
                }
            }
        }

        private void dgvPOS_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // For Serial No.
            
            int count = 0;

            foreach (DataGridViewRow row in dgvPOS.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
        }

        private void GetTotal() 
        {
            double total = 0;
            lblTotal.Text = "";

            foreach (DataGridViewRow item in dgvPOS.Rows)
            {
                total += double.Parse(item.Cells["dgvAmount"].Value.ToString());
            }

            lblTotal.Text = total.ToString("N2");
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            lblTable.Text = "";
            lblWaiter.Text = "";
            lblTable.Visible = false;
            lblWaiter.Visible = false;
            dgvPOS.Rows.Clear();
            MainID = 0;
            lblTotal.Text = "0.00";
        }

        private void btnDelivery_Click(object sender, EventArgs e)
        {
            lblTable.Text = "";
            lblWaiter.Text = "";
            lblTable.Visible = false;
            lblWaiter.Visible = false;
            OrderType = "Delivery";

            frmAddCustomer frm = new frmAddCustomer();
            frm.mainID = MainID;
            frm.orderType = OrderType;
            MainClass.BlurBackground(frm);

            if (frm.txtName.Text != "") // Take out does not require driverID, but we can still show the customer name and phone number
            {
                driverID = frm.driverID;
                lblDriverName.Text = "Customer Name: " + frm.txtName.Text + " Phone: " + frm.txtPhone.Text + " Driver: " + frm.cbxDriver.Text;
                lblDriverName.Visible = true;
                customerName = frm.txtName.Text;
                customerPhone = frm.txtPhone.Text;
            }
        }

        private void btnTakeOut_Click(object sender, EventArgs e)
        {
            lblTable.Text = "";
            lblWaiter.Text = "";
            lblTable.Visible = false;
            lblWaiter.Visible = false;
            OrderType = "Take Out";

            frmAddCustomer frm = new frmAddCustomer();
            frm.mainID = MainID;
            frm.orderType = OrderType;
            MainClass.BlurBackground(frm);

            if (frm.txtName.Text != "") // Take out does not require driverID, but we can still show the customer name and phone number
            { 
                driverID = frm.driverID;
                lblDriverName.Text = "Customer Name: " + frm.txtName.Text + " Phone: " + frm.txtPhone.Text;
                lblDriverName.Visible = true;
                customerName = frm.txtName.Text;
                customerPhone = frm.txtPhone.Text;
            }
        }

        private void btnDineIn_Click(object sender, EventArgs e)
        {
            OrderType = "Dine In";
            lblDriverName.Visible = false;
            frmTableSelect frm = new frmTableSelect();
            MainClass.BlurBackground(frm);
            if (frm.TableName != "")
            {
                lblTable.Text = frm.TableName;
                lblTable.Visible = true;
            }
            else
            {
                lblTable.Text = "";
                lblTable.Visible = false;
            }

            frmWaiterSelect frm2 = new frmWaiterSelect();
            MainClass.BlurBackground(frm2);
            if (frm2.waiterName != "")
            {
                lblWaiter.Text = frm2.waiterName;
                lblWaiter.Visible = true;
            }
            else
            {
                lblWaiter.Text = "";
                lblWaiter.Visible = false;
            }
        }

        private void btnKot_Click(object sender, EventArgs e)
        {
            // Check if there are any items in the DataGridView
            if (dgvPOS.Rows.Count == 0)
            {
                MessageBox.Show("Please select at least one item before placing the order.", "No Items Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method early
            }

            // Check if the total amount is greater than zero
            if (Convert.ToDouble(lblTotal.Text) <= 0)
            {
                MessageBox.Show("Please add items to the order before placing it.", "No Items Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method early
            }

            // Check if the order type is selected
            if (string.IsNullOrEmpty(OrderType))
            {
                MessageBox.Show("Please select an order type (Dine In, Take Out, or Delivery).", "Order Type Not Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method early
            }

            // Check if the waiter name and table name are provided (Dine In only)
            if (OrderType == "Dine In" && (string.IsNullOrEmpty(lblWaiter.Text) || string.IsNullOrEmpty(lblTable.Text)))
            {
                MessageBox.Show("Please select a waiter and a table before placing the order.", "Incomplete Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method early
            }
              
            string qry1 = ""; // For tblMain  
            string qry2 = ""; // For tblDetails  
            int detailID = 0;

            if (MainID == 0) // INSERT  
            {
                qry1 = @"INSERT INTO tblMain VALUES (@aDate, @aTime, @TableName, @WaiterName, @status, @orderType,  
                      @total, @received, @change, @driverID, @CustName, @CustPhone)  
                      SELECT SCOPE_IDENTITY()"; // This will return the last inserted ID  
            }
            else // UPDATE  
            {
                qry1 = @"UPDATE tblMain SET status = @status, total = @total, received = @received, change = @change WHERE MainID = @ID)";
            }

            SqlCommand cmd = new SqlCommand(qry1, MainClass.conn);
            cmd.Parameters.AddWithValue("@ID", MainID);
            cmd.Parameters.AddWithValue("@aDate", Convert.ToDateTime(DateTime.Now.Date));
            cmd.Parameters.AddWithValue("@aTime", DateTime.Now.ToShortTimeString());
            cmd.Parameters.AddWithValue("@TableName", lblTable.Text);
            cmd.Parameters.AddWithValue("@WaiterName", lblWaiter.Text);
            cmd.Parameters.AddWithValue("@status", "Pending");
            cmd.Parameters.AddWithValue("@orderType", OrderType);
            cmd.Parameters.AddWithValue("@total", Convert.ToDouble(lblTotal.Text)); // For Kitchen Value Only  
            cmd.Parameters.AddWithValue("@received", Convert.ToDouble(0));
            cmd.Parameters.AddWithValue("@change", Convert.ToDouble(0));
            cmd.Parameters.AddWithValue("@driverID", driverID);
            cmd.Parameters.AddWithValue("@CustName", customerName);
            cmd.Parameters.AddWithValue("@CustPhone", customerPhone);

            if (MainClass.conn.State == ConnectionState.Closed) { MainClass.conn.Open(); }
            if (MainID == 0) { MainID = Convert.ToInt32(cmd.ExecuteScalar()); } else { cmd.ExecuteNonQuery(); }
            if (MainClass.conn.State == ConnectionState.Open) { MainClass.conn.Close(); }

            // This is to insert the data into tblDetails  
            foreach (DataGridViewRow row in dgvPOS.Rows)
            {
                detailID = Convert.ToInt32(row.Cells["dgvid"].Value);

                if (detailID == 0) // INSERT  
                {
                    qry2 = @"INSERT INTO tblDetails VALUES (@MainID, @proID, @qty, @price, @amount)";
                }
                else // UPDATE  
                {
                    qry2 = @"UPDATE tblDetails SET proID = @proID, qty = @qty, price = @price, amount = @amount WHERE DetailID = @ID";
                }

                SqlCommand cmd2 = new SqlCommand(qry2, MainClass.conn);
                cmd2.Parameters.AddWithValue("@ID", detailID);
                cmd2.Parameters.AddWithValue("@MainID", MainID);
                cmd2.Parameters.AddWithValue("@proID", Convert.ToInt32(row.Cells["dgvproID"].Value));
                cmd2.Parameters.AddWithValue("@qty", Convert.ToInt32(row.Cells["dgvQty"].Value));
                cmd2.Parameters.AddWithValue("@price", Convert.ToDouble(row.Cells["dgvPrice"].Value));
                cmd2.Parameters.AddWithValue("@amount", Convert.ToDouble(row.Cells["dgvAmount"].Value));

                if (MainClass.conn.State == ConnectionState.Closed) MainClass.conn.Open();
                cmd2.ExecuteNonQuery();
                if (MainClass.conn.State == ConnectionState.Open) MainClass.conn.Close();
            }

            // This is to show the message after inserting the data  
            MessageBox.Show("Order has been placed successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            MainID = 0;
            detailID = 0;
            dgvPOS.Rows.Clear(); // This is to clear the dgvPOS after inserting the data  
            lblTable.Text = "";
            lblWaiter.Text = "";
            lblTable.Visible = false;
            lblWaiter.Visible = false;
            lblTotal.Text = "0.00";
            lblDriverName.Text = "";
        }

        public int id = 0;

        private void btnBill_Click(object sender, EventArgs e)
        {
            frmBillList frm = new frmBillList();
            MainClass.BlurBackground(frm);

            if (frm.MainID > 0)
            {
                id = frm.MainID;
                MainID = frm.MainID;
                LoadEntries();
            }
            //else // This is to check if the user has selected a bill or not
            //{
            //    MessageBox.Show("No Bill Selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }

        private void LoadEntries()
        {
            string qry = @"SELECT * FROM tblMain m
                           INNER JOIN tblDetails d ON m.MainID = d.MainID
                           INNER JOIN Products p ON p.pID = d.proID
                           WHERE m.MainID = " + id + "";

            SqlCommand cmd = new SqlCommand(qry, MainClass.conn);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["orderType"].ToString() == "Delivery")
                {
                    btnDelivery.Checked = true;
                    lblTable.Visible = false;
                    lblWaiter.Visible = false;
                }
                else if (dt.Rows[0]["orderType"].ToString() == "Take Out")
                {
                    btnTakeOut.Checked = true;
                    lblTable.Visible = false;
                    lblWaiter.Visible = false;
                }
                else
                {
                    btnDineIn.Checked = true;
                    lblTable.Visible = true;
                    lblWaiter.Visible = true;
                }
            }
            else 
            {
                MessageBox.Show("No data found for the given ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            dgvPOS.Rows.Clear(); // This is to clear the dgvPOS before loading the data

            foreach (DataRow item in dt.Rows) 
            {
                lblTable.Text = dt.Rows[0]["TableName"].ToString();
                lblWaiter.Text = dt.Rows[0]["WaiterName"].ToString();

                string detailID = item["DetailID"].ToString();
                string proName = item["pName"].ToString();
                string proID = item["proID"].ToString();
                string qty = item["qty"].ToString();
                string price = item["price"].ToString();
                string amount = item["amount"].ToString();

                object[] obj = {0, detailID, proID, proName, qty, price, amount}; 
                dgvPOS.Rows.Add(obj); // This is to add the data to the dgvPOS
            }
            GetTotal();
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            frmCheckOut frm = new frmCheckOut();
            frm.MainID = id;
            frm.amt = Convert.ToDouble(lblTotal.Text);
            MainClass.BlurBackground(frm);

            MainID = 0;
            dgvPOS.Rows.Clear(); // This is to clear the dgvPOS after inserting the data
            lblTable.Text = "";
            lblWaiter.Text = "";
            lblTable.Visible = false;
            lblWaiter.Visible = false;
            lblTotal.Text = "0.00";
        }

        private void btnHold_Click(object sender, EventArgs e)
        {
            // Check if there are any items in the DataGridView
            if (dgvPOS.Rows.Count == 0)
            {
                MessageBox.Show("Please select at least one item before placing the order.", "No Items Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method early
            }

            // Check if the total amount is greater than zero
            if (Convert.ToDouble(lblTotal.Text) <= 0)
            {
                MessageBox.Show("Please add items to the order before placing it.", "No Items Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method early
            }

            // Check if the order type is selected
            if (string.IsNullOrEmpty(OrderType))
            {
                MessageBox.Show("Please select an order type (Dine In, Take Out, or Delivery).", "Order Type Not Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method early
            }

            // Check if the waiter name and table name are provided (Dine In only)
            if (OrderType == "Dine In" && (string.IsNullOrEmpty(lblWaiter.Text) || string.IsNullOrEmpty(lblTable.Text)))
            {
                MessageBox.Show("Please select a waiter and a table before placing the order.", "Incomplete Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method early
            }
            
            string qry1 = ""; // For tblMain  
            string qry2 = ""; // For tblDetails  
            int detailID = 0;

            if (MainID == 0) // INSERT  
            {
                qry1 = @"INSERT INTO tblMain VALUES (@aDate, @aTime, @TableName, @WaiterName, @status, @orderType,  
                      @total, @received, @change, @driverID, @CustName, @CustPhone)  
                      SELECT SCOPE_IDENTITY()"; // This will return the last inserted ID  
            }
            else // UPDATE  
            {
                qry1 = @"UPDATE tblMain SET status = @status, total = @total, received = @received, change = @change WHERE MainID = @ID)";
            }

            SqlCommand cmd = new SqlCommand(qry1, MainClass.conn);
            cmd.Parameters.AddWithValue("@ID", MainID);
            cmd.Parameters.AddWithValue("@aDate", Convert.ToDateTime(DateTime.Now.Date));
            cmd.Parameters.AddWithValue("@aTime", DateTime.Now.ToShortTimeString());
            cmd.Parameters.AddWithValue("@TableName", lblTable.Text);
            cmd.Parameters.AddWithValue("@WaiterName", lblWaiter.Text);
            cmd.Parameters.AddWithValue("@status", "Hold");
            cmd.Parameters.AddWithValue("@orderType", OrderType);
            cmd.Parameters.AddWithValue("@total", Convert.ToDouble(lblTotal.Text)); // For Kitchen Value Only  
            cmd.Parameters.AddWithValue("@received", Convert.ToDouble(0));
            cmd.Parameters.AddWithValue("@change", Convert.ToDouble(0));
            cmd.Parameters.AddWithValue("@driverID", driverID);
            cmd.Parameters.AddWithValue("@CustName", customerName);
            cmd.Parameters.AddWithValue("@CustPhone", customerPhone);

            if (MainClass.conn.State == ConnectionState.Closed) { MainClass.conn.Open(); }
            if (MainID == 0) { MainID = Convert.ToInt32(cmd.ExecuteScalar()); } else { cmd.ExecuteNonQuery(); }
            if (MainClass.conn.State == ConnectionState.Open) { MainClass.conn.Close(); }

            // This is to insert the data into tblDetails  
            foreach (DataGridViewRow row in dgvPOS.Rows)
            {
                detailID = Convert.ToInt32(row.Cells["dgvid"].Value);

                if (detailID == 0) // INSERT  
                {
                    qry2 = @"INSERT INTO tblDetails VALUES (@MainID, @proID, @qty, @price, @amount)";
                }
                else // UPDATE  
                {
                    qry2 = @"UPDATE tblDetails SET proID = @proID, qty = @qty, price = @price, amount = @amount WHERE DetailID = @ID";
                }

                SqlCommand cmd2 = new SqlCommand(qry2, MainClass.conn);
                cmd2.Parameters.AddWithValue("@ID", detailID);
                cmd2.Parameters.AddWithValue("@MainID", MainID);
                cmd2.Parameters.AddWithValue("@proID", Convert.ToInt32(row.Cells["dgvproID"].Value));
                cmd2.Parameters.AddWithValue("@qty", Convert.ToInt32(row.Cells["dgvQty"].Value));
                cmd2.Parameters.AddWithValue("@price", Convert.ToDouble(row.Cells["dgvPrice"].Value));
                cmd2.Parameters.AddWithValue("@amount", Convert.ToDouble(row.Cells["dgvAmount"].Value));

                if (MainClass.conn.State == ConnectionState.Closed) MainClass.conn.Open();
                cmd2.ExecuteNonQuery();
                if (MainClass.conn.State == ConnectionState.Open) MainClass.conn.Close();
            }

            // This is to show the message after inserting the data  
            MessageBox.Show("Order has been placed successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            MainID = 0;
            detailID = 0;
            dgvPOS.Rows.Clear(); // This is to clear the dgvPOS after inserting the data  
            lblTable.Text = "";
            lblWaiter.Text = "";
            lblTable.Visible = false;
            lblWaiter.Visible = false;
            lblTotal.Text = "0.00";
            lblDriverName.Text = "";
        }
    }
}