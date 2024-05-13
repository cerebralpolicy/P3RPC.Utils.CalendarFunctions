using P3RPC.Utils.CalendarFunctions.Interfaces;
using P3RPC.Utils.CalendarFunctions.Interfaces.Structs;
using P3RPC.Utils.CalendarFunctions.Reloaded.Components;
using P3RPC.Utils.CalendarFunctions.Reloaded.Configuration;
using P3RPC.Utils.CalendarFunctions.Reloaded.Template;
using Reloaded.Hooks.ReloadedII.Interfaces;
using Reloaded.Memory.Sigscan.Definitions;
using Reloaded.Memory.SigScan.ReloadedII.Interfaces;
using Reloaded.Mod.Interfaces;
using SharedScans.Interfaces;
using System.Diagnostics;
using System.Drawing;

namespace P3RPC.Utils.CalendarFunctions.Reloaded
{

    /// <summary>
    /// Your mod logic goes here.
    /// </summary>
    public class Mod : ModBase // <= Do not Remove.
    {

        public const string NAME = "Calendar Functions";
        public bool P3IsRunning;
        /// <summary>
        /// Provides access to the mod loader API.
        /// </summary>
        private readonly IModLoader _modLoader;

        /// <summary>
        /// Provides access to the Reloaded.Hooks API.
        /// </summary>
        /// <remarks>This is null if you remove dependency on Reloaded.SharedLib.Hooks in your mod.</remarks>
        private readonly IReloadedHooks? _hooks;

        /// <summary>
        /// Provides access to the Reloaded logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Entry point into the mod, instance that created this class.
        /// </summary>
        private readonly IMod _owner;

        /// <summary>
        /// Provides access to this mod's configuration.
        /// </summary>
        private Config _configuration;


        /// <summary>
        /// The configuration of the currently executing mod.
        /// </summary>
        private readonly IModConfig _modConfig;

        /// <summary>
        /// Game Date Service
        /// </summary>
        private readonly GameDate _gameDate;

        private long baseAddress;

        public unsafe Mod(ModContext context)
        {
            _modLoader = context.ModLoader;
            _hooks = context.Hooks;
            _logger = context.Logger;
            _owner = context.Owner;
            _configuration = context.Configuration;
            _modConfig = context.ModConfig;

            P3IsRunning = false;

            Log.Initialize(NAME, _logger, Color.PowderBlue);
            Log.LogLevel = _configuration.LogLevel;
            
            var mainModule = Process.GetCurrentProcess().MainModule;
            if (mainModule == null) throw new Exception($"[{_modConfig.ModName}] Could not get main module");
            baseAddress = mainModule.BaseAddress;
            _modLoader.GetController<IStartupScanner>().TryGetTarget(out var startupScanner);
            _modLoader.GetController<ISharedScans>().TryGetTarget(out var sharedScans);
            if (_hooks == null) throw new Exception($"[{_modConfig.ModName}] Could not get Reloaded hooks");
            if (sharedScans == null) throw new Exception($"[{_modConfig.ModName}] Could not get controller for Shared Scans");
            if (startupScanner == null) throw new Exception($"[{_modConfig.ModName}] Could not get controller for Startup Scanner");

             _gameDate = new(sharedScans, startupScanner);


            this.ApplyConfig();

            P3IsRunning = true;
            // For more information about this template, please see
            // https://reloaded-project.github.io/Reloaded-II/ModTemplate/

            // If you want to implement e.g. unload support in your mod,
            // and some other neat features, override the methods in ModBase.

            // TODO: Implement some mod logic
            
        }

        private void DateTests()
        {
            var exampleDate = new P3Date(2009, 10, 01);
            var exampleElapsed = exampleDate.DaysElapsed;
            var exampleDateOnly = exampleDate.Date;
            Log.Debug($"Example date: {exampleDateOnly}");
            Log.Debug($"Example elapsed: {exampleElapsed}");
        }

        private unsafe void BoolTests()
        {
            var gd = _gameDate;
            var exampleDate = new P3Date(2009, 10, 01);
            var exampleElapsed = exampleDate.DaysElapsed;
            var boolCheck = gd.IsDateAtLeast(exampleDate);
            Log.Debug($"{exampleDate} has passed: {boolCheck}");
            var boolCheck2 = gd.IsDayAtLeast(exampleElapsed);
            Log.Debug($"Day {exampleElapsed} has passed: {boolCheck2}");
        }

        private void ApplyConfig()
        {
            Log.LogLevel = this._configuration.LogLevel;
            DateTests();
            if (_gameDate != null && P3IsRunning) { BoolTests(); }
        }

        #region Standard Overrides
        public override void ConfigurationUpdated(Config configuration)
        {
            // Apply settings from configuration.
            // ... your code here.
            _configuration = configuration;
            _logger.WriteLine($"[{_modConfig.ModId}] Config Updated: Applying");
            this.ApplyConfig();
        }
        #endregion

        #region For Exports, Serialization etc.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Mod() { }
#pragma warning restore CS8618
        #endregion
    }
}