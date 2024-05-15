// Decompiled with JetBrains decompiler
// Type: CraftCMS.QuoteUpdateReq
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

namespace CraftCMS
{
  public class QuoteUpdateReq
  {
    public string stage { get; set; }

    public QLineUpdate[] lineItems { get; set; }

    public string salesRepName { get; set; }

    public string salesRepPhone { get; set; }

    public string salesRepEmail { get; set; }

    public string visualQuoteId { get; set; }

    public string dateExpires { get; set; }
  }
}
