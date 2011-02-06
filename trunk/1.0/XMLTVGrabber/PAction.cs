using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using System.Xml;
using System.IO;

namespace XMLTVGrabber
{
	public class PAction
	{
		private const int MAX_THREADS = 3;

		public delegate void UILog();

		private object _lock = new object();
		private PData _data;
		private Logger _logger;
		private int _runningThreads;
		private bool _cont;

		public PAction(PData dataInstance, Logger logger)
		{
			_data = dataInstance;
			_runningThreads = 0;
			_logger = logger;
			_cont = true;
		}

		public void StopCurrentAction()
		{
			_cont = false;
			_logger.LogInfo("User aborted, please wait for last action to finish");
		}

		public void CreateXMLTV(int days) { CreateXMLTV(_data.XMLTVFilePath, days); }
		public void CreateXMLTV(string filename, int days)
		{
			_cont = true;
			_logger.LogInfo("Starting XML builder");
			XMLTVBuilder builder = new XMLTVBuilder(_data, this, _logger);
			XmlDocument doc = builder.Build(days);
			if (File.Exists(filename)) {
				_logger.LogWarning("XMLTV file already exist, deleting exiting file");
				File.Delete(filename);
			}
			_logger.LogInfo("Saving XMLTV file as " + filename);
			doc.Save(filename);

			if (_data.UseXMLTV2MXF) {
				_logger.LogInfo("Building Channels.xml file");
				doc = builder.BuildChannels();
				_logger.LogInfo("Saving Channels.xml file");
				filename = _data.XMLTV2MXFPath + "Channels.xml";
				if (File.Exists(filename)) {
					_logger.LogWarning("Channels.xml file already exist, deleting exiting file");
					File.Delete(filename);
				}
				doc.Save(filename);
			}
		}

		private void GetChannelCallback(IAsyncResult iar)
		{
			_runningThreads--;
		}

		private string GetIcon(string src, string filename)
		{
			string ext = src.Substring(src.LastIndexOf('.') + 1);
			filename += "." + ext;
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(src);
			HttpWebResponse res = (HttpWebResponse)req.GetResponse();
			Image img = Image.FromStream(res.GetResponseStream());
			if (!SaveIcon(img, ref filename))
				filename = "";
			return filename;
		}

		public bool SaveIcon(Image icon, ref string fileName)
		{
			bool ret = false;
			lock (_lock) {
				string path = _data.ChannelsIconsPath;
				fileName = path + "\\" + fileName;
				if (!Directory.Exists(path))
					Directory.CreateDirectory(path);
				if (!File.Exists(fileName)) {
					icon.Save(fileName);
					ret = true;
				}
			}
			return ret;
		}

