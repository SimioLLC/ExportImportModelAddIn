namespace ExportImportModelAddIn
{
    partial class ExportImportModelForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportImportModelForm));
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDelimiter = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ckDeleteTableData = new System.Windows.Forms.CheckBox();
            this.ckDeleteObjects = new System.Windows.Forms.CheckBox();
            this.ckExportTableData = new System.Windows.Forms.CheckBox();
            this.ckExportObjectsAndLinks = new System.Windows.Forms.CheckBox();
            this.ckExportObjectTypes = new System.Windows.Forms.CheckBox();
            this.ckExportObjects = new System.Windows.Forms.CheckBox();
            this.ckExportVertices = new System.Windows.Forms.CheckBox();
            this.ckExportNetworks = new System.Windows.Forms.CheckBox();
            this.ckExportElements = new System.Windows.Forms.CheckBox();
            this.ckExportLists = new System.Windows.Forms.CheckBox();
            this.ckExportModelPropertyValues = new System.Windows.Forms.CheckBox();
            this.ckExportTablePropertyValues = new System.Windows.Forms.CheckBox();
            this.ckImportTableData = new System.Windows.Forms.CheckBox();
            this.ckImportObjectsAndLinks = new System.Windows.Forms.CheckBox();
            this.ckImportObjectTypes = new System.Windows.Forms.CheckBox();
            this.ckImportObjects = new System.Windows.Forms.CheckBox();
            this.ckImportVertices = new System.Windows.Forms.CheckBox();
            this.ckImportNetworks = new System.Windows.Forms.CheckBox();
            this.ckImportElements = new System.Windows.Forms.CheckBox();
            this.ckImportLists = new System.Windows.Forms.CheckBox();
            this.ckImportModelPropertyValues = new System.Windows.Forms.CheckBox();
            this.ckImportTablePropertyValues = new System.Windows.Forms.CheckBox();
            this.ckAutoGenReadFiles = new System.Windows.Forms.CheckBox();
            this.txtAutoGenPrefix = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.txtSQLServerConnectionString = new System.Windows.Forms.TextBox();
            this.ckDeleteJustLinks = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ckExportTablesToDB = new System.Windows.Forms.CheckBox();
            this.ckImportTableFromDB = new System.Windows.Forms.CheckBox();
            this.ckExportTablesToCSV = new System.Windows.Forms.CheckBox();
            this.ckImportModelPropertiesFromCSV = new System.Windows.Forms.CheckBox();
            this.ckExportLogsToDB = new System.Windows.Forms.CheckBox();
            this.ckExportModelStateDefinitions = new System.Windows.Forms.CheckBox();
            this.tableCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(16, 15);
            this.okButton.Margin = new System.Windows.Forms.Padding(4);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(139, 64);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(16, 86);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(4);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(139, 63);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 159);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 16);
            this.label1.TabIndex = 13;
            this.label1.Text = "Delimiter";
            // 
            // txtDelimiter
            // 
            this.txtDelimiter.Location = new System.Drawing.Point(16, 180);
            this.txtDelimiter.Margin = new System.Windows.Forms.Padding(4);
            this.txtDelimiter.Name = "txtDelimiter";
            this.txtDelimiter.Size = new System.Drawing.Size(57, 22);
            this.txtDelimiter.TabIndex = 14;
            this.txtDelimiter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDelimiter.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(419, 11);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 17);
            this.label2.TabIndex = 26;
            this.label2.Text = "Export";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1029, 11);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 17);
            this.label3.TabIndex = 27;
            this.label3.Text = "Import";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(761, 10);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 17);
            this.label4.TabIndex = 28;
            this.label4.Text = "Delete";
            // 
            // ckDeleteTableData
            // 
            this.ckDeleteTableData.AutoSize = true;
            this.ckDeleteTableData.Location = new System.Drawing.Point(765, 30);
            this.ckDeleteTableData.Margin = new System.Windows.Forms.Padding(4);
            this.ckDeleteTableData.Name = "ckDeleteTableData";
            this.ckDeleteTableData.Size = new System.Drawing.Size(140, 20);
            this.ckDeleteTableData.TabIndex = 29;
            this.ckDeleteTableData.Text = "Delete Table Data";
            this.ckDeleteTableData.UseVisualStyleBackColor = true;
            // 
            // ckDeleteObjects
            // 
            this.ckDeleteObjects.AutoSize = true;
            this.ckDeleteObjects.Location = new System.Drawing.Point(765, 57);
            this.ckDeleteObjects.Margin = new System.Windows.Forms.Padding(4);
            this.ckDeleteObjects.Name = "ckDeleteObjects";
            this.ckDeleteObjects.Size = new System.Drawing.Size(118, 20);
            this.ckDeleteObjects.TabIndex = 30;
            this.ckDeleteObjects.Text = "Delete Objects";
            this.ckDeleteObjects.UseVisualStyleBackColor = true;
            // 
            // ckExportTableData
            // 
            this.ckExportTableData.AutoSize = true;
            this.ckExportTableData.Location = new System.Drawing.Point(423, 30);
            this.ckExportTableData.Margin = new System.Windows.Forms.Padding(4);
            this.ckExportTableData.Name = "ckExportTableData";
            this.ckExportTableData.Size = new System.Drawing.Size(190, 20);
            this.ckExportTableData.TabIndex = 33;
            this.ckExportTableData.Text = "Export Table(s) Data (XML)";
            this.ckExportTableData.UseVisualStyleBackColor = true;
            // 
            // ckExportObjectsAndLinks
            // 
            this.ckExportObjectsAndLinks.AutoSize = true;
            this.ckExportObjectsAndLinks.Location = new System.Drawing.Point(423, 59);
            this.ckExportObjectsAndLinks.Margin = new System.Windows.Forms.Padding(4);
            this.ckExportObjectsAndLinks.Name = "ckExportObjectsAndLinks";
            this.ckExportObjectsAndLinks.Size = new System.Drawing.Size(245, 20);
            this.ckExportObjectsAndLinks.TabIndex = 34;
            this.ckExportObjectsAndLinks.Text = "Export Objects And Links (Delimited)";
            this.ckExportObjectsAndLinks.UseVisualStyleBackColor = true;
            // 
            // ckExportObjectTypes
            // 
            this.ckExportObjectTypes.AutoSize = true;
            this.ckExportObjectTypes.Location = new System.Drawing.Point(423, 88);
            this.ckExportObjectTypes.Margin = new System.Windows.Forms.Padding(4);
            this.ckExportObjectTypes.Name = "ckExportObjectTypes";
            this.ckExportObjectTypes.Size = new System.Drawing.Size(188, 20);
            this.ckExportObjectTypes.TabIndex = 35;
            this.ckExportObjectTypes.Text = "Export Object Types (XML)";
            this.ckExportObjectTypes.UseVisualStyleBackColor = true;
            // 
            // ckExportObjects
            // 
            this.ckExportObjects.AutoSize = true;
            this.ckExportObjects.Location = new System.Drawing.Point(422, 116);
            this.ckExportObjects.Margin = new System.Windows.Forms.Padding(4);
            this.ckExportObjects.Name = "ckExportObjects";
            this.ckExportObjects.Size = new System.Drawing.Size(153, 20);
            this.ckExportObjects.TabIndex = 36;
            this.ckExportObjects.Text = "Export Objects (XML)";
            this.ckExportObjects.UseVisualStyleBackColor = true;
            // 
            // ckExportVertices
            // 
            this.ckExportVertices.AutoSize = true;
            this.ckExportVertices.Location = new System.Drawing.Point(422, 144);
            this.ckExportVertices.Margin = new System.Windows.Forms.Padding(4);
            this.ckExportVertices.Name = "ckExportVertices";
            this.ckExportVertices.Size = new System.Drawing.Size(187, 20);
            this.ckExportVertices.TabIndex = 37;
            this.ckExportVertices.Text = "Export Vertices (Delimited)";
            this.ckExportVertices.UseVisualStyleBackColor = true;
            // 
            // ckExportNetworks
            // 
            this.ckExportNetworks.AutoSize = true;
            this.ckExportNetworks.Location = new System.Drawing.Point(422, 172);
            this.ckExportNetworks.Margin = new System.Windows.Forms.Padding(4);
            this.ckExportNetworks.Name = "ckExportNetworks";
            this.ckExportNetworks.Size = new System.Drawing.Size(194, 20);
            this.ckExportNetworks.TabIndex = 38;
            this.ckExportNetworks.Text = "Export Networks (Delimited)";
            this.ckExportNetworks.UseVisualStyleBackColor = true;
            // 
            // ckExportElements
            // 
            this.ckExportElements.AutoSize = true;
            this.ckExportElements.Location = new System.Drawing.Point(422, 200);
            this.ckExportElements.Margin = new System.Windows.Forms.Padding(4);
            this.ckExportElements.Name = "ckExportElements";
            this.ckExportElements.Size = new System.Drawing.Size(194, 20);
            this.ckExportElements.TabIndex = 39;
            this.ckExportElements.Text = "Export Elements (Delimited)";
            this.ckExportElements.UseVisualStyleBackColor = true;
            // 
            // ckExportLists
            // 
            this.ckExportLists.AutoSize = true;
            this.ckExportLists.Location = new System.Drawing.Point(422, 229);
            this.ckExportLists.Margin = new System.Windows.Forms.Padding(4);
            this.ckExportLists.Name = "ckExportLists";
            this.ckExportLists.Size = new System.Drawing.Size(165, 20);
            this.ckExportLists.TabIndex = 40;
            this.ckExportLists.Text = "Export Lists (Delimited)";
            this.ckExportLists.UseVisualStyleBackColor = true;
            // 
            // ckExportModelPropertyValues
            // 
            this.ckExportModelPropertyValues.AutoSize = true;
            this.ckExportModelPropertyValues.Location = new System.Drawing.Point(422, 257);
            this.ckExportModelPropertyValues.Margin = new System.Windows.Forms.Padding(4);
            this.ckExportModelPropertyValues.Name = "ckExportModelPropertyValues";
            this.ckExportModelPropertyValues.Size = new System.Drawing.Size(244, 20);
            this.ckExportModelPropertyValues.TabIndex = 41;
            this.ckExportModelPropertyValues.Text = "Export Model Property Values (XML)";
            this.ckExportModelPropertyValues.UseVisualStyleBackColor = true;
            // 
            // ckExportTablePropertyValues
            // 
            this.ckExportTablePropertyValues.AutoSize = true;
            this.ckExportTablePropertyValues.Location = new System.Drawing.Point(422, 285);
            this.ckExportTablePropertyValues.Margin = new System.Windows.Forms.Padding(4);
            this.ckExportTablePropertyValues.Name = "ckExportTablePropertyValues";
            this.ckExportTablePropertyValues.Size = new System.Drawing.Size(242, 20);
            this.ckExportTablePropertyValues.TabIndex = 42;
            this.ckExportTablePropertyValues.Text = "Export Table Property Values (XML)";
            this.ckExportTablePropertyValues.UseVisualStyleBackColor = true;
            // 
            // ckImportTableData
            // 
            this.ckImportTableData.AutoSize = true;
            this.ckImportTableData.Location = new System.Drawing.Point(1031, 30);
            this.ckImportTableData.Margin = new System.Windows.Forms.Padding(4);
            this.ckImportTableData.Name = "ckImportTableData";
            this.ckImportTableData.Size = new System.Drawing.Size(174, 20);
            this.ckImportTableData.TabIndex = 43;
            this.ckImportTableData.Text = "Import Table Data (XML)";
            this.ckImportTableData.UseVisualStyleBackColor = true;
            // 
            // ckImportObjectsAndLinks
            // 
            this.ckImportObjectsAndLinks.AutoSize = true;
            this.ckImportObjectsAndLinks.Location = new System.Drawing.Point(1031, 58);
            this.ckImportObjectsAndLinks.Margin = new System.Windows.Forms.Padding(4);
            this.ckImportObjectsAndLinks.Name = "ckImportObjectsAndLinks";
            this.ckImportObjectsAndLinks.Size = new System.Drawing.Size(244, 20);
            this.ckImportObjectsAndLinks.TabIndex = 44;
            this.ckImportObjectsAndLinks.Text = "Import Objects And Links (Delimited)";
            this.ckImportObjectsAndLinks.UseVisualStyleBackColor = true;
            // 
            // ckImportObjectTypes
            // 
            this.ckImportObjectTypes.AutoSize = true;
            this.ckImportObjectTypes.Location = new System.Drawing.Point(1031, 86);
            this.ckImportObjectTypes.Margin = new System.Windows.Forms.Padding(4);
            this.ckImportObjectTypes.Name = "ckImportObjectTypes";
            this.ckImportObjectTypes.Size = new System.Drawing.Size(187, 20);
            this.ckImportObjectTypes.TabIndex = 45;
            this.ckImportObjectTypes.Text = "Import Object Types (XML)";
            this.ckImportObjectTypes.UseVisualStyleBackColor = true;
            // 
            // ckImportObjects
            // 
            this.ckImportObjects.AutoSize = true;
            this.ckImportObjects.Location = new System.Drawing.Point(1030, 114);
            this.ckImportObjects.Margin = new System.Windows.Forms.Padding(4);
            this.ckImportObjects.Name = "ckImportObjects";
            this.ckImportObjects.Size = new System.Drawing.Size(152, 20);
            this.ckImportObjects.TabIndex = 46;
            this.ckImportObjects.Text = "Import Objects (XML)";
            this.ckImportObjects.UseVisualStyleBackColor = true;
            // 
            // ckImportVertices
            // 
            this.ckImportVertices.AutoSize = true;
            this.ckImportVertices.Location = new System.Drawing.Point(1030, 143);
            this.ckImportVertices.Margin = new System.Windows.Forms.Padding(4);
            this.ckImportVertices.Name = "ckImportVertices";
            this.ckImportVertices.Size = new System.Drawing.Size(186, 20);
            this.ckImportVertices.TabIndex = 47;
            this.ckImportVertices.Text = "Import Vertices (Delimited)";
            this.ckImportVertices.UseVisualStyleBackColor = true;
            // 
            // ckImportNetworks
            // 
            this.ckImportNetworks.AutoSize = true;
            this.ckImportNetworks.Location = new System.Drawing.Point(1030, 171);
            this.ckImportNetworks.Margin = new System.Windows.Forms.Padding(4);
            this.ckImportNetworks.Name = "ckImportNetworks";
            this.ckImportNetworks.Size = new System.Drawing.Size(193, 20);
            this.ckImportNetworks.TabIndex = 48;
            this.ckImportNetworks.Text = "Import Networks (Delimited)";
            this.ckImportNetworks.UseVisualStyleBackColor = true;
            // 
            // ckImportElements
            // 
            this.ckImportElements.AutoSize = true;
            this.ckImportElements.Location = new System.Drawing.Point(1030, 199);
            this.ckImportElements.Margin = new System.Windows.Forms.Padding(4);
            this.ckImportElements.Name = "ckImportElements";
            this.ckImportElements.Size = new System.Drawing.Size(193, 20);
            this.ckImportElements.TabIndex = 49;
            this.ckImportElements.Text = "Import Elements (Delimited)";
            this.ckImportElements.UseVisualStyleBackColor = true;
            // 
            // ckImportLists
            // 
            this.ckImportLists.AutoSize = true;
            this.ckImportLists.Location = new System.Drawing.Point(1031, 228);
            this.ckImportLists.Margin = new System.Windows.Forms.Padding(4);
            this.ckImportLists.Name = "ckImportLists";
            this.ckImportLists.Size = new System.Drawing.Size(164, 20);
            this.ckImportLists.TabIndex = 50;
            this.ckImportLists.Text = "Import Lists (Delimited)";
            this.ckImportLists.UseVisualStyleBackColor = true;
            // 
            // ckImportModelPropertyValues
            // 
            this.ckImportModelPropertyValues.AutoSize = true;
            this.ckImportModelPropertyValues.Location = new System.Drawing.Point(1031, 256);
            this.ckImportModelPropertyValues.Margin = new System.Windows.Forms.Padding(4);
            this.ckImportModelPropertyValues.Name = "ckImportModelPropertyValues";
            this.ckImportModelPropertyValues.Size = new System.Drawing.Size(243, 20);
            this.ckImportModelPropertyValues.TabIndex = 51;
            this.ckImportModelPropertyValues.Text = "Import Model Property Values (XML)";
            this.ckImportModelPropertyValues.UseVisualStyleBackColor = true;
            // 
            // ckImportTablePropertyValues
            // 
            this.ckImportTablePropertyValues.AutoSize = true;
            this.ckImportTablePropertyValues.Location = new System.Drawing.Point(1030, 284);
            this.ckImportTablePropertyValues.Margin = new System.Windows.Forms.Padding(4);
            this.ckImportTablePropertyValues.Name = "ckImportTablePropertyValues";
            this.ckImportTablePropertyValues.Size = new System.Drawing.Size(241, 20);
            this.ckImportTablePropertyValues.TabIndex = 52;
            this.ckImportTablePropertyValues.Text = "Import Table Property Values (XML)";
            this.ckImportTablePropertyValues.UseVisualStyleBackColor = true;
            // 
            // ckAutoGenReadFiles
            // 
            this.ckAutoGenReadFiles.AutoSize = true;
            this.ckAutoGenReadFiles.Location = new System.Drawing.Point(426, 541);
            this.ckAutoGenReadFiles.Margin = new System.Windows.Forms.Padding(4);
            this.ckAutoGenReadFiles.Name = "ckAutoGenReadFiles";
            this.ckAutoGenReadFiles.Size = new System.Drawing.Size(211, 20);
            this.ckAutoGenReadFiles.TabIndex = 53;
            this.ckAutoGenReadFiles.Text = "Auto Generate And Read Files";
            this.ckAutoGenReadFiles.UseVisualStyleBackColor = true;
            // 
            // txtAutoGenPrefix
            // 
            this.txtAutoGenPrefix.Location = new System.Drawing.Point(573, 570);
            this.txtAutoGenPrefix.Margin = new System.Windows.Forms.Padding(4);
            this.txtAutoGenPrefix.Name = "txtAutoGenPrefix";
            this.txtAutoGenPrefix.Size = new System.Drawing.Size(279, 22);
            this.txtAutoGenPrefix.TabIndex = 55;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(431, 573);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 16);
            this.label5.TabIndex = 54;
            this.label5.Text = "AutoGeneratePrefix";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.textBox2.Location = new System.Drawing.Point(16, 208);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(153, 400);
            this.textBox2.TabIndex = 15;
            this.textBox2.Text = resources.GetString("textBox2.Text");
            // 
            // txtSQLServerConnectionString
            // 
            this.txtSQLServerConnectionString.Location = new System.Drawing.Point(426, 499);
            this.txtSQLServerConnectionString.Margin = new System.Windows.Forms.Padding(4);
            this.txtSQLServerConnectionString.Name = "txtSQLServerConnectionString";
            this.txtSQLServerConnectionString.Size = new System.Drawing.Size(548, 22);
            this.txtSQLServerConnectionString.TabIndex = 59;
            this.txtSQLServerConnectionString.Text = "Server=localhost\\SQLExpress;Database=SimioModelDB;Integrated Security=true";
            // 
            // ckDeleteJustLinks
            // 
            this.ckDeleteJustLinks.AutoSize = true;
            this.ckDeleteJustLinks.Location = new System.Drawing.Point(764, 86);
            this.ckDeleteJustLinks.Margin = new System.Windows.Forms.Padding(4);
            this.ckDeleteJustLinks.Name = "ckDeleteJustLinks";
            this.ckDeleteJustLinks.Size = new System.Drawing.Size(130, 20);
            this.ckDeleteJustLinks.TabIndex = 60;
            this.ckDeleteJustLinks.Text = "Delete Just Links";
            this.ckDeleteJustLinks.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(423, 479);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(180, 16);
            this.label6.TabIndex = 61;
            this.label6.Text = "SQLServer Connection String";
            // 
            // ckExportTablesToDB
            // 
            this.ckExportTablesToDB.AutoSize = true;
            this.ckExportTablesToDB.Location = new System.Drawing.Point(422, 368);
            this.ckExportTablesToDB.Margin = new System.Windows.Forms.Padding(4);
            this.ckExportTablesToDB.Name = "ckExportTablesToDB";
            this.ckExportTablesToDB.Size = new System.Drawing.Size(149, 20);
            this.ckExportTablesToDB.TabIndex = 62;
            this.ckExportTablesToDB.Text = "Export Tables to DB";
            this.ckExportTablesToDB.UseVisualStyleBackColor = true;
            // 
            // ckImportTableFromDB
            // 
            this.ckImportTableFromDB.AutoSize = true;
            this.ckImportTableFromDB.Location = new System.Drawing.Point(1030, 313);
            this.ckImportTableFromDB.Margin = new System.Windows.Forms.Padding(4);
            this.ckImportTableFromDB.Name = "ckImportTableFromDB";
            this.ckImportTableFromDB.Size = new System.Drawing.Size(163, 20);
            this.ckImportTableFromDB.TabIndex = 63;
            this.ckImportTableFromDB.Text = "Import Tables from DB";
            this.ckImportTableFromDB.UseVisualStyleBackColor = true;
            // 
            // ckExportTablesToCSV
            // 
            this.ckExportTablesToCSV.AutoSize = true;
            this.ckExportTablesToCSV.Location = new System.Drawing.Point(422, 340);
            this.ckExportTablesToCSV.Margin = new System.Windows.Forms.Padding(4);
            this.ckExportTablesToCSV.Name = "ckExportTablesToCSV";
            this.ckExportTablesToCSV.Size = new System.Drawing.Size(157, 20);
            this.ckExportTablesToCSV.TabIndex = 64;
            this.ckExportTablesToCSV.Text = "Export Tables to CSV";
            this.ckExportTablesToCSV.UseVisualStyleBackColor = true;
            // 
            // ckImportModelPropertiesFromCSV
            // 
            this.ckImportModelPropertiesFromCSV.AutoSize = true;
            this.ckImportModelPropertiesFromCSV.Location = new System.Drawing.Point(1028, 342);
            this.ckImportModelPropertiesFromCSV.Margin = new System.Windows.Forms.Padding(4);
            this.ckImportModelPropertiesFromCSV.Name = "ckImportModelPropertiesFromCSV";
            this.ckImportModelPropertiesFromCSV.Size = new System.Drawing.Size(210, 20);
            this.ckImportModelPropertiesFromCSV.TabIndex = 66;
            this.ckImportModelPropertiesFromCSV.Text = "Import Model Properties (CSV)";
            this.ckImportModelPropertiesFromCSV.UseVisualStyleBackColor = true;
            // 
            // ckExportLogsToDB
            // 
            this.ckExportLogsToDB.AutoSize = true;
            this.ckExportLogsToDB.Location = new System.Drawing.Point(422, 396);
            this.ckExportLogsToDB.Margin = new System.Windows.Forms.Padding(4);
            this.ckExportLogsToDB.Name = "ckExportLogsToDB";
            this.ckExportLogsToDB.Size = new System.Drawing.Size(136, 20);
            this.ckExportLogsToDB.TabIndex = 67;
            this.ckExportLogsToDB.Text = "Export Logs to DB";
            this.ckExportLogsToDB.UseVisualStyleBackColor = true;
            // 
            // ckExportModelStateDefinitions
            // 
            this.ckExportModelStateDefinitions.AutoSize = true;
            this.ckExportModelStateDefinitions.Location = new System.Drawing.Point(422, 312);
            this.ckExportModelStateDefinitions.Margin = new System.Windows.Forms.Padding(4);
            this.ckExportModelStateDefinitions.Name = "ckExportModelStateDefinitions";
            this.ckExportModelStateDefinitions.Size = new System.Drawing.Size(244, 20);
            this.ckExportModelStateDefinitions.TabIndex = 68;
            this.ckExportModelStateDefinitions.Text = "Export Model State Definitions (XML)";
            this.ckExportModelStateDefinitions.UseVisualStyleBackColor = true;
            // 
            // tableCheckedListBox
            // 
            this.tableCheckedListBox.FormattingEnabled = true;
            this.tableCheckedListBox.Location = new System.Drawing.Point(176, 41);
            this.tableCheckedListBox.Name = "tableCheckedListBox";
            this.tableCheckedListBox.Size = new System.Drawing.Size(230, 565);
            this.tableCheckedListBox.TabIndex = 69;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(173, 10);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 17);
            this.label7.TabIndex = 70;
            this.label7.Text = "Tables";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(270, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(68, 23);
            this.button1.TabIndex = 71;
            this.button1.Text = "All";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(344, 15);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(68, 23);
            this.button2.TabIndex = 72;
            this.button2.Text = "None";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ExportImportModelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1318, 636);
            this.ControlBox = false;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tableCheckedListBox);
            this.Controls.Add(this.ckExportModelStateDefinitions);
            this.Controls.Add(this.ckExportLogsToDB);
            this.Controls.Add(this.ckImportModelPropertiesFromCSV);
            this.Controls.Add(this.ckExportTablesToCSV);
            this.Controls.Add(this.ckImportTableFromDB);
            this.Controls.Add(this.ckExportTablesToDB);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ckDeleteJustLinks);
            this.Controls.Add(this.txtSQLServerConnectionString);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.txtAutoGenPrefix);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ckImportTablePropertyValues);
            this.Controls.Add(this.ckAutoGenReadFiles);
            this.Controls.Add(this.ckImportModelPropertyValues);
            this.Controls.Add(this.ckImportLists);
            this.Controls.Add(this.txtDelimiter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ckImportElements);
            this.Controls.Add(this.ckImportNetworks);
            this.Controls.Add(this.ckImportVertices);
            this.Controls.Add(this.ckImportObjects);
            this.Controls.Add(this.ckImportObjectTypes);
            this.Controls.Add(this.ckImportObjectsAndLinks);
            this.Controls.Add(this.ckImportTableData);
            this.Controls.Add(this.ckExportTablePropertyValues);
            this.Controls.Add(this.ckExportModelPropertyValues);
            this.Controls.Add(this.ckExportLists);
            this.Controls.Add(this.ckExportElements);
            this.Controls.Add(this.ckExportNetworks);
            this.Controls.Add(this.ckExportVertices);
            this.Controls.Add(this.ckExportObjects);
            this.Controls.Add(this.ckExportObjectTypes);
            this.Controls.Add(this.ckExportObjectsAndLinks);
            this.Controls.Add(this.ckExportTableData);
            this.Controls.Add(this.ckDeleteObjects);
            this.Controls.Add(this.ckDeleteTableData);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ExportImportModelForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Export Import Model";
            this.Load += new System.EventHandler(this.ExportImportModelForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtDelimiter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.CheckBox ckDeleteTableData;
        public System.Windows.Forms.CheckBox ckDeleteObjects;
        public System.Windows.Forms.CheckBox ckExportTableData;
        public System.Windows.Forms.CheckBox ckExportObjectsAndLinks;
        public System.Windows.Forms.CheckBox ckExportObjectTypes;
        public System.Windows.Forms.CheckBox ckExportObjects;
        public System.Windows.Forms.CheckBox ckExportVertices;
        public System.Windows.Forms.CheckBox ckExportNetworks;
        public System.Windows.Forms.CheckBox ckExportElements;
        public System.Windows.Forms.CheckBox ckExportLists;
        public System.Windows.Forms.CheckBox ckExportModelPropertyValues;
        public System.Windows.Forms.CheckBox ckExportTablePropertyValues;
        public System.Windows.Forms.CheckBox ckImportTableData;
        public System.Windows.Forms.CheckBox ckImportObjectsAndLinks;
        public System.Windows.Forms.CheckBox ckImportObjectTypes;
        public System.Windows.Forms.CheckBox ckImportObjects;
        public System.Windows.Forms.CheckBox ckImportVertices;
        public System.Windows.Forms.CheckBox ckImportNetworks;
        public System.Windows.Forms.CheckBox ckImportElements;
        public System.Windows.Forms.CheckBox ckImportLists;
        public System.Windows.Forms.CheckBox ckImportModelPropertyValues;
        public System.Windows.Forms.CheckBox ckImportTablePropertyValues;
        public System.Windows.Forms.CheckBox ckAutoGenReadFiles;
        public System.Windows.Forms.TextBox txtAutoGenPrefix;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox2;
        public System.Windows.Forms.TextBox txtSQLServerConnectionString;
        public System.Windows.Forms.CheckBox ckDeleteJustLinks;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.CheckBox ckExportTablesToDB;
        public System.Windows.Forms.CheckBox ckImportTableFromDB;
        public System.Windows.Forms.CheckBox ckExportTablesToCSV;
        public System.Windows.Forms.CheckBox ckImportModelPropertiesFromCSV;
        public System.Windows.Forms.CheckBox ckExportLogsToDB;
        public System.Windows.Forms.CheckBox ckExportModelStateDefinitions;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.CheckedListBox tableCheckedListBox;
    }
}