namespace P3RPC.Utils.CalendarFunctions.Interfaces.Types;

/// <summary>
/// Time slots in a given day? Social link/Event scheduling?
/// </summary>
public enum ECldDateMsgPeriod : byte
{
    /// <summary>
    /// Likely for one off events and in-engine cutscenes.
    /// </summary>
    Single = 0,
    /// <summary>
    /// First time slot, so Morning or AM.
    /// </summary>
    Start = 1,
    /// <summary>
    /// Second time slot, AfterSchool/PM?
    /// </summary>
    Mid = 2,
    End = 3,
};

/// <summary>
/// Time slots.
/// </summary>
public enum ECldTimeZone : byte
{
    None = 0,
    /// <summary>
    /// Dawn
    /// </summary>
    EarlyMorning = 1,
    /// <summary>
    /// Morning - School days?
    /// </summary>
    Morning = 2,
    /// <summary>
    /// Morning - holidays or lessons?
    /// </summary>
    AM = 3,
    /// <summary>
    /// Lunch
    /// </summary>
    Noon = 4,
    /// <summary>
    /// Afternoon - holidays?
    /// </summary>
    PM = 5,
    /// <summary>
    /// Afternoon - school days.
    /// </summary>
    AfterSchool = 6,
    /// <summary>
    /// Evening
    /// </summary>
    Night = 7,
    /// <summary>
    /// Dark Hour
    /// </summary>
    Shadow = 8,
    /// <summary>
    /// Post-Dark Hour, likely cutscenes only.
    /// </summary>
    Midnight = 9,
    TimeMax = 10,
    ECldTimeZone_MAX = 11,
};

public enum ECldWeek : byte
{
    Sunday = 0,
    Monday = 1,
    Tuesday = 2,
    Wednesday = 3,
    Thursday = 4,
    Friday = 5,
    Saturday = 6,
    WeekMax = 7,
    ECldWeek_MAX = 8,
};

public enum ECldMoonAge : byte
{
    /// <summary>
    /// Full Moon
    /// </summary>
    Moon01 = 0,
    Moon02 = 1,
    Moon03 = 2,
    Moon04 = 3,
    Moon05 = 4,
    Moon06 = 5,
    Moon07 = 6,
    Moon08 = 7,
    Moon09 = 8,
    Moon10 = 9,
    Moon11 = 10,
    Moon12 = 11,
    Moon13 = 12,
    Moon14 = 13,
    /// <summary>
    /// New Moon
    /// </summary>
    Moon15 = 14,
    Moon16 = 15,
    Moon17 = 16,
    Moon18 = 17,
    Moon19 = 18,
    Moon20 = 19,
    Moon21 = 20,
    Moon22 = 21,
    Moon23 = 22,
    Moon24 = 23,
    Moon25 = 24,
    Moon26 = 25,
    Moon27 = 26,
    Moon28 = 27,
    Moon29 = 28,
    Moon30 = 29,
    MoonMax = 30,
    ECldMoonAge_MAX = 31,
};

/// <summary>
/// Determines if the calendar updates at the end of a scene.
/// </summary>
public enum ECldSceneChangeType : byte
{
    /// <summary>
    /// The scene does not take up time.
    /// Potentially useful for a mod where activities don't take up time.
    /// </summary>
    None = 0,
    /// <summary>
    /// The time slot will roll over at the end of the scene.
    /// Standard activities I'm assuming.
    /// </summary>
    TimeChange = 1,
    /// <summary>
    /// The date will roll over with the end of the scene.
    /// Time slot resets to 0.
    /// </summary>
    DayChange = 2,
};

public enum ECldDateColor : byte
{
    /// <summary>
    /// Basic white font.
    /// </summary>
    Normal = 0,
    /// <summary>
    /// Red font on the Calendar screen.
    /// </summary>
    Red = 1,
};