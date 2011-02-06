using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace XMLTVGrabber
{
	public class Logger
	{
		public enum MessageType { DEBUG, ERROR, WARNING, INFO };

		private string m_path;
		private object m_lock;

		public MessageType Level { get; set; }

		public delegate void OnLogEvtArgs(string msg, MessageType mt);
		public event OnLogEvtArgs OnLog;

		public Logger(string program, string version) : this(program, version, null) { }
		public Logger(string program, string version, string path)
		{
			m_lock = new object();
			m_path = null;
			if (path != null) {
				if (Directory.Exists(path))
					m_path = path;
			}

			if (m_path == null)
				m_path = Directory.GetCurrentDirectory();

			m_path += "\\" + program.Replace(' ', '.') + "_" + version.Replace(' ', '.');
			m_path += ".txt";

			Level = MessageType.DEBUG | MessageType.ERROR | MessageType.INFO | MessageType.WARNING;

			Log(MessageType.INFO, "---------------------------");
			Log(MessageType.INFO, DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"));
		}

		~Logger()
		{
			Log(MessageType.INFO, "Program closed");
		}

		private void Log(MessageType mt, string msg)
		{
			if (Level >= mt) {
				lock (m_lock) {
					StreamWriter sw = File.AppendText(m_path);
					sw.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " | " +
						Enum.GetName(typeof(MessageType), mt) + ": " +
						msg);
					sw.Close();
				}
			}
		}

		private void RaiseEvent(string msg, MessageType mt)
		{
			if (OnLog != null)
				OnLog(msg, mt);
		}

		public void LogInfo(string msg) { Log(MessageType.INFO, msg); RaiseEvent(msg, MessageType.INFO); }
		public void LogWarning(string msg) { Log(MessageType.WARNING, msg); RaiseEvent(msg, MessageType.WARNING); }
		public void LogError(string msg) { Log(MessageType.ERROR, msg); RaiseEvent(msg, MessageType.ERROR); }
		public void LogDebug(string msg) { Log(MessageType.DEBUG, msg); }
	}
}
