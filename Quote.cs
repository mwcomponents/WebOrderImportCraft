// Decompiled with JetBrains decompiler
// Type: CraftCMS.Quote
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

namespace CraftCMS
{
  public class Quote
  {
    public int quoteId { get; set; }

    public string number { get; set; }

    public string type { get; set; }

    public string stage { get; set; }

    public string visualOrderId { get; set; }

    public string visualCustomerId { get; set; }

    public int? userId { get; set; }

    public bool isCompleted { get; set; }

    public Datecompleted dateCompleted { get; set; }

    public string email { get; set; }

    public string phone { get; set; }

    public string firstName { get; set; }

    public string lastName { get; set; }

    public string company { get; set; }

    public string comments { get; set; }

    public QLineitem[] lineItems { get; set; }

    public string salesRepName { get; set; }

    public string salesRepPhone { get; set; }

    public string salesRepEmail { get; set; }

    public string visualQuoteId { get; set; }

    public string dateExpires { get; set; }
  }
}
