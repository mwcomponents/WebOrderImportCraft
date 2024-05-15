// Decompiled with JetBrains decompiler
// Type: CraftCMS.QLineUpdate
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

namespace CraftCMS
{
  public class QLineUpdate
  {
    public QItemPrice[] prices;

    public string sku { get; set; }

    public string serviceChargeId { get; set; }

    public string description { get; set; }

    public string leadTime { get; set; }

    public string specs { get; set; }

    public bool required { get; set; }
  }
}
