// Decompiled with JetBrains decompiler
// Type: WebOrderImportCraft.Properties.Settings
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace WebOrderImportCraft.Properties
{
  [CompilerGenerated]
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.5.0.0")]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

    public static Settings Default
    {
      get
      {
        Settings defaultInstance = Settings.defaultInstance;
        return defaultInstance;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("Normal")]
    public FormWindowState SelectorWindowState
    {
      get => (FormWindowState) this[nameof (SelectorWindowState)];
      set => this[nameof (SelectorWindowState)] = (object) value;
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("100, 100")]
    public Point SelectorWindowLoc
    {
      get => (Point) this[nameof (SelectorWindowLoc)];
      set => this[nameof (SelectorWindowLoc)] = (object) value;
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("1700, 600")]
    public Size SelectorWindowSize
    {
      get => (Size) this[nameof (SelectorWindowSize)];
      set => this[nameof (SelectorWindowSize)] = (object) value;
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("")]
    public string SelectorWindowAccount
    {
      get => (string) this[nameof (SelectorWindowAccount)];
      set => this[nameof (SelectorWindowAccount)] = (object) value;
    }

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("CSC-Live")]
    public string ExternalReferenceGroup => (string) this[nameof (ExternalReferenceGroup)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("(USD) $")]
    public string CurrencyID => (string) this[nameof (CurrencyID)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("MMC")]
    public string EntityID => (string) this[nameof (EntityID)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("EC")]
    public string TerritoryID => (string) this[nameof (TerritoryID)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("Net30")]
    public string TermsID => (string) this[nameof (TermsID)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("CSC")]
    public string UDF7_Division => (string) this[nameof (UDF7_Division)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("CREATE_NEW_ACCOUNT")]
    public string GuestCustomerID => (string) this[nameof (GuestCustomerID)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("")]
    public string DiscountCode => (string) this[nameof (DiscountCode)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("Net30-1")]
    public string TermsCollectID => (string) this[nameof (TermsCollectID)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("ORIGIN")]
    public string FOB => (string) this[nameof (FOB)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("NON TAXABLE")]
    public string FreightTaxGroupID => (string) this[nameof (FreightTaxGroupID)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [SpecialSetting(SpecialSetting.WebServiceUrl)]
    [DefaultSettingValue("https://www.mwcomponents.com/actions/erp/api/")]
    public string EComSite_Service => (string) this[nameof (EComSite_Service)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("hogueerp")]
    public string APIUSER => (string) this[nameof (APIUSER)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("=3t+QYuZ+Hqs38fC")]
    public string APIKEY => (string) this[nameof (APIKEY)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("E-COM_")]
    public string AddressPrefix => (string) this[nameof (AddressPrefix)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("RETAIL")]
    public string MARKET => (string) this[nameof (MARKET)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("False")]
    public bool PartSpecOnly => (bool) this[nameof (PartSpecOnly)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("COUPON")]
    public string CouponPartID => (string) this[nameof (CouponPartID)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("AUTO")]
    public string ConfigSettingsKey => (string) this[nameof (ConfigSettingsKey)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("False")]
    public bool VerboseDialogs => (bool) this[nameof (VerboseDialogs)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("NON TAXABLE")]
    public string SmallOrderTaxGroupID => (string) this[nameof (SmallOrderTaxGroupID)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("CPASS")]
    public string PassivationSvcChg => (string) this[nameof (PassivationSvcChg)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("ZINC PLATING")]
    public string ZincPlatingSvcChg => (string) this[nameof (ZincPlatingSvcChg)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("GOLD IRRIDATE")]
    public string GoldIrridateSvcChg => (string) this[nameof (GoldIrridateSvcChg)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("13:00:00")]
    public TimeSpan ShippingCutOffTime => (TimeSpan) this[nameof (ShippingCutOffTime)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("C:\\Infor\\VISUAL\\VISUAL908_SP5\\VM.exe")]
    public string VM_EXE_Path => (string) this[nameof (VM_EXE_Path)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("True")]
    public bool ProportionalTax => (bool) this[nameof (ProportionalTax)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("True")]
    public bool ProportionalFreight => (bool) this[nameof (ProportionalFreight)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("VM908-key")]
    public string VMKey => (string) this[nameof (VMKey)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("True")]
    public bool ProportionalSmallOrderFee => (bool) this[nameof (ProportionalSmallOrderFee)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("https://www.mwcomponents.com/mwicdo/")]
    public string EComSite_AdminURL => (string) this[nameof (EComSite_AdminURL)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue(".")]
    public string LogFileRoot => (string) this[nameof (LogFileRoot)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("AMF:HOLD|ANA:HOLD|ASC:CLOSED|ASM:HOLD|ATL:HOLD|BTL:HOLD|CAP:HOLD|CSC:RELEASED|ECO:HOLD|EPV:EPV|ESP:HOLD|GRE:HOLD|HPC:HOLD|HPS:HOLD|LVP:HOLD|MHK:HOLD|MMC:HOLD|MPS:HOLD|MRX:HOLD|MWC:HOLD|MWS:HOLD|PON:HOLD|RAF:HOLD|RUM:HOLD|SPM:HOLD|SSX:HOLD|SVM:HOLD|TSI:HOLD|USA:HOLD")]
    public string OrderStatusDefault => (string) this[nameof (OrderStatusDefault)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("Ameriflex:AMF|Anaheim:ANA|Ideal Fasteners:ASC|Accurate Screw:ASM|Atlantic Spring:ATL|MWC Ormond Beach:BTL|BellowsTech:BTL|Capital Spring:CAP|Century Spring:CSC|Economy Spring:ECO|East Providence:EPV|Engineered Spring:ESP|Fox Valley:GRE|Helical:HPC|Hi-Performance Fastening:HPS|LaVezzi Precision:LVP|Mohawk Spring:MHK|Maudlin:MMC|Maryland Precision:MPS|Marox:MRX|MW Industries:MWC|Matthew-Warren Spring:MWS|MWC Pontotoc:PON|Hyperco:PON/HYP|RAF:RAF|Rumco Fastener:RUM|Springmasters:SPM|Sussex Wire:SSX|Servometer:SVM|Tri-Star:TSI|MWC Houston Fasteners:USA|USA Fastener:USA")]
    public string SiteAliases => (string) this[nameof (SiteAliases)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("AMF:AMF|ANA:ANA|ASC:FRT-IDEAL|ASM:Freight-ASM|ATL:ATL|BTL:SPC CHG 52 BTL|CAP:CAP|CSC:CFREIGHT|ECO:ECO|EPV:EPV|ESP:ESP|GRE:GRE|HPC:HPC|HPS:HPS|LVP:LVP|MHK:MHK|MMC:MMC-WEBFREIGHT|MPS:MPS|MRX:MRX|MWC:MWC|MWS:MWS|PON:Freight PON|RAF:RAF-ECOMFREIGHT|RUM:RUM|SPM:SPM|SSX:SSX|SVM:SPC CHG 38 SVM|TSI:TSI|USA:Freight USA")]
    public string FreightSvcChg => (string) this[nameof (FreightSvcChg)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("AMF:AMF|ANA:ANA|ASC:TAX-IDEAL|ASM:TAX-ASM|ATL:ATL|BTL:SPC CHG 51 BTL|CAP:CAP|CSC:TAX|ECO:ECO|EPV:EPV|ESP:ESP|GRE:GRE|HPC:HPC|HPS:HPS|LVP:LVP|MHK:MHK|MMC:MMC-WEBTAX|MPS:MPS|MRX:MRX|MWC:MWC|MWS:MWS|PON:Tax PON|RAF:RAF-ECOMTAX|RUM:RUM|SPM:SPM|SSX:SSX|SVM:SPC CHG 37 SVM|TSI:TSI|USA:Tax USA")]
    public string TaxSvcChg => (string) this[nameof (TaxSvcChg)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("AMF:AMF|ANA:ANA|ASC:SM-ORDER-IDEAL|ASM:SM-Order-ASM|ATL:ATL|BTL:SPC CHG 53 BTL|CAP:CAP|CSC:SMALL ORDER FEE|ECO:ECO|EPV:EPV|ESP:ESP|GRE:GRE|HPC:HPC|HPS:HPS|LVP:LVP|MHK:MHK|MMC:MMC-WEBSMORDER|MPS:MPS|MRX:MRX|MWC:MWC|MWS:MWS|PON:Small Order PON|RAF:RAF-ECOMSMORDER|RUM:RUM|SPM:SPM|SSX:SSX|SVM:SPC CHG 39 SVM|TSI:TSI|USA:Small Order USA")]
    public string SmallOrderSvcChg => (string) this[nameof (SmallOrderSvcChg)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("VM908-key2")]
    public string VMKey2 => (string) this[nameof (VMKey2)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("AMF:VM908-key2|ANA:VM908-key2|ASC:VM908-key2|ASM:VM908-key2|ATL:VM908-key2|BTL:VM908-key2|CAP:VM908-key2|CSC:VM908-key|ECO:VM908-key2|EPV:VM908-key2|ESP:VM908-key2|GRE:VM908-key2|HPC:VM908-key2|HPS:VM908-key2|LVP:VM908-key2|MHK:VM908-key2|MMC:VM908-key2|MPS:VM908-key2|MRX:VM908-key2|MWC:VM908-key2|MWS:VM908-key2|PON:VM908-key2|RAF:VM908-key2|RUM:VM908-key2|SPM:VM908-key2|SSX:VM908-key2|SVM:VM908-key2|TSI:VM908-key2|USA:VM908-key2")]
    public string SiteDBMap => (string) this[nameof (SiteDBMap)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("default:E|savedCart:ESC|ecommerceReorder:EER|rfqCart:EQC|reorderRequest:ERQ|offlineQuote:EOQ")]
    public string OrderPrefixMap => (string) this[nameof (OrderPrefixMap)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("default:WQC|savedCartQuote:WSC|reorderRequest:WRR")]
    public string QuotePrefixMap => (string) this[nameof (QuotePrefixMap)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("BLACK OXIDE")]
    public string BlackOxideSvcChg => (string) this[nameof (BlackOxideSvcChg)];
  }
}
