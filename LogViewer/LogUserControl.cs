using System;
using System.Drawing;
using System.Windows.Forms;

namespace LogViewer
{
	public class LogUserControl : UserControl
	{
		protected Label LabelLogName;
		protected ListBox ListBoxLog;
		protected Button ButtonReadLog;

		public LogUserControl()
		{
			// Create controls.
			this.LabelLogName = new Label();
			this.ListBoxLog = new ListBox();
			this.ButtonReadLog = new Button();

			//
			// LabelLogName
			//
			this.LabelLogName.Name = "LabelLogName";
			this.LabelLogName.Text = "Unnamed log";
			this.LabelLogName.Location = new Point(
				this.LabelLogName.Margin.Left,
				this.LabelLogName.Margin.Top
			);

			//
			// ListBoxLog
			//
			this.ListBoxLog.Name = "ListBoxLog";
			this.ListBoxLog.Location = new Point(
				this.ListBoxLog.Margin.Left,
				this.LabelLogName.Bottom + this.LabelLogName.Margin.Bottom + this.ListBoxLog.Margin.Top
			);

			//
			// ButtonReadLog
			//
			this.ButtonReadLog.Name = "ButtonReadLog";
			this.ButtonReadLog.Location = new Point(
				this.ButtonReadLog.Margin.Left,
				this.ListBoxLog.Bottom + this.ListBoxLog.Margin.Bottom + this.ButtonReadLog.Margin.Top
			);

			//
			// LogUserControl
			//

			// Add controls to user control.
			this.Controls.Add(this.LabelLogName);
			this.Controls.Add(this.ListBoxLog);
			this.Controls.Add(this.ButtonReadLog);
		}
	}
}
