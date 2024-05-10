
using P3RPC.Utils.CalendarFunctions.Interfaces;
using P3RPC.Utils.CalendarFunctions.Interfaces.Structs;
using P3RPC.Utils.CalendarFunctions.Reloaded.Components;
using P3RPC.Utils.CalendarFunctions.Reloaded.Configuration;
using P3RPC.Utils.CalendarFunctions.Reloaded.Examples;
using P3RPC.Utils.CalendarFunctions.Reloaded.Template;
using Reloaded.Hooks.ReloadedII.Interfaces;
using Reloaded.Memory.Sigscan.Definitions;
using Reloaded.Memory.SigScan.ReloadedII.Interfaces;
using Reloaded.Mod.Interfaces;
using SharedScans.Interfaces;
using System.Diagnostics;
using Unreal.ObjectsEmitter.Interfaces;
using static P3RPC.Utils.CalendarFunctions.Reloaded.Examples.Foobaw;

namespace P3RPC.Utils.CalendarFunctions.Reloaded
{

    /// <summary>
    /// Your mod logic goes here.
    /// </summary>
    public class Mod : ModBase // <= Do not Remove.
    {

        public const string NAME = "Calendar Functions";

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


        private readonly IUnreal _unreal;

        private readonly IUObjects _uObjects;

        private readonly ISharedScans? _sharedScans;

        private readonly IStartupScanner? _startupScanner;

        private readonly GameDate? _gameDate;

        private long baseAddress;

        static P3Date NFLSeasonStart = new P3Date(2009, 09, 10);
        static P3Date NFLSeasonEnd = new P3Date(2010, 01, 07);
        static P3Date NFLPlayoffStart = new P3Date(2010, 01, 09);
        static P3Date NFLPlayoffEnd = new P3Date(2010, 02, 07);
        static P3Date NewYearsDay = new(2010, 01, 01);
        static P3Date NewYearsEve = new(2009, 12, 31);

        public unsafe Mod(ModContext context)
        {
            _modLoader = context.ModLoader;
            _hooks = context.Hooks;
            _logger = context.Logger;
            _owner = context.Owner;
            _configuration = context.Configuration;
            _modConfig = context.ModConfig;

            _modLoader.GetController<IUObjects>().TryGetTarget(out var uObjects);
            if (uObjects == null) throw new Exception($"[{_modConfig.ModName}] Could not get Reloaded hooks");
            _uObjects = uObjects;
            _modLoader.GetController<IUnreal>().TryGetTarget(out var unreal);
            if (unreal == null) throw new Exception($"[{_modConfig.ModName}] Could not get Reloaded hooks");
            _unreal = unreal;

            var mainModule = Process.GetCurrentProcess().MainModule;
            if (mainModule == null) throw new Exception($"[{_modConfig.ModName}] Could not get main module");
            baseAddress = mainModule.BaseAddress;
            _modLoader.GetController<IStartupScanner>().TryGetTarget(out var startupScanner);
            _modLoader.GetController<ISharedScans>().TryGetTarget(out var sharedScans);
            if (_hooks == null) throw new Exception($"[{_modConfig.ModName}] Could not get Reloaded hooks");
            if (sharedScans == null) throw new Exception($"[{_modConfig.ModName}] Could not get controller for Shared Scans");
            _sharedScans = sharedScans;
            if (startupScanner == null) throw new Exception($"[{_modConfig.ModName}] Could not get controller for Startup Scanner");
            _startupScanner = startupScanner;

            //_methods = new(sharedScans);
            
            if (_sharedScans != null)
            {
                _gameDate = new(_sharedScans,_startupScanner,_configuration);
                if (_configuration.UseExamples && _gameDate != null)
                {
                    NFLSeasonProgression(_gameDate);
                    Yearis2009(_gameDate);
                    Yearis2010(_gameDate);
                }
            }



            // For more information about this template, please see
            // https://reloaded-project.github.io/Reloaded-II/ModTemplate/

            // If you want to implement e.g. unload support in your mod,
            // and some other neat features, override the methods in ModBase.

            // TODO: Implement some mod logic
            static void NFLSeasonProgression(GameDate gameDate)
            {
                if (gameDate.IsDateInRange(NFLSeasonStart, NFLSeasonEnd))
                {
                    var SeasonWeek = gameDate.WeeksOffset(NFLSeasonStart) + 1;
                    if (gameDate.P3WeekDay(true) == "Tuesday")
                    {
                        Log.Information($"The NFL has started Week {SeasonWeek}.");
                    }
                    else
                    {
                        Log.Information($"The NFL is currently on Week {SeasonWeek}.");
                    }
                }
                else if (gameDate.IsDateInRange(NFLPlayoffStart, NFLPlayoffEnd))
                {
                    Log.Information("The NFL Playoffs are being held.");
                }
            }
            static void Yearis2009(GameDate gameDate)
            {
                var DateCheck = gameDate.IsDateAtMost(NewYearsEve);
                if (DateCheck)
                {
                    Log.Information("It's still 2009.");
                }
            }
            static void Yearis2010(GameDate gameDate)
            {
                var DateCheck = gameDate.IsDateAtLeast(NewYearsDay);
                if (DateCheck)
                {
                    Log.Information("It's now 2010!");
                }
            }
        }

        #region Standard Overrides
        public override void ConfigurationUpdated(Config configuration)
        {
            // Apply settings from configuration.
            // ... your code here.
            _configuration = configuration;
            _logger.WriteLine($"[{_modConfig.ModId}] Config Updated: Applying");
        }
        #endregion

        #region For Exports, Serialization etc.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Mod() { }
#pragma warning restore CS8618
        #endregion
    }
}