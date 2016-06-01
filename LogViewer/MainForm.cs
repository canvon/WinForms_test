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
			this.LogUC1.LogName = "syslog";
			this.LogUC1.LogFilePath = "/var/log/syslog";

			//
			// MainForm
			//
			this.Name = "MainForm";
			this.Text = "LogViewer";
			this.Shown += MainForm_Shown;

			// Add controls to form.
			this.Controls.Add(this.LogUC1);

			// Layout manually once, then let the anchors
			// do automatic resizing.
			this.LogUC1.Size = new Size(
				this.ClientSize.Width - this.LogUC1.Left - this.LogUC1.Margin.Right,
				this.ClientSize.Height - this.LogUC1.Top - this.LogUC1.Margin.Bottom
			);
			this.LogUC1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
				| AnchorStyles.Left | AnchorStyles.Right;
		}

		void MainForm_Shown(object sender, EventArgs e)
		{
			this.LogUC1.ReadLogSafe();
		}
	}
}
