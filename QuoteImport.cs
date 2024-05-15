// Decompiled with JetBrains decompiler
// Type: WebOrderImportCraft.QuoteImport
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

using CraftCMS;
using MsgBox;
using Newtonsoft.Json;
using Synergy.DistributedERP;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WebOrderImportCraft.Properties;

namespace WebOrderImportCraft
{
  internal class QuoteImport
  {
    public ExternalReferenceService extRefSvc;
    public CustomerService custSvc;
    public NotationService notationSvc;
    public EstimateService quoteSvc;
    public InventoryService inventorySvc;
    public ContactService contactSvc;
    public UserDefinedFieldService UDFSvc;
    public OrderSelectForm orderSelectForm;
    public Quote webQuote = (Quote) null;
    public string quoteIncrement;
    public string VisualQuoteID;
    public string VisualCustID;
    public bool debug = false;
    private CraftUtil craftCmsUtil = (CraftUtil) null;

    internal QuoteImport(CraftUtil craftUtil) => this.craftCmsUtil = craftUtil;

    internal bool Import()
    {
      bool flag1 = true;
      try
      {
        if (!string.IsNullOrEmpty(this.quoteIncrement) || this.webQuote != null)
        {
          string empty1 = string.Empty;
          if (this.webQuote.lineItems != null)
            empty1 = Alias.Get(this.webQuote.lineItems[0].location);
          List<ExternalReference.Reference> referenceList = this.orderSelectForm.extRefSvc(empty1).ExternalReferenceLookup("false", "WebQuote", this.webQuote.quoteId.ToString(), (string) null, (string) null, (string) null, (string) null);
          if (referenceList.Count == 0)
          {
            if (this.webQuote.lineItems != null)
            {
              int num1 = this.webQuote.lineItems.GetLength(0);
              string str1 = string.Format("{0} Item(s).", (object) num1.ToString()) + "\r\n itemId : sku : qty : brand";
              List<string> stringList = new List<string>();
              foreach (QLineitem lineItem in this.webQuote.lineItems)
              {
                string[] strArray = new string[9]
                {
                  str1,
                  "\r\n",
                  lineItem.purchasableId.ToString(),
                  " :  : ",
                  lineItem.sku,
                  " : ",
                  null,
                  null,
                  null
                };
                num1 = lineItem.qty;
                strArray[6] = num1.ToString();
                strArray[7] = " : ";
                strArray[8] = lineItem.location;
                str1 = string.Concat(strArray);
                if (string.IsNullOrWhiteSpace(lineItem.location))
                  throw new Exception("Line with part# " + lineItem.sku + " does not have a location specified!");
                if (!stringList.Contains(lineItem.location))
                  stringList.Add(lineItem.location);
              }
              if (!Settings.Default.VerboseDialogs || MBox.Show(str1 + "\r\nCreate Quote?", "Create Quote", MessageBoxButtons.YesNo) == DialogResult.Yes)
              {
                bool flag2 = true;
                EstimateData data1 = new EstimateData();
                data1.Estimates = new List<EstimateHeader>();
                foreach (string key in stringList)
                {
                  ExternalReference.Reference reference1 = new ExternalReference.Reference();
                  reference1.ExternalType = "WebQuote";
                  ref ExternalReference.Reference local1 = ref reference1;
                  num1 = this.webQuote.quoteId;
                  string str2 = num1.ToString();
                  local1.ExternalID = str2;
                  reference1.ExternalLineNo = "0";
                  EstimateHeader estimateHeader1 = new EstimateHeader();
                  estimateHeader1.ReferenceList.Add(reference1);
                  estimateHeader1.SiteID = Alias.Get(key);
                  this.VisualCustID = this.webQuote.visualCustomerId;
                  if (!string.IsNullOrWhiteSpace(this.VisualCustID))
                    estimateHeader1.CustomerID = this.VisualCustID;
                  estimateHeader1.Status = "A";
                  estimateHeader1.TermsNetType = "A";
                  estimateHeader1.TermsDiscType = "A";
                  estimateHeader1.FreightTerms = "B";
                  estimateHeader1.Name = this.webQuote.company;
                  estimateHeader1.ContactFirstName = this.webQuote.firstName;
                  estimateHeader1.ContactLastName = this.webQuote.lastName;
                  estimateHeader1.ContactEmail = this.webQuote.email;
                  estimateHeader1.ContactPhone = this.webQuote.phone;
                  estimateHeader1.EstimateComments = this.webQuote.comments;
                  estimateHeader1.UserDefinedValues = new List<UserDefinedFieldValue>();
                  string str3 = "WQC";
                  if (!string.IsNullOrWhiteSpace(this.webQuote.type))
                    str3 = QuotePrefixMap.Get(this.webQuote.type) ?? "WQC";
                  num1 = this.webQuote.quoteId;
                  if (num1.ToString().StartsWith(str3))
                  {
                    EstimateHeader estimateHeader2 = estimateHeader1;
                    num1 = this.webQuote.quoteId;
                    string str4 = num1.ToString() + "-" + Alias.Get(key, true);
                    estimateHeader2.QuoteID = str4;
                  }
                  else
                  {
                    EstimateHeader estimateHeader3 = estimateHeader1;
                    string str5 = str3;
                    num1 = this.webQuote.quoteId;
                    string str6 = num1.ToString();
                    string str7 = Alias.Get(key, true);
                    string str8 = str5 + str6 + "-" + str7;
                    estimateHeader3.QuoteID = str8;
                  }
                  string quoteId = estimateHeader1.QuoteID;
                  EstimateHeader estimateHeader4 = new EstimateHeader();
                  estimateHeader4.QuoteID = estimateHeader1.QuoteID;
                  estimateHeader4.SiteID = estimateHeader1.SiteID;
                  int num2 = 0;
                  EstimateData data2 = new EstimateData()
                  {
                    Estimates = new List<EstimateHeader>()
                  };
                  data2.Estimates.Add(estimateHeader4);
                  EstimateData estimateData;
                  do
                  {
                    estimateData = this.orderSelectForm.quoteSvc(estimateHeader1.SiteID).SearchEstimate(data2);
                    estimateHeader1.QuoteID = data2.Estimates[0].QuoteID;
                    ++num2;
                    data2.Estimates[0].QuoteID = quoteId + "-" + num2.ToString();
                  }
                  while (estimateData.Estimates.Count > 0);
                  estimateHeader1.Lines = new List<EstimateLine>();
                  string empty2 = string.Empty;
                  string empty3 = string.Empty;
                  int? nullable;
                  foreach (QLineitem lineItem in this.webQuote.lineItems)
                  {
                    if (lineItem.location == key)
                    {
                      ExternalReference.Reference reference2 = new ExternalReference.Reference();
                      reference2.ExternalType = "WebQuoteLine";
                      ref ExternalReference.Reference local2 = ref reference2;
                      num1 = this.webQuote.quoteId;
                      string str9 = num1.ToString();
                      local2.ExternalID = str9;
                      ref ExternalReference.Reference local3 = ref reference2;
                      num1 = lineItem.lineItemId;
                      string str10 = num1.ToString();
                      local3.ExternalLineNo = str10;
                      EstimateLine estimateLine = new EstimateLine();
                      estimateLine.ReferenceList.Add(reference2);
                      estimateLine.LineComments = JsonConvert.SerializeObject((object) lineItem.options);
                      estimateLine.Price = new EstimateLinePrice();
                      estimateLine.Price.UnitPrice = new Decimal?(lineItem.previousUnitPrice.GetValueOrDefault());
                      estimateLine.Price.Quantity = new Decimal?((Decimal) lineItem.qty);
                      if (!Settings.Default.PartSpecOnly)
                      {
                        if (!string.IsNullOrEmpty(lineItem?.sku))
                        {
                          string upper = lineItem.sku.ToUpper();
                          string partID = upper + "-" + estimateHeader1.SiteID;
                          if (this.orderSelectForm.inventorySvc(estimateHeader1.SiteID).PartIDExists(partID))
                            estimateLine.PartID = partID.ToUpper();
                          else if (this.orderSelectForm.inventorySvc(estimateHeader1.SiteID).PartIDExists(upper))
                          {
                            estimateLine.PartID = upper.ToUpper();
                          }
                          else
                          {
                            string str11 = string.Empty;
                            if (estimateHeader1.SiteID == "RAF")
                              str11 = new RAFParts().CreatePart(this.orderSelectForm.Database(Alias.Get(estimateHeader1.SiteID)), estimateHeader1.SiteID, upper, this.orderSelectForm.inventorySvc(estimateHeader1.SiteID));
                            if (string.IsNullOrEmpty(str11))
                            {
                              string[] strArray = new string[8]
                              {
                                "NA: ",
                                upper,
                                " : ",
                                partID,
                                " : ",
                                lineItem.sku,
                                " : ",
                                null
                              };
                              nullable = lineItem.purchasableId;
                              ref int? local4 = ref nullable;
                              string str12;
                              if (!local4.HasValue)
                              {
                                str12 = (string) null;
                              }
                              else
                              {
                                num1 = local4.GetValueOrDefault();
                                str12 = num1.ToString();
                              }
                              strArray[7] = str12;
                              string str13 = string.Concat(strArray);
                              estimateLine.Description = str13.Substring(0, Math.Min(str13.Length, 40));
                              if (MBox.Show("Part not found: " + str13 + "\r\nCreate Order?", "Create Order", MessageBoxButtons.YesNo) == DialogResult.No)
                                flag2 = false;
                            }
                            else
                              estimateLine.PartID = str11;
                          }
                        }
                        else if (!string.IsNullOrEmpty(lineItem?.serviceChargeId))
                          estimateLine.ServiceChargeID = lineItem.serviceChargeId;
                      }
                      else
                      {
                        string sku = lineItem.sku;
                        nullable = lineItem.purchasableId;
                        ref int? local5 = ref nullable;
                        string str14;
                        if (!local5.HasValue)
                        {
                          str14 = (string) null;
                        }
                        else
                        {
                          num1 = local5.GetValueOrDefault();
                          str14 = num1.ToString();
                        }
                        string str15 = "SKU: " + sku + " ID: " + str14;
                        if (!string.IsNullOrEmpty(lineItem?.serviceChargeId))
                          str15 = str15 + " ServChg: " + lineItem?.serviceChargeId;
                        estimateLine.Description = str15.Substring(0, Math.Min(str15.Length, 40));
                      }
                      if (!string.IsNullOrWhiteSpace(lineItem.description))
                        estimateLine.Description = lineItem.description.Substring(0, Math.Min(lineItem.description.Length, 40));
                      estimateHeader1.Lines.Add(estimateLine);
                    }
                  }
                  data1.Estimates.Add(estimateHeader1);
                  if (flag2)
                  {
                    data1.ReturnErrorInResponse = new bool?(true);
                    data1.UseIndependentTransactions = new bool?(false);
                    EstimateDataResponse estimate1 = this.orderSelectForm.quoteSvc(estimateHeader1.SiteID).CreateEstimate(data1);
                    if (estimate1.HasErrors)
                    {
                      string message = string.Empty;
                      foreach (EstimateHeaderResponse estimate2 in estimate1.Estimates)
                        message = message + "\r\n" + estimate2.EstimateHeader.QuoteID + "\r\n" + estimate2.ErrorMessage + "\r\n";
                      int num3 = (int) MBox.Show(message);
                    }
                    else
                    {
                      foreach (EstimateHeader estimate3 in data1.Estimates)
                      {
                        NotationService.NotationData data3 = new NotationService.NotationData();
                        data3.NotationType = "Q";
                        data3.OwnerID = estimate3.QuoteID;
                        data3.UserID = string.IsNullOrEmpty(this.orderSelectForm.notationSvc(estimate3.SiteID)._Header.UserName) ? "WebUser" : this.orderSelectForm.notationSvc(estimate3.SiteID)._Header.UserName;
                        NotationService.NotationData notationData1 = data3;
                        num1 = this.webQuote.quoteId;
                        string str16 = num1.ToString() + " (" + this.webQuote.number + ")\n";
                        notationData1.Note = str16;
                        NotationService.NotationData notationData2 = data3;
                        string note1 = notationData2.Note;
                        num1 = this.webQuote.quoteId;
                        string str17 = num1.ToString();
                        notationData2.Note = note1 + "\nquoteId: " + str17;
                        NotationService.NotationData notationData3 = data3;
                        notationData3.Note = notationData3.Note + "\nnumber: " + this.webQuote.number;
                        NotationService.NotationData notationData4 = data3;
                        notationData4.Note = notationData4.Note + "\ntype: " + this.webQuote.type;
                        NotationService.NotationData notationData5 = data3;
                        notationData5.Note = notationData5.Note + "\nstage: " + this.webQuote.stage;
                        NotationService.NotationData notationData6 = data3;
                        notationData6.Note = notationData6.Note + "\nvisualOrderId: " + this.webQuote.visualOrderId;
                        NotationService.NotationData notationData7 = data3;
                        notationData7.Note = notationData7.Note + "\nvisualCustomerId: " + this.webQuote.visualCustomerId;
                        NotationService.NotationData notationData8 = data3;
                        string note2 = notationData8.Note;
                        nullable = this.webQuote.userId;
                        ref int? local6 = ref nullable;
                        string str18;
                        if (!local6.HasValue)
                        {
                          str18 = (string) null;
                        }
                        else
                        {
                          num1 = local6.GetValueOrDefault();
                          str18 = num1.ToString();
                        }
                        notationData8.Note = note2 + "\nuserId: " + str18;
                        NotationService.NotationData notationData9 = data3;
                        notationData9.Note = notationData9.Note + "\nisCompleted: " + this.webQuote.isCompleted.ToString();
                        NotationService.NotationData notationData10 = data3;
                        notationData10.Note = notationData10.Note + "\ndateCompleted: " + this.webQuote.dateCompleted?.date + " " + this.webQuote.dateCompleted?.time;
                        NotationService.NotationData notationData11 = data3;
                        notationData11.Note = notationData11.Note + "\nemail: " + this.webQuote.email;
                        NotationService.NotationData notationData12 = data3;
                        notationData12.Note = notationData12.Note + "\nphone: " + this.webQuote.phone;
                        NotationService.NotationData notationData13 = data3;
                        notationData13.Note = notationData13.Note + "\nfirstName: " + this.webQuote.firstName;
                        NotationService.NotationData notationData14 = data3;
                        notationData14.Note = notationData14.Note + "\nlastName: " + this.webQuote.lastName;
                        NotationService.NotationData notationData15 = data3;
                        notationData15.Note = notationData15.Note + "\ncompany: " + this.webQuote.company;
                        NotationService.NotationData notationData16 = data3;
                        notationData16.Note = notationData16.Note + "\ncomments: " + this.webQuote.comments;
                        NotationService.NotationData notationData17 = data3;
                        notationData17.Note = notationData17.Note + "\nsalesRepName: " + this.webQuote.salesRepName;
                        NotationService.NotationData notationData18 = data3;
                        notationData18.Note = notationData18.Note + "\nsalesRepPhone: " + this.webQuote.salesRepPhone;
                        NotationService.NotationData notationData19 = data3;
                        notationData19.Note = notationData19.Note + "\nsalesRepEmail: " + this.webQuote.salesRepEmail;
                        NotationService.NotationData notationData20 = data3;
                        notationData20.Note = notationData20.Note + "\nvisualQuoteId: " + this.webQuote.visualQuoteId;
                        NotationService.NotationData notationData21 = data3;
                        notationData21.Note = notationData21.Note + "\ndateExpires: " + this.webQuote.dateExpires;
                        data3.Note += "\n";
                        this.orderSelectForm.notationSvc(estimate3.SiteID).AddNotation(data3);
                      }
                      UDFLabels data4 = new UDFLabels()
                      {
                        UDFLabelList = new List<UDFLabel>()
                      };
                      data4.UDFLabelList.Add(new UDFLabel()
                      {
                        ProgramID = "VMESTWIN",
                        Label = "Entered By",
                        TabID = "Additional Info"
                      });
                      UDFDataResponse udfDataResponse = this.orderSelectForm.UDFSvc(estimateHeader1.SiteID).SearchUserDefinedLabel(data4);
                      if (!udfDataResponse.HasErrors && udfDataResponse.UDFLabels.Count == 1)
                      {
                        UDFData data5 = new UDFData()
                        {
                          UDFValueList = new List<UDFValue>()
                        };
                        data5.UDFValueList.Add(new UDFValue()
                        {
                          ProgramID = udfDataResponse.UDFLabels[0].UDFLabel.ProgramID,
                          DocumentID = estimate1.Estimates[0].EstimateHeader.QuoteID,
                          ID = udfDataResponse.UDFLabels[0].UDFLabel.ID,
                          StringValue = "Web User"
                        });
                        this.orderSelectForm.UDFSvc(estimateHeader1.SiteID).CreateUDF(data5);
                      }
                      this.VisualQuoteID = estimate1.Estimates[0].EstimateHeader.QuoteID;
                      string message = "Quote ID(s):";
                      if (estimate1.Estimates.Count > 1)
                        message = "Quote IDs:";
                      foreach (EstimateHeaderResponse estimate4 in estimate1.Estimates)
                        message = message + " " + estimate4.EstimateHeader.QuoteID;
                      if (!this.debug)
                      {
                        if (this.craftCmsUtil.updateQuoteStatus(this.quoteIncrement, "imported", this.VisualQuoteID))
                        {
                          if (Settings.Default.VerboseDialogs)
                          {
                            int num4 = (int) MBox.Show(message);
                          }
                        }
                        else
                        {
                          int num5 = (int) MBox.Show("updateQuoteStatus failed! \r\n\r\n " + message);
                        }
                      }
                      else if (Settings.Default.VerboseDialogs)
                      {
                        int num6 = (int) MBox.Show(message);
                      }
                    }
                  }
                  data1.Estimates.Clear();
                }
              }
            }
          }
          else
          {
            int num = (int) MBox.Show("Visual Order " + referenceList[0].ID + " exists for web order: " + this.quoteIncrement + " (" + referenceList[0].ExternalID + ")");
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
  }
}
