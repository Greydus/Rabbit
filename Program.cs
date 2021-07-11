using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;

[assembly:AssemblyVersion("2.3.*")]

namespace Rabbit
{
	class Program
	{
		static void Main(String[] args)
		{
			var _assembly = typeof(Program).Assembly.GetName();

			var _headers = new List<String>();
			var _body = String.Empty;

			var _uri = new Uri(args[0]);
			var _client = WebRequest.CreateHttp(_uri);

			_client.UserAgent = _assembly.Name + "/" + _assembly.Version.ToString(2);

			using (var _response = _client.GetResponse())
			{
				var __headers = _response.Headers;

				for (var i = 0; i < __headers.Count; i++)
				{
					_headers.Add(__headers.GetKey(i) + ": " + __headers.Get(i));
				}

				using (var __body = _response.GetResponseStream())
				{
					using (var ___body = new StreamReader(__body))
					{
						_body = ___body.ReadToEnd();
					}
				}
			}

			Console.Write(String.Join("\n", _headers));
			Console.WriteLine();
			Console.WriteLine();
			Console.Write(_body);
		}
	}
}