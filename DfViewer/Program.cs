using System;
using System.Windows.Forms;

namespace DfViewer
{
	public class Program
	{
		[STAThread]
		public static void Main(string[] args)
		{
			Application.EnableVisualStyles();

			Application.Run(new MainForm());
		}
	}
}
