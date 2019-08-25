using System;
using System.Windows.Forms;
using License;


namespace ScanHelper
{
    public partial class FormLicense : Form
    {
        public FormLicense()
        {
            InitializeComponent();
        }

        private void FormLicense_Load(object sender, EventArgs e)
        {
            Text = Application.ProductName + " - aktywacja programu";
            textBoxUid.Text = LicenseHandler.GenerateUid("ScanHelper");
        }

        private void LinkLabelCopy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.SetText(textBoxUid.Text);
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LinkLabelInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process
            {
                StartInfo = {FileName = "mailto:gisnet@gisnet.com.pl?subject=ScanHelper - aktywacja&body=ID komputera: " + textBoxUid.Text}
            };

            proc.Start();
        }

        private void LinkLabelSend_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process
            {
                StartInfo = {FileName = "mailto:gisnet@gisnet.com.pl?subject=ScanHelper - aktywacja&body=ID komputera: " + textBoxUid.Text}
            };
            
            proc.Start();
        }
    }
}
