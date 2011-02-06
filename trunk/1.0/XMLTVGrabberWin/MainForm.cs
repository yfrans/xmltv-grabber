using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;
using System.Xml;
using XMLTVGrabber;
using System.Runtime.Remoting.Messaging;

namespace XMLTVGrabberWin
{
	public partial class MainForm : Form
	{
		private PData _data;
		private PAction _actions;
		private Logger _logger;

		private bool _saved;

		public MainForm()
		{
			InitializeComponent();

			MainFormUI.Initialize(this);

			_saved = true;

			_logger = new Logger("XMLTVGrabber", "0.1a");
			_logger.OnLog += new Logger.OnLogEvtArgs(_logger_OnLog);

			_logger.LogInfo("Started");

			_logger.LogInfo("Loading data file");
			_data = PData.Load();
			_actions = new PAction(_data, _logger);

			MainFormUI.EnableXMLTV2MXF(_data.UseXMLTV2MXF);
			MainFormUI.EnableChannelsIcons(_data.DownloadIcons);

			chkBoxUseXMLTV2MXF.Checked = _data.UseXMLTV2MXF;
			chkBoxDownloadIcons.Checked = _data.DownloadIcons;

			chkBoxUseXMLTV2MXF.CheckedChanged += new EventHandler(chkBoxUseXMLTV2MXF_CheckedChanged);
			chkBoxDownloadIcons.CheckedChanged += new EventHandler(chkBoxDownloadIcons_CheckedChanged);

			txtXMLTV2MXFPath.Text = _data.XMLTV2MXFPath;
			txtIconsPath.Text = _data.ChannelsIconsPath;
			txtXMLTVPath.Text = _data.XMLTVFilePath;

			FillChannelsList();
		}

		void _logger_OnLog(string msg, Logger.MessageType mt)
		{
			string date = DateTime.Now.ToString("HH:mm:ss\t");
			switch (mt) {
				case Logger.MessageType.ERROR:
					MainFormUI.LogMessage(date + "Error: " + msg);
					break;
				case Logger.MessageType.WARNING:
					MainFormUI.LogMessage(date + "Warning: " + msg);
					break;
				case Logger.MessageType.INFO:
					MainFormUI.LogMessage(date + msg);
					break;
			}
		}

		private void FillChannelsList()
		{
			chkLbChannels_General.ItemCheck -= chkLbChannels_ItemCheck;
			chkLbChannels_HOT.ItemCheck -= chkLbChannels_ItemCheck;
			chkLbChannels_YES.ItemCheck -= chkLbChannels_ItemCheck;
			MainFormUI.ClearChannelsGeneral();
			MainFormUI.ClearChannelsHOT();
			MainFormUI.ClearChannelsYES();
			foreach (Channel ch in _data.All) {
				switch (ch.Supplier) {
					case Channel.Supplier_E.HOT:
						MainFormUI.AddChannelHOT(ch, _data.IsChecked(ch.Id));
						break;
					case Channel.Supplier_E.YES:
						MainFormUI.AddChannelYES(ch, _data.IsChecked(ch.Id));
						break;
					case Channel.Supplier_E.General:
						MainFormUI.AddChannelGeneral(ch, _data.IsChecked(ch.Id));
						break;
				}
			}
			chkLbChannels_General.ItemCheck += chkLbChannels_ItemCheck;
			chkLbChannels_HOT.ItemCheck += chkLbChannels_ItemCheck;
			chkLbChannels_YES.ItemCheck += chkLbChannels_ItemCheck;
		}

		private void chkLbChannels_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			Channel ch = (Channel)((CheckedListBox)sender).Items[e.Index];
			e.NewValue =  _data.Check(ch.Id) ? CheckState.Checked : CheckState.Unchecked;
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			SaveData();
		}

		private bool _creatingXMLTV = false;
		private void btnCreateXMLTV_Click(object sender, EventArgs e)
		{
			if (!_creatingXMLTV) {
				DaysCountForm dcf = new DaysCountForm();
				if (dcf.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
					_creatingXMLTV = true;
					MainFormUI.CreatingXMLTV(_creatingXMLTV);
					CreateXMLTVDelegate cxmltvDlg = new CreateXMLTVDelegate(_actions.CreateXMLTV);
					cxmltvDlg.BeginInvoke(dcf.Days, CreateXMLTVCallback, null);
				}
			} else {
				MainFormUI.CreatingXMLTVCancel();
				_actions.StopCurrentAction();
			}
		}

