// Decompiled with JetBrains decompiler
// Type: WebOrderImportCraft.SmallOrderSvcChg
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

using System.Collections.Generic;

namespace WebOrderImportCraft
{
  public static class SmallOrderSvcChg
  {
    public static Dictionary<string, string> _smallOrderSvcChgDictionary;

    public static string Get(string key)
    {
      if (SmallOrderSvcChg._smallOrderSvcChgDictionary != null && SmallOrderSvcChg._smallOrderSvcChgDictionary.ContainsKey(key))
      {
        string empty = string.Empty;
        if (SmallOrderSvcChg._smallOrderSvcChgDictionary.TryGetValue(key, out empty) && !string.IsNullOrWhiteSpace(empty))
          return empty;
      }
      return key;
    }

    public static Dictionary<string, string> smallOrderSvcChgDictionary
    {
      get
      {
        if (SmallOrderSvcChg._smallOrderSvcChgDictionary == null)
          SmallOrderSvcChg._smallOrderSvcChgDictionary = new Dictionary<string, string>();
        return SmallOrderSvcChg._smallOrderSvcChgDictionary;
      }
    }
  }
}
