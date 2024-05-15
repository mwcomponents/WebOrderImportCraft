// Decompiled with JetBrains decompiler
// Type: WebOrderImportCraft.CraftUtil
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

using CraftCMS;
using MsgBox;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using WebOrderImportCraft.Properties;

namespace WebOrderImportCraft
{
  internal class CraftUtil
  {
    public Binding binding = (Binding) null;
    public string accessToken = (string) null;
    internal static string ClientURL = Settings.Default.EComSite_Service + "?services=";

    internal static string GetCustomerCustomAttr(object custWeb, string code) => (string) null;

    internal static void SetCustomerCustomAttr(object custWeb, string code, string val)
    {
    }

    internal object GetSalesExtensionAttr(object extAttrs) => (object) null;

    internal object CustomerInfo(int id, object attributes)
    {
      object obj = (object) null;
      try
      {
      }
      catch (Exception ex)
      {
        int num = (int) MBox.Show("Get CustomerInfo Failed " + ex.ToString());
      }
      return obj;
    }

    internal bool CustomerUpdate(Customer customerData, string VisualCustID)
    {
      bool flag1 = false;
      bool flag2 = false;
      if (!string.IsNullOrWhiteSpace(VisualCustID))
      {
        try
        {
          string[] strArray1;
          if (customerData == null)
          {
            strArray1 = (string[]) null;
          }
          else
          {
            User user = customerData.user;
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
          }
          string[] strArray2 = strArray1;
          if (strArray2 != null)
          {
            foreach (string str in strArray2)
            {
              string strB = str?.Trim();
              if (!string.IsNullOrWhiteSpace(VisualCustID) && !string.IsNullOrWhiteSpace(strB) && string.Compare(VisualCustID, strB, true) == 0)
              {
                flag2 = true;
                break;
              }
            }
          }
          if (customerData.userId != null && !flag2)
          {
            string str = VisualCustID;
            if (!string.IsNullOrWhiteSpace(customerData?.user?.visualId))
              str = customerData.user.visualId + ", " + str;
            flag1 = this.webPost(Settings.Default.EComSite_Service + "update-user?userId=" + customerData.userId, "{\"visualId\": \"" + str + "\"}", "PATCH") == "{\"success\":true}";
          }
        }
        catch (Exception ex)
        {
          int num = (int) MBox.Show("CustomerUpdate Failed " + ex.ToString());
        }
      }
      return flag1;
    }

    internal object[] customerGroupList() => new object[0];

    internal CraftUtil.SearchFilterGroup[] AddFilter2(
      CraftUtil.SearchFilterGroup[] filtresIn,
      string key,
      string op,
      string value)
    {
      CraftUtil.Filter filter = new CraftUtil.Filter()
      {
        field = key,
        conditionType = op,
        value = value
      };
      CraftUtil.SearchFilterGroup searchFilterGroup = new CraftUtil.SearchFilterGroup();
      searchFilterGroup.filters = new CraftUtil.Filter[1];
      searchFilterGroup.filters[0] = filter;
      List<CraftUtil.SearchFilterGroup> list = ((IEnumerable<CraftUtil.SearchFilterGroup>) (filtresIn ?? new CraftUtil.SearchFilterGroup[0])).ToList<CraftUtil.SearchFilterGroup>();
      list.Add(searchFilterGroup);
      return list.ToArray();
    }

    internal object[] Stores()
    {
      try
      {
      }
      catch (Exception ex)
      {
        Console.Out.WriteLine(ex.ToString());
      }
      return new object[0];
    }

    internal Order[] Orders(CraftUtil.SearchFilterGroup[] filterGroups)
    {
      CraftSalesOrders craftSalesOrders = new CraftSalesOrders();
      EndpointAddress endpointAddress = new EndpointAddress(Settings.Default.EComSite_Service + "?services=salesOrderRepositoryV1");
      try
      {
        craftSalesOrders = JsonConvert.DeserializeObject<CraftSalesOrders>(this.WebGet("orders" + this.QueryDecode(filterGroups)).Replace("\"options\":[],", "").Replace("\"sourceSnapshot\":[],", "").Replace("\"location\":[null,null,null],", "\"location\":null,"));
      }
      catch (Exception ex)
      {
        int num = (int) MBox.Show(filterGroups.ToString() + " Failed " + ex.ToString());
      }
      return craftSalesOrders?.orders;
    }

