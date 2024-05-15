// Decompiled with JetBrains decompiler
// Type: CraftCMS2.Lineitem
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

using Newtonsoft.Json.Linq;
using System;

namespace CraftCMS2
{
  public class Lineitem
  {
    public int id { get; set; }

    public Decimal weight { get; set; }

    public Decimal length { get; set; }

    public Decimal height { get; set; }

    public Decimal width { get; set; }

    public Decimal qty { get; set; }

    public string note { get; set; }

    public string privateNote { get; set; }

    public string purchasableId { get; set; }

    public int orderId { get; set; }

    public object lineItemStatusId { get; set; }

    public int taxCategoryId { get; set; }

    public int shippingCategoryId { get; set; }

    public DateTime dateCreated { get; set; }

    public DateTime dateUpdated { get; set; }

    public string uid { get; set; }

    public object[] adjustments { get; set; }

    public string description { get; set; }

    public JToken options { get; set; }

    public string optionsSignature { get; set; }

    public bool onSale { get; set; }

    public Decimal price { get; set; }

    public Decimal saleAmount { get; set; }

    public Decimal salePrice { get; set; }

    public string sku { get; set; }

    public Decimal total { get; set; }

    public string priceAsCurrency { get; set; }

    public string saleAmountAsCurrency { get; set; }

    public string salePriceAsCurrency { get; set; }

    public string subtotalAsCurrency { get; set; }

    public string totalAsCurrency { get; set; }

    public string discountAsCurrency { get; set; }

    public string shippingCostAsCurrency { get; set; }

    public string taxAsCurrency { get; set; }

    public string taxIncludedAsCurrency { get; set; }

    public string adjustmentsTotalAsCurrency { get; set; }

    public Decimal subtotal { get; set; }
  }
}
