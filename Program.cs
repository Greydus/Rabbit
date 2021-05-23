using System;
using System.IO;
using System.Net;
using System.Text;

namespace Rabbit
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length > 0)
			{
				HttpWebRequest _hwreq = WebRequest.CreateHttp(args[0]);
				_hwreq.UserAgent = "Rabbit/1.3.3.7";
				if (args.Length == 2) _hwreq.Method = args[1];

				//WebResponse _wres = _hwreq.GetResponse();
				StringBuilder _headersTxt = new StringBuilder();
				String _bodyTxt = String.Empty;
				using (WebResponse _wres = _hwreq.GetResponse())
				{
					//WebHeaderCollection _whc = _webres.Headers;
					using (StringWriter _headers = new StringWriter(_headersTxt))
					{
						WebHeaderCollection _whc = _wres.Headers;
						//WriteHeadersToString(_headersTxt, _whc);
						for (int i = 0; i < _whc.Count; ++i)
						{
							_headers.WriteLine(_whc.GetKey(i) + ": " + _whc.Get(i));
							//Console.WriteLine(_whc.GetKey(i) + ": " + _whc.Get(i));
						}
					}
					using (Stream _wrs = _wres.GetResponseStream())
					{
						using (StreamReader _body = new StreamReader(_wrs))
						{
							_bodyTxt = _body.ReadToEnd();
						}
					}
				}
				Console.WriteLine(_headersTxt);
				Console.WriteLine(_bodyTxt);
			}
		}

		/*static async void WriteHeadersToString(StringBuilder sb, WebHeaderCollection whc)
		{
			using (StringWriter sw = new StringWriter(sb))
			{
				for (int i = 0; i < whc.Count; ++i)
				{
					await sw.WriteLineAsync(whc.GetKey(i) + ": " + whc.Get(i));
				}
			}
		}*/
	}
}