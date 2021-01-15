// Decompiled with JetBrains decompiler
// Type: FlexxonCustomizations.Descriptor.LSSOLine2POLine
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using PX.Data;
using PX.Objects.IN;
using PX.Objects.IN.Overrides.INDocumentRelease;
using PX.Objects.PO;
using PX.Objects.SO;
using System;

namespace FlexxonCustomizations.Descriptor
{
  public class LSSOLine2POLine : LSSelect<PX.Objects.SO.SOLine, SOLineSplit, Where2<Where<PX.Objects.SO.SOOrder.orderType, Equal<Current<POCreate.POCreateFilter.orderType>>, Or<Current<POCreate.POCreateFilter.orderNbr>, IsNull>>, And<Where<PX.Objects.SO.SOOrder.orderNbr, Equal<Current<POCreate.POCreateFilter.orderNbr>>, Or<Current<POCreate.POCreateFilter.orderNbr>, IsNull>>>>>
  {
    public LSSOLine2POLine(PXGraph graph)
      : base(graph)
      => this.MasterQtyField = typeof (PX.Objects.SO.SOLine.orderQty);

    public override void Availability_FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
    {
      PX.Objects.SO.SOLine row = (PX.Objects.SO.SOLine) e.Row;
      int num1;
      if (row == null)
      {
        num1 = 0;
      }
      else
      {
        bool? completed = row.Completed;
        bool flag = true;
        num1 = completed.GetValueOrDefault() == flag & completed.HasValue ? 1 : 0;
      }
      AvailabilityFetchMode availabilityFetchMode = num1 != 0 ? AvailabilityFetchMode.None : AvailabilityFetchMode.ExcludeCurrent;
      IQtyAllocated availability = this.AvailabilityFetch(sender, (ILSMaster) e.Row, availabilityFetchMode | AvailabilityFetchMode.TryOptimize);
      if (availability != null)
      {
        this.ReadInventoryItem(sender, ((PX.Objects.SO.SOLine) e.Row).InventoryID);
        Decimal num2 = INUnitAttribute.ConvertFromBase<PX.Objects.SO.SOLine.inventoryID, PX.Objects.SO.SOLine.uOM>(sender, e.Row, 1M, INPrecision.NOROUND);
        IQtyAllocated qtyAllocated1 = availability;
        Decimal? nullable1 = availability.QtyOnHand;
        Decimal? nullable2 = new Decimal?(PXDBQuantityAttribute.Round(new Decimal?(nullable1.Value * num2)));
        qtyAllocated1.QtyOnHand = nullable2;
        IQtyAllocated qtyAllocated2 = availability;
        nullable1 = availability.QtyAvail;
        Decimal? nullable3 = new Decimal?(PXDBQuantityAttribute.Round(new Decimal?(nullable1.Value * num2)));
        qtyAllocated2.QtyAvail = nullable3;
        IQtyAllocated qtyAllocated3 = availability;
        nullable1 = availability.QtyNotAvail;
        Decimal? nullable4 = new Decimal?(PXDBQuantityAttribute.Round(new Decimal?(nullable1.Value * num2)));
        qtyAllocated3.QtyNotAvail = nullable4;
        IQtyAllocated qtyAllocated4 = availability;
        nullable1 = availability.QtyHardAvail;
        Decimal? nullable5 = new Decimal?(PXDBQuantityAttribute.Round(new Decimal?(nullable1.Value * num2)));
        qtyAllocated4.QtyHardAvail = nullable5;
        e.ReturnValue = (object) PXMessages.LocalizeFormatNoPrefix("On Hand {1} {0}, Available {2} {0}, Available for Shipping {3} {0}", sender.GetValue<PX.Objects.SO.SOLine.uOM>(e.Row), (object) this.FormatQty(availability.QtyOnHand), (object) this.FormatQty(availability.QtyAvail), (object) this.FormatQty(availability.QtyHardAvail));
        this.AvailabilityCheck(sender, (ILSMaster) e.Row, availability);
      }
      else
      {
        INUnitAttribute.ConvertFromBase<PX.Objects.SO.SOLine.inventoryID, PX.Objects.SO.SOLine.uOM>(sender, e.Row, 0M, INPrecision.QUANTITY);
        e.ReturnValue = (object) string.Empty;
      }
      base.Availability_FieldSelecting(sender, e);
    }

    public override SOLineSplit Convert(PX.Objects.SO.SOLine item)
    {
      using (new LSSelect<PX.Objects.SO.SOLine, SOLineSplit, Where2<Where<PX.Objects.SO.SOOrder.orderType, Equal<Current<POCreate.POCreateFilter.orderType>>, Or<Current<POCreate.POCreateFilter.orderNbr>, IsNull>>, And<Where<PX.Objects.SO.SOOrder.orderNbr, Equal<Current<POCreate.POCreateFilter.orderNbr>>, Or<Current<POCreate.POCreateFilter.orderNbr>, IsNull>>>>>.InvtMultScope<PX.Objects.SO.SOLine>(item))
      {
        SOLineSplit soLineSplit1 = (SOLineSplit) item;
        SOLineSplit soLineSplit2 = soLineSplit1;
        Decimal? baseQty = item.BaseQty;
        Decimal? unassignedQty = item.UnassignedQty;
        Decimal? nullable = baseQty.HasValue & unassignedQty.HasValue ? new Decimal?(baseQty.GetValueOrDefault() - unassignedQty.GetValueOrDefault()) : new Decimal?();
        soLineSplit2.BaseQty = nullable;
        return soLineSplit1;
      }
    }
  }
}
