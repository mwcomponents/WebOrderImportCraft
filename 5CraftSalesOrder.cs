// Decompiled with JetBrains decompiler
// Type: CraftCMS.User
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

using System;

namespace CraftCMS
{
  public class User
  {
    public string username { get; set; }

    public int? photoId { get; set; }

    public string firstName { get; set; }

    public string lastName { get; set; }

    public string email { get; set; }

    public string password { get; set; }

    public string admin { get; set; }

    public string locked { get; set; }

    public string suspended { get; set; }

    public string pending { get; set; }

    public DateTime? lastLoginDate { get; set; }

    public int? invalidLoginCount { get; set; }

    public DateTime? lastInvalidLoginDate { get; set; }

    public DateTime? lockoutDate { get; set; }

    public string hasDashboard { get; set; }

    public bool passwordResetRequired { get; set; }

    public DateTime? lastPasswordChangeDate { get; set; }

    public string unverifiedEmail { get; set; }

    public string newPassword { get; set; }

    public string currentPassword { get; set; }

    public DateTime? verificationCodeIssuedDate { get; set; }

    public string verificationCode { get; set; }

    public string lastLoginAttemptIp { get; set; }

    public string authError { get; set; }

    public int? inheritorOnDelete { get; set; }

    public int id { get; set; }

    public int? tempId { get; set; }

    public int? draftId { get; set; }

    public int? revisionId { get; set; }

    public string uid { get; set; }

    public int siteSettingsId { get; set; }

    public int? fieldLayoutId { get; set; }

    public int contentId { get; set; }

    public bool enabled { get; set; }

    public bool archived { get; set; }

    public int siteId { get; set; }

    public string title { get; set; }

    public string slug { get; set; }

    public string uri { get; set; }

    public DateTime dateCreated { get; set; }

    public DateTime dateUpdated { get; set; }

    public DateTime? dateDeleted { get; set; }

    public bool trashed { get; set; }

    public string _ref { get; set; }

    public string status { get; set; }

    public int? structureId { get; set; }

    public string url { get; set; }

    public Avataxcustomerusagetype avataxCustomerUsageType { get; set; }

    public object pricingTier { get; set; }

    public bool termsCustomer { get; set; }

    public string company { get; set; }

    public string phone { get; set; }

    public string visualId { get; set; }

    public DateTime? cooldownEndTime { get; set; }

    public string friendlyName { get; set; }

    public string fullName { get; set; }

    public bool isCurrent { get; set; }

    public string name { get; set; }

    public string preferredLanguage { get; set; }

    public DateTime? remainingCooldownTime { get; set; }
  }
}
