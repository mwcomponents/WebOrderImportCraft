// Decompiled with JetBrains decompiler
// Type: WebOrderImportCraft.QuotePrefixMap
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

using System.Collections.Generic;

namespace WebOrderImportCraft
{
  public static class QuotePrefixMap
  {
    public static Dictionary<string, string> _prefixMapDictionary = (Dictionary<string, string>) null;
    private static char[] brandSep = new char[1]{ '/' };

    public static string Get(string key)
    {
      if (!string.IsNullOrEmpty(key) && QuotePrefixMap._prefixMapDictionary != null && QuotePrefixMap._prefixMapDictionary.ContainsKey(key))
      {
        string empty = string.Empty;
        if (QuotePrefixMap._prefixMapDictionary.TryGetValue(key, out empty) && !string.IsNullOrWhiteSpace(empty))
          return empty;
      }
      return key;
    }

    public static Dictionary<string, string> prefixMapDictionary
    {
      get
      {
        if (QuotePrefixMap._prefixMapDictionary == null)
          QuotePrefixMap._prefixMapDictionary = new Dictionary<string, string>();
        return QuotePrefixMap._prefixMapDictionary;
      }
    }
  }
}
