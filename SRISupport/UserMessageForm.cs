// Decompiled with JetBrains decompiler
// Type: SRISupport.UserMessageForm
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

using System.Windows.Forms;

namespace SRISupport
{
  public class UserMessageForm : UserMessage
  {
    public override void MessageSend(string message, string type, UserMessage.msgLevel level)
    {
      MessageBoxIcon icon;
      switch (level)
      {
        case UserMessage.msgLevel.Information:
          icon = MessageBoxIcon.Asterisk;
          break;
        case UserMessage.msgLevel.Warning:
          icon = MessageBoxIcon.Exclamation;
          break;
        case UserMessage.msgLevel.Error:
          icon = MessageBoxIcon.Hand;
          break;
        default:
          icon = MessageBoxIcon.Hand;
          break;
      }
      int num = (int) MessageBox.Show(message, type, MessageBoxButtons.OK, icon);
    }

    public new static void sendMessage(string message, string type, UserMessage.msgLevel level)
    {
      MessageBoxIcon icon;
      switch (level)
      {
        case UserMessage.msgLevel.Information:
          icon = MessageBoxIcon.Asterisk;
          break;
        case UserMessage.msgLevel.Warning:
          icon = MessageBoxIcon.Exclamation;
          break;
        case UserMessage.msgLevel.Error:
          icon = MessageBoxIcon.Hand;
          break;
        default:
          icon = MessageBoxIcon.Hand;
          break;
      }
      int num = (int) MessageBox.Show(message, type, MessageBoxButtons.OK, icon);
    }
  }
}
