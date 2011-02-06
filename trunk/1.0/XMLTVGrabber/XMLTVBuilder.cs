using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading;

namespace XMLTVGrabber
{
	public class XMLTVBuilder
	{
		private const int MAX_THREADS = 8;

		private PData _data;
		private PAction _action;
		private Logger _logger;

		private int _runningThreads;
		private int _days;

		private XmlDocument _doc;
		private List<XmlElement> _programs;
		private List<XmlElement> _channels;

		public XMLTVBuilder(PData pData, PAction pAction, Logger logger)
		{
			_data = pData;
			_action = pAction;
			_logger = logger;

			_runningThreads = 0;

			_channels = new List<XmlElement>();
			_programs = new List<XmlElement>();
		}

		private delegate void ProcessChannelDelegate(Channel ch);
		private void ProcessChannel(Channel ch)
		{
			_logger.LogInfo("Channel: " + ch.Name);

			ch.Programs.Clear();
			_action.UpdatePrograms(ch, _days);

			XmlElement chId = _doc.CreateElement("channel");
			chId.SetAttribute("id", ch.Id.ToString());
			XmlElement chName = _doc.CreateElement("display-name");
			chName.SetAttribute("lang", "he");
			chName.InnerText = ch.Name;
			chId.AppendChild(chName);
			if (ch.Icon != null) {
				XmlElement chIcon = _doc.CreateElement("icon");
				chIcon.SetAttribute("src", ch.Icon);
				chId.AppendChild(chIcon);
			}
			_channels.Add(chId);

			foreach (TvProgram p in ch.Programs) {
				XmlElement program = _doc.CreateElement("programme");
				program.SetAttribute("start", p.Start.ToString("yyyyMMddHHmmss") + " +0200");
				program.SetAttribute("stop", p.End.ToString("yyyyMMddHHmmss") + " +0200");
				program.SetAttribute("channel", ch.Id.ToString());

				XmlElement length = _doc.CreateElement("length");
				length.SetAttribute("units", "minutes");
				length.InnerText = p.Length.ToString();

				XmlElement title = _doc.CreateElement("title");
				title.SetAttribute("lang", "he");
				title.InnerText = p.Name;
				program.AppendChild(title);

				XmlElement desc = _doc.CreateElement("desc");
				desc.SetAttribute("lang", "he");
				desc.InnerText = p.Description;
				program.AppendChild(desc);

				if (p.Series != null) {
					XmlElement episode = _doc.CreateElement("episode-num");
					episode.SetAttribute("system", "xmltv_ns");
					episode.InnerText = p.Series.ToString();
					program.AppendChild(episode);
					episode = _doc.CreateElement("episode-num");
					episode.SetAttribute("system", "onscreen");
					episode.InnerText = p.Series.ToHumanReadableString();
					program.AppendChild(episode);
				}
				_programs.Add(program);
			}
		}

		private void ProcessChannelCallback(IAsyncResult iar)
		{
			_runningThreads--;
		}

		private void WaitForAvailableThread()
		{
			while (_runningThreads >= MAX_THREADS)
				Thread.Sleep(300);
		}

		public XmlDocument Build(int days)
		{
			_days = days > 0 ? days : 1;
			_logger.LogInfo("Generating XMLTV file");
			_logger.LogInfo("# of days: " + _days);

			_doc = new XmlDocument();
			_doc.AppendChild(_doc.CreateXmlDeclaration("1.0", "UTF-8", null));
			XmlElement root = _doc.CreateElement("tv");
			_doc.AppendChild(root);

			ProcessChannelDelegate pcDlg = new ProcessChannelDelegate(ProcessChannel);
			foreach (Channel ch in _data.CheckedChannels) {
				WaitForAvailableThread();
				_runningThreads++;
				pcDlg.BeginInvoke(ch, ProcessChannelCallback, null);
			}
			while (_runningThreads > 0)
				Thread.Sleep(300);

			foreach (XmlElement channel in _channels)
				root.AppendChild(channel);

			foreach (XmlElement program in _programs)
				root.AppendChild(program);

			return _doc;
		}

		public XmlDocument BuildChannels()
		{
			_doc = new XmlDocument();
			_doc.AppendChild(_doc.CreateXmlDeclaration("1.0", "UTF-8", "yes"));
			XmlElement root = _doc.CreateElement("Channels");
			_doc.AppendChild(root);
			int id = 0;
			foreach (Channel ch in _data.CheckedChannels) {
				XmlElement channel = _doc.CreateElement("Channel");
				channel.SetAttribute("id", id++.ToString());
				channel.SetAttribute("cid", ch.Id.ToString());
				channel.SetAttribute("name", ch.Name);
				channel.SetAttribute("logo", ch.Icon);
				root.AppendChild(channel);
			}
			return _doc;
		}
	}
}
