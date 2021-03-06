using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using NServiceBus;
using NServiceBus.Logging;

#pragma warning disable 618
[EndpointName("Samples.Logging.HostCustom")]
#region Config

public class EndpointConfig :
    IConfigureThisEndpoint
{
    public EndpointConfig()
    {
        var layout = new PatternLayout
        {
            ConversionPattern = "%d %-5p %c - %m%n"
        };
        layout.ActivateOptions();
        var appender = new ConsoleAppender
        {
            Layout = layout,
            Threshold = Level.Info
        };
        appender.ActivateOptions();

        BasicConfigurator.Configure(appender);

        LogManager.Use<Log4NetFactory>();
    }
#endregion
    public void Customize(EndpointConfiguration endpointConfiguration)
    {
        endpointConfiguration.UsePersistence<LearningPersistence>();
        endpointConfiguration.UseTransport<LearningTransport>();
    }
}
#pragma warning restore 618