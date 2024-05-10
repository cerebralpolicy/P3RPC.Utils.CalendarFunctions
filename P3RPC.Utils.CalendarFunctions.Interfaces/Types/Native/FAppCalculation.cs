
using System.Runtime.InteropServices;

namespace P3RPC.Utils.CalendarFunctions.Interfaces.Types.Native;


[StructLayout(LayoutKind.Explicit, Size = 0x18)]
public unsafe struct FAppCalculaterComponentWork
{
    [FieldOffset(0x0000)] public float Timer;
    [FieldOffset(0x0008)] public TArray<FAppCalculationItem> List;
}

[StructLayout(LayoutKind.Explicit, Size = 0x14)]
public unsafe struct FAppCalculationItem
{
    [FieldOffset(0x0000)] public float SrcValue;
    [FieldOffset(0x0004)] public float DstValue;
    [FieldOffset(0x0008)] public int Delay;
    [FieldOffset(0x000C)] public int DstFrame;
    [FieldOffset(0x0010)] public appCalculationType Type;

    public FAppCalculationItem(float SrcValue, float DstValue, int Delay, int DstFrame, appCalculationType Type)
    {
        this.SrcValue = SrcValue;
        this.DstValue = DstValue;
        this.Delay = Delay;
        this.DstFrame = DstFrame;
        this.Type = Type;
    }
}
