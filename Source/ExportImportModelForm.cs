using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using SimioAPI;
using SimioAPI.Extensions;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ExportImportModelAddIn
{
    public partial class ExportImportModelForm : Form
    {
        public String delimiter;
        public Boolean okButtonSelected;
        public IDesignContext _content;
        public ExportImportModelForm(IDesignContext context)
        {
            InitializeComponent();
            txtDelimiter.MouseDown += textBox1_MouseDown;
            _content = context;

            // add table names to combo
            List<String> tableNames = new List<String>();
            foreach(var table in context.ActiveModel.Tables)
            {
                tableNames.Add(table.Name);
            }
            tableNames.Sort();
            foreach (string tableName in tableNames)
            {
                tableCheckedListBox.Items.Add(tableName);
            }            
        }

        void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (txtDelimiter != null && !string.IsNullOrEmpty(txtDelimiter.Text))
            {
                txtDelimiter.SelectAll();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            okButtonSelected = false;
            this.Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            okButtonSelected = true;
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (txtDelimiter.Text.Length != 1)
            {
                txtDelimiter.Text = delimiter;
            }
        }

        private void ExportImportModelForm_Load(object sender, EventArgs e)
        {
            txtDelimiter.Text = delimiter;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < tableCheckedListBox.Items.Count; i++)
            {
                tableCheckedListBox.SetItemCheckState(i, CheckState.Checked);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < tableCheckedListBox.Items.Count; i++)
            {
                tableCheckedListBox.SetItemCheckState(i, CheckState.Unchecked);
            }
        }
    }
}
