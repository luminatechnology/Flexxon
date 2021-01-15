// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POReceiptExt
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using PX.Data;
using PX.Data.BQL;
using System;

namespace PX.Objects.PO
{
  public class POReceiptExt : PXCacheExtension<POReceipt>
  {
    [PXDBDate]
    [PXUIField(DisplayName = "Supplier Inv. Date", Visible = false)]
    [PXDefault(typeof (AccessInfo.businessDate))]
    public virtual DateTime? UsrSuppInvDate { get; set; }

    [PXDBString(30, IsUnicode = true)]
    [PXUIField(DisplayName = "Supplier Tracking Nbr.")]
    public virtual string UsrSuppTrackNbr { get; set; }

    [PXDecimal]
    [PXUIField(DisplayName = "Total Cost", IsReadOnly = true)]
    public virtual Decimal? UsrTotalCost { get; set; }

    public abstract class usrSuppInvDate : BqlType<IBqlDateTime, DateTime>.Field<POReceiptExt.usrSuppInvDate>
    {
    }

    public abstract class usrSuppTrackNbr : BqlType<IBqlString, string>.Field<POReceiptExt.usrSuppTrackNbr>
    {
    }

    public abstract class usrTotalCost : BqlType<IBqlDecimal, Decimal>.Field<POReceiptExt.usrTotalCost>
    {
    }
  }
}
