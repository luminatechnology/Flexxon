// Decompiled with JetBrains decompiler
// Type: FlexxonCustomizations.Descriptor.GetAttributeRecords
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using PX.Data;
using PX.Objects.CR;

namespace FlexxonCustomizations.Descriptor
{
  public class GetAttributeRecords
  {
    public GetAttributeRecords(PXGraph graph, ref int? bAccountID, ref int? inventoryID)
    {
      CROpportunity current1 = graph.Caches<CROpportunity>().Current as CROpportunity;
      PXFieldState valueExt = graph.Caches<CROpportunity>().GetValueExt((object) current1, "AttributeENDCUSTOME") as PXFieldState;
      bAccountID = PXSelectBase<BAccountR, PXSelect<BAccountR, Where<BAccountR.acctCD, Equal<Required<BAccountR.acctCD>>>>.Config>.Select(graph, valueExt.Value).TopFirst.BAccountID;
      CROpportunityProducts current2 = graph.Caches<CROpportunityProducts>().Current as CROpportunityProducts;
      inventoryID = current2.InventoryID;
    }
  }
}
