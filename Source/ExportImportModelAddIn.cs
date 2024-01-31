using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Linq;
using SimioAPI;
using SimioAPI.Extensions;
using static System.Windows.Forms.AxHost;

namespace ExportImportModelAddIn
{
    /// <summary>
    /// History:
    /// {Dec2017/dth} Imported links were not picking up Properties. Indices were off by 1.
    /// </summary>
    public class ExportImportModelAddIn : IDesignAddIn //, IDesignAddInGuiDetails
    {        

        #region IDesignAddIn Members

        /// <summary>
        /// Property returning the name of the add-in. This name may contain any characters and is used as the display name for the add-in in the UI.
        /// </summary>
        public string Name
        {
            get { return "Export Import Model"; }
        }

        /// <summary>
        /// Property returning a short description of what the add-in does.  
        /// </summary>
        public string Description
        {
            get { return "This addin permit exporting and importing models via character delimited text files"; }
        }
        
        /// <summary>
        /// Property returning an icon to display for the add-in in the UI.
        /// </summary>
        public Image Icon
        {
            get { return Properties.Resources.Icon; }
        }

        #endregion

        private System.IO.TextWriter _writer;
        private System.IO.TextReader _reader;
        private char[] listSeparator = { '^' };
        private string folder = string.Empty;
        private string autoGenPrefix = string.Empty;
        private bool folderChecked = false;
        /// <summary>
        /// Method called when the add-in is run.
        /// </summary>
        public void Execute(IDesignContext context)
        {
            var statWin = new StatusWindow("");

            try
            {
                // Check to make sure a model has been opened in Simio
                if (context.ActiveModel == null)
                {
                    MessageBox.Show("You must have an active model to run this add-in.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ExportImportModelForm exportImporForm = new ExportImportModelForm(context);
                exportImporForm.delimiter = listSeparator[0].ToString();
                exportImporForm.txtAutoGenPrefix.Text = autoGenPrefix;
                exportImporForm.ShowDialog();
                if (exportImporForm.okButtonSelected == true)
                {
                    char[] delimiterResult = exportImporForm.txtDelimiter.Text.ToCharArray();
                    if (delimiterResult.Length > 0)
                    {
                        listSeparator[0] = delimiterResult[0];
                    }

                    autoGenPrefix = exportImporForm.txtAutoGenPrefix.Text;
                    folderChecked = false;
                    // what check box(es)
                    // Exports
                    if (exportImporForm.ckExportTablesToCSV.Checked)
                    {
                        string folderName = GetFolderName();
                        if (folderName.Length > 0)
                        {
                            //Open Status Window
                            statWin.Text = "Export Tables To CSV";
                            statWin.Show();
                            // Update Status Window
                            statWin.UpdateProgress(50);

                            context.ActiveModel.BulkUpdate(model =>
                            {
                                ExportDataTablesAsCSV(context, folderName, exportImporForm);

                            });

                            statWin.Hide();
                        }
                    }
                    if (exportImporForm.ckExportTablesToDB.Checked)
                    {
                        //Open Status Window
                        statWin.Text = "Export Tables To DB";
                        statWin.Show();
                        // Update Status Window
                        statWin.UpdateProgress(50);

                        context.ActiveModel.BulkUpdate(model =>
                        {
                            ExportTablesToDatabase(context, @exportImporForm.txtSQLServerConnectionString.Text);
                        });

                        statWin.Hide();
                    }
                    if (exportImporForm.ckExportLogsToDB.Checked)
                    {
                        //Open Status Window
                        statWin.Text = "Export Logs To DB";
                        statWin.Show();
                        // Update Status Window
                        statWin.UpdateProgress(50);

                        context.ActiveModel.BulkUpdate(model =>
                        {
                            ExportLogsToDatabase(context, @exportImporForm.txtSQLServerConnectionString.Text);
                        });

                        statWin.Hide();
                    }
                    if (exportImporForm.ckExportTableData.Checked)
                    {
                        string caption = exportImporForm.ckExportTableData.Text;
                        string fileName = GetFileName(caption.Contains("XML"), caption, exportImporForm.ckAutoGenReadFiles.Checked);
                        if (fileName.Length > 0)
                        {
                            //Open Status Window
                            statWin.Text = caption;
                            statWin.Show();
                            // Update Status Window
                            statWin.UpdateProgress(50);

                            context.ActiveModel.BulkUpdate(model =>
                            {
                                ExportDataTablesAsXML(context, fileName, exportImporForm);
                            });

                            statWin.Hide();
                        }
                        else
                        {
                            throw new Exception("Canceled");
                        }
                    }
                    if (exportImporForm.ckExportModelPropertyValues.Checked)
                    {
                        string caption = exportImporForm.ckExportModelPropertyValues.Text;
                        string fileName = GetFileName(caption.Contains("XML"), caption, exportImporForm.ckAutoGenReadFiles.Checked);
                        if (fileName.Length > 0)
                        {
                            //Open Status Window
                            statWin.Text = caption;
                            statWin.Show();
                            // Update Status Window
                            statWin.UpdateProgress(50);

                            context.ActiveModel.BulkUpdate(model =>
                            {
                                ExportModelPropertyValuesAsXML(context, fileName);
                            });

                            statWin.Hide();
                        }
                        else
                        {
                            throw new Exception("Canceled");
                        }
                    }
                    if (exportImporForm.ckExportTablePropertyValues.Checked)
                    {
                        string caption = exportImporForm.ckExportTablePropertyValues.Text;
                        string fileName = GetFileName(caption.Contains("XML"), caption, exportImporForm.ckAutoGenReadFiles.Checked);
                        if (fileName.Length > 0)
                        {
                            //Open Status Window
                            statWin.Text = caption;
                            statWin.Show();
                            // Update Status Window
                            statWin.UpdateProgress(50);

                            context.ActiveModel.BulkUpdate(model =>
                            {
                                ExportTablePropertiesAsXML(context, fileName);
                            });

                            statWin.Hide();
                        }
                        else
                        {
                            throw new Exception("Canceled");
                        }
                    }
                    if (exportImporForm.ckExportModelStateDefinitions.Checked)
                    {
                        string caption = exportImporForm.ckExportTablePropertyValues.Text;
                        string fileName = GetFileName(caption.Contains("XML"), caption, exportImporForm.ckAutoGenReadFiles.Checked);
                        if (fileName.Length > 0)
                        {
                            //Open Status Window
                            statWin.Text = caption;
                            statWin.Show();
                            // Update Status Window
                            statWin.UpdateProgress(50);

                            context.ActiveModel.BulkUpdate(model =>
                            {
                                ExportModelStateValuesAsXML(context, fileName);
                            });

                            statWin.Hide();
                        }
                        else
                        {
                            throw new Exception("Canceled");
                        }
                    }
                    if (exportImporForm.ckExportObjectTypes.Checked)
                    {
                        string caption = exportImporForm.ckExportObjectTypes.Text;
                        string fileName = GetFileName(caption.Contains("XML"), caption, exportImporForm.ckAutoGenReadFiles.Checked);
                        if (fileName.Length > 0)
                        {
                            //Open Status Window
                            statWin.Text = caption;
                            statWin.Show();
                            // Update Status Window
                            statWin.UpdateProgress(50);

                            context.ActiveModel.BulkUpdate(model =>
                            {
                                ExportObjectTypesAsXML(context, fileName);
                            });

                            statWin.Hide();
                        }
                        else
                        {
                            throw new Exception("Canceled");
                        }
                    }
                    if (exportImporForm.ckExportObjects.Checked)
                    {
                        string caption = exportImporForm.ckExportObjects.Text;
                        string fileName = GetFileName(caption.Contains("XML"), caption, exportImporForm.ckAutoGenReadFiles.Checked);
                        if (fileName.Length > 0)
                        {
                            //Open Status Window
                            statWin.Text = caption;
                            statWin.Show();
                            // Update Status Window
                            statWin.UpdateProgress(50);

                            context.ActiveModel.BulkUpdate(model =>
                            {
                                ExportObjectsAsXML(context, fileName);
                            });

                            statWin.Hide();
                        }
                        else
                        {
                            throw new Exception("Canceled");
                        }
                    }
                    if (exportImporForm.ckExportObjectsAndLinks.Checked)
                    {
                        string caption = exportImporForm.ckExportObjectsAndLinks.Text;
                        string fileName = GetFileName(caption.Contains("XML"), caption, exportImporForm.ckAutoGenReadFiles.Checked);
                        if (fileName.Length > 0)
                        {
                            bool includeProperties = false;
                            DialogResult dr = MessageBox.Show("Include Properties In Export", "Include Properties", MessageBoxButtons.YesNoCancel);

                            if (dr == DialogResult.Cancel)
                            {
                                MessageBox.Show("Canceled by user.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            else if (dr == DialogResult.Yes)
                            {
                                includeProperties = true;
                            }

                            //Open Status Window
                            statWin.Text = caption;
                            statWin.Show();
                            // Update Status Window
                            statWin.UpdateProgress(50);

                            context.ActiveModel.BulkUpdate(model =>
                            {
                                ExportObjectsAndLinks(context, fileName, includeProperties);
                            });

                            statWin.Hide();
                        }
                        else
                        {
                            throw new Exception("Canceled");
                        }
                    }
                    if (exportImporForm.ckExportLists.Checked)
                    {
                        string caption = exportImporForm.ckExportLists.Text;
                        string fileName = GetFileName(caption.Contains("XML"), caption, exportImporForm.ckAutoGenReadFiles.Checked);
                        if (fileName.Length > 0)
                        {
                            //Open Status Window
                            statWin.Text = caption;
                            statWin.Show();
                            // Update Status Window
                            statWin.UpdateProgress(50);

                            context.ActiveModel.BulkUpdate(model =>
                            {
                                ExportLists(context, fileName);
                            });

                            statWin.Hide();
                        }
                        else
                        {
                            throw new Exception("Canceled");
                        }
                    }
                    if (exportImporForm.ckExportNetworks.Checked)
                    {
                        string caption = exportImporForm.ckExportNetworks.Text;
                        string fileName = GetFileName(caption.Contains("XML"), caption, exportImporForm.ckAutoGenReadFiles.Checked);
                        if (fileName.Length > 0)
                        {
                            //Open Status Window
                            statWin.Text = caption;
                            statWin.Show();
                            // Update Status Window
                            statWin.UpdateProgress(50);

                            context.ActiveModel.BulkUpdate(model =>
                            {
                                ExportNetworks(context, fileName);
                            });

                            statWin.Hide();
                        }
                        else
                        {
                            throw new Exception("Canceled");
                        }
                    }
                    if (exportImporForm.ckExportElements.Checked)
                    {
                        string caption = exportImporForm.ckExportElements.Text;
                        string fileName = GetFileName(caption.Contains("XML"), caption, exportImporForm.ckAutoGenReadFiles.Checked);
                        if (fileName.Length > 0)
                        {
                            //Open Status Window
                            statWin.Text = caption;
                            statWin.Show();
                            // Update Status Window
                            statWin.UpdateProgress(50);

                            context.ActiveModel.BulkUpdate(model =>
                            {
                                ExportElements(context, fileName);
                            });

                            statWin.Hide();
                        }
                        else
                        {
                            throw new Exception("Canceled");
                        }
                    }
                    if (exportImporForm.ckExportVertices.Checked)
                    {
                        string caption = exportImporForm.ckExportVertices.Text;
                        string fileName = GetFileName(caption.Contains("XML"), caption, exportImporForm.ckAutoGenReadFiles.Checked);
                        if (fileName.Length > 0)
                        {
                            //Open Status Window
                            statWin.Text = caption;
                            statWin.Show();
                            // Update Status Window
                            statWin.UpdateProgress(50);

                            context.ActiveModel.BulkUpdate(model =>
                            {
                                ExportVertices(context, fileName);
                            });

                            statWin.Hide();
                        }
                        else
                        {
                            throw new Exception("Canceled");
                        }
                    }
                    // Deletes
                    if (exportImporForm.ckDeleteTableData.Checked)
                    {
                        //Open Status Window
                        statWin.Text = "Delete Table Data";
                        statWin.Show();
                        // Update Status Window
                        statWin.UpdateProgress(50);

                        context.ActiveModel.BulkUpdate(model =>
                        {
                            DeleteTableData(context, exportImporForm);
                        });

                        statWin.Hide();
                    }
                    if (exportImporForm.ckDeleteObjects.Checked)
                    {
                        //Open Status Window
                        statWin.Text = "Delete Objects";
                        statWin.Show();
                        // Update Status Window
                        statWin.UpdateProgress(25);

                        context.ActiveModel.BulkUpdate(model =>
                        {
                            DeleteObjects(context, false);
                        });

                        statWin.Hide();
                    }
                    if (exportImporForm.ckDeleteJustLinks.Checked)
                    {
                        //Open Status Window
                        statWin.Text = "Delete Just Links";
                        statWin.Show();
                        // Update Status Window
                        statWin.UpdateProgress(25);

                        context.ActiveModel.BulkUpdate(model =>
                        {
                            DeleteObjects(context, true);
                        });

                        statWin.Hide();
                    }
                    // Imports
                    if (exportImporForm.ckImportTableFromDB.Checked)
                    {
                        //Open Status Window
                        statWin.Text = "Import Tables From DB";
                        statWin.Show();
                        // Update Status Window
                        statWin.UpdateProgress(50);

                        context.ActiveModel.BulkUpdate(model =>
                        {
                            ImportTablesFromDatabase(context, @exportImporForm.txtSQLServerConnectionString.Text);
                        });

                        statWin.Hide();
                    }                  
                    if (exportImporForm.ckImportTableData.Checked)
                    {
                        string caption = exportImporForm.ckImportTableData.Text;
                        string fileName = GetFileName(caption.Contains("XML"), caption, exportImporForm.ckAutoGenReadFiles.Checked);
                        if (fileName.Length > 0)
                        {
                            //Open Status Window
                            statWin.Text = caption;
                            statWin.Show();
                            // Update Status Window
                            statWin.UpdateProgress(50);

                            context.ActiveModel.BulkUpdate(model =>
                            {
                                ImportDataTablesWithXML(context, fileName);
                            });

                            statWin.Hide();
                        }
                        else
                        {
                            throw new Exception("Canceled");
                        }
                    }                  
                    if (exportImporForm.ckImportModelPropertyValues.Checked)
                    {
                        string caption = exportImporForm.ckImportModelPropertyValues.Text;
                        string fileName = GetFileName(caption.Contains("XML"), caption, exportImporForm.ckAutoGenReadFiles.Checked);
                        if (fileName.Length > 0)
                        {
                            //Open Status Window
                            statWin.Text = caption;
                            statWin.Show(); 
                            // Update Status Window
                            statWin.UpdateProgress(50);

                        context.ActiveModel.BulkUpdate(model =>
                        {
                            ImportModelPropertyValuesWithXML(context, fileName);
                        });                        

                        statWin.Hide();
                        }
                        else
                        {
                            throw new Exception("Canceled");
                        }
                    }           
                    if (exportImporForm.ckImportTablePropertyValues.Checked)
                    {
                        string caption = exportImporForm.ckImportTablePropertyValues.Text;
                        string fileName = GetFileName(caption.Contains("XML"), caption, exportImporForm.ckAutoGenReadFiles.Checked);
                        if (fileName.Length > 0)
                        {
                            //Open Status Window
                            statWin.Text = caption;
                            statWin.Show();
                            // Update Status Window
                            statWin.UpdateProgress(50);

                            context.ActiveModel.BulkUpdate(model =>
                            {
                                ImportTablePropertyValuesWithXML(context, fileName);
                            });

                            statWin.Hide();
                        }
                        else
                        {
                            throw new Exception("Canceled");
                        }
                    }                   
                    if (exportImporForm.ckImportObjects.Checked)
                    {
                        string caption = exportImporForm.ckImportObjects.Text;
                        string fileName = GetFileName(caption.Contains("XML"), caption, exportImporForm.ckAutoGenReadFiles.Checked);
                        if (fileName.Length > 0)
                        {
                            //Open Status Window
                            statWin.Text = caption;
                            statWin.Show();
                            // Update Status Window
                            statWin.UpdateProgress(50);

                            context.ActiveModel.BulkUpdate(model =>
                            {
                                ImportObjectsWithXML(context, fileName);
                            });

                            statWin.Hide();
                        }
                        else
                        {
                            throw new Exception("Canceled");
                        }

                    }
                    if (exportImporForm.ckImportObjectsAndLinks.Checked)
                    {
                        string caption = exportImporForm.ckImportObjectsAndLinks.Text;
                        string fileName = GetFileName(caption.Contains("XML"), caption, exportImporForm.ckAutoGenReadFiles.Checked);
                        if (fileName.Length > 0)
                        {
                            //Open Status Window
                            statWin.Text = caption;
                            statWin.Show();
                            // Update Status Window
                            statWin.UpdateProgress(50);

                            context.ActiveModel.BulkUpdate(model =>
                            {
                                ImportObjects(context, fileName);
                            });

                            statWin.UpdateProgress(75);

                            context.ActiveModel.BulkUpdate(model =>
                            {
                                ImportLinks(context, fileName);
                            });

                            statWin.Hide();
                        }
                        else
                        {
                            throw new Exception("Canceled");
                        }
                    }                    
                    if (exportImporForm.ckImportLists.Checked)
                    {
                        string caption = exportImporForm.ckImportLists.Text;
                        string fileName = GetFileName(caption.Contains("XML"), caption, exportImporForm.ckAutoGenReadFiles.Checked);
                        if (fileName.Length > 0)
                        {
                            //Open Status Window
                            statWin.Text = caption;
                            statWin.Show();
                            // Update Status Window
                            statWin.UpdateProgress(50);

                            context.ActiveModel.BulkUpdate(model =>
                            {
                                ImportLists(context, fileName);
                            });

                            statWin.Hide();
                        }
                        else
                        {
                            throw new Exception("Canceled");
                        }
                    }                    
                    if (exportImporForm.ckImportNetworks.Checked)
                    {
                        string caption = exportImporForm.ckImportNetworks.Text;
                        string fileName = GetFileName(caption.Contains("XML"), caption, exportImporForm.ckAutoGenReadFiles.Checked);
                        if (fileName.Length > 0)
                        {
                            //Open Status Window
                            statWin.Text = caption;
                            statWin.Show();
                            // Update Status Window
                            statWin.UpdateProgress(50);

                            context.ActiveModel.BulkUpdate(model =>
                            {
                                ImportNetworks(context, fileName);
                            });

                            statWin.Hide();
                        }
                        else
                        {
                            throw new Exception("Canceled");
                        }
                    }
                    
                    if (exportImporForm.ckImportElements.Checked)
                    {
                        string caption = exportImporForm.ckImportNetworks.Text;
                        string fileName = GetFileName(caption.Contains("XML"), caption, exportImporForm.ckAutoGenReadFiles.Checked);
                        if (fileName.Length > 0)
                        {
                            //Open Status Window
                            statWin.Text = caption;
                            statWin.Show();
                            // Update Status Window
                            statWin.UpdateProgress(50);

                            context.ActiveModel.BulkUpdate(model =>
                            {
                                ImportElements(context, fileName);
                            });

                            statWin.Hide();
                        }
                        else
                        {
                            throw new Exception("Canceled");
                        }
                    }
                    
                    if (exportImporForm.ckImportVertices.Checked)
                    {
                        string caption = exportImporForm.ckImportVertices.Text;
                        string fileName = GetFileName(caption.Contains("XML"), caption, exportImporForm.ckAutoGenReadFiles.Checked);
                        if (fileName.Length > 0)
                        {
                            //Open Status Window
                            statWin.Text = caption;
                            statWin.Show();
                            // Update Status Window
                            statWin.UpdateProgress(50);

                            context.ActiveModel.BulkUpdate(model =>
                            {
                                ImportVertices(context, fileName);
                            });

                            statWin.Hide();
                        }
                        else
                        {
                            throw new Exception("Canceled");
                        }
                    }

                    if (exportImporForm.ckImportModelPropertiesFromCSV.Checked)
                    {
                        string caption = exportImporForm.ckImportModelPropertiesFromCSV.Text;
                        string fileName = GetFileName(caption.Contains("XML"), caption, exportImporForm.ckAutoGenReadFiles.Checked);
                        if (fileName.Length > 0)
                        {
                            //Open Status Window
                            statWin.Text = caption;
                            statWin.Show();
                            // Update Status Window
                            statWin.UpdateProgress(50);

                            context.ActiveModel.BulkUpdate(model =>
                            {
                                ImportModelPropertiesWithCSV(context, fileName);
                            });

                            statWin.Hide();
                        }
                        else
                        {
                            throw new Exception("Canceled");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                statWin.Close();
                if (ex.Message != "Canceled")
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
        }

        private void ExportDataTablesAsCSV(IDesignContext context, String folderName, ExportImportModelForm f)
        {            
            bool useFormDelimiter = false;
            var dr1 = MessageBox.Show("Use form delimiter instead of using comma?", "Use Form Delimiter?", MessageBoxButtons.YesNo);
            if (dr1 == DialogResult.Yes)
            {
                useFormDelimiter = true;
            }
            bool includeDataType = false;
            var dr2 = MessageBox.Show("Include data unit in column name?", "Include data units?", MessageBoxButtons.YesNo);
            if (dr2 == DialogResult.Yes)
            {
                includeDataType = true;
            }

            // Create the log message list
            List<String> logMessages = new List<String>();

            var dataSet = new DataSet();

            var sortedListOfTables = context.ActiveModel.Tables.OrderBy(r => r.Name).ToList();

            for (int x = 0; x < f.tableCheckedListBox.CheckedItems.Count; x++)
            {
                // for each table
                foreach (ITable table in sortedListOfTables)
                {
                    if (table.Name == f.tableCheckedListBox.CheckedItems[x].ToString())
                    {
                        var dataTable = ConvertTableToDataTable(table, includeDataType);
                        dataTable.TableName = table.Name;
                        dataSet.Tables.Add(dataTable);
                        logMessages.Add(string.Format("Writing {0} ", table.Name));
                    }
                }
            }

            //  Save Table
            foreach (DataTable dataTable in dataSet.Tables)
            {
                string filePathAndName = folderName + "\\" + dataTable.TableName + ".csv";
                DataTableToCSV(dataTable, filePathAndName, useFormDelimiter);
            }

            //show log messages
            string msg = string.Join(System.Environment.NewLine, logMessages.ToArray());
            if (msg.Length > 32000) msg = msg.Substring(0, 32000);
            MessageBox.Show(msg, "Exported Table Data To CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void DataTableToCSV(DataTable dt, string filePathAndName, bool useFormDelimiter)
        {
            string delimiter = ",";
            if (useFormDelimiter == true) delimiter = listSeparator[0].ToString();               
            var header = String.Join(
                delimiter,
                dt.Columns.Cast<DataColumn>().Select(dc => dc.ColumnName));

            var rows =
                from dr in dt.Rows.Cast<DataRow>()
                select String.Join(
                    delimiter,
                    from dc in dt.Columns.Cast<DataColumn>()
                    let t1 = Convert.IsDBNull(dr[dc]) ? "" : dr[dc].ToString()
                    let t2 = t1.Contains(delimiter) ? String.Format("\"{0}\"", t1) : t1
                    select t2);

            using (var sw = new StreamWriter(filePathAndName))
            {
                sw.WriteLine(header);
                foreach (var row in rows)
                {
                    sw.WriteLine(row);
                }
                sw.Close();
            }       
        }

        private void ExportLogsToDatabase(IDesignContext context, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Int32 count = 0;
                connection.Open();

                var dt = SimioAPIHelpers.LogHelpers.ConvertLogToDataTable(context.ActiveModel, context.ActiveModel.Plan.ResourceUsageLog.LogExpressions, context.ActiveModel.Plan.ResourceUsageLog, "ResourceUsageLogs");

                string strCheckTable = String.Format("IF OBJECT_ID('{0}', 'U') IS NOT NULL SELECT 'true' ELSE SELECT 'false'", dt.TableName);

                var cmd = new SqlCommand(strCheckTable, connection);
                cmd.CommandType = CommandType.Text;

                if (Convert.ToBoolean(cmd.ExecuteScalar()))
                {
                    string sqlDelete = "DROP TABLE " + dt.TableName;
                    cmd = new SqlCommand(sqlDelete, connection);
                    cmd.ExecuteNonQuery();
                }
                //else
                //{
                bool firstColumn = true;
                string sqlCreate = "CREATE TABLE " + dt.TableName + "(";
                foreach (DataColumn col in dt.Columns)
                {
                    if (firstColumn == false)
                    {
                        sqlCreate += ", ";
                    }
                    else
                    {
                        firstColumn = false;
                    }
                    sqlCreate += "[" + col.ToString() + "] nvarchar(max)";
                }
                if (firstColumn == false)
                {
                    sqlCreate += ", Id int not null identity(1, 1) primary key)";
                }
                else
                {
                    sqlCreate += "Id int not null identity(1, 1) primary key)";
                }
                cmd = new SqlCommand(sqlCreate, connection);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(sqlCreate, ex.Message);
                }

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.DestinationTableName = dt.TableName;
                    try
                    {
                        bulkCopy.WriteToServer(dt);
                        count++;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                MessageBox.Show("Done..Number Of Logs Written - " + count.ToString());
            }
        }
        private void ExportTablesToDatabase(IDesignContext context, string connectionString)
        {            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Int32 count = 0;
                connection.Open();

                var sortedListOfTables = context.ActiveModel.Tables.OrderBy(r => r.Name).ToList();

                // for each table
                foreach (ITable table in sortedListOfTables)
                {
                    var dt = ConvertTableToDataTable(table, false);

                    string strCheckTable = String.Format("IF OBJECT_ID('{0}', 'U') IS NOT NULL SELECT 'true' ELSE SELECT 'false'", table.Name);

                    var cmd = new SqlCommand(strCheckTable, connection);
                    cmd.CommandType = CommandType.Text;

                    if (Convert.ToBoolean(cmd.ExecuteScalar()))
                    {
                        string sqlDelete = "DROP TABLE " + table.Name;
                        cmd = new SqlCommand(sqlDelete, connection);
                        cmd.ExecuteNonQuery();
                        //string sqlTrunc = "TRUNCATE TABLE " + table.Name;
                        //cmd = new SqlCommand(sqlTrunc, connection);
                        //cmd.ExecuteNonQuery();
                    }
                    //else
                    //{
                    bool firstColumn = true;
                    string sqlCreate = "CREATE TABLE " + table.Name + "(";
                    foreach (var col in table.Columns)
                    {
                        if (col.Name != "Id")
                        {
                            if (firstColumn == false)
                            {
                                sqlCreate += ", ";
                            }
                            else
                            {
                                firstColumn = false;
                            }
                            sqlCreate += "[" + col.Name + "] nvarchar(max) Default '" + col.DefaultString + "'";
                        }
                    }
                    foreach (var stateCol in table.StateColumns)
                    {
                        if (stateCol.Name != "Id")
                        {
                            if (firstColumn == false)
                            {
                                sqlCreate += ", ";
                            }
                            else
                            {
                                firstColumn = false;
                            }
                            sqlCreate += "[" + stateCol.Name + "] nvarchar(max)";
                        }
                    }
                    if (firstColumn == false)
                    {
                        sqlCreate += ", Id int not null identity(1, 1) primary key)";
                    }
                    else
                    {
                        sqlCreate += "Id int not null identity(1, 1) primary key)";
                    }
                    cmd = new SqlCommand(sqlCreate, connection);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(sqlCreate, ex.Message);
                    }

                    //}
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                    {
                        bulkCopy.DestinationTableName = table.Name;
                        try
                        {
                            bulkCopy.WriteToServer(dt);
                            count++;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }

                MessageBox.Show("Done..Number Of Tables Written - " + count.ToString());
            }
        }

       

        private void ImportTablesFromDatabase(IDesignContext context, string connectionString)
        {
            Int32 count = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (var table in context.ActiveModel.Tables)
                {
                    string strCheckTable = String.Format("IF OBJECT_ID('{0}', 'U') IS NOT NULL SELECT 'true' ELSE SELECT 'false'", table.Name);

                    var cmd = new SqlCommand(strCheckTable, connection);
                    cmd.CommandType = CommandType.Text;

                    if (Convert.ToBoolean(cmd.ExecuteScalar()))
                    {
                        string query = "select * from " + table.Name;
                        cmd = new SqlCommand(query, connection);

                        // create data adapter
                        var da = new SqlDataAdapter(cmd);
                        // this will query your database and return the result to your datatable
                        var dt = new DataTable();
                        da.Fill(dt);
                        da.Dispose();

                        table.Rows.Clear();

                        foreach (DataRow dataRow in dt.Rows)
                        {
                            //  iterate thorugh data
                            var row = table.Rows.Create();
                            foreach (var prop in row.Properties)
                            {
                                string propertyName = prop.Name;
                                try
                                {
                                    var value = dataRow[prop.Name];
                                    if (value != null) prop.Value = value.ToString();
                                }
                                catch
                                { }
                            }
                        }
                        count++;
                    }
                }

                MessageBox.Show("Done..Number Of Tables Written - " + count.ToString());
            }

        }

        private string GetFolderName()
        {
            var getFolder = new FolderBrowserDialog();
            if (folder == string.Empty) getFolder.SelectedPath = System.AppDomain.CurrentDomain.BaseDirectory;
            else getFolder.SelectedPath = folder;
            if (getFolder.ShowDialog() == DialogResult.Cancel || getFolder.SelectedPath.Length == 0)
            {
                MessageBox.Show("Canceled by user.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return String.Empty;
            }
            else
            {
                folder = getFolder.SelectedPath;
                folderChecked = true;
            }
            return folder;
        }

        private string GetFileName(Boolean xmlType, String caption, Boolean autoGenFiles)
        {            
            if (autoGenFiles)
            {
                if (folder == string.Empty || folderChecked == false)
                {
                    var getFolder = new FolderBrowserDialog();
                    if (folder == string.Empty) getFolder.SelectedPath = System.AppDomain.CurrentDomain.BaseDirectory;
                    else getFolder.SelectedPath = folder;
                    if (getFolder.ShowDialog() == DialogResult.Cancel || getFolder.SelectedPath.Length == 0)
                    {
                        MessageBox.Show("Canceled by user.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return String.Empty;
                    }
                    else
                    {
                        folder = getFolder.SelectedPath;
                        folderChecked = true;
                    }
                }

                // remove export
                caption = caption.Replace("Export", string.Empty);

                // remove import
                caption = caption.Replace("Import", string.Empty);

                // remove spaces
                caption = caption.Replace(" ", string.Empty);

                // start file name
                string filename = autoGenPrefix;

                // find first part of caption
                filename = filename + caption.Substring(0, caption.IndexOf('('));
                // if XML
                if (xmlType) filename = filename + ".xml";
                else filename = filename + ".txt";

                // create file
                if (File.Exists(filename) == false)
                {
                    File.Create(filename).Dispose();
                }

                return folder + "\\" + filename;
            }
            else
            {
                // Open the file.  Return immediately if the user cancels the file open dialog
                var getFile = new OpenFileDialog();
                getFile.Title = caption;
                if (xmlType)
                {
                    getFile.Filter = "XML or Text Files(*.xml)|*.xml;*.txt";
                }
                else
                {
                    getFile.Filter = "CSV or Text Files(*.csv, *.txt)|*.csv;*.txt";
                }
                if (getFile.ShowDialog() == DialogResult.Cancel || getFile.FileName.Length == 0)
                {
                    MessageBox.Show("Canceled by user.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return String.Empty;
                }
                else
                {
                    return getFile.FileName;
                }
            }        
        }

        private void DeleteObjects(IDesignContext context, Boolean justLinks)
        {
            // Create the log message list
            List<String> logMessages = new List<String>();

            // build up list
            ArrayList ioAL = new ArrayList();
            foreach (IIntelligentObject intellObj in context.ActiveModel.Facility.IntelligentObjects)
            {
                ioAL.Add(intellObj);                
            }
                        
            // remove objects
            foreach (IIntelligentObject intelObj in ioAL)
            {
                try
                {
                    ILinkObject link = intelObj as ILinkObject;

                    if (link != null || justLinks == false)
                    {
                        string deletedObjStr = intelObj.ObjectName;
                        context.ActiveModel.Facility.IntelligentObjects.Remove(intelObj);
                        logMessages.Add(string.Format("Reading {0} ", deletedObjStr));
                    }
                }
                catch { }
            }

            //show log messages
            string msg = string.Join(System.Environment.NewLine, logMessages.ToArray());
            if (msg.Length > 32000) msg = msg.Substring(0, 32000);
            MessageBox.Show(msg, "Delete Objects", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }        

        private void ExportLists(IDesignContext context, String filePath)
        {
            // Create the log message list
            List<String> logMessages = new List<String>();

            StartWriter(filePath);

            foreach (INamedList namedList in context.ActiveModel.NamedLists)
            {
                string listData = namedList.Name + listSeparator[0].ToString() + namedList.Description;       
                string writeout = "";               
                logMessages.Add(string.Format("Writing {0} ", namedList.Name));

                foreach (IRow row in namedList.Rows)
                {                    
                    foreach (IProperty rowProp in row.Properties)
                    {
                        writeout = listData + listSeparator[0].ToString() + rowProp.Name + listSeparator[0].ToString() + rowProp.Value;
                    }

                    _writer.WriteLine(writeout);
                }
            }

            if (_writer == null)
            {
                _writer.Flush();
            }

            EndWriter();

            //show log messages
            string msg = string.Join(System.Environment.NewLine, logMessages.ToArray());
            if (msg.Length > 32000) msg = msg.Substring(0, 32000);
            MessageBox.Show(msg, "ExportedLists", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void ExportNetworks(IDesignContext context, String filePath)
        {
            // Create the log message list
            List<String> logMessages = new List<String>();

            StartWriter(filePath);

            var sortedListOfElements = context.ActiveModel.Elements.OrderBy(r => r.ObjectName).ToList();

            foreach (var eleObj in sortedListOfElements)
            {                
                if (eleObj.TypeName == "Network")
                {
                    INetworkElementObject netEleObj = (INetworkElementObject)eleObj;
                    logMessages.Add(string.Format("Writing {0} ", netEleObj.ObjectName));
                    foreach (ILinkObject link in netEleObj.Links)
                    {
                        string writeout = netEleObj.ObjectName + listSeparator[0].ToString() + link.ObjectName;
                        _writer.WriteLine(writeout);
                    }
                }
            }

            if (_writer == null)
            {
                _writer.Flush();
            }

            EndWriter();

            //show log messages
            string msg = string.Join(System.Environment.NewLine, logMessages.ToArray());
            if (msg.Length > 32000) msg = msg.Substring(0, 32000);
            MessageBox.Show(msg, "ExportedNetworks", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void ImportLists(IDesignContext context, String filePath)
        {
            // Create the log message list
            List<String> logMessages = new List<String>();

            StartReader(filePath);

            string line = string.Empty;

            string prvListjStr = "";

            // Import Objects
            while (!String.IsNullOrEmpty((line = _reader.ReadLine())))
            {
                string[] lineArr = line.Split(listSeparator[0]);

                if (lineArr.Length > 3)
                {
                    if (prvListjStr != lineArr[0])
                    {
                        logMessages.Add(string.Format("Reading {0} ", lineArr[0]));                        
                    }

                    // Add list                    
                    var namedList = context.ActiveModel.NamedLists[lineArr[0]];
                    if (namedList == null)
                    {
                        if (lineArr[2] == "Object") namedList = context.ActiveModel.NamedLists.AddObjectList(lineArr[0]);
                        if (lineArr[2] == "Transporter") namedList = context.ActiveModel.NamedLists.AddTransporterList(lineArr[0]);
                        if (lineArr[2] == "Node") namedList = context.ActiveModel.NamedLists.AddNodeList(lineArr[0]);
                        if (lineArr[2] == "String") namedList = context.ActiveModel.NamedLists.AddStringList(lineArr[0]);
                        if (namedList == null)
                        {
                            DialogResult dr = MessageBox.Show(string.Format("Error creating list {0} of type {1}...", lineArr[0], lineArr[2]), null, MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            if (dr == DialogResult.OK) continue;
                            else
                            {
                                return;
                            }
                        }
                        namedList.Description = lineArr[1];
                    }
                    else  
                    {
                        // If first
                        if (prvListjStr != lineArr[0])
                        {
                            namedList.Description = lineArr[1];
                            namedList.Rows.Clear();
                        }
                    }
                    // set prvListjStr
                    prvListjStr = lineArr[0];

                    // Add property value
                    var row = namedList.Rows.Create();
                    row.Properties[0].Value = lineArr[3];                    
                }
            }

            EndReader();

            //show log messages
            string msg = string.Join(System.Environment.NewLine, logMessages.ToArray());
            if (msg.Length > 32000) msg = msg.Substring(0, 32000);
            MessageBox.Show(msg, "Imported Lists", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void ImportNetworks(IDesignContext context, String filePath)
        {
            // Create the log message list
            List<String> logMessages = new List<String>();

            StartReader(filePath);

            string line = string.Empty;

            string prvListjStr = "";

            // Import Network
            while (!String.IsNullOrEmpty((line = _reader.ReadLine())))
            {
                string[] lineArr = line.Split(listSeparator[0]);

                if (lineArr.Length > 1)
                {
                    if (prvListjStr != lineArr[0])
                    {
                        logMessages.Add(string.Format("Reading {0} ", lineArr[0]));
                    }

                    // Add network                    
                    var element = context.ActiveModel.Elements[lineArr[0]];
                    if (element == null)
                    {
                        element = context.ActiveModel.Elements.CreateElement("Network");
                        element.ObjectName = lineArr[0];
                    }

                    var netElementObj = element as INetworkElementObject;
                    if (netElementObj != null)
                    {
                        // If first, clear
                        if (prvListjStr != lineArr[0])
                        {
                            prvListjStr = lineArr[0];
                            netElementObj.Links.Clear();
                        }

                        var intellObj = context.ActiveModel.Facility.IntelligentObjects[lineArr[1]];                        
                        ILinkObject linkObj = intellObj as ILinkObject;
                        if (linkObj != null)
                        {
                            linkObj.Networks.Add(netElementObj);
                        }
                    }                   
                }
            }

            EndReader();

            //show log messages
            string msg = string.Join(System.Environment.NewLine, logMessages.ToArray());
            if (msg.Length > 32000) msg = msg.Substring(0, 32000);
            MessageBox.Show(msg, "Imported Networks", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void ExportVertices(IDesignContext context, String filePath)
        {
            // Create the log message list
            List<String> logMessages = new List<String>();

            StartWriter(filePath);

            var sortedListOfObjects = context.ActiveModel.Facility.IntelligentObjects.OrderBy(r => r.ObjectName).ToList();

            foreach (IIntelligentObject intellObj in sortedListOfObjects)
            {                   
                ILinkObject link = intellObj as ILinkObject;

                if (link != null)
                {
                    Boolean verticesFound = false;
                    if (link.InteriorVertices != null && link.InteriorVertices.Count > 0)
                    {
                        foreach (FacilityLocation fl in link.InteriorVertices)
                        {
                            string objectdata = intellObj.ObjectName + listSeparator[0].ToString() + fl.X.ToString() + listSeparator[0].ToString() + fl.Y.ToString() + listSeparator[0].ToString() + fl.Z.ToString();

                            if (verticesFound == false)
                            {
                                verticesFound = true;
                                logMessages.Add(string.Format("Writing {0} ", intellObj.ObjectName));
                            }

                            _writer.WriteLine(objectdata);
                        }
                    }
                }               
            }

            if (_writer == null)
            {
                _writer.Flush();
            }

            EndWriter();

            //show log messages
            string msg = string.Join(System.Environment.NewLine, logMessages.ToArray());
            if (msg.Length > 32000) msg = msg.Substring(0, 32000);
            MessageBox.Show(msg, "Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ImportLinks(IDesignContext context, String filePath)
        {
            // Create the log message list
            List<String> logMessages = new List<String>();

            StartReader(filePath);

            string line = string.Empty;

            string prvIntellObjStr = "";
            string prevErrorMsg = "";

            // Import Objects
            while (!String.IsNullOrEmpty((line = _reader.ReadLine())))
            {
                string[] lineArr = line.Split(listSeparator[0]);

                if (lineArr.Length > 1)
                {
                    // is a link
                    if (lineArr[8] == "1")
                    {
                        if (prvIntellObjStr != lineArr[1])
                        {
                            prvIntellObjStr = lineArr[1];
                            logMessages.Add(string.Format("Reading {0} ", lineArr[1]));
                        }
                        FacilitySize fs = new FacilitySize(Convert.ToDouble(lineArr[5]), Convert.ToDouble(lineArr[6]), Convert.ToDouble(lineArr[7]));

                        var fromNode = context.ActiveModel.Facility.IntelligentObjects[lineArr[9]] as INodeObject;
                        if (fromNode == null)
                        {
                            DialogResult dr = MessageBox.Show(string.Format("Error finding from node name {0} on class {1} of link {2}", lineArr[9], lineArr[0], lineArr[1]), null, MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            if (dr == DialogResult.OK) continue;
                            else
                            {
                                return;
                            }
                        }

                        var toNode = context.ActiveModel.Facility.IntelligentObjects[lineArr[10]] as INodeObject;
                        if (toNode == null)
                        {
                            DialogResult dr = MessageBox.Show(string.Format("Error reading to node name {0} on class {1} of link {2}", lineArr[10], lineArr[0], lineArr[1]), null, MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            if (dr == DialogResult.OK) continue;
                            else
                            {
                                return;
                            }
                        }

                        var link = context.ActiveModel.Facility.IntelligentObjects[lineArr[1]];
                        if (link == null)
                        {
                            // Define List of Facility Locatoins
                            List<FacilityLocation> locs = new List<FacilityLocation>();

                            // Add Link
                            link = context.ActiveModel.Facility.IntelligentObjects.CreateLink(lineArr[0], fromNode, toNode, locs);
                            link.Size = fs;
                            if (link == null)
                            {
                                string errorMsg = string.Format("Error creating link from object class {0} of from {1} to {2}", lineArr[0], fromNode.ObjectName, toNode.ObjectName);
                                if (prevErrorMsg != errorMsg)
                                {
                                    DialogResult dr = MessageBox.Show(errorMsg, null, MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                                    prevErrorMsg = errorMsg;
                                    if (dr == DialogResult.OK) continue;
                                    else
                                    {
                                        return;
                                    }
                                }
                                else continue;
                            }
                            else
                            {
                                // update name on link
                                link.ObjectName = lineArr[1];
                            }
                        }

                        if (lineArr.Length > 11)
                        {
                            foreach (IProperty prop in link.Properties)
                            {
                                if (lineArr[11] == prop.Name) // {Dec2017/dth} changed from 12
                                {
                                    prop.Value = lineArr[12];   // {Dec2017/dth} changed from 13

                                    var repeatProp = prop as IRepeatingProperty;

                                    if (repeatProp == null)
                                    {  
                                        SetUnit(prop, lineArr[13]); // {Dec2017/dth} changedfrom 14
                                    }
                                    else
                                    { 
                                        string[] rowsStrArr = lineArr[13].Split('~');   // {Dec2017/dth} changed from 14

                                        Int32 prevRowNumber = -1;
                                        for (int i = 0; i < rowsStrArr.Length; i++)
                                        {
                                            var rowValue = rowsStrArr[i];
                                            string[] rowStrArr = rowValue.Split(';');

                                            // If first value is repeat prop value...
                                            if (i == 0 && rowStrArr.Length == 4)
                                            {
                                                prop.Value = rowStrArr[0];
                                            }
                                            // get values for each property
                                            if (rowStrArr.Length > 3)
                                            {
                                                // get values for each property
                                                Int32 currentRowNumber = -1;
                                                bool result = Int32.TryParse(rowStrArr[rowStrArr.Length - 4], out currentRowNumber);
                                                IRow propRow;
                                                if (result == true && prevRowNumber < currentRowNumber)
                                                {
                                                    prevRowNumber = currentRowNumber;
                                                    if (currentRowNumber > repeatProp.Rows.Count) propRow = repeatProp.Rows.Create();
                                                    else propRow = repeatProp.Rows[currentRowNumber - 1];
                                                }
                                                else
                                                {
                                                    if (repeatProp.Rows.Count == 0) propRow = repeatProp.Rows.Create();
                                                    else propRow = repeatProp.Rows[repeatProp.Rows.Count - 1];
                                                }
                                                foreach (var propRowProp in propRow.Properties)
                                                {
                                                    if (rowStrArr[rowStrArr.Length - 3] == propRowProp.Name)
                                                    {
                                                        propRowProp.Value = rowStrArr[rowStrArr.Length - 2];
                                                        SetUnit(propRowProp, rowStrArr[rowStrArr.Length - 1]);
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }                        
                    }
                }
            }

            EndReader();

            //show log messages
            string msg = string.Join(System.Environment.NewLine, logMessages.ToArray());
            if (msg.Length > 32000) msg = msg.Substring(0, 32000);
            MessageBox.Show(msg, "Imported Links", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void ExportDataTablesAsXML(IDesignContext context, String filePath, ExportImportModelForm f)
        {
            // Create the log message list
            List<String> logMessages = new List<String>();

            var sortedListOfTables = context.ActiveModel.Tables.OrderBy(r => r.Name).ToList();

            var dataSet = new DataSet();
            for (int x = 0; x < f.tableCheckedListBox.CheckedItems.Count; x++)
            {
                // for each table
                foreach (ITable table in sortedListOfTables)
                {
                    if (table.Name == f.tableCheckedListBox.CheckedItems[x].ToString())
                    {
                        var dataTable = ConvertTableToDataTable(table, false);
                        dataTable.TableName = table.Name;
                        dataSet.Tables.Add(dataTable);
                        logMessages.Add(string.Format("Writing {0} ", table.Name));
                    }
                }
            }       

            //  Save Table
            dataSet.WriteXml(filePath);

            //show log messages
            string msg = string.Join(System.Environment.NewLine, logMessages.ToArray());
            if (msg.Length > 32000) msg = msg.Substring(0, 32000);
            MessageBox.Show(msg, "Exported Table Data", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void DeleteTableData(IDesignContext context, ExportImportModelForm f)
        {
            // Create the log message list
            List<String> logMessages = new List<String>();

            var sortedListOfTables = context.ActiveModel.Tables.OrderBy(r => r.Name).ToList();

            var dataSet = new DataSet();
            for (int x = 0; x < f.tableCheckedListBox.CheckedItems.Count; x++)
            {
                // for each table
                foreach (ITable table in sortedListOfTables)
                {
                    if (table.Name == f.tableCheckedListBox.CheckedItems[x].ToString())
                    {
                        table.Rows.Clear();
                        logMessages.Add(string.Format("Deleting Data From {0} ", table.Name));
                    }
                }
            }        

            //show log messages
            string msg = string.Join(System.Environment.NewLine, logMessages.ToArray());
            if (msg.Length > 32000) msg = msg.Substring(0, 32000);
            MessageBox.Show(msg, "Delete Table Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ImportDataTablesWithXML(IDesignContext context, String filePath)
        {
            // Create the log message list
            List<String> logMessages = new List<String>();

            var dataSet = new DataSet();
            dataSet.ReadXml(filePath);

            // each data table
            foreach (DataTable dataTable in dataSet.Tables)
            {
                var table = context.ActiveModel.Tables[dataTable.TableName];

                if (table != null)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        //  iterate thorugh data
                        IRow row = table.Rows.Create();
                        foreach (var prop in row.Properties)
                        {
                            string propertyName = prop.Name;
                            try
                            {
                                var value = dataRow[prop.Name];
                                if (value != null) prop.Value = (string)value;
                            }
                            catch
                            {
                                MessageBox.Show(string.Format("Property {0} Not In XML for Table {1} ", prop.Name, table.Name), "Property Not In XML");
                            }
                        }
                    }
                    logMessages.Add(string.Format("Writing {0} ", table.Name));
                }
                else
                {
                    logMessages.Add(string.Format("Table Not Found in Model {0} ", dataTable.TableName));
                }
            }

            //show log messages
            string msg = string.Join(System.Environment.NewLine, logMessages.ToArray());
            if (msg.Length > 32000) msg = msg.Substring(0, 32000);
            MessageBox.Show(msg, "Imported Table Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ExportTablePropertiesAsXML(IDesignContext context, String filePath)
        {
            // Create the log message list
            List<String> logMessages = new List<String>();

            var xDoc = new XmlDocument();
            var docNode = xDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xDoc.AppendChild(docNode);

            var tablesNode = xDoc.CreateElement("Tables");
            xDoc.AppendChild(tablesNode);

            var sortedListOfTables = context.ActiveModel.Tables.OrderBy(r => r.Name).ToList();

            // for each table
            foreach (ITable table in sortedListOfTables)
            {
                var tableNode = xDoc.CreateElement("Table");
                tablesNode.AppendChild(tableNode);
                
                var tableNameNode = xDoc.CreateElement("Name");
                tableNameNode.AppendChild(xDoc.CreateTextNode(table.Name));
                tableNode.AppendChild(tableNameNode);

                var propertiesNode = xDoc.CreateElement("Properties");
                tableNode.AppendChild(propertiesNode);
                var row = table.Rows.Create();
                foreach (IProperty prop in row.Properties)
                {
                    var propertyNode = xDoc.CreateElement("Property");
                    AddProperty(xDoc, propertyNode, prop);
                    propertiesNode.AppendChild(propertyNode);
                }
                logMessages.Add(string.Format("Writing {0} ", table.Name));
                table.Rows.Remove(row);
            }

            xDoc.Save(filePath);

            //show log messages
            string msg = string.Join(System.Environment.NewLine, logMessages.ToArray());
            if (msg.Length > 32000) msg = msg.Substring(0, 32000);
            MessageBox.Show(msg, "Model Properties Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void ExportModelPropertyValuesAsXML(IDesignContext context, String filePath)
        {
            // Create the log message list
            List<String> logMessages = new List<String>();

            var xDoc = new XmlDocument();
            var docNode = xDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xDoc.AppendChild(docNode);
  
            var propertiesNode = xDoc.CreateElement("Properties");
            xDoc.AppendChild(propertiesNode);

            var sortedListOfProperties = context.ActiveModel.Properties.OrderBy(r => r.Name).ToList();

            foreach (var prop in sortedListOfProperties)
            {
                var propertyNode = xDoc.CreateElement("Property");
                AddProperty(xDoc, propertyNode, prop);
                propertiesNode.AppendChild(propertyNode);
                logMessages.Add(string.Format("Writing {0} ", prop.Name));
            }           

            xDoc.Save(filePath);

            //show log messages
            string msg = string.Join(System.Environment.NewLine, logMessages.ToArray());
            if (msg.Length > 32000) msg = msg.Substring(0, 32000);
            MessageBox.Show(msg, "Model Properties Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void ExportModelStateValuesAsXML(IDesignContext context, String filePath)
        {
            // Create the log message list
            List<String> logMessages = new List<String>();

            var xDoc = new XmlDocument();
            var docNode = xDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xDoc.AppendChild(docNode);

            var statesNode = xDoc.CreateElement("StateDefinitions");
            xDoc.AppendChild(statesNode);

            var sortedListOfStateDefinitions = context.ActiveModel.StateDefinitions.OrderBy(r => r.Name).ToList();

            foreach (var stateDefinition in sortedListOfStateDefinitions)
            {
                var stateDefinitionNode = xDoc.CreateElement("StateDefinition");

                var nameNode = xDoc.CreateElement("Name");
                nameNode.AppendChild(xDoc.CreateTextNode(stateDefinition.Name));
                stateDefinitionNode.AppendChild(nameNode);

                var typeNode = xDoc.CreateElement("Type");
                typeNode.AppendChild(xDoc.CreateTextNode(GetTableStateColumnType(stateDefinition)));
                stateDefinitionNode.AppendChild(typeNode);

                statesNode.AppendChild(stateDefinitionNode);
                logMessages.Add(string.Format("Writing {0} ", stateDefinition.Name));
            }

            xDoc.Save(filePath);

            //show log messages
            string msg = string.Join(System.Environment.NewLine, logMessages.ToArray());
            if (msg.Length > 32000) msg = msg.Substring(0, 32000);
            MessageBox.Show(msg, "Model StateDefinitons Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private static string GetTableStateColumnType(IStateDefinition stateDefinition)
        {
            var iitsc = stateDefinition as IIntegerStateDefinition;
            if (iitsc != null)
            {
                return "Integer";
            }
            var rtsc = stateDefinition as IRealStateDefinition;
            if (rtsc != null)
            {
                return "Real";
            }
            var dtsc = stateDefinition as IDateTimeStateDefinition;
            if (dtsc != null)
            {
                return "DateTime";
            }
            var btsc = stateDefinition as IBooleanStateDefinition;
            if (btsc != null)
            {
                return "Boolean";
            }
            return "String";
        }

        private void ImportTablePropertyValuesWithXML(IDesignContext context, String filePath)
        {
            // Create the log message list
            List<String> logMessages = new List<String>();

            var xelement = XElement.Load(filePath);
            IEnumerable<XElement> sObjects = xelement.Elements();

            MessageBox.Show("Table properties cannot be updated via the API.  This option will only show the unit values that are different between XML and data tables in model.", "Table properties cannot be updated via the API");
            
            foreach (var sTable in sObjects)
            {
                var table = context.ActiveModel.Tables[sTable.Element("Name").Value];
                if (table != null)
                {
                    var row = table.Rows.Create();

                    IEnumerable<XElement> oProperties = sTable.Elements("Properties");

                    // Import Properties
                    foreach (var oProp in oProperties.Elements("Property"))
                    {
                        foreach (IProperty prop in row.Properties)
                        {
                            if (oProp.Element("Name").Value == prop.Name)
                            {
                                var propUnits = GetUnitString(prop);
                                var unitsEle = oProp.Element("Units");
                                if (unitsEle != null)
                                {
                                    if (propUnits != unitsEle.Value)
                                    {
                                        logMessages.Add(string.Format("Unit Do Not Match In XML File for table {0}, column {1}, xml unit {2} and table unit {3} ", table.Name, prop.Name, unitsEle.Value, propUnits));
                                    }
                                    //SetUnit(prop, unitsEle.Value);
                                }
                                else if (propUnits.Length > 0 && propUnits != "none")
                                {
                                    logMessages.Add(string.Format("Unit Not Found In XML File for table {0}, column {1} and unit {2} ", table.Name, prop.Name, propUnits));
                                }
                                break;
                            }
                        }
                    }
                    table.Rows.Remove(row);
                    //logMessages.Add(string.Format("Writing {0} ", table.Name));
                }
            }

            //show log messages
            string msg = string.Join(System.Environment.NewLine, logMessages.ToArray());
            if (msg.Length > 32000) msg = msg.Substring(0, 32000);
            MessageBox.Show(msg, "Mix Match Table Units", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ImportModelPropertyValuesWithXML(IDesignContext context, String filePath)
        {
            // Create the log message list
            //List<String> logMessages = new List<String>();

            var xelement = XElement.Load(filePath);

            // Make sure XML props are in model
            foreach (var oProp in xelement.Elements("Property"))
            {
                Boolean foundFlag = false;
                foreach (IProperty prop in context.ActiveModel.Properties)
                {
                    if (oProp.Element("Name").Value == prop.Name)
                    {
                        foundFlag = true;
                        break;
                    }
                }

                if (foundFlag == false)
                {
                    context.ActiveModel.PropertyDefinitions.AddRealProperty(oProp.Element("Name").Value, 0.0);
                    //logMessages.Add(string.Format("Real Properties Added To Model {0} ", oProp.Element("Name").Value));
                }
            }

            //show log messages
            string msg = "done";// string.Join(System.Environment.NewLine, logMessages.ToArray());
            if (msg.Length > 32000) msg = msg.Substring(0, 32000);
            MessageBox.Show(msg, "Properties in XML that are not in model", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //logMessages = new List<String>();

            // Import Properties
            foreach (var oProp in xelement.Elements("Property"))
            {
                foreach (IProperty prop in context.ActiveModel.Properties)
                {
                    if (oProp.Element("Name").Value == prop.Name)
                    {
                        var unitsEle = oProp.Element("Units");
                        if (unitsEle != null)
                        {
                            SetUnit(prop, unitsEle.Value);
                        }

                        prop.Value = oProp.Element("Value").Value;
                        //var repeatProp = prop as IRepeatingProperty;

                        //if (repeatProp != null && oProp.Element("Rows").HasElements)
                        //{
                        //    Int32 currentRowNumber = 0;
                        //    IEnumerable<XElement> rProperties = oProp.Element("Rows").Elements("Properties");
                        //    foreach (var rProps in oProp.Element("Rows").Elements("Properties"))
                        //    {
                        //        currentRowNumber++;
                        //        IRow propRow;
                        //        if (currentRowNumber > repeatProp.Rows.Count) propRow = repeatProp.Rows.Create();
                        //        else propRow = repeatProp.Rows[currentRowNumber - 1];

                        //        foreach (var rProp in rProps.Elements("Property"))
                        //        {
                        //            foreach (var propRowProp in propRow.Properties)
                        //            {
                        //                if (rProp.Element("Name").Value == propRowProp.Name)
                        //                {
                        //                    propRowProp.Value = rProp.Element("Value").Value;
                        //                    unitsEle = rProp.Element("Units");
                        //                    if (unitsEle != null)
                        //                    {
                        //                        SetUnit(propRowProp, unitsEle.Value);
                        //                    }
                        //                    break;
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                        //logMessages.Add(string.Format("Writing {0} ", prop.Name));
                    }                    
                }
            }

            //show log messages
            //msg = string.Join(System.Environment.NewLine, logMessages.ToArray());
            if (msg.Length > 32000) msg = msg.Substring(0, 32000);
            MessageBox.Show(msg, "Imported Model Properties", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //logMessages = new List<String>();            

            // Make sure model props are in XML
            //foreach (IProperty prop in context.ActiveModel.Properties)
            //{
            //    Boolean foundFlag = false;
            //    foreach (var oProp in xelement.Elements("Property"))
            //    {
            //        if (oProp.Element("Name").Value == prop.Name)
            //        {
            //            foundFlag = true;
            //            break;
            //        }
            //    }

            //    if (foundFlag == false)
            //    {
            //        //logMessages.Add(string.Format("Property In Model Not Found In XML {0} ", prop.Name));
            //    }
            //}

            //show log messages
            //msg = string.Join(System.Environment.NewLine, logMessages.ToArray());
            if (msg.Length > 32000) msg = msg.Substring(0, 32000);
            MessageBox.Show(msg, "Properties in model that are not in XML", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void ImportModelPropertiesWithCSV(IDesignContext context, String filePath)
        {
            // Create the log message list
            List<String> logMessages = new List<String>();

            StartReader(filePath);

            string line = string.Empty;

            // Import properties
            while (!String.IsNullOrEmpty((line = _reader.ReadLine())))
            {
                string[] lineArr = line.Split(listSeparator[0]);

                if (lineArr.Length < 3)
                {
                    DialogResult dr = MessageBox.Show(string.Format("3 parameters needed to import model properties (name, type, default value)..Current Input Line Is {0} ", line), null, MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    if (dr == DialogResult.OK) continue;
                    else
                    {
                        return;
                    }
                }
                else
                {
                    string name = lineArr[0].Trim();
                    string type = lineArr[1].ToLower().Trim();
                    string defaultValue = lineArr[2];

                    var pd = context.ActiveModel.PropertyDefinitions[name];

                    if (pd != null)
                    {
                        pd.DefaultString = defaultValue;
                        logMessages.Add(string.Format("Updating Default Value On {0} ", name));
                    }
                    else
                    {
                        switch (type)
                        {
                            case "real":
                                context.ActiveModel.PropertyDefinitions.AddRealProperty(name, Convert.ToDouble(defaultValue));
                                logMessages.Add(string.Format("Adding Real Property For {0} ", name));
                                break;
                            case "datetime":
                                context.ActiveModel.PropertyDefinitions.AddDateTimeProperty(name, Convert.ToDateTime(defaultValue));
                                logMessages.Add(string.Format("Adding DateTime Property For {0} ", name));
                                break;
                            case "boolean":
                                pd = context.ActiveModel.PropertyDefinitions.AddBooleanProperty(name);                                
                                pd.DefaultString = defaultValue;
                                logMessages.Add(string.Format("Adding Boolean Property For {0} ", name));
                                break;
                            default:
                                context.ActiveModel.PropertyDefinitions.AddStringProperty(name, defaultValue);
                                logMessages.Add(string.Format("Adding String Property For {0} ", name));
                                break;
                        }
                    }
                }
            }

            EndReader();

            //show log messages
            string msg = string.Join(System.Environment.NewLine, logMessages.ToArray());
            if (msg.Length > 32000) msg = msg.Substring(0, 32000);
            MessageBox.Show(msg, "Imported Model Property Definitions from CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }


        private void ExportObjectTypesAsXML(IDesignContext context, String filePath)
        {
            // Create the log message list
            List<String> logMessages = new List<String>();
            // Create the log message list
            List<String> loadedObjectTypes = new List<String>();

            foreach (IPropertyDefinition propDef in context.ActiveModel.PropertyDefinitions)
            {
                string defaultString = propDef.DefaultString;
            }

            var xDoc = new XmlDocument();
            var docNode = xDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xDoc.AppendChild(docNode);
            var objectTypesNode = xDoc.CreateElement("ObjectTypes");
            xDoc.AppendChild(objectTypesNode);

            var sortedListOfObjects = context.ActiveModel.Facility.IntelligentObjects.OrderBy(r => r.ObjectName).ToList();

            foreach (IIntelligentObject intellObj in sortedListOfObjects)
            {
                var link = intellObj as ILinkObject;
                if (link == null)
                {
                    string[] nameArray = intellObj.ObjectName.Split('@');
                    if (nameArray.Length == 1)
                    {
                        bool objectTypeFound = false;

                        foreach(var objTypeStr in loadedObjectTypes)
                        {
                            if (objTypeStr == intellObj.TypeName)
                            {
                                objectTypeFound = true;
                                break;
                            }
                        }

                        if (objectTypeFound == false)
                        {
                            loadedObjectTypes.Add(intellObj.TypeName);

                            var objectTypeNode = xDoc.CreateElement("ObjectType");
                            objectTypesNode.AppendChild(objectTypeNode);

                            var typeNameNode = xDoc.CreateElement("TypeName");
                            typeNameNode.AppendChild(xDoc.CreateTextNode(intellObj.TypeName));
                            objectTypeNode.AppendChild(typeNameNode);

                            var associatedNodesNode = xDoc.CreateElement("AssociatedNodes");
                            objectTypeNode.AppendChild(associatedNodesNode);

                            foreach (var nodeIntellObj in context.ActiveModel.Facility.IntelligentObjects)
                            {
                                string[] nodeNameArray = nodeIntellObj.ObjectName.Split('@');
                                if (nodeNameArray.Length == 2)
                                {
                                    if (nodeNameArray[1] == intellObj.ObjectName)
                                    {
                                        var nodeNode = xDoc.CreateElement("Node");
                                        associatedNodesNode.AppendChild(nodeNode);

                                        var nodeNameNode = xDoc.CreateElement("NodeName");
                                        nodeNameNode.AppendChild(xDoc.CreateTextNode(nodeNameArray[0] + "@"));
                                        nodeNode.AppendChild(nodeNameNode);

                                        var nodePropertiesNode = xDoc.CreateElement("Properties");
                                        nodeNode.AppendChild(nodePropertiesNode);

                                        AddProperties(xDoc, nodePropertiesNode, nodeIntellObj);
                                    }
                                }
                            }

                            var propertiesNode = xDoc.CreateElement("Properties");
                            objectTypeNode.AppendChild(propertiesNode);

                            AddProperties(xDoc, propertiesNode, intellObj);

                            logMessages.Add(string.Format("Writing {0} ", intellObj.TypeName));
                        }
                    }
                }
            }

            xDoc.Save(filePath);

            //show log messages
            string msg = string.Join(System.Environment.NewLine, logMessages.ToArray());
            if (msg.Length > 32000) msg = msg.Substring(0, 32000);
            MessageBox.Show(msg, "Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void AddModelProperties(XmlDocument xDoc, XmlElement propertiesNode, IPropertyDefinition propDef, IProperty prop)
        {
            IRepeatingProperty repeatProp = prop as IRepeatingProperty;

            var propertyNode = xDoc.CreateElement("Property");
            propertiesNode.AppendChild(propertyNode);

            var nameNode = xDoc.CreateElement("Name");
            nameNode.AppendChild(xDoc.CreateTextNode(prop.Name));
            propertyNode.AppendChild(nameNode);

            var defaultValueNode = xDoc.CreateElement("DefaultString");
            defaultValueNode.AppendChild(xDoc.CreateTextNode(propDef.DefaultString));
            propertyNode.AppendChild(defaultValueNode);

            var categoryNameNode = xDoc.CreateElement("CategoryName");
            categoryNameNode.AppendChild(xDoc.CreateTextNode(propDef.CategoryName));
            propertyNode.AppendChild(categoryNameNode);

            string unit = GetUnitString(prop);
            if (unit.Length > 0 && unit != "none")
            {
                var unitsNode = xDoc.CreateElement("Units");
                unitsNode.AppendChild(xDoc.CreateTextNode(unit));
                propertyNode.AppendChild(unitsNode);
            }

            var valueNode = xDoc.CreateElement("Value");
            valueNode.AppendChild(xDoc.CreateTextNode(prop.Value));
            propertyNode.AppendChild(valueNode);

        }

        private void AddProperties(XmlDocument xDoc, XmlElement propertiesNode, IIntelligentObject intellObj)
        {
            var sortedListOfProperties = intellObj.Properties.OrderBy(r => r.Name).ToList();

            foreach (var prop in sortedListOfProperties)
            {
                var propertyNode = xDoc.CreateElement("Property");
                AddProperty(xDoc, propertyNode, prop);
                propertiesNode.AppendChild(propertyNode);
            }
        }

        private void AddProperty(XmlDocument xDoc, XmlElement propertyNode, IProperty prop)
        {
            IPropertyDefinition propDef = prop as IPropertyDefinition;
            IRepeatingProperty repeatProp = prop as IRepeatingProperty;

            var nameNode = xDoc.CreateElement("Name");
            nameNode.AppendChild(xDoc.CreateTextNode(prop.Name));
            propertyNode.AppendChild(nameNode);

            string unit = GetUnitString(prop);
            if (unit.Length > 0 && unit != "none")
            {
                var unitsNode = xDoc.CreateElement("Units");
                unitsNode.AppendChild(xDoc.CreateTextNode(unit));
                propertyNode.AppendChild(unitsNode);
            }

            var valueNode = xDoc.CreateElement("Value");
            valueNode.AppendChild(xDoc.CreateTextNode(prop.Value));
            propertyNode.AppendChild(valueNode);

            if (repeatProp != null)
            {
                var rowsNode = xDoc.CreateElement("Rows");
                propertyNode.AppendChild(rowsNode);

                foreach (IRow row in repeatProp.Rows)
                {
                    var rPropertiesNode = xDoc.CreateElement("Properties");
                    rowsNode.AppendChild(rPropertiesNode);

                    foreach (IProperty rowProp in row.Properties)
                    {
                        var rPropertyNode = xDoc.CreateElement("Property");
                        rPropertiesNode.AppendChild(rPropertyNode);

                        var rNameNode = xDoc.CreateElement("Name");
                        rNameNode.AppendChild(xDoc.CreateTextNode(rowProp.Name));
                        rPropertyNode.AppendChild(rNameNode);

                        unit = GetUnitString(rowProp);
                        if (unit.Length > 0 && unit != "none")
                        {
                            var rUnitsNode = xDoc.CreateElement("Units");
                            rUnitsNode.AppendChild(xDoc.CreateTextNode(unit));
                            rPropertyNode.AppendChild(rUnitsNode);
                        }

                        var rValueNode = xDoc.CreateElement("Value");
                        rValueNode.AppendChild(xDoc.CreateTextNode(rowProp.Value));
                        rPropertyNode.AppendChild(rValueNode);
                    }
                }
            }
        }

        private void ExportObjectsAsXML(IDesignContext context, String filePath)
        {     
            // Create the log message list
            List<String> logMessages = new List<String>();

            var xDoc = new XmlDocument();
            var docNode = xDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xDoc.AppendChild(docNode);
            var objectsNode = xDoc.CreateElement("Objects");
            xDoc.AppendChild(objectsNode);

            var sortedListOfObjects = context.ActiveModel.Facility.IntelligentObjects.OrderBy(r => r.ObjectName).ToList();

            foreach (IIntelligentObject intellObj in sortedListOfObjects)
            {
                var link = intellObj as ILinkObject;
                if (link == null)
                {                    
                    var objectNode = xDoc.CreateElement("Object");
                    objectsNode.AppendChild(objectNode);

                    var objectTypeNode = xDoc.CreateElement("ObjectType");
                    objectTypeNode.AppendChild(xDoc.CreateTextNode(intellObj.TypeName));
                    objectNode.AppendChild(objectTypeNode);

                    var objectNameNode = xDoc.CreateElement("ObjectName");
                    objectNameNode.AppendChild(xDoc.CreateTextNode(intellObj.ObjectName));
                    objectNode.AppendChild(objectNameNode);

                    var xLocationNode = xDoc.CreateElement("XLocation");
                    xLocationNode.AppendChild(xDoc.CreateTextNode(intellObj.Location.X.ToString()));
                    objectNode.AppendChild(xLocationNode);

                    var yLocationNode = xDoc.CreateElement("YLocation");
                    yLocationNode.AppendChild(xDoc.CreateTextNode(intellObj.Location.Y.ToString()));
                    objectNode.AppendChild(yLocationNode);

                    var zLocationNode = xDoc.CreateElement("ZLocation");
                    zLocationNode.AppendChild(xDoc.CreateTextNode(intellObj.Location.Z.ToString()));
                    objectNode.AppendChild(zLocationNode);

                    var lengthNode = xDoc.CreateElement("Length");
                    lengthNode.AppendChild(xDoc.CreateTextNode(intellObj.Size.Length.ToString()));
                    objectNode.AppendChild(lengthNode);

                    var widthNode = xDoc.CreateElement("Width");
                    widthNode.AppendChild(xDoc.CreateTextNode(intellObj.Size.Width.ToString()));
                    objectNode.AppendChild(widthNode);

                    var heightNode = xDoc.CreateElement("Height");
                    heightNode.AppendChild(xDoc.CreateTextNode(intellObj.Size.Height.ToString()));
                    objectNode.AppendChild(heightNode);

                    var propertiesNode = xDoc.CreateElement("Properties");
                    objectNode.AppendChild(propertiesNode);

                    AddProperties(xDoc, propertiesNode, intellObj);

                    logMessages.Add(string.Format("Writing {0} ", intellObj.ObjectName));
                }
            }

            xDoc.Save(filePath);

            //show log messages
            string msg = string.Join(System.Environment.NewLine, logMessages.ToArray());
            if (msg.Length > 32000) msg = msg.Substring(0, 32000);
            MessageBox.Show(msg, "Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void ImportObjectsWithXML(IDesignContext context, String filePath)
        {
            // Create the log message list
            List<String> logMessages = new List<String>();

            var xelement = XElement.Load(filePath);    
            IEnumerable<XElement> sObjects = xelement.Elements();        

            // Import Objects
            foreach (var sObject in sObjects)
            {
                // Add the coordinates to the intelligent object
                FacilityLocation loc = new FacilityLocation(Convert.ToDouble(sObject.Element("XLocation").Value), Convert.ToDouble(sObject.Element("YLocation").Value), Convert.ToDouble(sObject.Element("ZLocation").Value));
                var intellObj = context.ActiveModel.Facility.IntelligentObjects[sObject.Element("ObjectName").Value];
                if (intellObj == null)
                {
                    intellObj = context.ActiveModel.Facility.IntelligentObjects.CreateObject(sObject.Element("ObjectType").Value, loc);
                    if (intellObj == null)
                    {
                        DialogResult dr = MessageBox.Show(string.Format("Error creating object of class {0} of name {1}...Object class might not exist in model", sObject.Element("ObjectType").Value, sObject.Element("ObjectName").Value), null, MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        if (dr == DialogResult.OK) continue;
                        else
                        {
                            return;
                        }
                    }
                    intellObj.ObjectName = sObject.Element("ObjectName").Value;
                }

                // size then locatoin again 
                FacilitySize fs = new FacilitySize(Convert.ToDouble(sObject.Element("Length").Value), Convert.ToDouble(sObject.Element("Width").Value), Convert.ToDouble(sObject.Element("Height").Value));
                intellObj.Size = fs;
                intellObj.Location = loc;

                IEnumerable<XElement> oProperties = sObject.Elements("Properties");

                foreach (var oProp in oProperties.Elements("Property"))
                {
                    foreach (IProperty prop in intellObj.Properties)
                    {
                        if (oProp.Element("Name").Value == prop.Name)
                        {
                            var unitsEle = oProp.Element("Units");
                            if (unitsEle != null)
                            {
                                SetUnit(prop, unitsEle.Value);
                            }

                            prop.Value = oProp.Element("Value").Value;
                            var repeatProp = prop as IRepeatingProperty;

                            if (repeatProp != null && oProp.Element("Rows").HasElements)
                            {
                                Int32 currentRowNumber = 0;
                                IEnumerable<XElement> rProperties = oProp.Element("Rows").Elements("Properties");
                                foreach (var rProps in oProp.Element("Rows").Elements("Properties"))
                                {
                                    currentRowNumber++;
                                    IRow propRow;
                                    if (currentRowNumber > repeatProp.Rows.Count) propRow = repeatProp.Rows.Create();
                                    else propRow = repeatProp.Rows[currentRowNumber - 1];

                                    foreach (var rProp in rProps.Elements("Property"))
                                    {
                                        foreach (var propRowProp in propRow.Properties)
                                        {
                                            if (rProp.Element("Name").Value == propRowProp.Name)
                                            {
                                                propRowProp.Value = rProp.Element("Value").Value;
                                                unitsEle = rProp.Element("Units");
                                                if (unitsEle != null)
                                                {
                                                    SetUnit(propRowProp, unitsEle.Value);
                                                }
                                                break;
                                            }
                                        }
                                    }
                                }
                            }     
                        }
                    }
                }
                logMessages.Add(string.Format("Writing {0} ", intellObj.ObjectName));
            }

            //show log messages
            string msg = string.Join(System.Environment.NewLine, logMessages.ToArray());
            if (msg.Length > 32000) msg = msg.Substring(0, 32000);
            MessageBox.Show(msg, "Imported Objects", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ExportObjectsAndLinks(IDesignContext context, String filePath, bool includeProperties)
        {
            // Create the log message list
            List<String> logMessages = new List<String>();

            StartWriter(filePath);

            var sortedListOfObjects = context.ActiveModel.Facility.IntelligentObjects.OrderBy(r => r.ObjectName).ToList();

            foreach (IIntelligentObject intellObj in sortedListOfObjects)
            { 
                string writeout = intellObj.ObjectName;

                string objectdata = intellObj.TypeName + listSeparator[0].ToString() + intellObj.ObjectName + listSeparator[0].ToString() + intellObj.Location.X.ToString() + listSeparator[0].ToString() + intellObj.Location.Y.ToString() + listSeparator[0].ToString() + intellObj.Location.Z.ToString();
                logMessages.Add(string.Format("Writing {0} ", intellObj.ObjectName));

                ILinkObject link = intellObj as ILinkObject;

                if (link != null)
                {
                    // add size
                    objectdata = objectdata + listSeparator[0].ToString() + link.Size.Length.ToString() + listSeparator[0].ToString() + link.Size.Width.ToString() + listSeparator[0].ToString() + intellObj.Size.Height.ToString();
                    // add type
                    objectdata = objectdata + listSeparator[0].ToString() + "1" + listSeparator[0].ToString()[0].ToString() + link.Begin.ObjectName + listSeparator[0].ToString() + link.End.ObjectName;                   
                }
                else
                {
                    // add size
                    objectdata = objectdata + listSeparator[0].ToString() + intellObj.Size.Length.ToString() +listSeparator[0].ToString() + intellObj.Size.Width.ToString() + listSeparator[0].ToString() + intellObj.Size.Height.ToString();
                    // add type
                    objectdata = objectdata + listSeparator[0].ToString() + "0";
                }

                if (includeProperties)
                {
                    var sortedListOfProperties = intellObj.Properties.OrderBy(r => r.Name).ToList();

                    foreach (var prop in sortedListOfProperties)
                    {
                        IPropertyDefinition propDef = prop as IPropertyDefinition;
                        IRepeatingProperty repeatProp = prop as IRepeatingProperty;

                        string propertyString = objectdata + listSeparator[0].ToString() + prop.Name + listSeparator[0].ToString() + prop.Value + listSeparator[0].ToString(); 

                        if (repeatProp != null)
                        {
                            int rowNumber = 0;
                            foreach (IRow row in repeatProp.Rows)
                            {
                                rowNumber++;
                                string repeatPropertyData = "";
                                foreach (IProperty rowProp in row.Properties)
                                {
                                    if (repeatPropertyData.Length == 0) repeatPropertyData = ";" + rowNumber.ToString() + ";" + rowProp.Name + ";" + rowProp.Value + ";" + GetUnitString(rowProp); 
                                    else repeatPropertyData = repeatPropertyData + "~" + rowNumber.ToString() + ";" + rowProp.Name + ";" + rowProp.Value + ";" + GetUnitString(rowProp); ;
                                }
                                writeout = propertyString + repeatPropertyData;
                                _writer.WriteLine(writeout);
                            }                            
                        }
                        else
                        {
                            writeout = propertyString + GetUnitString(prop); 
                            _writer.WriteLine(writeout);
                        }
                    }
                }
                else
                {
                    _writer.WriteLine(objectdata);
                }
            }

            if (_writer == null)
            {
                _writer.Flush();
            }
            
            EndWriter();

            //show log messages
            string msg = string.Join(System.Environment.NewLine, logMessages.ToArray());
            if (msg.Length > 32000) msg = msg.Substring(0, 32000);
            MessageBox.Show(msg, "Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void ImportObjects(IDesignContext context, String filePath)
        {
            // Create the log message list
            List<String> logMessages = new List<String>();

            StartReader(filePath);

            string line = string.Empty;

            string prvIntellObjStr = "";
            string prevErrorMsg = "";

            // Import Objects
            while (!String.IsNullOrEmpty((line = _reader.ReadLine())))
            {   
                string[] lineArr = line.Split(listSeparator[0]);

                if (lineArr.Length > 1)
                {
                    // not a link
                    if (lineArr[8] == "0")
                    {
                        if (prvIntellObjStr != lineArr[1])
                        {
                            logMessages.Add(string.Format("Reading {0} ", lineArr[1]));
                            prvIntellObjStr = lineArr[1];
                        }
                        // Add the coordinates to the intelligent object
                        FacilityLocation loc = new FacilityLocation(Convert.ToDouble(lineArr[2]), Convert.ToDouble(lineArr[3]), Convert.ToDouble(lineArr[4]));
                        var intellObj = context.ActiveModel.Facility.IntelligentObjects[lineArr[1]];
                        if (intellObj == null)
                        {
                            intellObj = context.ActiveModel.Facility.IntelligentObjects.CreateObject(lineArr[0], loc);
                            if (intellObj == null)
                            {
                                string errorMsg = string.Format("Error creating object of class {0} of name {1}...Object class might not exist in model", lineArr[0], lineArr[1]);
                                if (prevErrorMsg != errorMsg)
                                {
                                    DialogResult dr = MessageBox.Show(errorMsg, null, MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                                    prevErrorMsg = errorMsg;
                                    if (dr == DialogResult.OK) continue;
                                    else
                                    {
                                        return;
                                    }
                                }
                                else continue;
                            }
                            intellObj.ObjectName = lineArr[1];
                        }

                       // size then locatoin again 
                        FacilitySize fs = new FacilitySize(Convert.ToDouble(lineArr[5]), Convert.ToDouble(lineArr[6]), Convert.ToDouble(lineArr[7]));
                        intellObj.Size = fs;
                        intellObj.Location = loc;

                        if (lineArr.Length > 9)
                        {
                            foreach (IProperty prop in intellObj.Properties)
                            {
                                if (lineArr[9] == prop.Name)
                                {
                                    prop.Value = lineArr[10];                                    

                                    var repeatProp = prop as IRepeatingProperty;

                                    if (repeatProp == null)
                                    {
                                        SetUnit(prop, lineArr[11]);
                                    }
                                    else
                                    { 
                                        string[] rowsStrArr = lineArr[11].Split('~');

                                        Int32 prevRowNumber = -1;
                                        for (int i = 0; i < rowsStrArr.Length; i++)
                                        {
                                            var rowValue = rowsStrArr[i];
                                            string[] rowStrArr = rowValue.Split(';');

                                            // If first value is repeat prop value...
                                            if (i == 0 && rowStrArr.Length == 4)
                                            {
                                                prop.Value = rowStrArr[0];
                                            }
                                            // get values for each property
                                            if (rowStrArr.Length > 3)
                                            {
                                                // get values for each property
                                                Int32 currentRowNumber = -1;
                                                bool result = Int32.TryParse(rowStrArr[rowStrArr.Length - 4], out currentRowNumber);
                                                IRow propRow;
                                                if (result == true && prevRowNumber < currentRowNumber)
                                                {
                                                    prevRowNumber = currentRowNumber;
                                                    if (currentRowNumber > repeatProp.Rows.Count) propRow = repeatProp.Rows.Create();
                                                    else propRow = repeatProp.Rows[currentRowNumber - 1];
                                                }
                                                else
                                                {
                                                    if (repeatProp.Rows.Count == 0) propRow = repeatProp.Rows.Create();
                                                    else propRow = repeatProp.Rows[repeatProp.Rows.Count - 1];
                                                }
                                                foreach (var propRowProp in propRow.Properties)
                                                {
                                                    if (rowStrArr[rowStrArr.Length - 3] == propRowProp.Name)
                                                    {
                                                        propRowProp.Value = rowStrArr[rowStrArr.Length - 2];
                                                        SetUnit(propRowProp, rowStrArr[rowStrArr.Length - 1]);
                                                        break;                                               
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }                        
                    }
                }
            }

            EndReader();            

            //show log messages
            string msg = string.Join(System.Environment.NewLine, logMessages.ToArray());
            if (msg.Length > 32000) msg = msg.Substring(0, 32000);
            MessageBox.Show(msg, "Imported Objects", MessageBoxButtons.OK, MessageBoxIcon.Information);           

        }

        private void ImportVertices(IDesignContext context, String filePath)
        {
            // Create the log message list
            List<String> logMessages = new List<String>();

            StartReader(filePath);

            string line = string.Empty;

            string prvIntellObjStr = "";

            // Import Objects
            while (!String.IsNullOrEmpty((line = _reader.ReadLine())))
            {
                string[] lineArr = line.Split(listSeparator[0]);

                if (lineArr.Length < 4)
                {
                    DialogResult dr = MessageBox.Show(string.Format("4 parameters needed to import verticies (link, x, y and z) ", lineArr[0]), null, MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    if (dr == DialogResult.OK) continue;
                    else
                    {
                        return;
                    }
                }
                else
                {
                    var intellObj = context.ActiveModel.Facility.IntelligentObjects[lineArr[0]];
                    if (intellObj == null)
                    {
                        DialogResult dr = MessageBox.Show(string.Format("Object of name {0} does not exist in model", lineArr[0]), null, MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        if (dr == DialogResult.OK) continue;
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        ILinkObject link = intellObj as ILinkObject;
                        if (link == null)
                        {
                            DialogResult dr = MessageBox.Show(string.Format("Link of name {0} does not exist in model", lineArr[0]), null, MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            if (dr == DialogResult.OK) continue;
                            else
                            {
                                return;
                            }
                        }
                        else
                        {
                            if (prvIntellObjStr != lineArr[0])
                            {
                                logMessages.Add(string.Format("Reading {0} ", lineArr[0]));
                                prvIntellObjStr = lineArr[0];
                                try
                                {
                                    link.InteriorVertices.Clear();
                                }
                                catch
                                {
                                    DialogResult dr = MessageBox.Show(string.Format("Link of name {0} currently selected in facility.  Unselect link and try again.", lineArr[0]), null, MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                                    if (dr == DialogResult.OK) continue;
                                    else
                                    {
                                        return;
                                    }
                                }
                            }

                            FacilityLocation fl = new FacilityLocation(Convert.ToDouble(lineArr[1]), Convert.ToDouble(lineArr[2]), Convert.ToDouble(lineArr[3]));
                            link.InteriorVertices.InsertAt(link.InteriorVertices.Count, fl);
                        }
                    }
                }
            }

            EndReader();

            //show log messages
            string msg = string.Join(System.Environment.NewLine, logMessages.ToArray());
            if (msg.Length > 32000) msg = msg.Substring(0, 32000);
            MessageBox.Show(msg, "Imported Vertices", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void ExportElements(IDesignContext context, String filePath)
        {
            // Create the log message list
            List<String> logMessages = new List<String>();

            StartWriter(filePath);

            var sortedListOfElements = context.ActiveModel.Elements.OrderBy(r => r.ObjectName).ToList();

            foreach (var eleObj in sortedListOfElements)
            {               
                logMessages.Add(string.Format("Writing {0} ", eleObj.ObjectName));
                string elementData = eleObj.TypeName + listSeparator[0].ToString() + eleObj.ObjectName;
                foreach (var prop in eleObj.Properties)
                {
                    string writeout = "";
                    IRepeatingProperty repeatProp = prop as IRepeatingProperty;

                    string propertyString = elementData + listSeparator[0].ToString() + prop.Name + listSeparator[0].ToString() + prop.Value;

                    if (repeatProp != null)
                    {
                        int rowNumber = 0;
                        foreach (IRow row in repeatProp.Rows)
                        {
                            rowNumber++;
                            string repeatPropertyData = "";
                            foreach (IProperty rowProp in row.Properties)
                            {
                                if (repeatPropertyData.Length == 0) repeatPropertyData = ";" + rowNumber.ToString() + ";" + rowProp.Name + ";" + rowProp.Value;
                                else repeatPropertyData = repeatPropertyData + "~" + rowNumber.ToString() + ";" + rowProp.Name + ";" + rowProp.Value;
                            }
                            writeout = propertyString + repeatPropertyData;
                            _writer.WriteLine(writeout);
                        }
                    }
                    else
                    {
                        writeout = propertyString;
                        //if (prop.Unit != null)
                        //{
                        //    writeout = propertyString + ";" + GetUnitString(prop);
                        //}
                        _writer.WriteLine(writeout);
                    }                  
                }
            }

            if (_writer == null)
            {
                _writer.Flush();
            }

            EndWriter();

            //show log messages
            string msg = string.Join(System.Environment.NewLine, logMessages.ToArray());
            if (msg.Length > 32000) msg = msg.Substring(0, 32000);
            MessageBox.Show(msg, "ExportedElements", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void ImportElements(IDesignContext context, String filePath)
        {
            // Create the log message list
            List<String> logMessages = new List<String>();

            StartReader(filePath);

            string line = string.Empty;

            string prvElementjStr = "";

            // Import Element 
            while (!String.IsNullOrEmpty((line = _reader.ReadLine())))
            {
                string[] lineArr = line.Split(listSeparator[0]);

                if (lineArr.Length > 2)
                {
                    if (prvElementjStr != lineArr[1])
                    {
                        logMessages.Add(string.Format("Reading {0} ", lineArr[1]));
                        prvElementjStr = lineArr[1];
                    }

                    // Add Element                    
                    var element = context.ActiveModel.Elements[lineArr[1]];
                    if (element == null)
                    {
                        element = context.ActiveModel.Elements.CreateElement(lineArr[0]);
                        element.ObjectName = lineArr[1];
                    }


                    if (lineArr.Length > 3)
                    {
                        if (element != null)
                        {
                            foreach (IProperty prop in element.Properties)
                            {
                                if (lineArr[2] == prop.Name)
                                {
                                    var repeatProp = prop as IRepeatingProperty;

                                    if (repeatProp != null)
                                    {
                                        string[] rowsStrArr = lineArr[3].Split('~');

                                        Int32 prevRowNumber = -1;
                                        for (int i = 0; i < rowsStrArr.Length; i++)
                                        {
                                            var rowValue = rowsStrArr[i];
                                            string[] rowStrArr = rowValue.Split(';');

                                            // If first value is repeat prop value...
                                            if (i == 0 && rowStrArr.Length == 4)
                                            {
                                                prop.Value = rowStrArr[0];
                                            }
                                            // get values for each property
                                            if (rowStrArr.Length > 2)
                                            {
                                                // get values for each property
                                                Int32 currentRowNumber = -1;
                                                bool result = Int32.TryParse(rowStrArr[rowStrArr.Length - 3], out currentRowNumber);
                                                IRow propRow;
                                                if (result == true && prevRowNumber < currentRowNumber)
                                                {
                                                    prevRowNumber = currentRowNumber;
                                                    if (currentRowNumber > repeatProp.Rows.Count) propRow = repeatProp.Rows.Create();
                                                    else propRow = repeatProp.Rows[currentRowNumber - 1];
                                                }
                                                else
                                                {
                                                    if (repeatProp.Rows.Count == 0) propRow = repeatProp.Rows.Create();
                                                    else propRow = repeatProp.Rows[repeatProp.Rows.Count - 1];
                                                }
                                                foreach (var propRowProp in propRow.Properties)
                                                {
                                                    if (rowStrArr[rowStrArr.Length - 2] == propRowProp.Name)
                                                    {
                                                        propRowProp.Value = rowStrArr[rowStrArr.Length - 1];
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        prop.Value = lineArr[3];
                                    }
                                }
                            }
                        }
                    }                    
                }
            }

            EndReader();

            //show log messages
            string msg = string.Join(System.Environment.NewLine, logMessages.ToArray());
            if (msg.Length > 32000) msg = msg.Substring(0, 32000);
            MessageBox.Show(msg, "Import Elements", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
            
        public DataTable ConvertTableToDataTable(ITable table, bool includeDataType)
        {
            List<string[]> tableList = new List<string[]>();
            int rowNumber = 0;

            // get all column names
            List<string> colNames = new List<string>();

            // get property column names
            List<string> propColNames = new List<string>();
            if (includeDataType == true)
            {
                var row = table.Rows.Create();
                foreach (IProperty prop in row.Properties)
                {
                    var propUnits = GetUnitString(prop);
                    if (propUnits.Length > 0 && propUnits != "none")
                    {
                        colNames.Add(prop.Name + "(" + propUnits + ")");
                    }
                    else
                    {
                        colNames.Add(prop.Name);                        
                    }
                    propColNames.Add(prop.Name);

                }
                table.Rows.Remove(row);
            }
            else
            {
                foreach (var col in table.Columns)
                {
                    colNames.Add(col.Name);
                    propColNames.Add(col.Name);
                }
            }
            // get state column names
            List<string> stateColNames = new List<string>();
            foreach (var stateCol in table.StateColumns)
            {
                colNames.Add(stateCol.Name);
                stateColNames.Add(stateCol.Name);
            }
            tableList.Add(colNames.ToArray());

            // Get Row Data
            foreach (var row in table.Rows)
            {
                rowNumber++;
                List<string> thisRow = new List<string>();
                // get properties
                foreach (var array in propColNames)
                {
                    if (row.Properties[array.ToString()].Value != null) thisRow.Add(row.Properties[array.ToString()].Value);
                    else thisRow.Add("");
                }
                // get states
                foreach (var array in stateColNames)
                {
                    if (table.StateRows.Count > 0 && table.StateRows[rowNumber - 1].StateValues[array.ToString()].PlanValue != null) thisRow.Add(table.StateRows[rowNumber - 1].StateValues[array.ToString()].PlanValue.ToString());
                    else thisRow.Add("");
                }
                tableList.Add(thisRow.ToArray());
            }


            // New table.
            var dataTable = new DataTable();

            // Get max columns.
            int columns = 0;
            foreach (var array in tableList)
            {
                if (array.Length > columns)
                {
                    columns = array.Length;
                }
            }

            // Add columns.
            for (int i = 0; i < columns; i++)
            {
                var array = tableList[0];
                dataTable.Columns.Add(array[i]);
            }

            // Remove Column Headings
            if (tableList.Count > 0)
            {
                tableList.RemoveAt(0);
            }

            // sort rows
            //var sortedList = list.OrderBy(x => x[0]).ThenBy(x => x[3]).ToList();

            // Add rows.
            foreach (var array in tableList)
            {
                dataTable.Rows.Add(array);
            }

            return dataTable;
        }

        private string GetUnitString(IProperty prop)
        {
            IUnitBase unitBase = prop.Unit;
            
            ITimeUnit timeUnit = unitBase as ITimeUnit;
            if (timeUnit != null)
            {
                return timeUnit.Time.ToString();
            }
            ITravelRateUnit travalrateunit = unitBase as ITravelRateUnit;
            if (travalrateunit != null)
            {
                return travalrateunit.TravelRate.ToString();
            }
            ILengthUnit lengthunit = unitBase as ILengthUnit;
            if (lengthunit != null)
            {
                return lengthunit.Length.ToString();
            }
            ICurrencyUnit currencyunit  = unitBase as ICurrencyUnit;
            if (currencyunit != null)
            {
                return currencyunit.Currency.ToString();
            }
            IVolumeUnit volumeunit = unitBase as IVolumeUnit;
            if (volumeunit != null)
            {
                return volumeunit.Volume.ToString();
            }
            IMassUnit massunit = unitBase as IMassUnit;
            if (massunit != null)
            {
                return massunit.Mass.ToString();
            }
            IVolumeFlowRateUnit volumeflowrateunit = unitBase as IVolumeFlowRateUnit;
            if (volumeflowrateunit != null)
            {
                return volumeflowrateunit.Volume.ToString() + "/" + volumeflowrateunit.Time.ToString();
            }
            IMassFlowRateUnit massflowrateunit = unitBase as IMassFlowRateUnit;
            if (massflowrateunit != null)
            {
                return massflowrateunit.Mass.ToString() + "/" + massflowrateunit.Time.ToString();
            }
            ITravelAccelerationUnit timeaccelerationunit = unitBase as ITravelAccelerationUnit;
            if (timeaccelerationunit != null)
            {
                return timeaccelerationunit.Length.ToString() + "/" + timeaccelerationunit.Time.ToString();
            }
            ICurrencyPerTimeUnit currencepertimeunit = unitBase as ICurrencyPerTimeUnit;
            if (currencepertimeunit != null)
            {
                return currencepertimeunit.CurrencyPerTimeUnit.ToString();
            }

            return "none";
        }

        private void SetUnit(IProperty prop, string units)
        {
            IUnitBase unitBase = prop.Unit;

            ITimeUnit timeUnit = unitBase as ITimeUnit;
            if (timeUnit != null)
            {
                TimeUnit updateTimeUnit;
                if (Enum.TryParse<TimeUnit>(units, out updateTimeUnit) == true)
                {
                    timeUnit.Time = updateTimeUnit;
                    return;
                }
            }

            ITravelRateUnit travalrateunit = unitBase as ITravelRateUnit;
            if (travalrateunit != null)
            {
                TravelRateUnit updateTravelRateUnit;
                if (Enum.TryParse<TravelRateUnit>(units, out updateTravelRateUnit) == true)
                {
                    travalrateunit.TravelRate = updateTravelRateUnit;
                    return;
                }
            }

            ILengthUnit lengthunit = unitBase as ILengthUnit;
            if (lengthunit != null)
            {
                LengthUnit updateLengthUnit;
                if (Enum.TryParse<LengthUnit>(units, out updateLengthUnit) == true)
                {
                    lengthunit.Length = updateLengthUnit;
                    return;
                }
            }
            ICurrencyUnit currencyunit = unitBase as ICurrencyUnit;
            if (currencyunit != null)
            {
                currencyunit.Currency = units;
                return;
            }
            IVolumeUnit volumeunit = unitBase as IVolumeUnit;
            if (volumeunit != null)
            {
                VolumeUnit updateVolumeUnit;
                if (Enum.TryParse<VolumeUnit>(units, out updateVolumeUnit) == true)
                {
                    volumeunit.Volume = updateVolumeUnit;
                    return;
                }
            }
            IMassUnit massunit = unitBase as IMassUnit;
            if (massunit != null)
            {
                MassUnit updateMassUnit;
                if (Enum.TryParse<MassUnit>(units, out updateMassUnit) == true)
                {
                    massunit.Mass = updateMassUnit;
                    return;
                }
            }
            IVolumeFlowRateUnit volumeflowrateunit = unitBase as IVolumeFlowRateUnit;
            if (volumeflowrateunit != null)
            {
                string[] unitArr = units.Split('/');
                if (unitArr.Length == 2)
                {
                    VolumeUnit updateVolumeUnit;
                    TimeUnit updateTimeUnit;
                    if (Enum.TryParse<VolumeUnit>(unitArr[0], out updateVolumeUnit) == true && Enum.TryParse<TimeUnit>(unitArr[1], out updateTimeUnit) == true)
                    {
                        volumeflowrateunit.Volume = updateVolumeUnit;
                        volumeflowrateunit.Time = updateTimeUnit;
                        return;
                    }
                }
            }
            IMassFlowRateUnit massflowrateunit = unitBase as IMassFlowRateUnit;
            if (massflowrateunit != null)
            {
                string[] unitArr = units.Split('/');
                if (unitArr.Length == 2)
                {
                    MassUnit updateMassUnit;
                    TimeUnit updateTimeUnit;
                    if (Enum.TryParse<MassUnit>(unitArr[0], out updateMassUnit) == true && Enum.TryParse<TimeUnit>(unitArr[1], out updateTimeUnit) == true)
                    {
                        massflowrateunit.Mass = updateMassUnit;
                        massflowrateunit.Time = updateTimeUnit;
                        return;
                    }
                }
            }
            ITravelAccelerationUnit timeaccelerationunit = unitBase as ITravelAccelerationUnit;
            if (timeaccelerationunit != null)
            {
                string[] unitArr = units.Split('/');
                if (unitArr.Length == 2)
                {
                    LengthUnit updateLengthUnit;
                    TimeUnit updateTimeUnit;
                    if (Enum.TryParse<LengthUnit>(unitArr[0], out updateLengthUnit) == true && Enum.TryParse<TimeUnit>(unitArr[1], out updateTimeUnit) == true)
                    {
                        timeaccelerationunit.Length = updateLengthUnit;
                        timeaccelerationunit.Time = updateTimeUnit;
                        return;
                    }
                }
            }
            ICurrencyPerTimeUnit currencepertimeunit = unitBase as ICurrencyPerTimeUnit;
            if (currencepertimeunit != null)
            {
                currencepertimeunit.CurrencyPerTimeUnit = units;
                return;
            }

        }

        private void StartWriter(string filename)
        {
            StartEndWriter(filename, false);
        }

        private void EndWriter()
        {
            StartEndWriter("", true);
        }

        private void StartEndWriter(string filename, bool end)
        {
            if (end)
            {
                _writer.Close();

                _writer.Dispose();
            }
            if (!String.IsNullOrEmpty(filename))
            {
                _writer = new System.IO.StreamWriter(filename);                
            }
        }

        private void StartReader(string filename)
        {
            StartEndReader(filename, false);
        }

        private void EndReader()
        {
            StartEndReader("", true);
        }

        private void StartEndReader(string filename, bool end)
        {
            if (end)
            {
                _reader.Close();

                _reader.Dispose();
            }
            if (!String.IsNullOrEmpty(filename))
            {
                _reader = new System.IO.StreamReader(filename);
            }
        }

        //#region IDesignAddInGuiDetails Members

        //public string CategoryName
        //{
        //    get { return "Table Tools"; }
        //}

        //public string TabName
        //{
        //    get { return "Content"; }
        //}

        //public string GroupName
        //{
        //    get { return "Add-Ins"; }
        //}

        //#endregion

    }
}
