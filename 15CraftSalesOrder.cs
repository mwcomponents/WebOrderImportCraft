// Decompiled with JetBrains decompiler
// Type: CraftCMS.Shippingmethod
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

namespace CraftCMS
{
  public class Shippingmethod
  {
    public Provider1 provider { get; set; }

    public string rate { get; set; }

    public Rateoptions rateOptions { get; set; }

    public string shippingMethodCategories { get; set; }

    public int? id { get; set; }

    public string name { get; set; }

    public string handle { get; set; }

    public bool enabled { get; set; }

    public bool isLite { get; set; }
  }
}
