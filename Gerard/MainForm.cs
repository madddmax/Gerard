using System;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.Diagnostics;
using System.Configuration;
using Ctl.Data.Excel;
using OfficeOpenXml;
using AnotherJiraRestClient;
using AnotherJiraRestClient.JiraModel;
using Gerard.Model;


namespace Gerard
{
    public partial class MainForm : Form
    {
        string issueRequestTypeId = "10000";
        string issueObjectTypeId = "10100";
        string defaultDescription = "";
        string defaultPriorityId = "3";
        string[] defaultLabels = new string[] { };

        IssueRequest issueRequest;
        List<IssueLivingObject> issueLivingObjectsList;
        List<IssueNonLivingObject> issueNonLivingObjectsList;

        public MainForm()
        {
            InitializeComponent();
            InitializeProperties();
        }

        private void InitializeProperties()
        {
            issueRequest = new IssueRequest();
            issueLivingObjectsList = new List<IssueLivingObject>();
            issueNonLivingObjectsList = new List<IssueNonLivingObject>();

            gridIssueRequest.DataSource = null;
            gridIssueRequest.Update();
            gridIssueRequest.Refresh();

            gridIssueObjects.DataSource = null;
            gridIssueObjects.Update();
            gridIssueObjects.Refresh();

            btnCreate.Enabled = false;
            btnLoadLivingObjects.Enabled = true;
            btnLoadNonLivingObjects.Enabled = true;
        }

        private void btnLoadLivingObjects_Click(object sender, EventArgs e)
        {
            InitializeProperties();

            var openFileDialog = CreateOpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo fileInfo = new FileInfo(openFileDialog.FileName);

                try
                {
                    using (ExcelPackage pkg = new ExcelPackage(fileInfo))
                    {
                        ExcelWorksheet sheet = pkg.Workbook.Worksheets.First();
                        ExcelAddressBase dim = sheet.Dimension;
                        if (dim == null)
                        {
                            return;
                        }


                        int startFields = dim.Start.Row + 3;
                        if (startFields > dim.End.Row)
                        {
                            return;
                        }

                        ExcelRange rangeFields = sheet.Cells[startFields, dim.Start.Column, dim.End.Row, dim.End.Column];
                        ExcelReader readerFields = new ExcelReader(rangeFields, CreateExcelOptions());
                        readerFields.Read();
                        CheckFieldName(
                            readerFields.CurrentRow[1].Value,
                            "Адрес",
                            "Нет поля 'Адрес' в столбце B"
                        );
                        CheckFieldName(
                            readerFields.CurrentRow[2].Value,
                            "Этаж",
                            "Нет поля 'Этаж' в столбце C"
                        );
                        CheckFieldName(
                            readerFields.CurrentRow[3].Value,
                            "№ кв",
                            "Нет поля '№ кв' в столбце D"
                        );
                        CheckFieldName(
                            readerFields.CurrentRow[4].Value,
                            "Кол-во комнат",
                            "Нет поля 'Кол-во комнат' в столбце E"
                        );
                        CheckFieldName(
                            readerFields.CurrentRow[5].Value,
                            "Площадь",
                            "Нет поля 'Площадь' в столбце F"
                        );
                        CheckFieldName(
                            readerFields.CurrentRow[6].Value,
                            "Начальная стоимость",
                            "Нет поля 'Начальная стоимость' в столбце G"
                        );
                        CheckFieldName(
                            readerFields.CurrentRow[7].Value,
                            "Шаг аукциона",
                            "Нет поля 'Шаг аукциона' в столбце H"
                        );


                        int start = dim.Start.Row + 5;
                        if (start > dim.End.Row)
                        {
                            return;
                        }

                        ExcelRange range = sheet.Cells[start, dim.Start.Column, dim.End.Row, dim.End.Column];
                        ExcelReader reader = new ExcelReader(range, CreateExcelOptions());
                        while(reader.Read())
                        {
                            IssueLivingObject issueObject = new IssueLivingObject
                            {
                                Number = reader.CurrentRow[0].Value,
                                Address = reader.CurrentRow[1].Value,
                                Floor = reader.CurrentRow[2].Value,
                                FlatNumber = reader.CurrentRow[3].Value,
                                RoomsCount = reader.CurrentRow[4].Value,
                                Area = reader.CurrentRow[5].Value,
                                InitialCost = reader.CurrentRow[6].Value,
                                AuctionStep = reader.CurrentRow[7].Value,
                            };
                            issueLivingObjectsList.Add(issueObject);
                        }
                        gridIssueObjects.DataSource = issueLivingObjectsList;
                        gridIssueObjects.Update();
                        gridIssueObjects.Refresh();
                    }

                    string fileName = fileInfo.Name.Replace(fileInfo.Extension, "");
                    issueRequest.Summary = fileName;
                    gridIssueRequest.DataSource = new List<IssueRequest> { issueRequest };
                    gridIssueRequest.Update();
                    gridIssueRequest.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка чтения файла. " + ex.Message);
                }

                btnCreate.Enabled = true;
            }
        }