		private bool _updatingChannels = false;
		private void btnUpdChannels_Click(object sender, EventArgs e)
		{
			if (!_updatingChannels) {
				if (FormUI.Ask("Update channels? (your checked channels will be saved)") == System.Windows.Forms.DialogResult.Yes) {
					_saved = false;
					_updatingChannels = true;
					MainFormUI.UpdatingChannels(_updatingChannels);
					UpdateChannelsDelegate updChDlg = new UpdateChannelsDelegate(_actions.UpdateChannels);
					updChDlg.BeginInvoke(UpdateChannelsCallback, null);
				}
			} else {
				MainFormUI.UpdatingChannelsCancel();
				_actions.StopCurrentAction();
			}
		}

		private delegate void CreateXMLTVDelegate(int days);
		private void CreateXMLTVCallback(IAsyncResult iar)
		{
			_creatingXMLTV = false;
			MainFormUI.CreatingXMLTV(_creatingXMLTV);
			_logger.LogInfo("Done");
		}

		private delegate void UpdateChannelsDelegate();
		private void UpdateChannelsCallback(IAsyncResult iar)
		{
			_updatingChannels = false;
			_logger.LogInfo("Found " + _data.All.Count + " channels");
			FillChannelsList();
			MainFormUI.UpdatingChannels(_updatingChannels);
		}

		private void btnBrowseXMLTVPath_Click(object sender, EventArgs e)
		{
			string path = MainFormUI.BrowseFile();
			if (path != null) {
				_saved = false;
				_data.XMLTVFilePath = path;
				txtXMLTVPath.Text = path;
			}
		}

		private void btnBrowseXMLTV2MXFPath_Click(object sender, EventArgs e)
		{
			string path = MainFormUI.BrowseDirectory();
			if (path != null) {
				_saved = false;
				_data.XMLTV2MXFPath = path;
				txtXMLTV2MXFPath.Text = path;
			}
		}

		private void btnBrowseIconPath_Click(object sender, EventArgs e)
		{
			string path = MainFormUI.BrowseDirectory();
			if (path != null) {
				_saved = false;
				_data.ChannelsIconsPath = path;
				txtIconsPath.Text = path;
			}
		}

		private void chkBoxUseXMLTV2MXF_CheckedChanged(object sender, EventArgs e)
		{
			_saved = false;
			_data.UseXMLTV2MXF = chkBoxUseXMLTV2MXF.Checked;
			MainFormUI.EnableXMLTV2MXF(chkBoxUseXMLTV2MXF.Checked);
		}

		private void chkBoxDownloadIcons_CheckedChanged(object sender, EventArgs e)
		{
			_saved = false;
			_data.DownloadIcons = chkBoxDownloadIcons.Checked;
			MainFormUI.EnableChannelsIcons(chkBoxDownloadIcons.Checked);
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!_saved) {
				DialogResult sr = MessageBox.Show("You have unsaved changes. Save before close?", "Unsaved changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				switch (sr) {
					case System.Windows.Forms.DialogResult.Yes:
						if (!SaveData())
							e.Cancel = true;
						break;
					case System.Windows.Forms.DialogResult.Cancel:
						e.Cancel = true;
						break;
					case System.Windows.Forms.DialogResult.No:
						e.Cancel = false;
						break;
				}
			} else {
				e.Cancel = false;
			}
		}

		private bool SaveData()
		{
			string error;
			if (!_data.Save(out error)) {
				if (error != null)
					FormUI.Message(error);
				else
					FormUI.Message("Error while saving data file");
				return false;
			} else {
				_saved = true;
				FormUI.Message("Data file saved!");
				return true;
			}
		}

		private void btnRunXMLTV2MXF_Click(object sender, EventArgs e)
		{
			_actions.RunXMLTV2MXF();
		}
	}
}
