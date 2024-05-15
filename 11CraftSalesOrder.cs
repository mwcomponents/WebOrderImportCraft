// Decompiled with JetBrains decompiler
// Type: CraftCMS.Lasttransaction
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

using System;

namespace CraftCMS
{
  public class Lasttransaction
  {
    public string id { get; set; }

    public string orderId { get; set; }

    public string parentId { get; set; }

    public string userId { get; set; }

    public string hash { get; set; }

    public string gatewayId { get; set; }

    public string currency { get; set; }

    public Decimal paymentAmount { get; set; }

    public string paymentCurrency { get; set; }

    public string paymentRate { get; set; }

    public string type { get; set; }

    public Decimal amount { get; set; }

    public string status { get; set; }

    public string reference { get; set; }

    public string code { get; set; }

    public string message { get; set; }

    public string note { get; set; }

    public DateTime dateCreated { get; set; }

    public DateTime dateUpdated { get; set; }

    public string amountAsCurrency { get; set; }

    public string paymentAmountAsCurrency { get; set; }

    public string refundableAmountAsCurrency { get; set; }
  }
}
