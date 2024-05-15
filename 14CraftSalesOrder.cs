// Decompiled with JetBrains decompiler
// Type: CraftCMS.Provider
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

using System;

namespace CraftCMS
{
  public class Provider
  {
    public string weightUnit { get; set; }

    public string dimensionUnit { get; set; }

    public string name { get; set; }

    public string handle { get; set; }

    public bool enabled { get; set; }

    public string[] settings { get; set; }

    public Decimal? markUpRate { get; set; }

    public string markUpBase { get; set; }

    public string[] services { get; set; }

    public string restrictServices { get; set; }

    public string packingMethod { get; set; }

    public string boxSizes { get; set; }

    public int? id { get; set; }

    public DateTime? dateCreated { get; set; }

    public DateTime? dateUpdated { get; set; }
  }
}
