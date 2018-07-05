using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.AppConfiguration;
using Microsoft.Extensions.Options;
using Microsoft.ApplicationInsights;


namespace ApplicationCore.Logging
{
    public interface IApplicationInsightsLogger
    {
        void TrackException(Exception exception, Dictionary<string, string> properties);
    }

    public class ApplicationInsightsLogger : IApplicationInsightsLogger
    {
        private readonly TelemetryClient _telemetryClient;
        private readonly IOptions<ApplicationInsightSettings> _appsettings;

        public ApplicationInsightsLogger(IOptions<ApplicationInsightSettings> appSettings)
        {
            _telemetryClient = new TelemetryClient();
            _appsettings = appSettings;
            _telemetryClient.InstrumentationKey = appSettings.Value.InstrumentationKey;
        }

        public void TrackException(Exception exception, Dictionary<string, string> properties)
        {
            _telemetryClient.TrackException(exception, properties);
        }
    }
}

