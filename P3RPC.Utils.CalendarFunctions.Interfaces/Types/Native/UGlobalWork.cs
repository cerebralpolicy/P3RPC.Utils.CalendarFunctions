using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace P3RPC.Utils.CalendarFunctions.Interfaces.Types.Native;

[StructLayout(LayoutKind.Explicit, Size = 0x68)]
public unsafe struct UAssetLoader //: public UObject
{
};


[StructLayout(LayoutKind.Explicit, Size = 0x250E0)]
public unsafe struct UGlobalWork //: public UGameInstance
{
    //[FieldOffset(0x0)] public UGameInstance Super;
    //[FieldOffset(0x1b0)] public FDatUnitWork PlayerCharacters[11]; // 10100_3 to 10109_3
    //[FieldOffset(0x1f6c)] public ushort ActiveCharacters[10]; // 10050_1
    //[FieldOffset(0x1f80)] public ItemBag Items; // 10051_1
    //[FieldOffset(0x3cf8)] public uint Money; // 10052_1
    //[FieldOffset(0x3cfc)] public byte Section0[384]; // 10000_1
    //[FieldOffset(0x3e7c)] public byte Section1[384]; // 10001_2
    //[FieldOffset(0x3ffc)] public byte Section2[640]; // 10002_1
    //[FieldOffset(0x427c)] public byte Section3[64]; // 10003_1
    //[FieldOffset(0x42bc)] public byte Section4[64]; // 10004_1
    //[FieldOffset(0x42fc)] public byte Section5[64]; // 10005_1
    //[FieldOffset(0x433c)] public int Counters[384]; // 10010_1
    //[FieldOffset(0x439c)] public FDatUnitPersona Personas[464]; // 10053_1
    //[FieldOffset(0xa03c)] public byte Analysis[828]; // 10054_1
    [FieldOffset(0xa378)] public Calendar Calendar; // 10030_1
    //[FieldOffset(0xa388)] public byte Shop[7424];
    //[FieldOffset(0xc168)] public CharacterName Name; // 10061_4
    //[FieldOffset(0xc2c4)] public Mail Mail; // 10081_2

    //[FieldOffset(0x24778)] public USequence* mSequenceInstance_;
    [FieldOffset(0x24780)] public UCalendar* mCalendarInstance_;
    [FieldOffset(0x24788)] public UCldCommonData* mCldCommonData_;
    //[FieldOffset(0x24790)] public UFileNameManager* mFileNameData_;
    //[FieldOffset(0x24798)] public UFldCommonData* mFldCommonData_;
    //[FieldOffset(0x247A0)] public UDatItem* mItemData_;
    //[FieldOffset(0x247A8)] public UTrophyManager* mTrophy_;
    //[FieldOffset(0x247B0)] public ULeaderBoardManager* mLeaderBoard_;
    //[FieldOffset(0x247B8)] public USignedInDialog* mSignedInDialog_;
    //[FieldOffset(0x247C0)] public UErrorDialog* mErrorDialog_;
    //[FieldOffset(0x247C8)] public UMessageDialog* mMessageDialog_;
    //[FieldOffset(0x247D0)] public UBustupController* pBustupController;
    //[FieldOffset(0x247D8)] public UCommunityWork* pCommunityWork;
    //[FieldOffset(0x247E0)] public UMsgWork* pMsgWork;
    //[FieldOffset(0x247E8)] public UEvtDataLoad* pEvtDataLoad;
    //[FieldOffset(0x247F0)] public UFrameBufferCapture* pFrameBufferCapture;
    //[FieldOffset(0x247F8)] public UPadRumble* pPadRumble;
    //[FieldOffset(0x248C8)] public UFldCharParamTable* mFldCharParamTable_;
    //[FieldOffset(0x248D0)] public UAppCharFootstepsTable* mFootstepsTable_;
    //[FieldOffset(0x248D8)] public UAppCharacterPoolManager* mCharacterPoolManager_;
    //[FieldOffset(0x248E0)] public UDatSystemText* mSystemTextTable;
    //[FieldOffset(0x248E8)] public UDatUIUseText* mUIUseTextTable;
    //[FieldOffset(0x248F0)] public UDatUICalendarText* mUICalendarTextTable;
    //[FieldOffset(0x248F8)] public UXrd777SaveManager* mSaveManager_;
    //[FieldOffset(0x24900)] public UAddContent* mAddContent_;
    //[FieldOffset(0x24F78)] public ULoading* pLoadingInst;
    //[FieldOffset(0x24F80)] public ACmpMainActor* mCmpMainActor_;
    //[FieldOffset(0x24F88)] public ABtlGuiResourcesBase* mBtlGuiResourcesActor_;
    //[FieldOffset(0x24F90)] public UBtlEncountWipeLoader* mBtlEncountWipeLoader_;
    //[FieldOffset(0x24F98)] public ABtlEncountWipeCore* mBtlEncountWipeCore_;
    //[FieldOffset(0x24FA0)] public AFldLevelPoolManager* mLevelPoolManager_;
    //[FieldOffset(0x24FA8)] public bool mPoolSetting_;
    //[FieldOffset(0x24FE8)] public FSaveGameHeadder mTempSaveHeader_;
    //[FieldOffset(0x250B8)] public bool bTempSaveHeaderUsed_;
    //[FieldOffset(0x250C0)] public UGlobalGameData* mGameDataProc_;
    //[FieldOffset(0x250C8)] public AAppActor* mSystemMonitor_;

