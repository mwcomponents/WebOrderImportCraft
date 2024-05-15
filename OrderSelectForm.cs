// Decompiled with JetBrains decompiler
// Type: WebOrderImportCraft.OrderSelectForm
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

using CraftCMS;
using MsgBox;
using SRISupport;
using Synergy.BusinessObjects;
using Synergy.BusinessObjects.AllObjects;
using Synergy.BusinessObjects.Schema;
using Synergy.BusinessObjects.VE;
using Synergy.DistributedERP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Windows.Forms;
using WebOrderImportCraft.Properties;

namespace WebOrderImportCraft
{
  public class OrderSelectForm : Form
  {
    private bool _Debug = false;
    internal List<object> _OrderList = new List<object>();
    private int listOffset = 0;
    private int listChunkSize = 25;
    private bool ReadyToLoad = false;
    private char[] valSep = new char[1]{ '|' };
    private char[] qualSep = new char[1]{ ':' };
    private string accessToken = Settings.Default.APIKEY;
    private WSHttpBinding binding = new WSHttpBinding();
    private CraftUtil craftUtil = new CraftUtil();
    internal ExternalReferenceService extRefSvc0;
    internal CustomerService custSvc0;
    internal SalesOrderService orderSvc0;
    internal ContactService contactSvc0;
    internal ExternalReferenceService extRefSvc1;
    internal CustomerService custSvc1;
    internal SalesOrderService orderSvc1;
    internal ContactService contactSvc1;
    internal NotationService notationSvc1;
    internal InventoryService inventorySvc1;
    internal EstimateService quoteSvc1;
    internal UserDefinedFieldService UDFSvc1;
    internal ExternalReferenceService extRefSvc2;
    internal CustomerService custSvc2;
    internal SalesOrderService orderSvc2;
    internal ContactService contactSvc2;
    internal NotationService notationSvc2;
    internal InventoryService inventorySvc2;
    internal EstimateService quoteSvc2;
    internal UserDefinedFieldService UDFSvc2;
    internal Header header0 = (Header) null;
    internal Header header1 = (Header) null;
    internal Header header2 = (Header) null;
    internal DatabaseConnection Database0;
    internal DatabaseConnection Database1;
    internal DatabaseConnection Database2;
    private logger LogFile;
    private UserMessageBatch UserMsg;
    private MethodInfo SchemaGetter = (MethodInfo) null;
    private bool _isDroppedDown = false;
    private bool datePush = false;
    private IContainer components = (IContainer) null;
    private ComboBox comboBoxVisualConnect;
    private ComboBox comboBoxOrderStatus;
    private DateTimePicker dateTimePickerTo;
    private DateTimePicker dateTimePickerFrom;
    private ComboBox comboBoxCustomer;
    private Label label7;
    private Label label6;
    private Label label4;
    private Label label1;
    private DataGridView dataGridView1;
    private Label labelServerURL;
    private Label labelVersion;
    private Button buttonCancel;
    private Button buttonOK;
    private ContextMenuStrip contextMenuStripCustID;
    private ToolStripMenuItem toolStripMenuItemLookupID;
    private ToolStripMenuItem toolStripMenuItemEditID;
    private ContextMenuStrip contextMenuStripOrderID;
    private ToolStripMenuItem openCustomerOrderToolStripMenuItem;
    private Button buttonRefresh;
    private ComboBox comboBoxListChunk;
    private Button buttonNext;
    private Button buttonPrev;
    private ComboBox comboBoxStore;
    private ComboBox comboBoxTaxStatus;
    private Label label2;
    private Label label3;
    private Label labelTotalOrders;
    private Label labelSelectedOrders;
    private ContextMenuStrip contextMenuStripAssocOrder;
    private ToolStripMenuItem associateOrderToolStripMenuItem;
    private CheckBox chkBox_Incomplete;
    private DataGridViewTextBoxColumn Store;
    private DataGridViewTextBoxColumn OrderID;
    private DataGridViewTextBoxColumn VisualOrderID;
    private DataGridViewTextBoxColumn OrderDate;
    private DataGridViewTextBoxColumn CustomerID;
    private DataGridViewTextBoxColumn VisualCustomerID;
    private DataGridViewTextBoxColumn CustomerName;
    private DataGridViewTextBoxColumn VisualCustomerName;
    private DataGridViewTextBoxColumn EmailAddress;
    private DataGridViewTextBoxColumn PhoneNo;
    private DataGridViewTextBoxColumn OrderStatus;
    private DataGridViewTextBoxColumn PaymentAuth;
    private DataGridViewTextBoxColumn TaxStatus;
    private DataGridViewTextBoxColumn Shipping;
    private DataGridViewTextBoxColumn IncrementId;
    private DataGridViewTextBoxColumn CouponID;
    private ToolStripMenuItem craftOrderDetailToolStripMenuItem;
    private ToolStripMenuItem craftOrderDetailToolStripMenuItem2;
    internal Panel panelDownLoad;
    private Label labelDownLoading;
    private PictureBox pictureBox1;
    internal Label labelFName;

    public OrderSelectForm()
    {
      this.InitializeComponent();
      this.panelDownLoad.Visible = false;
      this.comboBoxStore.Visible = false;
      this.label3.Visible = false;
      this.comboBoxTaxStatus.Visible = false;
      this.label2.Visible = false;
      this.dataGridView1.Columns[nameof (TaxStatus)].Visible = false;
      Assembly executingAssembly = Assembly.GetExecutingAssembly();
      Label labelVersion = this.labelVersion;
      int num = executingAssembly.GetName().Version.Major;
      string str1 = num.ToString();
      num = executingAssembly.GetName().Version.Minor;
      string str2 = num.ToString();
      string str3 = "v." + str1 + "." + str2;
      labelVersion.Text = str3;
      EndpointAddress endpointAddress = new EndpointAddress(Settings.Default.EComSite_Service);
      this.binding.MaxReceivedMessageSize = 65536000L;
      if (endpointAddress.Uri.Scheme == "https")
      {
        this.binding.Security.Mode = SecurityMode.Transport;
        ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(OrderSelectForm.trustCertificatesCallback);
        ServicePointManager.SecurityProtocol = (SecurityProtocolType) 4080;
      }
      else
        this.binding.Security.Mode = SecurityMode.None;
      this.craftUtil.binding = (System.ServiceModel.Channels.Binding) this.binding;
      this.craftUtil.accessToken = this.accessToken;
      this.labelServerURL.Text = endpointAddress.Uri.ToString();
      this.header0 = new Header();
      this.header1 = new Header();
      this.header1.Key = Settings.Default.VMKey;
      this.header1.ExternalRefGroup = Settings.Default.ExternalReferenceGroup;
      this.header2 = new Header();
      this.header2.Key = Settings.Default.VMKey2;
      this.header2.ExternalRefGroup = Settings.Default.ExternalReferenceGroup;
      Alias.aliasDictionary.Clear();
      string siteAliases = Settings.Default.SiteAliases;
      if (!string.IsNullOrEmpty(siteAliases))
      {
        foreach (string str4 in siteAliases.Split(this.valSep))
        {
          string[] strArray = str4.Split(this.qualSep, 2);
          if (strArray.Length == 2 && !string.IsNullOrWhiteSpace(strArray[1]))
            Alias.aliasDictionary.Add(strArray[0], strArray[1]);
        }
      }
      FreightSvcChg.freightSvcChgDictionary.Clear();
      string freightSvcChg = Settings.Default.FreightSvcChg;
      if (!string.IsNullOrEmpty(freightSvcChg))
      {
        foreach (string str5 in freightSvcChg.Split(this.valSep))
        {
          string[] strArray = str5.Split(this.qualSep, 2);
          if (strArray.Length == 2 && !string.IsNullOrWhiteSpace(strArray[1]))
            FreightSvcChg.freightSvcChgDictionary.Add(strArray[0], strArray[1]);
        }
      }
      TaxSvcChg.taxSvcChgDictionary.Clear();
      string taxSvcChg = Settings.Default.TaxSvcChg;
      if (!string.IsNullOrEmpty(taxSvcChg))
      {
        foreach (string str6 in taxSvcChg.Split(this.valSep))
        {
          string[] strArray = str6.Split(this.qualSep, 2);
          if (strArray.Length == 2 && !string.IsNullOrWhiteSpace(strArray[1]))
            TaxSvcChg.taxSvcChgDictionary.Add(strArray[0], strArray[1]);
        }
      }
      SmallOrderSvcChg.smallOrderSvcChgDictionary.Clear();
      string smallOrderSvcChg = Settings.Default.SmallOrderSvcChg;
      if (!string.IsNullOrEmpty(smallOrderSvcChg))
      {
        foreach (string str7 in smallOrderSvcChg.Split(this.valSep))
        {
          string[] strArray = str7.Split(this.qualSep, 2);
          if (strArray.Length == 2 && !string.IsNullOrWhiteSpace(strArray[1]))
            SmallOrderSvcChg.smallOrderSvcChgDictionary.Add(strArray[0], strArray[1]);
        }
      }
      OrderStatusDefault.orderStatusDictionary.Clear();
      string orderStatusDefault = Settings.Default.OrderStatusDefault;
      if (!string.IsNullOrEmpty(orderStatusDefault))
      {
        foreach (string str8 in orderStatusDefault.Split(this.valSep))
        {
          string[] strArray = str8.Split(this.qualSep, 2);
          if (strArray.Length == 2 && !string.IsNullOrWhiteSpace(strArray[1]))
            OrderStatusDefault.orderStatusDictionary.Add(strArray[0], strArray[1]);
        }
      }
      SiteDBMap.siteDBMapDictionary.Clear();
      string siteDbMap = Settings.Default.SiteDBMap;
      if (!string.IsNullOrEmpty(siteDbMap))
      {
        foreach (string str9 in siteDbMap.Split(this.valSep))
        {
          string[] strArray = str9.Split(this.qualSep, 2);
          if (strArray.Length == 2 && !string.IsNullOrWhiteSpace(strArray[1]))
            SiteDBMap.siteDBMapDictionary.Add(strArray[0], strArray[1]);
        }
      }
      OrderPrefixMap.prefixMapDictionary.Clear();
      string orderPrefixMap = Settings.Default.OrderPrefixMap;
      if (!string.IsNullOrEmpty(orderPrefixMap))
      {
        foreach (string str10 in orderPrefixMap.Split(this.valSep))
        {
          string[] strArray = str10.Split(this.qualSep, 2);
          if (strArray.Length == 2 && !string.IsNullOrWhiteSpace(strArray[1]))
            OrderPrefixMap.prefixMapDictionary.Add(strArray[0], strArray[1]);
        }
      }
      QuotePrefixMap.prefixMapDictionary.Clear();
      string quotePrefixMap = Settings.Default.QuotePrefixMap;
      if (string.IsNullOrEmpty(quotePrefixMap))
        return;
      foreach (string str11 in quotePrefixMap.Split(this.valSep))
      {
        string[] strArray = str11.Split(this.qualSep, 2);
        if (strArray.Length == 2 && !string.IsNullOrWhiteSpace(strArray[1]))
          QuotePrefixMap.prefixMapDictionary.Add(strArray[0], strArray[1]);
      }
    }

    private static bool trustCertificatesCallback(
      object sender,
      X509Certificate cert,
      X509Chain chain,
      SslPolicyErrors errors)
    {
      switch (errors)
      {
        case SslPolicyErrors.None:
          return true;
        case SslPolicyErrors.RemoteCertificateNameMismatch:
          if (cert.Subject == "CN=*.cloudfront.net, O=\"Amazon.com, Inc.\", L=Seattle, S=Washington, C=US" || cert.Subject == "CN = mwi.ecomm.vigetx.com")
            return true;
          break;
      }
      return false;
    }

