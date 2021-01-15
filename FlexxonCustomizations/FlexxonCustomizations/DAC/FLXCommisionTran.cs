// Decompiled with JetBrains decompiler
// Type: FlexxonCustomizations.DAC.FLXCommissionTran
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using FlexxonCustomizations.Descriptor;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.SO;
using System;

namespace FlexxonCustomizations.DAC
{
  [PXCacheName("FLX Commission Tran")]
  [Serializable]
  public class FLXCommissionTran : IBqlTable
  {

    #region APBillRefNbrSelector
    [PXString(20, IsUnicode = true, InputMask = "")]
    [PXSelector(typeof(SelectFrom<FLXCommissionTran>.LeftJoin<APInvoice>.On<FLXCommissionTran.docType.IsEqual<APInvoice.docType>.And<FLXCommissionTran.aPBillRefNBr.IsEqual<APInvoice.refNbr>>>
                        .AggregateTo<GroupBy<FLXCommissionTran.aPBillRefNBr>>
                        .SearchFor<FLXCommissionTran.aPBillRefNBr>),
                        typeof(aPBillRefNBr),
                        typeof(APInvoice.vendorID),
                        typeof(APInvoice.vendorID_Vendor_acctName),
                        typeof(APInvoice.docDate))]
        public virtual string APBillRefNbrSelector { get; set; }
    public abstract class aPBillRefNbrSelector : PX.Data.BQL.BqlString.Field<aPBillRefNBr> { }
    #endregion

    [PXBool]
    [PXUIField(DisplayName = "Selected")]
    public virtual bool? Selected { get; set; }

    [PXDBString(15, InputMask = "CCCCCCCCCCCCCCC", IsKey = true, IsUnicode = true)]
    [PXUIField(DisplayName = "Commission Tran ID")]
    public virtual string CommissionTranID { get; set; }

    [PXDBString(2, InputMask = ">aa", IsFixed = true)]
    [PXUIField(DisplayName = "Order Type")]
    [PXSelector(typeof (Search5<SOOrderType.orderType, InnerJoin<SOOrderTypeOperation, On2<SOOrderTypeOperation.FK.OrderType, And<SOOrderTypeOperation.operation, Equal<SOOrderType.defaultOperation>>>, LeftJoin<SOSetupApproval, On<SOOrderType.orderType, Equal<SOSetupApproval.orderType>>>>, Aggregate<GroupBy<SOOrderType.orderType>>>))]
    [PXRestrictor(typeof (Where<SOOrderTypeOperation.iNDocType, NotEqual<INTranType.transfer>, Or<FeatureInstalled<PX.Objects.CS.FeaturesSet.warehouse>>>), "'{0}' cannot be found in the system.", new System.Type[] {typeof (SOOrderType.orderType)})]
    [PXRestrictor(typeof (Where<SOOrderType.requireAllocation, NotEqual<True>, Or<AllocationAllowed>>), "'{0}' cannot be found in the system.", new System.Type[] {typeof (SOOrderType.orderType)})]
    [PXRestrictor(typeof (Where<SOOrderType.active, Equal<True>>), null, new System.Type[] {})]
    public virtual string OrderType { get; set; }

    [PXDBString(15, InputMask = "", IsUnicode = true)]
    [PXUIField(DisplayName = "Order Nbr.")]
    [PX.Objects.SO.SO.RefNbr(typeof (Search2<PX.Objects.SO.SOOrder.orderNbr, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.SO.SOOrder.customerID, Equal<PX.Objects.AR.Customer.bAccountID>, And<Where<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>, Where<PX.Objects.SO.SOOrder.orderType, Equal<Optional<PX.Objects.SO.SOOrder.orderType>>, And<Where<PX.Objects.AR.Customer.bAccountID, PX.Data.IsNotNull, Or<Exists<Select<SOOrderType, Where<SOOrderType.orderType, Equal<PX.Objects.SO.SOOrder.orderType>, And<SOOrderType.aRDocType, Equal<ARDocType.noUpdate>, And<SOOrderType.behavior, Equal<SOBehavior.sO>>>>>>>>>>, OrderBy<Desc<PX.Objects.SO.SOOrder.orderNbr>>>), Filterable = true, ValidateValue = false)]
    public virtual string OrderNbr { get; set; }

    [PXDBInt]
    [PXUIField(DisplayName = "SO Line Nbr")]
    public virtual int? SOLineNbr { get; set; }

    [PXDBInt]
    [PXUIField(DisplayName = "AR Line Nbr")]
    public virtual int? ARLineNbr { get; set; }

    [CustomerActive(DescriptionField = typeof (PX.Objects.AR.Customer.acctName))]
    public virtual int? CustomerID { get; set; }

    [PXDBInt]
    [PXUIField(DisplayName = "Sales Rep")]
    [SalesRepSelector(typeof (Search<BAccountR.bAccountID>))]
    public virtual int? SalesRepID { get; set; }