    //public FDatUnitWork* GetUnit(int i) { fixed (UGlobalWork* self = &this) { return &((FDatUnitWork*)((nint)self + 0x1b0))[i]; } }


    public List<short> GetActiveCharacters()
    {
        List<short> ids = new();
        fixed (UGlobalWork* self = &this)
        {
            for (int i = 0; i < 10; i++)
            {
                var curr_mem = ((short*)((nint)self + 0x1f6c))[i];
                if (curr_mem == 0) break;
                ids.Add(curr_mem);
            }
        }
        return ids;
    }

    public static explicit operator UGlobalWork(UObject v)
    {
        return (UGlobalWork)v;
    }

    public static explicit operator UGlobalWork(Unreal.ObjectsEmitter.Interfaces.Types.UnrealObject v)
    {
        return (UGlobalWork)v;
    }

    public static explicit operator UGlobalWork(UGlobalWork* v)
    {
        return (UGlobalWork)v;
    }

    public bool GetBitflag(uint id)
    {
        uint section = id >> 0x1c;
        uint flag_int = (id >> 5 & 0x7fffff);
        uint flag_bit = (uint)(1 << ((int)id & 0x1f));
        fixed (UGlobalWork* self = &this)
        {
            switch (section)
            {
                case 0: return ((int*)((nint)self + 0x3cfc))[flag_int] % flag_bit == 1 ? true : false;
                case 1: return ((int*)((nint)self + 0x3e7c))[flag_int] % flag_bit == 1 ? true : false;
                case 2: return ((int*)((nint)self + 0x3ffc))[flag_int] % flag_bit == 1 ? true : false;
                case 3: return ((int*)((nint)self + 0x427c))[flag_int] % flag_bit == 1 ? true : false;
                case 4: return ((int*)((nint)self + 0x42bc))[flag_int] % flag_bit == 1 ? true : false;
                case 5: return ((int*)((nint)self + 0x42fc))[flag_int] % flag_bit == 1 ? true : false;
                default: return false;
            }
        }
    }
}
[StructLayout(LayoutKind.Explicit, Size = 0xE8)]
public unsafe struct UUIResources // : UGameInstanceSubsystem
{
    //[FieldOffset(0x0000)] public UGameInstanceSubsystem baseObj;
    [FieldOffset(0x30)] public byte bIsReady;
    [FieldOffset(0x0038)] public UAssetLoader* Loader_;
    [FieldOffset(0x0040)] public TArray<nint> Assets_;
    [FieldOffset(0x00D0)] public UDataTable* HandwritingLayoutData_;
    //[FieldOffset(0x00D8)] public UFontStyleAsset* FontStyleAsset_;
    //[FieldOffset(0x00E0)] public UFont* SystemFont_;

    public UObject* GetAssetEntry(byte index)
    {
        UObject* asset = null;
        if ((bIsReady & 1) != 0 && index < Assets_.arr_num)
            asset = (UObject*)Assets_.allocator_instance[index];
        return asset;
    }
}