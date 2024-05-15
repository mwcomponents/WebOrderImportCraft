// Decompiled with JetBrains decompiler
// Type: WebOrderImportCraft.SalesShippingInfo
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

using CraftCMS;

namespace WebOrderImportCraft
{
  internal class SalesShippingInfo
  {
    public Order WebSO;

    internal SalesShippingInfo(Order webSO) => this.WebSO = webSO;

    public virtual string GetShippingInfo(string field) => "";
  }
}
