using P3RPC.Utils.CalendarFunctions.Interfaces.Hooks;
using P3RPC.Utils.CalendarFunctions.Interfaces.Types;
using P3RPC.Utils.CalendarFunctions.Interfaces.Types.Native;
using Project.Utils;
using System.ComponentModel;

namespace P3RPC.Utils.CalendarFunctions.Interfaces; 
public unsafe interface IGameDate : INotifyPropertyChanged, IEquatable<IGameDate>
{

    public unsafe delegate UGlobalWork LoadGameInstance();

    Calendar inGameCalendar { get; set; }

    public void DaysSinceAprilUpdated(object? sender, PropertyChangedEventArgs e) 
    {
        LoadGameInstanceAsync();
        GameInstance = GameInstanceVal;
    }

    public UGlobalWork GameInstance { get; set; }

    public UGlobalWork GameInstanceVal { 
        get
        {
            return GameInstance;
        }
        set
        {
            if (value.Calendar.DaysSinceApril1 != GameInstance.Calendar.DaysSinceApril1)
            {
                var prop = nameof(GameInstance.Calendar.DaysSinceApril1);
                DaysSinceAprilUpdated(this, new PropertyChangedEventArgs(prop));
            }
        }
    }

    ValueTask<LoadGameInstance> LoadGameInstanceAsync() 
    {
        Delegate.CreateDelegate(typeof(UGlobalWork), target: GameInstance, "AssignDelegate");
        if (!inGameCalendar.Equals(GameInstance.Calendar))
        {
            inGameCalendar = GameInstance.Calendar;
        }
        return LoadGameInstanceAsync();
    }

    /// <summary>
    /// Gets in-game calendar.
    /// </summary>

}
