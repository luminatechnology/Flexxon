// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOLineSplitFilter
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using System;

namespace PX.Objects.SO
{
  [PXCacheName("SO Line Spilt Filter")]
  public class SOLineSplitFilter : IBqlTable
  {
    [PXDBQuantity]
    [PXUIField(DisplayName = "Split Qty.", Required = true)]
    [PXDefault]
    public virtual Decimal? SplitQty { get; set; }

    [PXDBDate]
    [PXUIField(DisplayName = "Ship Date")]
    [PXDefault(typeof (SOLine.shipDate))]
    public virtual DateTime? ShipDate { get; set; }

    public abstract class splitQty : BqlType<IBqlDecimal, Decimal>.Field<SOLineSplitFilter.splitQty>
    {
    }

    public abstract class shipDate : BqlType<IBqlDateTime, DateTime>.Field<SOLineSplitFilter.shipDate>
    {
    }
  }
}
