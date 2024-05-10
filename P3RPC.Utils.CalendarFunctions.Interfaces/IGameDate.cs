using P3RPC.Utils.CalendarFunctions.Interfaces.Hooks;
using P3RPC.Utils.CalendarFunctions.Interfaces.Structs;
using P3RPC.Utils.CalendarFunctions.Interfaces.Types;
using P3RPC.Utils.CalendarFunctions.Interfaces.Types.Native;
using Project.Utils;
using System.ComponentModel;

namespace P3RPC.Utils.CalendarFunctions.Interfaces; 
public interface IGameDate
{

    public unsafe delegate UGlobalWork* loadUGlobalWork();

    public unsafe string P3Weekday(bool invariant);
    
    public unsafe uint DaysOffset(P3Date date);

    public unsafe uint WeeksOffset(P3Date date);


    public unsafe bool IsDateExactly(P3Date date);

    public unsafe bool IsDateAtLeast(P3Date date);

    public unsafe bool IsDateAtMost(P3Date date);

    public unsafe bool IsDateInRange(P3Date start, P3Date end);
}
