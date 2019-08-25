namespace ScanHelper
{
    partial class FrmAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAbout));
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.lblNazwa = new System.Windows.Forms.Label();
            this.lblAdres = new System.Windows.Forms.Label();
            this.lblWWW = new System.Windows.Forms.LinkLabel();
            this.lblLicenseInfo = new System.Windows.Forms.Label();
            this.pictureBoxLogoOwner = new System.Windows.Forms.PictureBox();
            this.lblLicenseOwner = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.labelLicenseOptions = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogoOwner)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxLogo.Image")));
            this.pictureBoxLogo.Location = new System.Drawing.Point(412, 12);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(70, 65);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxLogo.TabIndex = 1;
            this.pictureBoxLogo.TabStop = false;
            // 
            // lblNazwa
            // 
            this.lblNazwa.AutoSize = true;
            this.lblNazwa.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblNazwa.Location = new System.Drawing.Point(12, 12);
            this.lblNazwa.Name = "lblNazwa";
            this.lblNazwa.Size = new System.Drawing.Size(357, 17);
            this.lblNazwa.TabIndex = 2;
            this.lblNazwa.Text = "GISNET Grzegorz Gogolewski i Wspólnicy Spółka Jawna";
            // 
            // lblAdres
            // 
            this.lblAdres.AutoSize = true;
            this.lblAdres.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblAdres.Location = new System.Drawing.Point(12, 39);
            this.lblAdres.Name = "lblAdres";
            this.lblAdres.Size = new System.Drawing.Size(190, 14);
            this.lblAdres.TabIndex = 4;
            this.lblAdres.Text = "ul. Lendziona 4b/4, 80-264 Gdańsk";
            // 
            // lblWWW
            // 
            this.lblWWW.AutoSize = true;
            this.lblWWW.Location = new System.Drawing.Point(12, 64);
            this.lblWWW.Name = "lblWWW";
            this.lblWWW.Size = new System.Drawing.Size(127, 13);
            this.lblWWW.TabIndex = 5;
            this.lblWWW.TabStop = true;
            this.lblWWW.Text = "http://www.gisnet.com.pl";
            this.lblWWW.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LblWWW_LinkClicked);
            // 
            // lblLicenseInfo
            // 
            this.lblLicenseInfo.Location = new System.Drawing.Point(12, 128);
            this.lblLicenseInfo.Name = "lblLicenseInfo";
            this.lblLicenseInfo.Size = new System.Drawing.Size(141, 22);
            this.lblLicenseInfo.TabIndex = 6;
            this.lblLicenseInfo.Text = "Licencja dla:";
            // 
            // pictureBoxLogoOwner
            // 
            this.pictureBoxLogoOwner.Location = new System.Drawing.Point(332, 117);
            this.pictureBoxLogoOwner.Name = "pictureBoxLogoOwner";
            this.pictureBoxLogoOwner.Size = new System.Drawing.Size(150, 100);
            this.pictureBoxLogoOwner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxLogoOwner.TabIndex = 7;
            this.pictureBoxLogoOwner.TabStop = false;
            // 
            // lblLicenseOwner
            // 
            this.lblLicenseOwner.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblLicenseOwner.Location = new System.Drawing.Point(12, 150);
            this.lblLicenseOwner.Name = "lblLicenseOwner";
            this.lblLicenseOwner.Size = new System.Drawing.Size(314, 67);
            this.lblLicenseOwner.TabIndex = 8;
            this.lblLicenseOwner.Text = "lblLicenseOwner";
            // 
            // btnOK
            // 
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Location = new System.Drawing.Point(402, 236);
            this.btnOK.Margin = new System.Windows.Forms.Padding(1);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 25);
            this.btnOK.TabIndex = 22;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // labelLicenseOptions
            // 
            this.labelLicenseOptions.ForeColor = System.Drawing.Color.Red;
            this.labelLicenseOptions.Location = new System.Drawing.Point(13, 221);
            this.labelLicenseOptions.Name = "labelLicenseOptions";
            this.labelLicenseOptions.Size = new System.Drawing.Size(313, 40);
            this.labelLicenseOptions.TabIndex = 23;
            // 
            // FrmAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 271);
            this.Controls.Add(this.labelLicenseOptions);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblLicenseOwner);
            this.Controls.Add(this.pictureBoxLogoOwner);
            this.Controls.Add(this.lblLicenseInfo);
            this.Controls.Add(this.lblWWW);
            this.Controls.Add(this.lblAdres);
            this.Controls.Add(this.lblNazwa);
            this.Controls.Add(this.pictureBoxLogo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "FrmAbout";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ScanHelper: O programie";
            this.Load += new System.EventHandler(this.FrmAbout_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogoOwner)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Label lblNazwa;
        private System.Windows.Forms.Label lblAdres;
        private System.Windows.Forms.LinkLabel lblWWW;
        private System.Windows.Forms.Label lblLicenseInfo;
        private System.Windows.Forms.PictureBox pictureBoxLogoOwner;
        private System.Windows.Forms.Label lblLicenseOwner;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label labelLicenseOptions;
    }
}