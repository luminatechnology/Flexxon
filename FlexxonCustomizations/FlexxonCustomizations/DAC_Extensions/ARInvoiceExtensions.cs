// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARInvoiceExt
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using PX.Data;
using PX.Data.BQL;
using PX.Objects.SO;
using System;

namespace PX.Objects.AR
{
  public class ARInvoiceExt : PXCacheExtension<ARInvoice>
  {
    [PXDecimal]
    public virtual Decimal? UsrXRate
    {
      [PXDependsOnFields(new System.Type[] {typeof (ARInvoice.curyID), typeof (ARInvoice.docDate)})] get => SOShipmentEntry_Extension.GetCurrencyRate(this.Base.CuryID, this.Base.DocDate);
    }

    public abstract class usrXRate : BqlType<IBqlDecimal, Decimal>.Field<ARInvoiceExt.usrXRate>
    {
    }
  }
}
