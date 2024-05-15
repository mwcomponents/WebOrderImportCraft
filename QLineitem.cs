// Decompiled with JetBrains decompiler
// Type: CraftCMS.QLineitem
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

using System;

namespace CraftCMS
{
  public class QLineitem
  {
    public QItemPrice[] prices;

    public int lineItemId { get; set; }

    public int? purchasableId { get; set; }

    public string sku { get; set; }

    public string serviceChargeId { get; set; }

    public string description { get; set; }

    public QOptions options { get; set; }

    public int qty { get; set; }

    public string location { get; set; }

    public object dateRequested { get; set; }

    public Decimal? previousUnitPrice { get; set; }

    public bool required { get; set; }
  }
}
