using FlexxonCustomizations.DAC;
using FlexxonCustomizations.Descriptor;
using FlexxonCustomizations.Graph;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.TM;
using System;

namespace PX.Objects.SO
{
    public class SOLineExt : PXCacheExtension<SOLine>
    {
        #region SiteID
        [PXRemoveBaseAttribute(typeof(SiteAvailAttribute))]
        [PXMergeAttributes(Method = MergeMethod.Append)]
        [SiteAvail2(typeof(SOLine.inventoryID), typeof(SOLine.subItemID), typeof(SOLine.branchID))]
        public virtual int? SiteID { get; set; }
        #endregion

        #region AlternateID
        [PXMergeAttributes(Method = MergeMethod.Replace)]
        [PXDBString(50, InputMask = "", IsUnicode = true)]
        [PXUIField(DisplayName = "Customer P/N")]
        [PXSelector(typeof(Search<INItemXRef.alternateID, Where<INItemXRef.inventoryID, Equal<Current<SOLine.inventoryID>>,
                                                                And<INItemXRef.bAccountID, Equal<Current<SOLine.customerID>>,
                                                                    And<INItemXRef.subItemID, Equal<Current<SOLine.subItemID>>>>>>),
                    new System.Type[] { typeof(INItemXRef.alternateID), typeof(INItemXRef.alternateType), typeof(INItemXRef.bAccountID) }, ValidateValue = false)]
        [PXDefault(typeof(Search<INItemXRef.alternateID, Where<INItemXRef.inventoryID, Equal<Current<SOLine.inventoryID>>,
                                                               And<INItemXRef.bAccountID, Equal<Current<SOLine.customerID>>,
                                                                   And<INItemXRef.subItemID, Equal<Current<SOLine.subItemID>>>>>>),
                   PersistingCheck = PXPersistingCheck.Nothing)]
        [PXFormula(typeof(Default<SOLine.inventoryID>))]
        public virtual string AlternateID { get; set; }
        #endregion

        #region POCreate
        [PXMergeAttributes(Method = MergeMethod.Merge)]
        [PXDBBool]
        [PXDefault(true)]
        [PXUIField(DisplayName = "Mark for PO")]
        public virtual bool? POCreate { get; set; }
        #endregion


        #region UsrEndCustomerID
        [PXDBInt]
        [PXUIField(DisplayName = "End Customer")]
        [PXDefault()]
        [EndCustomerSelector(typeof(SOLineExt.usrNonStockItem), Filterable = true)]
        [PXFormula(typeof(Default<SOLineExt.usrNonStockItem>))]
        public virtual int? UsrEndCustomerID { get; set; }
        public abstract class usrEndCustomerID : BqlType<IBqlInt, int>.Field<SOLineExt.usrEndCustomerID> { }
        #endregion

        #region UsrCustLineNbr
        [PXDBString(10, IsUnicode = true)]
        [PXUIField(DisplayName = "Cust. Line Nbr.")]
        [PXDefault()]
        public virtual string UsrCustLineNbr { get; set; }
        public abstract class usrCustLineNbr : BqlType<IBqlString, string>.Field<SOLineExt.usrCustLineNbr> { }
        #endregion

        #region UsrBrand
        [PXDBString(15, IsUnicode = true)]
        [PXUIField(DisplayName = "Brand")]
        [PXDefault(typeof(Search2<CSAnswers.value, InnerJoin<InventoryItem, On<InventoryItem.noteID, Equal<CSAnswers.refNoteID>,
                                                                               And<CSAnswers.attributeID, Equal<FLXProjectEntry.BrandAtt>>>>,
                                                   Where<InventoryItem.inventoryID, Equal<Current<SOLine.inventoryID>>>>),
                   PersistingCheck = PXPersistingCheck.Nothing)]
        [PXFormula(typeof(Default<SOLine.inventoryID>))]
        public virtual string UsrBrand { get; set; }
        public abstract class usrBrand : BqlType<IBqlString, string>.Field<SOLineExt.usrBrand> { }
        #endregion

        #region UsrProjectNbr
        [PXDBString(15, IsUnicode = true)]
        [PXUIField(DisplayName = "Project Nbr.")]
        [ProjectNbrSelector(typeof(Search<FLXProject.projectNbr, Where<FLXProject.customerID, Equal<Current<SOLine.customerID>>,
                                                                       And<FLXProject.endCustomerID, Equal<Current<SOLineExt.usrEndCustomerID>>,
                                                                           And<FLXProject.nonStockItem, Equal<Current<SOLineExt.usrNonStockItem>>,
                                                                               And<FLXProject.status, NotEqual<ProjectStatus.hold>>>>>>),
                            ValidateValue = false)]
        [PXFormula(typeof(Default<SOLineExt.usrEndCustomerID>))]
        public virtual string UsrProjectNbr { get; set; }
        public abstract class usrProjectNbr : BqlType<IBqlString, string>.Field<SOLineExt.usrProjectNbr> { }
        #endregion

