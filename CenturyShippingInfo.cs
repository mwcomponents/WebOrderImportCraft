// Decompiled with JetBrains decompiler
// Type: WebOrderImportCraft.CenturyShippingInfo
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

using CraftCMS;
using System;

namespace WebOrderImportCraft
{
  internal class CenturyShippingInfo : SalesShippingInfo
  {
    internal CenturyShippingInfo(Order webSO)
      : base(webSO)
    {
    }

    public override string GetShippingInfo(string field)
    {
      string empty1 = string.Empty;
      string str1 = string.Empty;
      string str2 = string.Empty;
      string str3 = string.Empty;
      string empty2 = string.Empty;
      string shippingMethodName = this.WebSO?.shippingMethodName;
      if (!string.IsNullOrEmpty(shippingMethodName))
      {
        if (shippingMethodName == "Shipping Account")
        {
          str3 = this.WebSO.shippingAccountNumber;
          str1 = this.WebSO.shippingAccountService.ToUpper();
          str2 = this.WebSO.shippingAccountMethod.ToUpper();
        }
        else
        {
          switch (shippingMethodName.Substring(0, Math.Min(3, shippingMethodName.Length)).ToUpper())
          {
            case "UPS":
              str1 = "UPS";
              break;
            case "FED":
              str1 = "FEDEX";
              break;
            case "DHL":
              str1 = "DHL";
              break;
          }
          string[] strArray = shippingMethodName.Split(new char[1]
          {
            ' '
          }, 2, StringSplitOptions.None);
          if (strArray.Length == 2)
            str2 = strArray[1].ToUpper().Trim();
        }
      }
      string shippingInfo;
      switch (field)
      {
        case "carrier":
          shippingInfo = str1;
          break;
        case "service":
          shippingInfo = str2;
          break;
        case "account":
          shippingInfo = str3;
          break;
        case "shipvia":
          switch (str1.ToUpper())
          {
            case "UPS":
              switch (str2.ToUpper())
              {
                case "01":
                case "1DA":
                case "NEXT DAY AIR":
                  shippingInfo = "UPS NEXT DAY AIR";
                  break;
                case "02":
                case "2DA":
                case "2ND DAY AIR":
                  shippingInfo = "UPS 2ND DAY AIR";
                  break;
                case "03":
                case "GND":
                case "GROUND":
                  shippingInfo = "UPS GROUND";
                  break;
                case "07":
                case "WORLDWIDE EXP":
                case "XPR":
                  shippingInfo = "UPS WORLDWIDE EXP";
                  break;
                case "08":
                case "WORLD WIDE EXP":
                case "XPD":
                  shippingInfo = "UPS WORLD WIDE EXPD";
                  break;
                case "11":
                case "CANADA STANDARD":
                case "STD":
                  shippingInfo = "UPS CANADA STANDARD";
                  break;
                case "12":
                case "3 DAY SELECT":
                case "3DS":
                  shippingInfo = "UPS 3 DAY SELECT";
                  break;
                case "SAT":
                case "SAT DEL":
                  shippingInfo = "UPS SAT DEL";
                  break;
                default:
                  shippingInfo = "UPS " + str2;
                  break;
              }
              break;
            case "FEDEX":
              switch (str2.ToUpper())
              {
                case "2 DAY":
                case "FEDEX_2_DAY":
                  shippingInfo = "FEDEX 2ND DAY";
                  break;
                case "EXPRESS SAVER":
                case "FEDEX_EXPRESS_SAVER":
                  shippingInfo = "FEDEX EXPRESS SAVER";
                  break;
                case "FEDEX_GROUND":
                case "GROUND":
                  shippingInfo = "FEDEX GROUND";
                  break;
                case "INTERNATIONAL ECONOMY":
                case "INTERNATIONAL_ECONOMY":
                  shippingInfo = "FEDEX INTL ECONO";
                  break;
                case "INTERNATIONAL PRIORITY":
                case "INTERNATIONAL_PRIORITY":
                  shippingInfo = "FEDEX INTL PRIORITY";
                  break;
                case "PRIORITY OVERNIGHT":
                case "PRIORITY_OVERNIGHT":
                  shippingInfo = "FEDEX PRIORITY OVERNIGHT";
                  break;
                case "SATURDAY":
                  shippingInfo = "FEDEX SATURDAY";
                  break;
                case "STANDARD OVERNIGHT":
                case "STANDARD_OVERNIGHT":
                  shippingInfo = "FEDEX STANDARD OVERNIGHT";
                  break;
                default:
                  shippingInfo = "FEDEX " + str2;
                  break;
              }
              break;
            case "DHL":
              switch (str2.ToUpper())
              {
                case "EXPRESS WORLDWIDE STANDARD":
                  shippingInfo = "DHL INTL STD";
                  break;
                case "EXPRESS WORLDWIDE PRIORITY":
                  shippingInfo = "DHL INTL PRIORITY";
                  break;
                default:
                  shippingInfo = "DHL " + str2;
                  break;
              }
              break;
            default:
              shippingInfo = str1 + " " + str2;
              break;
          }
          break;
        default:
          shippingInfo = string.Empty;
          break;
      }
      return shippingInfo;
    }
  }
}
