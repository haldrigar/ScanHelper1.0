namespace LicenseGenerator
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.textBoxUid = new System.Windows.Forms.TextBox();
            this.buttonGeneruj = new System.Windows.Forms.Button();
            this.textBoxLicencja = new System.Windows.Forms.TextBox();
            this.groupBoxUstawieniaLicencji = new System.Windows.Forms.GroupBox();
            this.buttonReadLicense = new System.Windows.Forms.Button();
            this.groupBoxLogo = new System.Windows.Forms.GroupBox();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.groupBoxDataZakonczenia = new System.Windows.Forms.GroupBox();
            this.dateTimePickerKoniec = new System.Windows.Forms.DateTimePicker();
            this.groupBoxNumerLicencji = new System.Windows.Forms.GroupBox();
            this.textBoxLicenseNumber = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxOwner = new System.Windows.Forms.TextBox();
            this.groupBoxIdLicencji = new System.Windows.Forms.GroupBox();
            this.groupBoxTypLicencji = new System.Windows.Forms.GroupBox();
            this.radioButtonVolume = new System.Windows.Forms.RadioButton();
            this.radioButtonSingle = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBoxUstawieniaLicencji.SuspendLayout();
            this.groupBoxLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.groupBoxDataZakonczenia.SuspendLayout();
            this.groupBoxNumerLicencji.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBoxIdLicencji.SuspendLayout();
            this.groupBoxTypLicencji.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxUid
            // 
            this.textBoxUid.Location = new System.Drawing.Point(7, 19);
            this.textBoxUid.Name = "textBoxUid";
            this.textBoxUid.Size = new System.Drawing.Size(282, 20);
            this.textBoxUid.TabIndex = 0;
            // 
            // buttonGeneruj
            // 
            this.buttonGeneruj.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonGeneruj.Location = new System.Drawing.Point(188, 377);
            this.buttonGeneruj.Name = "buttonGeneruj";
            this.buttonGeneruj.Size = new System.Drawing.Size(108, 23);
            this.buttonGeneruj.TabIndex = 1;
            this.buttonGeneruj.Text = "Generuj licencję";
            this.buttonGeneruj.UseVisualStyleBackColor = true;
            this.buttonGeneruj.Click += new System.EventHandler(this.ButtonGeneruj_Click);
            // 
            // textBoxLicencja
            // 
            this.textBoxLicencja.Location = new System.Drawing.Point(6, 19);
            this.textBoxLicencja.Multiline = true;
            this.textBoxLicencja.Name = "textBoxLicencja";
            this.textBoxLicencja.Size = new System.Drawing.Size(369, 381);
            this.textBoxLicencja.TabIndex = 2;
            // 
            // groupBoxUstawieniaLicencji
            // 
            this.groupBoxUstawieniaLicencji.Controls.Add(this.buttonReadLicense);
            this.groupBoxUstawieniaLicencji.Controls.Add(this.groupBoxLogo);
            this.groupBoxUstawieniaLicencji.Controls.Add(this.groupBoxDataZakonczenia);
            this.groupBoxUstawieniaLicencji.Controls.Add(this.groupBoxNumerLicencji);
            this.groupBoxUstawieniaLicencji.Controls.Add(this.buttonGeneruj);
            this.groupBoxUstawieniaLicencji.Controls.Add(this.groupBox2);
            this.groupBoxUstawieniaLicencji.Controls.Add(this.groupBoxIdLicencji);
            this.groupBoxUstawieniaLicencji.Controls.Add(this.groupBoxTypLicencji);
            this.groupBoxUstawieniaLicencji.Location = new System.Drawing.Point(13, 13);
            this.groupBoxUstawieniaLicencji.Name = "groupBoxUstawieniaLicencji";
            this.groupBoxUstawieniaLicencji.Size = new System.Drawing.Size(314, 413);
            this.groupBoxUstawieniaLicencji.TabIndex = 3;
            this.groupBoxUstawieniaLicencji.TabStop = false;
            this.groupBoxUstawieniaLicencji.Text = "Ustawienia licencji";
            // 
            // buttonReadLicense
            // 
            this.buttonReadLicense.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonReadLicense.Location = new System.Drawing.Point(188, 348);
            this.buttonReadLicense.Name = "buttonReadLicense";
            this.buttonReadLicense.Size = new System.Drawing.Size(108, 23);
            this.buttonReadLicense.TabIndex = 3;
            this.buttonReadLicense.Text = "Odczytaj licencję";
            this.buttonReadLicense.UseVisualStyleBackColor = true;
            this.buttonReadLicense.Click += new System.EventHandler(this.ButtonReadLicense_Click);
            // 
            // groupBoxLogo
            // 
            this.groupBoxLogo.Controls.Add(this.pictureBoxLogo);
            this.groupBoxLogo.Location = new System.Drawing.Point(7, 306);
            this.groupBoxLogo.Name = "groupBoxLogo";
            this.groupBoxLogo.Size = new System.Drawing.Size(151, 100);
            this.groupBoxLogo.TabIndex = 5;
            this.groupBoxLogo.TabStop = false;
            this.groupBoxLogo.Text = "Logo";
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxLogo.Location = new System.Drawing.Point(7, 20);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(132, 74);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxLogo.TabIndex = 0;
            this.pictureBoxLogo.TabStop = false;
            this.pictureBoxLogo.Click += new System.EventHandler(this.PictureBoxLogo_Click);
            // 
            // groupBoxDataZakonczenia
            // 
            this.groupBoxDataZakonczenia.Controls.Add(this.dateTimePickerKoniec);
            this.groupBoxDataZakonczenia.Location = new System.Drawing.Point(161, 145);
            this.groupBoxDataZakonczenia.Name = "groupBoxDataZakonczenia";
            this.groupBoxDataZakonczenia.Size = new System.Drawing.Size(140, 52);
            this.groupBoxDataZakonczenia.TabIndex = 4;
            this.groupBoxDataZakonczenia.TabStop = false;
            this.groupBoxDataZakonczenia.Text = "Data zakończenia";
            // 
            // dateTimePickerKoniec
            // 
            this.dateTimePickerKoniec.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerKoniec.Location = new System.Drawing.Point(6, 19);
            this.dateTimePickerKoniec.MinDate = new System.DateTime(2019, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerKoniec.Name = "dateTimePickerKoniec";
            this.dateTimePickerKoniec.Size = new System.Drawing.Size(129, 20);
            this.dateTimePickerKoniec.TabIndex = 0;
            // 
            // groupBoxNumerLicencji
            // 
            this.groupBoxNumerLicencji.Controls.Add(this.textBoxLicenseNumber);
            this.groupBoxNumerLicencji.Location = new System.Drawing.Point(6, 145);
            this.groupBoxNumerLicencji.Name = "groupBoxNumerLicencji";
            this.groupBoxNumerLicencji.Size = new System.Drawing.Size(140, 52);
            this.groupBoxNumerLicencji.TabIndex = 3;
            this.groupBoxNumerLicencji.TabStop = false;
            this.groupBoxNumerLicencji.Text = "Numer licencji";
            // 
            // textBoxLicenseNumber
            // 
            this.textBoxLicenseNumber.Location = new System.Drawing.Point(7, 20);
            this.textBoxLicenseNumber.Name = "textBoxLicenseNumber";
            this.textBoxLicenseNumber.Size = new System.Drawing.Size(127, 20);
            this.textBoxLicenseNumber.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxOwner);
            this.groupBox2.Location = new System.Drawing.Point(7, 203);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(295, 96);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Właściciel Licencji";
            // 
            // textBoxOwner
            // 
            this.textBoxOwner.Location = new System.Drawing.Point(7, 20);
            this.textBoxOwner.Multiline = true;
            this.textBoxOwner.Name = "textBoxOwner";
            this.textBoxOwner.Size = new System.Drawing.Size(282, 66);
            this.textBoxOwner.TabIndex = 0;
            // 
            // groupBoxIdLicencji
            // 
            this.groupBoxIdLicencji.Controls.Add(this.textBoxUid);
            this.groupBoxIdLicencji.Location = new System.Drawing.Point(7, 83);
            this.groupBoxIdLicencji.Name = "groupBoxIdLicencji";
            this.groupBoxIdLicencji.Size = new System.Drawing.Size(295, 56);
            this.groupBoxIdLicencji.TabIndex = 1;
            this.groupBoxIdLicencji.TabStop = false;
            this.groupBoxIdLicencji.Text = "ID komputera";
            // 
            // groupBoxTypLicencji
            // 
            this.groupBoxTypLicencji.Controls.Add(this.radioButtonVolume);
            this.groupBoxTypLicencji.Controls.Add(this.radioButtonSingle);
            this.groupBoxTypLicencji.Location = new System.Drawing.Point(7, 20);
            this.groupBoxTypLicencji.Name = "groupBoxTypLicencji";
            this.groupBoxTypLicencji.Size = new System.Drawing.Size(295, 56);
            this.groupBoxTypLicencji.TabIndex = 0;
            this.groupBoxTypLicencji.TabStop = false;
            this.groupBoxTypLicencji.Text = "Typ licencji";
            // 
            // radioButtonVolume
            // 
            this.radioButtonVolume.AutoSize = true;
            this.radioButtonVolume.Location = new System.Drawing.Point(154, 20);
            this.radioButtonVolume.Name = "radioButtonVolume";
            this.radioButtonVolume.Size = new System.Drawing.Size(111, 17);
            this.radioButtonVolume.TabIndex = 1;
            this.radioButtonVolume.Text = "Grupowa Licencja";
            this.radioButtonVolume.UseVisualStyleBackColor = true;
            this.radioButtonVolume.CheckedChanged += new System.EventHandler(this.RadioButtonVolume_CheckedChanged);
            // 
            // radioButtonSingle
            // 
            this.radioButtonSingle.AutoSize = true;
            this.radioButtonSingle.Checked = true;
            this.radioButtonSingle.Location = new System.Drawing.Point(7, 20);
            this.radioButtonSingle.Name = "radioButtonSingle";
            this.radioButtonSingle.Size = new System.Drawing.Size(123, 17);
            this.radioButtonSingle.TabIndex = 0;
            this.radioButtonSingle.TabStop = true;
            this.radioButtonSingle.Text = "Pojedyńcza Licencja";
            this.radioButtonSingle.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxLicencja);
            this.groupBox1.Location = new System.Drawing.Point(333, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(386, 413);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Klucz licencji";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 435);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxUstawieniaLicencji);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "Generowanie licencji";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.groupBoxUstawieniaLicencji.ResumeLayout(false);
            this.groupBoxLogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.groupBoxDataZakonczenia.ResumeLayout(false);
            this.groupBoxNumerLicencji.ResumeLayout(false);
            this.groupBoxNumerLicencji.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBoxIdLicencji.ResumeLayout(false);
            this.groupBoxIdLicencji.PerformLayout();
            this.groupBoxTypLicencji.ResumeLayout(false);
            this.groupBoxTypLicencji.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxUid;
        private System.Windows.Forms.Button buttonGeneruj;
        private System.Windows.Forms.TextBox textBoxLicencja;
        private System.Windows.Forms.GroupBox groupBoxUstawieniaLicencji;
        private System.Windows.Forms.GroupBox groupBoxTypLicencji;
        private System.Windows.Forms.RadioButton radioButtonVolume;
        private System.Windows.Forms.RadioButton radioButtonSingle;
        private System.Windows.Forms.GroupBox groupBoxIdLicencji;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxOwner;
        private System.Windows.Forms.Button buttonReadLicense;
        private System.Windows.Forms.GroupBox groupBoxNumerLicencji;
        private System.Windows.Forms.TextBox textBoxLicenseNumber;
        private System.Windows.Forms.GroupBox groupBoxDataZakonczenia;
        private System.Windows.Forms.DateTimePicker dateTimePickerKoniec;
        private System.Windows.Forms.GroupBox groupBoxLogo;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
    }
}

