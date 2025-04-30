using System;
using System.Windows.Forms;

namespace RestaurantManagementApp
{
    public partial class ucActionButtons : UserControl
    {
        public ucActionButtons()
        {
            InitializeComponent();
        }

        // Custom events to be exposed to parent forms
        public event EventHandler SaveClicked;
        public event EventHandler CloseClicked;

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveClicked?.Invoke(this, e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseClicked?.Invoke(this, e);
        }
    }
}
