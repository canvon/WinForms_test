using System;
using System.Drawing;
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
			listViewDfStatus.OwnerDraw = true;
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


			listViewDfStatus.DrawColumnHeader += ListViewDfStatus_DrawColumnHeader;
			listViewDfStatus.DrawItem += ListViewDfStatus_DrawItem;
			listViewDfStatus.DrawSubItem += ListViewDfStatus_DrawSubItem;
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

		void ListViewDfStatus_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
		{
			e.DrawDefault = true;
		}

		void ListViewDfStatus_DrawItem(object sender, DrawListViewItemEventArgs e)
		{
			//e.DrawDefault = true;  // This skips drawing the beam. :( So..:
			e.DrawBackground();
			e.DrawText();
			e.DrawFocusRectangle();
		}

		void ListViewDfStatus_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
		{
			if (e.ColumnIndex != 4) {
				e.DrawDefault = true;
				return;
			}

			// Draw a beam ourselves.
			Rectangle rect = e.Bounds;
			e.Graphics.FillRectangle(Brushes.Blue, rect);
			e.Graphics.FillRectangle(Brushes.Aqua, Rectangle.Inflate(rect, -3, -3));
		}
	}
}
