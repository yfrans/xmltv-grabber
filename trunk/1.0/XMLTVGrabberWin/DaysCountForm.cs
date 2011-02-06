using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XMLTVGrabberWin
{
	public partial class DaysCountForm : Form
	{
		public int Days { get; private set; }

		public DaysCountForm()
		{
			InitializeComponent();
			cmbBoxDays.SelectedIndex = 7;
		}

		private void btnGo_Click(object sender, EventArgs e)
		{
			Days = int.Parse((string)cmbBoxDays.SelectedItem);
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Close();
		}
	}
}
