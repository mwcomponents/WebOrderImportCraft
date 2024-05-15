// Decompiled with JetBrains decompiler
// Type: WebOrderImportCraft.CSCExtendedAPI
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

namespace WebOrderImportCraft
{
  internal class CSCExtendedAPI
  {
    public CSCExtendedAPICustomShipping custom_shipping { get; set; }

    public string po_number { get; set; }

    public CSCExtendedAPILot[] lot_charges { get; set; }

    public string json { get; set; }
  }
}
