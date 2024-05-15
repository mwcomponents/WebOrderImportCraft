// Decompiled with JetBrains decompiler
// Type: CraftCMS.Options
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

using System;

namespace CraftCMS
{
  public class Options
  {
    public string ServiceType { get; set; }

    public string ServiceDescription { get; set; }

    public string packagingType { get; set; }

    public string deliveryStation { get; set; }

    public string deliveryDayOfWeek { get; set; }

    public DateTime? deliveryTimestamp { get; set; }

    public string transitTime { get; set; }

    public string destinationAirportId { get; set; }

    public bool ineligibleForMoneyBackGuarantee { get; set; }

    public string originServiceArea { get; set; }

    public string destinationServiceArea { get; set; }
  }
}
