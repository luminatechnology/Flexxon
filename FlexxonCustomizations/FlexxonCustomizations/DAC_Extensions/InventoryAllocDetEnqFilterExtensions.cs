// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryAllocDetEnqFilterExt
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using PX.Data;
using PX.Data.BQL;
using System;

namespace PX.Objects.IN
{
  public class InventoryAllocDetEnqFilterExt : PXCacheExtension<InventoryAllocDetEnqFilter>
  {
    [PXDBQuantity]
    [PXUIField(DisplayName = "Available(+)", Enabled = false)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? UsrAvailablePlus { get; set; }

    public abstract class usrAvailablePlus : BqlType<IBqlDecimal, Decimal>.Field<InventoryAllocDetEnqFilterExt.usrAvailablePlus>
    {
    }
  }
}
