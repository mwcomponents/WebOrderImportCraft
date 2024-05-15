// Decompiled with JetBrains decompiler
// Type: CraftCMS2.Order
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

using System;

namespace CraftCMS2
{
  public class Order
  {
    public string number { get; set; }

    public string reference { get; set; }

    public object couponCode { get; set; }

    public string isCompleted { get; set; }

    public Dateordered dateOrdered { get; set; }

    public Datepaid datePaid { get; set; }

    public Dateauthorized dateAuthorized { get; set; }

    public string currency { get; set; }

    public string gatewayId { get; set; }

    public string lastIp { get; set; }

    public object message { get; set; }

    public object returnUrl { get; set; }

    public object cancelUrl { get; set; }

    public string orderStatusId { get; set; }

    public string orderLanguage { get; set; }

    public string orderSiteId { get; set; }

    public string origin { get; set; }

    public string billingAddressId { get; set; }

    public string shippingAddressId { get; set; }

    public object makePrimaryShippingAddress { get; set; }

    public object makePrimaryBillingAddress { get; set; }

    public object shippingSameAsBilling { get; set; }

    public object billingSameAsShipping { get; set; }

    public object estimatedBillingAddressId { get; set; }

    public object estimatedShippingAddressId { get; set; }

    public object estimatedBillingSameAsShipping { get; set; }

    public string shippingMethodHandle { get; set; }

    public string shippingMethodName { get; set; }

    public string customerId { get; set; }

    public object registerUserOnOrderComplete { get; set; }

    public object paymentSourceId { get; set; }

    public string storedTotalPrice { get; set; }

    public string storedTotalPaid { get; set; }

    public string storedItemTotal { get; set; }

    public string storedItemSubtotal { get; set; }

    public string storedTotalShippingCost { get; set; }

    public string storedTotalDiscount { get; set; }

    public string storedTotalTax { get; set; }

    public string storedTotalTaxIncluded { get; set; }

    public int id { get; set; }

    public object tempId { get; set; }

    public object draftId { get; set; }

    public object revisionId { get; set; }

    public string uid { get; set; }

    public int siteSettingsId { get; set; }

    public int? fieldLayoutId { get; set; }

    public int contentId { get; set; }

    public bool enabled { get; set; }

    public bool archived { get; set; }

    public int siteId { get; set; }

    public object title { get; set; }

    public object slug { get; set; }

    public object uri { get; set; }

    public Datecreated dateCreated { get; set; }

    public Dateupdated dateUpdated { get; set; }

    public object dateDeleted { get; set; }

    public bool trashed { get; set; }

    public object _ref { get; set; }

    public string status { get; set; }

    public object structureId { get; set; }

    public object url { get; set; }

    public string firstName { get; set; }

    public string lastName { get; set; }

    public string company { get; set; }

    public string phone { get; set; }

    public string additionalEmails { get; set; }

    public string shippingAccountNumber { get; set; }

    public string specialInstructions { get; set; }

    public Decimal adjustmentSubtotal { get; set; }

    public Decimal adjustmentsTotal { get; set; }

    public string paymentCurrency { get; set; }

    public Decimal paymentAmount { get; set; }

    public string email { get; set; }

    public bool isPaid { get; set; }

    public Decimal itemSubtotal { get; set; }

    public Decimal itemTotal { get; set; }

    public Lineitem[] lineItems { get; set; }

    public object[] orderAdjustments { get; set; }

    public Decimal outstandingBalance { get; set; }

    public string paidStatus { get; set; }

    public string recalculationMode { get; set; }

    public string shortNumber { get; set; }

    public Decimal totalPaid { get; set; }

    public Decimal total { get; set; }

    public Decimal totalPrice { get; set; }

    public Decimal totalQty { get; set; }

    public Decimal totalSaleAmount { get; set; }

    public Decimal totalTaxablePrice { get; set; }

    public Decimal totalWeight { get; set; }

    public string adjustmentSubtotalAsCurrency { get; set; }

    public string adjustmentsTotalAsCurrency { get; set; }

    public string itemSubtotalAsCurrency { get; set; }

    public string itemTotalAsCurrency { get; set; }

    public string outstandingBalanceAsCurrency { get; set; }

    public string paymentAmountAsCurrency { get; set; }

    public string totalPaidAsCurrency { get; set; }

    public string totalAsCurrency { get; set; }

    public string totalPriceAsCurrency { get; set; }

    public string totalSaleAmountAsCurrency { get; set; }

    public string totalTaxablePriceAsCurrency { get; set; }

    public string totalTaxAsCurrency { get; set; }

    public string totalTaxIncludedAsCurrency { get; set; }

    public string totalShippingCostAsCurrency { get; set; }

    public string totalDiscountAsCurrency { get; set; }

    public string storedTotalPriceAsCurrency { get; set; }

    public string storedTotalPaidAsCurrency { get; set; }

    public string storedItemTotalAsCurrency { get; set; }

    public string storedItemSubtotalAsCurrency { get; set; }

    public string storedTotalShippingCostAsCurrency { get; set; }

    public string storedTotalDiscountAsCurrency { get; set; }

    public string storedTotalTaxAsCurrency { get; set; }

    public string storedTotalTaxIncludedAsCurrency { get; set; }

    public string paidStatusHtml { get; set; }

    public string customerLinkHtml { get; set; }

    public string orderStatusHtml { get; set; }

    public Decimal totalTax { get; set; }

    public Decimal totalTaxIncluded { get; set; }

    public Decimal totalShippingCost { get; set; }

    public Decimal totalDiscount { get; set; }

    public object[] adjustments { get; set; }

    public Billingaddress billingAddress { get; set; }

    public Customer customer { get; set; }

    public Shippingaddress shippingAddress { get; set; }

    public Shippingmethod shippingMethod { get; set; }
  }
}
