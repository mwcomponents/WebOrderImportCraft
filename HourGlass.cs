// Decompiled with JetBrains decompiler
// Type: WebOrderImportCraft.HourGlass
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WebOrderImportCraft
{
  public class HourGlass : IDisposable
  {
    private IntPtr handle = IntPtr.Zero;

    [DllImport("user32.dll")]
    private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    public HourGlass()
    {
      this.handle = HourGlass.GetForegroundWindow();
      this.Enabled = true;
    }

    public void Dispose() => this.Enabled = false;

    private bool Enabled
    {
      set
      {
        if (value == Application.UseWaitCursor)
          return;
        Application.UseWaitCursor = value;
        HourGlass.SendMessage(this.handle, 32, this.handle, (IntPtr) 1);
      }
    }
  }
}
