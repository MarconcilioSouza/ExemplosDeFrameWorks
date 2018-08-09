using System;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.WindowsServer.TelemetryChannel;
using System.Linq;

namespace WebAppInsinghts
{
    public class SuccessfulDependencyFilter : ITelemetryProcessor
    {
        private ITelemetryProcessor Next { get; set; }

        // You can pass values from .config
        public string MyParamFromConfigFile { get; set; }
        // Link processors to each other in a chain.
        public SuccessfulDependencyFilter(ITelemetryProcessor next)
        {
            this.Next = next;
        }

        public void Process(ITelemetry item)
        {
            // To filter out an item, just return
            if (!OKtoSend(item))
            { return; }

            // Modify the item if required
            ModifyItem(item);

            this.Next.Process(item);
        }

        // Example: replace with your own criteria.
        private bool OKtoSend(ITelemetry item)
        {
            var dependency = item as DependencyTelemetry;
            if (dependency == null) return true;

            return dependency.Success != true;
        }

        // Example: replace with your own modifiers.

        //https://docs.microsoft.com/pt-br/azure/application-insights/app-insights-api-filtering-sampling#a-namefiltering-itelemetryprocessorafiltragem-itelemetryprocessor
        private void ModifyItem(ITelemetry item)
        {
            if (!item.Context.Properties.Keys.Any(n => n.Equals("Aplicação Teste 1")))
            {
                item.Context.Properties.Add("Aplicação Teste 1", MyParamFromConfigFile);
            }
        }
    }
}