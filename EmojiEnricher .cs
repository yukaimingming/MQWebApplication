using Serilog.Core;
using Serilog.Events;

public class EmojiEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var emoji = logEvent.Level switch
        {
            LogEventLevel.Information => " 🚀 ",
            LogEventLevel.Warning => " ⚠️ ",
            LogEventLevel.Error => " ❌ ",
            LogEventLevel.Debug => " 🐛 ",
            LogEventLevel.Fatal => " 💥 ",
            _ => ""
        };
        logEvent.AddPropertyIfAbsent(
            propertyFactory.CreateProperty("Emoji", emoji)
        );
    }
}
