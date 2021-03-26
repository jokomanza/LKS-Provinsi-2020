namespace PC_KAB_KLATEN_JOKO_SUPRIYANTO
{
    partial class MainForm
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
            this.btnMasterMember = new System.Windows.Forms.Button();
            this.btnMasterVehicle = new System.Windows.Forms.Button();
            this.btnPayment = new System.Windows.Forms.Button();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(69, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(290, 30);
            this.label1.TabIndex = 1;
            this.label1.Text = "MANDHEG PARKING SYSTEM";
            // 
            // btnMasterMember
            // 
            this.btnMasterMember.Location = new System.Drawing.Point(143, 100);
            this.btnMasterMember.Name = "btnMasterMember";
            this.btnMasterMember.Size = new System.Drawing.Size(143, 38);
            this.btnMasterMember.TabIndex = 0;
            this.btnMasterMember.Text = "Master Member";
            this.btnMasterMember.UseVisualStyleBackColor = true;
            this.btnMasterMember.Click += new System.EventHandler(this.btnMasterMember_Click);
            // 
            // btnMasterVehicle
            // 
            this.btnMasterVehicle.Location = new System.Drawing.Point(143, 144);
            this.btnMasterVehicle.Name = "btnMasterVehicle";
            this.btnMasterVehicle.Size = new System.Drawing.Size(143, 38);
            this.btnMasterVehicle.TabIndex = 1;
            this.btnMasterVehicle.Text = "Master Vehicle";
            this.btnMasterVehicle.UseVisualStyleBackColor = true;
            this.btnMasterVehicle.Click += new System.EventHandler(this.btnMasterVehicle_Click);
            // 
            // btnPayment
            // 
            this.btnPayment.Location = new System.Drawing.Point(143, 188);
            this.btnPayment.Name = "btnPayment";
            this.btnPayment.Size = new System.Drawing.Size(143, 38);
            this.btnPayment.TabIndex = 2;
            this.btnPayment.Text = "Parking Payment";
            this.btnPayment.UseVisualStyleBackColor = true;
            this.btnPayment.Click += new System.EventHandler(this.btnPayment_Click);
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Location = new System.Drawing.Point(12, 9);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(67, 13);
            this.lblWelcome.TabIndex = 3;
            this.lblWelcome.Text = "lblWelcome";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(12, 26);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(44, 13);
            this.lblDate.TabIndex = 3;
            this.lblDate.Text = "lblDate";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(400, 250);
            this.ClientSize = new System.Drawing.Size(428, 277);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.btnPayment);
            this.Controls.Add(this.btnMasterVehicle);
            this.Controls.Add(this.btnMasterMember);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnMasterMember;
        private System.Windows.Forms.Button btnMasterVehicle;
        private System.Windows.Forms.Button btnPayment;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblDate;
    }
}