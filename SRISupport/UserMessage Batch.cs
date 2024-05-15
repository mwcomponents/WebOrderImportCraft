// Decompiled with JetBrains decompiler
// Type: SRISupport.UserMessageBatch
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

namespace SRISupport
{
  public class UserMessageBatch : UserMessage
  {
    public logger logFile;

    internal UserMessageBatch(logger lf) => this.logFile = lf;

    public override void MessageSend(string message, string type, UserMessage.msgLevel level)
    {
      if (this.logFile == null)
        return;
      switch (level)
      {
        case UserMessage.msgLevel.Information:
        case UserMessage.msgLevel.Warning:
          this.logFile.addMsg(message);
          break;
        case UserMessage.msgLevel.Error:
          this.logFile.addErrorMsg(message, "Error!");
          break;
      }
    }

    public new static void sendMessage(string message, string type, UserMessage.msgLevel level)
    {
    }
  }
}
