using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using License;

namespace ScanHelper
{
    public partial class FrmAbout : Form
    {
        private readonly MyLicense _license;

        public FrmAbout(MyLicense license)
        {
            InitializeComponent();

            _license = license;
        }

        private void FrmAbout_Load(object sender, EventArgs e)
        {
            pictureBoxLogoOwner.Image = Resources.logo;
            lblLicenseOwner.Text = _license.LicenseOwner;
            labelLicenseOptions.Text = $"Rodzaj licencji: {_license.Type}, Licencja ważna do: {_license.LicenseEnd}";

            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(_license.LogoOwner)))
            {
                Bitmap bm = new Bitmap(ms);

                pictureBoxLogoOwner.Image = bm;
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LblWWW_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://gisnet.com.pl");
        }
    }
}