    internal Order OrderInfo(string id)
    {
      CraftSalesOrders craftSalesOrders = new CraftSalesOrders();
      try
      {
        craftSalesOrders = JsonConvert.DeserializeObject<CraftSalesOrders>(this.WebGet("orders?id=" + id));
      }
      catch (Exception ex)
      {
        int num = (int) MBox.Show("Get OrderInfo Failed " + ex.ToString());
      }
      return craftSalesOrders?.orders?[0];
    }

    internal Quote[] Quotes(CraftUtil.SearchFilterGroup[] filterGroups)
    {
      CraftQuotes craftQuotes = new CraftQuotes();
      try
      {
        craftQuotes = JsonConvert.DeserializeObject<CraftQuotes>(this.WebGet("quotes" + this.QueryDecode(filterGroups)).Replace("\"options\":[],", "").Replace("\"sourceSnapshot\":[],", "").Replace("\"location\":[null,null,null],", "\"location\":null,"));
      }
      catch (Exception ex)
      {
        int num = (int) MBox.Show(filterGroups.ToString() + " Failed " + ex.ToString());
      }
      return craftQuotes?.quotes;
    }

    internal Quote QuoteInfo(string number)
    {
      CraftQuotes craftQuotes = new CraftQuotes();
      try
      {
        craftQuotes = JsonConvert.DeserializeObject<CraftQuotes>(this.WebGet("quotes?number=" + number));
      }
      catch (Exception ex)
      {
        int num = (int) MBox.Show("Get QuoteInfo Failed " + ex.ToString());
      }
      return craftQuotes?.quotes?[0];
    }

    internal bool updateQuoteStatus(string quoteID, string stage, string visualQuoteId)
    {
      bool flag = false;
      string postURL = string.Empty;
      string postData = string.Empty;
      try
      {
        postURL = Settings.Default.EComSite_Service + "update-quote?quoteId=" + quoteID;
        if (string.IsNullOrWhiteSpace(visualQuoteId))
          postData = "{\"stage\": \"" + stage + "\"}";
        else
          postData = "{\"stage\": \"" + stage + "\",\"visualQuoteId\": \"" + visualQuoteId + "\"}";
        flag = this.webPost(postURL, postData, "PATCH") == "{\"success\":true}";
      }
      catch (Exception ex)
      {
        int num = (int) MBox.Show("updateQuoteStatus Failed \r" + postURL + "\r" + postData + "\r" + ex.ToString());
      }
      return flag;
    }

    internal bool updateQuote(string quoteID, QuoteUpdateReq quote)
    {
      bool flag = false;
      string postURL = string.Empty;
      string postData = string.Empty;
      try
      {
        postURL = Settings.Default.EComSite_Service + "update-quote?quoteId=" + quoteID;
        postData = JsonConvert.SerializeObject((object) quote);
        flag = this.webPost(postURL, postData, "PATCH") == "{\"success\":true}";
      }
      catch (Exception ex)
      {
        int num = (int) MBox.Show("updateOrderStatus Failed \r" + postURL + "\r" + postData + "\r" + ex.ToString());
      }
      return flag;
    }

    internal bool updateOrderStatus(string orderID, string status, string comment)
    {
      bool flag = false;
      try
      {
        flag = this.webPost(Settings.Default.EComSite_Service + "update-order?orderId=" + orderID, "{\"visualId\": \"" + comment + "\", \"status\": \"" + status + "\"}", "PATCH") == "{\"success\":true}";
      }
      catch (Exception ex)
      {
        int num = (int) MBox.Show("updateOrderStatus Failed " + ex.ToString());
      }
      return flag;
    }

    internal string invoiceOrder(string orderID, string comment) => "";

    internal bool updateShipmentTracking(string order, string trackingNo) => false;

    internal object[] CountryList()
    {
      try
      {
      }
      catch (Exception ex)
      {
        int num = (int) MBox.Show("Get Country List Failed " + ex.ToString());
      }
      return new object[0];
    }

    internal object[] RegionList(string id)
    {
      try
      {
      }
      catch (Exception ex)
      {
        int num = (int) MBox.Show("Get Region List Failed " + ex.ToString());
      }
      return new object[0];
    }

    private string WebGet(string getURL)
    {
      HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(new Uri(new Uri(Settings.Default.EComSite_Service), getURL));
      httpWebRequest.Method = "GET";
      httpWebRequest.Accept = "application/xml";
      string apikey = Settings.Default.APIKEY;
      httpWebRequest.Headers.Add("Authorization", "Bearer " + apikey);
      httpWebRequest.Accept = "text/html, application/xhtml+xml, application/xml, application/json, */*";
      string str = string.Empty;
      using (HttpWebResponse response = (HttpWebResponse) httpWebRequest.GetResponse())
      {
        Stream responseStream = response.GetResponseStream();
        StreamReader streamReader = new StreamReader(responseStream);
        str = streamReader.ReadToEnd();
        streamReader.Close();
        responseStream.Close();
      }
      return str;
    }

