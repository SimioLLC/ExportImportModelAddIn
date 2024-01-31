using SimioAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimioAPIHelpers
{
    /// <summary>
    /// Utility to demonstrate how to access and output Simio Logs.
    /// The logs are each defined with their own interface
    /// </summary>
    public static class LogHelpers
    {
        public static DataTable ConvertLogToDataTable<T>(IModel model, ILogExpressions logExpressions, ISimioCollection<T> logs, string tableName)
        {
            DataTable dt = new DataTable(typeof(T).ToString());
            dt.TableName = tableName;

            if ( !logs.Any())
                return dt;

            // Create a DataTable Column for each property of the Simio Log
            List<string> columnNames = new List<string>();
            foreach (PropertyInfo pi in typeof(T).GetProperties())
                dt.Columns.Add(pi.Name );

            // ... and also create a Column for each Custom Column
            foreach (ILogExpression logExpression in logExpressions)
            {
                dt.Columns.Add(logExpression.DisplayName);
            }

            // Find the method the fetches the value for the custom column using the DisplayName
            // We'll use it below as we invoke it for each custom column in each record.
            MethodInfo GetCustomValueMethod = null;

            foreach (MethodInfo mi in typeof(T).GetMethods())
            {
                if (mi.Name == "GetCustomColumnValue")
                {
                    ParameterInfo[] parameters = mi.GetParameters();
                    if (parameters.Length == 1)
                    {
                        GetCustomValueMethod = mi;
                        goto DoneLookingForMethod;
                    }
                }
            DoneLookingForMethod:;
            }

            int recordCount = 0;
            // each record in the log
            foreach (var record in logs)
            {
                recordCount += 1;
                DataRow dr = dt.NewRow();

                // Look at each property in the record and get its name and value (as a string)
                foreach (PropertyInfo pi in typeof(T).GetProperties())
                {
                    try
                    {
                        object fieldValue = pi.GetValue(record) ?? "";
                        dr[pi.Name] = fieldValue.ToString();
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException($"Record={recordCount}. Column={pi.Name} Err={ex}");
                    }

                } // for each property

                // Now add the Custom columns
                // Invoke our previously found method on the current record for each name of custom columns.
                foreach (ILogExpression logExpression in logExpressions)
                {
                    string expressionName = logExpression.DisplayName;

                    try
                    {
                        object value = GetCustomValueMethod?.Invoke(record, new object[] { expressionName });
                        dr[expressionName] = value.ToString();
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException($"Record={recordCount}. Custom Column={expressionName} Err={ex}");
                    }
                }

                dt.Rows.Add(dr);
            } // for each record

            dt.AcceptChanges();
            return dt;

        } // method
    } // class



}
