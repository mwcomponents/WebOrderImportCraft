// Decompiled with JetBrains decompiler
// Type: CraftCMS2.User
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

using System;

namespace CraftCMS2
{
  public class User
  {
    public string username { get; set; }

    public object photoId { get; set; }

    public string firstName { get; set; }

    public string lastName { get; set; }

    public string email { get; set; }

    public object password { get; set; }

    public string admin { get; set; }

    public string locked { get; set; }

    public string suspended { get; set; }

    public string pending { get; set; }

    public DateTime lastLoginDate { get; set; }

    public object invalidLoginCount { get; set; }

    public object lastInvalidLoginDate { get; set; }

    public object lockoutDate { get; set; }

    public string hasDashboard { get; set; }

    public bool passwordResetRequired { get; set; }

    public object lastPasswordChangeDate { get; set; }

    public object unverifiedEmail { get; set; }

    public object newPassword { get; set; }

    public object currentPassword { get; set; }

    public object verificationCodeIssuedDate { get; set; }

    public object verificationCode { get; set; }

    public object lastLoginAttemptIp { get; set; }

    public object authError { get; set; }

    public object inheritorOnDelete { get; set; }

    public int id { get; set; }

    public object tempId { get; set; }

    public object draftId { get; set; }

    public object revisionId { get; set; }

    public string uid { get; set; }

    public int siteSettingsId { get; set; }

    public object fieldLayoutId { get; set; }

    public int contentId { get; set; }

    public bool enabled { get; set; }

    public bool archived { get; set; }

    public int siteId { get; set; }

    public object title { get; set; }

    public object slug { get; set; }

    public object uri { get; set; }

    public DateTime dateCreated { get; set; }

    public DateTime dateUpdated { get; set; }

    public object dateDeleted { get; set; }

    public bool trashed { get; set; }

    public string _ref { get; set; }

    public string status { get; set; }

    public object structureId { get; set; }

    public object url { get; set; }

    public object cooldownEndTime { get; set; }

    public string friendlyName { get; set; }

    public string fullName { get; set; }

    public bool isCurrent { get; set; }

    public string name { get; set; }

    public string preferredLanguage { get; set; }

    public object remainingCooldownTime { get; set; }
  }
}
