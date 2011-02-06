using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Net;
using System.Runtime.Serialization;

namespace XMLTVGrabber
{
	public class Common
	{
		public static bool Get(string url, out string response, params string[] variables)
		{
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
			if (variables.Length > 0) {
				StringBuilder sb = new StringBuilder();
				foreach (string variable in variables)
					sb.Append("&" + variable);
				sb.Remove(0, 1);
				byte[] postData = Encoding.UTF8.GetBytes(sb.ToString());
				req.ContentType = "application/x-www-form-urlencoded";
				req.ContentLength = postData.Length;
				req.Method = "POST";
				req.GetRequestStream().Write(postData, 0, postData.Length);
			}
			HttpWebResponse res = (HttpWebResponse)req.GetResponse();
			if (res.StatusCode == HttpStatusCode.OK) {
				StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.GetEncoding("windows-1255"));
				response = sr.ReadToEnd();
				if (response.Length > 0)
					return true;
				else
					return false;
			} else {
				response = "";
				return false;
			}
		}

		public static bool Serialize(object obj, string file)
		{
			string tmpFile = file + ".temp";
			if (File.Exists(tmpFile))
				File.Delete(tmpFile);
			Stream stream = null;
			bool ret = false;
			try {
				BinaryFormatter bf = new BinaryFormatter();
				stream = File.Open(tmpFile, FileMode.Create);
				bf.Serialize(stream, obj);
				stream.Close();
				if (File.Exists(file))
					File.Delete(file);
				File.Move(tmpFile, file);
				ret = true;
			} catch (Exception e) {
				// TODO: Show error
			} finally {
				if (stream != null)
					stream.Close();
				if (File.Exists(tmpFile))
					File.Delete(tmpFile);
			}
			return ret;
		}

		public static object Deserialize(string file)
		{
			object ret = null;
			bool delFile = false;
			Stream stream = null;
			try {
				BinaryFormatter bf = new BinaryFormatter();
				stream = File.Open(file, FileMode.Open);
				ret = bf.Deserialize(stream);
			} catch (SerializationException e) {
				// TODO: Show error
				delFile = true;
			} catch (Exception e) {
				// TODO: Show error
			} finally {
				if (stream != null)
					stream.Close();
				if (delFile)
					File.Delete(file);
			}
			return ret;
		}
	}
}
