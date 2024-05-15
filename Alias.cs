// Decompiled with JetBrains decompiler
// Type: WebOrderImportCraft.Alias
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

using System.Collections.Generic;

namespace WebOrderImportCraft
{
  public static class Alias
  {
    public static Dictionary<string, string> _aliasDictionary = (Dictionary<string, string>) null;
    private static char[] brandSep = new char[1]{ '/' };

    public static string Get(string key, bool returnBrand = false)
    {
      if (!string.IsNullOrEmpty(key) && Alias._aliasDictionary != null && Alias._aliasDictionary.ContainsKey(key))
      {
        string empty = string.Empty;
        if (Alias._aliasDictionary.TryGetValue(key, out empty) && !string.IsNullOrWhiteSpace(empty))
        {
          string[] strArray = empty.Split(Alias.brandSep, 2);
          return strArray.Length == 2 && returnBrand ? strArray[1] : strArray[0];
        }
      }
      return key;
    }

    public static Dictionary<string, string> aliasDictionary
    {
      get
      {
        if (Alias._aliasDictionary == null)
          Alias._aliasDictionary = new Dictionary<string, string>();
        return Alias._aliasDictionary;
      }
    }
  }
}
