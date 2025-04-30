using System;
using System.Windows.Forms;

namespace RestaurantManagementApp
{
    public partial class SampleAdd : Form
    {
        public ucActionButtons ucActionButtons1;

        public SampleAdd()
        {
            InitializeComponent();

            // Initialize ucActionButtons and set its location and size
            ucActionButtons1 = new ucActionButtons();
            ucActionButtons1.Location = new System.Drawing.Point(0, 333); // Adjust the location as needed
            ucActionButtons1.Size = new System.Drawing.Size(636, 56); // Set size to match your form
            this.Controls.Add(ucActionButtons1);

            // Subscribe to Save and Close button events
            ucActionButtons1.SaveClicked += ucActionButtons1_SaveClicked;
            ucActionButtons1.CloseClicked += ucActionButtons1_CloseClicked;
        }

        private void ucActionButtons1_SaveClicked(object sender, EventArgs e)
        {
            // Implement your save logic here, e.g., saving data
            MessageBox.Show("Save button clicked.");
        }

        private void ucActionButtons1_CloseClicked(object sender, EventArgs e)
        {
            // Handle closing the form
            this.Close();
        }
    }
}
