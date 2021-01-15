// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOLineSplitExt
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using System;

namespace PX.Objects.SO
{
  public class SOLineSplitExt : PXCacheExtension<SOLineSplit>
  {
    [PXString(2, IsUnicode = true)]
    [PXUIField(DisplayName = "COO", IsReadOnly = true)]
    [PXDBScalar(typeof (Search<INTranSplitExt.usrCOO, Where<INTranSplit.lotSerialNbr, Equal<SOLineSplit.lotSerialNbr>, And<INTranSplit.inventoryID, Equal<SOLineSplit.inventoryID>, And<INTranSplit.docType, Equal<INDocType.receipt>>>>>))]
    [PXUnboundDefault(typeof (Search<INTranSplitExt.usrCOO, Where<INTranSplit.lotSerialNbr, Equal<Current<SOLineSplit.lotSerialNbr>>, And<INTranSplit.inventoryID, Equal<Current<SOLineSplit.inventoryID>>>>>))]
    [PXFormula(typeof (Default<SOLineSplit.lotSerialNbr>))]
    public virtual string UsrCOO { get; set; }

    [PXString(6, IsUnicode = true)]
    [PXUIField(DisplayName = "Date Code", IsReadOnly = true)]
    [PXDBScalar(typeof (Search<INTranSplitExt.usrDateCode, Where<INTranSplit.lotSerialNbr, Equal<SOLineSplit.lotSerialNbr>, And<INTranSplit.inventoryID, Equal<SOLineSplit.inventoryID>, And<INTranSplit.docType, Equal<INDocType.receipt>>>>>))]
    [PXUnboundDefault(typeof (Search<INTranSplitExt.usrDateCode, Where<INTranSplit.lotSerialNbr, Equal<Current<SOLineSplit.lotSerialNbr>>, And<INTranSplit.inventoryID, Equal<Current<SOLineSplit.inventoryID>>>>>))]
    [PXFormula(typeof (Default<SOLineSplit.lotSerialNbr>))]
    public virtual string UsrDateCode { get; set; }

    [PXDate]
    [PXUIField(DisplayName = "IN Tran Date", IsReadOnly = true)]
    [PXDBScalar(typeof (Search<INTranSplit.tranDate, Where<INTranSplit.lotSerialNbr, Equal<SOLineSplit.lotSerialNbr>, And<INTranSplit.inventoryID, Equal<SOLineSplit.inventoryID>>>>))]
    [PXUnboundDefault(typeof (Search<INTranSplit.tranDate, Where<INTranSplit.lotSerialNbr, Equal<Current<SOLineSplit.lotSerialNbr>>, And<INTranSplit.inventoryID, Equal<Current<SOLineSplit.inventoryID>>>>>))]
    [PXFormula(typeof (Default<SOLineSplit.lotSerialNbr>))]
    public virtual DateTime? UsrINTranDate { get; set; }

    public abstract class usrCOO : BqlType<IBqlString, string>.Field<SOLineSplitExt.usrCOO>
    {
    }

    public abstract class usrDateCode : BqlType<IBqlString, string>.Field<SOLineSplitExt.usrDateCode>
    {
    }

    public abstract class usrINTranDate : BqlType<IBqlDateTime, DateTime>.Field<SOLineSplitExt.usrINTranDate>
    {
    }
  }
}
