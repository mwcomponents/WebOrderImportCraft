// Decompiled with JetBrains decompiler
// Type: WebOrderImportCraft.OrderImport
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

using CraftCMS;
using MsgBox;
using Synergy.BusinessObjects;
using Synergy.BusinessObjects.AllObjects;
using Synergy.BusinessObjects.VE;
using Synergy.DistributedERP;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Windows.Forms;
using WebOrderImportCraft.Properties;

namespace WebOrderImportCraft
{
  internal class OrderImport
  {
    private static Dictionary<string, object> CountryDict = new Dictionary<string, object>();
    private static Dictionary<string, object> StateDict = new Dictionary<string, object>();
    private static Dictionary<string, string> CustGroupDict = new Dictionary<string, string>();
    public ExternalReferenceService extRefSvc;
    public CustomerService custSvc;
    public NotationService notationSvc;
    public SalesOrderService orderSvc;
    public InventoryService inventorySvc;
    public OrderSelectForm orderSelectForm;
    public Order webSO = (Order) null;
    public string orderIncrement;
    public string VisualOrderID;
    public string VisualCustID;
    public string CouponID;
    public bool debug = false;
    private string _City;
    private string _State;
    private string _ZipCode;
    private string _Country;
    private string _DefaultSalesGroupID;
    private Synergy.DistributedERP.Customer _CustomerInfo;
    private SalesShippingInfo WebShippingInfo = (SalesShippingInfo) null;
    public ContactService contactSvc;
    private CraftUtil craftCmsUtil = (CraftUtil) null;

    internal OrderImport(CraftUtil craftUtil) => this.craftCmsUtil = craftUtil;

    internal string getTermsID(object webSO) => (string) null;

