// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOShipmentPlanExt
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using PX.Data;
using PX.Data.BQL;
using System;

namespace PX.Objects.SO
{
  public class SOShipmentPlanExt : PXCacheExtension<SOShipmentPlan>
  {
    [PXDecimal]
    [PXUIField(DisplayName = "On Hand-All WH")]
    public virtual Decimal? UsrOnHand { get; set; }

    [PXDecimal]
    [PXUIField(DisplayName = "Qty. Available-All WH")]
    public virtual Decimal? UsrAvailability { get; set; }

    [PXDecimal]
    [PXUIField(DisplayName = "Available For Shipping-All WH")]
    public virtual Decimal? UsrQtyAvailShipping { get; set; }

    public abstract class usrOnHand : BqlType<IBqlDecimal, Decimal>.Field<SOShipmentPlanExt.usrOnHand>
    {
    }

    public abstract class usrAvailability : BqlType<IBqlDecimal, Decimal>.Field<SOShipmentPlanExt.usrAvailability>
    {
    }

    public abstract class usrQtyAvailShipping : BqlType<IBqlDecimal, Decimal>.Field<SOShipmentPlanExt.usrQtyAvailShipping>
    {
    }
  }
}
