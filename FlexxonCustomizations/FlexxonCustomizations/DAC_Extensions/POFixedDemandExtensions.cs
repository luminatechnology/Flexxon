// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POFixedDemandExt
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using PX.Data;
using PX.Data.BQL;
using System;

namespace PX.Objects.PO
{
  public class POFixedDemandExt : PXCacheExtension<POFixedDemand>
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

    [PXDecimal]
    [PXUIField(DisplayName = "Qty Available(+)-All WH")]
    public virtual Decimal? UsrQtyAvailPlus { get; set; }

    public abstract class usrOnHand : BqlType<IBqlDecimal, Decimal>.Field<POFixedDemandExt.usrOnHand>
    {
    }

    public abstract class usrAvailability : BqlType<IBqlDecimal, Decimal>.Field<POFixedDemandExt.usrAvailability>
    {
    }

    public abstract class usrQtyAvailShipping : BqlType<IBqlDecimal, Decimal>.Field<POFixedDemandExt.usrQtyAvailShipping>
    {
    }

    public abstract class usrQtyAvailPlus : BqlType<IBqlDecimal, Decimal>.Field<POFixedDemandExt.usrQtyAvailPlus>
    {
    }
  }
}
