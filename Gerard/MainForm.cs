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

        IssueRequest issueRequest = new IssueRequest();
        List<IssueObject> issueObjectsList = new List<IssueObject>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            issueRequest = new IssueRequest();
            issueObjectsList = new List<IssueObject>();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Path.GetDirectoryName(Directory.GetCurrentDirectory());
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
                string fileName = fileInfo.Name.Replace(fileInfo.Extension, "");
                issueRequest.Summary = fileName;
                gridIssueRequest.DataSource = new List<IssueRequest> { issueRequest };

                try
                {
                    ExcelOptions opts = new ExcelOptions
                    {
                        TrimWhitespace = true,
                        ReadFormatted = true,
                        UnformattedFormat = CultureInfo.InvariantCulture
                    };

                    using (ExcelPackage pkg = new ExcelPackage(fileInfo))
                    {
                        ExcelWorksheet sheet = pkg.Workbook.Worksheets.First();
                        ExcelAddressBase dim = sheet.Dimension;

                        if (dim == null)
                        {
                            return;
                        }

                        int start = dim.Start.Row + 5;
                        if (start > dim.End.Row)
                        {
                            return;
                        }

                        ExcelRange range = sheet.Cells[start, dim.Start.Column, dim.End.Row, dim.End.Column];
                        ExcelReader reader = new ExcelReader(range, opts);
                        while(reader.Read())
                        {
                            IssueObject issueObject = new IssueObject
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
                            issueObjectsList.Add(issueObject);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }

                gridIssueObjects.DataSource = issueObjectsList;
                gridIssueRequest.Update();
                gridIssueRequest.Refresh();
                gridIssueObjects.Update();
                gridIssueObjects.Refresh();
                btnCreate.Enabled = true;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            btnLoad.Enabled = false;

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

                //string issueKey = projectKey + "-" + "8";
                //ProjectMeta projectMetaData = client.GetProjectMeta(projectKey);
                //Issue issueWithAllFields = client.GetIssue(issueKey);

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

                foreach(var issueObject in issueObjectsList)
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not create jira tasks. Original error: " + ex.Message);
            }

            gridIssueRequest.Update();
            gridIssueRequest.Refresh();
            gridIssueObjects.Update();
            gridIssueObjects.Refresh();
            btnCreate.Enabled = false;
            btnLoad.Enabled = true;
        }

        private void gridIssueRequest_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 0 || string.IsNullOrWhiteSpace(issueRequest.JiraLink))
            {
                return;
            }

            Process.Start(issueRequest.JiraLink);
        }
    }
}
