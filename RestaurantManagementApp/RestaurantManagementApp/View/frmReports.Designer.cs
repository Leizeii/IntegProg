namespace RestaurantManagementApp.Model
{
    partial class frmReports
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.btnMenu = new Guna.UI2.WinForms.Guna2Button();
            this.btnStaff = new Guna.UI2.WinForms.Guna2Button();
            this.btnSalesCat = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(39, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Reports";
            // 
            // btnMenu
            // 
            this.btnMenu.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnMenu.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnMenu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnMenu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnMenu.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(55)))), ((int)(((byte)(89)))));
            this.btnMenu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnMenu.ForeColor = System.Drawing.Color.White;
            this.btnMenu.Image = global::RestaurantManagementApp.Properties.Resources.report;
            this.btnMenu.Location = new System.Drawing.Point(42, 71);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(151, 95);
            this.btnMenu.TabIndex = 1;
            this.btnMenu.Text = "Menu List";
            this.btnMenu.TextOffset = new System.Drawing.Point(5, 0);
            this.btnMenu.Click += new System.EventHandler(this.btnMenu_Click);
            // 
            // btnStaff
            // 
            this.btnStaff.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnStaff.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnStaff.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnStaff.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnStaff.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(55)))), ((int)(((byte)(89)))));
            this.btnStaff.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnStaff.ForeColor = System.Drawing.Color.White;
            this.btnStaff.Image = global::RestaurantManagementApp.Properties.Resources.report;
            this.btnStaff.Location = new System.Drawing.Point(199, 71);
            this.btnStaff.Name = "btnStaff";
            this.btnStaff.Size = new System.Drawing.Size(161, 95);
            this.btnStaff.TabIndex = 1;
            this.btnStaff.Text = "Staff List";
            this.btnStaff.TextOffset = new System.Drawing.Point(5, 0);
            this.btnStaff.Click += new System.EventHandler(this.btnStaff_Click);
            // 
            // btnSalesCat
            // 
            this.btnSalesCat.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSalesCat.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSalesCat.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSalesCat.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSalesCat.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(55)))), ((int)(((byte)(89)))));
            this.btnSalesCat.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSalesCat.ForeColor = System.Drawing.Color.White;
            this.btnSalesCat.Image = global::RestaurantManagementApp.Properties.Resources.report;
            this.btnSalesCat.Location = new System.Drawing.Point(366, 71);
            this.btnSalesCat.Name = "btnSalesCat";
            this.btnSalesCat.Size = new System.Drawing.Size(209, 95);
            this.btnSalesCat.TabIndex = 1;
            this.btnSalesCat.Text = "Sale by Category";
            this.btnSalesCat.TextOffset = new System.Drawing.Point(5, 0);
            this.btnSalesCat.Click += new System.EventHandler(this.btnSalesCat_Click);
            // 
            // frmReports
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSalesCat);
            this.Controls.Add(this.btnStaff);
            this.Controls.Add(this.btnMenu);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmReports";
            this.Text = " ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Button btnMenu;
        private Guna.UI2.WinForms.Guna2Button btnStaff;
        private Guna.UI2.WinForms.Guna2Button btnSalesCat;
    }
}