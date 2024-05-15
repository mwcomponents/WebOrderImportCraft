// Decompiled with JetBrains decompiler
// Type: SRISupport.logMessage
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

namespace SRISupport
{
  public class logMessage
  {
    public string msgStr;
    public uint errorFlag;
    public string errorMsg;

    public logMessage()
    {
      this.msgStr = "";
      this.errorFlag = 0U;
      this.errorMsg = "";
    }

    public logMessage(string str)
    {
      this.msgStr = str;
      this.errorFlag = 0U;
      this.errorMsg = "";
    }

    public static string SizemsgStr(long totalbytes)
    {
      double num1 = 1024.0;
      double num2 = 1024.0 * num1;
      double num3 = 1024.0 * num2;
      return (double) totalbytes >= num1 ? ((double) totalbytes >= num2 ? ((double) totalbytes >= num3 ? string.Format("{0:0.00} GigaBytes", (object) ((double) totalbytes / num3)) : string.Format("{0:0.00} MBytes", (object) ((double) totalbytes / num2))) : string.Format("{0:0.00} KBytes", (object) ((double) totalbytes / num1))) : string.Format("{0} Bytes", (object) totalbytes);
    }
  }
}
