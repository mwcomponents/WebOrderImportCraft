// Decompiled with JetBrains decompiler
// Type: WebOrderImportCraft.OrderStatusDefault
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

using System.Collections.Generic;

namespace WebOrderImportCraft
{
  public static class OrderStatusDefault
  {
    public static Dictionary<string, string> _orderStatusDictionary;

    public static string Get(string key)
    {
      if (OrderStatusDefault._orderStatusDictionary != null && OrderStatusDefault._orderStatusDictionary.ContainsKey(key))
      {
        string empty = string.Empty;
        if (OrderStatusDefault._orderStatusDictionary.TryGetValue(key, out empty) && !string.IsNullOrWhiteSpace(empty))
          return empty;
      }
      return "HOLD";
    }

    public static Dictionary<string, string> orderStatusDictionary
    {
      get
      {
        if (OrderStatusDefault._orderStatusDictionary == null)
          OrderStatusDefault._orderStatusDictionary = new Dictionary<string, string>();
        return OrderStatusDefault._orderStatusDictionary;
      }
    }
  }
}
