using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using XMLTVGrabber;

namespace Walla
{
	class Walla : IGrabber
	{
		#region Private members
		private List<string> _urls;
		#endregion

		#region IGrabber implemenation
		public event Logger.OnLogEvtArgs LogMessage;

		/// <summary>
		/// Initialize the grabber
		/// </summary>
		public void Initialize()
		{
			_urls = new List<string>();
			_urls.Add("http://tv.walla.co.il/?w=/2/");
			_urls.Add("http://tv.walla.co.il/?w=/3/");
		}


		/// <summary>
		/// The name of the grabber
		/// </summary>
		/// <returns>grabber name</returns>
		public override string ToString()
		{
			return "Walla - by YF";
		}

		/// <summary>
		/// Get the updated channel list
		/// </summary>
		/// <returns>list of channels</returns>
		public List<GrabberChannel> GetChannels()
		{
			List<GrabberChannel> ret = new List<GrabberChannel>();
			foreach (string url in _urls) {
				FireLog("Scraping " + url, Logger.MessageType.INFO);
				string resText;
				if (Common.Get(url, out resText)) {
					Regex channelsRgx = Common.InitRegex(@"<td[^>]+?class=\""bbr2\sch_name_td\sw2b\""[^>]+?>[^<>]*?<a\shref=\""(?<id>.*?)\""[^>]*?><span[^>]*?>(?<name>.*?)</.*?<img[^>]*?src=\""(?<icon>.*?)\""[^>]*?>");
					MatchCollection channels = channelsRgx.Matches(resText);
					FireLog("Found " + channels.Count + " channels", Logger.MessageType.INFO);
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
									GrabberChannel ch = new GrabberChannel() {
										Category = "",
										Name = channel.Groups["name"].Value,
										IconSrc = channel.Groups["icon"].Value,
									};
									ch.GrabberArgs = new object[] { id };
									ret.Add(ch);
								}
							}
						}
					}
				}
			}
			return ret;
		}

		/// <summary>
		/// Get the program guide for specified date
		/// </summary>
		/// <param name="args">the arguments for the grabber</param>
		/// <param name="date">the date</param>
		/// <returns>list of programs</returns>
		public List<TvProgram> GetPrograms(object[] args, DateTime date)
		{
			List<TvProgram> ret = new List<TvProgram>();
			string url = "http://tv.walla.co.il/?w=///" + (int)args[0] + "//" + date.ToString("yyyy-MM-dd");
			string resText;
			FireLog("Scraping...", Logger.MessageType.INFO);
			if (Common.Get(url, out resText)) {
				Regex rgx = Common.InitRegex(@"<td[^>]+?><a[^>]*?class=\""w3b\""[^>]*?href=\""(?<url>.*?)\"">(?<title>.*?)</");
				MatchCollection mc = rgx.Matches(resText);
				FireLog("Processing " + mc.Count + " programs", Logger.MessageType.INFO);
				int add = 0;
				foreach (Match m in mc) {
					TvProgram tvp = GetProgramDetails("http://tv.walla.co.il/" + m.Groups["url"].Value);
					if (tvp != null) {
						tvp.Start = tvp.Start.AddDays(add);
						tvp.End = tvp.End.AddDays(add);
						if (add == 0) {
							TimeSpan startEndDiff = tvp.End - tvp.Start;
							if (startEndDiff.TotalMinutes < 0) {	// End time is tomorrow
								tvp.End = tvp.End.AddDays(1);
								add++;
							}
						}
						ret.Add(tvp);
					} else {
						FireLog("Can't get program details for " + m.Groups["url"].Value, Logger.MessageType.ERROR);
					}
				}
			}
			return ret;
		}
		#endregion

		#region Helpers (private methods)
		/// <summary>
		/// Get program details for specific url
		/// </summary>
		/// <param name="url">the url</param>
		/// <returns>program details as TvProgram object</returns>
		private TvProgram GetProgramDetails(string url)
		{
			TvProgram ret = null;
			string resText;
			if (Common.Get(url, out resText)) {
				Regex rgx = Common.InitRegex(@"<select[^>]*?class=\""ido\""[^>]*?>.*?<option[^>]*?value=\""(?<date>[^>]+?)\""[^>]*?selected[^>]*?>.*?<table[^>]*?class=\""wp-0-b\""[^>]*?>.*?<td[^>]*?class=\""w4b\""[^>]*?>(?<title>.*?)</.*?(?:<td[^>]*?class=\""w2\""[^>]*?><a[^>]*?class=\""w3b\""[^>]*?>.*?\:\s(?<gnr>.*?)</)?.*?<td[^>]*?class=\""w2b\""[^>]*?><span[^>]*?>.*?\:\s(?<start>.*?)</.*?<span[^>]*?>.*?\:\s(?<end>.*?)</.*?<span[^>]*?>(?<desc>.*?)</");
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
						string desc = match.Groups["desc"].Value;
						desc = desc.Replace("&amp;", "&");
						desc = desc.Replace("&quot;", "\"");
						TvSeries sd = ParseProgramTitle(ref name);
						ret = new TvProgram() {
							Description = desc,
							End = end,
							Start = start,
							Id = 0,
							Name = name,
							Series = sd,
						};
						if (ret.Series != null)
							ret.Series.Program = ret;
						ret.Categories.Add(match.Groups["gnr"].Value);
					}
				}
			}
			return ret;
		}

		/// <summary>
		/// Fire log
		/// </summary>
		/// <param name="message">the message</param>
		/// <param name="mt">the type of the message</param>
		private void FireLog(string message, Logger.MessageType mt)
		{
			if (LogMessage != null)
				LogMessage(message, mt);
		}

		/// <summary>
		/// Parse program title and find series details
		/// </summary>
		/// <param name="title">the title to parse</param>
		/// <returns>series detail as TvSeries object if it's series, null if title is not series</returns>
		private TvSeries ParseProgramTitle(ref string title)
		{
			TvSeries ret = null;

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
				ret = new TvSeries() {
					EpisodeNumber = episodeNum,
					SeasonNumber = seassonNum
				};

			return ret;
		}
		#endregion
	}
}
