﻿using System;
using System.Windows.Forms;

namespace LogViewer
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
