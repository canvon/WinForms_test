using System;
using System.Drawing;
using System.Windows.Forms;

namespace LogViewer
{
	public class MainForm : Form
	{
		protected LogUserControl LogUC1;

		public MainForm()
		{
			// Create controls.
			this.LogUC1 = new LogUserControl();

			//
			// LogUC1
			//
			this.LogUC1.Name = "LogUC1";
			this.LogUC1.Location = new Point(
				this.LogUC1.Margin.Left,
				this.LogUC1.Margin.Top
			);
			this.LogUC1.LogName = "nonexistent";
			this.LogUC1.LogFilePath = "/var/log/nonexistent.log";

			//
			// MainForm
			//
			this.Name = "MainForm";
			this.Text = "LogViewer";
			this.Shown += MainForm_Shown;

			// Add controls to form.
			this.Controls.Add(this.LogUC1);
		}

		void MainForm_Shown(object sender, EventArgs e)
		{
			this.LogUC1.ReadLogSafe();
		}
	}
}
