using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantManagementApp.Model
{
    
    public partial class ucProduct : UserControl
    {
        public ucProduct()
        {
            InitializeComponent();
        }

        public event EventHandler onSelect = null;

        public int id { get; set; }
        public string PPrice { get; set; }
        public string PCategory { get; set; }

        public string PName 
        {
            get { return lblName.Text; }
            set { lblName.Text = value; }
        }

        public Image PImage
        {
            get { return pbPic.Image; }
            set { pbPic.Image = value; }
        }

        private void pbPic_Click(object sender, EventArgs e)
        {
            onSelect?.Invoke(this, e);

        }
    }
}
