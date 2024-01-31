using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExportImportModelAddIn
{
    public partial class StatusWindow : Form
    {
        public StatusWindow(String strCaption)
        {            
            InitializeComponent();
            this.Text = strCaption;
            this.Refresh();
        }

        public void UpdateProgress(Int32 curProgress)
        {
            progressBar1.Value = curProgress;
            progressBar1.Refresh();
        }
    }
}
