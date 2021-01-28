using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using System;

namespace PX.Objects.SO
{
    public class SOShipLineSplitExt : PXCacheExtension<SOShipLineSplit>
    {
        [PXDBString(2, IsUnicode = true)]
        [PXUIField(DisplayName = "COO")]
        [PXDefault(typeof(Search<INTranSplitExt.usrCOO, Where<INTranSplit.lotSerialNbr, Equal<Current<SOShipLineSplit.lotSerialNbr>>, 
                                                              And<INTranSplit.inventoryID, Equal<Current<SOShipLineSplit.inventoryID>>, 
                                                                  And<INTranSplit.locationID, Equal<Current<SOShipLineSplit.locationID>>, 
                                                                      And<INTranSplit.docType, In3<INDocType.receipt, INDocType.production, INDocType.issue>>>>>>),
                   PersistingCheck = PXPersistingCheck.Nothing)]
        [PXFormula(typeof(Default<SOShipLineSplit.lotSerialNbr>))]
        public virtual string UsrCOO { get; set; }
        public abstract class usrCOO : BqlType<IBqlString, string>.Field<SOShipLineSplitExt.usrCOO> { }

        [PXDBString(6, IsUnicode = true)]
        [PXUIField(DisplayName = "Date Code")]
        [PXDefault(typeof(Search<INTranSplitExt.usrDateCode, Where<INTranSplit.lotSerialNbr, Equal<Current<SOShipLineSplit.lotSerialNbr>>, 
                                                                   And<INTranSplit.inventoryID, Equal<Current<SOShipLineSplit.inventoryID>>, 
                                                                       And<INTranSplit.locationID, Equal<Current<SOShipLineSplit.locationID>>,
                                                                           And<INTranSplit.docType, In3<INDocType.receipt, INDocType.production, INDocType.issue>>>>>>), 
                   PersistingCheck = PXPersistingCheck.Nothing)]
        [PXFormula(typeof(Default<SOShipLineSplit.lotSerialNbr>))]
        public virtual string UsrDateCode { get; set; }
        public abstract class usrDateCode : BqlType<IBqlString, string>.Field<SOShipLineSplitExt.usrDateCode> { }

            #region Unbound Field
            [PXDate]
            [PXUIField(DisplayName = "IN Tran Date", IsReadOnly = true)]
            [PXDBScalar(typeof(Search<INTranSplit.tranDate, Where<INTranSplit.lotSerialNbr, Equal<SOShipLineSplit.lotSerialNbr>, 
                                                                  And<INTranSplit.inventoryID, Equal<SOShipLineSplit.inventoryID>, 
                                                                      And<INTranSplit.locationID, Equal<SOShipLineSplit.locationID>>>>>))]
            [PXDefault(typeof(Search<INTranSplit.tranDate, Where<INTranSplit.lotSerialNbr, Equal<Current<SOShipLineSplit.lotSerialNbr>>, 
                                                                 And<INTranSplit.inventoryID, Equal<Current<SOShipLineSplit.inventoryID>>, 
                                                                     And<INTranSplit.locationID, Equal<Current<SOShipLineSplit.locationID>>>>>>), 
                       PersistingCheck = PXPersistingCheck.Nothing)]
            [PXFormula(typeof(Default<SOShipLineSplit.lotSerialNbr>))]
            public virtual DateTime? UsrINTranDate { get; set; }
            public abstract class usrINTranDate : BqlType<IBqlDateTime, DateTime>.Field<SOShipLineSplitExt.usrINTranDate> { }
            #endregion
    }
}
