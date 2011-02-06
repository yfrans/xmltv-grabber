using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XMLTVGrabber;
using System.Windows.Forms;

namespace XMLTVGrabberWin
{
	public class MainFormUI : FormUI
	{
		public static void LogMessage(string msg) { LogMessage(msg, true); }
		public static void LogMessage(string msg, bool newLine)
		{
			if (newLine)
				msg += "\n";
			ChangeText("rtbLog", msg, true);
			ScrollToCaret("rtbLog");
			Invalidate("rtbLog");
		}

		public static void AddChannelGeneral(object obj, bool isChecked) { AddCheckedListValue("chkLbChannels_General", obj, isChecked); }
		public static void AddChannelHOT(object obj, bool isChecked) { AddCheckedListValue("chkLbChannels_HOT", obj, isChecked); }
		public static void AddChannelYES(object obj, bool isChecked) { AddCheckedListValue("chkLbChannels_YES", obj, isChecked); }

		public static void ClearChannelsGeneral() { ClearCheckedListBoxValues("chkLbChannels_General"); }
		public static void ClearChannelsHOT() { ClearCheckedListBoxValues("chkLbChannels_HOT"); }
		public static void ClearChannelsYES() { ClearCheckedListBoxValues("chkLbChannels_YES"); }

		public static void CreatingXMLTV(bool updating)
		{
			EnableDisableControl("tabControl1", !updating);
			EnableDisableControl("btnUpdChannels", !updating);
			EnableDisableControl("btnSave", !updating);
			ChangeText("btnCreateXMLTV", (updating ? "Cancel" : "Create XMLTV"), false);
			EnableDisableControl("btnCreateXMLTV", true);
		}

		public static void UpdatingChannels(bool updating)
		{
			EnableDisableControl("tabControl1", !updating);
			EnableDisableControl("btnCreateXMLTV", !updating);
			EnableDisableControl("btnSave", !updating);
			ChangeText("btnUpdChannels", (updating ? "Cancel" : "Update Channels"), false);
			EnableDisableControl("btnUpdChannels", true);
		}

		public static string BrowseFile()
		{
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.CheckPathExists = true;
			sfd.DefaultExt = ".xml";
			sfd.Filter = "XML File|*.xml";
			sfd.OverwritePrompt = true;
			if (sfd.ShowDialog() == DialogResult.OK)
				return sfd.FileName;
			return null;
		}

		public static string BrowseDirectory()
		{
			FolderBrowserDialog fbd = new FolderBrowserDialog();
			fbd.ShowNewFolderButton = true;
			if (fbd.ShowDialog() == DialogResult.OK)
				return fbd.SelectedPath;
			return null;
		}

		public static void EnableXMLTV2MXF(bool enable)
		{
			EnableDisableControl("txtXMLTV2MXFPath", enable);
			EnableDisableControl("btnBrowseXMLTV2MXFPath", enable);
			EnableDisableControl("btnRunXMLTV2MXF", enable);
		}

		public static void EnableChannelsIcons(bool enable)
		{
			EnableDisableControl("txtIconsPath", enable);
			EnableDisableControl("btnBrowseIconPath", enable);
		}

		public static void CreatingXMLTVCancel() { EnableDisableControl("btnCreateXMLTV", false); }
		public static void UpdatingChannelsCancel() { EnableDisableControl("btnUpdChannels", false); }
	}
}
