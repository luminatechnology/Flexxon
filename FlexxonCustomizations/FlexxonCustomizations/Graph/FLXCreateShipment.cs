// Decompiled with JetBrains decompiler
// Type: FlexxonCustomizations.Graph.FLXCreateShipment
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.PO;
using PX.Objects.SO;
using System;
using System.Collections.Generic;

namespace FlexxonCustomizations.Graph
{
    public class FLXCreateShipment : PXGraph<FLXCreateShipment>
    {
        public PXCancel<SOOrderFilter> Cancel;
        public PXFilter<SOOrderFilter> Filter;
        [PXFilterable(new System.Type[] { })]
        public PXFilteredProcessingJoin<SOShipmentPlan, SOOrderFilter, InnerJoin<SOLineSplit, On<SOLineSplit.planID, Equal<SOShipmentPlan.planID>>, InnerJoin<PX.Objects.SO.SOLine, On<PX.Objects.SO.SOLine.orderType, Equal<SOLineSplit.orderType>, And<PX.Objects.SO.SOLine.orderNbr, Equal<SOLineSplit.orderNbr>, And<PX.Objects.SO.SOLine.lineNbr, Equal<SOLineSplit.lineNbr>>>>, InnerJoin<PX.Objects.SO.SOOrder, On<PX.Objects.SO.SOOrder.orderType, Equal<PX.Objects.SO.SOLine.orderType>, And<PX.Objects.SO.SOOrder.orderNbr, Equal<PX.Objects.SO.SOLine.orderNbr>>>>>>, Where<PX.Objects.SO.SOLine.openQty, Greater<decimal0>, And<PX.Objects.SO.SOLine.branchID, Equal<Current<SOOrderFilterExt.usrBranchID>>, And2<Where<PX.Objects.SO.SOLine.shipDate, GreaterEqual<Current<SOOrderFilter.startDate>>, Or<Current<SOOrderFilter.startDate>, PX.Data.IsNull>>, And<PX.Objects.SO.SOLine.shipDate, LessEqual<Current<SOOrderFilter.endDate>>, And<Not<Exists<Select<PX.Objects.SO.SOShipLine, Where<PX.Objects.SO.SOShipLine.origOrderType, Equal<SOLineSplit.orderType>, And<PX.Objects.SO.SOShipLine.origOrderNbr, Equal<SOLineSplit.orderNbr>, And<PX.Objects.SO.SOShipLine.origLineNbr, Equal<SOLineSplit.lineNbr>, And<PX.Objects.SO.SOShipLine.origSplitLineNbr, Equal<SOLineSplit.splitLineNbr>>>>>>>>>>>>>, OrderBy<Asc<PX.Objects.SO.SOLine.orderNbr>>> Lines;

        public FLXCreateShipment()
        {
            this.Lines.SetProcessAllVisible(false);
            this.Lines.SetProcessCaption("Create");
            this.Lines.SetProcessDelegate((PXProcessingBase<SOShipmentPlan>.ProcessListDelegate)(list => FLXCreateShipment.CreateShipment(list)));
        }

        [PXMergeAttributes(Method = MergeMethod.Append)]
        [PX.Objects.SO.SO.RefNbr(typeof(Search2<PX.Objects.SO.SOOrder.orderNbr, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.SO.SOOrder.customerID, Equal<PX.Objects.AR.Customer.bAccountID>, And<Where<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>, Where<PX.Objects.SO.SOOrder.orderType, Equal<Current<SOShipmentPlan.orderType>>, And<Where<PX.Objects.AR.Customer.bAccountID, PX.Data.IsNotNull, Or<Exists<Select<PX.Objects.SO.SOOrderType, Where<PX.Objects.SO.SOOrderType.orderType, Equal<PX.Objects.SO.SOOrder.orderType>, And<PX.Objects.SO.SOOrderType.aRDocType, Equal<ARDocType.noUpdate>, And<PX.Objects.SO.SOOrderType.behavior, Equal<SOBehavior.sO>>>>>>>>>>, OrderBy<Desc<PX.Objects.SO.SOOrder.orderNbr>>>), Filterable = true)]
        protected void _(Events.CacheAttached<SOShipmentPlan.orderNbr> e)
        {
        }

        [PXMergeAttributes(Method = MergeMethod.Merge)]
        [PXUIField(DisplayName = "Open Qty.")]
        protected void _(Events.CacheAttached<SOShipmentPlan.planQty> e)
        {
        }

        protected void _(
          Events.FieldSelecting<SOShipmentPlanExt.usrOnHand> e)
        {
            if (!(e.Row is SOShipmentPlan row))
                return;
            e.ReturnValue = (object)POCreate_Extension.GetAllWHQty((PXGraph)this, typeof(Aggregate<Sum<PX.Objects.IN.INSiteStatus.qtyOnHand>>), INQtyType.OnHand, row.InventoryID, row.SubItemID);
        }

