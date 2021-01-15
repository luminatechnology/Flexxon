// Decompiled with JetBrains decompiler
// Type: FlexxonCustomizations.Graph.FLXShipmentTrkMaint
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using PX.Common;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Objects.SO;

namespace FlexxonCustomizations.Graph
{
  public class FLXShipmentTrkMaint : PXGraph<FLXShipmentTrkMaint>
  {
    public PXSave<SOShipment> Save;
    public PXCancel<SOShipment> Cancel;
    [PXFilterable(new System.Type[] {})]
    public FbqlSelect<SelectFromBase<SOShipment, TypeArrayOf<IFbqlJoin>.Empty>, SOShipment>.View Shipment;

    protected virtual void _(Events.RowSelected<SOShipment> e)
    {
      PXUIFieldAttribute.SetEnabled<SOShipment.shipmentType>(e.Cache, (object) e.Row, false);
      PXUIFieldAttribute.SetEnabled<SOShipment.shipmentNbr>(e.Cache, (object) e.Row, false);
      PXUIFieldAttribute.SetEnabled<SOShipment.customerID>(e.Cache, (object) e.Row, false);
      PXUIFieldAttribute.SetEnabled<SOShipment.shipmentQty>(e.Cache, (object) e.Row, false);
      PXUIFieldAttribute.SetEnabled<SOShipment.shipDate>(e.Cache, (object) e.Row, false);
      PXUIFieldAttribute.SetEnabled<SOShipment.siteID>(e.Cache, (object) e.Row, false);
    }
  }
}
