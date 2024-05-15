// Decompiled with JetBrains decompiler
// Type: CraftCMS.Sourcesnapshot
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

using System;

namespace CraftCMS
{
  public class Sourcesnapshot
  {
    public Provider provider { get; set; }

    public Shippingmethod shippingMethod { get; set; }

    public object options { get; set; }

    public string id { get; set; }

    public string name { get; set; }

    public string description { get; set; }

    public string shippingZoneId { get; set; }

    public string methodId { get; set; }

    public int priority { get; set; }

    public object enabled { get; set; }

    public string orderConditionFormula { get; set; }

    public Decimal minQty { get; set; }

    public Decimal maxQty { get; set; }

    public Decimal minTotal { get; set; }

    public Decimal maxTotal { get; set; }

    public string minMaxTotalType { get; set; }

    public Decimal minWeight { get; set; }

    public Decimal maxWeight { get; set; }

    public string baseRate { get; set; }

    public Decimal perItemRate { get; set; }

    public Decimal percentageRate { get; set; }

    public Decimal weightRate { get; set; }

    public Decimal minRate { get; set; }

    public Decimal maxRate { get; set; }

    public int isLite { get; set; }

    public string SelflockingDevice { get; set; }

    public Decimal unitPrice { get; set; }

    public string OptionalFinish { get; set; }
  }
}