		public void UpdateChannels()
		{
			_cont = true;

			_logger.LogInfo("Updating channels");

			_logger.LogInfo("Saving checked channels");
			List<int> checkedChannelsIds = new List<int>();
			foreach (Channel c in _data.CheckedChannels)
				checkedChannelsIds.Add(c.Id);

			_data.Clear();

			Dictionary<Channel.Supplier_E, string> urls = new Dictionary<Channel.Supplier_E, string>();
			urls.Add(Channel.Supplier_E.HOT, "http://tv.walla.co.il/?w=/2/");
			urls.Add(Channel.Supplier_E.YES, "http://tv.walla.co.il/?w=/3/");

			foreach (Channel.Supplier_E key in urls.Keys) {
				if (!_cont)
					break;

				_logger.LogInfo("Scraping " + urls[key]);

				string resText;
				if (Common.Get(urls[key], out resText)) {
					Regex channelsRgx = new Regex(@"<td[^>]+?class=\""bbr2\sch_name_td\sw2b\""[^>]+?>[^<>]*?<a\shref=\""(?<id>.*?)\""[^>]*?><span[^>]*?>(?<name>.*?)</.*?<img[^>]*?src=\""(?<icon>.*?)\""[^>]*?>", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline);
					MatchCollection channels = channelsRgx.Matches(resText);
					_logger.LogInfo("Found " + channels.Count + " channels");
					foreach (Match channel in channels) {
						string idStr = channel.Groups["id"].Value;
						int startIdx = idStr.IndexOf("//");
						if (startIdx > -1) {
							startIdx += 2;
							int endIdx = idStr.IndexOf("//", startIdx);
							if (endIdx > -1) {
								idStr = idStr.Substring(startIdx, endIdx - startIdx);
								int id;
								if (int.TryParse(idStr, out id)) {
									_data.AddChannel(id,
										new Channel() {
											Category = "",
											Id = id,
											Name = channel.Groups["name"].Value,
											Supplier = key,
											Icon = _data.DownloadIcons ? GetIcon(channel.Groups["icon"].Value, id.ToString()) : "",
										});
								}
							}
						}
					}
				}
			}

			_logger.LogInfo("Restoring checked channels");
			foreach (int id in checkedChannelsIds)
				_data.Check(id);

			_logger.LogInfo("Done");
		}

		private void WaitForAvailableThread()
		{
			while (_runningThreads >= MAX_THREADS)
				Thread.Sleep(500);
		}

		public void UpdatePrograms(Channel ch, int days) { UpdatePrograms(ch, days, true); }
		public void UpdatePrograms(Channel ch, int days, bool save)
		{
			_cont = true;

			_logger.LogInfo("Updating programs for channel: " + ch.Name + ", # days: " + days);
			
			ch.Programs.Clear();
			for (int i = 0; i < days; i++) {
				if (!_cont)
					break;
				
				string date = DateTime.Now.AddDays(i).ToString("yyyy-MM-dd");
				_logger.LogInfo("Channel: " + ch.Name + ", Date: " + date);

				string url = "http://tv.walla.co.il/?w=///" + ch.Id + "//" + date;
				string resText;
				if (Common.Get(url, out resText)) {
					Regex rgx = new Regex(@"<td[^>]+?><a[^>]*?class=\""w3b\""[^>]*?href=\""(?<url>.*?)\"">(?<title>.*?)</", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline);
					MatchCollection mc = rgx.Matches(resText);
					_logger.LogInfo("Processing " + mc.Count + " programs for channel " + ch.Name);
					int add = 0;
					foreach (Match m in mc) {
						if (!_cont)
							break;

						TvProgram tvp = GetProgramDetails("http://tv.walla.co.il/" + m.Groups["url"].Value);
						if (tvp != null) {
							if (tvp.Start.Hour == 0 && add == 0)
								add++;
							tvp.Start = tvp.Start.AddDays(add);
							tvp.End = tvp.End.AddDays(add);
							ch.Programs.Add(tvp);
						} else {
							_logger.LogError("Can't get program details for " + m.Groups["url"].Value + " (Channel " + ch.Name + ")");
						}
					}
				}
			}
//			if (save)
//				_data.Save();
		}

		public void UpdatePrograms(int days)
		{
			foreach (Channel ch in _data.CheckedChannels) {
				if (!_cont)
					break;
				UpdatePrograms(ch, days, false);
			}
//			_data.Save();
		}

		private TvProgram GetProgramDetails(string url)
		{
			TvProgram ret = null;
			string resText;
			if (Common.Get(url, out resText)) {
				Regex rgx = new Regex(@"<select[^>]*?class=\""ido\""[^>]*?>.*?<option[^>]*?value=\""(?<date>[^>]+?)\""[^>]*?selected[^>]*?>.*?<table[^>]*?class=\""wp-0-b\""[^>]*?>.*?<td[^>]*?class=\""w4b\""[^>]*?>(?<title>.*?)</.*?(?:<td[^>]*?class=\""w2\""[^>]*?><a[^>]*?class=\""w3b\""[^>]*?>.*?\:\s(?<gnr>.*?)</)?.*?<td[^>]*?class=\""w2b\""[^>]*?><span[^>]*?>.*?\:\s(?<start>.*?)</.*?<span[^>]*?>.*?\:\s(?<end>.*?)</.*?<span[^>]*?>(?<desc>.*?)</", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline);
				Match match = rgx.Match(resText);
				if (match != null) {
					string date = match.Groups["date"].Value;
					string startStr = date + " " + match.Groups["start"].Value;
					string endStr = date + " " + match.Groups["end"].Value;
					DateTime start;
					DateTime end;
					if (DateTime.TryParse(startStr, out start) &&
						DateTime.TryParse(endStr, out end)) {
							string name = match.Groups["title"].Value;
							SeriesDetail sd = ParseProgramTitle(ref name);
							ret = new TvProgram() {
								Description = match.Groups["desc"].Value,
								End = end,
								Start = start,
								Id = 0,
								Name = name,
								Series = sd,
							};
							ret.Categories.Add(match.Groups["gnr"].Value);
					}
				}
			}
			return ret;
		}

		private SeriesDetail ParseProgramTitle(ref string title)
		{
			SeriesDetail ret = null;

			title = title.Replace("&amp;", "&");
			title = title.Replace("&quot;", "\"");

			int tmp;
			if (int.TryParse(title.Trim(), out tmp))
				return ret;

			int episodeNum = 0, seassonNum = 0;
			StringBuilder sb = new StringBuilder();
			int i = title.Length - 1;
			while (Char.IsDigit(title[i]))
				sb.Insert(0, title[i--]);
			if (sb.Length > 0) {
				string[] titleSplit = title.Split('-');
				if (titleSplit.Length > 0) {
					episodeNum = int.Parse(sb.ToString());
					sb = new StringBuilder();
					titleSplit[0] = titleSplit[0].Trim();
					title = titleSplit[0];
					i = titleSplit[0].Length - 1;
					while (Char.IsDigit(titleSplit[0][i]))
						sb.Insert(0, titleSplit[0][i--]);
					if (sb.Length > 0)
						seassonNum = int.Parse(sb.ToString());
				} else {
					seassonNum = int.Parse(sb.ToString());
				}
			}

			if (episodeNum > 0 || seassonNum > 0)
				ret = new SeriesDetail() {
					EpisodeNumber = episodeNum,
					SeassonNumber = seassonNum
				};

			return ret;
		}

		public void RunXMLTV2MXF()
		{
			string confFile = _data.XMLTV2MXFPath + "\\XMLTV2MXF_WPF.exe.config";
			if (File.Exists(confFile)) {
				_logger.LogInfo("Setting XMLTV2MXF config");
				XmlDocument doc = new XmlDocument();
				doc.Load(confFile);
				foreach (XmlNode node in doc["configuration"]["userSettings"]["XMLTV2MXF_WPF.Properties.Settings"].ChildNodes) {
					if (node.Attributes["name"].Value.Equals("inputXMLTVfile")) {
						node["value"].InnerText = _data.XMLTVFilePath;
						break;
					}
				}
				doc.Save(confFile);
				_logger.LogInfo("Starting XMLTV2MXF process");

				ReadProcessOutputDelegate rpout = new ReadProcessOutputDelegate(ReadProcessOutput);
				System.Diagnostics.Process p = new System.Diagnostics.Process();
				p.StartInfo.FileName = "XMLTV2MXF_WPF.exe";
				p.StartInfo.Arguments = "/batch:true";
				p.StartInfo.CreateNoWindow = true;
				p.StartInfo.RedirectStandardOutput = true;
				p.StartInfo.UseShellExecute = false;
				p.Start();
				rpout.BeginInvoke(p.StandardOutput, null, null);
			} else {
				_logger.LogError("XMLTV2MXF config not exist");
			}
		}

		void  p_Exited(object sender, EventArgs e)
		{
			_logger.LogInfo("XMLTV2MXF finished");
		}

		private delegate void ReadProcessOutputDelegate(StreamReader sr);
		private void ReadProcessOutput(StreamReader sr)
		{
			string str;
			while ((str = sr.ReadLine()) != null)
				_logger.LogInfo(str);
		}
	}
}
