using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;

namespace XMLTVGrabber
{
	[Serializable]
	public class PData
	{
		private static readonly string FILE_NAME = Directory.GetCurrentDirectory() + "\\XMLTVGrabber.dat";

		private object _lock = new object();
		private Dictionary<int, Channel> m_channels;
		private List<int> m_checkedChannels;

		public string XMLTVFilePath { get; set; }
		public string XMLTV2MXFPath { get; set; }
		public bool UseXMLTV2MXF { get; set; }
		public string ChannelsIconsPath { get; set; }
		public bool DownloadIcons { get; set; }
		

		public List<Channel> All { get { return m_channels.Values.ToList(); } }
		public Channel this[int id] {
			get {
				if (m_channels.ContainsKey(id))
					return m_channels[id];
				return null;
			}
		}
		public IEnumerable<Channel> CheckedChannels {
			get {
				foreach (int id in m_checkedChannels)
					yield return this[id];
			}
		}

		public void Clear()
		{
			m_channels.Clear();
			m_checkedChannels.Clear();
		}

		private int Find(Channel ch)
		{
			foreach (int key in m_channels.Keys)
				if (m_channels[key].Name.ToLower().Equals(ch.Name.ToLower()))
					return key;
			return -1;
		}

		public bool AddChannel(int id, Channel channel)
		{
			bool ret = false;
			lock (_lock) {
				if (m_channels.ContainsKey(id)) {
					m_channels[id].Supplier = Channel.Supplier_E.General;
				} else {
					m_channels.Add(id, channel);
					ret = true;
				}
			}
			return ret;
		}

		private PData()
		{
			m_channels = new Dictionary<int, Channel>();
			m_checkedChannels = new List<int>();
		}

		public bool IsChecked(int id)
		{
			return m_checkedChannels.Contains(id);
		}

		public bool Check(int id)
		{
			if (m_channels.ContainsKey(id)) {
				if (!IsChecked(id)) {
					m_checkedChannels.Add(id);
					return true;
				} else {
					m_checkedChannels.Remove(id);
					return false;
				}
			}
			return false;
		}

		public static PData Load()
		{
			object o = Common.Deserialize(FILE_NAME);
			if (o != null) {
				if (o is PData) {
					PData pdata = (PData)o;
					if (pdata.ChannelsIconsPath == null)
						pdata.ChannelsIconsPath = "";
					if (pdata.XMLTV2MXFPath == null)
						pdata.XMLTV2MXFPath = "";
					if (pdata.XMLTVFilePath == null)
						pdata.XMLTVFilePath = "";
					return pdata;
				}
			}
			return new PData();
		}

		public bool Save(out string error)
		{
			error = null;
			
			if (XMLTV2MXFPath.Length > 0 && !XMLTV2MXFPath.EndsWith("\\"))
				XMLTV2MXFPath += "\\";

			string xmltvPathToCheck = XMLTVFilePath;
			int idx = XMLTVFilePath.LastIndexOf('\\');
			if (idx > -1)
				xmltvPathToCheck = XMLTVFilePath.Substring(0, idx);
			if (!Directory.Exists(xmltvPathToCheck)) {
				error = "XMLTV File path error";
				return false;
			}

			if (DownloadIcons) {
				if (!Directory.Exists(ChannelsIconsPath)) {
					error = "Icons folder doesn't exist";
					return false;
				}
			}

			if (UseXMLTV2MXF) {
				if (!File.Exists(XMLTV2MXFPath + "XMLTV2MXF_WPF.exe")) {
					error = "XMLTV2MXF_WPF.exe is missing";
					return false;
				}
				if (!File.Exists(XMLTV2MXFPath + "XMLTV2MXF_WPF.exe.config")) {
					error = "XMLTV2MXF_WPF.exe.config is missing";
					return false;
				}
			}

			return Common.Serialize(this, FILE_NAME);
		}
	}
}
