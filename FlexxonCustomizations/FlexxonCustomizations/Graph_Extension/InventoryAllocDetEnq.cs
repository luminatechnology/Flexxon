// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryAllocDetEnq_Extension
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using PX.Data;
using System;

namespace PX.Objects.IN
{
  public class InventoryAllocDetEnq_Extension : PXGraphExtension<InventoryAllocDetEnq>
  {
    protected void _(
      Events.FieldSelecting<InventoryAllocDetEnqFilterExt.usrAvailablePlus> e)
    {
      InventoryAllocDetEnqFilter current = this.Base.Filter.Current;
      if (current == null)
        return;
      Events.FieldSelecting<InventoryAllocDetEnqFilterExt.usrAvailablePlus> fieldSelecting = e;
      Decimal? qtyAvail = current.QtyAvail;
      Decimal? qtyPoFixedOrders = current.QtyPOFixedOrders;
      Decimal? nullable1 = qtyAvail.HasValue & qtyPoFixedOrders.HasValue ? new Decimal?(qtyAvail.GetValueOrDefault() + qtyPoFixedOrders.GetValueOrDefault()) : new Decimal?();
      Decimal? qtyPoFixedReceipts = current.QtyPOFixedReceipts;
      Decimal? nullable2 = nullable1.HasValue & qtyPoFixedReceipts.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + qtyPoFixedReceipts.GetValueOrDefault()) : new Decimal?();
      Decimal? qtySoFixed = current.QtySOFixed;
      // ISSUE: variable of a boxed type
      Decimal? local =(nullable2.HasValue & qtySoFixed.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - qtySoFixed.GetValueOrDefault()) : new Decimal?());
      fieldSelecting.ReturnValue = (object) local;
    }
  }
}
