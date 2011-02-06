using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XMLTVGrabber
{
	[Serializable]
	public class Channel
	{
		public enum Supplier_E { General, HOT, YES };

		public string Name { get; set; }
		public string Category { get; set; }
		public int Id { get; set; }
		public string Icon { get; set; }
		public List<TvProgram> Programs { get; set; }
		public Supplier_E Supplier { get; set; }

		public Channel()
		{
			Programs = new List<TvProgram>();
			Icon = null;
		}

		public override string ToString()
		{
			return Name.ToUpper();
		}
	}
}
