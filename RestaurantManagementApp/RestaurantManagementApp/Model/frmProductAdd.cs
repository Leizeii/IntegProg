using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantManagementApp.Model
{
    public partial class frmProductAdd : Form
    {
        public frmProductAdd()
        {
            InitializeComponent();
        }

        public int id = 0;
        public int cID = 0;

        private void frmProductAdd_Load(object sender, EventArgs e)
        {
            // For CB Fill
            string qry = "SELECT catID 'id', catName 'name' FROM Category";
            MainClass.CBFill(qry, cbxCat);

            if (cID > 0) // For UPDATE
            {
                cbxCat.SelectedValue = cID;
            }

            if (id > 0) // For UPDATE
            {
                ForUpdateLoadData();
            }
        }

        string filePath;
        Byte[] imageByteArray;

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filePath = ofd.FileName;
                pbPic.Image = new Bitmap(filePath);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string qry = "";

            if (id == 0) // INSERT
            {
                qry = "INSERT INTO Products VALUES(@Name, @Price, @Cat, @Img)";
            }
            else // UPDATE
            {
                qry = "UPDATE Products SET pName = @Name, pPrice = @Price, CategoryID = @Cat, pImage = @Img WHERE pID = @id";
            }

            // For IMAGE
            Image temp = new Bitmap(pbPic.Image);
            MemoryStream ms = new MemoryStream();
            temp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            imageByteArray = ms.ToArray();


            Hashtable ht = new Hashtable();
            ht.Add("@id", id);
            ht.Add("@Name", txtName.Text);
            ht.Add("@Price", txtPrice.Text);
            ht.Add("@Cat", Convert.ToInt32(cbxCat.SelectedValue));
            ht.Add("@Img", imageByteArray);

            if (MainClass.SQL(qry, ht) > 0)
            {
                MessageBox.Show("Product Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                id = 0;
                cID = 0;
                txtName.Clear();
                txtPrice.Clear();
                cbxCat.SelectedIndex = 0;
                cbxCat.SelectedIndex = -1;
                pbPic.Image = RestaurantManagementApp.Properties.Resources.bag;
                txtName.Focus();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ForUpdateLoadData()
        {
            string qry = "SELECT * FROM Products WHERE pID = " + id + "";
            SqlCommand cmd = new SqlCommand(qry, MainClass.conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                txtName.Text = dt.Rows[0]["pName"].ToString();
                txtPrice.Text = dt.Rows[0]["pPrice"].ToString();
                cbxCat.SelectedValue = dt.Rows[0]["CategoryID"].ToString();
                // For IMAGE
                byte[] imageArray = (byte[])dt.Rows[0]["pImage"];
                byte[] imageByteArray = imageArray;
                pbPic.Image = Image.FromStream(new MemoryStream(imageArray));
            }
            else
            {
                MessageBox.Show("No Record Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
