using Fittings.Connections;
using Fittings.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Fittings
{
    public partial class ImportWindow : Form
    {
        /// <summary>
        /// Путь к файлу для импорта.
        /// </summary>
        private string PathToFile { get; set; }

        /// <summary>
        /// Лист фиттингов (из файла).
        /// </summary>
        private List<FittingModel> FittingList { get; set; }

        public ImportWindow()
        {
            new UI.UserCulture().SetCultureOnForm();
            InitializeComponent();

            dataGridView1.DataError += DataGridView1_DataError;
            FittingList = new List<FittingModel>();

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Авто подбор ширины столбца.
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Выделение строки целиком.
        }

        private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = ProjectStrings.SelectFileForImportTitle;
            openFileDialog1.Filter = "TXT (*.txt)|*txt|CSV (*.csv)|*.csv|All files|*.*";
            openFileDialog1.Multiselect = false;
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                PathToFile = openFileDialog1.FileName;
                LoadFittingsFromFile(PathToFile);

                dataGridView1.DataSource = FittingList;
            }
        }

        /// <summary>
        /// Загружает данные из файла и записывает их в виде List<FittingModel>.
        /// </summary>
        /// <param name="path">Путь к файлу.</param>
        private async void LoadFittingsFromFile(string path)
        {
            FittingList.Clear();

            using (StreamReader fs = new StreamReader(path, Encoding.Default))
            {
                string text = await fs.ReadToEndAsync();

                string extension = Path.GetExtension(path);
                //char separator = new char();                

                string[] lines = text.Split('\n');
                progressBar1.Minimum = 0;
                progressBar1.Maximum = lines.Length;
                progressBar1.Value = 0;

                foreach (string line in lines)
                {
                    if (line != "" || !String.IsNullOrEmpty(line))
                    {
                        string[] words = new string[2];
                        switch (extension)
                        {
                            case ".csv":
                                words = line.Split(';');
                                break;
                            case ".txt":
                                words = line.Split('\t');
                                break;
                        }
                        FittingList.Add(new FittingModel(words[0], words[1]));
                    }

                    progressBar1.Value++;
                    progressBar1.Refresh();
                }
            }
        }

        /// <summary>
        /// Добавить новый фиттинг.
        /// </summary>
        private bool Add(string name, double price)
        {
            string sql = String.Format("exec AddFitting N'{0}', {1}",
                name,
                price);
            using (SqlConnection connection = new SqlConnection(Connection.MainSqlConnection))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);

                try
                {
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Error sql", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        private async Task<bool> AddAsync(string name, double price)
        {
            bool result = await Task.Run(() => Add(name, price));
            return result;
        }

        private async void btnImport_Click(object sender, EventArgs e)
        {
            if (FittingList.Count < 1)
            {
                MessageBox.Show(ProjectStrings.NotDataForImport, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool flag = false;
            progressBar1.Value = 0;
            progressBar1.Maximum = FittingList.Count;

            foreach (FittingModel fitting in FittingList)
            {
                //flag = Add(fitting.Name, double.Parse(fitting.Price));
                flag = await AddAsync(fitting.Name, double.Parse(fitting.Price));
                if (!flag)
                    break;
                progressBar1.Value++;
                progressBar1.Refresh();
            }

            MessageBox.Show(ProjectStrings.ImportIsDone, "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClearMemory_Click(object sender, EventArgs e)
        {
            FittingList.Clear();
            dataGridView1.DataSource = null;
        }

        private async void btnMakeSql_Click(object sender, EventArgs e)
        {
            if (FittingList.Count < 1)
            {
                MessageBox.Show(ProjectStrings.NotDataForImport, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = saveFileDialog1.FileName;

                progressBar1.Value = 0;
                progressBar1.Maximum = FittingList.Count;

                using (StreamWriter sw = new StreamWriter(path, true, Encoding.Default))
                {
                    foreach (FittingModel fitting in FittingList)
                    {
                        string sql = String.Format("exec AddFitting N'{0}', {1}", fitting.Name, double.Parse(fitting.Price));
                        await sw.WriteLineAsync(sql);

                        progressBar1.Value++;
                        progressBar1.Refresh();
                    }
                }

                MessageBox.Show(ProjectStrings.FileIsCreate, "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
