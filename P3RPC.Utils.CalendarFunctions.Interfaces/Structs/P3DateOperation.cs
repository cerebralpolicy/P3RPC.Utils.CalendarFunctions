using P3RPC.Utils.CalendarFunctions.Interfaces.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace P3RPC.Utils.CalendarFunctions.Interfaces.Structs;

public struct P3DateOperation
{
    public string opID;
    public P3Date StartDate;
    public P3Date? EndDate;
    /// <summary>
    /// Calendar Functions will check to see if this has completed.
    /// </summary>
    public bool IsActive;
    /// <summary>
    /// No longer check for bool.
    /// </summary>
    public bool HasCompleted;
    public P3DateOperation(uint StartDay,DateOperation dateOperation,uint ThisDay)
    {
        string[] IDChunkArray = { };
        var Start = new P3Date(StartDay);
        StartDate = Start;
        var StartString = Start.Date.ToString("ddMM.yyyy");
        IDChunkArray.Append(StringToHex(StartString));
        var End = new P3Date(2010,03,31);
        var EndString = End.Date.ToString("ddMM.yyyy");
        IDChunkArray.Append(StringToHex(EndString));
        string OpString = dateOperation.ToString();
        IDChunkArray.Append(StringToHex(OpString));

        var _opID = String.Join("-",IDChunkArray);
        opID = _opID;

        if (dateOperation == DateOperation.AtLeast)
        {
            if (ThisDay > StartDay)
            {
                IsActive = false;
                HasCompleted = true;
            }
            else if (ThisDay == StartDay)
            {
                IsActive = true;
                HasCompleted = true;
            }
            else
            {
                IsActive = false;
                HasCompleted = false;
            }
        }
        // ENCODER
        string StringToHex(string input)
        {
            // Convert string to byte array
            byte[] byteArray = Encoding.UTF8.GetBytes(input);

            // Convert byte array to hexadecimal string
            StringBuilder hexBuilder = new StringBuilder();
            foreach (byte b in byteArray)
            {
                hexBuilder.Append(b.ToString("X2")); // X2 ensures two-digit representation
            }

            return hexBuilder.ToString();
        }
    }
}