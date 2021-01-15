// Decompiled with JetBrains decompiler
// Type: FlexxonCustomizations.DAC.FLXSupplyDemand
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using FlexxonCustomizations.Descriptor;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using System;

namespace FlexxonCustomizations.DAC
{
  [PXCacheName("Supply & Demand")]
  [Serializable]
  public class FLXSupplyDemand : IBqlTable
  {
    [PXBool]
    [PXUIField(DisplayName = "Selected", Visible = false)]
    public virtual bool? Selected { get; set; }

    [PXDBInt(IsKey = true)]
    [PXUIField(DisplayName = "Line Nbr.", Visible = false)]
    public virtual int? LineNbr { get; set; }

    [Inventory]
    public virtual int? InventoryID { get; set; }

    [PXDBString(1, InputMask = ">a", IsFixed = true)]
    [PXUIField(DisplayName = "Type")]
    [PXStringList(new string[] {"S", "D"}, new string[] {"Supply", "Demand"})]
    public virtual string Type { get; set; }

    [PXDBQuantity]
    [PXUIField(DisplayName = "Open Qty")]
    public virtual Decimal? OpenQty { get; set; }

    [PXDBDate]
    [PXUIField(DisplayName = "PO Request/SO Ship")]
    public virtual DateTime? OrderDate { get; set; }

    [PXDBString(2, InputMask = "", IsFixed = true)]
    [PXUIField(DisplayName = "SO Order Type")]
    public virtual string SOOrderType { get; set; }

    [PXDBString(15, InputMask = "", IsUnicode = true)]
    [PXUIField(DisplayName = "SO Order Nbr.")]
    public virtual string SOOrderNbr { get; set; }

    [PXDBString(1, InputMask = "", IsFixed = true)]
    [PXUIField(DisplayName = "SO Order Status")]
    [PX.Objects.SO.SOOrderStatus.List]
    public virtual string SOOrderStatus { get; set; }

    [PXDBString(2, InputMask = "", IsFixed = true)]
    [PXUIField(DisplayName = "PO Order Type")]
    public virtual string POOrderType { get; set; }

    [PXDBString(15, InputMask = "", IsUnicode = true)]
    [PXUIField(DisplayName = "PO Order Nbr.")]
    public virtual string POOrderNbr { get; set; }

    [PXDBString(1, InputMask = "", IsFixed = true)]
    [PXUIField(DisplayName = "PO Order Status")]
    [PX.Objects.PO.POOrderStatus.List]
    public virtual string POOrderStatus { get; set; }

    [PXDBInt]
    [PXUIField(DisplayName = "End Customer ID")]
    [EndCustomerSelector]
    public virtual int? EndCustomerID { get; set; }

    [Inventory(DisplayName = "Non Stock/MPN")]
    public virtual int? NonStockMPN { get; set; }

    [PXDBString(15, InputMask = "", IsUnicode = true)]
    [PXUIField(DisplayName = "Project Nbr.")]
    public virtual string ProjectNbr { get; set; }

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

    [PXDBTimestamp]
    public virtual byte[] Tstamp { get; set; }

    public abstract class selected : BqlType<IBqlBool, bool>.Field<FLXSupplyDemand.selected>
    {
    }

    public abstract class lineNbr : BqlType<IBqlInt, int>.Field<FLXSupplyDemand.lineNbr>
    {
    }

    public abstract class inventoryID : BqlType<IBqlInt, int>.Field<FLXSupplyDemand.inventoryID>
    {
    }

    public abstract class type : BqlType<IBqlString, string>.Field<FLXSupplyDemand.type>
    {
    }

    public abstract class openQty : BqlType<IBqlDecimal, Decimal>.Field<FLXSupplyDemand.openQty>
    {
    }

    public abstract class orderDate : BqlType<IBqlDateTime, DateTime>.Field<FLXSupplyDemand.orderDate>
    {
    }

    public abstract class sOOrderType : BqlType<IBqlString, string>.Field<FLXSupplyDemand.sOOrderType>
    {
    }

    public abstract class sOOrderNbr : BqlType<IBqlString, string>.Field<FLXSupplyDemand.sOOrderNbr>
    {
    }

    public abstract class sOOrderStatus : BqlType<IBqlString, string>.Field<FLXSupplyDemand.sOOrderStatus>
    {
    }

    public abstract class pOOrderType : BqlType<IBqlString, string>.Field<FLXSupplyDemand.pOOrderType>
    {
    }

    public abstract class pOOrderNbr : BqlType<IBqlString, string>.Field<FLXSupplyDemand.pOOrderNbr>
    {
    }

    public abstract class pOOrderStatus : BqlType<IBqlString, string>.Field<FLXSupplyDemand.pOOrderStatus>
    {
    }

    public abstract class endCustomerID : BqlType<IBqlInt, int>.Field<FLXSupplyDemand.endCustomerID>
    {
    }

    public abstract class nonStockMPN : BqlType<IBqlInt, int>.Field<FLXSupplyDemand.nonStockMPN>
    {
    }

    public abstract class projectNbr : BqlType<IBqlString, string>.Field<FLXSupplyDemand.projectNbr>
    {
    }

    public abstract class createdByID : BqlType<IBqlGuid, Guid>.Field<FLXSupplyDemand.createdByID>
    {
    }

    public abstract class createdByScreenID : BqlType<IBqlString, string>.Field<FLXSupplyDemand.createdByScreenID>
    {
    }

    public abstract class createdDateTime : BqlType<IBqlDateTime, DateTime>.Field<FLXSupplyDemand.createdDateTime>
    {
    }

    public abstract class lastModifiedByID : BqlType<IBqlGuid, Guid>.Field<FLXSupplyDemand.lastModifiedByID>
    {
    }

    public abstract class lastModifiedByScreenID : BqlType<IBqlString, string>.Field<FLXSupplyDemand.lastModifiedByScreenID>
    {
    }

    public abstract class lastModifiedDateTime : BqlType<IBqlDateTime, DateTime>.Field<FLXSupplyDemand.lastModifiedDateTime>
    {
    }

    public abstract class tstamp : BqlType<IBqlByteArray, byte[]>.Field<FLXSupplyDemand.tstamp>
    {
    }
  }
}
