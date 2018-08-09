using System;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using System.Linq;
using System.Collections.Generic;

namespace WebAppInsinghts
{
    /*
    * Custom TelemetryInitializer that overrides the default SDK
    * behavior of treating response codes >= 400 as failed requests
    *
    * aqui podemos dizer o que sera inicializado
    * 
    * tem que configura no ApplicationInsights.config ou no Application_Start ....
    * <Add Type="WebAppInsinghts.MyTelemetryInitializer, WebAppInsinghts"/>
    */
    public class MyTelemetryInitializer : ITelemetryInitializer
    {
        public void Initialize(ITelemetry telemetry)
        {
            var requestTelemetry = telemetry as RequestTelemetry;
            // Is this a TrackRequest() ?
            if (requestTelemetry == null) return;
            int code;
            bool parsed = Int32.TryParse(requestTelemetry.ResponseCode, out code);
            if (!parsed) return;
            if (code >= 400 && code < 500)
            {
                var tc = new TelimetriaCliente();

                Dictionary<string, string> a = new Dictionary<string, string>();
                a.Add(code.ToString(), requestTelemetry.Name);

                tc.LogAplicationInsightMsg(" HTTP " + code, SeverityLevel.Error);
                requestTelemetry.Success = true;

            }
        }
    }
}