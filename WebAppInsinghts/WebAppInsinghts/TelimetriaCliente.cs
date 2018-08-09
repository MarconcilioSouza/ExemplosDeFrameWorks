using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Collections.Generic;
using System.Web.Configuration;

namespace WebAppInsinghts
{
    public class TelimetriaCliente
    {
        private TelemetryClient tc;

        public TelimetriaCliente()
        {
            tc = new TelemetryClient();
            tc.Context.InstrumentationKey = WebConfigurationManager.AppSettings["ApplicationInsight"];
            tc.Context.Properties.Add("Aplicação X", "Filtro Aplicação X");
        }
        public void LogAplicationInsightEvento(String evento)
        {
            tc.TrackEvent(evento);
        }

        public void LogAplicationInsightException(Exception ex, IDictionary<string, string> properties)
        {
            tc.TrackException(ex, properties);
        }

        public void LogAplicationInsightMsg(String msg, SeverityLevel level)
        {
            tc.TrackTrace(msg, level);
        }

        public void LogAplicationInsightMsg(String msg, SeverityLevel level, IDictionary<string, string> properties)
        {
            tc.TrackTrace(msg, level, properties);
        }

        public void LogAplicationInsightMsg(MetricTelemetry metrica)
        {
            tc.TrackMetric(metrica);
        }
    }
}