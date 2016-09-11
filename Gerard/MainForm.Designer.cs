namespace Gerard
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.gridIssueObjects = new System.Windows.Forms.DataGridView();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnLoadLivingObjects = new System.Windows.Forms.Button();
            this.gridIssueRequest = new System.Windows.Forms.DataGridView();
            this.JiraLink = new System.Windows.Forms.DataGridViewLinkColumn();
            this.btnLoadNonLivingObjects = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridIssueObjects)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridIssueRequest)).BeginInit();
            this.SuspendLayout();
            // 
            // gridIssueObjects
            // 
            this.gridIssueObjects.AllowUserToAddRows = false;
            this.gridIssueObjects.AllowUserToDeleteRows = false;
            this.gridIssueObjects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridIssueObjects.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridIssueObjects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridIssueObjects.Location = new System.Drawing.Point(12, 66);
            this.gridIssueObjects.Name = "gridIssueObjects";
            this.gridIssueObjects.ReadOnly = true;
            this.gridIssueObjects.RowTemplate.Height = 24;
            this.gridIssueObjects.Size = new System.Drawing.Size(598, 301);
            this.gridIssueObjects.TabIndex = 0;
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreate.Location = new System.Drawing.Point(464, 372);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(146, 50);
            this.btnCreate.TabIndex = 1;
            this.btnCreate.Text = "Создать задачи";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnLoadLivingObjects
            // 
            this.btnLoadLivingObjects.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadLivingObjects.Location = new System.Drawing.Point(12, 372);
            this.btnLoadLivingObjects.Name = "btnLoadLivingObjects";
            this.btnLoadLivingObjects.Size = new System.Drawing.Size(220, 50);
            this.btnLoadLivingObjects.TabIndex = 2;
            this.btnLoadLivingObjects.Text = "Загрузить заявку с жилыми помещениями";
            this.btnLoadLivingObjects.UseVisualStyleBackColor = true;
            this.btnLoadLivingObjects.Click += new System.EventHandler(this.btnLoadLivingObjects_Click);
            // 
            // gridIssueRequest
            // 
            this.gridIssueRequest.AllowUserToAddRows = false;
            this.gridIssueRequest.AllowUserToDeleteRows = false;
            this.gridIssueRequest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridIssueRequest.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridIssueRequest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridIssueRequest.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.JiraLink});
            this.gridIssueRequest.Location = new System.Drawing.Point(12, 12);
            this.gridIssueRequest.Name = "gridIssueRequest";
            this.gridIssueRequest.ReadOnly = true;
            this.gridIssueRequest.RowTemplate.Height = 24;
            this.gridIssueRequest.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.gridIssueRequest.Size = new System.Drawing.Size(598, 53);
            this.gridIssueRequest.TabIndex = 3;
            this.gridIssueRequest.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridIssueRequest_CellContentClick);
            // 
            // JiraLink
            // 
            this.JiraLink.DataPropertyName = "JiraLink";
            this.JiraLink.HeaderText = "JiraLink";
            this.JiraLink.Name = "JiraLink";
            this.JiraLink.ReadOnly = true;
            // 
            // btnLoadNonLivingObjects
            // 
            this.btnLoadNonLivingObjects.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadNonLivingObjects.Location = new System.Drawing.Point(238, 372);
            this.btnLoadNonLivingObjects.Name = "btnLoadNonLivingObjects";
            this.btnLoadNonLivingObjects.Size = new System.Drawing.Size(220, 50);
            this.btnLoadNonLivingObjects.TabIndex = 4;
            this.btnLoadNonLivingObjects.Text = "Загрузить заявку с нежилыми помещениями";
            this.btnLoadNonLivingObjects.UseVisualStyleBackColor = true;
            this.btnLoadNonLivingObjects.Click += new System.EventHandler(this.btnLoadNonLivingObjects_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 433);
            this.Controls.Add(this.btnLoadNonLivingObjects);
            this.Controls.Add(this.gridIssueRequest);
            this.Controls.Add(this.btnLoadLivingObjects);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.gridIssueObjects);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "MainForm";
            this.Text = "GerardLoader";
            ((System.ComponentModel.ISupportInitialize)(this.gridIssueObjects)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridIssueRequest)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridIssueObjects;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnLoadLivingObjects;
        private System.Windows.Forms.DataGridView gridIssueRequest;
        private System.Windows.Forms.DataGridViewLinkColumn JiraLink;
        private System.Windows.Forms.Button btnLoadNonLivingObjects;
    }
}

