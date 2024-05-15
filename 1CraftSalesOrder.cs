// Decompiled with JetBrains decompiler
// Type: CraftCMS.Order
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

using System;

namespace CraftCMS
{
  public class Order
  {
    public int id { get; set; }

    public string type { get; set; }

    public int orderSiteId { get; set; }

    public string number { get; set; }

    public string reference { get; set; }

    public string visualId { get; set; }

    public string visualQuoteId { get; set; }

    public string quoteId { get; set; }

    public Dateordered dateOrdered { get; set; }

    public Datecreated dateCreated { get; set; }

    public string status { get; set; }

    public string couponCode { get; set; }

    public string email { get; set; }

    public Customer customer { get; set; }

    public string customerId { get; set; }

    public Shippingaddress shippingAddress { get; set; }

    public string shippingAccountNumber { get; set; }

    public string shippingAccountService { get; set; }

    public string shippingAccountMethod { get; set; }

    public string shippingMethodName { get; set; }

    public string deliveryInstructions { get; set; }

    public Billingaddress billingAddress { get; set; }

    public Decimal paymentAmount { get; set; }

    public string paymentMethod { get; set; }

    public string poNumber { get; set; }

    public Decimal totalSaleAmount { get; set; }

    public Decimal totalTax { get; set; }

    public Decimal totalShippingCost { get; set; }

    public Adjustment[] adjustments { get; set; }

    public Lineitem[] lineItems { get; set; }

    public Decimal total { get; set; }

    public Lasttransaction lastTransaction { get; set; }

    public string[] documents { get; set; }
  }
}
