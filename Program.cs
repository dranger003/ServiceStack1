using System;

using Mono.Unix;
using Mono.Unix.Native;

namespace ServiceStack1
{
	public class Service1 : ServiceStack.Service
	{
		public object Any(object request)
		{
			return new object();
		}
	}

	public class AppHost : ServiceStack.AppSelfHostBase
	{
		public AppHost()
			: base("ServiceStack1", typeof(Service1).Assembly)
		{ }

		public override void Configure(Funq.Container container)
		{
			Plugins.Add(new ServiceStack.Razor.RazorFormat());
			Plugins.Add(new ServiceStack.ProtoBuf.ProtoBufFormat());
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			var baseUrl = "http://{0}:80/";
			var url = String.Format(baseUrl, "*");

			(new AppHost()).Init().Start(url);

			var signals = new UnixSignal[]
			{
				new UnixSignal(Signum.SIGINT),
				new UnixSignal(Signum.SIGTERM),
			};

			for (var quit = false; !quit; )
			{
				var signal = UnixSignal.WaitAny(signals);
				if (signal >= 0 && signal < signals.Length)
					if (signals[signal].IsSet)
						quit = true;
			}
		}
	}
}
