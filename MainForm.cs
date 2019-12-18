using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FCEngine;
using System.IO;
using System.Threading;
using System.Globalization;
using System.Xml;

namespace VisualEditor
{
    public partial class MainForm : Form
    {
        IEngineLoader _engineLoader;
        IEngine _engine;
        IDocument _document;
        IFlexiCaptureProcessor _processor;
        string _pathFolderWork = "";
        string _nameCurrentFile = "";

        public MainForm(string workDirectory, string fileLoad)
        {
            InitializeComponent();

            // инициализация движка ABBYY
            try
            {
                _engineLoader = new FCEngine.InprocLoader
                {
                    CustomerProjectId = FceConfig.GetCustomerProjectId(), LicensePassword = "", LicensePath = ""
                };
                _engine = _engineLoader.GetEngine();

                //engine = engineLoader.Load(FceConfig.GetCustomerProjectId(), "");
                _processor = _engine.CreateFlexiCaptureProcessor();
                // Initialize DocumentView component
                documentView.Engine = _engine;

                _pathFolderWork = workDirectory;

                if (_pathFolderWork.EndsWith("\\"))
                    _pathFolderWork = _pathFolderWork.Substring(0, _pathFolderWork.Length - 1);

                if (String.IsNullOrEmpty(Path.GetDirectoryName(_pathFolderWork)))
                    throw new Exception(_pathFolderWork + " не является корректной директорией");
               
                _pathFolderWork += "\\visualeditorfiles\\";
                if (!String.IsNullOrEmpty(fileLoad))
                    _nameCurrentFile = Path.GetFileNameWithoutExtension(fileLoad);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось загрузить компоненту ABBYY по причине: " + ex.Message);
                Application.Exit();
            }
            //updateButtonsState();
            LoadWorkDocument();
        }

        private void loadButton_Click(object sender, EventArgs e) // загружаем следующий по списку документ
        {
            // Create a sample document using FlexiCapture Processor
            try {
			    
                Cursor.Current = Cursors.WaitCursor;

                if (!String.IsNullOrEmpty(_pathFolderWork))
                {
                    string[] pathFiles = Directory.GetFiles(_pathFolderWork);
                    
                    if (pathFiles.Length == 0)
                    {
                        MessageBox.Show("Отсутствуют файлы в рабочей папке " + _pathFolderWork);
                        return;
                    }
                    else if (pathFiles.Length == 1)
                    {
                        MessageBox.Show("Доступен только один документ для редактирования");
                        return;
                    }

                    for (int i = 0; i < pathFiles.Length; i++)
                    {
                        var nameFile = Path.GetFileNameWithoutExtension(pathFiles[i]);
                        if (nameFile == _nameCurrentFile)
                        {
                            if (i < pathFiles.Length - 1)
                                _nameCurrentFile = Path.GetFileNameWithoutExtension(pathFiles[i + 1]);
                            else
                                _nameCurrentFile = Path.GetFileNameWithoutExtension(pathFiles[0]);

                            break;
                        }
                    }
                }                   
                else
                {
                    MessageBox.Show("Невозможно загрузить документ, отсутствует рабочая папка");
                    return;
                }

                // выгружаем предыдущий документ
                documentView.Document = null;
                _document = null;

                _document = _processor.CreateDocument();

                _document.AsCustomStorage.LoadFromFile(_pathFolderWork + _nameCurrentFile + ".mydoc");          

                // отображаем текущий документ с помощью documentView
                documentView.Document = _document;
                documentView.SelectFirstFieldWithRuleErrors();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки документов: " + ex.Message + Environment.NewLine + "Путь: " + _pathFolderWork);
            }
            finally {
                Cursor.Current = Cursors.Default;
            }                      
            
            UpdateButtonsState();
        }

        private void unloadButton_Click(object sender, EventArgs e) // запись результатов в исходных xml файл
        {
            var directoryInfo = Directory.GetParent(_pathFolderWork).Parent;
            if (directoryInfo != null)
            {
                string test = directoryInfo.FullName;
                string yoo = test + "\\" + _nameCurrentFile + ".xml";

                try
                {
                    File.Delete(_pathFolderWork + _nameCurrentFile + ".mydoc");
                    File.Delete(yoo);
                    SaveResultsToDisk(test, _nameCurrentFile);
                
                } catch (Exception ex)
                {
                    MessageBox.Show("Не удалось сохранить результаты на диске, по причине: " + ex.Message);
                }
            }

            label2.Show();
            var t = new System.Windows.Forms.Timer {Interval = 3000};
            // it will Tick in 3 seconds
            t.Tick += (s, ea) =>
            {
                label2.Hide();
                t.Stop();
            };
            t.Start();


        }

