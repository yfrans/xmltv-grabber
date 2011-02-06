using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XMLTVGrabber
{
	[Serializable]
	public class TvProgram
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime Start { get; set; }
		public DateTime End { get; set; }
		public List<string> Categories { get; set; }
		public SeriesDetail Series { get; set; }
		public int Length { get { return (this.End - this.Start).Minutes; } }

		public TvProgram()
		{
			Categories = new List<string>();
		}
	}

	[Serializable]
	public class SeriesDetail
	{
		public int SeassonNumber { get; set; }
		public int EpisodeNumber { get; set; }
		public string EpisodeName { get; set; }

		public string ToHumanReadableString()
		{
			StringBuilder sb = new StringBuilder();
			if (SeassonNumber > 0)
				sb.Append("עונה " + SeassonNumber);
			if (EpisodeNumber > 0) {
				if (sb.Length > 0)
					sb.Append(" - ");
				sb.Append("פרק " + EpisodeNumber);
			}
			return sb.ToString();
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(SeassonNumber > 0 ? SeassonNumber.ToString() : "");
			sb.Append(" . ");
			sb.Append(EpisodeNumber > 0 ? EpisodeNumber.ToString() : "");
			sb.Append(" . ");
			return sb.ToString();
		}
	}
}
