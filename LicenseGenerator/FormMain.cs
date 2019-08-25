using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Security;
using System.Windows.Forms;
using License;

namespace LicenseGenerator
{
    public partial class FormMain : Form
    {
        private byte[] _certPrivateKeyData;
        private byte[] _certPublicKeyData;

        private readonly SecureString _certPwd = new SecureString();

        private string _logoPath = string.Empty;

        public FormMain()
        {
            InitializeComponent();

            _certPwd.AppendChar('g');
            _certPwd.AppendChar('r');
            _certPwd.AppendChar('z');
            _certPwd.AppendChar('e');
            _certPwd.AppendChar('g');
            _certPwd.AppendChar('o');
            _certPwd.AppendChar('r');
            _certPwd.AppendChar('z');
            _certPwd.AppendChar('g');
            _certPwd.AppendChar('i');
            _certPwd.AppendChar('s');
            _certPwd.AppendChar('n');
            _certPwd.AppendChar('e');
            _certPwd.AppendChar('t');
            _certPwd.AppendChar('g');
            _certPwd.AppendChar('d');
            _certPwd.AppendChar('a');
            _certPwd.AppendChar('n');
            _certPwd.AppendChar('s');
            _certPwd.AppendChar('k');
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            using (MemoryStream memoryStream = new MemoryStream())
            {
                assembly.GetManifestResourceStream("LicenseGenerator.LicenseSign.pfx")?.CopyTo(memoryStream);

                _certPrivateKeyData = memoryStream.ToArray();
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                assembly.GetManifestResourceStream("LicenseGenerator.LicenseVerify.cer")?.CopyTo(memoryStream);

                _certPublicKeyData = memoryStream.ToArray();
            }
        }

        private void ButtonGeneruj_Click(object sender, EventArgs e)
        {
            MyLicense licencja = new MyLicense
            {
                LicenseOwner = textBoxOwner.Text,
                CreateDateTime = DateTime.Now,
                LicenseNumber = textBoxLicenseNumber.Text,
                LicenseEnd = new DateTime(dateTimePickerKoniec.Value.Year, dateTimePickerKoniec.Value.Month, dateTimePickerKoniec.Value.Day, 23, 59, 59)
            };

            using (MemoryStream ms = new MemoryStream())
            using (Bitmap bitmap = new Bitmap(_logoPath))
            {
                bitmap.Save(ms, ImageFormat.Png);
               
                licencja.LogoOwner = Convert.ToBase64String(ms.GetBuffer()); 
            }

            if (radioButtonSingle.Checked)
            {
                if (LicenseHandler.ValidateUidFormat(textBoxUid.Text.Trim()))
                {
                    licencja.Type = LicenseTypes.Single;
                    licencja.Uid = textBoxUid.Text.Trim();
                }
                else
                {
                    MessageBox.Show(@"Błędny lub pusty identyfikator licencji", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

            } else if (radioButtonVolume.Checked)
            {
                licencja.Type = LicenseTypes.Volume;
                licencja.Uid = string.Empty;
            }

            textBoxLicencja.Text = LicenseHandler.GenerateLicense(licencja, _certPrivateKeyData, _certPwd);

        }

        private void RadioButtonVolume_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton) sender;

            textBoxUid.Enabled = !radioButton.Checked;
        }

        private void ButtonReadLicense_Click(object sender, EventArgs e)
        {
            MyLicense license = (MyLicense)LicenseHandler.ReadLicense(typeof(MyLicense), textBoxLicencja.Text, _certPublicKeyData, out LicenseStatus status, out string msg);

            switch (status)
            {
                case LicenseStatus.Undefined:

                    MessageBox.Show(@"By używać tej aplikacji musisz posiadać ważną licencję", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case LicenseStatus.Invalid:
                case LicenseStatus.Cracked:
                

                    MessageBox.Show(msg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case LicenseStatus.Valid:
                case LicenseStatus.Expired:

                    textBoxUid.Text = license.Uid;
                    textBoxLicenseNumber.Text = license.LicenseNumber;
                    textBoxOwner.Text = license.LicenseOwner.Replace("\n", Environment.NewLine);
                    dateTimePickerKoniec.Value = license.LicenseEnd;

                    using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(license.LogoOwner)))
                    {
                        Bitmap bm = new Bitmap(ms);

                        pictureBoxLogo.Image = bm;
                    }

                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void PictureBoxLogo_Click(object sender, EventArgs e)
        {

            using (OpenFileDialog dlg = new OpenFileDialog {Multiselect = false, Filter = @"Pliki PNG (*.png)|*.png"})
            {
                if (dlg.ShowDialog() != DialogResult.OK) return;

                _logoPath = dlg.FileName;

                pictureBoxLogo.Image = Image.FromFile(_logoPath);
            }
        }
    }
}
