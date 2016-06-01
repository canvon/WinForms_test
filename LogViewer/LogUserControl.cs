using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace LogViewer
{
	public class LogUserControl : UserControl
	{
		protected Label LabelLogName;
		protected ListBox ListBoxLog;
		protected Button ButtonReadLog;

		private string _LogName;
		public string LogName {
			get { return _LogName; }
			set {
				_LogName = value;
				this.LabelLogName.Text = string.Format("{0} log",
					string.IsNullOrWhiteSpace(_LogName) ?
					    "Unnamed" :
					    _LogName
				);
			}
		}

		private string _LogFilePath;
		public string LogFilePath {
			get { return _LogFilePath; }
			set {
				_LogFilePath = value;
				this.ListBoxLog.Items.Clear();
				// TODO: Read log in immediately?
				this.ButtonReadLog.Enabled = !string.IsNullOrWhiteSpace(_LogFilePath);
			}
		}

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
			this.ButtonReadLog.Click += ButtonReadLog_Click;

			//
			// LogUserControl
			//

			// Add controls to user control.
			this.Controls.Add(this.LabelLogName);
			this.Controls.Add(this.ListBoxLog);
			this.Controls.Add(this.ButtonReadLog);
		}

		void ButtonReadLog_Click(object sender, EventArgs e)
		{
			Cursor prevCursor = System.Windows.Forms.Cursor.Current;
			try {
				System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
				ReadLog();
			}
			catch (Exception ex) {
				MessageBox.Show(
					string.Format("Error reading {0} log file \"{1}\":" + Environment.NewLine + "{2}",
						string.IsNullOrWhiteSpace(_LogName) ? "unnamed" : _LogName,
						_LogFilePath,
						ex.Message),
					string.Format("{0} log", string.IsNullOrWhiteSpace(_LogName) ? "Unnamed" : _LogName),
					MessageBoxButtons.OK, MessageBoxIcon.Error
				);
				return;
			}
			finally {
				System.Windows.Forms.Cursor.Current = prevCursor;
			}
		}

		public void ReadLog()
		{
			if (string.IsNullOrWhiteSpace(_LogFilePath))
				throw new InvalidOperationException("Attempted reading log when no log file name was set");

			try {
				this.ListBoxLog.BeginUpdate();
				this.ListBoxLog.Items.Clear();
				using (StreamReader reader = new StreamReader(_LogFilePath)) {
					while (!reader.EndOfStream)
						this.ListBoxLog.Items.Add(reader.ReadLine());
				}
			}
			finally {
				this.ListBoxLog.EndUpdate();
			}

			this.ListBoxLog.SelectedIndex = this.ListBoxLog.Items.Count - 1;
		}
	}
}
