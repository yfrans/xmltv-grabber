using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using XMLTVGrabber;

namespace XMLTVGrabberWin
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			bool showWindow = true;
			string filename = "";
			int days = 1;
			int idx = 0;
			if (args.Length > 0) {
				if (args[idx].ToLower().Equals("false")) {
					showWindow = false;
					idx++;
				}
				if (args.Length > idx) {
					if (int.TryParse(args[idx], out days)) {
						days = days > 0 ? days : 1;
						days = days > 14 ? 1 : days;
						idx++;
					}
					if (args.Length > idx) {
						filename = args[idx];
					}
				}
			}

			if (showWindow) {
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new MainForm());
			} else {
				Logger lgr = new Logger("XMLTVGrabber", "0.1a");
				lgr.LogInfo("XMLTV Grabber (No Window) Started, loading data");

				PData pData = PData.Load();
				PAction pAction = new PAction(pData, lgr);

				if (filename.Length == 0)
					filename = pData.XMLTVFilePath;

				lgr.LogInfo("# days: " + days);
				lgr.LogInfo("File: " + filename);

				pAction.CreateXMLTV(filename, days);
				lgr.LogInfo("Finish!");
			}
		}
	}
}
