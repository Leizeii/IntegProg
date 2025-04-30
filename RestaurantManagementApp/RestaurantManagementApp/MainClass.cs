using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantManagementApp
{
    internal class MainClass
    {

        public static readonly string conn_String = "Data Source=DESKTOP-R791T79\\SQLEXPRESS;Initial Catalog=RM;Integrated Security=True";
        public static SqlConnection conn = new SqlConnection(conn_String);

        //Method to check user validation
        public static bool IsValidUser(string user, string pass)
        {
            bool isValid = false;

            string qry = @"SELECT * FROM Users WHERE Username = '" + user + "' AND uPass = '" + pass + "'";
            SqlCommand cmd = new SqlCommand(qry, conn);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            if (dt.Rows.Count > 0) {
                isValid = true;
                USER = dt.Rows[0]["uName"].ToString();
            } else {
                isValid = false;
            }
            return isValid;

        }
        //Create property for user

        public static string user;

        public static string USER
        {
            get { return user; }
            private set { user = value; }
        }
        
        //Method for CRUD Operations
        public static int SQL(string qry, Hashtable ht) 
        {
            int result = 0;

            try
            {
                SqlCommand cmd = new SqlCommand(qry, conn);
                cmd.CommandType = CommandType.Text;
                if (ht != null)
                {
                    foreach (DictionaryEntry item in ht)
                    {
                        cmd.Parameters.AddWithValue(item.Key.ToString(), item.Value);
                    }
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    result = cmd.ExecuteNonQuery();
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                conn.Close();
            }

            return result;
        }

        //For loading data from database
        public static void LoadData(string qry, DataGridView gv, ListBox lb)
        {
            // Serial No. in GridView
            gv.CellFormatting += new DataGridViewCellFormattingEventHandler(gv_CellFormatting);

            try
            {
                SqlCommand cmd = new SqlCommand(qry, conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < lb.Items.Count; i++)
                {
                    string colNam1 = ((DataGridViewColumn)lb.Items[i]).Name;
                    gv.Columns[colNam1].DataPropertyName = dt.Columns[i].ToString();
                }

                gv.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                conn.Close();
            }
        }

        private static void gv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Guna.UI2.WinForms.Guna2DataGridView gv = (Guna.UI2.WinForms.Guna2DataGridView)sender;
            int count = 0;

            foreach (DataGridViewRow row in gv.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
        }

        public static void BlurBackground(Form Model)
        {
            Form Background = new Form();
            using (Model) 
            {
                Background.StartPosition = FormStartPosition.Manual;
                Background.FormBorderStyle = FormBorderStyle.None;
                Background.Opacity = 0.5d;
                Background.BackColor = Color.Black;
                Background.Size = frmMain.Instance.Size;
                Background.Location = frmMain.Instance.Location;   
                Background.ShowInTaskbar = false;
                Background.Show();
                Model.Owner = Background;
                Model.ShowDialog(Background);
                Background.Dispose();
            }
        }

        // For CB Fill
        public static void CBFill (string qry, ComboBox cbx)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(qry, conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cbx.DisplayMember = "name";
                cbx.ValueMember = "id";
                cbx.DataSource = dt;
                cbx.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                conn.Close();
            }
        }
    }
}
