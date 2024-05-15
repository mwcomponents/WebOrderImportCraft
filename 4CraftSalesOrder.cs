// Decompiled with JetBrains decompiler
// Type: CraftCMS.Customer
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

namespace CraftCMS
{
  public class Customer
  {
    public string id { get; set; }

    public string userId { get; set; }

    public string primaryBillingAddressId { get; set; }

    public string primaryShippingAddressId { get; set; }

    public User user { get; set; }
  }
}