        private void verifyButton_Click(object sender, EventArgs e)
        {
            // Create a collection of documents to verify (only one document in this sample)
            IDocumentsCollection documents = _engine.CreateDocumentsCollection();
            documents.Add( _document );
            
            // Create and show verification dialog. See the VerificationDialog.cs for details.
            // VerificationView in this dialog should be properly disposed of to ensure that
            // the associated verification session is closed and all resources are released in timely mannar
            using( VerificationDialog dialog = new VerificationDialog( _engine, documents ) ) {
                dialog.ShowDialog();
            }
        }

        private void UpdateButtonsState()
        {
            // Update buttons' state
            //loadButton.Enabled = ( document == null );
            unloadButton.Enabled = ( _document != null );
            verifyButton.Enabled = ( _document != null );
        }

        private void CleanUp()
        {
            // Clear all views
           documentView.Document = null;

            // чистка папок
            new Thread(() =>
            {
                var directoryInfo = Directory.GetParent(_pathFolderWork).Parent;
                if (directoryInfo != null)
                    CleanDirectories(directoryInfo.FullName);
            }).Start();


            // Unload the engine
            if ( _engineLoader != null ) {
                _engineLoader.Unload();
                _engineLoader = null;
            }
            _engine = null;
        }

        private void LoadWorkDocument() // загружаем рабочий документ при инициализации
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (!String.IsNullOrEmpty(_pathFolderWork)) 
                {
                    string[] pathFiles = Directory.GetFiles(_pathFolderWork);

                    if (pathFiles.Length == 0)
                    {
                        MessageBox.Show("Отсутствуют файлы в рабочей папке " + _pathFolderWork);
                        return;
                    }
                    else if (pathFiles.Length > 1)
                        loadButton.Enabled = true;
                    else
                        loadButton.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Невозможно загрузить документ, отсутствует рабочая папка");
                    return;
                }
                // начало загрузки
                _document = _processor.CreateDocument();

                if (!String.IsNullOrEmpty(_nameCurrentFile))
                    _document.AsCustomStorage.LoadFromFile(_pathFolderWork + _nameCurrentFile + ".mydoc");
                else
                {
                    string[] pathFiles = Directory.GetFiles(_pathFolderWork);
                    _nameCurrentFile = Path.GetFileNameWithoutExtension(pathFiles[0]);
                    _document.AsCustomStorage.LoadFromFile(_pathFolderWork + _nameCurrentFile + ".mydoc");
                }
                    

