// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOShipLineSplitExt
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using System;

namespace PX.Objects.SO
{
    public class SOShipLineSplitExt : PXCacheExtension<SOShipLineSplit>
    {
        [PXString(2, IsUnicode = true)]
        [PXUIField(DisplayName = "COO", IsReadOnly = true)]
        [PXDBScalar(typeof(Search<INTranSplitExt.usrCOO, Where<INTranSplit.lotSerialNbr, Equal<SOShipLineSplit.lotSerialNbr>, And<INTranSplit.inventoryID, Equal<SOShipLineSplit.inventoryID>, And<INTranSplit.locationID, Equal<SOShipLineSplit.locationID>, And<INTranSplit.docType, In3<INDocType.receipt, INDocType.production>>>>>>))]
        [PXDefault(typeof(Search<INTranSplitExt.usrCOO, Where<INTranSplit.lotSerialNbr, Equal<Current<SOShipLineSplit.lotSerialNbr>>, And<INTranSplit.inventoryID, Equal<Current<SOShipLineSplit.inventoryID>>, And<INTranSplit.locationID, Equal<Current<SOShipLineSplit.locationID>>, And<INTranSplit.docType, In3<INDocType.receipt, INDocType.production>>>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
        [PXFormula(typeof(Default<SOShipLineSplit.lotSerialNbr>))]
        public virtual string UsrCOO { get; set; }

        [PXString(6, IsUnicode = true)]
        [PXUIField(DisplayName = "Date Code", IsReadOnly = true)]
        [PXDBScalar(typeof(Search<INTranSplitExt.usrDateCode, Where<INTranSplit.lotSerialNbr, Equal<SOShipLineSplit.lotSerialNbr>, And<INTranSplit.inventoryID, Equal<SOShipLineSplit.inventoryID>, And<INTranSplit.locationID, Equal<SOShipLineSplit.locationID>, And<INTranSplit.docType, In3<INDocType.receipt, INDocType.production>>>>>>))]
        [PXDefault(typeof(Search<INTranSplitExt.usrDateCode, Where<INTranSplit.lotSerialNbr, Equal<Current<SOShipLineSplit.lotSerialNbr>>, And<INTranSplit.inventoryID, Equal<Current<SOShipLineSplit.inventoryID>>, And<INTranSplit.locationID, Equal<Current<SOShipLineSplit.locationID>>, And<INTranSplit.docType, In3<INDocType.receipt, INDocType.production>>>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
        [PXFormula(typeof(Default<SOShipLineSplit.lotSerialNbr>))]
        public virtual string UsrDateCode { get; set; }

        [PXDate]
        [PXUIField(DisplayName = "IN Tran Date", IsReadOnly = true)]
        [PXDBScalar(typeof(Search<INTranSplit.tranDate, Where<INTranSplit.lotSerialNbr, Equal<SOShipLineSplit.lotSerialNbr>, And<INTranSplit.inventoryID, Equal<SOShipLineSplit.inventoryID>, And<INTranSplit.locationID, Equal<SOShipLineSplit.locationID>>>>>))]
        [PXDefault(typeof(Search<INTranSplit.tranDate, Where<INTranSplit.lotSerialNbr, Equal<Current<SOShipLineSplit.lotSerialNbr>>, And<INTranSplit.inventoryID, Equal<Current<SOShipLineSplit.inventoryID>>, And<INTranSplit.locationID, Equal<Current<SOShipLineSplit.locationID>>>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
        [PXFormula(typeof(Default<SOShipLineSplit.lotSerialNbr>))]
        public virtual DateTime? UsrINTranDate { get; set; }

        public abstract class usrCOO : BqlType<IBqlString, string>.Field<SOShipLineSplitExt.usrCOO>
        {
        }

        public abstract class usrDateCode : BqlType<IBqlString, string>.Field<SOShipLineSplitExt.usrDateCode>
        {
        }

        public abstract class usrINTranDate : BqlType<IBqlDateTime, DateTime>.Field<SOShipLineSplitExt.usrINTranDate>
        {
        }
    }
}