        private void btnLoadNonLivingObjects_Click(object sender, EventArgs e)
        {
            InitializeProperties();

            var openFileDialog = CreateOpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo fileInfo = new FileInfo(openFileDialog.FileName);

                try
                {
                    using (ExcelPackage pkg = new ExcelPackage(fileInfo))
                    {
                        ExcelWorksheet sheet = pkg.Workbook.Worksheets.First();
                        ExcelAddressBase dim = sheet.Dimension;
                        if (dim == null)
                        {
                            return;
                        }


                        int startFields = dim.Start.Row;
                        if (startFields > dim.End.Row)
                        {
                            return;
                        }

                        ExcelRange rangeFields = sheet.Cells[startFields, dim.Start.Column, dim.End.Row, dim.End.Column];
                        ExcelReader readerFields = new ExcelReader(rangeFields, CreateExcelOptions());
                        readerFields.Read();
                        CheckFieldName(
                            readerFields.CurrentRow[1].Value,
                            "Адрес", 
                            "Нет поля 'Адрес' в столбце B"
                        );
                        CheckFieldName(
                            readerFields.CurrentRow[2].Value,
                            "№ помещения",
                            "Нет поля '№ помещения' в столбце C"
                        );
                        CheckFieldName(
                            readerFields.CurrentRow[3].Value,
                            "Общая площадь",
                            "Нет поля 'Общая площадь' в столбце D"
                        );
                        CheckFieldName(
                            readerFields.CurrentRow[4].Value,
                            "Начальная стоимость",
                            "Нет поля 'Начальная стоимость' в столбце E"
                        );
                        CheckFieldName(
                            readerFields.CurrentRow[5].Value,
                            "Обеспечение заявки",
                            "Нет поля 'Обеспечение заявки' в столбце F"
                        );
                        CheckFieldName(
                            readerFields.CurrentRow[10].Value,
                            "Назначение помещений",
                            "Нет поля 'Назначение помещений' в столбце K"
                        );


                        int start = dim.Start.Row + 1;
                        if (start > dim.End.Row)
                        {
                            return;
                        }

                        ExcelRange range = sheet.Cells[start, dim.Start.Column, dim.End.Row, dim.End.Column];
                        ExcelReader reader = new ExcelReader(range, CreateExcelOptions());
                        while (reader.Read())
                        {
                            IssueNonLivingObject issueNonLivingObject = new IssueNonLivingObject
                            {
                                Number = reader.CurrentRow[0].Value,
                                Address = reader.CurrentRow[1].Value,
                                RoomNumber = reader.CurrentRow[2].Value,
                                Area = reader.CurrentRow[3].Value,
                                InitialCost = reader.CurrentRow[4].Value,
                                EnsureBid = reader.CurrentRow[5].Value,
                                RoomFunction = reader.CurrentRow[10].Value
                            };
                            issueNonLivingObjectsList.Add(issueNonLivingObject);
                        }
                        gridIssueObjects.DataSource = issueNonLivingObjectsList;
                        gridIssueObjects.Update();
                        gridIssueObjects.Refresh();
                    }

                    string fileName = fileInfo.Name.Replace(fileInfo.Extension, "");
                    issueRequest.Summary = fileName;
                    gridIssueRequest.DataSource = new List<IssueRequest> { issueRequest };
                    gridIssueRequest.Update();
                    gridIssueRequest.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка чтения файла. " + ex.Message);
                }

                btnCreate.Enabled = true;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            btnLoadLivingObjects.Enabled = false;
            btnLoadNonLivingObjects.Enabled = false;

            try
            {
                string jiraUrl = ConfigurationManager.AppSettings["jiraUrl"];
                string userName = ConfigurationManager.AppSettings["userName"];
                string password = ConfigurationManager.AppSettings["password"];
                string projectKey = ConfigurationManager.AppSettings["projectKey"];

                var client = new JiraClient(new JiraAccount
                {
                    ServerUrl = jiraUrl,
                    User = userName,
                    Password = password
                });

                CreateIssue createIssueRequest = new CreateIssue(
                    projectKey, 
                    issueRequest.Summary, 
                    defaultDescription, 
                    issueRequestTypeId, 
                    defaultPriorityId, 
                    defaultLabels
                );
                createIssueRequest.AddField(JiraFields.EpicName, issueRequest.Summary);
                BasicIssue resultIssueRequest = client.CreateIssue(createIssueRequest);

                issueRequest.JiraLink = jiraUrl + @"browse/" + resultIssueRequest.key;
                gridIssueRequest.Update();
                gridIssueRequest.Refresh();

                if (issueLivingObjectsList.Count > 0)
                {
                    foreach (var issueObject in issueLivingObjectsList)
                    {
                        CreateIssue createIssueObject = new CreateIssue(
                            projectKey,
                            issueObject.Address + ", кв. " + issueObject.FlatNumber,
                            defaultDescription,
                            issueObjectTypeId,
                            defaultPriorityId,
                            defaultLabels
                        );
                        createIssueObject.AddField(JiraFields.EpicLink, resultIssueRequest.key);
                        createIssueObject.AddField(JiraFields.Address, issueObject.Address);
                        createIssueObject.AddField(JiraFields.Floor, issueObject.Floor);
                        createIssueObject.AddField(JiraFields.FlatNumber, issueObject.FlatNumber);
                        createIssueObject.AddField(JiraFields.RoomsCount, issueObject.RoomsCount);
                        createIssueObject.AddField(JiraFields.Area, issueObject.Area);
                        createIssueObject.AddField(JiraFields.InitialCost, issueObject.InitialCost);
                        createIssueObject.AddField(JiraFields.AuctionStep, issueObject.AuctionStep);
                        BasicIssue resultIssueObject = client.CreateIssue(createIssueObject);

                        issueObject.JiraLink = jiraUrl + @"browse/" + resultIssueObject.key;
                    }
                }

                if(issueNonLivingObjectsList.Count > 0)
                {
                    foreach (var issueObject in issueNonLivingObjectsList)
                    {
                        CreateIssue createIssueObject = new CreateIssue(
                            projectKey,
                            issueObject.Address + ", № помещения " + issueObject.RoomNumber,
                            defaultDescription,
                            issueObjectTypeId,
                            defaultPriorityId,
                            defaultLabels
                        );
                        createIssueObject.AddField(JiraFields.EpicLink, resultIssueRequest.key);
                        createIssueObject.AddField(JiraFields.Address, issueObject.Address);
                        createIssueObject.AddField(JiraFields.RoomNumber, issueObject.RoomNumber);
                        createIssueObject.AddField(JiraFields.Area, issueObject.Area);
                        createIssueObject.AddField(JiraFields.InitialCost, issueObject.InitialCost);
                        createIssueObject.AddField(JiraFields.EnsureBid, issueObject.EnsureBid);
                        createIssueObject.AddField(JiraFields.RoomFunction, issueObject.RoomFunction);
                        BasicIssue resultIssueObject = client.CreateIssue(createIssueObject);

                        issueObject.JiraLink = jiraUrl + @"browse/" + resultIssueObject.key;
                    }
                }

                gridIssueObjects.Update();
                gridIssueObjects.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка создания задач в JIRA. " + ex.Message);
            }

            btnCreate.Enabled = false;
            btnLoadLivingObjects.Enabled = true;
            btnLoadNonLivingObjects.Enabled = true;
        }

        private void gridIssueRequest_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 0 || string.IsNullOrWhiteSpace(issueRequest.JiraLink))
            {
                return;
            }

            Process.Start(issueRequest.JiraLink);
        }

        private OpenFileDialog CreateOpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string currentDir = Directory.GetCurrentDirectory();
            openFileDialog.InitialDirectory = Path.GetDirectoryName(currentDir);
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            return openFileDialog;
        }

        private ExcelOptions CreateExcelOptions()
        {
            return new ExcelOptions
            {
                TrimWhitespace = true,
                ReadFormatted = true,
                UnformattedFormat = CultureInfo.InvariantCulture
            };
        }

        private void CheckFieldName(string value, string name, string message)
        {
            if (value == null || !value.Contains(name))
            {
                throw new Exception(message);
            }
        }
    }
}
