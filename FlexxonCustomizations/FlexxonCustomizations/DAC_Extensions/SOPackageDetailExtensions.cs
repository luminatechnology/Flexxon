// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOPackageDetailExt
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

namespace PX.Objects.SO
{
    public class SOPackageDetailExt : PXCacheExtension<PX.Objects.SO.SOPackageDetail>
    {
        protected Decimal? _Qty;
        protected string _BoxID;
        protected Decimal? _Weight;

        [PXDBString(4, IsUnicode = true)]
        [PXUIField(DisplayName = "Carton")]
        public virtual string UsrCarton { get; set; }

        [PXDBString(10, IsUnicode = true)]
        [PXUIField(DisplayName = "Cust. Line Nbr.", IsReadOnly = true)]
        [PXDefault(typeof(Search<SOLineExt.usrCustLineNbr, Where<SOLineExt.usrCombineNbr, Equal<Current<SOPackageDetailExt.usrCombineNbr>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
        [PXFormula(typeof(Default<SOPackageDetailExt.usrCombineNbr>))]
        public virtual string UsrCustLineNbr { get; set; }

        [PXDBString(2, IsFixed = true)]
        [PXUIField(DisplayName = "Order Type", Enabled = false)]
        [PXDefault(typeof(Search<SOLine.orderType, Where<SOLineExt.usrCombineNbr, Equal<Current<SOPackageDetailExt.usrCombineNbr>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
        [PXFormula(typeof(Default<SOPackageDetailExt.usrCombineNbr>))]
        public virtual string UsrOrderType { get; set; }

        [PXDBString(15, InputMask = "", IsUnicode = true)]
        [PXUIField(DisplayName = "Order Nbr.", Enabled = false)]
        [PXDefault(typeof(Search<SOLine.orderNbr, Where<SOLineExt.usrCombineNbr, Equal<Current<SOPackageDetailExt.usrCombineNbr>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
        [PXFormula(typeof(Default<SOPackageDetailExt.usrCombineNbr>))]
        public virtual string UsrOrderNbr { get; set; }

        [PXDBInt(MinValue = 0)]
        [PXUIField(DisplayName = "Length")]
        public virtual int? UsrLength { get; set; }

        [PXDBInt(MinValue = 0)]
        [PXUIField(DisplayName = "Width")]
        public virtual int? UsrWidth { get; set; }

        [PXDBInt(MinValue = 0)]
        [PXUIField(DisplayName = "Height")]
        public virtual int? UsrHeight { get; set; }

        [PXDBDecimal(4)]
        [PXUIField(DisplayName = "Net Weight")]
        public virtual Decimal? UsrNetWeight { get; set; }

        [PXDBString(100, IsUnicode = true)]
        [PXUIField(DisplayName = "Lot/Serial Nbr.")]
        [PXSelector(typeof(Search<SOShipLineSplit.lotSerialNbr, Where<SOShipLineSplit.shipmentNbr, Equal<Current<SOPackageDetail.shipmentNbr>>, And<SOShipLineSplit.inventoryID, Equal<Current<SOPackageDetailExt.usrInventoryID>>>>>), new System.Type[] { typeof(SOShipLineSplit.lotSerialNbr), typeof(SOShipLineSplit.inventoryID), typeof(SOShipLineSplit.qty), typeof(SOShipLineSplitExt.usrINTranDate), typeof(SOShipLineSplitExt.usrCOO), typeof(SOShipLineSplitExt.usrDateCode), typeof(SOShipLineSplit.siteID), typeof(SOShipLineSplit.locationID) })]
        public virtual string UsrLotSerialNbr { get; set; }

        [PXDBString(2, IsUnicode = true)]
        [PXUIField(DisplayName = "COO")]
        [Country]
        [PXDefault(typeof(Search<INTranSplitExt.usrCOO, Where<INTranSplit.lotSerialNbr, Equal<Current<SOPackageDetailExt.usrLotSerialNbr>>, And<INTranSplit.inventoryID, Equal<Current<SOPackageDetailExt.usrInventoryID>>, And<INTranSplit.docType, In3<INDocType.receipt, INDocType.production>>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
        [PXFormula(typeof(Default<SOPackageDetailExt.usrLotSerialNbr>))]
        public virtual string UsrCOO { get; set; }

        [PXDBString(6, IsFixed = true, IsUnicode = true)]
        [PXUIField(DisplayName = "Date Code")]
        [PXDefault(typeof(Search<INTranSplitExt.usrDateCode, Where<INTranSplit.lotSerialNbr, Equal<Current<SOPackageDetailExt.usrLotSerialNbr>>, And<INTranSplit.inventoryID, Equal<Current<SOPackageDetailExt.usrInventoryID>>, And<INTranSplit.docType, In3<INDocType.receipt, INDocType.production>>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
        [PXFormula(typeof(Default<SOPackageDetailExt.usrLotSerialNbr>))]
        public virtual string UsrDateCode { get; set; }

        [PXDBString(25, IsUnicode = true)]
        [PXUIField(DisplayName = "Cust. Combine Nbr.")]
        [PXSelector(typeof(Search5<SOLineExt.usrCombineNbr, InnerJoin<SOShipLine, On<SOLine.orderType, Equal<SOShipLine.origOrderType>, And<SOLine.orderNbr, Equal<SOShipLine.origOrderNbr>, And<SOLine.lineNbr, Equal<SOShipLine.origLineNbr>>>>>, Where<SOShipLine.shipmentNbr, Equal<Current<SOPackageDetail.shipmentNbr>>>, Aggregate<GroupBy<SOShipLine.origOrderNbr, GroupBy<SOShipLine.inventoryID, GroupBy<SOShipLine.origLineNbr>>>>>), new System.Type[] { typeof(SOLineExt.usrCombineNbr), typeof(SOLineExt.usrCustLineNbr), typeof(SOLine.inventoryID), typeof(SOLine.tranDesc), typeof(SOLineExt.usrRemainQty), typeof(SOLine.shippedQty), typeof(SOLine.uOM) })]
        public virtual string UsrCombineNbr { get; set; }

        [PXString(40, IsUnicode = true)]
        [PXUIField(DisplayName = "Customer Order Nbr.", Enabled = false)]
        [PXDBScalar(typeof(Search2<SOOrder.customerOrderNbr, InnerJoin<SOLine, On<SOLine.orderType, Equal<SOOrder.orderType>, And<SOLine.orderNbr, Equal<SOOrder.orderNbr>>>>, Where<SOLine.orderType, Equal<SOPackageDetailExt.usrOrderType>, And<SOLine.orderNbr, Equal<SOPackageDetailExt.usrOrderNbr>, And<SOLineExt.usrCustLineNbr, Equal<SOPackageDetailExt.usrCustLineNbr>>>>>))]
        public virtual string UsrCustOrderNbr { get; set; }

        [PXInt]
        [PXUIField(DisplayName = "Inventory ID", Enabled = false)]
        [PXSelector(typeof(Search<PX.Objects.IN.InventoryItem.inventoryID>), SubstituteKey = typeof(PX.Objects.IN.InventoryItem.inventoryCD))]
        [PXDBScalar(typeof(Search<SOLine.inventoryID, Where<SOLineExt.usrCombineNbr, Equal<SOPackageDetailExt.usrCombineNbr>>>))]
        [PXUnboundDefault(typeof(Search<SOLine.inventoryID, Where<SOLineExt.usrCombineNbr, Equal<Current<SOPackageDetailExt.usrCombineNbr>>>>))]
        [PXFormula(typeof(Default<SOPackageDetailExt.usrCustLineNbr>))]
        public virtual int? UsrInventoryID { get; set; }

        [PXString(256, IsUnicode = true)]
        [PXUIField(DisplayName = "Line Description", Enabled = false)]
        [PXDBScalar(typeof(Search<SOLine.tranDesc, Where<SOLineExt.usrCombineNbr, Equal<SOPackageDetailExt.usrCombineNbr>>>))]
        [PXUnboundDefault(typeof(Search<SOLine.tranDesc, Where<SOLineExt.usrCombineNbr, Equal<Current<SOPackageDetailExt.usrCombineNbr>>>>))]
        [PXFormula(typeof(Default<SOPackageDetailExt.usrInventoryID>))]
        public virtual string UsrTranDesc { get; set; }

        [PXDecimal]
        [PXUIField(DisplayName = "Ship. Line Qty.", Enabled = false)]
        [PXDBScalar(typeof(Search<SOLine.shippedQty, Where<SOLineExt.usrCombineNbr, Equal<SOPackageDetailExt.usrCombineNbr>, And<SOLine.shippedQty, Greater<decimal0>>>>))]
        [PXUnboundDefault(typeof(Search<SOLine.shippedQty, Where<SOLineExt.usrCombineNbr, Equal<Current<SOPackageDetailExt.usrCombineNbr>>, And<SOLine.shippedQty, Greater<decimal0>>>>))]
        [PXFormula(typeof(Default<SOPackageDetailExt.usrInventoryID>))]
        public virtual Decimal? UsrShippedQty { get; set; }

        [PXDBQuantity(0)]
        [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Qty", Enabled = true)]
        public virtual Decimal? Qty
        {
            get => this._Qty;
            set => this._Qty = value;
        }

        [PXDBString(15, IsUnicode = true)]
        [PXDefault(typeof(Search<PX.Objects.CS.CSBox.boxID>))]
        [PXSelector(typeof(Search2<PX.Objects.CS.CSBox.boxID, LeftJoin<CarrierPackage, On<PX.Objects.CS.CSBox.boxID, Equal<CarrierPackage.boxID>, And<Current<SOShipment.shipVia>, PX.Data.IsNotNull>>>, Where<Current<SOShipment.shipVia>, PX.Data.IsNull, Or<Where<CarrierPackage.carrierID, Equal<Current<SOShipment.shipVia>>, And<Current<SOShipment.shipVia>, PX.Data.IsNotNull>>>>>))]
        [PXUIField(DisplayName = "Box ID")]
        public virtual string BoxID
        {
            get => this._BoxID;
            set => this._BoxID = value;
        }

        [PXDBDecimal(4)]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Gross Weight")]
        public virtual Decimal? Weight
        {
            get => this._Weight;
            set => this._Weight = value;
        }

        public abstract class usrCarton : BqlType<IBqlString, string>.Field<SOPackageDetailExt.usrCarton>
        {
        }

        public abstract class usrCustLineNbr : BqlType<IBqlString, string>.Field<SOPackageDetailExt.usrCustLineNbr>
        {
        }

        public abstract class usrOrderType : BqlType<IBqlString, string>.Field<SOPackageDetailExt.usrOrderType>
        {
        }

        public abstract class usrOrderNbr : BqlType<IBqlString, string>.Field<SOPackageDetailExt.usrOrderNbr>
        {
        }

        public abstract class usrLength : BqlType<IBqlInt, int>.Field<SOPackageDetailExt.usrLength>
        {
        }

        public abstract class usrWidth : BqlType<IBqlInt, int>.Field<SOPackageDetailExt.usrWidth>
        {
        }

        public abstract class usrHeight : BqlType<IBqlInt, int>.Field<SOPackageDetailExt.usrHeight>
        {
        }

        public abstract class usrNetWeight : BqlType<IBqlDecimal, Decimal>.Field<SOPackageDetailExt.usrNetWeight>
        {
        }

        public abstract class usrLotSerialNbr : BqlType<IBqlString, string>.Field<SOPackageDetailExt.usrLotSerialNbr>
        {
        }

        public abstract class usrCOO : BqlType<IBqlString, string>.Field<SOPackageDetailExt.usrCOO>
        {
        }

        public abstract class usrDateCode : BqlType<IBqlString, string>.Field<SOPackageDetailExt.usrDateCode>
        {
        }

        public abstract class usrCombineNbr : BqlType<IBqlString, string>.Field<SOPackageDetailExt.usrCombineNbr>
        {
        }

        public abstract class usrCustOrderNbr : BqlType<IBqlString, string>.Field<SOPackageDetailExt.usrCustOrderNbr>
        {
        }

        public abstract class usrInventoryID : BqlType<IBqlInt, int>.Field<SOPackageDetailExt.usrInventoryID>
        {
        }

        public abstract class usrTranDesc : BqlType<IBqlString, string>.Field<SOPackageDetailExt.usrTranDesc>
        {
        }

        public abstract class usrShippedQty : BqlType<IBqlDecimal, Decimal>.Field<SOPackageDetailExt.usrShippedQty>
        {
        }

        public abstract class qty : BqlType<IBqlDecimal, Decimal>.Field<SOPackageDetailExt.qty>
        {
        }

        public abstract class boxID : BqlType<IBqlString, string>.Field<SOPackageDetailExt.boxID>
        {
        }

        public abstract class weight : BqlType<IBqlDecimal, Decimal>.Field<SOPackageDetailExt.weight>
        {
        }

        #region UsrNonStock
        [PXInt()]
        [PXUIField(DisplayName = "Non Stock/MPN")]
        [PXSelector(typeof(Search<InventoryItem.inventoryID>), 
                    DescriptionField = typeof(InventoryItem.descr), 
                    SubstituteKey = typeof(InventoryItem.inventoryCD))]
        [PXFormula(typeof(Default<SOPackageDetailExt.usrCombineNbr>))]
        [PXDefault(typeof(Search<SOLineExt.usrNonStockItem, Where<SOLineExt.usrCombineNbr, Equal<Current<SOPackageDetailExt.usrCombineNbr>>>>),
                   PersistingCheck = PXPersistingCheck.Nothing)]
        [PXDBScalar(typeof(Search<SOLineExt.usrNonStockItem, Where<SOLineExt.usrCombineNbr, Equal<SOPackageDetailExt.usrCombineNbr>>>))]
        public virtual int? UsrNonStock { get; set; }
        public abstract class usrNonStock : PX.Data.BQL.BqlInt.Field<usrNonStock> { }
        #endregion

        #region UsrBrand
        [PXString(15, IsUnicode = true)]
        [PXUIField(DisplayName = "Brand")]
        [PXFormula(typeof(Default<SOPackageDetailExt.usrCombineNbr>))]
        [PXDefault(typeof(Search<SOLineExt.usrBrand, Where<SOLineExt.usrCombineNbr, Equal<Current<SOPackageDetailExt.usrCombineNbr>>>>), 
                   PersistingCheck = PXPersistingCheck.Nothing)]
        [PXDBScalar(typeof(Search<SOLineExt.usrBrand, Where<SOLineExt.usrCombineNbr, Equal<SOPackageDetailExt.usrCombineNbr>>>))]
        public virtual string UsrBrand { get; set; }
        public abstract class usrBrand : PX.Data.BQL.BqlString.Field<usrBrand> { }
        #endregion

        #region UsrCustomerPN
        [PXString(50, IsUnicode = true)]
        [PXUIField(DisplayName = "Customer P/N")]
        [PXFormula(typeof(Default<SOPackageDetailExt.usrCombineNbr>))]
        [PXDefault(typeof(Search<SOLine.alternateID, Where<SOLineExt.usrCombineNbr, Equal<Current<SOPackageDetailExt.usrCombineNbr>>>>),
                   PersistingCheck = PXPersistingCheck.Nothing)]
        [PXDBScalar(typeof(Search<SOLine.alternateID, Where<SOLineExt.usrCombineNbr, Equal<SOPackageDetailExt.usrCombineNbr>>>))]
        public virtual string UsrCustomerPN { get; set; }
        public abstract class usrCustomerPN : PX.Data.BQL.BqlString.Field<usrCustomerPN> { }
        #endregion
    }
}
