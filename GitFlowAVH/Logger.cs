using System;
using System.Collections.Generic;
using System.Reflection;
//using Microsoft.ApplicationInsights;
using GitFlowAVH.TeamExplorer;
using Microsoft.ApplicationInsights;

namespace GitFlowAVH
{
	public static class Logger
	{
		private static TelemetryClient TelemetryClient { get; set; }

		static Logger()
		{
			TelemetryClient = new TelemetryClient
			{
				InstrumentationKey = "4bd63010-cc6c-42dd-ad4f-a7db2b566714"
			};
			TelemetryClient.Context.GlobalProperties["VisualStudioVersion"] = VSVersion.FullVersion.ToString();
			TelemetryClient.Context.Component.Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
			TelemetryClient.Context.Session.Id = Guid.NewGuid().ToString();
			TelemetryClient.Context.User.Id = UserSettings.UserId;
		}

		public static void PageView(string page)
		{
			TelemetryClient.TrackPageView(page);
		}
		public static void Event(string eventName, IDictionary<string,string> properties = null )
		{
			if (properties == null)
				TelemetryClient.TrackEvent(eventName);
			else
			{
				TelemetryClient.TrackEvent(eventName, properties);
			}
		}

		public static void Metric(string name, double value)
		{
            TelemetryClient.TrackMetric(name, value);
		}

		public static void Exception(Exception ex)
		{
            TelemetryClient.TrackException(ex);
		}
	}
}
