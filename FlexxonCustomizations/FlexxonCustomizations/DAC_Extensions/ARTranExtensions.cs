// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARTranExt
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.CS;
using System;

namespace PX.Objects.AR
{
  public class ARTranExt : PXCacheExtension<ARTran>
  {
    [PXDBString(15, IsUnicode = true)]
    [PXUIField(DisplayName = "Commission ID")]
    public virtual string UsrCommissionID { get; set; }

    [PXDBBool]
    [PXUIField(DisplayName = "Commission Created")]
    public virtual bool? UsrComisionCreated { get; set; }

    [PXDBCurrency(typeof (ARTran.curyInfoID), typeof (ARTran.tranAmt), BaseCalc = false)]
    [PXUIField(DisplayName = "Commission Amt")]
    public virtual Decimal? UsrCommissionAmt { get; set; }

    [PXDBCurrency(typeof (Search<CommonSetup.decPlPrcCst>), typeof (ARTran.curyInfoID), typeof (ARTran.unitPrice))]
    [PXUIField(DisplayName = "PI Unit Price", Visibility = PXUIVisibility.SelectorVisible)]
    [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
    public virtual Decimal? UsrPIUnitPrice { get; set; }

    public abstract class usrCommissionID : BqlType<IBqlString, string>.Field<ARTranExt.usrCommissionID>
    {
    }

    public abstract class usrComisionCreated : BqlType<IBqlBool, bool>.Field<ARTranExt.usrComisionCreated>
    {
    }

    public abstract class usrCommissionAmt : BqlType<IBqlDecimal, Decimal>.Field<ARTranExt.usrCommissionAmt>
    {
    }

    public abstract class usrPIUnitPrice : BqlType<IBqlDecimal, Decimal>.Field<ARTranExt.usrPIUnitPrice>
    {
    }
  }
}
