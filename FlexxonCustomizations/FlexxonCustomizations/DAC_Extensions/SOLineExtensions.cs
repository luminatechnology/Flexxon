// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOLineExt
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using FlexxonCustomizations.DAC;
using FlexxonCustomizations.Descriptor;
using FlexxonCustomizations.Graph;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common;
using PX.Objects.Common.Bql;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.TM;
using System;

namespace PX.Objects.SO
{
    public class SOLineExt : PXCacheExtension<SOLine>
    {
        protected int? _SiteID;

        [PXDBInt]
        [PXUIField(DisplayName = "End Customer")]
        [PXDefault]
        [EndCustomerSelector(typeof(SOLineExt.usrNonStockItem), Filterable = true)]
        [PXFormula(typeof(Default<SOLineExt.usrNonStockItem>))]
        public virtual int? UsrEndCustomerID { get; set; }

        [PXDBString(10, IsUnicode = true)]
        [PXUIField(DisplayName = "Cust. Line Nbr.")]
        [PXDefault]
        public virtual string UsrCustLineNbr { get; set; }

        [PXDBString(15, IsUnicode = true)]
        [PXUIField(DisplayName = "Brand")]
        [PXDefault(typeof(Search2<CSAnswers.value, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.noteID, Equal<CSAnswers.refNoteID>, And<CSAnswers.attributeID, Equal<FLXProjectEntry.BrandAtt>>>>, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<SOLine.inventoryID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
        [PXFormula(typeof(Default<SOLine.inventoryID>))]
        public virtual string UsrBrand { get; set; }

        [PXDBString(15, IsUnicode = true)]
        [PXUIField(DisplayName = "Project Nbr.")]
        [ProjectNbrSelector(typeof(Search<FLXProject.projectNbr, Where<FLXProject.customerID, Equal<Current<SOLine.customerID>>, And<FLXProject.endCustomerID, Equal<Current<SOLineExt.usrEndCustomerID>>, And<FLXProject.nonStockItem, Equal<Current<SOLineExt.usrNonStockItem>>, And<FLXProject.status, NotEqual<ProjectStatus.hold>>>>>>), ValidateValue = false)]
        [PXFormula(typeof(Default<SOLineExt.usrEndCustomerID>))]
        public virtual string UsrProjectNbr { get; set; }

        [PXDBDate]
        [PXUIField(DisplayName = "Est. Arrival Date")]
        public virtual DateTime? UsrEstArrivalDate { get; set; }

        [SOLineInventoryItem(DisplayName = "Non Stock/MPN", Filterable = true)]
        public virtual int? UsrNonStockItem { get; set; }

        [PXDBQuantity]
        [PXUIField(DisplayName = "Original Qty.", IsReadOnly = true)]
        public virtual Decimal? UsrOrigQty { get; set; }

        [PXDBQuantity]
        [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "PI Quantity")]
        public virtual Decimal? UsrPIQty { get; set; }

        [PXDBDecimal(2, MaxValue = 100.0, MinValue = 0.0)]
        [PXUIField(DisplayName = "PI %")]
        [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
        [PXFormula(typeof(IIf<Where<SOLine.orderQty, Equal<decimal0>>, decimal0, Mult<Div<SOLineExt.usrPIQty, SOLine.orderQty>, decimal100>>))]
        public virtual Decimal? UsrPIPct { get; set; }

        [PXDBDecimal(2)]
        [PXUIField(DisplayName = "PI Amount")]
        [PXFormula(typeof(Div<Mult<SOLine.curyLineAmt, SOLineExt.usrPIPct>, decimal100>))]
        public virtual Decimal? UsrPIAmt { get; set; }

        [PXDBString(25, IsUnicode = true)]
        [PXUIField(DisplayName = "Order Cust. Line Nbr.", Enabled = false)]
        [PXFormula(typeof(Default<SOLineExt.usrCustLineNbr>))]
        public virtual string UsrCombineNbr { get; set; }

        [PXDecimal]
        [PXUIField(DisplayName = "Remaining Qty.", IsReadOnly = true)]
        [PXUnboundDefault(typeof(SOLine.shippedQty), PersistingCheck = PXPersistingCheck.Nothing)]
        public virtual Decimal? UsrRemainQty { get; set; }

        [PXString(2, IsUnicode = true)]
        [PXUIField(DisplayName = "Project Status", Enabled = false)]
        [ProjectStatus.List]
        [PXDBScalar(typeof(Search<FLXProject.status, Where<FLXProject.projectNbr, Equal<SOLineExt.usrProjectNbr>>>))]
        [PXUnboundDefault(typeof(Search<FLXProject.status, Where<FLXProject.projectNbr, Equal<Current<SOLineExt.usrProjectNbr>>>>))]
        [PXFormula(typeof(Default<SOLineExt.usrProjectNbr>))]
        public virtual string UsrProjStatus { get; set; }

        [PXGuid]
        [PXUIField(DisplayName = "Project Owner", Enabled = false)]
        [PXOwnerSelector]
        [PXDBScalar(typeof(Search<FLXProject.ownerID, Where<FLXProject.projectNbr, Equal<SOLineExt.usrProjectNbr>>>))]
        [PXUnboundDefault(typeof(Search<FLXProject.ownerID, Where<FLXProject.projectNbr, Equal<Current<SOLineExt.usrProjectNbr>>>>))]
        [PXFormula(typeof(Default<SOLineExt.usrProjectNbr>))]
        public virtual Guid? UsrProjOwnerID { get; set; }

        [PXDBString(50, InputMask = "", IsUnicode = true)]
        [PXUIField(DisplayName = "Customer P/N")]
        [PXSelector(typeof(Search<INItemXRef.alternateID, Where<INItemXRef.inventoryID, Equal<Current<SOLine.inventoryID>>, And<INItemXRef.bAccountID, Equal<Current<SOLine.customerID>>, And<INItemXRef.subItemID, Equal<Current<SOLine.subItemID>>>>>>), new System.Type[] { typeof(INItemXRef.alternateID), typeof(INItemXRef.alternateType), typeof(INItemXRef.bAccountID) }, ValidateValue = false)]
        [PXDefault(typeof(Search<INItemXRef.alternateID, Where<INItemXRef.inventoryID, Equal<Current<SOLine.inventoryID>>, And<INItemXRef.bAccountID, Equal<Current<SOLine.customerID>>, And<INItemXRef.subItemID, Equal<Current<SOLine.subItemID>>>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
        [PXFormula(typeof(Default<SOLine.inventoryID>))]
        public virtual string AlternateID { get; set; }

        [PXDBBool]
        [PXDefault(true)]
        [PXUIField(DisplayName = "Mark for PO")]
        public virtual bool? POCreate { get; set; }

        [SiteAvail2(typeof(SOLine.inventoryID), typeof(SOLine.subItemID), typeof(SOLine.branchID))]
        [PXParent(typeof(Select<SOOrderSite, Where<SOOrderSite.orderType, Equal<Current<SOLine.orderType>>, And<SOOrderSite.orderNbr, Equal<Current<SOLine.orderNbr>>, And<SOOrderSite.siteID, Equal<Current2<SOLine.siteID>>>>>>), LeaveChildren = true, ParentCreate = true)]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIRequired(typeof(IIf<Where<SOLine.lineType, NotEqual<SOLineType.miscCharge>>, True, False>))]
        [InterBranchRestrictor(typeof(Where2<SameOrganizationBranch<INSite.branchID, Current<SOOrder.branchID>>, Or<Current<SOOrder.behavior>, Equal<SOBehavior.qT>>>))]
        public virtual int? SiteID
        {
            get => this._SiteID;
            set => this._SiteID = value;
        }

        public abstract class usrEndCustomerID : BqlType<IBqlInt, int>.Field<SOLineExt.usrEndCustomerID>
        {
        }

        public abstract class usrCustLineNbr : BqlType<IBqlString, string>.Field<SOLineExt.usrCustLineNbr>
        {
        }

        public abstract class usrBrand : BqlType<IBqlString, string>.Field<SOLineExt.usrBrand>
        {
        }

        public abstract class usrProjectNbr : BqlType<IBqlString, string>.Field<SOLineExt.usrProjectNbr>
        {
        }

        public abstract class usrEstArrivalDate : BqlType<IBqlDateTime, DateTime>.Field<SOLineExt.usrEstArrivalDate>
        {
        }

        public abstract class usrNonStockItem : BqlType<IBqlInt, int>.Field<SOLineExt.usrNonStockItem>
        {
        }

        public abstract class usrOrigQty : BqlType<IBqlDecimal, Decimal>.Field<SOLineExt.usrOrigQty>
        {
        }

        public abstract class usrPIQty : BqlType<IBqlDecimal, Decimal>.Field<SOLineExt.usrPIQty>
        {
        }

        public abstract class usrPIPct : BqlType<IBqlDecimal, Decimal>.Field<SOLineExt.usrPIPct>
        {
        }

        public abstract class usrPIAmt : BqlType<IBqlDecimal, Decimal>.Field<SOLineExt.usrPIAmt>
        {
        }

        public abstract class usrCombineNbr : BqlType<IBqlString, string>.Field<SOLineExt.usrCombineNbr>
        {
        }

        public abstract class usrRemainQty : BqlType<IBqlDecimal, Decimal>.Field<SOLineExt.usrRemainQty>
        {
        }

        public abstract class usrProjStatus : BqlType<IBqlString, string>.Field<SOLineExt.usrProjStatus>
        {
        }

        public abstract class usrProjOwnerID : BqlType<IBqlGuid, Guid>.Field<SOLineExt.usrProjOwnerID>
        {
        }

        public abstract class alternateID : BqlType<IBqlString, string>.Field<SOLineExt.alternateID>
        {
        }

        public abstract class pOCreate : BqlType<IBqlBool, bool>.Field<SOLineExt.pOCreate>
        {
        }

        public abstract class siteID : BqlType<IBqlInt, int>.Field<SOLineExt.siteID>
        {
        }
    }
}
