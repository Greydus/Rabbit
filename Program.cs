using System;
using System.IO;
using System.Net;
using System.Text;

namespace Rabbit
{
	class Program
	{
		static void Main(String[] args)
		{
			try
			{
				var _headers = new StringBuilder();
				var _body = String.Empty;

				var _uri = new Uri(args[0]);
				var _wreq = WebRequest.CreateHttp(_uri);

				_wreq.UserAgent = "Rabbit/1.3.3.7";

				using (var _wres = _wreq.GetResponse())
				{
					using (var __headers = new StringWriter(_headers))
					{
						var ___headers = _wres.Headers;

						AsyncWriteHeadersToString(__headers, ___headers);
					}

					using (var __wres = _wres.GetResponseStream())
					{
						using (var __body = new StreamReader(__wres))
						{
							_body = __body.ReadToEnd();
						}
					}
				}

				Console.WriteLine(_headers);
				Console.WriteLine(_body);
			}
			catch (WebException e)
			{
				Console.WriteLine(e.Message);
			}
		}

		static async void AsyncWriteHeadersToString(StringWriter sw, WebHeaderCollection whc)
		{
			for (var i = 0; i < whc.Count; i++)
			{
				await sw.WriteLineAsync(whc.GetKey(i) + ": " + whc.Get(i));
			}
		}
	}
}