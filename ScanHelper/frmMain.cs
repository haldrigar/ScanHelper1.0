using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using License;
using PdfSharp.Pdf.IO;
using ScanHelper.Functions;
using Spire.Pdf;

//todo Przy wyborze rodzaju wyświetlanego pliku możnabyłoby ustawić tak, że klikając lewym przyciskiem myszy mamy nazwany dokument, który należy tylko do jednej grupy dokumentów, a klikając prawym przyciskiem ten dokument mógłby się kopiować, żeby można było go przypisać do drugiej grupy dokumentów

namespace ScanHelper
{
    public partial class FrmMain : Form
    {
        private readonly DataSet _dsPdfFiles = new DataSet();
        private readonly DataSet _dsDictionary = new DataSet();

        private int _idActivePdf;
        private int _filesCounter;
        private int _filesSkipped;

        private bool _autoZnak;
        private string _powiat;

        private readonly int[] _btnDictionary = new int[100];

        byte[] _certPublicKeyData;

        private MyLicense _license;

        private int _zoom = 100;

        public FrmMain()
        {
            InitializeComponent();

            InitializeCustom();
        }

        private void InitializeCustom()
        {
            Icon = Resources.ScanHelper;

            btnOpenDirectory.Text = @"Wskaż folder";
            btnOpenFiles.Text = @"Wskaż pliki";

            btnBack.Text = @"Cofnij";
            btnRotate.Text = @"Obróć";
            btnSkip.Text = @"Pomiń";
            btnScalAuto.Text = @"Scal pliki";
            btnZnakWodny.Text = @"Znak wodny";

            statusStripMainLabel.Text = @"Aktualny plik PDF: 0/0";

            _powiat = Functions.IniParser.ReadIni("powiat", "nazwa");
            textBoxOperat.Text = Functions.IniParser.ReadIni("Operat", "RecentOperat");

            if (string.IsNullOrEmpty(Functions.IniParser.ReadIni("settings", "autoznak")))
            {
                Functions.IniParser.SaveIni("settings", "autoznak", checkBoxZnakWodny.Checked.ToString());
                _autoZnak = false;
                checkBoxZnakWodny.Checked = false;
                checkBoxZnakWodny.Enabled = false;
            }
            else
            {
                _autoZnak = Convert.ToBoolean(Functions.IniParser.ReadIni("settings", "autoznak"));
                checkBoxZnakWodny.Checked = _autoZnak;
                checkBoxZnakWodny.Enabled = false;
            }

            btnZnakWodny.Enabled = false;

            // ustawienie atrybutów początkowych dla przycisków wyboru rodzaju pliku
            foreach (Button btn in groupBoxButtons.Controls.OfType<Button>())
            {
                btn.Enabled = false;
                btn.Text = @"brak";
            }

            pdfDocumentViewer.HorizontalScroll.Visible = false;
            pdfDocumentViewer.VerticalScroll.Visible = false;

            pdfDocumentViewer.HorizontalScroll.Enabled = false;
            pdfDocumentViewer.VerticalScroll.Enabled = false;

            pdfDocumentViewer.MouseWheel += PdfDocumentViewerOnMouseWheel;

            // dodanie tabeli z listą plików do przetworzenia
            _dsPdfFiles.DataSetName = "dsPDFFiles";

            DataTable pdfFiles = new DataTable("PDFFiles");
            pdfFiles.Columns.Add("Id", typeof(int));
            pdfFiles.Columns.Add("PathAndFileName", typeof(string));
            pdfFiles.Columns.Add("FileName", typeof(string));
            pdfFiles.Columns.Add("Path", typeof(string));
            pdfFiles.Columns.Add("FileNameNew", typeof(string));
            pdfFiles.Columns.Add("Prefix", typeof(string));
            pdfFiles.Columns.Add("PrefixCode", typeof(int));
            _dsPdfFiles.Tables.Add(pdfFiles);

            // dodanie tabeli z konfiguracją słowników i ilością plików danego rodzaju
            DataTable dtDictionary = new DataTable("Dictionary");
            dtDictionary.Columns.Add("ID_RODZ_DOK", typeof(string));
            dtDictionary.Columns.Add("OPIS", typeof(string));
            dtDictionary.Columns.Add("PREFIX", typeof(string));
            dtDictionary.Columns.Add("SCAL", typeof(string));
            _dsDictionary.Tables.Add(dtDictionary);

            // wczytanie słownika rodzajów plików i utworzenie na podstawie niego przycisków
            _dsDictionary.ReadXml(@"Dictionary.xml");

            for (int buttonIndex = 1; buttonIndex <= _dsDictionary.Tables["Dictionary"].Rows.Count; buttonIndex++)
            {
                groupBoxButtons.Controls["btnDictionary" + buttonIndex].Text = _dsDictionary.Tables["Dictionary"].Rows[buttonIndex - 1]["OPIS"].ToString();
            }

            // ustawieie liczników dokumentów
            for (int i = 0; i < _btnDictionary.Length; i++) _btnDictionary[i] = 0;
        }

