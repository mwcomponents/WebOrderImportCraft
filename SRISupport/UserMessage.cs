// Decompiled with JetBrains decompiler
// Type: SRISupport.UserMessage
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

namespace SRISupport
{
  public class UserMessage
  {
    public virtual void MessageSend(string message, string type, UserMessage.msgLevel level)
    {
    }

    public static void sendMessage(string message, string type, UserMessage.msgLevel level)
    {
    }

    public enum msgLevel
    {
      Information,
      Warning,
      Error,
    }
  }
}
