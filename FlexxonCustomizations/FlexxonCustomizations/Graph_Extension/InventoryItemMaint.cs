// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryItemMaint_Extension
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using PX.Data;

namespace PX.Objects.IN
{
  public class InventoryItemMaint_Extension : PXGraphExtension<InventoryItemMaint>
  {
    protected void _(Events.RowSelected<InventoryItem> e, PXRowSelected invokeBaseHandler)
    {
      if (invokeBaseHandler != null)
        invokeBaseHandler(e.Cache, e.Args);
      PXUIFieldAttribute.SetEnabled<InventoryItem.kitItem>(e.Cache, (object) e.Row, true);
    }
  }
}
