// Decompiled with JetBrains decompiler
// Type: MsgBox.MBox
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

using SRISupport;
using System.Windows.Forms;

namespace MsgBox
{
  internal static class MBox
  {
    public static bool logIt;
    public static UserMessageBatch UserMsg;

    public static DialogResult Show(string message)
    {
      if (!MBox.logIt)
        return MessageBox.Show(message);
      MBox.UserMsg.MessageSend(message, "Program", UserMessage.msgLevel.Information);
      return DialogResult.OK;
    }

    public static DialogResult Show(string text, string caption)
    {
      if (!MBox.logIt)
        return MessageBox.Show(text, caption);
      MBox.UserMsg.MessageSend(caption + ": " + text, "Program", UserMessage.msgLevel.Information);
      return DialogResult.OK;
    }

    public static DialogResult Show(string text, string caption, MessageBoxButtons buttons)
    {
      if (!MBox.logIt)
        return MessageBox.Show(text, caption, buttons);
      MBox.UserMsg.MessageSend(caption + ": " + text, "Program", UserMessage.msgLevel.Information);
      return buttons == MessageBoxButtons.YesNo ? DialogResult.Yes : DialogResult.OK;
    }

    public static DialogResult Show(
      string text,
      string caption,
      MessageBoxButtons buttons,
      MessageBoxIcon icon)
    {
      if (!MBox.logIt)
        return MessageBox.Show(text, caption, buttons, icon);
      MBox.UserMsg.MessageSend(caption + ": " + text, "Program", UserMessage.msgLevel.Information);
      return buttons == MessageBoxButtons.YesNo ? DialogResult.Yes : DialogResult.OK;
    }

    public static DialogResult Show(
      string text,
      string caption,
      MessageBoxButtons buttons,
      MessageBoxIcon icon,
      MessageBoxDefaultButton defaultButton)
    {
      if (!MBox.logIt)
        return MessageBox.Show(text, caption, buttons, icon, defaultButton);
      MBox.UserMsg.MessageSend(caption + ": " + text, "Program", UserMessage.msgLevel.Information);
      return buttons == MessageBoxButtons.YesNo ? DialogResult.Yes : DialogResult.OK;
    }
  }
}
