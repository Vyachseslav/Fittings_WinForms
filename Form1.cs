using Fittings.Connections;
using Fittings.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Fittings
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Текущий грид в фокусе.
        /// </summary>
        public DevExpress.XtraGrid.GridControl FocusedGrid { get; set; }

        public Form1()
        {
            new UI.UserCulture().SetCultureOnForm();
            InitializeComponent();

            if (!String.IsNullOrEmpty(Properties.Settings.Default.Language))
            {
                if (Properties.Settings.Default.Language == "en-US")
                {
                    menuRusLang.Checked = false;
                    menuEngLang.Checked = true;
                }
                else
                {
                    menuRusLang.Checked = true;
                    menuEngLang.Checked = false;
                }
            }

            new Connections.ReadConnection().GetUserExtendedConnection();

            this.FormClosing += Form1_FormClosing;

            gridProjects.GotFocus += GridProjects_GotFocus;
            gridFittings.GotFocus += GridFittings_GotFocus;
            gridFullProject.GotFocus += GridFullProject_GotFocus;
            gridEquipments.GotFocus += GridEquipments_GotFocus;

            try
            {
                LoadProjects();
                LoadFullProjects();
                gridView1.FocusedRowChanged += GridView1_FocusedRowChanged;

                gridProjects.Select();
                gridView1.FocusedRowHandle = 0;
                GridView1_FocusedRowChanged(null, null);

                foreach (DevExpress.XtraGrid.Columns.GridColumn column in gridView1.Columns)
                {
                    column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                    column.AppearanceCell.Options.UseTextOptions = true;
                    if (column.Caption.Contains("Id") || column.Caption.Contains("ID"))
                    {
                        column.Visible = false;
                    }
                }
                foreach (DevExpress.XtraGrid.Columns.GridColumn column in gridView2.Columns)
                {
                    column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                    column.AppearanceCell.Options.UseTextOptions = true;
                    if (column.FieldName.Contains("Id") || column.FieldName.Contains("ID"))
                    {
                        column.Visible = false;
                    }
                }
                foreach (DevExpress.XtraGrid.Columns.GridColumn column in gridView3.Columns)
                {
                    column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                    column.AppearanceCell.Options.UseTextOptions = true;
                }

                gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
                gridView2.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
                gridView3.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                gridView3.OptionsSelection.EnableAppearanceFocusedCell = false;
                gridView6.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                gridView6.OptionsSelection.EnableAppearanceFocusedCell = false;

                gridView1.Columns["Id"].Caption = "№";
                gridView1.Columns["Name"].Caption = "Название проекта";
                gridView1.Columns["Description"].Caption = "Описание проекта";
                gridView1.Columns["CreateDate"].Caption = "Дата создания";
                gridView1.Columns["CreateDate"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridView1.Columns["CreateDate"].DisplayFormat.FormatString = "dd.MM.yyyy";
                gridView1.Columns["FittingSum"].Caption = "Сумма комплектующих";
                gridView1.Columns["TransportSum"].Caption = "Транспортный расход";
                gridView1.Columns["LaborIntencity"].Caption = "Трудоемкость";
                gridView1.Columns["UnexpectedExpenses"].Caption = "Непредвиденные расходы";
                gridView1.Columns["TotalSum"].Caption = "Себестоимость";
                gridView1.Columns["NumberOfGoods"].Caption = "Кол-во товара";
                gridView1.Columns["Distanсe"].Caption = "Расстояние (км)";
                gridView1.Columns["Hours"].Caption = "Затраченное время (ч)";
                gridView1.Columns["PeopleCount"].Caption = "Число сотрудников";
                gridView1.Columns["TotalSumWithPercent2"].Caption = "Цена реализации";
                gridView1.Columns["AmortizationSum"].Caption = "Цена амортизации";



                gridView2.Columns["Name"].Caption = "Название";
                gridView2.Columns["FittingCount"].Caption = "Кол-во (шт)";
                gridView2.Columns["FittingSum"].Caption = "Сумма";

                gridView3.Columns["ProjectName"].Caption = "Название проекта";
                gridView3.Columns["FittingName"].Caption = "Название фиттинга";
                gridView3.Columns["FittingCount"].Caption = "Кол-во (шт)";
                gridView3.Columns["FittingSum"].Caption = "Сумма";
            }
            catch
            {
                MessageBox.Show("Произошла ошибка соедения с сервером.\nПожалуйста, настройте соединение.", "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            FocusedGrid = gridProjects;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                gridProjects.MainView.RestoreLayoutFromXml(GridSettings.GridProjectSettings);
                gridFittings.MainView.RestoreLayoutFromXml(GridSettings.GridFittingSettings);
                gridFullProject.MainView.RestoreLayoutFromXml(GridSettings.GridFullProjectSettings);
                gridEquipments.MainView.RestoreLayoutFromXml(GridSettings.GridEquipmentsSettings);
            }
            catch
            {
                MessageBox.Show("Произошла ошибка загрузки настроек грида.\nЕсли Вы впервые запускаете приложение, то просто нажимите ОК.", 
                    "Ошибка настройки грида", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!String.IsNullOrEmpty(Properties.Settings.Default.Language))
                {
                    string lang = Properties.Settings.Default.Language;
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                gridProjects.MainView.SaveLayoutToXml(GridSettings.GridProjectSettings);
                gridFittings.MainView.SaveLayoutToXml(GridSettings.GridFittingSettings);
                gridFullProject.MainView.SaveLayoutToXml(GridSettings.GridFullProjectSettings);
                gridEquipments.MainView.SaveLayoutToXml(GridSettings.GridEquipmentsSettings);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Properties.Settings.Default.Language = (menuRusLang.Checked) ? "ru-RU" : "en-US";
                Properties.Settings.Default.Save();

                Application.Exit();
            }
        }

        private void GridFullProject_GotFocus(object sender, EventArgs e)
        {
            FocusedGrid = gridFullProject;
            label1.Text = "В фокусе таблица проектов без каскада";
            //gridView3.Columns[0].Caption = "Fuck";
            //gridView3.Columns[0].Visible = false;
        }

        private void GridFittings_GotFocus(object sender, EventArgs e)
        {
            FocusedGrid = gridFittings;
            label1.Text = "В фокусе таблица фиттингов";
        }

        private void GridProjects_GotFocus(object sender, EventArgs e)
        {
            FocusedGrid = gridProjects;
            label1.Text = "В фокусе таблица проектов";
        }

        private void GridEquipments_GotFocus(object sender, EventArgs e)
        {
            FocusedGrid = gridEquipments;
            label1.Text = "В фокусе таблица оборудования";
        }

        private void GridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int index = 0;
            object idSelectedProject = 0;
            try
            {
                index = gridView1.FocusedRowHandle;
                idSelectedProject = gridView1.GetRowCellValue(index, "Id");
            }
            catch { }
            finally
            {
                LoadFittings(idSelectedProject);
                LoadEquipments(idSelectedProject);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            MessageBox.Show(FocusedGrid.Name);
        }

        private void LoadProjects()
        {
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = new SqlMethods().LoadProjects("exec ShowProjects");
            gridProjects.DataSource = bindingSource;
        }

        private void LoadFittings(object idProject)
        {
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = new SqlMethods().LoadProjects(String.Format("exec ShowAccessories {0}", idProject));
            gridFittings.DataSource = bindingSource;
        }

        private void LoadFullProjects()
        {
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = new SqlMethods().LoadProjects("exec SelectFullProjects");
            gridFullProject.DataSource = bindingSource;
        }

        private void LoadEquipments(object idProject)
        {
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = new SqlMethods().LoadProjects(String.Format("exec ShowEquipmentToProject {0}", idProject));
            gridEquipments.DataSource = bindingSource;

            gridView6.Columns["Name"].Caption = "Название оборудования";
            gridView6.Columns["EquipmentUsingPercent"].Caption = "Использование оборудования, %";
            gridView6.Columns["AmortizationPrice"].Caption = "Сумма амортизации";

            foreach (DevExpress.XtraGrid.Columns.GridColumn column in gridView6.Columns)
            {
                column.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                column.AppearanceCell.Options.UseTextOptions = true;

                if (column.FieldName.Contains("Id") || column.FieldName.Contains("ID") || column.FieldName.Contains("EquipmentPrice"))
                {
                    column.Visible = false;
                }
            }
        }

        private void exportToExcelMenu_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Экспорт в Excel";
            saveFileDialog1.Filter = "Excel 2007 (.xlsx)|*.xlsx";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FocusedGrid.ExportToXlsx(saveFileDialog1.FileName);
            }
        }

        private void pdfExportMenu_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Экспорт в PDF";
            saveFileDialog1.Filter = "PDF (.pdf)|*.pdf";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FocusedGrid.ExportToPdf(saveFileDialog1.FileName);
            }
        }

        private void menuHandbooks_Click(object sender, EventArgs e)
        {
            Handbooks handbooks = new Handbooks();
            handbooks.Show();
        }

        private void btnAddFitting_Click(object sender, EventArgs e)
        {
            AddFitting();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            LoadFullProjects();
        }

        private void btnEditFitting_Click(object sender, EventArgs e)
        {
            EditFittingOfProject();
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            EditFittingOfProject();
        }

        

        private void btnAddProject_Click(object sender, EventArgs e)
        {
            AddProject();
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditProject();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            EditProject();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            /*AboutBox aboutBox = new AboutBox();
            aboutBox.StartPosition = FormStartPosition.CenterParent;
            aboutBox.Owner = this;
            aboutBox.ShowDialog();*/
            ViewForms.AboutWindow window = new ViewForms.AboutWindow();
            window.ShowDialog();
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            switch (FocusedGrid.Name)
            {
                case "gridProjects":
                    AddProject();
                    break;
                case "gridFittings":
                    AddFitting();
                    break;
                case "gridEquipments":
                    AddEquipment();
                    break;
            }
            
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //int oldIndex = 0;

            switch (FocusedGrid.Name)
            {
                case "gridProjects":
                    //oldIndex = gridView1.FocusedRowHandle; // Индекс до обновления.
                    EditProject();
                    //gridView1.FocusedRowHandle = oldIndex;
                    break;
                case "gridFittings":                    
                    EditFittingOfProject();
                    break;
                case "gridEquipments":
                    EditEquipment();
                    break;
            }
        }

        private void EditEquipment()
        {
            object id = gridView6.GetRowCellValue(gridView6.FocusedRowHandle, "Id");
            object idProject = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id");
            object idEquipment = gridView6.GetRowCellValue(gridView6.FocusedRowHandle, "EquipmentID");
            object percent = gridView6.GetRowCellValue(gridView6.FocusedRowHandle, "EquipmentUsingPercent");

            Cards.ProjectEquipmentCard card = new Cards.ProjectEquipmentCard(id, idProject, idEquipment, percent);
            card.Owner = this;
            if (card.ShowDialog() == DialogResult.OK)
            {
                LoadProjects();
                gridView1.FocusedRowHandle = UI.GridControlUIModifications.GetIndexRowOfItem(idProject, gridView1);

                LoadEquipments(idProject);
                gridView6.FocusedRowHandle = UI.GridControlUIModifications.GetIndexRowOfItem(card.ProjectEquipmentModel.Id, gridView6);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            bool flag = false;
            int idProject = int.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id").ToString());
            int oldIndex = 0;

            switch (FocusedGrid.Name)
            {
                case "gridProjects":
                    flag = DeleteProject();
                    if (flag)
                    {
                        oldIndex = gridView1.FocusedRowHandle; // Индекс до обновления.
                        RefreshProjectsGrid();
                        gridView1.FocusedRowHandle = (oldIndex > 0) ? oldIndex - 1 : oldIndex;
                    }
                    break;
                case "gridFittings":                    
                    flag = DeleteFitting();
                    if (flag)
                    {
                        // Обновляем строку с проектом.
                        DataTable dt = new SqlMethods().LoadProjects(String.Format("exec ShowTheOneProjects {0}", idProject));
                        gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "FittingSum", dt.Rows[0]["FittingSum"]);
                        gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "TransportSum", dt.Rows[0]["TransportSum"]);
                        gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "LaborIntencity", dt.Rows[0]["LaborIntencity"]);
                        gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "UnexpectedExpenses", dt.Rows[0]["UnexpectedExpenses"]);
                        gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "TotalSum", dt.Rows[0]["TotalSum"]);

                        oldIndex = gridView2.FocusedRowHandle; // Индекс до обновления.
                        RefreshFittingGrid();
                        gridView2.FocusedRowHandle = (oldIndex > 0) ? oldIndex - 1 : oldIndex;
                    }
                    break;
                case "gridEquipments":
                    flag = DeleteEquipment();
                    if (flag)
                    {
                        oldIndex = gridView6.FocusedRowHandle; // Индекс до обновления.

                        // Обновляем строку с проектом.
                        LoadProjects();
                        gridView1.FocusedRowHandle = UI.GridControlUIModifications.GetIndexRowOfItem(idProject, gridView1);

                        
                        LoadEquipments(idProject);
                        gridView6.FocusedRowHandle = (oldIndex > 0) ? oldIndex - 1 : oldIndex;
                    }
                    break;
            }

            
        }

        private bool DeleteEquipment()
        {
            int id = int.Parse(gridView6.GetRowCellValue(gridView6.FocusedRowHandle, "Id").ToString());

            if (MessageBox.Show("Вы действительно хотите удалить оборудование из проекта?", 
                "Удаление оборудования", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                DialogResult.No)
            {
                return false;
            }

            Model.ProjectEquipmentModel model = new ProjectEquipmentModel();
            int flag = model.Remove(id);
            return (flag > 0) ? true : false;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            int oldIndex = 0;
            switch (FocusedGrid.Name)
            {
                case "gridProjects":
                    gridView1.ShowLoadingPanel();
                    oldIndex = gridView1.FocusedRowHandle; // Индекс до обновления.
                    RefreshProjectsGrid();
                    gridView1.FocusedRowHandle = oldIndex;
                    gridView1.HideLoadingPanel();
                    break;
                case "gridFittings":
                    gridView2.ShowLoadingPanel();
                    oldIndex = gridView2.FocusedRowHandle; // Индекс до обновления.
                    RefreshFittingGrid();
                    gridView2.FocusedRowHandle = oldIndex;
                    gridView2.HideLoadingPanel();
                    break;
                case "gridFullProject":
                    gridView3.ShowLoadingPanel();
                    oldIndex = gridView3.FocusedRowHandle;
                    RefreshFullProjectGrid();
                    gridView3.FocusedRowHandle = oldIndex;
                    gridView3.HideLoadingPanel();
                    break;
                case "gridEquipments":
                    gridView6.ShowLoadingPanel();
                    oldIndex = gridView6.FocusedRowHandle;
                    object idProject = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id");
                    LoadEquipments(idProject);
                    gridView6.FocusedRowHandle = oldIndex;
                    gridView6.HideLoadingPanel();
                    break;
            }
            
        }



        /// <summary>
        /// Добавление проекта.
        /// </summary>
        private void AddProject()
        {
            ProjectCard projectCard = new ProjectCard();
            projectCard.Owner = this;
            projectCard.ShowDialog();

            if (projectCard.DialogResult == DialogResult.OK)
            {
                LoadProjects();

                // Выделяем строку с новым фиттингом.
                int idNewProject = projectCard.IdAddedProject;
                for (int i = 0; i < gridView1.DataRowCount; i++)
                {
                    object b = gridView1.GetRowCellValue(i, "Id");
                    if (b != null && b.Equals(idNewProject))
                    {
                        gridView1.FocusedRowHandle = i;
                        continue;
                    }
                }

                if (MessageBox.Show("Хотите добавить портфель к проекту?", 
                    "Добавление портфеля", 
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    ViewForms.SelectBriefcaseWindow selectItem = new ViewForms.SelectBriefcaseWindow();
                    selectItem.Owner = this;
                    if (selectItem.ShowDialog() == DialogResult.OK)
                    {
                        List<int> id = selectItem.SelectedBriefcaseID;
                        // Добавляем портфели к проекту.
                        foreach (int oneID in id)
                        {
                            List<Model.AddFittingModel> models = new List<AddFittingModel>();
                            DataTable dt = new SqlMethods().LoadProjects(String.Format("select IDFitting, FittingCount from BriefcaseAccessories where IDBriefcase={0}", oneID));
                            foreach (DataRow row in dt.Rows)
                            {
                                models.Add(new AddFittingModel()
                                {
                                    IdProject = idNewProject,
                                    IdFitting = int.Parse(row["IDFitting"].ToString()),
                                    FittingCount = double.Parse(row["FittingCount"].ToString())
                                });
                            }

                            foreach (Model.AddFittingModel model in models)
                            {
                                AddFittingToProject(model.IdProject, model.IdFitting, model.FittingCount);
                            }
                        }                        

                        LoadFittings(idNewProject);

                        // Обновляем строку с проектом.
                        DataTable dt1 = new SqlMethods().LoadProjects(String.Format("exec ShowTheOneProjects {0}", idNewProject));
                        gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "FittingSum", dt1.Rows[0]["FittingSum"]);
                        gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "TransportSum", dt1.Rows[0]["TransportSum"]);
                        gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "LaborIntencity", dt1.Rows[0]["LaborIntencity"]);
                        gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "UnexpectedExpenses", dt1.Rows[0]["UnexpectedExpenses"]);
                        gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "TotalSum", dt1.Rows[0]["TotalSum"]);
                        gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "TotalSumWithPercent2", dt1.Rows[0]["TotalSumWithPercent2"]);
                    }
                }
            }
        }



        /// <summary>
        /// Добавление фиттинга к проекту.
        /// </summary>
        private void AddFitting()
        {
            object idProject = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id");

            FittingCard fittingCard = new FittingCard() { ProjectID = int.Parse(idProject.ToString()) };
            fittingCard.Owner = this;
            fittingCard.ShowDialog();

            if (fittingCard.DialogResult == DialogResult.OK)
            {
                LoadFittings(idProject);

                // Обновляем строку с проектом.
                DataTable dt = new SqlMethods().LoadProjects(String.Format("exec ShowTheOneProjects {0}", idProject));
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "FittingSum", dt.Rows[0]["FittingSum"]);
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "TransportSum", dt.Rows[0]["TransportSum"]);
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "LaborIntencity", dt.Rows[0]["LaborIntencity"]);
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "UnexpectedExpenses", dt.Rows[0]["UnexpectedExpenses"]);
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "TotalSum", dt.Rows[0]["TotalSum"]);
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "TotalSumWithPercent2", dt.Rows[0]["TotalSumWithPercent2"]);

                // Выделяем строку с новым фиттингом.
                int idNewFitting = fittingCard.IdAddedAccessories;
                for (int i = 0; i < gridView2.DataRowCount; i++)
                {
                    object b = gridView2.GetRowCellValue(i, "Id");
                    if (b != null && b.Equals(idNewFitting))
                    {
                        gridView2.FocusedRowHandle = i;
                        return;
                    }
                }
            }
        }

        private void AddEquipment()
        {
            object idProject = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id");
            Cards.ProjectEquipmentCard card = new Cards.ProjectEquipmentCard(idProject); 
            card.Owner = this;
            
            if (card.ShowDialog() == DialogResult.OK)
            {
                LoadProjects();
                gridView1.FocusedRowHandle = UI.GridControlUIModifications.GetIndexRowOfItem(idProject, gridView1);

                LoadEquipments(idProject);
                gridView6.FocusedRowHandle = UI.GridControlUIModifications.GetIndexRowOfItem(card.ProjectEquipmentModel.Id, gridView6);
            }
        }

        /// <summary>
        /// Обновить таблицу фиттингов.
        /// </summary>
        private void RefreshFittingGrid()
        {
            object idProject = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id");
            LoadFittings(idProject);
        }

        /// <summary>
        /// Обновить таблицу проектов.
        /// </summary>
        private void RefreshProjectsGrid()
        {
            //int oldIndex = gridView1.FocusedRowHandle; // Индекс до обновления.
            LoadProjects();
            //gridView1.FocusedRowHandle = oldIndex;
        }

        /// <summary>
        /// Обновить грид FullProject.
        /// </summary>
        private void RefreshFullProjectGrid()
        {
            LoadFullProjects();
        }

        /// <summary>
        /// Редактировать выбранный фиттинг.
        /// </summary>
        private void EditFittingOfProject()
        {
            object idProject = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id");
            object idAccessories = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "Id");
            object fittingCount = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "FittingCount");
            object fittingId = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "FittingID");

            if (idAccessories == null)
            {
                return;
            }

            FittingCard fittingCard = new FittingCard(
                int.Parse(idProject.ToString()),
                int.Parse(idAccessories.ToString()),
                int.Parse(fittingId.ToString()),
                double.Parse(fittingCount.ToString()),
                true);
            fittingCard.Owner = this;
            fittingCard.ShowDialog();

            if (fittingCard.DialogResult == DialogResult.OK)
            {
                //int oldIndex = gridView2.FocusedRowHandle; // Индекс до обновления.
                LoadFittings(idProject);
                //gridView2.FocusedRowHandle = oldIndex;

                //int idNewProject = projectCard.IdAddedProject;
                for (int i = 0; i < gridView2.DataRowCount; i++)
                {
                    object b = gridView2.GetRowCellValue(i, "Id");
                    if (b != null && b.Equals(idAccessories))
                    {
                        gridView2.FocusedRowHandle = i;
                        continue;
                    }
                }

                // Обновляем строку с проектом.
                DataTable dt = new SqlMethods().LoadProjects(String.Format("exec ShowTheOneProjects {0}", idProject));
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "FittingSum", dt.Rows[0]["FittingSum"]);
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "TransportSum", dt.Rows[0]["TransportSum"]);
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "LaborIntencity", dt.Rows[0]["LaborIntencity"]);
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "UnexpectedExpenses", dt.Rows[0]["UnexpectedExpenses"]);
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "TotalSum", dt.Rows[0]["TotalSum"]);
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "TotalSumWithPercent2", dt.Rows[0]["TotalSumWithPercent2"]);
            }
        }

        /// <summary>
        /// Редактировать выбранный проект.
        /// </summary>
        private void EditProject()
        {
            object idProject = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id");
            object nameProject = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Name");
            object descriptionProject = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Description");

            object fitSum = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "FittingSum");
            object transportSum = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TransportSum");
            object laborSum = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "LaborIntencity");
            object unexcpectSum = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "UnexpectedExpenses");
            object totalSum = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TotalSum");

            object numOfGoods = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "NumberOfGoods");
            object distance = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Distanсe");
            object hours = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Hours");
            object peopleCount = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PeopleCount");



            ProjectCard projectCard = new ProjectCard(int.Parse(idProject.ToString()), nameProject.ToString(), descriptionProject.ToString(),
                fitSum, transportSum, laborSum, unexcpectSum, totalSum,
                numOfGoods, distance, hours, peopleCount);
            projectCard.Owner = this;
            projectCard.ShowDialog();

            if (projectCard.DialogResult == DialogResult.OK)
            {
                //int oldIndex = gridView1.FocusedRowHandle; // Индекс до обновления.
                LoadProjects();
                //gridView1.FocusedRowHandle = oldIndex;

                for (int i = 0; i < gridView1.DataRowCount; i++)
                {
                    object b = gridView1.GetRowCellValue(i, "Id");
                    if (b != null && b.Equals(idProject))
                    {
                        gridView1.FocusedRowHandle = i;
                        continue;
                    }
                }
            }
        }

        /// <summary>
        /// Удаляет выбранный проект.
        /// </summary>
        private bool DeleteProject()
        {
            int idProject = int.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id").ToString());

            if (MessageBox.Show(ProjectStrings.RemoveProjectText, ProjectStrings.RemoveTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                DialogResult.No)
            {
                return false;
            }

            string sql = String.Format("exec DropProject {0}", idProject);

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
                    MessageBox.Show(ex.Message, "Ошибка sql", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        /// <summary>
        /// Удаляет выбранный фиттинг.
        /// </summary>
        private bool DeleteFitting()
        {
            int idAccessory = int.Parse(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "Id").ToString());

            if (MessageBox.Show(ProjectStrings.RemoveFittingText, ProjectStrings.RemoveTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                DialogResult.No)
            {
                return false;
            }

            string sql = String.Format("exec DropAccessory {0}", idAccessory);

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
                    MessageBox.Show(ex.Message, "Ошибка sql", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        private void btnConnectionSettings_Click(object sender, EventArgs e)
        {
            ConnectionWindow connection = new ConnectionWindow();
            connection.StartPosition = FormStartPosition.CenterParent;
            connection.Owner = this;
            connection.ShowDialog();
        }

        private void btnSendMessageToDeveloper_Click(object sender, EventArgs e)
        {
            MessageToDeveloper message = new MessageToDeveloper();
            message.StartPosition = FormStartPosition.CenterParent;
            message.Owner = this;
            message.Show();
        }

        private void menuEngLang_Click(object sender, EventArgs e)
        {
            menuRusLang.Checked = false;
            menuEngLang.Checked = true;
        }

        private void menuRusLang_Click(object sender, EventArgs e)
        {
            menuRusLang.Checked = true;
            menuEngLang.Checked = false;
        }

        private void BtnConstants_Click(object sender, EventArgs e)
        {
            ConstantWindow window = new ConstantWindow();
            window.Owner = this;
            window.ShowDialog();
        }




        private bool AddFittingToProject(int idProject, int idFitting, double fittingCount)
        {
            //double pp = GetFittingSum(double.Parse(txtCount.Text.Replace('.',',')));

            string sql = String.Format("exec AddAccessory {0}, {1}, {2}",
                idProject,
                idFitting,
                fittingCount);
            using (SqlConnection connection = new SqlConnection(Connection.MainSqlConnection))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);

                try
                {
                    //IdAddedAccessories = int.Parse(command.ExecuteScalar().ToString());
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

        private void BtnAddBriefcase_Click(object sender, EventArgs e)
        {
            int idProject = int.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Id").ToString());

            ViewForms.SelectBriefcaseWindow selectItem = new ViewForms.SelectBriefcaseWindow();
            selectItem.Owner = this;
            if (selectItem.ShowDialog() == DialogResult.OK)
            {
                List<int> id = selectItem.SelectedBriefcaseID;
                // Добавляем портфели к проекту.
                foreach (int oneID in id)
                {
                    List<Model.AddFittingModel> models = new List<AddFittingModel>();
                    DataTable dt = new SqlMethods().LoadProjects(String.Format("select IDFitting, FittingCount from BriefcaseAccessories where IDBriefcase={0}", oneID));
                    foreach (DataRow row in dt.Rows)
                    {
                        models.Add(new AddFittingModel()
                        {
                            IdProject = idProject,
                            IdFitting = int.Parse(row["IDFitting"].ToString()),
                            FittingCount = double.Parse(row["FittingCount"].ToString())
                        });
                    }

                    foreach (Model.AddFittingModel model in models)
                    {
                        AddFittingToProject(model.IdProject, model.IdFitting, model.FittingCount);
                    }
                }

                LoadFittings(idProject);

                // Обновляем строку с проектом.
                DataTable dt1 = new SqlMethods().LoadProjects(String.Format("exec ShowTheOneProjects {0}", idProject));
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "FittingSum", dt1.Rows[0]["FittingSum"]);
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "TransportSum", dt1.Rows[0]["TransportSum"]);
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "LaborIntencity", dt1.Rows[0]["LaborIntencity"]);
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "UnexpectedExpenses", dt1.Rows[0]["UnexpectedExpenses"]);
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "TotalSum", dt1.Rows[0]["TotalSum"]);
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "TotalSumWithPercent2", dt1.Rows[0]["TotalSumWithPercent2"]);
            }
        }

        private void TabControl1_Click(object sender, EventArgs e)
        {
            //(sender as XtraTabControl).SelectedTabPage.Name;
            string tabName = (sender as TabControl).SelectedTab.Name;

            switch(tabName)
            {
                case "tabPageAccessories":
                    gridFittings.Focus();
                    break;
                case "tabPageEquipments":
                    gridEquipments.Focus();
                    break;
            }

            
        }

        private void GridEquipments_DoubleClick(object sender, EventArgs e)
        {
            EditEquipment();
        }
    }
}
