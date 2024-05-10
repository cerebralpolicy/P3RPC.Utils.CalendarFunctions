using System.Runtime.InteropServices;
using Unreal.ObjectsEmitter.Interfaces.Types;

namespace P3RPC.Utils.CalendarFunctions.Interfaces.Types;
/// <summary>
/// Core date architecture.
/// </summary>
[StructLayout(LayoutKind.Explicit, Size = 0x10)]
public unsafe struct Calendar
{
    /// <summary>
    /// Current day.
    /// </summary>
    [FieldOffset(0x0)] public uint DaysSinceApril1;
    /// <summary>
    /// Current time slot.
    /// </summary>
    [FieldOffset(0x4)] public ECldTimeZone TimeOfDay;
    /// <summary>
    /// Next day, can skip forward.
    /// </summary>
    [FieldOffset(0x8)] public uint NextTimeskipDay;
    /// <summary>
    /// Next time slot.
    /// </summary>
    [FieldOffset(0xc)] public ECldTimeZone NextTimeskipTime;

    public static explicit operator Calendar(UObject v) 
    {
        return (Calendar)v;
    }
}

/// <summary>
/// Main date system.
/// </summary>
[StructLayout(LayoutKind.Explicit, Size = 0xB8)]
public unsafe struct UCalendar
{
    /// <summary>
    /// UObject emitted
    /// </summary>
    [FieldOffset(0x0000)] public UObject baseObj;
    //[FieldOffset(0x0090)] public ACldBindingEventActor* mEventActor_;
    [FieldOffset(0x0098)] public uint mChangeFlag_;
    /// <summary>
    /// Controls the progression of the calendar.
    /// </summary>
    [FieldOffset(0x009C)] public ECldSceneChangeType mChangeType_;
    [FieldOffset(0x009D)] public ECldSceneChangeType mChangeSetted_;
    /// <summary>
    /// (current day)-1?
    /// </summary>
    [FieldOffset(0x00A0)] public int mChangePrevDay_;
    /// <summary>
    /// Previous time slot
    /// </summary>
    [FieldOffset(0x00A4)] public ECldTimeZone mChangePrevTimeZone_;
    /// <summary>
    /// (current day)+1?
    /// </summary>
    [FieldOffset(0x00A8)] public int mChangeNextDay_;
    /// <summary>
    /// Next time slot.
    /// </summary>
    [FieldOffset(0x00AC)] public ECldTimeZone mChangeNextTimeZone_;
    [FieldOffset(0x00B0)] public int cursorDay;
}
/// <summary>
/// The data for the UI calendar.
/// </summary>
[StructLayout(LayoutKind.Explicit, Size = 0x60)]
public unsafe struct UCldCommonData
{
    [FieldOffset(0x0000)] public UObject baseObj;
    //[FieldOffset(0x0030)] public ACldCmnDataActor* mActor_;
    [FieldOffset(0x0048)] public UCldDateDataAsset* mDatesData_;
    //[FieldOffset(0x0050)] public UClass* mBpClass_;
}

/// <summary>
/// Date datatable definition.
/// </summary>
[StructLayout(LayoutKind.Explicit, Size = 0x40)]
public unsafe struct UCldDateDataAsset
{
    //[FieldOffset(0x0000)] public UAppDataAsset baseObj;
    [FieldOffset(0x0030)] public TArray<FCldDateTableItem> Data;
}
/// <summary>
/// An entry in the date data table
/// </summary>
[StructLayout(LayoutKind.Explicit, Size = 0x6)]
public unsafe struct FCldDateTableItem
{
    /// <summary>
    /// The month an entry belongs to.
    /// </summary>
    [FieldOffset(0x0000)] public byte Month;
    /// <summary>
    /// The day of this entry defines.
    /// </summary>
    [FieldOffset(0x0001)] public byte Day;
    /// <summary>
    /// Current phase of the moon as of the entry.
    /// </summary>
    [FieldOffset(0x0002)] public byte MoonAge;
    /// <summary>
    /// True if the date falls on a Sunday.
    /// </summary>
    [FieldOffset(0x0003)] public bool IsHoliday;
    /// <summary>
    /// True for Stat Holidays, like Constution Day or Christmas.
    /// </summary>
    [FieldOffset(0x0004)] public bool IsPublicHoliday;
    [FieldOffset(0x0005)] public ECldDateColor NumColorType;
}

/// <summary>
/// Used to display the name of holidays?
/// </summary>
[StructLayout(LayoutKind.Explicit, Size = 0x14)]
public unsafe struct FCldDateMessage
{
    [FieldOffset(0x0000)] public int Key;
    [FieldOffset(0x0004)] public ushort TotalDay;
    [FieldOffset(0x0006)] public byte Month;
    [FieldOffset(0x0007)] public byte Day;
    //    [FieldOffset(0x0008)] public ECldDateMsgPeriod Period;
    //    [FieldOffset(0x000C)] public uint MsgLabel;
    //    [FieldOffset(0x0010)] public uint VisibleFlag;
}

[StructLayout(LayoutKind.Explicit, Size = 0x10)]
public unsafe struct FCldDateMessageMonth
{
    [FieldOffset(0x0000)] public TArray<FCldDateMessage> Data;
}

[StructLayout(LayoutKind.Explicit, Size = 0x40)]
public unsafe struct UCldDateMessageDataAsset
{
    //[FieldOffset(0x0000)] public UAppDataAsset baseObj;
    [FieldOffset(0x0030)] public TArray<FCldDateMessageMonth> Data;
}