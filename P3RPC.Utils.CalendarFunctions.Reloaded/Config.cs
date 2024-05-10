using P3RPC.Utils.CalendarFunctions.Interfaces.Types;
using P3RPC.Utils.CalendarFunctions.Reloaded.Template.Configuration;
using Reloaded.Mod.Interfaces.Structs;
using System.ComponentModel;

namespace P3RPC.Utils.CalendarFunctions.Reloaded.Configuration
{
    public class Config : Configurable<Config>
    {
        [DisplayName("Log Level")]
        [DefaultValue(LogLevel.Information)]
        public LogLevel LogLevel { get; set; } = LogLevel.Information;
        [DisplayName("Date Format")]
        [DefaultValue(DateFormatType.International)]
        public DateFormatType DateFormat { get; set; } = DateFormatType.International;
        [DisplayName("Run Examples")]
        [DefaultValue(true)]
        public bool UseExamples { get; set; }
    }

    /// <summary>
    /// Allows you to override certain aspects of the configuration creation process (e.g. create multiple configurations).
    /// Override elements in <see cref="ConfiguratorMixinBase"/> for finer control.
    /// </summary>
    public class ConfiguratorMixin : ConfiguratorMixinBase
    {
        // 
    }
}
