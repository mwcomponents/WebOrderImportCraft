// Decompiled with JetBrains decompiler
// Type: WebOrderImportCraft.RAFParts
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

using MsgBox;
using Synergy.BusinessObjects;
using Synergy.BusinessObjects.VE;
using Synergy.DistributedERP;
using System;
using System.Data;

namespace WebOrderImportCraft
{
  internal class RAFParts
  {
    internal string CreatePart(
      DatabaseConnection Database,
      string SiteID,
      string SKU,
      InventoryService inventorySvc)
    {
      string part1 = (string) null;
      if (SiteID == "RAF")
      {
        int length = SKU.LastIndexOf('-');
        if (length > SKU.Length - 4)
        {
          string str = SKU.Substring(0, length);
          if (inventorySvc.PartIDExists(str))
          {
            try
            {
              using (IDbConnection connection = Database.Connection)
              {
                try
                {
                  if (Database.Transaction == null)
                    Database.Transaction = connection.BeginTransaction();
                  PART part2 = new PART(Database).Load(str);
                  part2.ID = SKU.ToUpper();
                  part2.ORDER_POINT = new Decimal?();
                  part2.ORDER_UP_TO_QTY = new Decimal?();
                  part2.SAFETY_STOCK_QTY = new Decimal?();
                  part2.FIXED_ORDER_QTY = new Decimal?();
                  part2.DAYS_OF_SUPPLY = new Decimal?();
                  part2.MINIMUM_ORDER_QTY = new Decimal?();
                  part2.MAXIMUM_ORDER_QTY = new Decimal?();
                  part2.MFG_NAME = (string) null;
                  part2.MFG_PART_ID = (string) null;
                  part2.ABC_CODE = "C";
                  part2.ANNUAL_USAGE_QTY = new Decimal?();
                  part2.INVENTORY_LOCKED = "N";
                  part2.QTY_ON_HAND = new Decimal?(0M);
                  part2.QTY_AVAILABLE_ISS = new Decimal?(0M);
                  part2.QTY_AVAILABLE_MRP = new Decimal?(0M);
                  part2.QTY_ON_ORDER = new Decimal?(0M);
                  part2.QTY_IN_DEMAND = new Decimal?(0M);
                  part2.USER_6 = "Y";
                  part2.STATUS = "A";
                  part2.CREATE_DATE = new DateTime?(DateTime.Now);
                  part2.MODIFY_DATE = new DateTime?(DateTime.Now);
                  part2.Save(true);
                  PART_SITE partSite = new PART_SITE(Database).Load(SiteID, str);
                  partSite.SITE_ID = SiteID;
                  partSite.PART_ID = part2.ID;
                  partSite.PLANNING_LEADTIME = new Decimal?();
                  partSite.ORDER_POLICY = (string) null;
                  partSite.ORDER_POINT = new Decimal?();
                  partSite.ORDER_UP_TO_QTY = new Decimal?();
                  partSite.SAFETY_STOCK_QTY = new Decimal?();
                  partSite.FIXED_ORDER_QTY = new Decimal?();
                  partSite.DAYS_OF_SUPPLY = new Decimal?();
                  partSite.MINIMUM_ORDER_QTY = new Decimal?();
                  partSite.MAXIMUM_ORDER_QTY = new Decimal?();
                  partSite.ENGINEERING_MSTR = "PLT";
                  partSite.PRODUCT_CODE = (string) null;
                  partSite.ABC_CODE = "C";
                  partSite.ANNUAL_USAGE_QTY = new Decimal?();
                  partSite.INVENTORY_LOCKED = "N";
                  partSite.UNIT_PRICE = new Decimal?();
                  partSite.EFF_DATE_PRICE = (string) null;
                  partSite.UNIT_MATERIAL_COST = new Decimal?(0M);
                  partSite.UNIT_LABOR_COST = new Decimal?(0M);
                  partSite.WHSALE_UNIT_COST = new Decimal?();
                  partSite.BURDEN_PERCENT = new Decimal?(0M);
                  partSite.BURDEN_PER_UNIT = new Decimal?(0M);
                  partSite.EXCISE_UNIT_PRICE = new Decimal?();
                  partSite.PURC_BUR_PERCENT = new Decimal?(0M);
                  partSite.UNIT_BURDEN_COST = new Decimal?(0M);
                  partSite.FIXED_COST = new Decimal?(0M);
                  partSite.UNIT_SERVICE_COST = new Decimal?(0M);
                  partSite.NEW_MATERIAL_COST = new Decimal?(0M);
                  partSite.NEW_LABOR_COST = new Decimal?(0M);
                  partSite.NEW_BURDEN_COST = new Decimal?(0M);
                  partSite.NEW_SERVICE_COST = new Decimal?(0M);
                  partSite.NEW_BURDEN_PERCENT = new Decimal?(0M);
                  partSite.NEW_BURDEN_PERUNIT = new Decimal?(0M);
                  partSite.PURC_BUR_PER_UNIT = new Decimal?(0M);
                  partSite.NEW_FIXED_COST = new Decimal?(0M);
                  partSite.QTY_ON_HAND = new Decimal?(0M);
                  partSite.QTY_AVAILABLE_ISS = new Decimal?(0M);
                  partSite.QTY_AVAILABLE_MRP = new Decimal?(0M);
                  partSite.QTY_ON_ORDER = new Decimal?(0M);
                  partSite.QTY_IN_DEMAND = new Decimal?(0M);
                  partSite.QTY_COMMITTED = new Decimal?(0M);
                  partSite.STATUS = "A";
                  partSite.STATUS_EFF_DATE = new DateTime?(DateTime.Now);
                  partSite.USE_SUPPLY_BEF_LT = (string) null;
                  partSite.MINIMUM_LEADTIME = new Decimal?();
                  partSite.LEADTIME_BUFFER = new Decimal?();
                  partSite.EMERGENCY_STOCKPCT = new Decimal?();
                  partSite.REPLENISH_LEVEL = new Decimal?();
                  partSite.MIN_BATCH_SIZE = new Decimal?();
                  partSite.YELLOW_STOCKPCT = new Decimal?();
                  partSite.MRP_EXCEPTION_INFO = (string) null;
                  partSite.MULTIPLE_ORDER_QTY = new Decimal?();
                  partSite.LAST_IMPLODE_DATE = new DateTime?();
                  partSite.MRO_CLASS = (string) null;
                  partSite.USER_6 = "Y";
                  partSite.BUFFER_PROFILE_ID = (string) null;
                  partSite.LAST_ABC_DATE = new DateTime?();
                  partSite.CREATE_DATE = new DateTime?(DateTime.Now);
                  partSite.MODIFY_DATE = new DateTime?();
                  partSite.Save(true);
                  TRACE_PROFILE traceProfile = new TRACE_PROFILE(Database).Load(part2.ID, SiteID);
                  if (traceProfile.RowState != DataRowState.Added)
                  {
                    traceProfile.APPLY_TO_REC = "Y";
                    traceProfile.APPLY_TO_ISSUE = "Y";
                    traceProfile.APPLY_TO_ADJ = "N";
                    traceProfile.APPLY_TO_LABOR = "N";
                    traceProfile.TRACE_ID_LABEL = "LOT ID";
                    traceProfile.PRE_ASSIGN = "N";
                    traceProfile.ASSIGN_METHOD = "U";
                    traceProfile.APROPERTY_1_REQD = "N";
                    traceProfile.APROPERTY_2_REQD = "N";
                    traceProfile.APROPERTY_3_REQD = "N";
                    traceProfile.APROPERTY_4_REQD = "N";
                    traceProfile.APROPERTY_5_REQD = "N";
                    traceProfile.NPROPERTY_1_REQD = "N";
                    traceProfile.NPROPERTY_2_REQD = "N";
                    traceProfile.NPROPERTY_3_REQD = "N";
                    traceProfile.NPROPERTY_4_REQD = "N";
                    traceProfile.NPROPERTY_5_REQD = "N";
                    traceProfile.APROPERTY_1_EDIT = "N";
                    traceProfile.APROPERTY_2_EDIT = "N";
                    traceProfile.APROPERTY_3_EDIT = "N";
                    traceProfile.APROPERTY_4_EDIT = "N";
                    traceProfile.APROPERTY_5_EDIT = "N";
                    traceProfile.NPROPERTY_1_EDIT = "N";
                    traceProfile.NPROPERTY_2_EDIT = "N";
                    traceProfile.NPROPERTY_3_EDIT = "N";
                    traceProfile.NPROPERTY_4_EDIT = "N";
                    traceProfile.NPROPERTY_5_EDIT = "N";
                    traceProfile.APROPERTY_1_VIS = "N";
                    traceProfile.APROPERTY_2_VIS = "N";
                    traceProfile.APROPERTY_3_VIS = "N";
                    traceProfile.APROPERTY_4_VIS = "N";
                    traceProfile.APROPERTY_5_VIS = "N";
                    traceProfile.NPROPERTY_1_VIS = "N";
                    traceProfile.NPROPERTY_2_VIS = "N";
                    traceProfile.NPROPERTY_3_VIS = "N";
                    traceProfile.NPROPERTY_4_VIS = "N";
                    traceProfile.NPROPERTY_5_VIS = "N";
                    traceProfile.AUTO_FILL_TRACE = "A";
                    traceProfile.EDIT_EXP_DATE = "N";
                    traceProfile.APPLY_TO_SERVDISP = "N";
                    traceProfile.APPLY_TO_SERVREC = "N";
                    traceProfile.OWNERSHIP = "N";
                    traceProfile.LOT = "N";
                    traceProfile.SERIAL = "N";
                    traceProfile.COLOCATE_OWNERS = "";
                    traceProfile.COLOCATE_LOTS = "N";
                    traceProfile.EXPIRATION = "N";
                    traceProfile.COLOCATE_ALPHAS = "N";
                    traceProfile.COLOCATE_NUMERICS = "N";
                    traceProfile.ACCEPT_EXPIRED_RCV = "N";
                    traceProfile.OWNERSHIP_KNOWN = "N";
                    traceProfile.LOT_KNOWN = "N";
                    traceProfile.SERIAL_KNOWN = "N";
                    traceProfile.EXPIRATION_KNOWN = "N";
                    traceProfile.APROPERTY_1_KNOWN = "N";
                    traceProfile.APROPERTY_2_KNOWN = "N";
                    traceProfile.APROPERTY_3_KNOWN = "N";
                    traceProfile.APROPERTY_4_KNOWN = "N";
                    traceProfile.APROPERTY_5_KNOWN = "N";
                    traceProfile.NPROPERTY_1_KNOWN = "N";
                    traceProfile.NPROPERTY_2_KNOWN = "N";
                    traceProfile.NPROPERTY_3_KNOWN = "N";
                    traceProfile.NPROPERTY_4_KNOWN = "N";
                    traceProfile.NPROPERTY_5_KNOWN = "N";
                    traceProfile.COUNT_DETAIL = "N";
                    traceProfile.PRODUCTION = "N";
                    traceProfile.PRODUCTION_KNOWN = "N";
                    traceProfile.COLOCATE_PROD = "N";
                    traceProfile.RECEIVE_BY = "N";
                    traceProfile.RECEIVE_BY_KNOWN = "N";
                    traceProfile.COLOCATE_REC_BY = "N";
                    traceProfile.AVAILABLE = "N";
                    traceProfile.AVAILABLE_KNOWN = "N";
                    traceProfile.COLOCATE_AVAILABLE = "N";
                    traceProfile.SHIP_BY = "N";
                    traceProfile.SHIP_BY_KNOWN = "N";
                    traceProfile.COLOCATE_SHIP_BY = "N";
                    traceProfile.Save();
                  }
                  part1 = SKU.ToUpper();
                  Database.Transaction.Commit();
                  Database.Transaction.Dispose();
                  Database.Transaction = (IDbTransaction) null;
                }
                catch (Exception ex)
                {
                  try
                  {
                    Database.Transaction.Rollback();
                    Database.Transaction.Dispose();
                  }
                  catch
                  {
                  }
                  Database.Transaction = (IDbTransaction) null;
                  int num = (int) MBox.Show("Can't create part: " + SKU.ToUpper() + "/r/n" + ex.ToString());
                }
              }
            }
            finally
            {
              try
              {
                Database.Transaction.Dispose();
              }
              catch
              {
              }
              Database.Transaction = (IDbTransaction) null;
            }
          }
        }
      }
      return part1;
    }
  }
}
