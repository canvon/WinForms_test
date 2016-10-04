using System;
using System.Windows.Forms;

namespace DfViewer
{
	public class MainForm : Form
	{
		protected ListView listViewDfStatus;


		public MainForm()
		{
			//
			// listViewDfStatus
			//
			listViewDfStatus = new ListView();
			listViewDfStatus.Name = "listViewDfStatus";
			listViewDfStatus.Dock = DockStyle.Fill;
			listViewDfStatus.Columns.Add("Path");
			listViewDfStatus.Columns.Add("Size");
			listViewDfStatus.Columns.Add("Used");
			listViewDfStatus.Columns.Add("Free");
			listViewDfStatus.Columns.Add("Beam");
			listViewDfStatus.View = View.Details;

			//
			// MainForm
			//
			this.Name = "MainForm";
			this.Text = "df viewer";

			this.Controls.Add(listViewDfStatus);


			LoadDfStatuses();
		}


		public void LoadDfStatuses()
		{
			listViewDfStatus.Items.Clear();
			listViewDfStatus.Items.Add(new ListViewItem(new string[] {
				"/", "500000", "400000", "100000", string.Empty
			}));
			listViewDfStatus.Items.Add(new ListViewItem(new string[] {
				"/dev", "10", "1", "9", string.Empty
			}));
			listViewDfStatus.Items.Add(new ListViewItem(new string[] {
				"/boot", "500", "200", "300", string.Empty
			}));
		}
	}
}
