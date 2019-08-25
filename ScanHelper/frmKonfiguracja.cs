using System;
using System.Data;
using System.Windows.Forms;

namespace ScanHelper
{
    public partial class FrmKonfiguracja : Form
    {
        public FrmKonfiguracja()
        {
            InitializeComponent();
        }

        private void FrmKonfiguracja_Load(object sender, EventArgs e)
        {
            Text = Application.ProductName + ": " + "Konfiguracja";
            groupBoxMain.Text = "Słownik rodzajów plików";
            btnApply.Text = "Zatwierdź";
            btnOK.Text = "OK";
            btnCancel.Text = "Anuluj";

            DataSet dsDictionary = new DataSet();
            DataTable dtDictionary = new DataTable("Dictionary");
            dtDictionary.Columns.Add("ID_RODZ_DOK", typeof(string));
            dtDictionary.Columns.Add("OPIS", typeof(string));
            dtDictionary.Columns.Add("PREFIX", typeof(string));
            dtDictionary.Columns.Add("SCAL", typeof(string));
            dsDictionary.Tables.Add(dtDictionary);

            dsDictionary.ReadXml("Dictionary.xml");

            dataGridDictionary.DataSource = dsDictionary.Tables["Dictionary"];

            dataGridDictionary.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            DataSet dsDictionary = new DataSet
            {
                DataSetName = "dsDictionary"
            };

            DataTable dtDictionary = new DataTable("Dictionary");
            dtDictionary.Columns.Add("ID_RODZ_DOK", typeof(string));
            dtDictionary.Columns.Add("OPIS", typeof(string));
            dtDictionary.Columns.Add("PREFIX", typeof(string));
            dtDictionary.Columns.Add("SCAL", typeof(string));
            dsDictionary.Tables.Add(dtDictionary);

            for (int i = 0; i < dataGridDictionary.Rows.Count - 1; i++)
            {
                DataRow drDictionary = dsDictionary.Tables["Dictionary"].NewRow();

                drDictionary["ID_RODZ_DOK"] = dataGridDictionary.Rows[i].Cells["ID_RODZ_DOK"].Value.ToString();
                drDictionary["OPIS"] = dataGridDictionary.Rows[i].Cells["OPIS"].Value.ToString();
                drDictionary["PREFIX"] = dataGridDictionary.Rows[i].Cells["PREFIX"].Value.ToString();
                drDictionary["SCAL"] = dataGridDictionary.Rows[i].Cells["SCAL"].Value.ToString();

                dsDictionary.Tables["Dictionary"].Rows.Add(drDictionary);
            }

            dsDictionary.Tables["Dictionary"].WriteXml("Dictionary.xml");
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            BtnApply_Click(sender, e);
            Close();
        }
    }
}