                // отображаем текущий документ с помощью documentView
                documentView.Document = _document;
                documentView.SelectFirstFieldWithRuleErrors();
            } catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки документов: " + ex.Message + Environment.NewLine + "Путь: " + _pathFolderWork +
                    Environment.NewLine + ex.StackTrace);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

            UpdateButtonsState();
        }

        void SaveResultsToDisk(string exportFolder, string fileName)
        {
            IFileExportParams exportParams = _engine.CreateFileExportParams();
            exportParams.FileFormat = FileExportFormatEnum.FEF_XML;
            exportParams.XMLParams.ExportErrors = true;
            //exportParams.XMLParams.MarkSuspiciousSymbols = true;
            exportParams.FileNamePattern = fileName;
            exportParams.FileOverwriteRule = FileOverwriteRuleEnum.FOR_Rename;
            exportParams.ExportOriginalImages = false;

            //processor.ExportDocumentEx(document, exportFolder, null, _exportParams);
            _processor.ExportDocument(_document, exportFolder);

            _document.AsCustomStorage.SaveToFile(_pathFolderWork + _nameCurrentFile + ".mydoc");
        }

        public void CleanDirectories(string pathFolderClean, int countDaysStore = 3)
        {
            Thread.CurrentThread.IsBackground = true;
            IEnumerable<string> directoriesPath = Directory.GetDirectories(pathFolderClean);
            foreach (string dPath in directoriesPath)
            {
                string[] files = Directory.GetFiles(dPath);

                foreach (string file in files)
                {
                    var fi = new FileInfo(file);

                    try
                    {
                        if (fi.LastWriteTime < DateTime.Now.AddDays(-countDaysStore))
                            fi.Delete();
                    }
                    catch (Exception ex)
                    {
                        string errorText = "Не удалось выполнить очистку рабочих папок по пути: " + fi;
                        errorText += Environment.NewLine + ex.Message;
                        MessageBox.Show(errorText);
                    }

                }
            }

            DeleteEmptyRows(pathFolderClean);
        }

        void DeleteEmptyRows(string paramPathExportFolder)
        {
            string[] xmlFiles = Directory.GetFiles(paramPathExportFolder, "*.xml");

            foreach (string xmlFile in xmlFiles)
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(xmlFile);

                XmlNodeList itemNodes = xmlDocument.GetElementsByTagName("_Table");
                string nodeTaxIncluded = null;

                bool condTaxField = false;
                XmlNodeList listNodesTax = xmlDocument.GetElementsByTagName("_TaxIncluded");
                if (listNodesTax.Count == 1)
                {
                    nodeTaxIncluded = listNodesTax[0].InnerText;
                    if (String.IsNullOrEmpty(nodeTaxIncluded))
                        condTaxField = true;
                }

                if (itemNodes.Count == 0)
                    itemNodes = xmlDocument.GetElementsByTagName("_Table1");

                if (itemNodes.Count > 0)
                {

                    for (int i = itemNodes.Count - 1; i >= 0; i--)
                    {
                        string fieldDescript;
                        try
                        {
                            fieldDescript = itemNodes[i].SelectSingleNode("_Descript")?.InnerText;
                        }
                        catch
                        {
                            break;
                        }

                        if (String.IsNullOrEmpty(fieldDescript) || fieldDescript.Length < 3)
                        {
                            var parentNode = itemNodes[i].ParentNode;
                            parentNode?.RemoveChild(itemNodes[i]);
                        }
                        else if (condTaxField) // вычисляем НДС входит в стоимость или нет
                        {
                            double price = 0;
                            double qty = 0;
                            double cost = 0;
                            double sumWTax = 0;

                            var nodeSumWithTax = itemNodes[i].SelectSingleNode("_SumWithTax").InnerText;
                            nodeSumWithTax = nodeSumWithTax.Replace('.', ',');

                            if (nodeSumWithTax != null)
                            {
                                double.TryParse(nodeSumWithTax, NumberStyles.Currency, null, out sumWTax);
                                string nodeCost = null;
                                try
                                {
                                    nodeCost = itemNodes[i].SelectSingleNode("_Cost").InnerText;
                                    nodeCost = nodeCost.Replace('.', ',');
                                }
                                catch
                                {
                                    // ignored
                                }


                                if (String.IsNullOrEmpty(nodeCost))
                                {

                                    string nodePrice = null;
                                    string nodeQty = null;

                                    try
                                    {
                                        nodePrice = itemNodes[i].SelectSingleNode("_Price").InnerText;
                                        nodeQty = itemNodes[i].SelectSingleNode("_Qty").InnerText;
                                    }
                                    catch { }


                                    if (nodePrice != null & nodeQty != null)
                                    {
                                        nodePrice = nodePrice.Replace('.', ',');
                                        nodeQty = nodeQty.Replace('.', ',');
                                        Double.TryParse(nodePrice, NumberStyles.Currency, null, out price);
                                        Double.TryParse(nodeQty, NumberStyles.Currency, null, out qty);
                                    }

                                    if (price > 0 & qty > 0)
                                        cost = price * qty;
                                }
                                else
                                    Double.TryParse(nodeCost, NumberStyles.Currency, null, out cost);

                                if (cost > 0)
                                {
                                    if (Math.Abs(sumWTax - cost) > 0.1)
                                        nodeTaxIncluded = "false";
                                    else
                                        nodeTaxIncluded = "true";

                                    listNodesTax[0].InnerText = nodeTaxIncluded;
                                    condTaxField = false;
                                }
                            }


                        }
                    }

                    xmlDocument.Save(xmlFile);

                }
            }
        }
    }
}
