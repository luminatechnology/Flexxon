// Decompiled with JetBrains decompiler
// Type: FlexxonCustomizations.Graph.FLXSupplyDemandProc
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using FlexxonCustomizations.DAC;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.PO;
using PX.Objects.SO;
using System;

namespace FlexxonCustomizations.Graph
{
    public class FLXSupplyDemandProc : PXGraph<FLXSupplyDemandProc>
    {
        public PXSave<FLXSupplyDemand> Save;
        public PXCancel<FLXSupplyDemand> Cancel;
        [PXFilterable(new System.Type[] { })]
        public PXProcessing<FLXSupplyDemand> SupplyDemandProc;

        public FLXSupplyDemandProc()
        {
            this.SupplyDemandProc.SetProcessVisible(false);
            this.SupplyDemandProc.SetProcessAllCaption("Generate");
            this.SupplyDemandProc.SetProcessDelegate<FLXSupplyDemandProc>(new PXProcessingBase<FLXSupplyDemand>.ProcessItemDelegate<FLXSupplyDemandProc>(FLXSupplyDemandProc.GenerateData));
        }

        protected static void GenerateData(FLXSupplyDemandProc graph, FLXSupplyDemand record)
        {
            try
            {
                graph.SupplyDemandProc.Delete(record);
                graph.Save.Press();
                if (graph.SupplyDemandProc.Select().Count == 0)
                {
                    int num = 1;
                    foreach (PXResult<SOLine, PX.Objects.SO.SOOrder> pxResult in SelectFrom<SOLine>.InnerJoin<PX.Objects.SO.SOOrder>.On<SOLine.orderType.IsEqual<PX.Objects.SO.SOOrder.orderType>
                                                                                                                                         .And<SOLine.orderNbr.IsEqual<PX.Objects.SO.SOOrder.orderNbr>>>
                                                                                                    .Where<SOLine.openQty.IsGreater<decimal0>
                                                                                                           .And<Where<PX.Objects.SO.SOOrder.orderType.IsEqual<P.AsString>
                                                                                                                      .Or<PX.Objects.SO.SOOrder.orderType.IsEqual<P.AsString>
                                                                                                                          .Or<PX.Objects.SO.SOOrder.orderType.IsEqual<P.AsString>>>>>>.View.ReadOnly.Select((PXGraph)graph, (object)"SO", (object)"SF", (object)"SS"))
                    {
                        PX.Objects.SO.SOOrder soOrder = (PX.Objects.SO.SOOrder)pxResult;
                        SOLine soLine = (SOLine)pxResult;
                        SOLineExt extension = soLine.GetExtension<SOLineExt>();
                        FLXSupplyDemand flxSupplyDemand = new FLXSupplyDemand()
                        {
                            LineNbr = new int?(num++),
                            InventoryID = soLine.InventoryID,
                            Type = "D",
                            OpenQty = soLine.OpenQty,
                            OrderDate = soLine.ShipDate,
                            SOOrderType = soLine.OrderType,
                            SOOrderNbr = soLine.OrderNbr,
                            SOOrderStatus = soOrder.Status,
                            EndCustomerID = extension.UsrEndCustomerID,
                            NonStockMPN = extension.UsrNonStockItem,
                            ProjectNbr = extension.UsrProjectNbr
                        };
                        graph.SupplyDemandProc.Insert(flxSupplyDemand);
                    }
                    foreach (PXResult<POLine, POOrder> pxResult in SelectFrom<POLine>.InnerJoin<POOrder>.On<POLine.orderType.IsEqual<POOrder.orderType>
                                                                                                            .And<POLine.orderNbr.IsEqual<POOrder.orderNbr>>>
                                                                                     .Where<POLine.openQty.IsGreater<decimal0>>.View.ReadOnly.Select((PXGraph)graph))
                    {
                        POOrder poOrder = (POOrder)pxResult;
                        POLine poLine = (POLine)pxResult;
                        POLineExt extension = poLine.GetExtension<POLineExt>();
                        FLXSupplyDemand flxSupplyDemand = new FLXSupplyDemand()
                        {
                            LineNbr = new int?(num++),
                            InventoryID = poLine.InventoryID,
                            Type = "S",
                            OpenQty = poLine.OpenQty,
                            OrderDate = poLine.RequestedDate,
                            POOrderType = poLine.OrderType,
                            POOrderNbr = poLine.OrderNbr,
                            POOrderStatus = poOrder.Status,
                            EndCustomerID = extension.UsrEndCustomerID,
                            NonStockMPN = extension.UsrNonStockItem,
                            ProjectNbr = extension.UsrProjectNbr
                        };
                        graph.SupplyDemandProc.Insert(flxSupplyDemand);
                    }
                }
                graph.Save.Press();
            }
            catch (Exception ex)
            {
                PXProcessing.SetError<FLXSupplyDemand>(ex);
                throw;
            }
        }
    }
}