    [PXDBDecimal]
    [PXUIField(DisplayName = "Percentage")]
    public virtual Decimal? Percentage { get; set; }

    [PXDBString(5, InputMask = "", IsUnicode = true)]
    [PXUIField(DisplayName = "Cury ID")]
    [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
    public virtual string CuryID { get; set; }

    [PXDBCurrency(typeof (PX.Objects.AR.ARTran.curyInfoID), typeof (PX.Objects.AR.ARTran.tranAmt))]
    [PXUIField(DisplayName = "Cury Tran Amt")]
    public virtual Decimal? CuryTranAmt { get; set; }

    [PXDBCurrency(typeof (PX.Objects.AR.ARTran.curyInfoID), typeof (PX.Objects.AR.ARTran.tranAmt))]
    [PXUIField(DisplayName = "Commission Amt")]
    [PXFormula(typeof (Mult<FLXCommissionTran.percentage, Div<FLXCommissionTran.curyTranAmt, decimal100>>))]
    public virtual Decimal? CommisionAmt { get; set; }

    [Inventory(Filterable = true)]
    public virtual int? InventoryID { get; set; }

    [PXUIField(DisplayName = "Quantity", Visibility = PXUIVisibility.Visible)]
    [PXDBQuantity(typeof (PX.Objects.AR.ARTran.uOM), typeof (PX.Objects.AR.ARTran.baseQty), HandleEmptyKey = true)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? Qty { get; set; }

    [PXUIField(DisplayName = "Unit Price", Visibility = PXUIVisibility.SelectorVisible)]
    [PXDBCurrency(typeof (Search<CommonSetup.decPlPrcCst>), typeof (PX.Objects.AR.ARTran.curyInfoID), typeof (PX.Objects.AR.ARTran.unitPrice))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? CuryUnitPrice { get; set; }

    [PXDBDate]
    [PXUIField(DisplayName = "Doc Date")]
    public virtual DateTime? DocDate { get; set; }

    [PXDBString(40, InputMask = "", IsUnicode = true)]
    [PXUIField(DisplayName = "Customer Order Nbr")]
    public virtual string CustomerOrderNbr { get; set; }

    [PXDBString(15, InputMask = "", IsUnicode = true)]
    [PXUIField(DisplayName = "Project Nbr.")]
    [ProjectNbrSelector(ValidateValue = false)]
    public virtual string ProjectNbr { get; set; }

    [PXDBInt]
    [PXUIField(DisplayName = "End Customer")]
    [EndCustomerSelector]
    public virtual int? EndCustomerID { get; set; }

    [Inventory(DisplayName = "Non Stock/MPN")]
    public virtual int? NonStockItem { get; set; }

    [PX.Objects.GL.Branch(null, null, true, true, true)]
    public virtual int? BranchID { get; set; }

    [PXDBString(15, IsUnicode = true)]
    [PXUIField(DisplayName = "AP Bill Ref. Nbr")]
    [PXSelector(typeof (Search<PX.Objects.AP.APRegister.refNbr, Where<PX.Objects.AP.APRegister.refNbr, Equal<Current<FLXCommissionTran.aPBillRefNBr>>, And<PX.Objects.AP.APRegister.docType, Equal<APDocType.invoice>>>>), DescriptionField = typeof (PX.Objects.AP.APRegister.docDesc))]
    public virtual string APBillRefNbr { get; set; }

    [PXDBBool]
    [PXUIField(DisplayName = "AP Bill Created")]
    public virtual bool? APBillCreated { get; set; }

    [PXDBString(3, IsFixed = true)]
    [ARInvoiceType.List]
    [PXUIField(DisplayName = "Doc. Type", Visibility = PXUIVisibility.SelectorVisible)]
    public virtual string DocType { get; set; }

    [PXDBString(15, InputMask = ">CCCCCCCCCCCCCCC", IsUnicode = true)]
    [PXUIField(DisplayName = "Reference Nbr.", Visibility = PXUIVisibility.SelectorVisible)]
    [PXSelector(typeof (Search<PX.Objects.AR.ARRegister.refNbr, Where<PX.Objects.AR.ARRegister.docType, Equal<Current<FLXCommissionTran.docType>>>>), Filterable = true)]
    public virtual string RefNbr { get; set; }

    [PXDBCreatedByID]
    public virtual Guid? CreatedByID { get; set; }

    [PXDBCreatedByScreenID]
    public virtual string CreatedByScreenID { get; set; }

    [PXDBCreatedDateTime]
    public virtual DateTime? CreatedDateTime { get; set; }

    [PXDBLastModifiedByID]
    public virtual Guid? LastModifiedByID { get; set; }

    [PXDBLastModifiedByScreenID]
    public virtual string LastModifiedByScreenID { get; set; }

    [PXDBLastModifiedDateTime]
    public virtual DateTime? LastModifiedDateTime { get; set; }

    [PXNote]
    public virtual Guid? NoteID { get; set; }

    [PXDBTimestamp]
    public virtual byte[] Tstamp { get; set; }

    [PXDate]
    [PXUIField(DisplayName = "AP Date", IsReadOnly = true)]
    public virtual DateTime? APDate { get; set; }

    public abstract class selected : BqlType<IBqlBool, bool>.Field<FLXCommissionTran.selected>
    {
    }

    public abstract class commissionTranID : BqlType<IBqlString, string>.Field<FLXCommissionTran.commissionTranID>
    {
    }

    public abstract class orderType : BqlType<IBqlString, string>.Field<FLXCommissionTran.orderType>
    {
    }

    public abstract class orderNbr : BqlType<IBqlString, string>.Field<FLXCommissionTran.orderNbr>
    {
    }

    public abstract class sOLineNbr : BqlType<IBqlInt, int>.Field<FLXCommissionTran.sOLineNbr>
    {
    }

    public abstract class aRLineNbr : BqlType<IBqlInt, int>.Field<FLXCommissionTran.aRLineNbr>
    {
    }

    public abstract class customerID : BqlType<IBqlInt, int>.Field<FLXCommissionTran.customerID>
    {
    }

    public abstract class salesRepID : BqlType<IBqlInt, int>.Field<FLXCommissionTran.salesRepID>
    {
    }

    public abstract class percentage : BqlType<IBqlDecimal, Decimal>.Field<FLXCommissionTran.percentage>
    {
    }

    public abstract class curyID : BqlType<IBqlString, string>.Field<FLXCommissionTran.curyID>
    {
    }

    public abstract class curyTranAmt : BqlType<IBqlDecimal, Decimal>.Field<FLXCommissionTran.curyTranAmt>
    {
    }

    public abstract class commisionAmt : BqlType<IBqlDecimal, Decimal>.Field<FLXCommissionTran.commisionAmt>
    {
    }

    public abstract class inventoryID : BqlType<IBqlInt, int>.Field<FLXCommissionTran.inventoryID>
    {
    }

    public abstract class qty : BqlType<IBqlDecimal, Decimal>.Field<FLXCommissionTran.qty>
    {
    }

    public abstract class curyUnitPrice : BqlType<IBqlDecimal, Decimal>.Field<FLXCommissionTran.curyUnitPrice>
    {
    }

    public abstract class docDate : BqlType<IBqlDateTime, DateTime>.Field<FLXCommissionTran.docDate>
    {
    }

    public abstract class customerOrderNbr : BqlType<IBqlString, string>.Field<FLXCommissionTran.customerOrderNbr>
    {
    }

    public abstract class projectNbr : BqlType<IBqlString, string>.Field<FLXCommissionTran.projectNbr>
    {
    }

    public abstract class endCustomerID : BqlType<IBqlInt, int>.Field<FLXCommissionTran.endCustomerID>
    {
    }

    public abstract class nonStockItem : BqlType<IBqlInt, int>.Field<FLXCommissionTran.nonStockItem>
    {
    }

    public abstract class branchID : BqlType<IBqlInt, int>.Field<FLXCommissionTran.branchID>
    {
    }

    public abstract class aPBillRefNBr : BqlType<IBqlString, string>.Field<FLXCommissionTran.aPBillRefNBr>
    {
    }

    public abstract class aPBillCreated : BqlType<IBqlBool, bool>.Field<FLXCommissionTran.aPBillCreated>
    {
    }

    public abstract class docType : BqlType<IBqlString, string>.Field<FLXCommissionTran.docType>
    {
    }

    public abstract class refNbr : BqlType<IBqlString, string>.Field<FLXCommissionTran.refNbr>
    {
    }

    public abstract class createdByID : BqlType<IBqlGuid, Guid>.Field<FLXCommissionTran.createdByID>
    {
    }

    public abstract class createdByScreenID : BqlType<IBqlString, string>.Field<FLXCommissionTran.createdByScreenID>
    {
    }

    public abstract class createdDateTime : BqlType<IBqlDateTime, DateTime>.Field<FLXCommissionTran.createdDateTime>
    {
    }

    public abstract class lastModifiedByID : BqlType<IBqlGuid, Guid>.Field<FLXCommissionTran.lastModifiedByID>
    {
    }

    public abstract class lastModifiedByScreenID : BqlType<IBqlString, string>.Field<FLXCommissionTran.lastModifiedByScreenID>
    {
    }

    public abstract class lastModifiedDateTime : BqlType<IBqlDateTime, DateTime>.Field<FLXCommissionTran.lastModifiedDateTime>
    {
    }

    public abstract class noteID : BqlType<IBqlGuid, Guid>.Field<FLXCommissionTran.noteID>
    {
    }

    public abstract class tstamp : BqlType<IBqlByteArray, byte[]>.Field<FLXCommissionTran.tstamp>
    {
    }

    public abstract class aPDate : BqlType<IBqlInt, int>.Field<FLXCommissionTran.aPDate>
    {
    }
  }
}
