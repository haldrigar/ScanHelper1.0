namespace ScanHelper
{
    partial class FormLicense
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLicense));
            this.textBoxUid = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.linkLabelCopy = new System.Windows.Forms.LinkLabel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.linkLabelSend = new System.Windows.Forms.LinkLabel();
            this.linkLabelInfo = new System.Windows.Forms.LinkLabel();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxUid
            // 
            this.textBoxUid.Location = new System.Drawing.Point(6, 19);
            this.textBoxUid.Name = "textBoxUid";
            this.textBoxUid.ReadOnly = true;
            this.textBoxUid.Size = new System.Drawing.Size(334, 20);
            this.textBoxUid.TabIndex = 0;
            this.textBoxUid.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxUid);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(346, 49);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ID komputera";
            // 
            // linkLabelCopy
            // 
            this.linkLabelCopy.AutoSize = true;
            this.linkLabelCopy.Location = new System.Drawing.Point(13, 69);
            this.linkLabelCopy.Name = "linkLabelCopy";
            this.linkLabelCopy.Size = new System.Drawing.Size(97, 13);
            this.linkLabelCopy.TabIndex = 2;
            this.linkLabelCopy.TabStop = true;
            this.linkLabelCopy.Text = "Kopiuj do schowka";
            this.linkLabelCopy.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelCopy_LinkClicked);
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOK.Location = new System.Drawing.Point(284, 182);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.ButtonOK_Click);
            // 
            // linkLabelSend
            // 
            this.linkLabelSend.AutoSize = true;
            this.linkLabelSend.Location = new System.Drawing.Point(298, 69);
            this.linkLabelSend.Name = "linkLabelSend";
            this.linkLabelSend.Size = new System.Drawing.Size(61, 13);
            this.linkLabelSend.TabIndex = 4;
            this.linkLabelSend.TabStop = true;
            this.linkLabelSend.Text = "Wyślij email";
            this.linkLabelSend.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelSend_LinkClicked);
            // 
            // linkLabelInfo
            // 
            this.linkLabelInfo.LinkArea = new System.Windows.Forms.LinkArea(95, 20);
            this.linkLabelInfo.Location = new System.Drawing.Point(13, 104);
            this.linkLabelInfo.Name = "linkLabelInfo";
            this.linkLabelInfo.Size = new System.Drawing.Size(346, 75);
            this.linkLabelInfo.TabIndex = 5;
            this.linkLabelInfo.TabStop = true;
            this.linkLabelInfo.Text = resources.GetString("linkLabelInfo.Text");
            this.linkLabelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabelInfo.UseCompatibleTextRendering = true;
            this.linkLabelInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelInfo_LinkClicked);
            // 
            // FormLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 216);
            this.Controls.Add(this.linkLabelInfo);
            this.Controls.Add(this.linkLabelSend);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.linkLabelCopy);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLicense";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Aktywacja proramu";
            this.Load += new System.EventHandler(this.FormLicense_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxUid;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel linkLabelCopy;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.LinkLabel linkLabelSend;
        private System.Windows.Forms.LinkLabel linkLabelInfo;
    }
}