        // funkcja ładowania głównego okna aplikacji
        private void FrmMain_Load(object sender, EventArgs e)
        {
            string msg = string.Empty;
            LicenseStatus status = LicenseStatus.Undefined;

            Assembly assembly = Assembly.GetExecutingAssembly();

            using (MemoryStream memoryStream = new MemoryStream())
            {
                assembly.GetManifestResourceStream("ScanHelper.LicenseVerify.cer")?.CopyTo(memoryStream);

                _certPublicKeyData = memoryStream.ToArray();
            }

            if (File.Exists("license.lic"))
            {
                _license = (MyLicense)LicenseHandler.ReadLicense(typeof(MyLicense), File.ReadAllText("license.lic"), _certPublicKeyData, out status, out msg);
            }
            else
            {
                FormLicense frm = new FormLicense();
                frm.ShowDialog(this);
            }

            switch (status)
            {
                case LicenseStatus.Undefined:

                    MessageBox.Show(@"By używać tej aplikacji musisz posiadać aktualną licencję", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                    break;

                case LicenseStatus.Invalid:
                case LicenseStatus.Cracked:
                case LicenseStatus.Expired:

                    MessageBox.Show(msg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                    break;

                case LicenseStatus.Valid:

                    string licenseOwner = _license?.LicenseOwner;

                    statusStripLicense.Text = $"Licencja: {_license?.Type}. Licencja ważna do: {_license?.LicenseEnd}";

                    Text = Application.ProductName + ' ' + Application.ProductVersion + @" - " + licenseOwner?.Split('\n').First();

                    Location = new Point(Convert.ToInt32(Functions.IniParser.ReadIni("FormMain", "X")), Convert.ToInt32(Functions.IniParser.ReadIni("FormMain", "Y")));

                    pdfDocumentViewer.LoadFromFile(AppDomain.CurrentDomain.BaseDirectory + "ScanHelper.pdf");

                    _zoom = GetZoom(AppDomain.CurrentDomain.BaseDirectory + "ScanHelper.pdf");
                    pdfDocumentViewer.ZoomTo(_zoom);

                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void PdfDocumentViewerOnMouseWheel(object sender, MouseEventArgs e)
        {
            int wheelValue = e.Delta / 10;

            _zoom += wheelValue;

            if (_zoom < 0) _zoom = 0;

            pdfDocumentViewer.ZoomTo(_zoom);
        }

        // funkcja wywołująca okno z konfiguracją
        private void MnuMainKonfiguracja_Click(object sender, EventArgs e)
        {
            // wywołanie okna z konfiguracją programu
            using (FrmKonfiguracja frm = new FrmKonfiguracja())
            {
                frm.ShowDialog(this);
            }
        }

        // funkjca wyboru plików pdf z dysku
        private void BtnOpenPDF_Click(object sender, EventArgs e)
        {
            string folderName = string.Empty;
            string[] fileNames = { };

            DialogResult result;

            string buttonName = ((Button) sender).Name;

            switch (buttonName)
            {
                case "btnOpenFiles":

                    OpenFileDialog ofDialog = new OpenFileDialog
                    {
                        Filter = "PDF (*.pdf)|*.pdf",
                        Multiselect = true,
                        InitialDirectory = Functions.IniParser.ReadIni("Files", "LastDirectory")
                    };

                    result = ofDialog.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        folderName = Path.GetDirectoryName(ofDialog.FileName);
                        fileNames = ofDialog.FileNames;
                        Array.Sort(fileNames, new NaturalStringComparer());

                        Functions.IniParser.SaveIni("Files", "LastDirectory", folderName);
                    }
                    else return;

                    break;

                case "btnOpenDirectory":
                
                    FolderBrowserDialog fbdOpen = new FolderBrowserDialog
                    {
                        ShowNewFolderButton = false,
                        SelectedPath = Functions.IniParser.ReadIni("Files", "LastDirectory")
                    };

                    result = fbdOpen.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        folderName = fbdOpen.SelectedPath;
                        fileNames = Directory.GetFiles(folderName, "*.pdf",SearchOption.TopDirectoryOnly);
                        Array.Sort(fileNames, new NaturalStringComparer());

                        Functions.IniParser.SaveIni("Files", "LastDirectory", folderName);

                        if (fileNames.Length == 0)  
                        {
                            pdfDocumentViewer.LoadFromFile(AppDomain.CurrentDomain.BaseDirectory + "ScanHelper.pdf");

                            _zoom = GetZoom(AppDomain.CurrentDomain.BaseDirectory + "ScanHelper.pdf");

                            pdfDocumentViewer.ZoomTo(_zoom);

                            return;
                        }

                    } else return;

                    break;
            }

            listBoxFiles.Items.Clear();
            
            _dsPdfFiles.Tables["PDFFiles"].Clear();
            _filesCounter = 0;
            _filesSkipped = 0;

            int id = 0;
            foreach (string pathAndFileName in fileNames)
            {
                DataRow row = _dsPdfFiles.Tables["PDFFiles"].NewRow();
                row["Id"] = id++;
                row["PathAndFileName"] = pathAndFileName;
                row["FileName"] = pathAndFileName.Substring(pathAndFileName.LastIndexOf('\\') + 1);
                row["Path"] = pathAndFileName.Substring(0, pathAndFileName.LastIndexOf('\\'));
                _dsPdfFiles.Tables["PDFFiles"].Rows.Add(row);

                listBoxFiles.Items.Add(row["FileName"]);
            }

            _dsPdfFiles.Tables["PDFFiles"].WriteXml("PDFFiles.xml");

            // wyświetl pierwszy z plików z listy wskazanych
            _idActivePdf = 0;

            DataRow[] rPdfFiles = _dsPdfFiles.Tables["PDFFiles"].Select("Id = '" + _idActivePdf + "'");

            //iTextSharp.text.Rectangle pageSize = null;
            //byte[] pdData;

            //using (var srcImage = new Bitmap(rPdfFiles[0]["PathAndFileName"].ToString()))
            //{
            //    pageSize = new iTextSharp.text.Rectangle(0, 0, srcImage.Width, srcImage.Height);
            //}

            //using (var ms = new MemoryStream())
            //{
            //    var document = new Document(pageSize, 0, 0, 0, 0);
            //    PdfWriter.GetInstance(document, ms).SetFullCompression();

            //    document.Open();
            //    var image = iTextSharp.text.Image.GetInstance(rPdfFiles[0]["PathAndFileName"].ToString());
            //    document.Add(image);
            //    document.Close();

            //    pdData = ms.ToArray();
            //}

            //pdfDocumentViewer.LoadFromStream(new MemoryStream(pdData));

            pdfDocumentViewer.LoadFromFile(rPdfFiles[0]["PathAndFileName"].ToString());

            _zoom = GetZoom(rPdfFiles[0]["PathAndFileName"].ToString());

            pdfDocumentViewer.ZoomTo(_zoom);

            pdfDocumentViewer.EnableHandTool();

            listBoxFiles.SetSelected(_idActivePdf, true);

            long fileSize = new FileInfo(rPdfFiles[0]["PathAndFileName"].ToString()).Length / 1024;

            statusStripMainLabel.Text = $@"Aktualny plik PDF: {(Convert.ToInt16(rPdfFiles[0]["Id"]) + 1)}/{_dsPdfFiles.Tables["PDFFiles"].Rows.Count} - {rPdfFiles[0]["PathAndFileName"]} [{fileSize} KB]";

            // uaktywnij przyciski, które mają wartości
            foreach (Button btn in groupBoxButtons.Controls.OfType<Button>())
            {
                if (btn.Text != @"brak") btn.Enabled = true;
            }

            for (int i = 0; i < _btnDictionary.Length; i++) _btnDictionary[i] = 0;

            textBoxOperat.Text = folderName?.Split(Path.DirectorySeparatorChar).Last();

            btnZnakWodny.Enabled = true;

        }

        private void BtnDictionary_Click(object sender, EventArgs e)
        {

            if (_filesCounter == _dsPdfFiles.Tables["PDFFiles"].Rows.Count)
            {
                MessageBox.Show(@"Wszystkie dokumenty zindeksowane", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (listBoxFiles.SelectedIndex != _filesCounter + _filesSkipped)
            {
                MessageBox.Show(@"Wybierz pierwszy niezindeksowany plik na liście", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string opis = ((Button)sender).Text;

            DataRow[] rDictionary = _dsDictionary.Tables["Dictionary"].Select("OPIS = '" + opis + "'");

            string prefix = rDictionary[0]["PREFIX"].ToString();

            // pobierz nazwę aktualnie wyświetlanego pliku
            DataRow[] rPdfFiles = _dsPdfFiles.Tables["PDFFiles"].Select("Id = '" + _idActivePdf + "'");

            string pathAndFileName = rPdfFiles[0]["PathAndFileName"].ToString();
            string path = rPdfFiles[0]["Path"].ToString();

            // licznik plików danego typu
            int btnDictionaryCounter = Int16.Parse(((Button)sender).Name.Replace("btnDictionary", ""));
            _btnDictionary[btnDictionaryCounter] = _btnDictionary[btnDictionaryCounter] + 1;

            _filesCounter++;

            string filesCounter = _filesCounter.ToString().PadLeft(3, '0');

            string fileNamenew;

            switch (_powiat)
            {
                case "gdansk":
                    fileNamenew = textBoxOperat.Text + "_" + filesCounter + prefix.Replace(".", string.Empty) + "_" + _btnDictionary[btnDictionaryCounter] + ".pdf";
                    break;

                case "kwidzyn":
                    fileNamenew = filesCounter + "_" + textBoxOperat.Text + "_" + _btnDictionary[btnDictionaryCounter] + prefix + "pdf";
                    break;

                case "kartuzy":
                    fileNamenew = textBoxOperat.Text + "_" + filesCounter + prefix  + "pdf";
                    break;

                default:
                    throw new Exception("Nieznany powiat!");
            }

            rPdfFiles[0]["FileNameNew"] = fileNamenew;
            rPdfFiles[0]["Prefix"] = prefix;
            rPdfFiles[0]["PrefixCode"] = btnDictionaryCounter;

            listBoxFiles.Items[_idActivePdf] = "OK -> " + fileNamenew;

            // -------------------------------------------------------------
            //  Utwórz katalog wynikowy jeśli go nie było
            // -------------------------------------------------------------
            DirectoryInfo dir = new DirectoryInfo(path + "\\" + textBoxOperat.Text);

            if (!dir.Exists)
            {
                Directory.CreateDirectory(path + "\\" + textBoxOperat.Text);
            }
            // -------------------------------------------------------------

            // skopiuj plik pod nową nazwą do katalogu wynikowego
            File.Copy(pathAndFileName, path + "\\" + textBoxOperat.Text + "\\" + fileNamenew);

            if (_autoZnak)
            {
                SetZnakWodny(path + "\\" + textBoxOperat.Text + "\\" + fileNamenew);
            }

            // -----------------------------
            // załaduj następny plik do okna
            if (++_idActivePdf < _dsPdfFiles.Tables["PDFFiles"].Rows.Count)
            {
                rPdfFiles = _dsPdfFiles.Tables["PDFFiles"].Select("Id = '" + _idActivePdf + "'");

                pdfDocumentViewer.LoadFromFile(rPdfFiles[0]["PathAndFileName"].ToString());

                _zoom = GetZoom(rPdfFiles[0]["PathAndFileName"].ToString());

                pdfDocumentViewer.ZoomTo(_zoom);

                pdfDocumentViewer.EnableHandTool();

                listBoxFiles.SetSelected(_idActivePdf, true);

                long fileSize = new FileInfo(rPdfFiles[0]["PathAndFileName"].ToString()).Length / 1024;

                statusStripMainLabel.Text = $"Aktualny plik PDF: {(Convert.ToInt16(rPdfFiles[0]["Id"]) + 1)}/{_dsPdfFiles.Tables["PDFFiles"].Rows.Count} - {rPdfFiles[0]["PathAndFileName"]} [{fileSize} KB]";
            }
            else
            {
                MessageBox.Show("Wszystkie dokumenty zindeksowane", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // załaduj następny plik do okna
            // -----------------------------
        }

        private void MnuMainOProgramie_Click(object sender, EventArgs e)
        {
            using (FrmAbout frm = new FrmAbout(_license))
            {
                frm.ShowDialog();
            }
        }

        private void MnuMainExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            if (listBoxFiles.SelectedIndex != _filesCounter + _filesSkipped)
            {
                MessageBox.Show(@"Wybierz pierwszy niezindeksowany plik na liście", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_idActivePdf > 0)
            {
                --_idActivePdf;
                

                DataRow[] rPdfFiles = _dsPdfFiles.Tables["PDFFiles"].Select("Id = '" + _idActivePdf + "'");

                string path = rPdfFiles[0]["Path"].ToString();
                string fileNameNew = rPdfFiles[0]["FileNameNew"].ToString();
                int prefixCode = (int)rPdfFiles[0]["PrefixCode"];

                if (prefixCode != 99) // jeśli był SKIP to nie odejmuj
                {
                    --_filesCounter;
                }
                else
                {
                    --_filesSkipped;
                }

                _btnDictionary[prefixCode] = _btnDictionary[prefixCode] - 1;

                File.Delete(path + "\\" + textBoxOperat.Text + "\\" + fileNameNew);

                pdfDocumentViewer.LoadFromFile(rPdfFiles[0]["PathAndFileName"].ToString());

                _zoom = GetZoom(rPdfFiles[0]["PathAndFileName"].ToString());

                pdfDocumentViewer.ZoomTo(_zoom);

                pdfDocumentViewer.EnableHandTool();

                listBoxFiles.SetSelected(_idActivePdf, true);
                listBoxFiles.Items[_idActivePdf] = rPdfFiles[0]["FileName"];

                long fileSize = new FileInfo(rPdfFiles[0]["PathAndFileName"].ToString()).Length / 1024;

                statusStripMainLabel.Text = $"Aktualny plik PDF: {(Convert.ToInt16(rPdfFiles[0]["Id"]) + 1)}/{_dsPdfFiles.Tables["PDFFiles"].Rows.Count} - {rPdfFiles[0]["PathAndFileName"]} [{fileSize} KB]";
            }

        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Functions.IniParser.SaveIni("Operat", "RecentOperat", textBoxOperat.Text);
            Functions.IniParser.SaveIni("FormMain", "X", Location.X.ToString());
            Functions.IniParser.SaveIni("FormMain", "Y", Location.Y.ToString());
        }

        private void ListBoxFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox a = (ListBox) sender;

            if (a.SelectedIndex >= 0)
            {
                _idActivePdf = a.SelectedIndex;

                DataRow[] rPdfFiles = _dsPdfFiles.Tables["PDFFiles"].Select("Id = '" + _idActivePdf + "'");

                pdfDocumentViewer.LoadFromFile(rPdfFiles[0]["PathAndFileName"].ToString());

                _zoom = GetZoom(rPdfFiles[0]["PathAndFileName"].ToString());

                pdfDocumentViewer.ZoomTo(_zoom);

                pdfDocumentViewer.EnableHandTool();

                long fileSize = new FileInfo(rPdfFiles[0]["PathAndFileName"].ToString()).Length / 1024;

                statusStripMainLabel.Text = $"Aktualny plik PDF: {(Convert.ToInt16(rPdfFiles[0]["Id"]) + 1)}/{_dsPdfFiles.Tables["PDFFiles"].Rows.Count} - {rPdfFiles[0]["PathAndFileName"]} [{fileSize} KB]";
            }

        }

        // obsługa przycisków CTRL + Lewy lub Prawy
        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right))
            {
                BtnRotate_ClickOrKeyPress(sender, e);
                e.Handled = true;
            }
        }

        // blokada klawiszy strzałek w lewo i prawo dla listy plików, by można było obsłużyć CTRL + strzałki
        private void ListBoxFiles_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Right || e.KeyCode == Keys.Left) e.Handled = true;
        }

        private void BtnRotate_ClickOrKeyPress(object sender, EventArgs e)
        {
            DataRow[] rPdfFiles = _dsPdfFiles.Tables["PDFFiles"].Select("Id = '" + _idActivePdf + "'");

            Spire.Pdf.PdfDocument doc = new Spire.Pdf.PdfDocument();

            doc.LoadFromFile(rPdfFiles[0]["PathAndFileName"].ToString());

            PdfPageBase page = doc.Pages[0];

            int rotation = (int)page.Rotation;

            // jeśli obracanie zostało wywołane klawiszem
            if (e.GetType() == typeof(KeyEventArgs))
            {
                KeyEventArgs arg = (KeyEventArgs)e;

                rotation += arg.KeyData == (Keys.Control | Keys.Right) ? (int)PdfPageRotateAngle.RotateAngle90 : (int)PdfPageRotateAngle.RotateAngle270;
            }

            // jeśli obracanie zostało wywołane myszką
            if (e.GetType() == typeof(MouseEventArgs))
            {
                MouseEventArgs arg = (MouseEventArgs)e;

                if (arg.Button == MouseButtons.Left)
                {
                    rotation += (int)PdfPageRotateAngle.RotateAngle270;
                }
                else if (arg.Button == MouseButtons.Right)
                {
                    rotation += (int)PdfPageRotateAngle.RotateAngle90;
                }
            }

            page.Rotation = (PdfPageRotateAngle)rotation;

            doc.SaveToFile(rPdfFiles[0]["PathAndFileName"].ToString());

            pdfDocumentViewer.LoadFromFile(rPdfFiles[0]["PathAndFileName"].ToString());

            _zoom = GetZoom(rPdfFiles[0]["PathAndFileName"].ToString());

            pdfDocumentViewer.ZoomTo(_zoom);

            pdfDocumentViewer.EnableHandTool();
        }

        private void BtnSkip_Click(object sender, EventArgs e)
        {
            if (listBoxFiles.SelectedIndex != _filesCounter + _filesSkipped)
            {
                MessageBox.Show(@"Wybierz pierwszy niezindeksowany plik na liście", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            _filesSkipped++;

            DataRow[] rPdfFiles = _dsPdfFiles.Tables["PDFFiles"].Select("Id = '" + _idActivePdf + "'");

            string fileNamePath = rPdfFiles[0]["PathAndFileName"].ToString();

            string fileName = "!----" + Path.GetFileName(fileNamePath);

            File.Move(fileNamePath, Path.GetDirectoryName(fileNamePath) + "\\" + fileName);

            // licznik plików danego typu
            _btnDictionary[99] = _btnDictionary[99] + 1;

            rPdfFiles[0]["PathAndFileName"] = Path.GetDirectoryName(fileNamePath) + "\\" + fileName;
            rPdfFiles[0]["FileName"] = fileName;
            rPdfFiles[0]["FileNameNew"] = fileName;
            rPdfFiles[0]["Prefix"] = "skip";
            rPdfFiles[0]["PrefixCode"] = 99;

            listBoxFiles.Items[_idActivePdf] = "!----" + listBoxFiles.Items[_idActivePdf] + " -> SKIP";

            // -----------------------------
            // załaduj następny plik do okna
            if (++_idActivePdf < _dsPdfFiles.Tables["PDFFiles"].Rows.Count)
            {
                rPdfFiles = _dsPdfFiles.Tables["PDFFiles"].Select("Id = '" + _idActivePdf + "'");

                pdfDocumentViewer.LoadFromFile(rPdfFiles[0]["PathAndFileName"].ToString());

                _zoom = GetZoom(rPdfFiles[0]["PathAndFileName"].ToString());

                pdfDocumentViewer.ZoomTo(_zoom);

                pdfDocumentViewer.EnableHandTool();

                listBoxFiles.SetSelected(_idActivePdf, true);

                long fileSize = new FileInfo(rPdfFiles[0]["PathAndFileName"].ToString()).Length / 1024;

                statusStripMainLabel.Text = $"Aktualny plik PDF: {(Convert.ToInt16(rPdfFiles[0]["Id"]) + 1)}/{_dsPdfFiles.Tables["PDFFiles"].Rows.Count} - {rPdfFiles[0]["PathAndFileName"]} [{fileSize} KB]";
            }
            else
            {
                MessageBox.Show("Wszystkie dokumenty zindeksowane", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void BtnScalAuto_Click(object sender, EventArgs e)
        {
            List<string> pdfs = _dsPdfFiles.Tables["PDFFiles"].Rows.OfType<DataRow>().Select(dr => (string)dr["PathAndFileName"]).ToList(); // lista wybranych plików PDF 

            string pdfMergeFolder = Path.GetDirectoryName(pdfs[0]) + "\\merge";
            string pdfInputFolder = Path.GetDirectoryName(pdfs[0]);

            string operat = pdfInputFolder?.Split(Path.DirectorySeparatorChar).Last();

            if (operat != textBoxOperat.Text)
            {
                MessageBox.Show($"Nazwa ustawionego operatu jest inna niż nazwa katalogu!\n{textBoxOperat.Text}\n{operat}", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Directory.Exists(pdfMergeFolder))
            {
                Directory.CreateDirectory(pdfMergeFolder);
            }

            List<string> typesToMerge = _dsDictionary.Tables["Dictionary"].Select("SCAL = 'tak'").Select(p => (string)p["PREFIX"]).ToList();    //  lista prefixów do złączenia

            long sizeBefore = 0;
            long sizeAfter = 0;

            foreach (string prefix in typesToMerge)
            {
                using (PdfSharp.Pdf.PdfDocument targetDoc = new PdfSharp.Pdf.PdfDocument())
                {
                    foreach (string pdf in pdfs)
                    {
                        string inputFile = Path.GetFileName(pdf);

                        if (inputFile != null && inputFile.Contains(prefix))
                        {
                            long length = new FileInfo(pdf).Length;

                            sizeBefore += length;

                            PdfSharp.Pdf.IO.PdfReader.Open(pdf, PdfDocumentOpenMode.ReadOnly);

                            using (PdfSharp.Pdf.PdfDocument pdfDoc = PdfSharp.Pdf.IO.PdfReader.Open(pdf, PdfDocumentOpenMode.Import))
                            {
                                for (int i = 0; i < pdfDoc.PageCount; i++)
                                {
                                    targetDoc.AddPage(pdfDoc.Pages[i]);
                                }
                            }

                            // kasowanie scalonego pliku
                            File.Delete(pdf);
                        }
                    }

                    if (targetDoc.PageCount > 0)
                    {
                        string outputFile;

                        switch (_powiat)
                        {
                            case "gdansk":
                                outputFile = Path.Combine(pdfMergeFolder, textBoxOperat.Text + prefix + "pdf");
                                break;

                            case "kwidzyn":
                                outputFile = Path.Combine(pdfMergeFolder, textBoxOperat.Text + "_" + "1" + prefix + "pdf");
                                break;

                            case "kartuzy":
                                outputFile = Path.Combine(pdfMergeFolder, textBoxOperat.Text + prefix + "pdf");
                                break;

                            default:
                                throw new Exception("Nieznany powiat!");
                        }

                        targetDoc.Save(outputFile);

                        long length = new FileInfo(outputFile).Length;

                        sizeAfter += length;
                    }
                }
            }

            MessageBox.Show($"Pliki połączono!\n\nRozmiar przed: {Math.Round(sizeBefore / (double)1024, 2)} MB\nRozmiar po:      {Math.Round(sizeAfter / (double)1024, 2)} MB\n\nWspółczynnik zmiany rozmiaru: { Math.Round(sizeAfter / (double)sizeBefore, 3) }", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnZnakWodny_Click(object sender, EventArgs e)
        {
            List<string> pdfs = _dsPdfFiles.Tables["PDFFiles"].Rows.OfType<DataRow>().Select(dr => (string)dr["PathAndFileName"]).ToList(); // lista wybranych plików PDF 

            for (int i = 0; i < pdfs.Count; i++)
            {
                SetZnakWodny(pdfs[i]);
                listBoxFiles.SetSelected(i, true);
            }

            pdfDocumentViewer.EnableHandTool();

            MessageBox.Show(@"Wstawiono znak wodny", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private int GetZoom(string fileName)
        {
            Spire.Pdf.PdfDocument pdf = new Spire.Pdf.PdfDocument(fileName);

            PdfPageRotateAngle rotation = pdf.Pages[0].Rotation;

            double pdfPageSizeYPoint;
            double pdfPageSizeXPoint;

            switch (rotation)
            {
                case PdfPageRotateAngle.RotateAngle0:

                    pdfPageSizeYPoint = pdf.Pages[0].Size.Height;
                    pdfPageSizeXPoint = pdf.Pages[0].Size.Width;

                    break;

                case PdfPageRotateAngle.RotateAngle90:  

                    pdfPageSizeYPoint = pdf.Pages[0].Size.Width;
                    pdfPageSizeXPoint = pdf.Pages[0].Size.Height;

                    break;

                case PdfPageRotateAngle.RotateAngle180:

                    pdfPageSizeYPoint = pdf.Pages[0].Size.Height;
                    pdfPageSizeXPoint = pdf.Pages[0].Size.Width;

                    break;

                case PdfPageRotateAngle.RotateAngle270: 

                    pdfPageSizeYPoint = pdf.Pages[0].Size.Width;
                    pdfPageSizeXPoint = pdf.Pages[0].Size.Height;

                    break;

                default:

                    throw new ArgumentOutOfRangeException();
            }

            pdf.Close();

            float dpiX;
            float dpiY;

            using (Graphics graphics = Graphics.FromHwnd(IntPtr.Zero))
            {
                dpiX = graphics.DpiX;
                dpiY = graphics.DpiY;
            }

            double pdfPageSizeXPix = pdfPageSizeXPoint * dpiX / 72;
            double pdfPageSizeYPix = pdfPageSizeYPoint * dpiY / 72;
            
            int pdfViewerSizeYPix = pdfDocumentViewer.Height;
            int pdfViewerSizeXPix = pdfDocumentViewer.Width;

            double scaleX = pdfViewerSizeXPix / pdfPageSizeXPix * 100 - 0.5;
            double scaleY = pdfViewerSizeYPix / pdfPageSizeYPix * 100 - 0.5;


            return scaleX < scaleY ? (int) scaleX : (int) scaleY;

        }

        private void SetZnakWodny(string fileName)
        {
            iTextSharp.text.pdf.PdfReader pdfReader = new iTextSharp.text.pdf.PdfReader(fileName);

            BaseFont baseFont;

            using (Stream fontStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ScanHelper.Resources.arial.ttf"))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fontStream?.CopyTo(ms);
                    baseFont = BaseFont.CreateFont("arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, BaseFont.CACHED, ms.ToArray(), null);
                }
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfStamper pdfStamper = new PdfStamper(pdfReader, memoryStream);

                for (int i = 1; i <= pdfReader.NumberOfPages; i++)
                {
                    iTextSharp.text.Rectangle pageRectangle = pdfReader.GetPageSizeWithRotation(i);

                    PdfContentByte pdfPageContents = pdfStamper.GetOverContent(i);

                    pdfPageContents.SaveState();

                    PdfGState state = new PdfGState { FillOpacity = 0.5f, StrokeOpacity = 0.5f };

                    pdfPageContents.SetGState(state);

                    pdfPageContents.BeginText();

                    pdfPageContents.SetFontAndSize(baseFont, 8f);
                    pdfPageContents.SetRGBColorFill(128, 128, 128);

                    ColumnText ctOperat = new ColumnText(pdfPageContents);
                    Phrase pOperatText = new Phrase(textBoxOperat.Text, new iTextSharp.text.Font(baseFont, 14f));
                    ctOperat.SetSimpleColumn(pOperatText, 10, pageRectangle.Height - 50, 150, pageRectangle.Height - 10, 14f, Element.ALIGN_LEFT);
                    ctOperat.Go();

                    ColumnText ctStopka = new ColumnText(pdfPageContents);
                    Phrase myText = new Phrase(Resources.stopkaKwidzyn, new iTextSharp.text.Font(baseFont, 8f));
                    ctStopka.SetSimpleColumn(myText, 0, 0, pageRectangle.Width, 50, 8f, Element.ALIGN_CENTER);
                    ctStopka.Go();

                    pdfPageContents.EndText();

                    pdfPageContents.RestoreState();
                }

                pdfStamper.Close();

                byte[] byteArray = memoryStream.ToArray();

                using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(byteArray, 0, byteArray.Length);
                }
            }

            pdfReader.Close();
        }

        private void FrmMain_ResizeEnd(object sender, EventArgs e)
        {
            DataRow[] rPdfFiles = _dsPdfFiles.Tables["PDFFiles"].Select("Id = '" + _idActivePdf + "'");

            _zoom = _idActivePdf == 0 ? 
                GetZoom(AppDomain.CurrentDomain.BaseDirectory + "ScanHelper.pdf") : GetZoom(rPdfFiles[0]["PathAndFileName"].ToString());

            pdfDocumentViewer.ZoomTo(_zoom);

            pdfDocumentViewer.EnableHandTool();
        }

        private void BtnRotate_Click(object sender, EventArgs e)
        {

        }
    }
}