        #region UsrEstArrivalDate
        [PXDBDate]
        [PXUIField(DisplayName = "Est. Arrival Date")]
        public virtual DateTime? UsrEstArrivalDate { get; set; }
        public abstract class usrEstArrivalDate : BqlType<IBqlDateTime, DateTime>.Field<SOLineExt.usrEstArrivalDate> { }
        #endregion

        #region UsrNonStockItem
        [SOLineInventoryItem(DisplayName = "Non Stock/MPN", Filterable = true)]
        public virtual int? UsrNonStockItem { get; set; }
        public abstract class usrNonStockItem : BqlType<IBqlInt, int>.Field<SOLineExt.usrNonStockItem> { }
        #endregion

        #region UsrOrigQty
        [PXDBQuantity]
        [PXUIField(DisplayName = "Original Qty.", IsReadOnly = true)]
        public virtual decimal? UsrOrigQty { get; set; }
        public abstract class usrOrigQty : BqlType<IBqlDecimal, Decimal>.Field<SOLineExt.usrOrigQty> { }
        #endregion

        #region UsrPIQty
        [PXDBQuantity]
        [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "PI Quantity")]
        public virtual decimal? UsrPIQty { get; set; }
        public abstract class usrPIQty : BqlType<IBqlDecimal, Decimal>.Field<SOLineExt.usrPIQty> { }
        #endregion

        #region UsrPIPct
        [PXDBDecimal(2, MaxValue = 100.0, MinValue = 0.0)]
        [PXUIField(DisplayName = "PI %")]
        [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
        [PXFormula(typeof(IIf<Where<SOLine.orderQty, Equal<decimal0>>, decimal0, Mult<Div<SOLineExt.usrPIQty, SOLine.orderQty>, decimal100>>))]
        public virtual decimal? UsrPIPct { get; set; }
        public abstract class usrPIPct : BqlType<IBqlDecimal, Decimal>.Field<SOLineExt.usrPIPct> { }
        #endregion

        #region UsrPIAmt
        [PXDBDecimal(2)]
        [PXUIField(DisplayName = "PI Amount")]
        [PXFormula(typeof(Div<Mult<SOLine.curyLineAmt, SOLineExt.usrPIPct>, decimal100>))]
        public virtual decimal? UsrPIAmt { get; set; }
        public abstract class usrPIAmt : BqlType<IBqlDecimal, Decimal>.Field<SOLineExt.usrPIAmt> { }
        #endregion

        #region UsrCombineNbr
        [PXDBString(25, IsUnicode = true)]
        [PXUIField(DisplayName = "Order Cust. Line Nbr.", Enabled = false)]
        [PXFormula(typeof(Default<SOLineExt.usrCustLineNbr>))]
        public virtual string UsrCombineNbr { get; set; }
        public abstract class usrCombineNbr : BqlType<IBqlString, string>.Field<SOLineExt.usrCombineNbr> { }
        #endregion

        #region UsrRemainQty
        [PXDecimal]
        [PXUIField(DisplayName = "Remaining Qty.", IsReadOnly = true)]
        [PXUnboundDefault(typeof(SOLine.shippedQty), PersistingCheck = PXPersistingCheck.Nothing)]
        public virtual decimal? UsrRemainQty { get; set; }
        public abstract class usrRemainQty : BqlType<IBqlDecimal, Decimal>.Field<SOLineExt.usrRemainQty> { }
        #endregion

        #region UsrProjStatus
        [PXString(2, IsUnicode = true)]
        [PXUIField(DisplayName = "Project Status", Enabled = false)]
        [ProjectStatus.List]
        [PXDBScalar(typeof(Search<FLXProject.status, Where<FLXProject.projectNbr, Equal<SOLineExt.usrProjectNbr>>>))]
        [PXUnboundDefault(typeof(Search<FLXProject.status, Where<FLXProject.projectNbr, Equal<Current<SOLineExt.usrProjectNbr>>>>))]
        [PXFormula(typeof(Default<SOLineExt.usrProjectNbr>))]
        public virtual string UsrProjStatus { get; set; }
        public abstract class usrProjStatus : BqlType<IBqlString, string>.Field<SOLineExt.usrProjStatus> { }
        #endregion

        #region UsrProjOwnerID
        [PXGuid]
        [PXUIField(DisplayName = "Project Owner", Enabled = false)]
        [PXOwnerSelector]
        [PXDBScalar(typeof(Search<FLXProject.ownerID, Where<FLXProject.projectNbr, Equal<SOLineExt.usrProjectNbr>>>))]
        [PXUnboundDefault(typeof(Search<FLXProject.ownerID, Where<FLXProject.projectNbr, Equal<Current<SOLineExt.usrProjectNbr>>>>))]
        [PXFormula(typeof(Default<SOLineExt.usrProjectNbr>))]
        public virtual Guid? UsrProjOwnerID { get; set; }
        public abstract class usrProjOwnerID : BqlType<IBqlGuid, Guid>.Field<SOLineExt.usrProjOwnerID> { }
        #endregion
    }
}
