// Decompiled with JetBrains decompiler
// Type: WebOrderImportCraft.TaxSvcChg
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

using System.Collections.Generic;

namespace WebOrderImportCraft
{
  public static class TaxSvcChg
  {
    public static Dictionary<string, string> _taxSvcChgDictionary;

    public static string Get(string key)
    {
      if (TaxSvcChg._taxSvcChgDictionary != null && TaxSvcChg._taxSvcChgDictionary.ContainsKey(key))
      {
        string empty = string.Empty;
        if (TaxSvcChg._taxSvcChgDictionary.TryGetValue(key, out empty) && !string.IsNullOrWhiteSpace(empty))
          return empty;
      }
      return key;
    }

    public static Dictionary<string, string> taxSvcChgDictionary
    {
      get
      {
        if (TaxSvcChg._taxSvcChgDictionary == null)
          TaxSvcChg._taxSvcChgDictionary = new Dictionary<string, string>();
        return TaxSvcChg._taxSvcChgDictionary;
      }
    }
  }
}
