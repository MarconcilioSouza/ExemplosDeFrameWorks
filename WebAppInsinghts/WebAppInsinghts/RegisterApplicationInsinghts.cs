using Microsoft.ApplicationInsights.Extensibility;

namespace WebAppInsinghts
{
    public static class RegisterApplicationInsinghts
    {
        public static void RegistreAppInsinght()
        {

            ////https://github.com/MicrosoftDocs/azure-docs/blob/master/articles/application-insights/app-insights-api-filtering-sampling.md
            //// https://pt.stackoverflow.com/questions/216470/como-usar-application-insights-em-mais-de-uma-aplica%c3%a7%c3%a3o

            //// Set Instrumentation Key
            //var configuration = new TelemetryConfiguration();
            //configuration.InstrumentationKey = "11108eb8-fa03-4a6b-9e4c-4092b14f56eb";

            ////// Automatically collect dependency calls
            //var dependencies = new DependencyTrackingTelemetryModule();
            //dependencies.Initialize(configuration);

            //// Automatically correlate all telemetry data with request
            //configuration.TelemetryInitializers.Add(new
            //  OperationCorrelationTelemetryInitializer());

            // ...
            TelemetryConfiguration.Active.TelemetryInitializers.Add(new MyTelemetryInitializer());

            var builder = TelemetryConfiguration.Active.TelemetryProcessorChainBuilder;
            builder.Use((next) => new SuccessfulDependencyFilter(next));

            // If you have more processors:
            // builder.Use((next) => new MyTelemetryInitializer(next));

          
            
            builder.Build();
        }
    }
}