        protected void _(
          Events.FieldSelecting<SOShipmentPlanExt.usrAvailability> e)
        {
            if (!(e.Row is SOShipmentPlan row))
                return;
            e.ReturnValue = (object)POCreate_Extension.GetAllWHQty((PXGraph)this, typeof(Aggregate<Sum<PX.Objects.IN.INSiteStatus.qtyAvail>>), INQtyType.Available, row.InventoryID, row.SubItemID);
        }

        protected void _(
          Events.FieldSelecting<SOShipmentPlanExt.usrQtyAvailShipping> e)
        {
            if (!(e.Row is SOShipmentPlan row))
                return;
            e.ReturnValue = (object)POCreate_Extension.GetAllWHQty((PXGraph)this, typeof(Aggregate<Sum<PX.Objects.IN.INSiteStatus.qtyAvail>>), INQtyType.AvailShipping, row.InventoryID, row.SubItemID);
        }

        protected void _(Events.FieldDefaulting<SOOrderFilter.endDate> e) => e.NewValue = (object)this.Accessinfo.BusinessDate.Value.AddDays(4.0);

        public static void CreateShipment(System.Collections.Generic.List<SOShipmentPlan> list)
        {
            int num1 = 0;
            try
            {
                SOShipmentEntry instance = PXGraph.CreateInstance<SOShipmentEntry>();
                DocumentList<PX.Objects.SO.SOShipment> list1 = new DocumentList<PX.Objects.SO.SOShipment>((PXGraph)instance);
                Dictionary<long, PXResult<SOShipmentPlan, PX.Objects.SO.SOOrder>> dictionary = new Dictionary<long, PXResult<SOShipmentPlan, PX.Objects.SO.SOOrder>>();
                SOShipmentPlan soShipmentPlan1 = new SOShipmentPlan();
                PX.Objects.SO.SOOrder soOrder = new PX.Objects.SO.SOOrder();
                for (int index = 0; index < list.Count; ++index)
                {
                    SOShipmentPlan i0 = list[index];
                    PX.Objects.SO.SOOrder i1 = SelectFrom<PX.Objects.SO.SOOrder>.Where<PX.Objects.SO.SOOrder.orderType.IsEqual<P.AsString>
                                                                                       .And<PX.Objects.SO.SOOrder.orderNbr.IsEqual<P.AsString>>>.View.Select((PXGraph)instance, (object)i0.OrderType, (object)i0.OrderNbr);
                    int? nullable1;
                    int? customerLocationId;
                    int? nullable2;
                    int num2;
                    if (num1 != 0)
                    {
                        int num3 = num1;
                        nullable1 = i1.CustomerID;
                        customerLocationId = i1.CustomerLocationID;
                        nullable2 = nullable1.HasValue & customerLocationId.HasValue ? new int?(nullable1.GetValueOrDefault() + customerLocationId.GetValueOrDefault()) : new int?();
                        int valueOrDefault = nullable2.GetValueOrDefault();
                        num2 = num3 == valueOrDefault & nullable2.HasValue ? 1 : 0;
                    }
                    else
                        num2 = 1;
                    if (num2 == 0)
                        throw new PXException("Please Only Select The Same Customer ID & Location For One Shipment");
                    nullable2 = i1.CustomerID;
                    customerLocationId = i1.CustomerLocationID;
                    int? nullable3;
                    if (!(nullable2.HasValue & customerLocationId.HasValue))
                    {
                        nullable1 = new int?();
                        nullable3 = nullable1;
                    }
                    else
                        nullable3 = new int?(nullable2.GetValueOrDefault() + customerLocationId.GetValueOrDefault());
                    nullable1 = nullable3;
                    num1 = nullable1.Value;
                    dictionary.Add(i0.PlanID.Value, new PXResult<SOShipmentPlan, PX.Objects.SO.SOOrder>(i0, i1));
                }
                PX.Objects.SO.SOOrder order = new PX.Objects.SO.SOOrder();
                foreach (PXResult<SOShipmentPlan, PX.Objects.SO.SOOrder> pxResult in dictionary.Values)
                {
                    SOShipmentPlan soShipmentPlan2 = (SOShipmentPlan)pxResult;
                    if (order.OrderNbr != pxResult.GetItem<PX.Objects.SO.SOOrder>().OrderNbr)
                    {
                        order = (PX.Objects.SO.SOOrder)pxResult;
                        instance.GetExtension<SOShipmentEntry_Extension>().CreateShipment2(order, soShipmentPlan2.SiteID, soShipmentPlan2.PlanDate, new bool?(false), "I", list1, list);
                    }
                }
                throw new PXPopupRedirectException((PXGraph)instance, "Shipment <*> is created", true);
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("Shipment <*> is created"))
                    PXProcessing.SetProcessed();
                else
                    PXProcessing.SetError(ex);
                throw;
            }
        }
    }
}
