// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POLineExt
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using FlexxonCustomizations.DAC;
using FlexxonCustomizations.Descriptor;
using FlexxonCustomizations.Graph;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.SO;
using System;

namespace PX.Objects.PO
{
  public class POLineExt : PXCacheExtension<POLine>
  {
    protected int? _InventoryID;

    [PXDBInt]
    [PXUIField(DisplayName = "End Customer")]
    [EndCustomerSelector(Filterable = true)]
    public virtual int? UsrEndCustomerID { get; set; }

    [PXDBString(15, IsUnicode = true)]
    [PXUIField(DisplayName = "Project Nbr")]
    [ProjectNbrSelector(typeof (Search<FLXProject.projectNbr, Where<FLXProject.endCustomerID, Equal<Current<POLineExt.usrEndCustomerID>>, And<FLXProject.nonStockItem, Equal<Current<POLineExt.usrNonStockItem>>>>>))]
    public virtual string UsrProjectNbr { get; set; }

    [PXDBString(15, IsUnicode = true)]
    [PXUIField(DisplayName = "Brand")]
    [PXDefault(typeof (Search2<CSAnswers.value, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.noteID, Equal<CSAnswers.refNoteID>, And<CSAnswers.attributeID, Equal<FLXProjectEntry.BrandAtt>>>>, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<POLine.inventoryID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
    [PXFormula(typeof (Default<POLine.inventoryID>))]
    public virtual string UsrBrand { get; set; }

    [PXDBString(30, IsUnicode = true)]
    [PXUIField(DisplayName = "Factory P/N")]
    [PXDefault(typeof (Search<FLXProject.factoryPN, Where<FLXProject.projectNbr, Equal<Current<POLineExt.usrProjectNbr>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
    [PXFormula(typeof (Default<POLine.inventoryID>))]
    public virtual string UsrFactoryPN { get; set; }

    [PXDBString(30, IsUnicode = true)]
    [PXUIField(DisplayName = "Customer To Factory")]
    [PXDefault(typeof (Search<FLXProject.cust2Factory, Where<FLXProject.projectNbr, Equal<Current<POLineExt.usrProjectNbr>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
    [PXFormula(typeof (Default<POLine.inventoryID>))]
    public virtual string UsrCust2Factory { get; set; }

    [PXDBDate]
    [PXUIField(DisplayName = "Vendor Conf. Date")]
    public virtual DateTime? UsrVendConfDate { get; set; }

    [POLineInventoryItem(DisplayName = "Non Stock/MPN")]
    public virtual int? UsrNonStockItem { get; set; }

    [PXDBString(256, IsUnicode = true)]
    [PXUIField(DisplayName = "Alternate Descr.")]
    [PXFormula(typeof (Default<POLine.alternateID>))]
    [PXDefault(typeof (Search<INItemXRef.descr, Where<INItemXRef.alternateID, Equal<Current<POLine.alternateID>>, And<INItemXRef.inventoryID, Equal<Current<POLine.inventoryID>>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
    public virtual string UsrAlternateDescr { get; set; }

    [PXString(30, IsUnicode = true)]
    [PXUIField(DisplayName = "Customer P/N", IsReadOnly = true)]
    [PXDBScalar(typeof (Search2<PX.Objects.SO.SOLine.alternateID, InnerJoin<SOLineSplit, On<SOLineSplit.orderType, Equal<PX.Objects.SO.SOLine.orderType>, And<SOLineSplit.orderNbr, Equal<PX.Objects.SO.SOLine.orderNbr>, And<SOLineSplit.lineNbr, Equal<PX.Objects.SO.SOLine.lineNbr>>>>>, Where<SOLineSplit.pOType, Equal<POLine.orderType>, And<SOLineSplit.pONbr, Equal<POLine.orderNbr>, And<SOLineSplit.pOLineNbr, Equal<POLine.lineNbr>>>>>))]
    public virtual string UsrCustomerPN { get; set; }

    [PXDBString(50, InputMask = "", IsUnicode = true)]
    [PXUIField(DisplayName = "Vendor P/N")]
    [PXSelector(typeof (Search<INItemXRef.alternateID, Where<INItemXRef.inventoryID, Equal<Current<POLine.inventoryID>>, And<INItemXRef.bAccountID, Equal<Current<POLine.vendorID>>, And<INItemXRef.subItemID, Equal<Current<POLine.subItemID>>>>>>), new System.Type[] {typeof (INItemXRef.alternateID), typeof (INItemXRef.descr), typeof (INItemXRef.bAccountID)}, DescriptionField = typeof (INItemXRef.descr))]
    [PXDefault(typeof (Search<INItemXRef.alternateID, Where<INItemXRef.inventoryID, Equal<Current<POLine.inventoryID>>, And<INItemXRef.bAccountID, Equal<Current<POLine.vendorID>>, And<INItemXRef.subItemID, Equal<Current<POLine.subItemID>>>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
    [PXFormula(typeof (Default<POLine.inventoryID>))]
    public virtual string AlternateID { get; set; }

    [PXDefault(typeof (Search<FLXProject.stockItem, Where<FLXProject.projectNbr, Equal<Current<POLineExt.usrProjectNbr>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
    [PXFormula(typeof (Default<POLineExt.usrProjectNbr>))]
    [POLineInventoryItem(Filterable = true)]
    [PXForeignReference(typeof (POLine.FK.InventoryItem))]
    public virtual int? InventoryID
    {
      get => this._InventoryID;
      set => this._InventoryID = value;
    }

    public abstract class usrEndCustomerID : BqlType<IBqlInt, int>.Field<POLineExt.usrEndCustomerID>
    {
    }

    public abstract class usrProjectNbr : BqlType<IBqlString, string>.Field<POLineExt.usrProjectNbr>
    {
    }

    public abstract class usrBrand : BqlType<IBqlString, string>.Field<POLineExt.usrBrand>
    {
    }

    public abstract class usrFactoryPN : BqlType<IBqlString, string>.Field<POLineExt.usrFactoryPN>
    {
    }

    public abstract class usrCust2Factory : BqlType<IBqlString, string>.Field<POLineExt.usrCust2Factory>
    {
    }

    public abstract class usrVendConfDate : BqlType<IBqlDateTime, DateTime>.Field<POLineExt.usrVendConfDate>
    {
    }

    public abstract class usrNonStockItem : BqlType<IBqlInt, int>.Field<POLineExt.usrNonStockItem>
    {
    }

    public abstract class usrAlternateDescr : BqlType<IBqlString, string>.Field<POLineExt.usrAlternateDescr>
    {
    }

    public abstract class usrCustomerPN : BqlType<IBqlString, string>.Field<POLineExt.usrCustomerPN>
    {
    }

    public abstract class alternateID : BqlType<IBqlString, string>.Field<POLineExt.alternateID>
    {
    }

    public abstract class inventoryID : BqlType<IBqlInt, int>.Field<POLineExt.inventoryID>
    {
      public class InventoryBaseUnitRule : PX.Objects.IN.InventoryItem.baseUnit.PreventEditIfExists<Select<POLine, Where<POLineExt.inventoryID, Equal<Current<PX.Objects.IN.InventoryItem.inventoryID>>, And2<Where2<SetOfConstantsFluent<string, TypeArrayOf<IConstant<string>>.FilledWith<POLineType.goodsForInventory, POLineType.goodsForDropShip, POLineType.goodsForSalesOrder, POLineType.goodsForServiceOrder, POLineType.goodsForReplenishment, POLineType.goodsForManufacturing>>.AsStrings.Contains<POLine.lineType>, Or<SetOfConstantsFluent<string, TypeArrayOf<IConstant<string>>.FilledWith<POLineType.nonStock, POLineType.nonStockForDropShip, POLineType.nonStockForSalesOrder, POLineType.nonStockForServiceOrder, POLineType.service, POLineType.nonStockForManufacturing>>.AsStrings.Contains<POLine.lineType>>>, And<POLine.completed, NotEqual<True>>>>>>
      {
      }
    }
  }
}
