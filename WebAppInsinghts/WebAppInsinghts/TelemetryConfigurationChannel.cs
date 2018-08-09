using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.WindowsServer.TelemetryChannel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppInsinghts
{
    public class TelemetryConfigurationChannel: ITelemetryChannel, ITelemetryModule
    {
        private ServerTelemetryChannel channel;       
        public TelemetryConfigurationChannel()
        {
            this.channel = new ServerTelemetryChannel();
        }

        // ITelemetryModule implementação configura o envio para o azure.
        // necessario a alteração da linha 
        // <TelemetryChannel Type="Microsoft.ApplicationInsights.WindowsServer.TelemetryChannel.ServerTelemetryChannel, Microsoft.AI.ServerTelemetryChannel"/>
        // no ApplicationInsights.config
        public void Initialize(TelemetryConfiguration configuration)
        {
            this.channel.Initialize(configuration);
        }

        // envia para o azure
        public void Send(ITelemetry item)
        {
            if (item is RequestTelemetry)
            {
                var requestTelemetry = item as RequestTelemetry;

                if (requestTelemetry != null && requestTelemetry.Success.Value
                    && MyFiltros(requestTelemetry))
                {
                    // do nothing
                }
                else
                {
                    this.channel.Send(item);
                }
            }
            else
            {
                this.channel.Send(item);
            }
        }

        private bool MyFiltros(RequestTelemetry request)
        {
            if (request.Url.AbsolutePath.StartsWith("/image.axd"))
            {
                return true;
            }

            return false;
        }

        public bool? DeveloperMode
        {
            get { return this.channel.DeveloperMode; }
            set { this.channel.DeveloperMode = value; }
        }

        public string EndpointAddress
        {
            get { return this.channel.EndpointAddress; }
            set { this.channel.EndpointAddress = value; }
        }

        public void Flush()
        {
            this.channel.Flush();
        }

        public void Dispose()
        {
            this.channel.Dispose();
        }

    }
}