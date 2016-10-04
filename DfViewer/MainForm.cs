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
			var dfStatuses = new DfStatus[] {
				new DfStatus("/", 500000, 400000, 100000),
				new DfStatus("/dev", 10, 1, 9),
				new DfStatus("/boot", 500, 200, 300),
			};

			listViewDfStatus.Items.Clear();
			foreach (DfStatus status in dfStatuses) {
				var item = new ListViewItem(new string[] {
					status.FilesystemPath,
					status.DfSize.ToString(),
					status.DfUsed.ToString(),
					status.DfFree.ToString(),
					string.Empty
				});
				item.Tag = status;
				listViewDfStatus.Items.Add(item);
			}
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
			Rectangle inner = Rectangle.Inflate(e.Bounds, -3, -3);
			e.Graphics.FillRectangle(Brushes.Blue, e.Bounds);
			e.Graphics.FillRectangle(Brushes.Aqua, inner);

			var status = e.Item.Tag as DfStatus;
			if (!object.ReferenceEquals(status, null)) {
				Rectangle amount = inner;
				if (status.DfSize == 0) {
					// Indicate something like an error.
					e.Graphics.FillRectangle(Brushes.Red, amount);
				}
				else {
					// Draw the actual amount, as a white bar.
					amount.Width = (int)(inner.Width * status.DfUsed / status.DfSize);
					e.Graphics.FillRectangle(Brushes.White, amount);
				}
			}
		}
	}
}
