// Decompiled with JetBrains decompiler
// Type: FlexxonCustomizations.Descriptor.SOAllocationLotSerialNbr2Attribute
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.SO;

namespace FlexxonCustomizations.Descriptor
{
  public class SOAllocationLotSerialNbr2Attribute : SOLotSerialNbrAttribute
  {
    public SOAllocationLotSerialNbr2Attribute(
      System.Type InventoryType,
      System.Type SubItemType,
      System.Type SiteType,
      System.Type LocationType)
      : base(InventoryType, SubItemType, SiteType, LocationType)
    {
      System.Type itemType = BqlCommand.GetItemType(InventoryType);
      if (!typeof (ILSMaster).IsAssignableFrom(itemType))
        throw new PXArgumentException("itemType", "The specified type {0} must implement the {1} interface.", new object[2]
        {
          (object) itemType.GetLongName(),
          (object) typeof (ILSMaster).GetLongName()
        });
      this._InventoryType = InventoryType;
      this._SubItemType = SubItemType;
      this._LocationType = LocationType;
      this._Attributes.Add((PXEventSubscriberAttribute) new PXSelectorAttribute(BqlTemplate.OfCommand<Search2<INLotSerialStatus.lotSerialNbr, InnerJoin<INSiteLotSerial, On<INLotSerialStatus.inventoryID, Equal<INSiteLotSerial.inventoryID>, And<INLotSerialStatus.siteID, Equal<INSiteLotSerial.siteID>, And<INLotSerialStatus.lotSerialNbr, Equal<INSiteLotSerial.lotSerialNbr>>>>, LeftJoin<INTranSplit, On<INTranSplit.inventoryID, Equal<INLotSerialStatus.inventoryID>, And<INTranSplit.subItemID, Equal<INLotSerialStatus.subItemID>, And<INTranSplit.lotSerialNbr, Equal<INLotSerialStatus.lotSerialNbr>>>>>>, Where<INLotSerialStatus.inventoryID, Equal<Optional<BqlPlaceholder.A>>, And<INLotSerialStatus.subItemID, Equal<Optional<BqlPlaceholder.B>>, And2<Where<INLotSerialStatus.siteID, Equal<Optional<BqlPlaceholder.C>>, Or<Optional<BqlPlaceholder.C>, PX.Data.IsNull>>, And2<Where<INLotSerialStatus.locationID, Equal<Optional<BqlPlaceholder.D>>, Or<Optional<BqlPlaceholder.D>, PX.Data.IsNull>>, And<INLotSerialStatus.qtyOnHand, Greater<decimal0>>>>>>>>.Replace<BqlPlaceholder.A>(InventoryType).Replace<BqlPlaceholder.B>(SubItemType).Replace<BqlPlaceholder.C>(SiteType).Replace<BqlPlaceholder.D>(LocationType).ToType(), new System.Type[7]
      {
        typeof (INTranSplit.lotSerialNbr),
        typeof (INTranSplit.siteID),
        typeof (INTranSplit.tranDate),
        typeof (INTranSplitExt.usrCOO),
        typeof (INTranSplitExt.usrDateCode),
        typeof (INLotSerialStatus.qtyOnHand),
        typeof (INSiteLotSerial.qtyAvail)
      }));
      this._SelAttrIndex = this._Attributes.Count - 1;
    }

    public SOAllocationLotSerialNbr2Attribute(
      System.Type InventoryType,
      System.Type SubItemType,
      System.Type SiteType,
      System.Type LocationType,
      System.Type ParentLotSerialNbrType)
      : base(InventoryType, SubItemType, SiteType, LocationType)
    {
      this._Attributes[this._DefAttrIndex] = (PXEventSubscriberAttribute) new PXDefaultAttribute(ParentLotSerialNbrType)
      {
        PersistingCheck = PXPersistingCheck.NullOrBlank
      };
    }
  }
}