    private void PokeContactNoSchema(BaseDomainObject obj)
    {
      try
      {
        if (this.SchemaGetter == (MethodInfo) null)
          this.SchemaGetter = typeof (BaseDomainObject).GetMethod("get__TableObjectSchema", BindingFlags.Instance | BindingFlags.NonPublic);
        iTableObjectSchema tableObjectSchema = (iTableObjectSchema) this.SchemaGetter.Invoke((object) obj, (object[]) null);
        if (tableObjectSchema != null && tableObjectSchema.Fields.ContainsKey("CONTACT_NO"))
        {
          bool isPrimaryKey = tableObjectSchema.Fields["CONTACT_NO"].IsPrimaryKey;
          tableObjectSchema.Fields.Remove("CONTACT_NO");
          tableObjectSchema.Fields.Add("CONTACT_NO", new FieldObjectDefinition("CONTACT_NO", typeof (Decimal), 8, isPrimaryKey, 10));
        }
        else
        {
          int num = (int) MBox.Show("Field not Found: CONTACT_NO", "Error in Schema Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
      }
      catch (Exception ex)
      {
        int num1 = (int) MBox.Show(ex.ToString(), "Error in Schema Update", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        if (ex.InnerException == null)
          return;
        int num2 = (int) MBox.Show("Inner Exception\r\n\r\n" + ex.InnerException.Message, "Error in Schema Update", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }

    internal void AutoImportQuotes()
    {
      this.LogFile = new logger(Settings.Default.LogFileRoot, "LogFiles");
      this.UserMsg = new UserMessageBatch(this.LogFile);
      MBox.logIt = true;
      MBox.UserMsg = this.UserMsg;
      try
      {
        Assembly executingAssembly = Assembly.GetExecutingAssembly();
        this.UserMsg.MessageSend(string.Format("Running WebOrderImport, version {0}", (object) ("v." + executingAssembly.GetName().Version.Major.ToString() + "." + executingAssembly.GetName().Version.Minor.ToString())), "Program", UserMessage.msgLevel.Information);
        this.JobSelector_Load((object) null, (EventArgs) null);
        this.JobSelector_Shown((object) null, (EventArgs) null);
        this.comboBoxVisualConnect.SelectedItem = (object) "Quotes";
        this.dateTimePickerFrom.Value = DateTime.Today.AddDays(-10.0);
        foreach (DataGridViewRow row in (IEnumerable) this.dataGridView1.Rows)
        {
          if (row.Cells[10].Value.ToString() == "submitted")
          {
            row.Selected = true;
            this.UserMsg.MessageSend(string.Format("Processing: {0}", (object) row.Cells[1].Value.ToString()), "Program", UserMessage.msgLevel.Information);
          }
        }
        this.buttonOK_Click((object) null, (EventArgs) null);
      }
      catch (Exception ex)
      {
        this.UserMsg.MessageSend(string.Format("Exception: {0}", (object) ex.ToString()), "Program", UserMessage.msgLevel.Error);
      }
      this.LogFile.createlog();
    }

    private void LoadGrid(bool resetOffset)
    {
      Decimal num1 = 0M;
      if (!this.ReadyToLoad)
        return;
      using (new HourGlass())
      {
        try
        {
          this.dataGridView1.Rows.Clear();
          this.labelTotalOrders.Text = "Total Orders: ";
          this.labelSelectedOrders.Text = "Selected Orders: ";
          Application.DoEvents();
          if (resetOffset)
          {
            this.listOffset = 0;
            this.buttonPrev.Enabled = false;
            this.buttonNext.Enabled = true;
          }
          CraftUtil.SearchFilterGroup[] filtresIn1 = new CraftUtil.SearchFilterGroup[0];
          CraftUtil.SearchFilterGroup[] filtresIn2 = new CraftUtil.SearchFilterGroup[0];
          CraftUtil.SearchFilterGroup[] filtresIn3 = this.craftUtil.AddFilter2(filtresIn1, "isCompleted", "eq", this.chkBox_Incomplete.Checked ? "false" : "true");
          CraftUtil.SearchFilterGroup[] searchFilterGroupArray1 = this.craftUtil.AddFilter2(filtresIn2, "isCompleted", "eq", this.chkBox_Incomplete.Checked ? "false" : "true");
          CraftUtil.SearchFilterGroup[] searchFilterGroupArray2 = this.craftUtil.AddFilter2(filtresIn3, "ordered_at", "from", this.dateTimePickerFrom.Value.ToString("yyyy-MM-dd HH:mm:ss"));
          CraftUtil craftUtil1 = this.craftUtil;
          CraftUtil.SearchFilterGroup[] filtresIn4 = searchFilterGroupArray2;
          DateTime dateTime = this.dateTimePickerTo.Value;
          dateTime = dateTime.AddDays(1.0);
          string str1 = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
          CraftUtil.SearchFilterGroup[] searchFilterGroupArray3 = craftUtil1.AddFilter2(filtresIn4, "ordered_at", "to", str1);
          CraftUtil craftUtil2 = this.craftUtil;
          CraftUtil.SearchFilterGroup[] filtresIn5 = searchFilterGroupArray1;
          dateTime = this.dateTimePickerFrom.Value;
          string str2 = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
          CraftUtil.SearchFilterGroup[] searchFilterGroupArray4 = craftUtil2.AddFilter2(filtresIn5, "dateCompleted", "from", str2);
          CraftUtil craftUtil3 = this.craftUtil;
          CraftUtil.SearchFilterGroup[] filtresIn6 = searchFilterGroupArray4;
          dateTime = this.dateTimePickerTo.Value;
          dateTime = dateTime.AddDays(1.0);
          string str3 = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
          CraftUtil.SearchFilterGroup[] filterGroups = this.craftUtil.AddFilter2(craftUtil3.AddFilter2(filtresIn6, "dateCompleted", "to", str3), "type", "eq", "reorderRequest,default");
          if (this.comboBoxCustomer.SelectedIndex > 0)
          {
            if (this.comboBoxCustomer.SelectedItem.ToString().Split(' ').Length == 0)
              ;
          }
          if (this.comboBoxOrderStatus.SelectedIndex > 0)
          {
            string str4 = this.comboBoxOrderStatus.SelectedItem.ToString();
            searchFilterGroupArray3 = !(str4 == "Not complete") ? this.craftUtil.AddFilter2(searchFilterGroupArray3, "status", "eq", str4) : this.craftUtil.AddFilter2(searchFilterGroupArray3, "status", "nin", "Complete");
          }
          if (this.comboBoxTaxStatus.SelectedIndex <= 0)
            ;
          List<string> stringList = new List<string>();
          Order[] orderArray = new Order[0];
          Quote[] quoteArray = new Quote[0];
          if (this.comboBoxVisualConnect?.SelectedItem.ToString() == "Quotes")
            quoteArray = this.craftUtil.Quotes(filterGroups);
          else
            orderArray = this.craftUtil.Orders(searchFilterGroupArray3);
          int num2;
          foreach (Quote quote in quoteArray)
          {
            string id = "";
            string str5 = "";
            string str6 = "";
            string str7 = "";
            string str8 = "";
            bool flag1 = false;
            if (((this.comboBoxStore.Text != "" ? 1 : 0) | 1) != 0)
            {
              bool flag2 = false;
              if (quote.lineItems != null)
              {
                foreach (QLineitem lineItem in quote.lineItems)
                {
                  if (str7 == "")
                    str7 = Alias.Get(lineItem.location ?? "");
                  if (Alias.Get(lineItem.location ?? "") != Alias.Get(lineItem.location ?? "", true))
                  {
                    if (Alias.Get(lineItem.location ?? "") + "/" + Alias.Get(lineItem.location ?? "", true) == this.comboBoxStore.Text)
                    {
                      flag2 = true;
                      break;
                    }
                  }
                  else if (Alias.Get(lineItem.location ?? "") == this.comboBoxStore.Text)
                  {
                    flag2 = true;
                    break;
                  }
                }
              }
              if (this.comboBoxStore.Text != "" && !flag2)
                continue;
            }
            ExternalReferenceService referenceService = this.extRefSvc(str7);
            num2 = quote.quoteId;
            string ExternalID1 = num2.ToString();
            List<ExternalReference.Reference> referenceList1 = referenceService.ExternalReferenceLookup("N", "WebQuote", ExternalID1, "0", "EstimateHeader", (string) null, "0");
            if (referenceList1.Count >= 1)
              str6 = referenceList1[0].ID;
            string str9 = string.Empty;
            if (quote.lineItems != null)
            {
              foreach (QLineitem lineItem in quote.lineItems)
              {
                if (string.IsNullOrEmpty(lineItem.location))
                {
                  str9 = str9 + "\r\n" + lineItem.sku;
                  if (!stringList.Contains(lineItem.sku))
                    stringList.Add(lineItem.sku);
                  if (lineItem.sku.EndsWith("CS"))
                  {
                    lineItem.location = "Century Spring";
                  }
                  else
                  {
                    string sku = lineItem.sku;
                    num2 = quote.quoteId;
                    string str10 = num2.ToString();
                    int num3 = (int) MBox.Show("No Location specified for SKU: " + sku + " on order: " + str10);
                  }
                }
                if (string.IsNullOrEmpty(str8))
                {
                  if (!string.IsNullOrEmpty(lineItem.location))
                    str8 = Alias.Get(lineItem.location);
                }
                else if (!string.IsNullOrEmpty(lineItem.location) && str8 != Alias.Get(lineItem.location))
                  flag1 = true;
              }
            }
            string ExternalID2 = "NOMATCHCUSTOMERID!@#$";
            int? nullable = quote.userId;
            if (!nullable.HasValue)
            {
              if (flag1)
              {
                string str11 = str8;
                num2 = quote.quoteId;
                string str12 = num2.ToString();
                int num4 = (int) MBox.Show("Multiple sites on a Guest Quote. Using site: " + str11 + " For Quote: " + str12);
              }
              if (string.IsNullOrEmpty(str8))
              {
                string[] strArray = new string[5]
                {
                  "No Site found on Guest Quote: ",
                  null,
                  null,
                  null,
                  null
                };
                num2 = quote.quoteId;
                strArray[1] = num2.ToString();
                strArray[2] = " ";
                strArray[3] = quote.number.Substring(0, 7);
                strArray[4] = str9;
                int num5 = (int) MBox.Show(string.Concat(strArray));
              }
              else
                ExternalID2 = "[" + str8 + "_Guest]";
            }
            else
              ExternalID2 = quote.visualCustomerId;
            if (!string.IsNullOrWhiteSpace(ExternalID2))
            {
              List<ExternalReference.Reference> referenceList2 = this.extRefSvc(str7).ExternalReferenceLookup("N", "WebCustomer", ExternalID2, "0", "Customer", (string) null, "0");
              if (referenceList2.Count >= 1)
              {
                id = referenceList2[0].ID;
                str5 = this.custSvc(str7).LookupNameByID(id);
              }
            }
            DateTime result = new DateTime();
            DateTime.TryParse(quote.dateCompleted.date + " " + quote.dateCompleted.time, out result);
            if (!(quote.type == "default") || DateTime.Compare(DateTime.Parse(quote.dateCompleted.date), new DateTime(2023, 8, 30)) >= 0)
            {
              DataGridViewRowCollection rows = this.dataGridView1.Rows;
              object[] objArray = new object[16];
              objArray[0] = (object) str7;
              string[] strArray1 = new string[5];
              num2 = quote.quoteId;
              strArray1[0] = num2.ToString();
              strArray1[1] = " (";
              strArray1[2] = QuotePrefixMap.Get(quote.type);
              strArray1[3] = ")";
              strArray1[4] = flag1 ? " ***" : "";
              objArray[1] = (object) string.Concat(strArray1);
              objArray[2] = (object) ("QUOTE: " + str6);
              objArray[3] = (object) result.ToString("yyyy-MM-dd HH:mm:ss");
              nullable = quote.userId;
              string str13;
              if (nullable.HasValue)
              {
                nullable = quote.userId;
                ref int? local = ref nullable;
                if (!local.HasValue)
                {
                  str13 = (string) null;
                }
                else
                {
                  num2 = local.GetValueOrDefault();
                  str13 = num2.ToString();
                }
              }
              else
                str13 = ExternalID2;
              objArray[4] = (object) str13;
              objArray[5] = string.IsNullOrEmpty(id) ? (object) quote.visualCustomerId : (object) id;
              objArray[6] = (object) (quote.firstName + " " + quote.lastName + " " + quote.company + " " + quote.visualOrderId + " " + quote.visualCustomerId);
              objArray[7] = (object) str5;
              objArray[8] = (object) quote.email;
              objArray[9] = (object) "";
              objArray[10] = (object) quote.stage;
              objArray[11] = (object) "";
              objArray[12] = (object) "";
              objArray[13] = (object) "";
              objArray[14] = (object) quote.quoteId;
              objArray[15] = (object) "";
              int index1 = rows.Add(objArray);
              this.dataGridView1.Rows[index1].Tag = (object) quote;
              this.dataGridView1.Rows[index1].DefaultCellStyle.BackColor = Color.Bisque;
              for (int index2 = 0; index2 < this.dataGridView1.ColumnCount; ++index2)
              {
                if (this.dataGridView1.Columns[index2].HeaderText == "Website Order ID")
                {
                  string str14 = "No line Items";
                  if (quote.lineItems != null)
                  {
                    num2 = quote.lineItems.GetLength(0);
                    str14 = string.Format("{0} Item(s).", (object) num2.ToString()) + "\r\n itemId : sku/svc : qty : brand";
                    foreach (QLineitem lineItem in quote.lineItems)
                    {
                      string[] strArray2 = new string[10];
                      strArray2[0] = str14;
                      strArray2[1] = "\r\n";
                      nullable = lineItem.purchasableId;
                      strArray2[2] = nullable.ToString();
                      strArray2[3] = " : ";
                      strArray2[4] = lineItem.sku;
                      strArray2[5] = lineItem.serviceChargeId;
                      strArray2[6] = " : ";
                      num2 = lineItem.qty;
                      strArray2[7] = num2.ToString();
                      strArray2[8] = " : ";
                      strArray2[9] = lineItem.location;
                      str14 = string.Concat(strArray2);
                    }
                  }
                  this.dataGridView1.Rows[index1].Cells[index2].ToolTipText = str14;
                }
                else if (this.dataGridView1.Columns[index2].HeaderText == "Visual Order ID")
                {
                  string str15 = string.Empty;
                  foreach (ExternalReference.Reference reference in referenceList1)
                    str15 = str15 + reference.ID + "\r\n";
                  this.dataGridView1.Rows[index1].Cells[index2].ToolTipText = str15;
                }
                else
                  this.dataGridView1.Rows[index1].Cells[index2].ToolTipText = Convert.ToString(this.dataGridView1.Rows[index1].Cells[index2].Value);
              }
              if (id != "" || ExternalID2 != null && ExternalID2.EndsWith("_Guest]"))
                this.dataGridView1.Rows[index1].Cells["VisualCustomerID"].ReadOnly = true;
            }
          }
          foreach (Order order in orderArray)
          {
            string id = "";
            string str16 = "";
            string str17 = "";
            string str18 = "";
            if (((this.comboBoxStore.Text != "" ? 1 : 0) | 1) != 0)
            {
              bool flag = false;
              if (order.lineItems != null)
              {
                foreach (Lineitem lineItem in order.lineItems)
                {
                  if (str18 == "")
                    str18 = Alias.Get(lineItem.location ?? "");
                  if (Alias.Get(lineItem.location ?? "") != Alias.Get(lineItem.location ?? "", true))
                  {
                    if (Alias.Get(lineItem.location ?? "") + "/" + Alias.Get(lineItem.location ?? "", true) == this.comboBoxStore.Text)
                    {
                      flag = true;
                      break;
                    }
                  }
                  else if (Alias.Get(lineItem.location ?? "") == this.comboBoxStore.Text)
                  {
                    flag = true;
                    break;
                  }
                }
              }
              if (this.comboBoxStore.Text != "" && !flag)
                continue;
            }
            ExternalReferenceService referenceService = this.extRefSvc(str18);
            num2 = order.id;
            string ExternalID3 = num2.ToString();
            List<ExternalReference.Reference> referenceList3 = referenceService.ExternalReferenceLookup("N", "WebOrder", ExternalID3, "0", "CustomerOrderHeader", (string) null, "0");
            if (referenceList3.Count >= 1)
              str17 = referenceList3[0].ID;
            string str19 = string.Empty;
            string str20 = "";
            bool flag3 = false;
            if (order.lineItems != null)
            {
              foreach (Lineitem lineItem in order.lineItems)
              {
                if (string.IsNullOrEmpty(lineItem.location))
                {
                  str19 = str19 + "\r\n" + lineItem.sku;
                  if (!stringList.Contains(lineItem.sku))
                    stringList.Add(lineItem.sku);
                  if (lineItem.sku.EndsWith("CS"))
                  {
                    lineItem.location = "Century Spring";
                  }
                  else
                  {
                    string sku = lineItem.sku;
                    num2 = order.id;
                    string str21 = num2.ToString();
                    int num6 = (int) MBox.Show("No Location specified for SKU: " + sku + " on order: " + str21);
                  }
                }
                if (string.IsNullOrEmpty(str20))
                {
                  if (!string.IsNullOrEmpty(lineItem.location))
                    str20 = Alias.Get(lineItem.location);
                }
                else if (!string.IsNullOrEmpty(lineItem.location) && str20 != Alias.Get(lineItem.location))
                  flag3 = true;
              }
            }
            string ExternalID4 = "NOMATCHCUSTOMERID!@#$";
            if (string.IsNullOrEmpty(order.customer.userId))
            {
              if (flag3 && order.status == "new")
              {
                string str22 = str20;
                num2 = order.id;
                string str23 = num2.ToString();
                int num7 = (int) MBox.Show("Multiple sites on a Guest Order. Using site: " + str22 + " For Order: " + str23);
              }
              if (string.IsNullOrEmpty(str20))
              {
                string[] strArray = new string[5]
                {
                  "No Site found on Guest Order: ",
                  null,
                  null,
                  null,
                  null
                };
                num2 = order.id;
                strArray[1] = num2.ToString();
                strArray[2] = " ";
                strArray[3] = order.reference;
                strArray[4] = str19;
                int num8 = (int) MBox.Show(string.Concat(strArray));
              }
              else
                ExternalID4 = "[" + str20 + "_Guest]";
            }
            else
              ExternalID4 = order.customerId;
            List<ExternalReference.Reference> referenceList4 = this.extRefSvc(str18).ExternalReferenceLookup("N", "WebCustomer", ExternalID4, "0", "Customer", (string) null, "0");
            if (referenceList4.Count >= 1)
            {
              id = referenceList4[0].ID;
              str16 = this.custSvc(str18).LookupNameByID(id);
            }
            if (this.comboBoxVisualConnect.SelectedIndex >= 0)
            {
              Shippingaddress shippingaddress = order.shippingAddress ?? new Shippingaddress();
              Adjustment adjustment1 = ((IEnumerable<Adjustment>) order.adjustments).Where<Adjustment>((Func<Adjustment, bool>) (x => x.type == "shipping")).FirstOrDefault<Adjustment>() ?? new Adjustment();
              Adjustment adjustment2 = ((IEnumerable<Adjustment>) order.adjustments).Where<Adjustment>((Func<Adjustment, bool>) (x => x.type == "tax")).FirstOrDefault<Adjustment>() ?? new Adjustment();
              Adjustment adjustment3 = ((IEnumerable<Adjustment>) order.adjustments).Where<Adjustment>((Func<Adjustment, bool>) (x => x.type == "small-order")).FirstOrDefault<Adjustment>() ?? new Adjustment();
              if (string.IsNullOrEmpty(this.comboBoxVisualConnect.SelectedItem.ToString()) || this.comboBoxVisualConnect.SelectedItem.ToString() == "Customer" && id != "" || this.comboBoxVisualConnect.SelectedItem.ToString() == "Customer Only" && str17 == "" && id != "" || this.comboBoxVisualConnect.SelectedItem.ToString() == "Order" && str17 != "" || this.comboBoxVisualConnect.SelectedItem.ToString() == "No Order" && str17 == "" || this.comboBoxVisualConnect.SelectedItem.ToString() == "Neither" && str17 == "" && id == "")
              {
                DataGridViewRowCollection rows = this.dataGridView1.Rows;
                object[] objArray = new object[16];
                objArray[0] = (object) str18;
                string[] strArray3 = new string[6];
                num2 = order.id;
                strArray3[0] = num2.ToString();
                strArray3[1] = " (";
                strArray3[2] = order.reference;
                strArray3[3] = ") ";
                strArray3[4] = OrderPrefixMap.Get(order.type);
                strArray3[5] = flag3 ? " ***" : "";
                objArray[1] = (object) string.Concat(strArray3);
                objArray[2] = (object) str17;
                objArray[3] = (object) order.dateOrdered.date.Substring(0, 19);
                objArray[4] = string.IsNullOrEmpty(order.customer.userId) ? (object) ExternalID4 : (object) order.customerId;
                objArray[5] = (object) id;
                objArray[6] = (object) ((order.customer?.user?.firstName ?? order.billingAddress?.firstName) + " " + (order.customer?.user?.lastName ?? order.billingAddress?.lastName) + (order.customer?.user?.visualId == null || !this.Debug ? "" : " {" + order.customer?.user?.visualId + "}"));
                objArray[7] = (object) str16;
                objArray[8] = (object) order.email;
                objArray[9] = (object) shippingaddress.phone;
                objArray[10] = (object) order.status;
                objArray[11] = (object) (order.lastTransaction?.paymentAmountAsCurrency ?? "No CC Info");
                objArray[12] = Convert.ToDecimal(order.totalTax) == 0M ? (object) "" : (object) "Taxed";
                objArray[13] = (object) (order.shippingAddress?.id.ToString() + " " + order.shippingMethodName);
                objArray[14] = (object) order.id;
                Decimal num9 = order.total;
                objArray[15] = (object) (num9.ToString() + " : " + order.couponCode);
                int index3 = rows.Add(objArray);
                this.dataGridView1.Rows[index3].Tag = (object) order;
                if (!order.customer?.user?.termsCustomer.GetValueOrDefault() && order.paymentMethod != "creditCard")
                {
                  this.dataGridView1.Rows[index3].DefaultCellStyle.BackColor = Color.Red;
                  this.dataGridView1.Rows[index3].DefaultCellStyle.ForeColor = Color.White;
                }
                for (int index4 = 0; index4 < this.dataGridView1.ColumnCount; ++index4)
                {
                  if (this.dataGridView1.Columns[index4].HeaderText == "Shipping")
                  {
                    string str24 = "";
                    string str25 = "" + " \r\n" + shippingaddress.id + " \r\n" + shippingaddress.firstName + " \r\n" + shippingaddress.lastName;
                    num9 = order.totalShippingCost;
                    string str26 = num9.ToString();
                    string str27 = str25 + " \r\n" + str26 + " \r\n" + order.shippingMethodName + " \r\n" + order.shippingAccountNumber + " \r\n" + order.deliveryInstructions;
                    this.dataGridView1.Rows[index3].Cells[index4].ToolTipText = str27 + "\r\n" + str24;
                  }
                  else if (this.dataGridView1.Columns[index4].HeaderText == "Website Order ID")
                  {
                    string str28 = "No line Items";
                    if (order.lineItems != null)
                    {
                      num2 = order.lineItems.GetLength(0);
                      str28 = string.Format("{0} Item(s).", (object) num2.ToString()) + "\r\n itemId : desc : price : sku : qty : brand";
                      foreach (Lineitem lineItem in order.lineItems)
                      {
                        string[] strArray4 = new string[11];
                        strArray4[0] = str28;
                        strArray4[1] = "\r\n";
                        strArray4[2] = lineItem.purchasableId;
                        strArray4[3] = " : ";
                        num9 = lineItem.price;
                        strArray4[4] = num9.ToString();
                        strArray4[5] = " : ";
                        strArray4[6] = lineItem.sku;
                        strArray4[7] = " : ";
                        num9 = lineItem.qty;
                        strArray4[8] = num9.ToString();
                        strArray4[9] = " : ";
                        strArray4[10] = lineItem.location;
                        str28 = string.Concat(strArray4);
                      }
                    }
                    string str29 = str28 + "\r\n\r\n Adjustments" + "\r\n Small Order Fee : " + adjustment3.amountAsCurrency + "\r\n Tax : " + adjustment2.amountAsCurrency + "\r\n Shipping : " + adjustment1.name + " : " + adjustment1.amountAsCurrency;
                    this.dataGridView1.Rows[index3].Cells[index4].ToolTipText = str29;
                  }
                  else if (this.dataGridView1.Columns[index4].HeaderText == "Visual Order ID")
                  {
                    string str30 = string.Empty;
                    foreach (ExternalReference.Reference reference in referenceList3)
                      str30 = str30 + reference.ID + "\r\n";
                    this.dataGridView1.Rows[index3].Cells[index4].ToolTipText = str30;
                  }
                  else if (this.dataGridView1.Columns[index4].HeaderText == "Payment Auth")
                  {
                    string str31 = "No CC Info";
                    if (order.lastTransaction != null)
                    {
                      string str32 = "method: " + order.paymentMethod + "\r\n Id : " + order.lastTransaction.id + "\r\n orderId : " + order.lastTransaction.orderId + "\r\n parentId : " + order.lastTransaction.parentId + "\r\n userId : " + order.lastTransaction.userId + "\r\n hash : " + order.lastTransaction.hash + "\r\n gatewayId : " + order.lastTransaction.gatewayId + "\r\n currency : " + order.lastTransaction.currency;
                      num9 = order.lastTransaction.paymentAmount;
                      string str33 = num9.ToString();
                      string str34 = str32 + "\r\n paymentAmount : " + str33 + "\r\n paymentCurrency : " + order.lastTransaction.paymentCurrency + "\r\n paymentRate : " + order.lastTransaction.paymentRate + "\r\n type : " + order.lastTransaction.type;
                      num9 = order.lastTransaction.amount;
                      string str35 = num9.ToString();
                      string str36 = str34 + "\r\n amount : " + str35 + "\r\n status : " + order.lastTransaction.status + "\r\n reference : " + order.lastTransaction.reference + "\r\n code : " + order.lastTransaction.code + "\r\n message : " + order.lastTransaction.message + "\r\n note : " + order.lastTransaction.note;
                      dateTime = order.lastTransaction.dateCreated;
                      string shortDateString1 = dateTime.ToShortDateString();
                      string str37 = str36 + "\r\n dateCreated : " + shortDateString1;
                      dateTime = order.lastTransaction.dateUpdated;
                      string shortDateString2 = dateTime.ToShortDateString();
                      str31 = str37 + "\r\n dateUpdated : " + shortDateString2 + "\r\n amountAsCurrency : " + order.lastTransaction.amountAsCurrency + "\r\n paymentAmountAsCurrency : " + order.lastTransaction.paymentAmountAsCurrency + "\r\n refundableAmountAsCurrency : " + order.lastTransaction.refundableAmountAsCurrency;
                    }
                    this.dataGridView1.Rows[index3].Cells[index4].ToolTipText = str31;
                  }
                  else
                    this.dataGridView1.Rows[index3].Cells[index4].ToolTipText = Convert.ToString(this.dataGridView1.Rows[index3].Cells[index4].Value);
                }
                if (id != "" || ExternalID4 != null && ExternalID4.EndsWith("_Guest]"))
                  this.dataGridView1.Rows[index3].Cells["VisualCustomerID"].ReadOnly = true;
                if (order.lastTransaction != null)
                  num1 += order.lastTransaction.amount;
              }
            }
          }
          if (this.Debug && stringList.Count > 0)
          {
            stringList.Sort();
            string text = string.Empty;
            int num10 = 1;
            foreach (string str38 in stringList)
            {
              string[] strArray = new string[5]
              {
                text,
                null,
                null,
                null,
                null
              };
              num2 = num10++;
              strArray[1] = num2.ToString();
              strArray[2] = ". ";
              strArray[3] = str38;
              strArray[4] = "\r\n";
              text = string.Concat(strArray);
            }
            int num11 = (int) MBox.Show(text, "Parts with no Location specified");
          }
          this.dataGridView1.Sort(this.dataGridView1.Columns["OrderDate"], ListSortDirection.Ascending);
          if (this.dataGridView1.Rows.Count > 0)
            this.dataGridView1.ClearSelection();
          Label labelTotalOrders1 = this.labelTotalOrders;
          num2 = this.dataGridView1.Rows.Count;
          string str39 = "Total Orders: " + num2.ToString();
          labelTotalOrders1.Text = str39;
          if (this.Debug)
          {
            Label labelTotalOrders2 = this.labelTotalOrders;
            labelTotalOrders2.Text = labelTotalOrders2.Text + "     Total Sold: " + num1.ToString("C");
          }
          Label labelSelectedOrders = this.labelSelectedOrders;
          num2 = this.dataGridView1.SelectedRows.Count;
          string str40 = "Selected Orders: " + num2.ToString();
          labelSelectedOrders.Text = str40;
        }
        catch (Exception ex)
        {
          int num12 = (int) MBox.Show(ex.ToString());
        }
      }
    }

    private bool isWindowVisible(Rectangle rect)
    {
      foreach (Screen allScreen in Screen.AllScreens)
      {
        if (allScreen.Bounds.IntersectsWith(rect))
          return true;
      }
      return false;
    }

    internal int findDB(string visualSite)
    {
      string str = SiteDBMap.Get(visualSite);
      if (str == Settings.Default.VMKey)
        return 1;
      return str == Settings.Default.VMKey2 ? 2 : 0;
    }

    internal ExternalReferenceService extRefSvc(string visualSite)
    {
      switch (this.findDB(visualSite))
      {
        case 1:
          return this.extRefSvc1;
        case 2:
          return this.extRefSvc2;
        default:
          return this.extRefSvc1;
      }
    }

    internal CustomerService custSvc(string location)
    {
      switch (this.findDB(location))
      {
        case 1:
          return this.custSvc1;
        case 2:
          return this.custSvc2;
        default:
          return this.custSvc1;
      }
    }

    internal SalesOrderService orderSvc(string location)
    {
      switch (this.findDB(location))
      {
        case 1:
          return this.orderSvc1;
        case 2:
          return this.orderSvc2;
        default:
          return this.orderSvc1;
      }
    }

    internal ContactService contactSvc(string location)
    {
      switch (this.findDB(location))
      {
        case 1:
          return this.contactSvc1;
        case 2:
          return this.contactSvc2;
        default:
          return this.contactSvc1;
      }
    }

    internal NotationService notationSvc(string location)
    {
      switch (this.findDB(location))
      {
        case 1:
          return this.notationSvc1;
        case 2:
          return this.notationSvc2;
        default:
          return this.notationSvc1;
      }
    }

    internal InventoryService inventorySvc(string location)
    {
      switch (this.findDB(location))
      {
        case 1:
          return this.inventorySvc1;
        case 2:
          return this.inventorySvc2;
        default:
          return this.inventorySvc1;
      }
    }

    internal EstimateService quoteSvc(string location)
    {
      switch (this.findDB(location))
      {
        case 1:
          return this.quoteSvc1;
        case 2:
          return this.quoteSvc2;
        default:
          return this.quoteSvc1;
      }
    }

    internal UserDefinedFieldService UDFSvc(string location)
    {
      switch (this.findDB(location))
      {
        case 1:
          return this.UDFSvc1;
        case 2:
          return this.UDFSvc2;
        default:
          return this.UDFSvc1;
      }
    }

    internal DatabaseConnection Database(string location)
    {
      switch (this.findDB(location))
      {
        case 1:
          return this.Database1;
        case 2:
          return this.Database2;
        default:
          return this.Database1;
      }
    }

    public string Version
    {
      get => this.labelVersion.Text;
      set
      {
        this.labelVersion.Text = value;
        Assembly executingAssembly = Assembly.GetExecutingAssembly();
        this.labelVersion.Text = this.labelVersion.Text + "/v." + executingAssembly.GetName().Version.Major.ToString() + "." + executingAssembly.GetName().Version.Minor.ToString();
      }
    }

    public bool Debug
    {
      get => this._Debug;
      set => this._Debug = value;
    }

    private void JobSelector_Load(object sender, EventArgs e)
    {
      if (this.Debug)
        this.labelServerURL.Text += " - DEBUG MODE ENABLED";
      this.Size = new Size(Settings.Default.SelectorWindowSize.Width, Settings.Default.SelectorWindowSize.Height);
      if (string.IsNullOrEmpty(this.header1.UserName))
        this.Text = this.Text + "   " + this.header1.Key;
      else
        this.Text = this.Text + "   " + this.header1.Key + "/" + (this.header1.UserName.EndsWith("#") ? this.header1.UserName.Substring(0, this.header1.UserName.Length - 1) : this.header1.UserName);
      if (string.IsNullOrEmpty(this.header2.UserName))
        this.Text = this.Text + "   " + this.header2.Key;
      else
        this.Text = this.Text + "   " + this.header2.Key + "/" + (this.header1.UserName.EndsWith("#") ? this.header2.UserName.Substring(0, this.header2.UserName.Length - 1) : this.header2.UserName);
      this.Size = Settings.Default.SelectorWindowSize;
      this.Location = Settings.Default.SelectorWindowLoc;
      this.WindowState = Settings.Default.SelectorWindowState;
      if (this.WindowState == FormWindowState.Minimized)
        this.WindowState = FormWindowState.Normal;
      if (!this.isWindowVisible(this.Bounds))
        this.Location = new Point(0, 0);
      this.extRefSvc0 = new ExternalReferenceService();
      this.extRefSvc0._Header = this.header0;
      this.custSvc0 = new CustomerService();
      this.custSvc0._Header = this.header0;
      this.orderSvc0 = new SalesOrderService();
      this.orderSvc0._Header = this.header0;
      this.contactSvc0 = new ContactService();
      this.contactSvc0._Header = this.header0;
      this.extRefSvc1 = new ExternalReferenceService();
      this.extRefSvc1._Header = this.header1;
      this.custSvc1 = new CustomerService();
      this.custSvc1._Header = this.header1;
      this.orderSvc1 = new SalesOrderService();
      this.orderSvc1._Header = this.header1;
      this.contactSvc1 = new ContactService();
      this.contactSvc1._Header = this.header1;
      this.notationSvc1 = new NotationService();
      this.notationSvc1._Header = this.header1;
      this.inventorySvc1 = new InventoryService();
      this.inventorySvc1._Header = this.header1;
      this.quoteSvc1 = new EstimateService();
      this.quoteSvc1._Header = this.header1;
      this.UDFSvc1 = new UserDefinedFieldService();
      this.UDFSvc1._Header = this.header1;
      this.extRefSvc2 = new ExternalReferenceService();
      this.extRefSvc2._Header = this.header2;
      this.custSvc2 = new CustomerService();
      this.custSvc2._Header = this.header2;
      this.orderSvc2 = new SalesOrderService();
      this.orderSvc2._Header = this.header2;
      this.contactSvc2 = new ContactService();
      this.contactSvc2._Header = this.header2;
      this.notationSvc2 = new NotationService();
      this.notationSvc2._Header = this.header2;
      this.inventorySvc2 = new InventoryService();
      this.inventorySvc2._Header = this.header2;
      this.quoteSvc2 = new EstimateService();
      this.quoteSvc2._Header = this.header2;
      this.UDFSvc2 = new UserDefinedFieldService();
      this.UDFSvc2._Header = this.header2;
      this.buttonPrev.Visible = false;
      this.buttonNext.Visible = false;
      this.comboBoxListChunk.Visible = false;
      PropertyInfo property = typeof (CustomerService).GetProperty("Database", BindingFlags.Instance | BindingFlags.NonPublic);
      this.Database0 = (DatabaseConnection) property.GetValue((object) this.custSvc0);
      this.PokeContactNoSchema((BaseDomainObject) new CUST_CONT_EML_DOC(this.Database0));
      this.PokeContactNoSchema((BaseDomainObject) new CUST_CONTACT(this.Database0));
      this.PokeContactNoSchema((BaseDomainObject) new CUSTOMER_CONTACT(this.Database0));
      this.PokeContactNoSchema((BaseDomainObject) new VEND_CONT_EML_DOC(this.Database0));
      this.PokeContactNoSchema((BaseDomainObject) new VEND_CONTACT(this.Database0));
      this.PokeContactNoSchema((BaseDomainObject) new VENDOR_CONTACT(this.Database0));
      this.Database1 = (DatabaseConnection) property.GetValue((object) this.custSvc1);
      this.PokeContactNoSchema((BaseDomainObject) new CUST_CONT_EML_DOC(this.Database1));
      this.PokeContactNoSchema((BaseDomainObject) new CUST_CONTACT(this.Database1));
      this.PokeContactNoSchema((BaseDomainObject) new CUSTOMER_CONTACT(this.Database1));
      this.PokeContactNoSchema((BaseDomainObject) new VEND_CONT_EML_DOC(this.Database1));
      this.PokeContactNoSchema((BaseDomainObject) new VEND_CONTACT(this.Database1));
      this.PokeContactNoSchema((BaseDomainObject) new VENDOR_CONTACT(this.Database1));
      this.Database2 = (DatabaseConnection) property.GetValue((object) this.custSvc2);
      this.PokeContactNoSchema((BaseDomainObject) new CUST_CONT_EML_DOC(this.Database2));
      this.PokeContactNoSchema((BaseDomainObject) new CUST_CONTACT(this.Database2));
      this.PokeContactNoSchema((BaseDomainObject) new CUSTOMER_CONTACT(this.Database2));
      this.PokeContactNoSchema((BaseDomainObject) new VEND_CONT_EML_DOC(this.Database2));
      this.PokeContactNoSchema((BaseDomainObject) new VEND_CONTACT(this.Database2));
      this.PokeContactNoSchema((BaseDomainObject) new VENDOR_CONTACT(this.Database2));
    }

    private void JobSelector_Shown(object sender, EventArgs e)
    {
      using (new HourGlass())
      {
        try
        {
          Application.DoEvents();
          this.comboBoxStore.Items.Clear();
          this.comboBoxStore.Items.Add((object) "");
          foreach (KeyValuePair<string, string> keyValuePair in Alias.aliasDictionary.ToList<KeyValuePair<string, string>>())
          {
            if (!this.comboBoxStore.Items.Contains((object) keyValuePair.Value))
              this.comboBoxStore.Items.Add((object) keyValuePair.Value);
          }
          this.comboBoxVisualConnect.SelectedIndex = 4;
          this.comboBoxOrderStatus.SelectedIndex = 0;
          this.comboBoxStore.SelectedIndex = 0;
          this.comboBoxTaxStatus.SelectedIndex = 0;
          this.listOffset = 0;
          this.dateTimePickerFrom.Value = DateTime.Today;
          this.dateTimePickerTo.Value = DateTime.UtcNow.Date;
          this.comboBoxListChunk.SelectedIndex = 0;
          if (this.comboBoxStore.Items.Count > 2)
          {
            this.comboBoxStore.Visible = true;
            this.label3.Visible = true;
          }
          this.comboBoxTaxStatus.Visible = true;
          this.label2.Visible = true;
          this.dataGridView1.Columns["TaxStatus"].Visible = true;
          this.ReadyToLoad = true;
          this.LoadGrid(true);
        }
        catch (Exception ex)
        {
          int num = (int) MBox.Show(ex.ToString());
        }
      }
    }

    private void JobSelector_FormClosing(object sender, FormClosingEventArgs e)
    {
      Settings.Default.SelectorWindowState = this.WindowState;
      if (this.WindowState == FormWindowState.Normal)
      {
        Settings.Default.SelectorWindowSize = this.Size;
        Settings.Default.SelectorWindowLoc = this.Location;
      }
      else
      {
        // ISSUE: variable of a compiler-generated type
        Settings settings1 = Settings.Default;
        Rectangle restoreBounds = this.RestoreBounds;
        Size size = restoreBounds.Size;
        settings1.SelectorWindowSize = size;
        // ISSUE: variable of a compiler-generated type
        Settings settings2 = Settings.Default;
        restoreBounds = this.RestoreBounds;
        Point location = restoreBounds.Location;
        settings2.SelectorWindowLoc = location;
      }
      Settings.Default.SelectorWindowAccount = this.comboBoxCustomer.Text;
      Settings.Default.Save();
    }

    private void dateTimePicker_CloseUp(object sender, EventArgs e)
    {
      this._isDroppedDown = false;
      this.dateTimePicker_ValueChanged(sender, e);
    }

    private void dateTimePicker_DropDown(object sender, EventArgs e) => this._isDroppedDown = true;

    private void dateTimePicker_ValueChanged(object sender, EventArgs e)
    {
      DateTimePicker dateTimePicker = (DateTimePicker) sender;
      if (this._isDroppedDown)
        return;
      if (!this.datePush)
      {
        if (DateTime.Compare(this.dateTimePickerFrom.Value, this.dateTimePickerTo.Value) > 0)
        {
          this.datePush = true;
          if (dateTimePicker.Name == "dateTimePickerFrom")
            this.dateTimePickerTo.Value = this.dateTimePickerFrom.Value;
          else
            this.dateTimePickerFrom.Value = this.dateTimePickerTo.Value;
        }
        this.LoadGrid(true);
      }
      else
        this.datePush = false;
    }

    private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      Color color = Color.White;
      QuoteImport quoteImport = new QuoteImport(this.craftUtil);
      quoteImport.debug = this.Debug;
      quoteImport.orderSelectForm = this;
      OrderImport orderImport = new OrderImport(this.craftUtil);
      orderImport.debug = this.Debug;
      orderImport.orderSelectForm = this;
      if (e.RowIndex < 0 || this.dataGridView1.Rows[e.RowIndex].HeaderCell.Value != null)
        return;
      if (this.dataGridView1.Rows[e.RowIndex].Tag.GetType().Name == "Quote")
      {
        string str = this.dataGridView1.Rows[e.RowIndex].Cells["Store"].Value.ToString();
        quoteImport.custSvc = this.custSvc(str);
        quoteImport.extRefSvc = this.extRefSvc(str);
        quoteImport.notationSvc = this.notationSvc(str);
        quoteImport.quoteSvc = this.quoteSvc(str);
        quoteImport.contactSvc = this.contactSvc(str);
        quoteImport.inventorySvc = this.inventorySvc(str);
        quoteImport.UDFSvc = this.UDFSvc(str);
        quoteImport.webQuote = (Quote) this.dataGridView1.Rows[e.RowIndex].Tag;
        if (quoteImport.webQuote.stage == "submitted")
        {
          quoteImport.quoteIncrement = this.dataGridView1.Rows[e.RowIndex].Cells["IncrementId"].Value.ToString();
          if (quoteImport.Import())
          {
            this.dataGridView1.Rows[e.RowIndex].Cells["VisualCustomerID"].Value = (object) quoteImport.VisualCustID;
            this.dataGridView1.Rows[e.RowIndex].Cells["VisualOrderID"].Value = (object) quoteImport.VisualQuoteID;
            if (!string.IsNullOrEmpty(quoteImport.VisualCustID))
              this.dataGridView1.Rows[e.RowIndex].Cells["VisualCustomerID"].ReadOnly = true;
            color = Color.White;
          }
          else
            color = Color.Red;
        }
        else
        {
          int num = (int) MBox.Show("Cannot import Quote unless it is in the \"submitted\" state.");
        }
      }
      else
      {
        string str = this.dataGridView1.Rows[e.RowIndex].Cells["Store"].Value.ToString();
        orderImport.custSvc = this.custSvc(str);
        orderImport.extRefSvc = this.extRefSvc(str);
        orderImport.notationSvc = this.notationSvc(str);
        orderImport.orderSvc = this.orderSvc(str);
        orderImport.contactSvc = this.contactSvc(str);
        orderImport.inventorySvc = this.inventorySvc(str);
        orderImport.webSO = (Order) this.dataGridView1.Rows[e.RowIndex].Tag;
        orderImport.orderIncrement = this.dataGridView1.Rows[e.RowIndex].Cells["IncrementId"].Value.ToString();
        orderImport.CouponID = this.dataGridView1.Rows[e.RowIndex].Cells["CouponId"].Value.ToString();
        if (orderImport.Import())
        {
          this.dataGridView1.Rows[e.RowIndex].Cells["VisualCustomerID"].Value = (object) orderImport.VisualCustID;
          this.dataGridView1.Rows[e.RowIndex].Cells["VisualOrderID"].Value = (object) orderImport.VisualOrderID;
          if (!string.IsNullOrEmpty(orderImport.VisualCustID))
            this.dataGridView1.Rows[e.RowIndex].Cells["VisualCustomerID"].ReadOnly = true;
          color = Color.White;
        }
        else
          color = Color.Red;
      }
      for (int index = 0; index < this.dataGridView1.Columns.Count; ++index)
      {
        this.dataGridView1.Rows[e.RowIndex].Cells[index].Style.ForeColor = color;
        this.dataGridView1.Rows[e.RowIndex].Cells[index].Style.BackColor = Color.LightGray;
      }
      this.dataGridView1.Rows[e.RowIndex].HeaderCell.Style.ForeColor = color;
      this.dataGridView1.Rows[e.RowIndex].HeaderCell.Style.BackColor = Color.LightGray;
      this.dataGridView1.EnableHeadersVisualStyles = false;
      this.dataGridView1.Rows[e.RowIndex].HeaderCell.Value = (object) "***";
    }

    private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e) => this.LoadGrid(true);

    private void comboBoxListChunk_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.listChunkSize = int.Parse(this.comboBoxListChunk.Text);
      this.LoadGrid(true);
    }

    private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
    {
      if (e.RowIndex < 0 || e.ColumnIndex < 0)
        return;
      DataGridViewCell cell1 = this.dataGridView1.Rows[e.RowIndex].Cells["VisualCustomerID"];
      DataGridViewCell cell2 = this.dataGridView1.Rows[e.RowIndex].Cells["CustomerID"];
      DataGridViewCell cell3 = this.dataGridView1.Rows[e.RowIndex].Cells["VisualOrderID"];
      DataGridViewCell cell4 = this.dataGridView1.Rows[e.RowIndex].Cells["OrderID"];
      Point client = this.dataGridView1.PointToClient(Cursor.Position);
      DataGridViewCell cell5 = (sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex];
      try
      {
        this.dataGridView1.CurrentCell = cell5;
      }
      catch
      {
        this.dataGridView1.CancelEdit();
      }
      if (e.Button == MouseButtons.Right && cell3.Value != null && !string.IsNullOrEmpty(cell3.Value.ToString()))
        this.contextMenuStripOrderID.Show((Control) this.dataGridView1, client);
      else if (e.Button == MouseButtons.Right && (cell3.Value == null || string.IsNullOrEmpty(cell3.Value.ToString())) && (e.ColumnIndex == cell3.ColumnIndex || e.ColumnIndex == cell4.ColumnIndex))
      {
        if (!cell5.IsInEditMode)
        {
          this.dataGridView1.CancelEdit();
          this.contextMenuStripAssocOrder.Show((Control) this.dataGridView1, client);
        }
      }
      else if (e.Button == MouseButtons.Right && e.ColumnIndex != cell3.ColumnIndex && (string.IsNullOrEmpty(cell1.Value?.ToString()) || !cell1.Value.ToString().EndsWith("_Guest")) && (string.IsNullOrEmpty(cell2.Value?.ToString()) || !cell2.Value.ToString().EndsWith("_Guest]")) && (string.IsNullOrEmpty(cell1.Value?.ToString()) || MBox.Show("Override Existing Customer Connection?", "Customer Selector", MessageBoxButtons.YesNo) == DialogResult.Yes) && !cell5.IsInEditMode)
      {
        cell1.Value = (object) null;
        this.dataGridView1.CancelEdit();
        this.contextMenuStripCustID.Show((Control) this.dataGridView1, client);
      }
    }

    private void associateOrderToolStripMenuItem_Click(object sender, EventArgs e)
    {
      int rowIndex = this.dataGridView1.CurrentCell.RowIndex;
      DataGridViewCell cell1 = this.dataGridView1.Rows[rowIndex].Cells["VisualOrderID"];
      DataGridViewCell cell2 = this.dataGridView1.Rows[rowIndex].Cells["VisualCustomerID"];
      if (cell1.Value != null && !string.IsNullOrEmpty(cell1.Value.ToString()))
        return;
      this.dataGridView1.CancelEdit();
      this.dataGridView1.CurrentCell = cell1;
      CustomerOrderSelector customerOrderSelector = new CustomerOrderSelector();
      if (string.IsNullOrWhiteSpace(this.dataGridView1.Rows[rowIndex].Cells["VisualCustomerID"]?.Value.ToString()))
      {
        int num = (int) MBox.Show("Customer ID is required to select an Order.");
      }
      else
      {
        customerOrderSelector.Customer_ID = this.dataGridView1.Rows[rowIndex].Cells["VisualCustomerID"].Value.ToString();
        DateTime date = DateTime.UtcNow.Date;
        DateTime dateTime = this.dateTimePickerFrom.Value;
        try
        {
          TimeZoneInfo systemTimeZoneById = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
          dateTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime, systemTimeZoneById).Date.AddDays(-1.0);
        }
        catch (TimeZoneNotFoundException ex)
        {
        }
        catch (InvalidTimeZoneException ex)
        {
        }
        catch (Exception ex)
        {
          Console.Out.WriteLine("Exception in date conversion: \r\n" + ex.ToString());
          dateTime = DateTime.UtcNow.Date;
        }
        customerOrderSelector.Date_Limit = dateTime;
        Header header = this.extRefSvc(this.dataGridView1.Rows[rowIndex].Cells["Store"]?.Value.ToString())._Header;
        if (customerOrderSelector.ShowDialog((IWin32Window) this, header) == DialogResult.OK)
        {
          cell1.Value = (object) customerOrderSelector.Order_ID;
          if (MBox.Show("Commit Customer Order Association?", "Customer Order Association", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
          {
            string ExternalID1 = this.dataGridView1.Rows[rowIndex].Cells["OrderId"].Value.ToString();
            int length = ExternalID1.IndexOf(" (");
            if (length > 0)
              ExternalID1 = ExternalID1.Substring(0, length);
            ExternalReferenceService referenceService = new ExternalReferenceService();
            referenceService._Header = header;
            if (referenceService.ExternalReferenceLookup("false", "WebOrder", ExternalID1, (string) null, (string) null, (string) null, (string) null).Count == 0)
              referenceService.ExternalReferenceUpdate(new List<ExternalReference.Reference>()
              {
                new ExternalReference.Reference()
                {
                  ExternalGroup = header.ExternalRefGroup,
                  ExternalType = "WebOrder",
                  ExternalID = ExternalID1,
                  ExternalLineNo = "0",
                  SourceType = "CustomerOrderHeader",
                  ID = customerOrderSelector.Order_ID,
                  LineNo = "0"
                }
              });
            string orderID = this.dataGridView1.Rows[rowIndex].Cells["IncrementID"].Value.ToString();
            Order order = this.craftUtil.OrderInfo(this.dataGridView1.Rows[rowIndex].Cells["IncrementID"].Value.ToString());
            if (!this.Debug)
              this.craftUtil.updateOrderStatus(orderID, order.status, "WebOrderImport created Visual Order manual association: " + customerOrderSelector.Order_ID + " for Visual Customer: " + customerOrderSelector.Customer_ID);
            if (!string.IsNullOrEmpty(order.customer.userId))
            {
              if (cell2.Value == null || string.IsNullOrEmpty(cell2.Value.ToString()))
              {
                string ExternalID2 = this.dataGridView1.Rows[rowIndex].Cells["CustomerID"].Value.ToString();
                if (referenceService.ExternalReferenceLookup("false", "WebCustomer", ExternalID2, (string) null, (string) null, (string) null, (string) null).Count == 0)
                {
                  cell2.Value = (object) customerOrderSelector.Customer_ID;
                  referenceService.ExternalReferenceUpdate(new List<ExternalReference.Reference>()
                  {
                    new ExternalReference.Reference()
                    {
                      ExternalGroup = header.ExternalRefGroup,
                      ExternalType = "WebCustomer",
                      ExternalID = this.dataGridView1.Rows[rowIndex].Cells["CustomerID"].Value.ToString(),
                      ExternalLineNo = "0",
                      SourceType = "Customer",
                      ID = customerOrderSelector.Customer_ID,
                      LineNo = "0"
                    }
                  });
                }
              }
              if (order.customer.userId != null && !this.Debug)
                this.craftUtil.CustomerUpdate(order.customer, customerOrderSelector.Customer_ID);
            }
          }
          else
            cell1.Value = (object) null;
        }
      }
    }

    private void toolStripMenuItemLookupID_Click(object sender, EventArgs e)
    {
      try
      {
        int rowIndex = this.dataGridView1.CurrentCell.RowIndex;
        DataGridViewCell cell1 = this.dataGridView1.Rows[rowIndex].Cells["VisualCustomerID"];
        if (cell1.Value == null || string.IsNullOrEmpty(cell1.Value.ToString()))
        {
          this.dataGridView1.CancelEdit();
          this.dataGridView1.CurrentCell = this.dataGridView1.Rows[rowIndex].Cells["VisualCustomerName"];
          string str = (string) null;
          DataGridViewCell cell2 = this.dataGridView1.Rows[rowIndex].Cells["CustomerID"];
          if (cell2.Value != null && !string.IsNullOrEmpty(cell2.Value.ToString()) && cell2.Value.ToString() != "0")
          {
            string[] strArray = new string[1]
            {
              "visual_customer_id"
            };
          }
          CustomerSelector customerSelector = new CustomerSelector();
          customerSelector.Customer_ID = str;
          customerSelector.Site_ID = this.comboBoxStore.Text;
          Header header = this.extRefSvc(this.dataGridView1.Rows[rowIndex].Cells["Store"]?.Value.ToString())._Header;
          if (customerSelector.ShowDialog((IWin32Window) this, header) != DialogResult.OK)
            return;
          cell1.Value = (object) customerSelector.Customer_ID;
          this.dataGridView1_CellEndEdit(sender, new DataGridViewCellEventArgs(cell1.ColumnIndex, cell1.RowIndex));
        }
        else
        {
          int num = (int) MBox.Show("Customer ID is already set.");
        }
      }
      catch (Exception ex)
      {
        int num = (int) MBox.Show(ex.ToString());
      }
    }

    private void toolStripMenuItemEditID_Click(object sender, EventArgs e)
    {
      DataGridViewCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells["VisualCustomerID"];
      if (cell.Value == null || string.IsNullOrEmpty(cell.Value.ToString()))
      {
        cell.ReadOnly = false;
        this.dataGridView1.CurrentCell = cell;
        this.dataGridView1.BeginEdit(true);
      }
      else
      {
        int num = (int) MBox.Show("Customer ID is already set.");
      }
    }

    private void toolStripMenuItemCustomerOrderEntry_Click(object sender, EventArgs e)
    {
      int rowIndex = this.dataGridView1.CurrentCell.RowIndex;
      DataGridViewCell cell = this.dataGridView1.Rows[rowIndex].Cells["VisualOrderID"];
      if (cell.Value == null || string.IsNullOrEmpty(cell.Value.ToString()))
        return;
      this.dataGridView1.CurrentCell = cell;
      string path = Path.GetTempPath() + Guid.NewGuid().ToString() + ".vmx";
      if (this.dataGridView1.Rows[rowIndex].Tag.GetType().Name == "Quote")
        System.IO.File.WriteAllText(path, "LSASTART\r\nVMESTWIN~" + cell.Value.ToString().Replace("QUOTE: ", "") + "\r\n");
      else
        System.IO.File.WriteAllText(path, "LSASTART\r\nVMORDENT~" + cell.Value?.ToString() + "\r\n");
      Process process = new Process();
      if (string.IsNullOrEmpty(Settings.Default.VM_EXE_Path))
      {
        process.StartInfo.FileName = path;
        process.StartInfo.UseShellExecute = true;
      }
      else
      {
        process.StartInfo.FileName = Settings.Default.VM_EXE_Path;
        process.StartInfo.Arguments = path;
      }
      process.Start();
      process.WaitForExit(7000);
      System.IO.File.Delete(path);
    }

    private void toolStripMenuItemCraftOrderDetails_Click(object sender, EventArgs e)
    {
      DataGridViewCell cell = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells["OrderID"];
      if (cell.Value == null || string.IsNullOrEmpty(cell.Value.ToString()))
        return;
      this.dataGridView1.CurrentCell = cell;
      char[] chArray = new char[1]{ ' ' };
      string absoluteUri = new Uri(new Uri(Settings.Default.EComSite_AdminURL), "commerce/orders/" + cell.Value.ToString().Split(chArray)[0] + "#orderDetailsTab").AbsoluteUri;
      try
      {
        Process.Start("chrome", absoluteUri);
      }
      catch
      {
        try
        {
          Process.Start("firefox", absoluteUri);
        }
        catch
        {
          try
          {
            Process.Start("microsoft-edge:" + absoluteUri);
          }
          catch
          {
            try
            {
              Process.Start(absoluteUri);
            }
            catch
            {
              int num = (int) MBox.Show("Can't launch browser on " + absoluteUri);
            }
          }
        }
      }
    }

    private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
      int rowIndex = e.RowIndex;
      DataGridViewCell cell = this.dataGridView1.Rows[rowIndex].Cells["VisualCustomerID"];
      string location = this.dataGridView1.Rows[rowIndex].Cells["Store"].Value.ToString();
      if (cell.Value == null || string.IsNullOrEmpty(cell.Value.ToString()))
      {
        cell.ErrorText = string.Empty;
        this.dataGridView1.Rows[rowIndex].Cells["VisualCustomerName"].Value = (object) string.Empty;
      }
      if (cell.ErrorText != string.Empty)
        this.dataGridView1.CurrentCell = cell;
      else if (cell.Value != null && !string.IsNullOrEmpty(cell.Value.ToString()))
      {
        if (cell.Value.ToString().ToUpper().EndsWith("_GUEST"))
        {
          int num = (int) MBox.Show("Cannot associate Guest account " + cell.Value.ToString() + " to a Website Customer ID!");
          cell.Value = (object) string.Empty;
          this.dataGridView1.Rows[rowIndex].Cells["VisualCustomerName"].Value = (object) string.Empty;
          this.dataGridView1.CancelEdit();
          this.dataGridView1.CurrentCell = this.dataGridView1.Rows[rowIndex].Cells["VisualCustomerName"];
        }
        else
        {
          CustomerData data = new CustomerData();
          data.Customers = new List<Synergy.DistributedERP.Customer>();
          Synergy.DistributedERP.Customer customer = new Synergy.DistributedERP.Customer(cell.Value.ToString());
          data.Customers.Add(customer);
          CustomerData customerData = this.custSvc(location).SearchCustomer(data);
          if (customerData.Customers.Count > 0)
          {
            cell.ErrorText = string.Empty;
            this.dataGridView1.Rows[rowIndex].Cells["VisualCustomerName"].Value = (object) customerData.Customers[0].CustomerName;
            if (MBox.Show("Commit Customer Association?", "Customer Association", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
              cell.ReadOnly = true;
              data.Customers[0].ReferenceList.Add(new ExternalReference.Reference()
              {
                ExternalType = "WebCustomer",
                ExternalID = this.dataGridView1.Rows[rowIndex].Cells["CustomerID"].Value.ToString(),
                ExternalLineNo = "0"
              });
              this.custSvc(location).CreateCustomer(data);
              Order tag = (Order) this.dataGridView1.Rows[rowIndex].Tag;
              if (tag.customer.userId != null && !this.Debug)
                this.craftUtil.CustomerUpdate(tag.customer, data.Customers[0].CustomerID);
            }
            else
            {
              cell.Value = (object) string.Empty;
              this.dataGridView1.Rows[rowIndex].Cells["VisualCustomerName"].Value = (object) string.Empty;
              this.dataGridView1.CancelEdit();
              this.dataGridView1.CurrentCell = this.dataGridView1.Rows[rowIndex].Cells["VisualCustomerName"];
            }
          }
          else
          {
            int num = (int) MBox.Show("Customer ID does not exist: " + cell.Value.ToString());
            this.dataGridView1.CurrentCell = cell;
            cell.ErrorText = "Customer ID does not exist: " + cell.Value.ToString();
          }
        }
      }
    }

    private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
    {
      int rowIndex = e.RowIndex;
      DataGridViewCell cell = this.dataGridView1.Rows[rowIndex].Cells["VisualCustomerID"];
      string location = this.dataGridView1.Rows[rowIndex].Cells["Store"].Value.ToString();
      if (rowIndex <= 0)
        return;
      cell.ErrorText = string.Empty;
      if (cell.EditedFormattedValue != null && !string.IsNullOrEmpty(cell.EditedFormattedValue.ToString()) && this.custSvc(location).GetCustomerList((string) null, "Y", 0, 0, (string) null, cell.EditedFormattedValue.ToString(), (string) null, (string) null, (string) null, "Y", (string) null, (string) null).CustomerCount == 0)
      {
        if (MBox.Show("Customer ID does not exist: " + cell.EditedFormattedValue.ToString() + "\n\n Clear the ID?", this.Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
          this.dataGridView1.CancelEdit();
          cell.Value = (object) null;
        }
        else
        {
          cell.ErrorText = "Customer ID does not exist: " + cell.EditedFormattedValue.ToString();
          this.dataGridView1.CurrentCell = cell;
          this.dataGridView1.BeginEdit(true);
        }
        e.Cancel = true;
      }
    }

    private void dataGridView1_SelectionChanged(object sender, EventArgs e) => this.labelSelectedOrders.Text = "Selected Orders: " + this.dataGridView1.SelectedRows.Count.ToString();

    private void buttonOK_Click(object sender, EventArgs e)
    {
      if (this.dataGridView1.RowCount <= 0)
        return;
      foreach (DataGridViewRow row in (IEnumerable) this.dataGridView1.Rows)
      {
        if (row.Selected && row.HeaderCell.Value == null)
          this.dataGridView1_CellDoubleClick(sender, new DataGridViewCellEventArgs(0, row.Index));
      }
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void buttonPrev_Click(object sender, EventArgs e)
    {
      if (sender == this.buttonPrev)
        this.buttonNext.Enabled = true;
      this.listOffset -= this.listChunkSize;
      if (this.listOffset < 0)
        this.listOffset = 0;
      this.LoadGrid(false);
      if (this.listOffset != 0)
        return;
      this.buttonPrev.Enabled = false;
    }

    private void buttonNext_Click(object sender, EventArgs e)
    {
      this.buttonPrev.Enabled = true;
      this.listOffset += this.listChunkSize;
      this.LoadGrid(false);
      if (this.dataGridView1.Rows.Count < this.listChunkSize)
        this.buttonNext.Enabled = false;
      if (this.dataGridView1.Rows.Count != 0)
        return;
      this.buttonPrev_Click(sender, e);
    }

    private void buttonRefresh_Click(object sender, EventArgs e) => this.LoadGrid(true);

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new System.ComponentModel.Container();
      DataGridViewCellStyle gridViewCellStyle1 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle2 = new DataGridViewCellStyle();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (OrderSelectForm));
      this.comboBoxVisualConnect = new ComboBox();
      this.comboBoxOrderStatus = new ComboBox();
      this.dateTimePickerTo = new DateTimePicker();
      this.dateTimePickerFrom = new DateTimePicker();
      this.comboBoxCustomer = new ComboBox();
      this.label7 = new Label();
      this.label6 = new Label();
      this.label4 = new Label();
      this.label1 = new Label();
      this.dataGridView1 = new DataGridView();
      this.Store = new DataGridViewTextBoxColumn();
      this.OrderID = new DataGridViewTextBoxColumn();
      this.VisualOrderID = new DataGridViewTextBoxColumn();
      this.OrderDate = new DataGridViewTextBoxColumn();
      this.CustomerID = new DataGridViewTextBoxColumn();
      this.VisualCustomerID = new DataGridViewTextBoxColumn();
      this.CustomerName = new DataGridViewTextBoxColumn();
      this.VisualCustomerName = new DataGridViewTextBoxColumn();
      this.EmailAddress = new DataGridViewTextBoxColumn();
      this.PhoneNo = new DataGridViewTextBoxColumn();
      this.OrderStatus = new DataGridViewTextBoxColumn();
      this.PaymentAuth = new DataGridViewTextBoxColumn();
      this.TaxStatus = new DataGridViewTextBoxColumn();
      this.Shipping = new DataGridViewTextBoxColumn();
      this.IncrementId = new DataGridViewTextBoxColumn();
      this.CouponID = new DataGridViewTextBoxColumn();
      this.labelServerURL = new Label();
      this.labelVersion = new Label();
      this.buttonCancel = new Button();
      this.buttonOK = new Button();
      this.contextMenuStripCustID = new ContextMenuStrip(this.components);
      this.toolStripMenuItemLookupID = new ToolStripMenuItem();
      this.toolStripMenuItemEditID = new ToolStripMenuItem();
      this.contextMenuStripOrderID = new ContextMenuStrip(this.components);
      this.openCustomerOrderToolStripMenuItem = new ToolStripMenuItem();
      this.craftOrderDetailToolStripMenuItem = new ToolStripMenuItem();
      this.buttonRefresh = new Button();
      this.comboBoxListChunk = new ComboBox();
      this.buttonNext = new Button();
      this.buttonPrev = new Button();
      this.comboBoxStore = new ComboBox();
      this.comboBoxTaxStatus = new ComboBox();
      this.label2 = new Label();
      this.label3 = new Label();
      this.labelTotalOrders = new Label();
      this.labelSelectedOrders = new Label();
      this.contextMenuStripAssocOrder = new ContextMenuStrip(this.components);
      this.associateOrderToolStripMenuItem = new ToolStripMenuItem();
      this.craftOrderDetailToolStripMenuItem2 = new ToolStripMenuItem();
      this.chkBox_Incomplete = new CheckBox();
      this.pictureBox1 = new PictureBox();
      this.panelDownLoad = new Panel();
      this.labelDownLoading = new Label();
      this.labelFName = new Label();
      ((ISupportInitialize) this.dataGridView1).BeginInit();
      this.contextMenuStripCustID.SuspendLayout();
      this.contextMenuStripOrderID.SuspendLayout();
      this.contextMenuStripAssocOrder.SuspendLayout();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.panelDownLoad.SuspendLayout();
      this.SuspendLayout();
      this.comboBoxVisualConnect.FormattingEnabled = true;
      this.comboBoxVisualConnect.Items.AddRange(new object[7]
      {
        (object) "",
        (object) "Customer",
        (object) "Customer Only",
        (object) "Order",
        (object) "No Order",
        (object) "Neither",
        (object) "Quotes"
      });
      this.comboBoxVisualConnect.Location = new Point(431, 36);
      this.comboBoxVisualConnect.Name = "comboBoxVisualConnect";
      this.comboBoxVisualConnect.Size = new Size(106, 21);
      this.comboBoxVisualConnect.TabIndex = 27;
      this.comboBoxVisualConnect.SelectedIndexChanged += new EventHandler(this.comboBoxType_SelectedIndexChanged);
      this.comboBoxOrderStatus.FormattingEnabled = true;
      this.comboBoxOrderStatus.Items.AddRange(new object[10]
      {
        (object) "",
        (object) "Pending",
        (object) "Pending Payment",
        (object) "Processing",
        (object) "Complete",
        (object) "Canceled",
        (object) "Closed",
        (object) "On hold",
        (object) "New",
        (object) "Done"
      });
      this.comboBoxOrderStatus.Location = new Point(694, 36);
      this.comboBoxOrderStatus.Name = "comboBoxOrderStatus";
      this.comboBoxOrderStatus.Size = new Size(106, 21);
      this.comboBoxOrderStatus.TabIndex = 25;
      this.comboBoxOrderStatus.SelectedIndexChanged += new EventHandler(this.comboBoxType_SelectedIndexChanged);
      this.dateTimePickerTo.Format = DateTimePickerFormat.Short;
      this.dateTimePickerTo.Location = new Point(300, 37);
      this.dateTimePickerTo.Name = "dateTimePickerTo";
      this.dateTimePickerTo.Size = new Size(106, 20);
      this.dateTimePickerTo.TabIndex = 24;
      this.dateTimePickerTo.CloseUp += new EventHandler(this.dateTimePicker_CloseUp);
      this.dateTimePickerTo.ValueChanged += new EventHandler(this.dateTimePicker_ValueChanged);
      this.dateTimePickerTo.DropDown += new EventHandler(this.dateTimePicker_DropDown);
      this.dateTimePickerFrom.Format = DateTimePickerFormat.Short;
      this.dateTimePickerFrom.Location = new Point(169, 37);
      this.dateTimePickerFrom.Name = "dateTimePickerFrom";
      this.dateTimePickerFrom.Size = new Size(106, 20);
      this.dateTimePickerFrom.TabIndex = 23;
      this.dateTimePickerFrom.CloseUp += new EventHandler(this.dateTimePicker_CloseUp);
      this.dateTimePickerFrom.ValueChanged += new EventHandler(this.dateTimePicker_ValueChanged);
      this.dateTimePickerFrom.DropDown += new EventHandler(this.dateTimePicker_DropDown);
      this.comboBoxCustomer.FormattingEnabled = true;
      this.comboBoxCustomer.Items.AddRange(new object[2]
      {
        (object) "",
        (object) "Guest_Accounts"
      });
      this.comboBoxCustomer.Location = new Point(562, 36);
      this.comboBoxCustomer.Name = "comboBoxCustomer";
      this.comboBoxCustomer.Size = new Size(106, 21);
      this.comboBoxCustomer.TabIndex = 22;
      this.comboBoxCustomer.SelectedIndexChanged += new EventHandler(this.comboBoxType_SelectedIndexChanged);
      this.label7.AutoSize = true;
      this.label7.Location = new Point(435, 7);
      this.label7.Name = "label7";
      this.label7.Size = new Size(97, 13);
      this.label7.TabIndex = 33;
      this.label7.Text = "Visual Connections";
      this.label6.AutoSize = true;
      this.label6.Location = new Point((int) byte.MaxValue, 7);
      this.label6.Name = "label6";
      this.label6.Size = new Size(65, 13);
      this.label6.TabIndex = 32;
      this.label6.Text = "Date Range";
      this.label4.AutoSize = true;
      this.label4.Location = new Point(714, 7);
      this.label4.Name = "label4";
      this.label4.Size = new Size(66, 13);
      this.label4.TabIndex = 30;
      this.label4.Text = "Order Status";
      this.label1.AutoSize = true;
      this.label1.Location = new Point(572, 7);
      this.label1.Name = "label1";
      this.label1.Size = new Size(93, 13);
      this.label1.TabIndex = 28;
      this.label1.Text = "Website Customer";
      this.dataGridView1.AllowUserToAddRows = false;
      this.dataGridView1.AllowUserToDeleteRows = false;
      this.dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      gridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle1.BackColor = SystemColors.Control;
      gridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      gridViewCellStyle1.ForeColor = SystemColors.WindowText;
      gridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
      gridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle1.WrapMode = DataGridViewTriState.True;
      this.dataGridView1.ColumnHeadersDefaultCellStyle = gridViewCellStyle1;
      this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView1.Columns.AddRange((DataGridViewColumn) this.Store, (DataGridViewColumn) this.OrderID, (DataGridViewColumn) this.VisualOrderID, (DataGridViewColumn) this.OrderDate, (DataGridViewColumn) this.CustomerID, (DataGridViewColumn) this.VisualCustomerID, (DataGridViewColumn) this.CustomerName, (DataGridViewColumn) this.VisualCustomerName, (DataGridViewColumn) this.EmailAddress, (DataGridViewColumn) this.PhoneNo, (DataGridViewColumn) this.OrderStatus, (DataGridViewColumn) this.PaymentAuth, (DataGridViewColumn) this.TaxStatus, (DataGridViewColumn) this.Shipping, (DataGridViewColumn) this.IncrementId, (DataGridViewColumn) this.CouponID);
      gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle2.BackColor = SystemColors.Window;
      gridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      gridViewCellStyle2.ForeColor = SystemColors.ControlText;
      gridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
      gridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle2.WrapMode = DataGridViewTriState.False;
      this.dataGridView1.DefaultCellStyle = gridViewCellStyle2;
      this.dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
      this.dataGridView1.Location = new Point(34, 74);
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dataGridView1.Size = new Size(1165, 199);
      this.dataGridView1.TabIndex = 34;
      this.dataGridView1.CellDoubleClick += new DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
      this.dataGridView1.CellEndEdit += new DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
      this.dataGridView1.CellMouseDown += new DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
      this.dataGridView1.CellValidating += new DataGridViewCellValidatingEventHandler(this.dataGridView1_CellValidating);
      this.dataGridView1.SelectionChanged += new EventHandler(this.dataGridView1_SelectionChanged);
      this.Store.HeaderText = "Store";
      this.Store.Name = "Store";
      this.Store.ReadOnly = true;
      this.Store.Width = 75;
      this.OrderID.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.OrderID.HeaderText = "Website Order ID";
      this.OrderID.Name = "OrderID";
      this.OrderID.ReadOnly = true;
      this.OrderID.Width = 95;
      this.VisualOrderID.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.VisualOrderID.HeaderText = "Visual Order ID";
      this.VisualOrderID.Name = "VisualOrderID";
      this.VisualOrderID.ReadOnly = true;
      this.VisualOrderID.Width = 85;
      this.OrderDate.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.OrderDate.HeaderText = "Order Date";
      this.OrderDate.Name = "OrderDate";
      this.OrderDate.ReadOnly = true;
      this.OrderDate.Width = 78;
      this.CustomerID.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.CustomerID.HeaderText = "Website Customer ID";
      this.CustomerID.Name = "CustomerID";
      this.CustomerID.ReadOnly = true;
      this.CustomerID.Width = 111;
      this.VisualCustomerID.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.VisualCustomerID.HeaderText = "Visual Customer ID";
      this.VisualCustomerID.Name = "VisualCustomerID";
      this.VisualCustomerID.Width = 101;
      this.CustomerName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.CustomerName.HeaderText = "Website Customer Name";
      this.CustomerName.Name = "CustomerName";
      this.CustomerName.ReadOnly = true;
      this.VisualCustomerName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.VisualCustomerName.HeaderText = "Visual Customer Name";
      this.VisualCustomerName.Name = "VisualCustomerName";
      this.VisualCustomerName.ReadOnly = true;
      this.EmailAddress.HeaderText = "Email Address";
      this.EmailAddress.Name = "EmailAddress";
      this.EmailAddress.ReadOnly = true;
      this.PhoneNo.HeaderText = "Phone #";
      this.PhoneNo.Name = "PhoneNo";
      this.PhoneNo.ReadOnly = true;
      this.PhoneNo.Width = 75;
      this.OrderStatus.HeaderText = "Order Status";
      this.OrderStatus.Name = "OrderStatus";
      this.OrderStatus.ReadOnly = true;
      this.OrderStatus.Width = 75;
      this.PaymentAuth.HeaderText = "Payment Auth";
      this.PaymentAuth.Name = "PaymentAuth";
      this.PaymentAuth.ReadOnly = true;
      this.PaymentAuth.Width = 75;
      this.TaxStatus.HeaderText = "Tax Status";
      this.TaxStatus.Name = "TaxStatus";
      this.TaxStatus.ReadOnly = true;
      this.TaxStatus.Width = 75;
      this.Shipping.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
      this.Shipping.HeaderText = "Shipping";
      this.Shipping.Name = "Shipping";
      this.Shipping.ReadOnly = true;
      this.Shipping.Width = 5;
      this.IncrementId.HeaderText = "IncrementId ";
      this.IncrementId.Name = "IncrementId";
      this.IncrementId.ReadOnly = true;
      this.IncrementId.Visible = false;
      this.IncrementId.Width = 75;
      this.CouponID.HeaderText = "CouponID ";
      this.CouponID.Name = "CouponID";
      this.CouponID.ReadOnly = true;
      this.CouponID.Visible = false;
      this.CouponID.Width = 75;
      this.labelServerURL.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.labelServerURL.AutoSize = true;
      this.labelServerURL.Location = new Point(79, 315);
      this.labelServerURL.Name = "labelServerURL";
      this.labelServerURL.Size = new Size(60, 13);
      this.labelServerURL.TabIndex = 37;
      this.labelServerURL.Text = "ServerURL";
      this.labelVersion.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.labelVersion.AutoSize = true;
      this.labelVersion.Font = new Font("Segoe UI", 9f);
      this.labelVersion.ForeColor = SystemColors.ControlDark;
      this.labelVersion.Location = new Point(10, 313);
      this.labelVersion.Name = "labelVersion";
      this.labelVersion.Size = new Size(54, 15);
      this.labelVersion.TabIndex = 36;
      this.labelVersion.Text = "v0.0/v0.0";
      this.buttonCancel.Anchor = AnchorStyles.Bottom;
      this.buttonCancel.Location = new Point(613, 291);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new Size(75, 23);
      this.buttonCancel.TabIndex = 39;
      this.buttonCancel.Text = "Close";
      this.buttonCancel.UseVisualStyleBackColor = true;
      this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
      this.buttonOK.Anchor = AnchorStyles.Bottom;
      this.buttonOK.Location = new Point(517, 291);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.Size = new Size(75, 23);
      this.buttonOK.TabIndex = 38;
      this.buttonOK.Text = "Import";
      this.buttonOK.UseVisualStyleBackColor = true;
      this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
      this.contextMenuStripCustID.ImageScalingSize = new Size(20, 20);
      this.contextMenuStripCustID.Items.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.toolStripMenuItemLookupID,
        (ToolStripItem) this.toolStripMenuItemEditID
      });
      this.contextMenuStripCustID.Name = "contextMenuStripCustID";
      this.contextMenuStripCustID.Size = new Size(184, 48);
      this.toolStripMenuItemLookupID.Name = "toolStripMenuItemLookupID";
      this.toolStripMenuItemLookupID.Size = new Size(183, 22);
      this.toolStripMenuItemLookupID.Text = "Lookup Customer ID";
      this.toolStripMenuItemLookupID.Click += new EventHandler(this.toolStripMenuItemLookupID_Click);
      this.toolStripMenuItemEditID.Name = "toolStripMenuItemEditID";
      this.toolStripMenuItemEditID.Size = new Size(183, 22);
      this.toolStripMenuItemEditID.Text = "Edit Customer ID";
      this.toolStripMenuItemEditID.Click += new EventHandler(this.toolStripMenuItemEditID_Click);
      this.contextMenuStripOrderID.ImageScalingSize = new Size(20, 20);
      this.contextMenuStripOrderID.Items.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.openCustomerOrderToolStripMenuItem,
        (ToolStripItem) this.craftOrderDetailToolStripMenuItem
      });
      this.contextMenuStripOrderID.Name = "contextMenuStripOrderID";
      this.contextMenuStripOrderID.Size = new Size(235, 48);
      this.openCustomerOrderToolStripMenuItem.Name = "openCustomerOrderToolStripMenuItem";
      this.openCustomerOrderToolStripMenuItem.Size = new Size(234, 22);
      this.openCustomerOrderToolStripMenuItem.Text = "Cust Order Entry / Est Window";
      this.openCustomerOrderToolStripMenuItem.Click += new EventHandler(this.toolStripMenuItemCustomerOrderEntry_Click);
      this.craftOrderDetailToolStripMenuItem.Name = "craftOrderDetailToolStripMenuItem";
      this.craftOrderDetailToolStripMenuItem.Size = new Size(234, 22);
      this.craftOrderDetailToolStripMenuItem.Text = "Craft Order Detail";
      this.craftOrderDetailToolStripMenuItem.Click += new EventHandler(this.toolStripMenuItemCraftOrderDetails_Click);
      this.buttonRefresh.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.buttonRefresh.Location = new Point(1118, 291);
      this.buttonRefresh.Name = "buttonRefresh";
      this.buttonRefresh.Size = new Size(75, 23);
      this.buttonRefresh.TabIndex = 43;
      this.buttonRefresh.Text = "Refresh";
      this.buttonRefresh.UseVisualStyleBackColor = true;
      this.buttonRefresh.Click += new EventHandler(this.buttonRefresh_Click);
      this.comboBoxListChunk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.comboBoxListChunk.DisplayMember = "25";
      this.comboBoxListChunk.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBoxListChunk.FormattingEnabled = true;
      this.comboBoxListChunk.Items.AddRange(new object[3]
      {
        (object) "25",
        (object) "50",
        (object) "100"
      });
      this.comboBoxListChunk.Location = new Point(958, 293);
      this.comboBoxListChunk.Name = "comboBoxListChunk";
      this.comboBoxListChunk.Size = new Size(44, 21);
      this.comboBoxListChunk.TabIndex = 42;
      this.comboBoxListChunk.SelectedIndexChanged += new EventHandler(this.comboBoxListChunk_SelectedIndexChanged);
      this.buttonNext.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.buttonNext.Location = new Point(1022, 291);
      this.buttonNext.Name = "buttonNext";
      this.buttonNext.Size = new Size(75, 23);
      this.buttonNext.TabIndex = 41;
      this.buttonNext.Text = "Next ->";
      this.buttonNext.UseVisualStyleBackColor = true;
      this.buttonNext.Click += new EventHandler(this.buttonNext_Click);
      this.buttonPrev.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.buttonPrev.Enabled = false;
      this.buttonPrev.Location = new Point(862, 290);
      this.buttonPrev.Name = "buttonPrev";
      this.buttonPrev.Size = new Size(75, 23);
      this.buttonPrev.TabIndex = 40;
      this.buttonPrev.Text = "<- Prevoius";
      this.buttonPrev.UseVisualStyleBackColor = true;
      this.buttonPrev.Click += new EventHandler(this.buttonPrev_Click);
      this.comboBoxStore.FormattingEnabled = true;
      this.comboBoxStore.Location = new Point(46, 36);
      this.comboBoxStore.Name = "comboBoxStore";
      this.comboBoxStore.Size = new Size(106, 21);
      this.comboBoxStore.TabIndex = 44;
      this.comboBoxStore.SelectedIndexChanged += new EventHandler(this.comboBoxType_SelectedIndexChanged);
      this.comboBoxTaxStatus.FormattingEnabled = true;
      this.comboBoxTaxStatus.Items.AddRange(new object[3]
      {
        (object) "",
        (object) "Non-Taxed",
        (object) "Taxed"
      });
      this.comboBoxTaxStatus.Location = new Point(832, 36);
      this.comboBoxTaxStatus.Name = "comboBoxTaxStatus";
      this.comboBoxTaxStatus.Size = new Size(106, 21);
      this.comboBoxTaxStatus.TabIndex = 45;
      this.comboBoxTaxStatus.SelectedIndexChanged += new EventHandler(this.comboBoxType_SelectedIndexChanged);
      this.label2.AutoSize = true;
      this.label2.Location = new Point(847, 7);
      this.label2.Name = "label2";
      this.label2.Size = new Size(58, 13);
      this.label2.TabIndex = 46;
      this.label2.Text = "Tax Status";
      this.label3.AutoSize = true;
      this.label3.Location = new Point(90, 7);
      this.label3.Name = "label3";
      this.label3.Size = new Size(25, 13);
      this.label3.TabIndex = 47;
      this.label3.Text = "Site";
      this.labelTotalOrders.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.labelTotalOrders.AutoSize = true;
      this.labelTotalOrders.Location = new Point(10, 277);
      this.labelTotalOrders.Name = "labelTotalOrders";
      this.labelTotalOrders.Size = new Size(68, 13);
      this.labelTotalOrders.TabIndex = 48;
      this.labelTotalOrders.Text = "Total Orders:";
      this.labelSelectedOrders.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.labelSelectedOrders.AutoSize = true;
      this.labelSelectedOrders.Location = new Point(10, 295);
      this.labelSelectedOrders.Name = "labelSelectedOrders";
      this.labelSelectedOrders.Size = new Size(86, 13);
      this.labelSelectedOrders.TabIndex = 49;
      this.labelSelectedOrders.Text = "Selected Orders:";
      this.contextMenuStripAssocOrder.Items.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.associateOrderToolStripMenuItem,
        (ToolStripItem) this.craftOrderDetailToolStripMenuItem2
      });
      this.contextMenuStripAssocOrder.Name = "contextMenuStripAssocOrder";
      this.contextMenuStripAssocOrder.Size = new Size(167, 48);
      this.associateOrderToolStripMenuItem.Name = "associateOrderToolStripMenuItem";
      this.associateOrderToolStripMenuItem.Size = new Size(166, 22);
      this.associateOrderToolStripMenuItem.Text = "Associate Order";
      this.associateOrderToolStripMenuItem.Click += new EventHandler(this.associateOrderToolStripMenuItem_Click);
      this.craftOrderDetailToolStripMenuItem2.Name = "craftOrderDetailToolStripMenuItem2";
      this.craftOrderDetailToolStripMenuItem2.Size = new Size(166, 22);
      this.craftOrderDetailToolStripMenuItem2.Text = "Craft Order Detail";
      this.craftOrderDetailToolStripMenuItem2.Click += new EventHandler(this.toolStripMenuItemCraftOrderDetails_Click);
      this.chkBox_Incomplete.AutoSize = true;
      this.chkBox_Incomplete.Location = new Point(13, 7);
      this.chkBox_Incomplete.Name = "chkBox_Incomplete";
      this.chkBox_Incomplete.Size = new Size(61, 17);
      this.chkBox_Incomplete.TabIndex = 50;
      this.chkBox_Incomplete.Text = "Incomp";
      this.chkBox_Incomplete.UseVisualStyleBackColor = true;
      this.pictureBox1.Anchor = AnchorStyles.Top;
      this.pictureBox1.BackColor = SystemColors.Window;
      this.pictureBox1.BorderStyle = BorderStyle.FixedSingle;
      this.pictureBox1.Image = (Image) Resources.Network;
      this.pictureBox1.Location = new Point(135, 14);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(102, 75);
      this.pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
      this.pictureBox1.TabIndex = 51;
      this.pictureBox1.TabStop = false;
      this.panelDownLoad.Controls.Add((Control) this.labelFName);
      this.panelDownLoad.Controls.Add((Control) this.labelDownLoading);
      this.panelDownLoad.Controls.Add((Control) this.pictureBox1);
      this.panelDownLoad.Location = new Point(423, 130);
      this.panelDownLoad.Name = "panelDownLoad";
      this.panelDownLoad.Size = new Size(373, 160);
      this.panelDownLoad.TabIndex = 52;
      this.labelDownLoading.Anchor = AnchorStyles.Top;
      this.labelDownLoading.AutoSize = true;
      this.labelDownLoading.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.labelDownLoading.Location = new Point(77, 97);
      this.labelDownLoading.Name = "labelDownLoading";
      this.labelDownLoading.Size = new Size(219, 20);
      this.labelDownLoading.TabIndex = 52;
      this.labelDownLoading.Text = "Downloading Attachments";
      this.labelFName.AutoSize = true;
      this.labelFName.Location = new Point(12, 130);
      this.labelFName.Name = "labelFName";
      this.labelFName.Size = new Size(60, 13);
      this.labelFName.TabIndex = 53;
      this.labelFName.Text = "{ filename }";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.AutoScroll = true;
      this.ClientSize = new Size(1218, 337);
      this.Controls.Add((Control) this.panelDownLoad);
      this.Controls.Add((Control) this.chkBox_Incomplete);
      this.Controls.Add((Control) this.labelSelectedOrders);
      this.Controls.Add((Control) this.labelTotalOrders);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.comboBoxTaxStatus);
      this.Controls.Add((Control) this.comboBoxStore);
      this.Controls.Add((Control) this.buttonRefresh);
      this.Controls.Add((Control) this.comboBoxListChunk);
      this.Controls.Add((Control) this.buttonNext);
      this.Controls.Add((Control) this.buttonPrev);
      this.Controls.Add((Control) this.buttonCancel);
      this.Controls.Add((Control) this.buttonOK);
      this.Controls.Add((Control) this.labelServerURL);
      this.Controls.Add((Control) this.labelVersion);
      this.Controls.Add((Control) this.dataGridView1);
      this.Controls.Add((Control) this.label7);
      this.Controls.Add((Control) this.label6);
      this.Controls.Add((Control) this.label4);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.comboBoxVisualConnect);
      this.Controls.Add((Control) this.comboBoxOrderStatus);
      this.Controls.Add((Control) this.dateTimePickerTo);
      this.Controls.Add((Control) this.dateTimePickerFrom);
      this.Controls.Add((Control) this.comboBoxCustomer);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Margin = new Padding(2);
      this.Name = nameof (OrderSelectForm);
      this.Text = "Web Order Import";
      this.FormClosing += new FormClosingEventHandler(this.JobSelector_FormClosing);
      this.Load += new EventHandler(this.JobSelector_Load);
      this.Shown += new EventHandler(this.JobSelector_Shown);
      ((ISupportInitialize) this.dataGridView1).EndInit();
      this.contextMenuStripCustID.ResumeLayout(false);
      this.contextMenuStripOrderID.ResumeLayout(false);
      this.contextMenuStripAssocOrder.ResumeLayout(false);
      ((ISupportInitialize) this.pictureBox1).EndInit();
      this.panelDownLoad.ResumeLayout(false);
      this.panelDownLoad.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