    private string QueryDecode(CraftUtil.SearchFilterGroup[] filterGroups)
    {
      string str1 = string.Empty;
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string str2 = "?";
      foreach (CraftUtil.SearchFilterGroup filterGroup in filterGroups)
      {
        foreach (CraftUtil.Filter filter in filterGroup.filters)
        {
          string empty3 = string.Empty;
          string str3;
          switch (filter.conditionType?.ToLower())
          {
            case "eq":
              str3 = "=";
              break;
            case "finset":
              str3 = "X";
              break;
            case "from":
              str3 = ">=";
              break;
            case "gt":
              str3 = ">";
              break;
            case "gteq":
              str3 = ">=";
              break;
            case "in":
              str3 = "in";
              break;
            case "like":
              str3 = "like";
              break;
            case "lt":
              str3 = "<";
              break;
            case "lteq":
              str3 = "<=";
              break;
            case "moreq":
              str3 = ">=";
              break;
            case "neq":
              str3 = "<>";
              break;
            case "nfinset":
              str3 = "not_in";
              break;
            case "nin":
              str3 = "not in";
              break;
            case "notnull":
              str3 = "not_null";
              break;
            case "null":
              str3 = "null";
              break;
            case "to":
              str3 = "<=";
              break;
            default:
              throw new Exception("\r\n\r\nBad Filter Condition: " + filter.conditionType + "\r\n\r\n");
          }
          if (str1 != string.Empty)
            str2 = "&";
          switch (filter.field)
          {
            case "created_at":
              switch (filter.conditionType)
              {
                case "from":
                  empty1 = filter.value;
                  break;
                case "to":
                  empty2 = filter.value;
                  break;
              }
              if (empty1 != string.Empty && empty2 != string.Empty)
              {
                str1 = str1 + str2 + "dateCreated=and,>=" + empty1.Replace(" 00:00:00", "") + ",<=" + empty2.Replace(" 00:00:00", "");
                break;
              }
              break;
            case "ordered_at":
              switch (filter.conditionType)
              {
                case "from":
                  empty1 = filter.value;
                  break;
                case "to":
                  empty2 = filter.value;
                  break;
              }
              if (empty1 != string.Empty && empty2 != string.Empty)
              {
                str1 = str1 + str2 + "dateOrdered=and,>=" + empty1.Replace(" 00:00:00", "") + ",<=" + empty2.Replace(" 00:00:00", "");
                break;
              }
              break;
            case "dateCompleted":
              switch (filter.conditionType)
              {
                case "from":
                  empty1 = filter.value;
                  break;
                case "to":
                  empty2 = filter.value;
                  break;
              }
              if (empty1 != string.Empty && empty2 != string.Empty)
              {
                str1 = str1 + str2 + "dateCompleted=and,>=" + empty1.Replace(" 00:00:00", "") + ",<=" + empty2.Replace(" 00:00:00", "");
                break;
              }
              break;
            default:
              str1 = str1 + str2 + filter.field + str3 + filter.value;
              break;
          }
        }
      }
      return str1;
    }

    private string webPost(string postURL, string postData, string method = "POST")
    {
      byte[] numArray = new byte[postData.Length];
      byte[] bytes = new UTF8Encoding().GetBytes(postData);
      CraftUtil.MyWebClient myWebClient = new CraftUtil.MyWebClient();
      myWebClient.Headers.Add("accept", "text/xml, text/plain, application/json");
      myWebClient.Headers.Add("Content-Type", "application/json");
      myWebClient.Headers.Add("Cache-Control", "no-cache");
      myWebClient.Headers.Add("Pragma", "no-cache");
      string apikey = Settings.Default.APIKEY;
      myWebClient.Headers.Add("Authorization", "Bearer " + apikey);
      return Encoding.UTF8.GetString(myWebClient.UploadData(postURL, method, bytes));
    }

    private class MyWebClient : WebClient
    {
      protected override WebRequest GetWebRequest(Uri uri)
      {
        WebRequest webRequest = base.GetWebRequest(uri);
        webRequest.Timeout = 600000;
        return webRequest;
      }
    }

    public class OrderFilter
    {
    }

    public class SearchFilterGroup
    {
      public CraftUtil.Filter[] filters;
    }

    public class Filter
    {
      public string field;
      public string conditionType;
      public string value;
    }
  }
}
