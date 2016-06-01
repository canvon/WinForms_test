using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace LogViewer
{
	public class LogUserControl : UserControl
	{
		protected Label LabelLogName;
		protected RichTextBox RichTextBoxLog;
		protected Button ButtonReadLog;

		/// <summary>
		/// A user-friendly name of the log.
		/// Will be "Unnamed"/"unnamed" (depending on context) when not set.
		/// </summary>
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

		/// <summary>
		/// The log file path.
		/// Can be set to <code>null</code>, but the button to read the log in
		/// will be disabled, and attempting to read the log programmatically
		/// will throw an exception (see <see cref="ReadLog()"/>).
		/// </summary>
		private string _LogFilePath;
		public string LogFilePath {
			get { return _LogFilePath; }
			set {
				_LogFilePath = value;
				this.RichTextBoxLog.Clear();
				// TODO: Read log in immediately?
				this.ButtonReadLog.Enabled = !string.IsNullOrWhiteSpace(_LogFilePath);
			}
		}

		public LogUserControl()
		{
			// Create controls.
			this.LabelLogName = new Label();
			this.RichTextBoxLog = new RichTextBox();
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
			// RichTextBoxLog
			//
			this.RichTextBoxLog.Name = "RichTextBoxLog";
			this.RichTextBoxLog.Location = new Point(
				this.RichTextBoxLog.Margin.Left,
				this.LabelLogName.Bottom + this.LabelLogName.Margin.Bottom + this.RichTextBoxLog.Margin.Top
			);
			this.RichTextBoxLog.ScrollBars = RichTextBoxScrollBars.Both;

			//
			// ButtonReadLog
			//
			this.ButtonReadLog.Name = "ButtonReadLog";
			this.ButtonReadLog.Text = "Read log";
			this.ButtonReadLog.Location = new Point(
				this.ButtonReadLog.Margin.Left,
				this.RichTextBoxLog.Bottom + this.RichTextBoxLog.Margin.Bottom + this.ButtonReadLog.Margin.Top
			);
			this.ButtonReadLog.Click += ButtonReadLog_Click;

			//
			// LogUserControl
			//

			// Add controls to user control.
			this.Controls.Add(this.LabelLogName);
			this.Controls.Add(this.RichTextBoxLog);
			this.Controls.Add(this.ButtonReadLog);

			// Do post-layout that would not have been done
			// by Designer-generated code; but it would have
			// been layouted statically then, with seemingly
			// magic size values in the code, which we don't
			// have.  So do this.
			this.RichTextBoxLog.Size = new Size(
				this.ClientSize.Width - this.RichTextBoxLog.Left - this.RichTextBoxLog.Margin.Right,
				this.ClientSize.Height
				- this.LabelLogName.Bottom - this.LabelLogName.Margin.Bottom
				- this.ButtonReadLog.Height - this.ButtonReadLog.Margin.Vertical
			);
			this.RichTextBoxLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
				| AnchorStyles.Left | AnchorStyles.Right;
			this.ButtonReadLog.Width = this.ClientSize.Width
				- this.ButtonReadLog.Left - this.ButtonReadLog.Margin.Right;
			this.ButtonReadLog.Anchor = AnchorStyles.Bottom
				| AnchorStyles.Left | AnchorStyles.Right;


			// Now do the non-WinForms initialization
			LogName = null;
			LogFilePath = null;
		}

		void ButtonReadLog_Click(object sender, EventArgs e)
		{
			Cursor prevCursor = System.Windows.Forms.Cursor.Current;
			try {
				System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
				ReadLogSafe();
			}
			finally {
				System.Windows.Forms.Cursor.Current = prevCursor;
			}
		}

		/// <summary>
		/// Reads the log from <see cref="LogFilePath"/> safely, i.e.,
		/// exceptions are caught and turned to a <c>MessageBox.Show()</c>
		/// invocation. Success can still be determined, by looking at the
		/// return value.
		/// </summary>
		/// <returns><c>true</c> if log was successfully read,
		/// <c>false</c> otherwise.</returns>
		public bool ReadLogSafe()
		{
			try {
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
				return false;
			}
			return true;
		}

		/// <summary>
		/// Reads the log from <see cref="LogFilePath"/>, or throws an
		/// <code>InvalidOperationException</code> if that is not set.
		/// </summary>
		public void ReadLog()
		{
			if (string.IsNullOrWhiteSpace(_LogFilePath))
				throw new InvalidOperationException("Attempted reading log when no log file name was set");

			this.RichTextBoxLog.Clear();
			var builder = new StringBuilder();
			using (StreamReader reader = new StreamReader(_LogFilePath)) {
				while (!reader.EndOfStream)
					builder.AppendLine(reader.ReadLine());
			}
			this.RichTextBoxLog.Text = builder.ToString();
			this.RichTextBoxLog.SelectionStart = builder.Length;
		}
	}
}
