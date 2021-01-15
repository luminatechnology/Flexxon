// Decompiled with JetBrains decompiler
// Type: FlexxonCustomizations.Descriptor.NonStockExpItemAttribute
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

namespace FlexxonCustomizations.Descriptor
{
  [PXDBInt]
  [PXUIField(DisplayName = "Non Stock Exp. Item", Visibility = PXUIVisibility.Visible)]
  public class NonStockExpItemAttribute : InventoryAttribute
  {
    public static System.Type Search => typeof (PX.Data.Search<InventoryItem.inventoryID, Where<InventoryItem.itemType, Equal<INItemTypes.expenseItem>, And<Match<Current<AccessInfo.userName>>>>>);

    public static PXRestrictorAttribute CreateRestrictor() => new PXRestrictorAttribute(typeof (Where<InventoryItem.stkItem, Equal<boolFalse>, And<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.unknown>, And<InventoryItem.isTemplate, Equal<False>>>>), "The inventory item is a stock item.", Array.Empty<System.Type>());

    public static PXRestrictorAttribute CreateRestrictorDependingOnFeature<TFeature>() where TFeature : IBqlField => new PXRestrictorAttribute(typeof (Where2<FeatureInstalled<TFeature>, Or<InventoryItem.stkItem, Equal<boolFalse>>>), "The inventory item is a stock item.", Array.Empty<System.Type>());

    public NonStockExpItemAttribute()
      : base(NonStockExpItemAttribute.Search, typeof (InventoryItem.inventoryCD), typeof (InventoryItem.descr))
      => this._Attributes.Add((PXEventSubscriberAttribute) NonStockExpItemAttribute.CreateRestrictor());
  }
}
