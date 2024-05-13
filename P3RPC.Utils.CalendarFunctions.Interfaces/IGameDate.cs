using P3RPC.Utils.CalendarFunctions.Interfaces.Structs;
using P3RPC.Utils.CalendarFunctions.Interfaces.Types.Native;

namespace P3RPC.Utils.CalendarFunctions.Interfaces; 
public interface IGameDate
{

    public unsafe delegate UGlobalWork* loadUGlobalWork();

    public unsafe bool IsDayAtLeast(uint daysElapsed);

    public unsafe bool IsDayExactly(uint daysElapsed);

    public unsafe bool IsDayAtMost(uint daysElapsed);

    public unsafe bool IsDayInRange(uint daysStart, uint daysEnd);

    public unsafe bool IsDateAtLeast(P3Date date);

    public unsafe bool IsDateExactly(P3Date date);

    public unsafe bool IsDateAtMost(P3Date date);

    public unsafe bool IsDateInRange(P3Date start, P3Date end);

}
