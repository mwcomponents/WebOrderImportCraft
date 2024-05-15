// Decompiled with JetBrains decompiler
// Type: WebOrderImportCraft.Properties.Resources
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace WebOrderImportCraft.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (WebOrderImportCraft.Properties.Resources.resourceMan == null)
          WebOrderImportCraft.Properties.Resources.resourceMan = new ResourceManager("WebOrderImportCraft.Properties.Resources", typeof (WebOrderImportCraft.Properties.Resources).Assembly);
        return WebOrderImportCraft.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => WebOrderImportCraft.Properties.Resources.resourceCulture;
      set => WebOrderImportCraft.Properties.Resources.resourceCulture = value;
    }

    internal static Bitmap Network => (Bitmap) WebOrderImportCraft.Properties.Resources.ResourceManager.GetObject(nameof (Network), WebOrderImportCraft.Properties.Resources.resourceCulture);
  }
}
