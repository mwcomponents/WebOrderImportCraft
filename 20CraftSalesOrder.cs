// Decompiled with JetBrains decompiler
// Type: CraftCMS.LineitemOption
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

using Newtonsoft.Json;

namespace CraftCMS
{
  public class LineitemOption
  {
    [JsonProperty("Optional Finish")]
    public string OptionalFinish { get; set; }

    public string order_type { get; set; }
  }
}