    internal bool Import()
    {
      bool flag1 = true;
      bool flag2 = false;
      bool flag3 = false;
      try
      {
        if (!string.IsNullOrEmpty(this.orderIncrement) || this.webSO != null)
        {
          if (this.webSO == null)
            this.webSO = this.craftCmsUtil.OrderInfo(this.orderIncrement);
          Shippingaddress shippingaddress = this.webSO.shippingAddress ?? new Shippingaddress();
          Adjustment adjustment1 = ((IEnumerable<Adjustment>) this.webSO.adjustments).Where<Adjustment>((System.Func<Adjustment, bool>) (x => x.type == "shipping")).FirstOrDefault<Adjustment>() ?? new Adjustment();
          Adjustment adjustment2 = ((IEnumerable<Adjustment>) this.webSO.adjustments).Where<Adjustment>((System.Func<Adjustment, bool>) (x => x.type == "tax")).FirstOrDefault<Adjustment>() ?? new Adjustment();
          Adjustment adjustment3 = ((IEnumerable<Adjustment>) this.webSO.adjustments).Where<Adjustment>((System.Func<Adjustment, bool>) (x => x.type == "small-order")).FirstOrDefault<Adjustment>() ?? new Adjustment();
          Adjustment adjustment4 = ((IEnumerable<Adjustment>) this.webSO.adjustments).Where<Adjustment>((System.Func<Adjustment, bool>) (x => x.type == "discount")).FirstOrDefault<Adjustment>() ?? new Adjustment();
          this.WebShippingInfo = (SalesShippingInfo) new CenturyShippingInfo(this.webSO);
          Shippingaddress shippingAddress = this.webSO?.shippingAddress;
          if (shippingAddress != null)
          {
            this._City = shippingAddress.city;
            this._State = !string.IsNullOrWhiteSpace(shippingAddress.abbreviationText) ? shippingAddress.abbreviationText : shippingAddress.stateText;
            this._ZipCode = shippingAddress.zipCode;
            this._Country = shippingAddress.countryIso;
            if (string.IsNullOrWhiteSpace(this._City) || string.IsNullOrWhiteSpace(this._State) || string.IsNullOrWhiteSpace(this._ZipCode) || string.IsNullOrWhiteSpace(this._Country))
            {
              if (MBox.Show(this.webSO.reference + " Some shipping info not supplied: \r\n\r\n City:\t\t" + this._City + "\r\n State/Provence:\t" + this._State + "\r\n ZipCode:\t\t" + this._ZipCode + "\r\n Country:\t\t" + this._Country, "Shipping Info Warning: " + this.webSO.reference, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
                return false;
            }
          }
          string str1 = "No line Items";
          List<SiteCustCont> source = new List<SiteCustCont>();
          if (this.webSO.lineItems != null)
          {
            str1 = string.Format("{0} Item(s).", (object) this.webSO.lineItems.GetLength(0).ToString()) + "\r\n itemId : desc : price : sku : qty : brand";
            foreach (Lineitem lineItem in this.webSO.lineItems)
            {
              Lineitem sol = lineItem;
              str1 = str1 + "\r\n" + sol.purchasableId + " :  : " + sol.price.ToString() + " : " + sol.sku + " : " + sol.qty.ToString() + " : " + sol.location;
              if (string.IsNullOrWhiteSpace(sol.location))
                throw new Exception("Line with part# " + sol.sku + " does not have a location specified!");
              if (source.Where<SiteCustCont>((System.Func<SiteCustCont, bool>) (x => x.SiteId == sol.location)).FirstOrDefault<SiteCustCont>() == null)
                source.Add(new SiteCustCont()
                {
                  SiteId = sol.location
                });
              if (sol.options?.order_type == "RE")
                flag2 = true;
              if (sol.options?.order_type == "QE" || sol.options?.order_type == "WRFQ")
                flag3 = true;
            }
          }
          foreach (SiteCustCont siteCustCont in source)
          {
            siteCustCont.CustomerID = "cust1";
            siteCustCont.ContactID = "cont2";
          }
          this.VisualCustID = this.GetCustomer(this.webSO, Alias.Get(source.FirstOrDefault<SiteCustCont>().SiteId));
          if (this.VisualCustID != null)
          {
            string str2 = string.Empty;
            ExternalReferenceService referenceService = this.orderSelectForm.extRefSvc(source.FirstOrDefault<SiteCustCont>().SiteId);
            int id = this.webSO.id;
            string ExternalID = id.ToString();
            List<ExternalReference.Reference> referenceList = referenceService.ExternalReferenceLookup("false", "WebOrder", ExternalID, (string) null, (string) null, (string) null, (string) null);
            if (referenceList.Count == 0)
            {
              if (this.webSO.lineItems != null && (!Settings.Default.VerboseDialogs || MBox.Show(str1 + "\r\nCreate Order?", "Create Order", MessageBoxButtons.YesNo) == DialogResult.Yes))
              {
                bool flag4 = true;
                CustomerOrderData data1 = new CustomerOrderData();
                data1.Orders = new List<CustomerOrderHeader>();
                foreach (SiteCustCont siteCustCont in source)
                {
                  DatabaseConnection databaseConnection = this.orderSelectForm.Database(Alias.Get(siteCustCont.SiteId));
                  if (str2 != string.Empty && str2 != databaseConnection.UserCredentials.Database)
                    this.VisualCustID = this.GetCustomer(this.webSO, Alias.Get(siteCustCont.SiteId));
                  str2 = databaseConnection.UserCredentials.Database;
                  int num1;
                  if (this.VisualCustID == null)
                    num1 = MBox.Show("Create New Customer for " + siteCustCont.SiteId + " in DB " + str2 + "?\r\n\r\n" + str1, "Create New Customer", MessageBoxButtons.YesNo) == DialogResult.Yes ? 1 : 0;
                  else
                    num1 = 0;
                  if (num1 != 0)
                    this.VisualCustID = this.GetCustomer(this.webSO, Alias.Get(siteCustCont.SiteId), true);
                  ExternalReference.Reference reference1 = new ExternalReference.Reference();
                  reference1.ExternalType = "WebOrder";
                  ref ExternalReference.Reference local1 = ref reference1;
                  id = this.webSO.id;
                  string str3 = id.ToString();
                  local1.ExternalID = str3;
                  reference1.ExternalLineNo = "0";
                  CustomerOrderHeader VMorder = new CustomerOrderHeader();
                  VMorder.ReferenceList.Add(reference1);
                  VMorder.SiteID = Alias.Get(siteCustCont.SiteId);
                  VMorder.CustomerID = this.VisualCustID;
                  if (!string.IsNullOrEmpty(this.webSO.poNumber))
                    VMorder.CustomerPurchaseOrderID = this.webSO.poNumber;
                  bool flag5 = false;
                  int num2;
                  if (!string.IsNullOrEmpty(this.webSO.customer.userId))
                  {
                    Lasttransaction lastTransaction = this.webSO.lastTransaction;
                    if ((lastTransaction != null ? (lastTransaction.amount == 0.00M ? 1 : 0) : 0) != 0 && this.webSO.paymentMethod != "creditCard")
                    {
                      num2 = string.IsNullOrWhiteSpace(this.webSO.lastTransaction?.hash) ? 1 : 0;
                      goto label_44;
                    }
                  }
                  num2 = 0;
label_44:
                  if (num2 != 0)
                    flag5 = this.webSO.customer?.user?.termsCustomer.GetValueOrDefault();
                  if (this.webSO.paymentMethod != "invoice" && !flag5)
                    VMorder.TermsID = !string.IsNullOrEmpty(this.WebShippingInfo.GetShippingInfo("account")) ? Settings.Default.TermsCollectID : Settings.Default.TermsID;
                  VMorder.SalesTaxID = string.Empty;
                  if (flag3)
                  {
                    VMorder.Status = "H";
                  }
                  else
                  {
                    switch (OrderStatusDefault.Get(Alias.Get(siteCustCont.SiteId))?.ToUpper())
                    {
                      case "C":
                      case "CLOSED":
                        VMorder.Status = "C";
                        break;
                      case "CANCELLED":
                      case "CANCELLED/VOID":
                      case "VOID":
                      case "X":
                        VMorder.Status = "X";
                        break;
                      case "F":
                      case "FIRMED":
                        VMorder.Status = "F";
                        break;
                      case "H":
                      case "HOLD":
                      case "ON HOLD":
                      case "ON-HOLD":
                      case "ONHOLD":
                        VMorder.Status = "H";
                        break;
                      case "R":
                      case "RELEASED":
                        VMorder.Status = "R";
                        break;
                      default:
                        VMorder.Status = "H";
                        break;
                    }
                  }
                  VMorder.DesiredShipDate = new DateTime?(DateTime.Today);
                  VMorder.FOB = Settings.Default.FOB;
                  VMorder.ShipVIA = this.WebShippingInfo.GetShippingInfo("shipvia");
                  VMorder.CarrierID = this.WebShippingInfo.GetShippingInfo("carrier");
                  VMorder.ContactID = this.GetContact(this.webSO, this.VisualCustID, VMorder.SiteID);
                  VMorder.ContactFirstName = this.webSO.customer?.user?.firstName;
                  VMorder.ContactLastName = this.webSO.customer?.user?.lastName;
                  VMorder.ContactEmail = this.webSO.email;
                  VMorder.ContactPhoneNumber = this.webSO.billingAddress?.phone;
                  VMorder.ContactFaxNumber = this.webSO.billingAddress?.phone;
                  VMorder.UserDefined1 = this.webSO.lastTransaction?.reference;
                  VMorder.UserDefined2 = this.webSO.reference;
                  string str4 = "E";
                  if (flag2 | flag3)
                    str4 = "EQ";
                  if (!string.IsNullOrWhiteSpace(this.webSO.type))
                    str4 = OrderPrefixMap.Get(this.webSO.type) ?? "E";
                  id = this.webSO.id;
                  if (id.ToString().StartsWith(str4))
                  {
                    CustomerOrderHeader customerOrderHeader = VMorder;
                    id = this.webSO.id;
                    string str5 = id.ToString() + "-" + Alias.Get(siteCustCont.SiteId, true);
                    customerOrderHeader.CustomerOrderID = str5;
                  }
                  else
                  {
                    CustomerOrderHeader customerOrderHeader = VMorder;
                    string str6 = str4;
                    id = this.webSO.id;
                    string str7 = id.ToString();
                    string str8 = Alias.Get(siteCustCont.SiteId, true);
                    string str9 = str6 + str7 + "-" + str8;
                    customerOrderHeader.CustomerOrderID = str9;
                  }
                  string customerOrderId = VMorder.CustomerOrderID;
                  CustomerOrderHeader customerOrderHeader1 = new CustomerOrderHeader();
                  customerOrderHeader1.CustomerOrderID = VMorder.CustomerOrderID;
                  customerOrderHeader1.SiteID = VMorder.SiteID;
                  int num3 = 0;
                  CustomerOrderData data2 = new CustomerOrderData()
                  {
                    Orders = new List<CustomerOrderHeader>()
                  };
                  data2.Orders.Add(customerOrderHeader1);
                  CustomerOrderData customerOrderData;
                  do
                  {
                    customerOrderData = this.orderSelectForm.orderSvc(VMorder.SiteID).SearchCustomerOrder(data2);
                    VMorder.CustomerOrderID = data2.Orders[0].CustomerOrderID;
                    ++num3;
                    data2.Orders[0].CustomerOrderID = customerOrderId + "-" + num3.ToString();
                  }
                  while (customerOrderData.Orders.Count > 0);
                  if (shippingAddress != null)
                    this.GetAddress(this.webSO, VMorder);
                  List<UDFValue> udfValueList = new List<UDFValue>();
                  udfValueList.Add(new UDFValue()
                  {
                    ID = "00029",
                    StringValue = "Ecommerce"
                  });
                  udfValueList.Add(new UDFValue()
                  {
                    ID = "00047",
                    DateValue = new DateTime?(DateTime.Today)
                  });
                  udfValueList.Add(new UDFValue()
                  {
                    ID = "00030",
                    StringValue = this.webSO.customer?.user?.firstName + " " + this.webSO.customer?.user?.lastName
                  });
                  udfValueList.Add(new UDFValue()
                  {
                    ID = "00129",
                    NumberValue = new Decimal?((Decimal) this.webSO.orderSiteId)
                  });
                  string shippingInfo = this.WebShippingInfo.GetShippingInfo("rush");
                  VMorder.UDFValues = udfValueList;
                  VMorder.Lines = new List<CustomerOrderLine>();
                  string str10 = string.Empty;
                  Decimal num4 = 0M;
                  string empty = string.Empty;
                  foreach (Lineitem lineItem in this.webSO.lineItems)
                  {
                    if (lineItem.location == siteCustCont.SiteId)
                    {
                      ExternalReference.Reference reference2 = new ExternalReference.Reference();
                      reference2.ExternalType = "WebOrderLine";
                      ref ExternalReference.Reference local2 = ref reference2;
                      id = this.webSO.id;
                      string str11 = id.ToString();
                      local2.ExternalID = str11;
                      ref ExternalReference.Reference local3 = ref reference2;
                      id = lineItem.id;
                      string str12 = id.ToString();
                      local3.ExternalLineNo = str12;
                      CustomerOrderLine customerOrderLine1 = new CustomerOrderLine();
                      customerOrderLine1.ReferenceList.Add(reference2);
                      customerOrderLine1.QTY = new Decimal?(Convert.ToDecimal(lineItem.qty));
                      if (!Settings.Default.PartSpecOnly)
                      {
                        string str13 = lineItem.sku.ToUpper();
                        if (VMorder.SiteID == "CSC")
                        {
                          int startIndex = str13.LastIndexOf('-');
                          if (startIndex > 0)
                          {
                            str10 = str13.Substring(startIndex).ToUpper();
                            switch (str10)
                            {
                              case "-P":
                                str13 = str13.Substring(0, str13.Length - 2);
                                customerOrderLine1.UserDefined1 = "SP";
                                break;
                              case "-Z":
                                str13 = str13.Substring(0, str13.Length - 2);
                                customerOrderLine1.UserDefined1 = "SP";
                                break;
                              case "-G":
                              case "-GI":
                                str13 = str13.Substring(0, str13.Length - str10.Length);
                                customerOrderLine1.UserDefined1 = "SP";
                                break;
                              case "-BO":
                                str13 = str13.Substring(0, str13.Length - str10.Length);
                                customerOrderLine1.UserDefined1 = "SP";
                                break;
                              default:
                                str10 = string.Empty;
                                break;
                            }
                          }
                        }
                        string partID = str13 + "-" + VMorder.SiteID;
                        if (this.orderSelectForm.inventorySvc(VMorder.SiteID).PartIDExists(partID))
                          customerOrderLine1.PartID = partID.ToUpper();
                        else if (this.orderSelectForm.inventorySvc(VMorder.SiteID).PartIDExists(str13))
                        {
                          customerOrderLine1.PartID = str13.ToUpper();
                        }
                        else
                        {
                          string str14 = string.Empty;
                          if (VMorder.SiteID == "RAF")
                            str14 = new RAFParts().CreatePart(databaseConnection, VMorder.SiteID, str13, this.orderSelectForm.inventorySvc(VMorder.SiteID));
                          if (string.IsNullOrEmpty(str14))
                          {
                            string[] strArray = new string[8]
                            {
                              "NA: ",
                              str13,
                              " : ",
                              partID,
                              " : ",
                              lineItem.sku,
                              " : ",
                              null
                            };
                            id = lineItem.id;
                            strArray[7] = id.ToString();
                            string str15 = string.Concat(strArray);
                            customerOrderLine1.LineDescription = str15.Substring(0, Math.Min(str15.Length, 40));
                            if (MBox.Show("Part not found: " + str15 + "\r\nCreate Order?", "Create Order", MessageBoxButtons.YesNo) == DialogResult.No)
                              flag4 = false;
                          }
                          else
                            customerOrderLine1.PartID = str14;
                        }
                      }
                      else
                      {
                        string sku = lineItem.sku;
                        id = lineItem.id;
                        string str16 = id.ToString();
                        string str17 = sku + " : " + str16;
                        customerOrderLine1.LineDescription = str17.Substring(0, Math.Min(str17.Length, 40));
                      }
                      DateTime dateTime = DateTime.Today;
                      if (DateTime.Now.TimeOfDay > Settings.Default.ShippingCutOffTime || dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday)
                        dateTime = dateTime.DayOfWeek != DayOfWeek.Friday ? (dateTime.DayOfWeek != DayOfWeek.Saturday ? dateTime.AddDays(1.0) : dateTime.AddDays(2.0)) : dateTime.AddDays(3.0);
                      if (!string.IsNullOrEmpty(str10))
                        dateTime = dateTime.DayOfWeek != DayOfWeek.Monday ? dateTime.AddDays(6.0) : dateTime.AddDays(4.0);
                      customerOrderLine1.UnitPrice = new Decimal?(Convert.ToDecimal(lineItem.price));
                      customerOrderLine1.DiscountPercent = new Decimal?(0M);
                      customerOrderLine1.DesiredShipDate = new DateTime?(dateTime);
                      if (!string.IsNullOrEmpty(shippingInfo))
                        customerOrderLine1.UserDefined3 = "RUSH";
                      if (lineItem.options?.order_type == "RE")
                        customerOrderLine1.UDFValues = new List<UDFValue>()
                        {
                          new UDFValue()
                          {
                            ID = "00076",
                            BooleanValue = new bool?(true)
                          }
                        };
                      if (lineItem.options?.order_type == "QE")
                        customerOrderLine1.UDFValues = new List<UDFValue>()
                        {
                          new UDFValue()
                          {
                            ID = "00122",
                            BooleanValue = new bool?(true)
                          }
                        };
                      if (lineItem.options?.order_type == "WRFQ")
                        customerOrderLine1.UDFValues = new List<UDFValue>()
                        {
                          new UDFValue()
                          {
                            ID = "00125",
                            BooleanValue = new bool?(true)
                          }
                        };
                      VMorder.Lines.Add(customerOrderLine1);
                      if (!empty.Contains(str10) && !string.IsNullOrEmpty(str10))
                      {
                        CustomerOrderLine customerOrderLine2 = new CustomerOrderLine();
                        customerOrderLine2.ReferenceList.Add(reference2);
                        customerOrderLine2.QTY = new Decimal?((Decimal) 1);
                        customerOrderLine2.UnitPrice = new Decimal?(0M);
                        foreach (Adjustment adjustment5 in this.webSO.adjustments)
                        {
                          int? lineItemId = adjustment5.lineItemId;
                          id = lineItem.id;
                          if (lineItemId.GetValueOrDefault() == id & lineItemId.HasValue)
                          {
                            customerOrderLine2.UnitPrice = new Decimal?(adjustment5.amount);
                            num4 += adjustment5.amount;
                            break;
                          }
                        }
                        customerOrderLine2.DesiredShipDate = new DateTime?(dateTime);
                        if (!string.IsNullOrEmpty(shippingInfo))
                          customerOrderLine2.UserDefined3 = "RUSH";
                        switch (str10)
                        {
                          case "-P":
                            customerOrderLine2.LineDescription = "Finish Service: Passivation";
                            customerOrderLine2.ServiceChargeID = Settings.Default.PassivationSvcChg;
                            break;
                          case "-Z":
                            customerOrderLine2.LineDescription = "Finish Service: Zinc";
                            customerOrderLine2.ServiceChargeID = Settings.Default.ZincPlatingSvcChg;
                            break;
                          case "-G":
                          case "-GI":
                            customerOrderLine2.LineDescription = "Finish Service: Gold Irridite";
                            customerOrderLine2.ServiceChargeID = Settings.Default.GoldIrridateSvcChg;
                            break;
                          case "-BO":
                            customerOrderLine2.LineDescription = "Finish Service: Black Oxide";
                            customerOrderLine2.ServiceChargeID = Settings.Default.BlackOxideSvcChg;
                            break;
                          default:
                            customerOrderLine2.LineDescription = "Finish service identifier not found. " + str10;
                            break;
                        }
                        VMorder.Lines.Add(customerOrderLine2);
                        empty += str10;
                      }
                    }
                  }
                  if (empty.Contains("-P"))
                    VMorder.OrderComments += "^Passivation Required^";
                  if (empty.Contains("-Z"))
                    VMorder.OrderComments += "^Zinc Plating Required^";
                  if (empty.Contains("-G") || empty.Contains("-GI"))
                    VMorder.OrderComments += "^Gold Iridite Required^";
                  VMorder.OrderComments += string.IsNullOrWhiteSpace(VMorder.OrderComments) ? "" : "\r\n";
                  VMorder.OrderComments += this.webSO.deliveryInstructions;
                  Decimal num5 = 1.0M;
                  if (source.Count > 1 && (Settings.Default.ProportionalFreight || Settings.Default.ProportionalTax))
                  {
                    Decimal num6 = 0.0M;
                    Decimal num7 = 0.0M;
                    foreach (CustomerOrderLine line in VMorder.Lines)
                    {
                      Decimal num8 = num6;
                      Decimal? nullable = line.UnitPrice;
                      Decimal num9 = nullable ?? 0.0M;
                      nullable = line.QTY;
                      Decimal num10 = nullable ?? 0.0M;
                      Decimal num11 = num9 * num10;
                      num6 = num8 + num11;
                    }
                    foreach (Lineitem lineItem in this.webSO.lineItems)
                      num7 += lineItem.total;
                    num5 = !(num7 != 0.0M) ? 1.0M / (Decimal) source.Count : num6 / num7;
                    if (num5 > 1.0M)
                      throw new Exception("Proprotion Calculation failure: " + VMorder.CustomerOrderID + "-" + this.webSO.id.ToString());
                  }
                  if ((data1.Orders.Count == 0 || Settings.Default.ProportionalFreight) && !string.IsNullOrEmpty(FreightSvcChg.Get(VMorder.SiteID)) && this.webSO.totalShippingCost != 0M)
                    VMorder.Lines.Add(new CustomerOrderLine()
                    {
                      QTY = new Decimal?((Decimal) 1),
                      ServiceChargeID = FreightSvcChg.Get(VMorder.SiteID),
                      SalesTaxID = Settings.Default.FreightTaxGroupID,
                      UnitPrice = !Settings.Default.ProportionalFreight ? new Decimal?(this.webSO.totalShippingCost) : new Decimal?(Math.Round(this.webSO.totalShippingCost * num5, 2, MidpointRounding.ToEven))
                    });
                  if ((data1.Orders.Count == 0 || Settings.Default.ProportionalTax) && !string.IsNullOrEmpty(TaxSvcChg.Get(VMorder.SiteID)) && this.webSO.totalTax != 0M)
                    VMorder.Lines.Add(new CustomerOrderLine()
                    {
                      QTY = new Decimal?((Decimal) 1),
                      ServiceChargeID = TaxSvcChg.Get(VMorder.SiteID),
                      UnitPrice = !Settings.Default.ProportionalTax ? new Decimal?(this.webSO.totalTax) : new Decimal?(Math.Round(this.webSO.totalTax * num5, 2, MidpointRounding.ToEven))
                    });
                  if (data1.Orders.Count == 0 || Settings.Default.ProportionalSmallOrderFee)
                  {
                    Decimal amount = adjustment3.amount;
                    if (!string.IsNullOrEmpty(SmallOrderSvcChg.Get(VMorder.SiteID)) && amount > 0.00M)
                      VMorder.Lines.Add(new CustomerOrderLine()
                      {
                        QTY = new Decimal?((Decimal) 1),
                        ServiceChargeID = SmallOrderSvcChg.Get(VMorder.SiteID),
                        UnitPrice = !Settings.Default.ProportionalSmallOrderFee ? new Decimal?(amount) : new Decimal?(Math.Round(amount * num5, 2, MidpointRounding.ToEven))
                      });
                  }
                  data1.Orders.Add(VMorder);
                  if (flag4)
                  {
                    data1.ReturnErrorInResponse = new bool?(true);
                    data1.UseIndependentTransactions = new bool?(false);
                    CustomerOrderDataResponse salesOrder = this.orderSelectForm.orderSvc(VMorder.SiteID).CreateSalesOrder(data1);
                    if (salesOrder.HasErrors)
                    {
                      string message = string.Empty;
                      foreach (CustomerOrderHeaderResponse order in salesOrder.Orders)
                        message = message + "\r\n" + order.CustomerOrderHeader.CustomerOrderID + "\r\n" + order.ErrorMessage + "\r\n";
                      int num12 = (int) MBox.Show(message);
                    }
                    else
                    {
                      foreach (CustomerOrderHeader order in data1.Orders)
                      {
                        if (!string.IsNullOrEmpty(this.webSO.paymentMethod) && this.webSO.paymentMethod != "invoice")
                        {
                          NotationService.NotationData data3 = new NotationService.NotationData();
                          data3.NotationType = "CO";
                          data3.OwnerID = order.CustomerOrderID;
                          data3.UserID = string.IsNullOrEmpty(this.orderSelectForm.notationSvc(VMorder.SiteID)._Header.UserName) ? "WebUser" : this.orderSelectForm.notationSvc(VMorder.SiteID)._Header.UserName;
                          NotationService.NotationData notationData1 = data3;
                          string[] strArray = new string[6];
                          id = this.webSO.id;
                          strArray[0] = id.ToString();
                          strArray[1] = " (";
                          strArray[2] = this.webSO.reference;
                          strArray[3] = " : ";
                          strArray[4] = this.webSO.number;
                          strArray[5] = ")\n";
                          string str18 = string.Concat(strArray);
                          notationData1.Note = str18;
                          NotationService.NotationData notationData2 = data3;
                          notationData2.Note = notationData2.Note + "\npaymentMethod: " + this.webSO.paymentMethod;
                          NotationService.NotationData notationData3 = data3;
                          notationData3.Note = notationData3.Note + "\nTransactionID: " + this.webSO.lastTransaction?.id;
                          NotationService.NotationData notationData4 = data3;
                          notationData4.Note = notationData4.Note + "\norderId: " + this.webSO.lastTransaction?.orderId;
                          NotationService.NotationData notationData5 = data3;
                          notationData5.Note = notationData5.Note + "\nparentId: " + this.webSO.lastTransaction?.parentId;
                          NotationService.NotationData notationData6 = data3;
                          notationData6.Note = notationData6.Note + "\nuserId: " + this.webSO.lastTransaction?.userId;
                          NotationService.NotationData notationData7 = data3;
                          notationData7.Note = notationData7.Note + "\nhash: " + this.webSO.lastTransaction?.hash;
                          NotationService.NotationData notationData8 = data3;
                          notationData8.Note = notationData8.Note + "\ngatewayId: " + this.webSO.lastTransaction?.gatewayId;
                          NotationService.NotationData notationData9 = data3;
                          notationData9.Note = notationData9.Note + "\ncurrency: " + this.webSO.lastTransaction?.currency;
                          NotationService.NotationData notationData10 = data3;
                          notationData10.Note = notationData10.Note + "\npaymentAmount: " + this.webSO.lastTransaction?.paymentAmount.ToString();
                          NotationService.NotationData notationData11 = data3;
                          notationData11.Note = notationData11.Note + "\npaymentCurrency: " + this.webSO.lastTransaction?.paymentCurrency;
                          NotationService.NotationData notationData12 = data3;
                          notationData12.Note = notationData12.Note + "\npaymentRate: " + this.webSO.lastTransaction?.paymentRate;
                          NotationService.NotationData notationData13 = data3;
                          notationData13.Note = notationData13.Note + "\ntype: " + this.webSO.lastTransaction?.type;
                          NotationService.NotationData notationData14 = data3;
                          notationData14.Note = notationData14.Note + "\namount: " + this.webSO.lastTransaction?.amount.ToString();
                          NotationService.NotationData notationData15 = data3;
                          notationData15.Note = notationData15.Note + "\nstatus: " + this.webSO.lastTransaction?.status;
                          NotationService.NotationData notationData16 = data3;
                          notationData16.Note = notationData16.Note + "\nreference: " + this.webSO.lastTransaction?.reference;
                          NotationService.NotationData notationData17 = data3;
                          notationData17.Note = notationData17.Note + "\ncode: " + this.webSO.lastTransaction?.code;
                          NotationService.NotationData notationData18 = data3;
                          notationData18.Note = notationData18.Note + "\nmessage: " + this.webSO.lastTransaction?.message;
                          NotationService.NotationData notationData19 = data3;
                          notationData19.Note = notationData19.Note + "\nnote: " + this.webSO.lastTransaction?.note;
                          NotationService.NotationData notationData20 = data3;
                          notationData20.Note = notationData20.Note + "\ndateCreated: " + this.webSO.lastTransaction?.dateCreated.ToString();
                          NotationService.NotationData notationData21 = data3;
                          notationData21.Note = notationData21.Note + "\ndateUpdated: " + this.webSO.lastTransaction?.dateUpdated.ToString();
                          NotationService.NotationData notationData22 = data3;
                          notationData22.Note = notationData22.Note + "\namountAsCurrency: " + this.webSO.lastTransaction?.amountAsCurrency;
                          NotationService.NotationData notationData23 = data3;
                          notationData23.Note = notationData23.Note + "\npaymentAmountAsCurrency: " + this.webSO.lastTransaction?.paymentAmountAsCurrency;
                          NotationService.NotationData notationData24 = data3;
                          notationData24.Note = notationData24.Note + "\nrefundableAmountAsCurrency: " + this.webSO.lastTransaction?.refundableAmountAsCurrency;
                          data3.Note += "\n";
                          this.orderSelectForm.notationSvc(VMorder.SiteID).AddNotation(data3);
                        }
                        Downloader downloader = new Downloader(this.orderSelectForm);
                        if (this.webSO.documents != null && this.webSO.documents.Length != 0)
                        {
                          using (IDbConnection connection = databaseConnection.Connection)
                          {
                            try
                            {
                              if (databaseConnection.Transaction == null)
                                databaseConnection.Transaction = connection.BeginTransaction();
                              string path1 = new APPLICATION_GLOBAL(databaseConnection).Load(new Decimal?((Decimal) 1)).DOCUMENT_DIRECTORY;
                              DOCUMENT_REF_PATH documentRefPath = new DOCUMENT_REF_PATH(databaseConnection).Load(order.SiteID, "Customer Order");
                              if (documentRefPath.RowState == DataRowState.Unchanged)
                                path1 = documentRefPath.DOC_FILE_PATH;
                              int num13 = 0;
                              foreach (string document1 in this.webSO.documents)
                              {
                                ++num13;
                                string str19 = order.CustomerOrderID + "_" + num13.ToString() + "_";
                                Uri uri = new Uri(document1);
                                string path = HttpUtility.ParseQueryString(uri.Query)?.Get("filename");
                                string path2;
                                if (!string.IsNullOrWhiteSpace(path))
                                {
                                  string extension = Path.GetExtension(path);
                                  string withoutExtension = Path.GetFileNameWithoutExtension(path);
                                  path2 = str19 + withoutExtension.Substring(0, Math.Min(withoutExtension.Length, 60 - extension.Length)) + extension;
                                }
                                else
                                  path2 = str19 + ".pdf";
                                if (downloader.Download(uri.AbsoluteUri, Path.Combine(path1, path2)))
                                {
                                  DOCUMENT document2 = new DOCUMENT(databaseConnection);
                                  document2.Load(uri.Segments[uri.Segments.Length - 1]);
                                  if (document2.RowState == DataRowState.Added)
                                  {
                                    document2.ID = path2;
                                    document2.REFERENCE_TYPE = "G";
                                    document2.DESCRIPTION = uri.Segments[0];
                                    document2.DOC_FILE_PATH = path1;
                                    document2.ECN_REV_CONTROL = "N";
                                    document2.ALLOW_EMAIL = "N";
                                    document2.PATH_TYPE = "N";
                                    document2.SITE_ID = order.SiteID;
                                    document2.Save();
                                  }
                                  if (document2 != null)
                                  {
                                    DOCUMENT_REFERENCE documentReference = new DOCUMENT_REFERENCE(databaseConnection);
                                    documentReference.ID = order.CustomerOrderID;
                                    documentReference.DOCUMENT_ID = document2.ID;
                                    documentReference.LINE_NO = new Decimal?(0M);
                                    documentReference.SOURCE_TYPE = "T";
                                    documentReference.SITE_ID = document2.SITE_ID;
                                    documentReference.Save();
                                  }
                                }
                              }
                              databaseConnection.Transaction.Commit();
                              databaseConnection.Transaction.Dispose();
                              databaseConnection.Transaction = (IDbTransaction) null;
                            }
                            catch (Exception ex)
                            {
                              try
                              {
                                databaseConnection.Transaction.Rollback();
                                databaseConnection.Transaction.Dispose();
                              }
                              catch
                              {
                              }
                              databaseConnection.Transaction = (IDbTransaction) null;
                              int num14 = (int) MBox.Show("Can't create Document Reference: ---/r/n" + ex.ToString());
                            }
                          }
                        }
                        if (!string.IsNullOrEmpty(this.webSO.visualQuoteId))
                        {
                          try
                          {
                            QUOTE_ORDER quoteOrder = new QUOTE_ORDER(databaseConnection).Load(this.webSO.visualQuoteId, order.CustomerOrderID);
                            if (!quoteOrder.CREATE_DATE.HasValue)
                              quoteOrder.CREATE_DATE = new DateTime?(DateTime.Now);
                            quoteOrder.Save();
                            QUOTE quote = new QUOTE(databaseConnection).Load(this.webSO.visualQuoteId);
                            quote.STATUS = "W";
                            quote.WON_LOSS_DATE = new DateTime?(DateTime.Today);
                            quote.Save();
                          }
                          catch (Exception ex)
                          {
                            int num15 = (int) MBox.Show("Can't create Quote to Order Relationship for " + this.webSO.visualQuoteId + " and " + order.CustomerOrderID + Environment.NewLine + ex.ToString());
                          }
                        }
                      }
                      this.VisualOrderID = salesOrder.Orders[0].CustomerOrderHeader.CustomerOrderID;
                      string message = "Order ID(s):";
                      if (salesOrder.Orders.Count > 1)
                        message = "Order IDs:";
                      foreach (CustomerOrderHeaderResponse order in salesOrder.Orders)
                        message = message + " " + order.CustomerOrderHeader.CustomerOrderID;
                      if (!this.debug)
                      {
                        if (this.craftCmsUtil.updateOrderStatus(this.orderIncrement, "processing", "WebOrderImport created Visual " + message + " for Visual Customer: " + salesOrder.Orders[0].CustomerOrderHeader.CustomerID))
                        {
                          if (Settings.Default.VerboseDialogs)
                          {
                            int num16 = (int) MBox.Show(message);
                          }
                        }
                        else
                        {
                          int num17 = (int) MBox.Show("updateOrderStatus failed! \r\n\r\n " + message);
                        }
                      }
                      else if (Settings.Default.VerboseDialogs)
                      {
                        int num18 = (int) MBox.Show(message);
                      }
                    }
                  }
                  data1.Orders.Clear();
                }
              }
            }
            else
            {
              int num = (int) MBox.Show("Visual Order " + referenceList[0].ID + " exists for web order: " + this.orderIncrement + " (" + referenceList[0].ExternalID + ")");
            }
          }
          else
          {
            int num = (int) MBox.Show("No Customer ID: Visual Order not created for web order: " + this.webSO.id.ToString());
            flag1 = false;
          }
        }
      }
      catch (Exception ex)
      {
        int num = (int) MBox.Show(ex.ToString());
        flag1 = false;
      }
      return flag1;
    }

    internal string GetCustomer(Order webSO, string SiteId, bool createNewCust = false)
    {
      bool flag = Settings.Default.GuestCustomerID.ToUpper() == "CREATE_NEW_ACCOUNT" && webSO.customer.user == null;
      CraftCMS.Customer customer1;
      if (!flag)
      {
        customer1 = webSO.customer;
      }
      else
      {
        customer1 = new CraftCMS.Customer()
        {
          user = new User(),
          id = "-1"
        };
        customer1.user.email = string.IsNullOrEmpty(webSO.customer.user?.email) ? webSO.email : webSO.customer.user.email;
        customer1.user.firstName = string.IsNullOrEmpty(webSO.customer.user?.firstName) ? webSO.billingAddress.firstName : webSO.customer.user?.firstName;
        customer1.user.lastName = string.IsNullOrEmpty(webSO.customer.user?.lastName) ? webSO.billingAddress.lastName : webSO.customer.user?.lastName;
      }
      string email = webSO.customerId.ToString();
      if (flag)
        email = webSO.email;
      string ExternalID = webSO.customerId;
      if (string.IsNullOrEmpty(webSO.customer.userId))
        ExternalID = "[" + SiteId + "_Guest]";
      List<ExternalReference.Reference> referenceList = this.orderSelectForm.extRefSvc(SiteId).ExternalReferenceLookup("false", "WebCustomer", ExternalID, (string) null, (string) null, (string) null, (string) null);
      string VisualCustID;
      if (referenceList.Count == 0 | createNewCust)
      {
        if (string.IsNullOrEmpty(webSO.customer.user?.visualId) | createNewCust)
        {
          int num1;
          if (Settings.Default.VerboseDialogs)
            num1 = MBox.Show("ecomm Customer: " + customer1.user?.firstName + " " + customer1.user?.lastName + " " + customer1.user?.email + " is not a Visual Customer\r\nCreate?", "Create Customer", MessageBoxButtons.YesNo) == DialogResult.Yes ? 1 : 0;
          else
            num1 = 1;
          if (num1 != 0)
          {
            CustomerData data1 = new CustomerData();
            data1.Customers = new List<Synergy.DistributedERP.Customer>();
            ExternalReference.Reference reference = new ExternalReference.Reference();
            reference.ExternalType = "WebCustomer";
            reference.ExternalID = email;
            reference.ExternalLineNo = "0";
            Synergy.DistributedERP.Customer customer2 = new Synergy.DistributedERP.Customer();
            customer2.ReferenceList.Add(reference);
            customer2.CurrencyID = Settings.Default.CurrencyID;
            customer2.ContactEmail = string.IsNullOrEmpty(customer1.user?.email) ? webSO.email : customer1.user.email;
            customer2.CustomerID = SiteId + "_" + webSO.customerId;
            Synergy.DistributedERP.Customer customer3 = new Synergy.DistributedERP.Customer();
            customer3.CustomerID = customer2.CustomerID;
            string customerId = customer2.CustomerID;
            int num2 = 0;
            CustomerData data2 = new CustomerData()
            {
              Customers = new List<Synergy.DistributedERP.Customer>()
            };
            data2.Customers.Add(customer3);
            CustomerData customerData;
            do
            {
              customerData = this.orderSelectForm.custSvc(SiteId).SearchCustomer(data2);
              customer2.CustomerID = data2.Customers[0].CustomerID;
              ++num2;
              data2.Customers[0].CustomerID = customerId + "-" + num2.ToString();
            }
            while (customerData.Customers.Count > 0);
            string str = (customer1.user?.firstName.Trim() + " " + customer1.user?.lastName).Trim();
            if (string.IsNullOrWhiteSpace(str))
            {
              str = webSO.billingAddress?.firstName.Trim() + " " + webSO.billingAddress?.lastName.Trim();
              if (string.IsNullOrWhiteSpace(str))
                str = webSO.billingAddress?.fullName;
            }
            customer2.BillingName = customer2.CustomerName = string.IsNullOrEmpty(webSO.billingAddress.businessName) ? str : webSO.billingAddress.businessName;
            customer2.BillingAddress1 = customer2.Address1 = webSO.billingAddress.address1;
            customer2.BillingAddress2 = customer2.Address2 = webSO.billingAddress.address2;
            if (customer2.BillingName != str)
              customer2.BillingAddress3 = customer2.Address3 = str;
            customer2.BillingCity = customer2.City = webSO.billingAddress.city;
            customer2.BillingCountryID = customer2.CountryID = webSO.billingAddress.countryIso;
            customer2.BillingCountry = customer2.Country = webSO.billingAddress.countryText;
            customer2.BillingState = !string.IsNullOrWhiteSpace(webSO.billingAddress.abbreviationText) ? webSO.billingAddress.abbreviationText : (customer2.State = webSO.billingAddress.stateText);
            customer2.BillingZipCode = customer2.ZipCode = webSO.billingAddress.zipCode;
            if (SiteId == "CSC")
            {
              if (!string.IsNullOrEmpty(Settings.Default.TerritoryID))
                customer2.TerritoryID = Settings.Default.TerritoryID;
              if (!string.IsNullOrEmpty(Settings.Default.DiscountCode))
                customer2.DiscountCode = Settings.Default.DiscountCode;
            }
            if (!string.IsNullOrEmpty(Settings.Default.TermsID))
              customer2.TermsID = Settings.Default.TermsID;
            customer2.UserDefined7 = SiteId;
            customer2.TaxExempt = new bool?(!string.IsNullOrEmpty(customer1.user?.avataxCustomerUsageType?.value));
            string customerCustomAttr = CraftUtil.GetCustomerCustomAttr((object) customer1, "industry");
            if (!string.IsNullOrEmpty(customerCustomAttr))
              customer2.IndustrialCode = customerCustomAttr;
            string shippingInfo1 = this.WebShippingInfo.GetShippingInfo("account");
            string shippingInfo2 = this.WebShippingInfo.GetShippingInfo("carrier");
            if (!string.IsNullOrEmpty(shippingInfo1))
            {
              switch (shippingInfo2)
              {
                case "UPS":
                  customer2.UserDefined8 = shippingInfo1;
                  break;
                case "FEDEX":
                  customer2.UserDefined9 = shippingInfo1;
                  break;
                case "DHL":
                  customer2.UserDefined10 = shippingInfo1;
                  break;
              }
            }
            customer2.Entities.Add(new CustomerEntity()
            {
              EntityID = Settings.Default.EntityID
            });
            data1.Customers.Add(customer2);
            CustomerData customer4 = this.orderSelectForm.custSvc(SiteId).CreateCustomer(data1);
            if (customer4.Customers != null && customer4.Customers.Count > 0)
            {
              this._CustomerInfo = customer4.Customers[0];
              VisualCustID = customer4.Customers[0].CustomerID;
              if (!flag && customer1.userId != null && !this.debug)
                this.craftCmsUtil.CustomerUpdate(customer1, VisualCustID);
              if (Settings.Default.VerboseDialogs)
              {
                int num3 = (int) MBox.Show("ecomm Customer: " + customer1.user?.firstName + " " + customer1.user?.lastName + " " + customer1.user?.email + "\r\nVisual Customer: " + VisualCustID, "Customer Creation");
              }
            }
            else
            {
              int num4 = (int) MBox.Show("Visual Customer Creation Failed for: " + customer1.user?.firstName + " " + customer1.user?.lastName + " " + customer1.user?.email);
              VisualCustID = (string) null;
            }
          }
          else
            VisualCustID = (string) null;
        }
        else
        {
          VisualCustID = (string) null;
          User user = customer1.user;
          string[] strArray1;
          if (user == null)
          {
            strArray1 = (string[]) null;
          }
          else
          {
            string visualId = user.visualId;
            if (visualId == null)
              strArray1 = (string[]) null;
            else
              strArray1 = visualId.Split(',');
          }
          string[] strArray2 = strArray1;
          if (strArray2 != null)
          {
            foreach (string str in strArray2)
            {
              if (this.ValidateCustomer(str.Trim(), customer1, true, false, SiteId))
              {
                VisualCustID = str.Trim();
                if (Settings.Default.VerboseDialogs)
                {
                  int num = (int) MBox.Show("ecomm Customer: " + customer1.user?.firstName + " " + customer1.user?.lastName + " " + customer1.user?.email + "\r\nVisual Customer: " + VisualCustID, "Customer Match ecomm->Visual");
                  break;
                }
                break;
              }
            }
          }
          if (VisualCustID == null)
          {
            int num5 = (int) MBox.Show("ecomm Customer: " + webSO.customerId + "\r\n" + customer1.user?.firstName + " " + customer1.user?.lastName + " " + customer1.user?.email + "\r\n Does not exist in Visual but has a Visual Customer ID list :" + customer1.user?.visualId);
          }
        }
      }
      else
      {
        if (flag && customer1.userId != null && !this.debug)
          this.craftCmsUtil.CustomerUpdate(customer1, referenceList[0].ID);
        if (string.IsNullOrEmpty(webSO.customer.user?.visualId))
          VisualCustID = referenceList[0].ID;
        else if (!this.InCSVList(referenceList[0].ID, webSO.customer.user?.visualId))
        {
          VisualCustID = (string) null;
          int num = (int) MBox.Show("The Visual ID's don't match for ecomm CustomerID: " + webSO.customerId + "\r\n External Reference: " + referenceList[0].ID + "\r\necomm Visual Customer ID list: " + customer1.user?.visualId);
        }
        else if (this.ValidateCustomer(referenceList[0].ID, customer1, false, false, SiteId))
        {
          VisualCustID = referenceList[0].ID;
          if (Settings.Default.VerboseDialogs)
          {
            int num = (int) MBox.Show("ecomm Customer: " + customer1.user?.firstName + " " + customer1.user?.lastName + " " + customer1.user?.email + "\r\nVisual Customer: " + VisualCustID, "Customer Match Visual==ecomm");
          }
        }
        else
          VisualCustID = (string) null;
      }
      return VisualCustID;
    }

    internal string GetAddress(Order webSO, CustomerOrderHeader VMorder)
    {
      bool flag = false;
      AddressData data = new AddressData();
      data.Addresses = new List<CustomerAddress>();
      data.CustomerID = VMorder.CustomerID;
      Shippingaddress shippingAddress = webSO?.shippingAddress;
      string str1 = shippingAddress.firstName + " " + shippingAddress.lastName;
      if (string.IsNullOrWhiteSpace(str1))
        str1 = shippingAddress.fullName;
      string str2 = string.IsNullOrEmpty(shippingAddress.businessName) ? str1 : shippingAddress.businessName;
      List<ExternalReference.Reference> referenceList = this.orderSelectForm.extRefSvc(VMorder.SiteID).ExternalReferenceLookup("false", "WebAddress", shippingAddress.id.ToString(), "0", "CustomerAddress", VMorder.CustomerID, (string) null);
      if (referenceList.Count == 0)
        flag = true;
      else if (referenceList.Count == 1)
      {
        CustomerAddress customerAddress = new CustomerAddress();
        customerAddress.AddressNO = new Decimal?(Convert.ToDecimal(referenceList[0].LineNo));
        data.Addresses.Clear();
        data.Addresses.Add(customerAddress);
        AddressData addressData = this.orderSelectForm.custSvc(VMorder.SiteID).SearchAddress(data);
        if (addressData.Addresses.Count == 1)
        {
          CustomerAddress address = addressData.Addresses[0];
          if ((address.Name ?? "") != (str2 ?? "") || (address.Address1 ?? "") != (shippingAddress.address1 ?? "") || (address.Address2 ?? "") != (shippingAddress.address2 ?? "") || (str2 ?? "") != (str1 ?? "") && (address.Address3 ?? "") != (str1 ?? "") || (str2 ?? "") == (str1 ?? "") && (address.Address3 ?? "") != "" || (address.City ?? "") != (this._City ?? "") || (address.State ?? "") != (this._State ?? "") || (address.ZipCode ?? "") != (this._ZipCode ?? "") || (address.Country ?? "") != (this._Country ?? ""))
          {
            if (Settings.Default.VerboseDialogs)
            {
              int num = (int) MBox.Show("Address information does not match, creating new ShipToID in Visual");
            }
            flag = true;
          }
          else
            VMorder.ShipToID = address.ShipToID;
        }
        else if (addressData.Addresses.Count > 1)
        {
          int num1 = (int) MBox.Show("Multiple ShipTos associatesd with \"Address\" " + referenceList[0].LineNo);
        }
      }
      else if (referenceList.Count > 1)
      {
        int num2 = (int) MBox.Show("Multiple addresses associatesd with \"WebAddress\" " + shippingAddress.id.ToString());
      }
      if (flag)
      {
        ExternalReference.Reference reference = new ExternalReference.Reference();
        reference.ExternalType = "WebAddress";
        reference.ExternalID = shippingAddress.id.ToString();
        reference.ExternalLineNo = "0";
        CustomerAddress caData = new CustomerAddress();
        caData.ReferenceList.Add(reference);
        caData.Name = str2;
        caData.Address1 = shippingAddress.address1;
        caData.Address2 = shippingAddress.address2;
        if (str2 != str1)
          caData.Address3 = str1;
        caData.City = this._City;
        caData.State = this._State;
        caData.ZipCode = this._ZipCode;
        caData.Country = this._Country;
        if (VMorder.SiteID == "CSC")
        {
          if (!string.IsNullOrEmpty(Settings.Default.DiscountCode))
            caData.DiscountCode = Settings.Default.DiscountCode;
          if (!string.IsNullOrEmpty(Settings.Default.TerritoryID))
            caData.Territory = Settings.Default.TerritoryID;
        }
        if (!string.IsNullOrEmpty(Settings.Default.FOB))
          caData.FOB = Settings.Default.FOB;
        if (!string.IsNullOrEmpty(Settings.Default.FreightTaxGroupID))
          caData.DefSlsTaxGrpID = Settings.Default.FreightTaxGroupID;
        caData.CarrierID = this.WebShippingInfo.GetShippingInfo("carrier");
        string shippingInfo = this.WebShippingInfo.GetShippingInfo("account");
        if (caData.CarrierID != null && !string.IsNullOrEmpty(shippingInfo))
        {
          if (caData.CarrierID.ToUpper() == "UPS")
            caData.UserDefined8 = shippingInfo;
          else if (caData.CarrierID.ToUpper() == "FEDEX")
            caData.UserDefined9 = shippingInfo;
          else if (caData.CarrierID.ToUpper() == "DHL")
            caData.UserDefined10 = shippingInfo;
        }
        string str3 = Settings.Default.AddressPrefix + (caData.Name.Replace(" ", "").Replace("'", "") + "___").Substring(0, 3) + (caData.City.Replace(" ", "").Replace("'", "") + "___").Substring(0, 3);
        CustomerAddress customerAddress = new CustomerAddress();
        customerAddress.ShipToID = str3;
        data.Addresses.Clear();
        data.Addresses.Add(customerAddress);
        int num3 = 0;
        AddressData addressData;
        do
        {
          addressData = this.orderSelectForm.custSvc(VMorder.SiteID).SearchAddress(data);
          caData.ShipToID = data.Addresses[0].ShipToID;
          ++num3;
          data.Addresses[0].ShipToID = str3 + num3.ToString();
        }
        while (addressData.Addresses.Count > 0);
        this.orderSelectForm.custSvc(VMorder.SiteID).AddNewAddress(VMorder.CustomerID, caData, (string) null);
        VMorder.ShipToID = caData.ShipToID;
      }
      return VMorder.ShipToID;
    }

    internal string GetContact(Order webSO, string CustomerID, string SiteId)
    {
      bool flag1 = false;
      bool flag2 = false;
      string contact1 = (string) null;
      ContactData data1 = new ContactData();
      data1.Contacts = new List<Contact>();
      List<ExternalReference.Reference> referenceList = this.orderSelectForm.extRefSvc(SiteId).ExternalReferenceLookup("false", "WebAddress", webSO.email, "0", "Contact", (string) null, "0");
      if (referenceList.Count == 0)
      {
        flag1 = true;
        CustContactData data2 = new CustContactData();
        CustContact custContact = new CustContact();
        custContact.CustomerID = CustomerID;
        data2.CustContacts = new List<CustContact>();
        data2.CustContacts.Add(custContact);
        if (this.orderSelectForm.contactSvc(SiteId).SearchCustContact(data2).CustContacts.Count == 0)
          flag2 = true;
      }
      else if (referenceList.Count == 1)
      {
        ContactListDataResponse contactList = this.orderSelectForm.contactSvc(SiteId).GetContactList("N", 0, 0, "N", CustomerID, (string) null, (string) null, "Y");
        if (contactList.ContactCount == 1)
        {
          ContactListItem contact2 = contactList.ContactList[0];
          if (contact2.Contact.FirstName != (webSO.customer.userId == null ? webSO.billingAddress.firstName : webSO.customer.user.firstName) || contact2.Contact.LastName != (webSO.customer.userId == null ? webSO.billingAddress.lastName : webSO.customer.user.lastName) || contact2.Contact.Email != (webSO.customer.userId == null ? webSO.email : webSO.customer.user.email))
          {
            int num = (int) MBox.Show("Contact information does not match, creating new ContactID in Visual");
            flag1 = true;
          }
          else
            contact1 = referenceList[0].ID;
        }
        else if (contactList.ContactCount > 1)
        {
          flag1 = true;
          foreach (ContactListItem contact3 in contactList.ContactList)
          {
            if (contact3.Contact.FirstName == (webSO.customer.userId == null ? webSO.billingAddress.firstName : webSO.customer.user.firstName) && contact3.Contact.LastName == (webSO.customer.userId == null ? webSO.billingAddress.lastName : webSO.customer.user.lastName) && contact3.Contact.Email == (webSO.customer.userId == null ? webSO.email : webSO.customer.user.email))
            {
              flag1 = false;
              contact1 = contact3.ContactID;
              break;
            }
          }
        }
      }
      else if (referenceList.Count > 1)
      {
        int num1 = (int) MBox.Show("Multiple Contacts associatesd with \"WebAddress\" " + webSO.email);
      }
      if (flag1)
      {
        ExternalReference.Reference reference = new ExternalReference.Reference();
        reference.ExternalType = "WebAddress";
        reference.ExternalID = webSO.email;
        reference.ExternalLineNo = "0";
        CustContact custContact = new CustContact();
        custContact.CustomerID = CustomerID;
        if (flag2)
          custContact.PrimaryContact = new bool?(true);
        Contact contact4 = new Contact();
        contact4.ReferenceList.Add(reference);
        contact4.FirstName = webSO.customer.userId == null ? webSO.billingAddress.firstName : webSO.customer.user.firstName;
        contact4.LastName = webSO.customer.userId == null ? webSO.billingAddress.lastName : webSO.customer.user.lastName;
        contact4.Email = webSO.customer.userId == null ? webSO.email : webSO.customer.user.email;
        contact4.Phone = webSO.customer.userId == null ? webSO.billingAddress.phone : webSO.customer.user.phone;
        contact4.MaritalStatus = "N";
        contact4.GenderCode = "N";
        contact4.NoEmail = new bool?(true);
        contact4.NoCallPhone = new bool?(true);
        contact4.NoCallMobile = new bool?(true);
        contact4.Customers = new List<CustContact>();
        contact4.Customers.Add(custContact);
        data1.Contacts.Clear();
        data1.Contacts.Add(contact4);
        ContactDataResponse contact5 = this.orderSelectForm.contactSvc(SiteId).CreateContact(data1);
        if (contact5.Contacts.Count == 1)
          contact1 = contact5.Contacts[0].Contact.ContactID;
      }
      return contact1;
    }

    internal object GetCountryEntity(string country_id)
    {
      if (OrderImport.CountryDict.Count == 0)
      {
        foreach (object country in this.craftCmsUtil.CountryList())
          ;
      }
      object obj;
      return OrderImport.CountryDict.TryGetValue(country_id, out obj) ? obj : new object();
    }

    internal object GetRegionEntity(int region_id, string country_id)
    {
      this.GetCountryEntity(country_id);
      region_id.ToString();
      return new object();
    }

    internal bool ValidateCustomer(
      string visualCustomerID,
      CraftCMS.Customer custWeb,
      bool createXref,
      bool updateWebCustomer,
      string SiteId)
    {
      CustomerData data = new CustomerData();
      data.Customers = new List<Synergy.DistributedERP.Customer>();
      Synergy.DistributedERP.Customer customer1 = new Synergy.DistributedERP.Customer(visualCustomerID);
      customer1.CustomerID = visualCustomerID;
      data.Customers.Add(customer1);
      CustomerData customerData = this.orderSelectForm.custSvc(SiteId).SearchCustomer(data);
      if (customerData.Customers.Count <= 0)
        return false;
      CustomerValidate customerValidate = new CustomerValidate(customerData.Customers[0], custWeb);
      if ((createXref || Settings.Default.VerboseDialogs) && customerValidate.ShowDialog() != DialogResult.OK)
        return false;
      this._CustomerInfo = customerData.Customers[0];
      if (createXref)
      {
        customer1.ReferenceList.Add(new ExternalReference.Reference()
        {
          ExternalType = "WebCustomer",
          ExternalID = custWeb.id.ToString(),
          ExternalLineNo = "0"
        });
        data.Customers.Clear();
        data.Customers.Add(customer1);
        CustomerData customer2 = this.orderSelectForm.custSvc(SiteId).CreateCustomer(data);
        if (customer2.Customers != null && customer2.Customers.Count > 0)
          this._CustomerInfo = customer2.Customers[0];
        if (updateWebCustomer && custWeb.userId != null && !this.debug)
          this.craftCmsUtil.CustomerUpdate(custWeb, visualCustomerID);
      }
      return true;
    }

    private bool InCSVList(string target, string CSVList)
    {
      bool flag = false;
      string str1 = CSVList;
      char[] chArray = new char[1]{ ',' };
      foreach (string str2 in str1.Split(chArray))
      {
        string strB = str2?.Trim();
        if (!string.IsNullOrWhiteSpace(target) && !string.IsNullOrWhiteSpace(strB) && string.Compare(target, strB, true) == 0)
        {
          flag = true;
          break;
        }
      }
      return flag;
    }
  }
}
