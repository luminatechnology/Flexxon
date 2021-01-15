using System;
using PX.Data;
using PX.Objects.IN;

namespace PX.Objects.SO
{
    public class SOShipLineExt : PXCacheExtension<PX.Objects.SO.SOShipLine>
    {
        #region UsrNonStock
        [PXInt()]
        [PXUIField(DisplayName = "Non Stock/MPN")]
        [PXSelector(typeof(Search<InventoryItem.inventoryID>),
                    DescriptionField = typeof(InventoryItem.descr),
                    SubstituteKey = typeof(InventoryItem.inventoryCD))]
        [PXFormula(typeof(Default<SOShipLine.origOrderNbr>))]
        [PXDefault(typeof(Search<SOLineExt.usrNonStockItem, Where<SOLine.orderType, Equal<Current<SOShipLine.origOrderType>>,
                                                                  And<SOLine.orderNbr, Equal<Current<SOShipLine.origOrderNbr>>,
                                                                      And<SOLine.lineNbr, Equal<Current<SOShipLine.origLineNbr>>>>>>),
                   PersistingCheck = PXPersistingCheck.Nothing)]
        [PXDBScalar(typeof(Search<SOLineExt.usrNonStockItem, Where<SOLine.orderType, Equal<SOShipLine.origOrderType>,
                                                                  And<SOLine.orderNbr, Equal<SOShipLine.origOrderNbr>,
                                                                      And<SOLine.lineNbr, Equal<SOShipLine.origLineNbr>>>>>))]
        public virtual int? UsrNonStock { get; set; }
        public abstract class usrNonStock : PX.Data.BQL.BqlInt.Field<usrNonStock> { }
        #endregion

        #region UsrBrand
        [PXString(15, IsUnicode = true)]
        [PXUIField(DisplayName = "Brand")]
        [PXFormula(typeof(Default<SOShipLine.origOrderNbr>))]
        [PXDefault(typeof(Search<SOLineExt.usrBrand, Where<SOLine.orderType, Equal<Current<SOShipLine.origOrderType>>,
                                                           And<SOLine.orderNbr, Equal<Current<SOShipLine.origOrderNbr>>,
                                                               And<SOLine.lineNbr, Equal<Current<SOShipLine.origLineNbr>>>>>>),
                   PersistingCheck = PXPersistingCheck.Nothing)]
        [PXDBScalar(typeof(Search<SOLineExt.usrBrand, Where<SOLine.orderType, Equal<SOShipLine.origOrderType>,
                                                                  And<SOLine.orderNbr, Equal<SOShipLine.origOrderNbr>,
                                                                      And<SOLine.lineNbr, Equal<SOShipLine.origLineNbr>>>>>))]
        public virtual string UsrBrand { get; set; }
        public abstract class usrBrand : PX.Data.BQL.BqlString.Field<usrBrand> { }
        #endregion

        #region UsrCustomerPN
        [PXString(50, IsUnicode = true)]
        [PXUIField(DisplayName = "Customer P/N")]
        [PXFormula(typeof(Default<SOShipLine.origOrderNbr>))]
        [PXDefault(typeof(Search<SOLine.alternateID, Where<SOLine.orderType, Equal<Current<SOShipLine.origOrderType>>,
                                                           And<SOLine.orderNbr, Equal<Current<SOShipLine.origOrderNbr>>,
                                                               And<SOLine.lineNbr, Equal<Current<SOShipLine.origLineNbr>>>>>>),
                   PersistingCheck = PXPersistingCheck.Nothing)]
        [PXDBScalar(typeof(Search<SOLine.alternateID, Where<SOLine.orderType, Equal<SOShipLine.origOrderType>,
                                                                  And<SOLine.orderNbr, Equal<SOShipLine.origOrderNbr>,
                                                                      And<SOLine.lineNbr, Equal<SOShipLine.origLineNbr>>>>>))]
        public virtual string UsrCustomerPN { get; set; }
        public abstract class usrCustomerPN : PX.Data.BQL.BqlString.Field<usrCustomerPN> { }
        #endregion
    }
}
