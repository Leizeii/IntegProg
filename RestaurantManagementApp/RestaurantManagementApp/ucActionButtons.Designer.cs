namespace RestaurantManagementApp
{
    partial class ucActionButtons
    {
        private System.ComponentModel.IContainer components = null;

        // Guna2 buttons
        public Guna.UI2.WinForms.Guna2Button btnSave;
        public Guna.UI2.WinForms.Guna2Button btnClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.btnSave = new Guna.UI2.WinForms.Guna2Button();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.AutoRoundedCorners = true;
            this.btnSave.BorderRadius = 17;
            this.btnSave.CustomizableEdges.TopRight = false;
            this.btnSave.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(85)))), ((int)(((byte)(126)))));
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(18, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(85, 36);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "SAVE";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.AutoRoundedCorners = true;
            this.btnClose.BorderRadius = 17;
            this.btnClose.CustomizableEdges.TopRight = false;
            this.btnClose.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(55)))), ((int)(((byte)(89)))));
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(109, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(85, 36);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "CLOSE";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ucActionButtons
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Name = "ucActionButtons";
            this.Size = new System.Drawing.Size(230, 56);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
