// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POOrderEntry_Extension
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using FlexxonCustomizations.Descriptor;
using PX.Common;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Objects.IN;

namespace PX.Objects.PO
{
  public class POOrderEntry_Extension : PXGraphExtension<POOrderEntry>
  {
    public FbqlSelect<SelectFromBase<FLXProjPurchDetails2, TypeArrayOf<IFbqlJoin>.Empty>, FLXProjPurchDetails2>.View ProjPurchDtls;

    public void UpdateSOLine(POOrderEntry.SOLineSplit3 split, int? vendorID, bool poCreated)
    {
      int? vendorId1 = split.VendorID;
      int? nullable1 = vendorID;
      bool flag1 = !(vendorId1.GetValueOrDefault() == nullable1.GetValueOrDefault() & vendorId1.HasValue == nullable1.HasValue);
      bool? poCreated1 = split.POCreated;
      bool flag2 = poCreated;
      bool flag3 = !(poCreated1.GetValueOrDefault() == flag2 & poCreated1.HasValue);
      if (!(flag1 | flag3))
        return;
      POOrderEntry.SOLine5 soLine5 = (POOrderEntry.SOLine5) this.Base.FixedDemandOrigSOLine.Select((object) split.OrderType, (object) split.OrderNbr, (object) split.LineNbr);
      bool flag4 = false;
      if (flag1)
      {
        split.VendorID = vendorID;
        int num;
        if (soLine5 != null)
        {
          int? vendorId2 = soLine5.VendorID;
          int? nullable2 = vendorID;
          num = !(vendorId2.GetValueOrDefault() == nullable2.GetValueOrDefault() & vendorId2.HasValue == nullable2.HasValue) ? 1 : 0;
        }
        else
          num = 0;
        if (num != 0)
        {
          soLine5.VendorID = vendorID;
          flag4 = true;
        }
      }
      if (flag3)
      {
        split.POCreated = new bool?(poCreated);
        int num;
        if (soLine5 != null)
        {
          bool? poCreated2 = soLine5.POCreated;
          bool flag5 = poCreated;
          num = !(poCreated2.GetValueOrDefault() == flag5 & poCreated2.HasValue) ? 1 : 0;
        }
        else
          num = 0;
        if (num != 0)
        {
          soLine5.POCreated = new bool?(poCreated);
          flag4 = true;
        }
      }
      if (flag4)
        this.Base.FixedDemandOrigSOLine.Cache.SetStatus((object) soLine5, PXEntryStatus.Updated);
    }

    [PXRemoveBaseAttribute(typeof (POSiteAvailAttribute))]
    [SiteAvail2(typeof (POLine.inventoryID), typeof (POLine.subItemID), typeof (POLine.branchID))]
    protected virtual void _(Events.CacheAttached<POLine.siteID> e)
    {
    }

    protected void _(Events.RowSelected<POLine> e, PXRowSelected invokeBaseHandler)
    {
      if (invokeBaseHandler != null)
        invokeBaseHandler(e.Cache, e.Args);
      PXUIFieldAttribute.SetEnabled<POLineExt.usrVendConfDate>(e.Cache, (object) e.Row, this.Base.CurrentDocument.Current.Status.IsIn<string>("H", "N"));
    }
  }
}
