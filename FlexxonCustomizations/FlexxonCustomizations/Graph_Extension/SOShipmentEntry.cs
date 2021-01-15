// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOShipmentEntry_Extension
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using PX.Common;
using PX.Common.Collection;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CM;
using PX.Objects.Common.Discount;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.IN.Overrides.INDocumentRelease;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PX.Objects.SO
{
    public class SOShipmentEntry_Extension : PXGraphExtension<SOShipmentEntry>
    {
        private bool skipAdjustFreeItemLines = false;
        public PXSelectJoin<SOShipmentPlan, InnerJoin<SOLineSplit, On<SOLineSplit.planID, Equal<SOShipmentPlan.planID>>, InnerJoin<SOLine, On<SOLine.orderType, Equal<SOLineSplit.orderType>, And<SOLine.orderNbr, Equal<SOLineSplit.orderNbr>, And<SOLine.lineNbr, Equal<SOLineSplit.lineNbr>>>>, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<SOShipmentPlan.inventoryID>>, LeftJoin<INLotSerClass, On<PX.Objects.IN.InventoryItem.FK.LotSerClass>, LeftJoin<INSite, On<SOLine.FK.Site>, LeftJoin<SOShipLine, On<SOShipLine.origOrderType, Equal<SOLineSplit.orderType>, And<SOShipLine.origOrderNbr, Equal<SOLineSplit.orderNbr>, And<SOShipLine.origLineNbr, Equal<SOLineSplit.lineNbr>, And<SOShipLine.origSplitLineNbr, Equal<SOLineSplit.splitLineNbr>, And<SOShipLine.confirmed, Equal<boolFalse>, And<SOShipLine.shipmentNbr, NotEqual<Current<SOShipment.shipmentNbr>>>>>>>>>>>>>>, Where<SOShipmentPlan.siteID, Equal<Optional<SOOrderFilter.siteID>>, And<SOShipmentPlan.orderType, Equal<Required<SOOrder.orderType>>, And<SOShipmentPlan.orderNbr, Equal<Required<SOOrder.orderNbr>>, And<SOLine.operation, Equal<Required<SOLine.operation>>, And<SOShipLine.origOrderNbr, PX.Data.IsNull, And<Where<SOShipmentPlan.requireAllocation, Equal<False>, Or<SOShipmentPlan.inclQtySOShipping, Equal<True>, Or<SOShipmentPlan.inclQtySOShipped, Equal<True>, Or<SOLineSplit.lineType, Equal<SOLineType.nonInventory>>>>>>>>>>>> ShipmentScheduleSelect2;

        [PXOverride]
        public void ConfirmShipment(
          SOOrderEntry docgraph,
          SOShipment shiporder,
          SOShipmentEntry_Extension.DelConfirmShipment baseMethod)
        {
            if (shiporder.Operation != "R")
            {
                Decimal? nullable1 = new Decimal?(0M);
                foreach (PXResult<SOPackageDetail> pxResult in SelectFrom<SOPackageDetail>.Where<SOPackageDetail.shipmentNbr.IsEqual<P.AsString>>
                                                                                          .Aggregate<To<Sum<SOPackageDetail.qty>>>.View.Select((PXGraph)this.Base, (object)shiporder.ShipmentNbr))
                {
                    SOPackageDetail soPackageDetail = (SOPackageDetail)pxResult;
                    Decimal? nullable2 = nullable1;
                    Decimal? qty = soPackageDetail.Qty;
                    nullable1 = nullable2.HasValue & qty.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + qty.GetValueOrDefault()) : new Decimal?();
                }
                if (!nullable1.HasValue)
                    throw new PXException("The Shipment Package Can’t Be Empty.");
                Decimal? shipmentQty = shiporder.ShipmentQty;
                Decimal? nullable3 = nullable1;
                if (!(shipmentQty.GetValueOrDefault() == nullable3.GetValueOrDefault() & shipmentQty.HasValue == nullable3.HasValue))
                    throw new PXException("The Shipped Quantity Isn't Equal To Total Package Quantity.");
            }
            baseMethod(docgraph, shiporder);
        }

        [PXOverride]
        public virtual void InvoiceShipment(
          SOInvoiceEntry docgraph,
          SOShipment shiporder,
          DateTime invoiceDate,
          InvoiceList list,
          PXQuickProcess.ActionFlow quickProcessFlow,
          SOShipmentEntry_Extension.DelInvoiceShipment baseMethod)
        {
            System.Collections.Generic.List<SOOrder> orderLists = SelectFrom<SOOrder>.InnerJoin<SOShipLine>.On<SOOrder.orderType.IsEqual<SOShipLine.origOrderType>
                                                                                                               .And<SOOrder.orderNbr.IsEqual<SOShipLine.origOrderNbr>>>
                                                                                     .Where<SOShipLine.shipmentType.IsEqual<P.AsString>
                                                                                            .And<SOShipLine.shipmentNbr.IsEqual<P.AsString>>>.View.Select((PXGraph)this.Base, (object)shiporder.ShipmentType, (object)shiporder.ShipmentNbr).RowCast<SOOrder>().ToList<SOOrder>();
            for (int i = 0; i < orderLists.Count; i++)
            {
                if (orderLists.Exists((Predicate<SOOrder>)(order =>
               {
                   int? billAddressId1 = order.BillAddressID;
                   int? billAddressId2 = orderLists[i].BillAddressID;
                   if (!(billAddressId1.GetValueOrDefault() == billAddressId2.GetValueOrDefault() & billAddressId1.HasValue == billAddressId2.HasValue))
                       return true;
                   int? billContactId1 = order.BillContactID;
                   int? billContactId2 = orderLists[i].BillContactID;
                   return !(billContactId1.GetValueOrDefault() == billContactId2.GetValueOrDefault() & billContactId1.HasValue == billContactId2.HasValue);
               })))
                    throw new PXException("Prepare Invoice For This Shipment Will Create Multiple Invoices, Please Go Sales Invoice To Create Directly.");
            }
            baseMethod(docgraph, shiporder, invoiceDate, list, quickProcessFlow);
        }

        [PXMergeAttributes(Method = MergeMethod.Merge)]
        [PXUIField(Visible = false)]
        protected void _(Events.CacheAttached<SOShipLineSplit.expireDate> e)
        {
        }

        protected void _(Events.FieldSelecting<SOShipmentExt.usrNote> e)
        {
            SOShipment row = e.Row as SOShipment;
            string str = string.Empty;
            if (row != null)
            {
                foreach (PXResult<Note> pxResult1 in SelectFrom<Note>.InnerJoin<PX.Objects.AR.Customer>.On<PX.Objects.AR.Customer.noteID.IsEqual<Note.noteID>>
                                                                     .Where<PX.Objects.AR.Customer.bAccountID.IsEqual<P.AsInt>>.View.ReadOnly.Select((PXGraph)this.Base, (object)row.CustomerID))
                {
                    str = ((Note)pxResult1).NoteText + "\n";
                    foreach (PXResult<Note> pxResult2 in SelectFrom<Note>.InnerJoin<SOOrder>.On<SOOrder.noteID.IsEqual<Note.noteID>>
                                                                         .InnerJoin<SOOrderShipment>.On<SOOrderShipment.orderType.IsEqual<SOOrder.orderType>
                                                                                                        .And<SOOrderShipment.orderNbr.IsEqual<SOOrder.orderNbr>>>
                                                                         .Where<SOOrderShipment.shipmentNbr.IsEqual<P.AsString>>.View.ReadOnly.Select((PXGraph)this.Base, (object)row.ShipmentNbr))
                    {
                        Note note = (Note)pxResult2;
                        str += note.NoteText;
                    }
                }
            }
            e.ReturnValue = (object)str;
        }

        protected void _(Events.FieldDefaulting<SOLineExt.usrRemainQty> e)
        {
            SOLine row = e.Row as SOLine;
            Decimal? shippedQty = row.ShippedQty;
            Decimal? nullable1 = new Decimal?(0M);
            foreach (SOPackageDetailEx soPackageDetailEx in this.Base.Packages.Cache.Cached)
            {
                if (row.GetExtension<SOLineExt>().UsrCombineNbr == soPackageDetailEx.GetExtension<SOPackageDetailExt>().UsrCombineNbr)
                {
                    Decimal? nullable2 = nullable1;
                    Decimal? qty = soPackageDetailEx.Qty;
                    nullable1 = nullable2.HasValue & qty.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + qty.GetValueOrDefault()) : new Decimal?();
                }
            }
            Events.FieldDefaulting<SOLineExt.usrRemainQty> fieldDefaulting = e;
            Decimal? nullable3 = shippedQty;
            Decimal? nullable4 = nullable1;
            // ISSUE: variable of a boxed type
            Decimal? local = (nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?());
            fieldDefaulting.NewValue = (object)local;
        }

        private DiscountEngine<SOShipLine, SOShipmentDiscountDetail> _discountEngine => DiscountEngineProvider.GetEngineFor<SOShipLine, SOShipmentDiscountDetail>();

        public void CreateShipment2(
          SOOrder order,
          int? SiteID,
          DateTime? ShipDate,
          bool? useOptimalShipDate,
          string operation,
          DocumentList<SOShipment> list,
          System.Collections.Generic.List<SOShipmentPlan> plans,
          PXQuickProcess.ActionFlow quickProcessFlow = PXQuickProcess.ActionFlow.NoFlow)
        {
            SiteLotSerialAccumulatorAttribute.ForceAvailQtyValidation((PXGraph)this.Base, true);
            ItemLotSerialAccumulatorAttribute.ForceAvailQtyValidation((PXGraph)this.Base, true);
            SOOrderType ordertype = (SOOrderType)this.Base.soordertype.Select((object)order.OrderType);
            if (operation == null)
                operation = ordertype.DefaultOperation;
            SOOrderTypeOperation orderOperation = SOOrderTypeOperation.PK.Find((PXGraph)this.Base, ordertype.OrderType, operation);
            if ((uint)quickProcessFlow > 0U)
                this.Base.sosetup.Current.HoldShipments = new bool?(false);
            int num1;
            if (orderOperation != null)
            {
                bool? active = orderOperation.Active;
                bool flag = true;
                if (active.GetValueOrDefault() == flag & active.HasValue)
                {
                    num1 = string.IsNullOrEmpty(orderOperation.ShipmentPlanType) ? 1 : 0;
                    goto label_8;
                }
            }
            num1 = 0;
        label_8:
            if (num1 != 0)
            {
                object stateExt = this.Base.Caches<SOOrderTypeOperation>().GetStateExt<SOOrderTypeOperation.operation>((object)orderOperation);
                throw new PXException("For order type '{0}' and operation '{1}', no shipment plan type is specified on the Order Types (SO.20.20.00) form.", new object[2]
                {
          (object) ordertype.OrderType,
          stateExt
                });
            }
            SOShipment soShipment;
            if (list != null)
            {
                this.Base.Clear();
                bool? shipSeparately = order.ShipSeparately;
                bool flag = false;
                SOShipment shipment;
                if (shipSeparately.GetValueOrDefault() == flag & shipSeparately.HasValue)
                {
                    shipment = list.Find<SOShipment.customerID, SOShipment.shipAddressID, SOShipment.shipContactID, SOShipment.siteID, SOShipment.fOBPoint, SOShipment.shipVia, SOShipment.shipTermsID, SOShipment.shipZoneID, SOShipment.useCustomerAccount, SOShipment.shipmentType, SOShipment.freightAmountSource, SOShipment.hidden, SOShipment.isManualPackage>((object)order.CustomerID, (object)order.ShipAddressID, (object)order.ShipContactID, (object)SiteID, (object)order.FOBPoint, (object)order.ShipVia, (object)order.ShipTermsID, (object)order.ShipZoneID, (object)order.UseCustomerAccount, (object)INTranType.DocType(orderOperation.INDocType), (object)order.FreightAmountSource, (object)false, (object)order.IsManualPackage) ?? new SOShipment();
                }
                else
                {
                    shipment = new SOShipment();
                    shipment.Hidden = new bool?(true);
                }
                bool newlyCreated = shipment.ShipmentNbr == null;
                if (newlyCreated)
                    shipment = this.Base.Document.Insert(shipment);
                else
                    this.Base.Document.Current = (SOShipment)this.Base.Document.Search<SOShipment.shipmentNbr>((object)shipment.ShipmentNbr);
                if (this.Base.SetShipmentFieldsFromOrder(order, shipment, SiteID, ShipDate, operation, orderOperation, newlyCreated))
                    shipment = this.Base.Document.Update(shipment);
                if (newlyCreated)
                {
                    this.Base.SetShipAddressAndContact(shipment, order.ShipAddressID, order.ShipContactID);
                    soShipment = (SOShipment)this.Base.Document.Search<SOShipment.shipmentNbr>((object)this.Base.Document.Update(shipment).ShipmentNbr);
                }
            }
            else
            {
                SOShipment copy = PXCache<SOShipment>.CreateCopy(this.Base.Document.Current);
                int? orderCntr = copy.OrderCntr;
                int num2 = 0;
                bool newlyCreated = orderCntr.GetValueOrDefault() == num2 & orderCntr.HasValue;
                bool flag = this.Base.SetShipmentFieldsFromOrder(order, copy, SiteID, ShipDate, operation, orderOperation, newlyCreated);
                if (newlyCreated)
                    this.Base.SetShipAddressAndContact(copy, order.ShipAddressID, order.ShipContactID);
                if (flag)
                    soShipment = this.Base.Document.Update(copy);
            }
            SOOrderShipment soOrderShipment1 = new SOOrderShipment();
            soOrderShipment1.OrderType = order.OrderType;
            soOrderShipment1.OrderNbr = order.OrderNbr;
            soOrderShipment1.OrderNoteID = order.NoteID;
            soOrderShipment1.ShipmentNbr = this.Base.Document.Current.ShipmentNbr;
            soOrderShipment1.ShipmentType = this.Base.Document.Current.ShipmentType;
            soOrderShipment1.ShippingRefNoteID = this.Base.Document.Current.NoteID;
            soOrderShipment1.Operation = this.Base.Document.Current.Operation;
            PXParentAttribute.SetParent(this.Base.OrderList.Cache, (object)soOrderShipment1, typeof(SOOrder), (object)order);
            this.Base.OrderListSimple.Select().ToList<PXResult<SOOrderShipment>>();
            SOOrderShipment soOrderShipment2 = this.Base.OrderList.Locate(soOrderShipment1);
            SOOrderShipment soOrderShipment3 = soOrderShipment2 != null ? (!this.Base.OrderList.Cache.GetStatus((object)soOrderShipment2).IsIn<PXEntryStatus>(PXEntryStatus.Deleted, PXEntryStatus.InsertedDeleted) ? soOrderShipment2 : this.Base.OrderList.Insert(soOrderShipment2)) : this.Base.OrderList.Insert(soOrderShipment1);
            PXRowDeleting handler1 = (PXRowDeleting)((sender, e) => e.Cancel = true);
            this.Base.RowDeleting.AddHandler<SOOrderShipment>(handler1);
            bool anydeleted = false;
            PXRowDeleted handler2 = (PXRowDeleted)((sender, e) => anydeleted = true);
            this.Base.RowDeleted.AddHandler<SOShipLine>(handler2);
            foreach (PXResult<SOLine2> pxResult in PXSelectBase<SOLine2, PXSelect<SOLine2, Where<SOLine2.orderType, Equal<Required<SOLine2.orderType>>, And<SOLine2.orderNbr, Equal<Required<SOLine2.orderNbr>>, And<SOLine2.siteID, Equal<Required<SOLine2.siteID>>, And<SOLine2.operation, Equal<Required<SOLine2.operation>>, And<SOLine2.completed, NotEqual<True>>>>>>>.Config>.Select((PXGraph)this.Base, (object)order.OrderType, (object)order.OrderNbr, (object)SiteID, (object)operation))
                PXParentAttribute.SetParent(this.Base.soline.Cache, (object)(SOLine2)pxResult, typeof(SOOrder), (object)order);
            foreach (PXResult<SOLineSplit2> pxResult in PXSelectBase<SOLineSplit2, PXSelect<SOLineSplit2, Where<SOLineSplit2.orderType, Equal<Required<SOLineSplit2.orderType>>, And<SOLineSplit2.orderNbr, Equal<Required<SOLineSplit2.orderNbr>>, And<SOLineSplit2.siteID, Equal<Required<SOLineSplit2.siteID>>, And<SOLineSplit2.operation, Equal<Required<SOLineSplit2.operation>>, And<SOLineSplit2.completed, NotEqual<True>>>>>>>.Config>.Select((PXGraph)this.Base, (object)order.OrderType, (object)order.OrderNbr, (object)SiteID, (object)operation))
            {
                SOLineSplit2 soLineSplit2 = (SOLineSplit2)pxResult;
            }
            foreach (PXResult<SOShipLine> pxResult in PXSelectBase<SOShipLine, PXSelect<SOShipLine, Where<SOShipLine.shipmentType, Equal<Current<SOShipLine.shipmentType>>, And<SOShipLine.shipmentNbr, Equal<Current<SOShipLine.shipmentNbr>>>>>.Config>.Select((PXGraph)this.Base))
                PXParentAttribute.SetParent(this.Base.Transactions.Cache, (object)(SOShipLine)pxResult, typeof(SOOrder), (object)order);
            this.skipAdjustFreeItemLines = true;
            System.Collections.Generic.List<SOShipmentEntry_Extension.ShipmentSchedule> shipmentScheduleList = new System.Collections.Generic.List<SOShipmentEntry_Extension.ShipmentSchedule>();
            try
            {
                foreach (PXResult<SOShipmentPlan, SOLineSplit, SOLine, PX.Objects.IN.InventoryItem, INLotSerClass, INSite, SOShipLine> pxResult in this.ShipmentScheduleSelect2.Select((object)SiteID, (object)order.OrderType, (object)order.OrderNbr, (object)operation))
                    shipmentScheduleList.Add(new SOShipmentEntry_Extension.ShipmentSchedule(new PXResult<SOShipmentPlan, SOLineSplit, SOLine, PX.Objects.IN.InventoryItem, INLotSerClass, INSite, SOShipLine>((SOShipmentPlan)pxResult, (SOLineSplit)pxResult, (SOLine)pxResult, (PX.Objects.IN.InventoryItem)pxResult, (INLotSerClass)pxResult, (INSite)pxResult, (SOShipLine)pxResult), new SOShipLine()
                    {
                        OrigSplitLineNbr = ((SOLineSplit)pxResult).SplitLineNbr
                    }));
                Dictionary<SOLine2, SOShipmentEntry_Extension.LineShipment> dictionary = new Dictionary<SOLine2, SOShipmentEntry_Extension.LineShipment>();
                shipmentScheduleList.Sort();
                foreach (SOShipmentEntry_Extension.ShipmentSchedule shipmentSchedule in shipmentScheduleList)
                {
                    shipmentSchedule.ShipLine.ShipmentType = this.Base.Document.Current.ShipmentType;
                    shipmentSchedule.ShipLine.ShipmentNbr = this.Base.Document.Current.ShipmentNbr;
                    shipmentSchedule.ShipLine.LineNbr = (int?)PXLineNbrAttribute.NewLineNbr<SOShipLine.lineNbr>(this.Base.Transactions.Cache, (object)this.Base.Document.Current);
                    PXParentAttribute.SetParent(this.Base.Transactions.Cache, (object)shipmentSchedule.ShipLine, typeof(SOOrder), (object)order);
                    SOLine2 key = this.Base.soline.Locate(new SOLine2()
                    {
                        OrderType = ((SOLine)shipmentSchedule.Result).OrderType,
                        OrderNbr = ((SOLine)shipmentSchedule.Result).OrderNbr,
                        LineNbr = ((SOLine)shipmentSchedule.Result).LineNbr
                    });
                    if (key != null)
                        PXParentAttribute.SetParent(this.Base.Transactions.Cache, (object)shipmentSchedule.ShipLine, typeof(SOLine2), (object)key);
                    SOShipmentEntry_Extension.LineShipment lineShipment;
                    if (!dictionary.TryGetValue(key, out lineShipment))
                        lineShipment = dictionary[key] = new SOShipmentEntry_Extension.LineShipment();
                    lineShipment.Add(shipmentSchedule.ShipLine);
                    SOLineSplit2 soLineSplit2 = this.Base.solinesplit.Locate(new SOLineSplit2()
                    {
                        OrderType = ((SOLineSplit)shipmentSchedule.Result).OrderType,
                        OrderNbr = ((SOLineSplit)shipmentSchedule.Result).OrderNbr,
                        LineNbr = ((SOLineSplit)shipmentSchedule.Result).LineNbr,
                        SplitLineNbr = ((SOLineSplit)shipmentSchedule.Result).SplitLineNbr
                    });
                    if (soLineSplit2 != null)
                        PXParentAttribute.SetParent(this.Base.Transactions.Cache, (object)shipmentSchedule.ShipLine, typeof(SOLineSplit2), (object)soLineSplit2);
                    PXParentAttribute.SetParent(this.Base.Transactions.Cache, (object)shipmentSchedule.ShipLine, typeof(SOOrderShipment), (object)soOrderShipment3);
                    if (list == null || key.ShipComplete != "C" || !lineShipment.AnyDeleted)
                        lineShipment.AnyDeleted = this.CreateShipmentFromSchedules2(shipmentSchedule.Result, shipmentSchedule.ShipLine, ordertype, operation, list, plans);
                    if (list != null && key.ShipComplete == "C" && lineShipment.AnyDeleted)
                    {
                        foreach (SOShipLine soShipLine in lineShipment)
                            this.Base.Transactions.Delete(soShipLine);
                        lineShipment.Clear();
                    }
                }
                foreach (KeyValuePair<SOLine2, SOShipmentEntry_Extension.LineShipment> keyValuePair in dictionary)
                {
                    int num2;
                    if (keyValuePair.Key.ShipComplete == "C")
                    {
                        Decimal? shippedQty = keyValuePair.Key.ShippedQty;
                        Decimal? orderQty = keyValuePair.Key.OrderQty;
                        Decimal? completeQtyMin = keyValuePair.Key.CompleteQtyMin;
                        Decimal? nullable = orderQty.HasValue & completeQtyMin.HasValue ? new Decimal?(orderQty.GetValueOrDefault() * completeQtyMin.GetValueOrDefault() / 100M) : new Decimal?();
                        num2 = shippedQty.GetValueOrDefault() < nullable.GetValueOrDefault() & (shippedQty.HasValue & nullable.HasValue) ? 1 : 0;
                    }
                    else
                        num2 = 0;
                    if (num2 != 0)
                    {
                        foreach (SOShipLine shipline in keyValuePair.Value)
                            this.Base.RemoveLineFromShipment(shipline, list != null);
                    }
                }
            }
            finally
            {
                this.skipAdjustFreeItemLines = false;
            }
            bool? nullable1;
            int num3;
            if (quickProcessFlow != PXQuickProcess.ActionFlow.NoFlow)
            {
                nullable1 = this.Base.sosetup.Current.RequireShipmentTotal;
                bool flag = true;
                num3 = nullable1.GetValueOrDefault() == flag & nullable1.HasValue ? 1 : 0;
            }
            else
                num3 = 0;
            if (num3 != 0)
                this.Base.Document.Current.ControlQty = this.Base.Document.Current.ShipmentQty;
            Dictionary<SOShipmentEntry_Extension.DiscKey, Decimal> dictionary1 = new Dictionary<SOShipmentEntry_Extension.DiscKey, Decimal>();
            System.Collections.Generic.List<SOShipLine> soShipLineList = new System.Collections.Generic.List<SOShipLine>();
            bool flag1 = false;
            foreach (PXResult<SOShipLine> pxResult in this.Base.Transactions.Select())
            {
                SOShipLine soShipLine = (SOShipLine)pxResult;
                int num2;
                if (soShipLine.OrigOrderType == order.OrderType && soShipLine.OrigOrderNbr == order.OrderNbr)
                {
                    nullable1 = soShipLine.IsFree;
                    bool flag2 = false;
                    num2 = nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue ? 1 : 0;
                }
                else
                    num2 = 0;
                if (num2 != 0)
                    soShipLineList.Add(soShipLine);
                nullable1 = soShipLine.IsFree;
                bool flag3 = true;
                if (nullable1.GetValueOrDefault() == flag3 & nullable1.HasValue)
                    flag1 = true;
            }
            bool flag4 = DiscountEngine.ApplyQuantityDiscountByBaseUOMForAR((PXGraph)this.Base);
            if (flag1)
            {
                PXCache cach = this.Base.Caches[typeof(SOLine)];
                PXSelectBase<SOLine> documentDetailsSelect = (PXSelectBase<SOLine>)new PXSelect<SOLine, Where<SOLine.orderType, Equal<Current<SOOrder.orderType>>, And<SOLine.orderNbr, Equal<Current<SOOrder.orderNbr>>>>>((PXGraph)this.Base);
                PXSelectBase<SOOrderDiscountDetail> discountDetailsSelect = (PXSelectBase<SOOrderDiscountDetail>)new PXSelect<SOOrderDiscountDetail, Where<SOOrderDiscountDetail.orderType, Equal<Current<SOOrder.orderType>>, And<SOOrderDiscountDetail.orderNbr, Equal<Current<SOOrder.orderNbr>>>>>((PXGraph)this.Base);
                TwoWayLookup<SOOrderDiscountDetail, SOLine> andDocumentLines = DiscountEngineProvider.GetEngineFor<SOLine, SOOrderDiscountDetail>().GetListOfLinksBetweenDiscountsAndDocumentLines(cach, documentDetailsSelect, discountDetailsSelect);
                if (this.Base.sosetup.Current.FreeItemShipping == "P")
                {
                    foreach (SOOrderDiscountDetail left in andDocumentLines.LeftValues.Where<SOOrderDiscountDetail>((Func<SOOrderDiscountDetail, bool>)(x =>
                   {
                       Decimal? freeItemQty = x.FreeItemQty;
                       Decimal num2 = 0M;
                       return freeItemQty.GetValueOrDefault() > num2 & freeItemQty.HasValue;
                   })))
                    {
                        Decimal num2 = 0M;
                        foreach (SOLine soLine in andDocumentLines.RightsFor(left))
                        {
                            foreach (SOShipLine soShipLine in soShipLineList)
                            {
                                int? lineNbr = soLine.LineNbr;
                                int? origLineNbr = soShipLine.OrigLineNbr;
                                if (lineNbr.GetValueOrDefault() == origLineNbr.GetValueOrDefault() & lineNbr.HasValue == origLineNbr.HasValue)
                                    num2 += (flag4 ? soShipLine.BaseShippedQty : soShipLine.ShippedQty).GetValueOrDefault();
                            }
                        }
                        Decimal d = num2 * left.FreeItemQty.Value / left.DiscountableQty.Value;
                        SOShipmentEntry_Extension.DiscKey key = new SOShipmentEntry_Extension.DiscKey(left.DiscountID, left.DiscountSequenceID, left.FreeItemID.Value);
                        dictionary1.Add(key, Math.Floor(d));
                    }
                }
                else
                {
                    foreach (SOOrderDiscountDetail left in andDocumentLines.LeftValues.Where<SOOrderDiscountDetail>((Func<SOOrderDiscountDetail, bool>)(x =>
                   {
                       Decimal? freeItemQty = x.FreeItemQty;
                       Decimal num2 = 0M;
                       return freeItemQty.GetValueOrDefault() > num2 & freeItemQty.HasValue;
                   })))
                    {
                        Decimal num2 = 0M;
                        Decimal num4 = 0M;
                        Decimal num5 = 0M;
                        Decimal num6 = 0M;
                        Decimal? nullable2;
                        foreach (SOLine soLine in andDocumentLines.RightsFor(left))
                        {
                            SOLine2 soLine2 = (SOLine2)this.Base.Caches[typeof(SOLine2)].Locate((object)new SOLine2()
                            {
                                OrderType = soLine.OrderType,
                                OrderNbr = soLine.OrderNbr,
                                LineNbr = soLine.LineNbr
                            });
                            if (soLine2 != null)
                            {
                                num6 += soLine.Qty.GetValueOrDefault();
                                if (soLine.ShipComplete == "B")
                                {
                                    num4 += soLine.Qty.GetValueOrDefault();
                                    Decimal? shippedQty = soLine2.ShippedQty;
                                    nullable2 = soLine.OrderQty;
                                    if (shippedQty.GetValueOrDefault() >= nullable2.GetValueOrDefault() & (shippedQty.HasValue & nullable2.HasValue))
                                    {
                                        int? lineNbr1 = soLine.LineNbr;
                                        int? lineNbr2 = soLine2.LineNbr;
                                        if (lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue)
                                        {
                                            Decimal num7 = num2;
                                            nullable2 = soLine2.ShippedQty;
                                            Decimal valueOrDefault = nullable2.GetValueOrDefault();
                                            num2 = num7 + valueOrDefault;
                                        }
                                    }
                                }
                                else
                                {
                                    Decimal num7 = num5;
                                    nullable2 = soLine2.ShippedQty;
                                    Decimal valueOrDefault = nullable2.GetValueOrDefault();
                                    num5 = num7 + valueOrDefault;
                                }
                            }
                        }
                        Decimal d;
                        if (num5 + num2 < num6)
                        {
                            Decimal num7 = num5 + num2;
                            nullable2 = left.DiscountableQty;
                            Decimal num8 = nullable2.Value;
                            Decimal num9 = num7 / num8;
                            nullable2 = left.FreeItemQty;
                            Decimal num10 = nullable2.Value;
                            d = num9 * num10;
                        }
                        else
                        {
                            nullable2 = left.FreeItemQty;
                            d = nullable2.Value;
                        }
                        SOShipmentEntry_Extension.DiscKey key = new SOShipmentEntry_Extension.DiscKey(left.DiscountID, left.DiscountSequenceID, left.FreeItemID.Value);
                        dictionary1.Add(key, num2 >= num4 ? Math.Floor(d) : 0M);
                    }
                }
                foreach (KeyValuePair<SOShipmentEntry_Extension.DiscKey, Decimal> keyValuePair in dictionary1)
                {
                    SOShipmentDiscountDetail newTrace = new SOShipmentDiscountDetail();
                    newTrace.Type = "L";
                    newTrace.OrderType = order.OrderType;
                    newTrace.OrderNbr = order.OrderNbr;
                    SOShipmentDiscountDetail shipmentDiscountDetail1 = newTrace;
                    SOShipmentEntry_Extension.DiscKey key = keyValuePair.Key;
                    string discId = key.DiscID;
                    shipmentDiscountDetail1.DiscountID = discId;
                    SOShipmentDiscountDetail shipmentDiscountDetail2 = newTrace;
                    key = keyValuePair.Key;
                    string discSeqId = key.DiscSeqID;
                    shipmentDiscountDetail2.DiscountSequenceID = discSeqId;
                    SOShipmentDiscountDetail shipmentDiscountDetail3 = newTrace;
                    key = keyValuePair.Key;
                    int? nullable2 = new int?(key.FreeItemID);
                    shipmentDiscountDetail3.FreeItemID = nullable2;
                    newTrace.FreeItemQty = new Decimal?(keyValuePair.Value);
                    SOShipmentDiscountDetail trace = (SOShipmentDiscountDetail)PXSelectBase<SOShipmentDiscountDetail, PXSelect<SOShipmentDiscountDetail, Where<SOShipmentDiscountDetail.shipmentNbr, Equal<Current<SOShipment.shipmentNbr>>, And<SOShipmentDiscountDetail.orderType, Equal<Required<SOShipmentDiscountDetail.orderType>>, And<SOShipmentDiscountDetail.orderNbr, Equal<Required<SOShipmentDiscountDetail.orderNbr>>, And<SOShipmentDiscountDetail.type, Equal<Required<SOShipmentDiscountDetail.type>>, And<SOShipmentDiscountDetail.discountID, Equal<Required<SOShipmentDiscountDetail.discountID>>, And<SOShipmentDiscountDetail.discountSequenceID, Equal<Required<SOShipmentDiscountDetail.discountSequenceID>>>>>>>>>.Config>.Select((PXGraph)this.Base, (object)newTrace.OrderType, (object)newTrace.OrderNbr, (object)newTrace.Type, (object)newTrace.DiscountID, (object)newTrace.DiscountSequenceID);
                    if (trace != null)
                    {
                        trace.DiscountableQty = newTrace.DiscountableQty;
                        trace.DiscountPct = newTrace.DiscountPct;
                        trace.FreeItemID = newTrace.FreeItemID;
                        trace.FreeItemQty = newTrace.FreeItemQty;
                        this._discountEngine.UpdateDiscountDetail(this.Base.DiscountDetails.Cache, (PXSelectBase<SOShipmentDiscountDetail>)this.Base.DiscountDetails, trace);
                    }
                    else
                        this._discountEngine.InsertDiscountDetail(this.Base.DiscountDetails.Cache, (PXSelectBase<SOShipmentDiscountDetail>)this.Base.DiscountDetails, newTrace);
                }
            }
            foreach (PXResult<SOShipLine> pxResult1 in this.Base.Transactions.Select())
            {
                SOShipLine soShipLine = (SOShipLine)pxResult1;
                bool? nullable2 = soShipLine.IsFree;
                bool flag2 = true;
                int num2;
                if (nullable2.GetValueOrDefault() == flag2 & nullable2.HasValue)
                {
                    nullable2 = soShipLine.ManualDisc;
                    bool flag3 = true;
                    num2 = !(nullable2.GetValueOrDefault() == flag3 & nullable2.HasValue) ? 1 : 0;
                }
                else
                    num2 = 0;
                if (num2 != 0)
                {
                    if (this.skipAdjustFreeItemLines)
                        return;
                    PXResultset<SOShipmentDiscountDetail> pxResultset = new PXSelect<SOShipmentDiscountDetail, Where<SOShipmentDiscountDetail.shipmentNbr, Equal<Current<SOShipment.shipmentNbr>>, And<SOShipmentDiscountDetail.freeItemID, Equal<Required<SOShipmentDiscountDetail.freeItemID>>, And<SOShipmentDiscountDetail.orderType, Equal<Required<SOShipmentDiscountDetail.orderType>>, And<SOShipmentDiscountDetail.orderNbr, Equal<Required<SOShipmentDiscountDetail.orderNbr>>>>>>>((PXGraph)this.Base).Select((object)soShipLine.InventoryID, (object)soShipLine.OrigOrderType, (object)soShipLine.OrigOrderNbr);
                    if ((uint)pxResultset.Count > 0U)
                    {
                        Decimal? nullable3 = new Decimal?(0M);
                        foreach (PXResult<SOShipmentDiscountDetail> pxResult2 in pxResultset)
                        {
                            SOShipmentDiscountDetail shipmentDiscountDetail = (SOShipmentDiscountDetail)pxResult2;
                            int num4;
                            if (shipmentDiscountDetail.FreeItemID.HasValue)
                            {
                                Decimal? freeItemQty = shipmentDiscountDetail.FreeItemQty;
                                if (freeItemQty.HasValue)
                                {
                                    freeItemQty = shipmentDiscountDetail.FreeItemQty;
                                    num4 = freeItemQty.Value > 0M ? 1 : 0;
                                    goto label_182;
                                }
                            }
                            num4 = 0;
                        label_182:
                            if (num4 != 0)
                            {
                                Decimal? nullable4 = nullable3;
                                Decimal? nullable5 = shipmentDiscountDetail.FreeItemQty;
                                Decimal num5 = nullable5.Value;
                                Decimal? nullable6;
                                if (!nullable4.HasValue)
                                {
                                    nullable5 = new Decimal?();
                                    nullable6 = nullable5;
                                }
                                else
                                    nullable6 = new Decimal?(nullable4.GetValueOrDefault() + num5);
                                nullable3 = nullable6;
                            }
                        }
                        SOShipLine copy = PXCache<SOShipLine>.CreateCopy(soShipLine);
                        copy.ShippedQty = nullable3;
                        this.Base.FreeItems.Update(copy);
                    }
                }
            }
            this.Base.Transactions.View.RequestRefresh();
            Decimal? nullable7;
            if (this.Base.Document.Current != null && order != null)
            {
                bool flag2 = false;
                PX.Objects.CS.Carrier carrier = PX.Objects.CS.Carrier.PK.Find((PXGraph)this.Base, order.ShipVia);
                if (carrier != null && carrier.CalcMethod == "M")
                {
                    int num2;
                    if (this.Base.sosetup.Current != null && this.Base.sosetup.Current.FreightAllocation == "A")
                    {
                        int? shipmentCntr = order.ShipmentCntr;
                        int num4 = 1;
                        num2 = shipmentCntr.GetValueOrDefault() > num4 & shipmentCntr.HasValue ? 1 : 0;
                    }
                    else
                        num2 = 0;
                    if (num2 != 0)
                        return;
                    SOShipment copy = PXCache<SOShipment>.CreateCopy(this.Base.Document.Current);
                    nullable7 = this.Base.Document.Current.ShipmentQty;
                    Decimal num5 = 0M;
                    Decimal num6;
                    if (!(nullable7.GetValueOrDefault() > num5 & nullable7.HasValue))
                        num6 = 0M;
                    else if (flag2)
                    {
                        nullable7 = order.CuryFreightCost;
                        num6 = -nullable7.GetValueOrDefault();
                    }
                    else
                    {
                        nullable7 = order.CuryFreightCost;
                        num6 = nullable7.GetValueOrDefault();
                    }
                    Decimal num7 = num6;
                    int num8;
                    if (this.Base.sosetup.Current != null && this.Base.sosetup.Current.FreightAllocation == "P")
                    {
                        nullable7 = order.OrderQty;
                        if (nullable7.HasValue)
                        {
                            nullable7 = order.OrderQty;
                            Decimal num4 = 0M;
                            num8 = nullable7.GetValueOrDefault() > num4 & nullable7.HasValue ? 1 : 0;
                            goto label_214;
                        }
                    }
                    num8 = 0;
                label_214:
                    if (num8 != 0)
                    {
                        nullable7 = this.Base.Document.Current.ShipmentQty;
                        Decimal valueOrDefault = nullable7.GetValueOrDefault();
                        nullable7 = order.OrderQty;
                        Decimal num4 = nullable7 ?? 1M;
                        num7 = valueOrDefault / num4 * num7;
                    }
                    Decimal num9;
                    if (!flag2)
                    {
                        nullable7 = copy.CuryFreightCost;
                        num9 = nullable7.GetValueOrDefault() + num7;
                    }
                    else
                    {
                        nullable7 = copy.CuryFreightCost;
                        Decimal valueOrDefault = nullable7.GetValueOrDefault();
                        nullable7 = copy.CuryFreightCost;
                        Decimal num4 = nullable7.GetValueOrDefault() + num7;
                        num9 = valueOrDefault - num4;
                    }
                    Decimal curyval = num9;
                    Decimal baseval = 0M;
                    PXCurrencyAttribute.CuryConvBase<SOOrder.curyInfoID>(this.Base.soorder.Cache, (object)order, curyval, out baseval);
                    copy.CuryFreightCost = new Decimal?(baseval);
                    this.Base.Document.Update(copy);
                }
            }
            this.Base.RowDeleting.RemoveHandler<SOOrderShipment>(handler1);
            this.Base.RowDeleted.RemoveHandler<SOShipLine>(handler2);
            foreach (SOOrderShipment soOrderShipment4 in this.Base.OrderList.Cache.Inserted)
            {
                int num2;
                if (list == null)
                {
                    nullable7 = soOrderShipment4.ShipmentQty;
                    Decimal num4 = 0M;
                    num2 = nullable7.GetValueOrDefault() == num4 & nullable7.HasValue ? 1 : 0;
                }
                else
                    num2 = 0;
                if (num2 != 0)
                {
                    if ((SOShipLine)PXSelectBase<SOShipLine, PXSelect<SOShipLine, Where<SOShipLine.shipmentType, Equal<Required<SOOrderShipment.shipmentType>>, And<SOShipLine.shipmentNbr, Equal<Required<SOOrderShipment.shipmentNbr>>, And<SOShipLine.origOrderType, Equal<Required<SOOrderShipment.orderType>>, And<SOShipLine.origOrderNbr, Equal<Required<SOOrderShipment.orderNbr>>>>>>>.Config>.SelectSingleBound((PXGraph)this.Base, (object[])null, (object)soOrderShipment4.ShipmentType, (object)soOrderShipment4.ShipmentNbr, (object)soOrderShipment4.OrderType, (object)soOrderShipment4.OrderNbr) == null)
                        this.Base.OrderList.Delete(soOrderShipment4);
                }
                try
                {
                    int num4;
                    if (list != null)
                    {
                        int? lineCntr = soOrderShipment4.LineCntr;
                        int num5 = 0;
                        if (lineCntr.GetValueOrDefault() > num5 & lineCntr.HasValue)
                        {
                            nullable7 = soOrderShipment4.ShipmentQty;
                            Decimal num6 = 0M;
                            if (nullable7.GetValueOrDefault() == num6 & nullable7.HasValue)
                            {
                                bool? nullable2 = this.Base.sosetup.Current.AddAllToShipment;
                                bool flag2 = true;
                                if (nullable2.GetValueOrDefault() == flag2 & nullable2.HasValue)
                                {
                                    nullable2 = this.Base.sosetup.Current.CreateZeroShipments;
                                    bool flag3 = true;
                                    num4 = !(nullable2.GetValueOrDefault() == flag3 & nullable2.HasValue) ? 1 : 0;
                                    goto label_237;
                                }
                            }
                        }
                    }
                    num4 = 0;
                label_237:
                    if (num4 != 0)
                        throw new SOShipmentException("Order {0} {1} does not contain any available items. Check previous warnings.", new object[2]
                        {
              (object) soOrderShipment4.OrderType,
              (object) soOrderShipment4.OrderNbr
                        });
                    int num7;
                    if (list != null)
                    {
                        int? lineCntr = soOrderShipment4.LineCntr;
                        int num5 = 0;
                        num7 = lineCntr.GetValueOrDefault() == num5 & lineCntr.HasValue ? 1 : 0;
                    }
                    else
                        num7 = 0;
                    if (num7 != 0)
                    {
                        if (anydeleted)
                            throw new SOShipmentException("Order {0} {1} cannot be shipped in full. Check Trace for more details.", new object[2]
                            {
                (object) soOrderShipment4.OrderType,
                (object) soOrderShipment4.OrderNbr
                            });
                        if (operation == "I")
                            throw new SOShipmentException("Order {0} {1} does not contain any items planned for shipment on '{2}'.", new object[3]
                            {
                (object) soOrderShipment4.OrderType,
                (object) soOrderShipment4.OrderNbr,
                (object) soOrderShipment4.ShipDate
                            });
                        throw new SOShipmentException("Order {0} {1} does not contain any items planned for receipt on '{2}'.", new object[3]
                        {
              (object) soOrderShipment4.OrderType,
              (object) soOrderShipment4.OrderNbr,
              (object) soOrderShipment4.ShipDate
                        });
                    }
                    if (list != null && soOrderShipment4.ShipComplete == "C")
                    {
                        foreach (PXResult<SOLine2> pxResult in PXSelectBase<SOLine2, PXSelect<SOLine2, Where<SOLine2.orderType, Equal<Required<SOLine2.orderType>>, And<SOLine2.orderNbr, Equal<Required<SOLine2.orderNbr>>, And<SOLine2.siteID, Equal<Required<SOLine2.siteID>>, And<SOLine2.operation, Equal<Required<SOLine2.operation>>, And<SOLine2.completed, NotEqual<True>>>>>>>.Config>.Select((PXGraph)this.Base, (object)soOrderShipment4.OrderType, (object)soOrderShipment4.OrderNbr, (object)soOrderShipment4.SiteID, (object)soOrderShipment4.Operation))
                        {
                            SOLine2 soLine2 = (SOLine2)pxResult;
                            int num5;
                            if (soLine2.LineType == "GI")
                            {
                                nullable7 = soLine2.ShippedQty;
                                Decimal num6 = 0M;
                                if (nullable7.GetValueOrDefault() == num6 & nullable7.HasValue && DateTime.Compare(soLine2.ShipDate.Value, soOrderShipment4.ShipDate.Value) <= 0)
                                {
                                    num5 = soLine2.POSource != "D" ? 1 : 0;
                                    goto label_255;
                                }
                            }
                            num5 = 0;
                        label_255:
                            if (num5 != 0)
                                throw new SOShipmentException("Order {0} {1} cannot be shipped in full. Check Trace for more details.", new object[2]
                                {
                  (object) order.OrderType,
                  (object) order.OrderNbr
                                });
                        }
                    }
                }
                catch (SOShipmentException ex)
                {
                    SOOrder soOrder1 = (SOOrder)PXParentAttribute.SelectParent(this.Base.soorder.Cache, (object)this.Base.soorder.Current, typeof(SOOrder));
                    bool? nullable2;
                    if (soOrder1 != null)
                    {
                        soOrder1.ShipmentDeleted = new bool?(true);
                        SOOrder soOrder2 = soOrder1;
                        int? shipmentCntr = soOrder2.ShipmentCntr;
                        soOrder2.ShipmentCntr = shipmentCntr.HasValue ? new int?(shipmentCntr.GetValueOrDefault() - 1) : new int?();
                        nullable2 = ((SOOrderShipment)this.Base.soorder.Current).Confirmed;
                        bool flag2 = false;
                        if (nullable2.GetValueOrDefault() == flag2 & nullable2.HasValue)
                        {
                            SOOrder soOrder3 = soOrder1;
                            int? openShipmentCntr = soOrder3.OpenShipmentCntr;
                            soOrder3.OpenShipmentCntr = openShipmentCntr.HasValue ? new int?(openShipmentCntr.GetValueOrDefault() - 1) : new int?();
                        }
                        this.Base.soorder.Cache.SetStatus((object)order, PXEntryStatus.Updated);
                    }
                    SOOrder soOrder4 = (SOOrder)PXParentAttribute.SelectParent(this.Base.soorder.Cache, (object)this.Base.soorder.Current, typeof(SOOrder));
                    if (soOrder4 != null)
                    {
                        SOOrder soOrder2 = soOrder4;
                        nullable2 = new bool?();
                        bool? nullable3 = nullable2;
                        soOrder2.ShipmentDeleted = nullable3;
                        SOOrder soOrder3 = soOrder4;
                        int? nullable4 = soOrder3.ShipmentCntr;
                        soOrder3.ShipmentCntr = nullable4.HasValue ? new int?(nullable4.GetValueOrDefault()) : new int?();
                        nullable2 = ((SOOrderShipment)this.Base.soorder.Current).Confirmed;
                        bool flag2 = false;
                        if (nullable2.GetValueOrDefault() == flag2 & nullable2.HasValue)
                        {
                            SOOrder soOrder5 = soOrder4;
                            nullable4 = soOrder5.OpenShipmentCntr;
                            soOrder5.OpenShipmentCntr = nullable4.HasValue ? new int?(nullable4.GetValueOrDefault()) : new int?();
                        }
                        this.Base.soorder.Cache.SetStatus((object)order, PXEntryStatus.Updated);
                    }
                    throw;
                }
            }
            int? openShipmentCntr1 = order.OpenShipmentCntr;
            int num11 = 0;
            if (openShipmentCntr1.GetValueOrDefault() > num11 & openShipmentCntr1.HasValue)
            {
                order.Status = "S";
                order.Hold = new bool?(false);
                this.Base.soorder.Update(order);
            }
            if (list != null)
            {
                if (this.Base.OrderList.Cache.Inserted.Count() > 0L || this.Base.OrderList.SelectWindowed(0, 1) != null)
                {
                    PXAutomation.CompleteSimple(this.Base.soorder.View);
                    this.Base.Save.Press();
                    PXAutomation.RemovePersisted<SOOrder>((PXGraph)this.Base, order);
                    SOOrder copy;
                    if ((copy = this.Base.soorder.Locate(order)) != null)
                    {
                        bool? selected = order.Selected;
                        PXCache<SOOrder>.RestoreCopy(order, copy);
                        order.Selected = selected;
                    }
                    if (list.Find((object)this.Base.Document.Current) == null)
                        list.Add(this.Base.Document.Current);
                }
                else
                    PXAutomation.StorePersisted((PXGraph)this.Base, typeof(SOOrder), new System.Collections.Generic.List<object>()
          {
            (object) order
          });
            }
            ItemLotSerialAccumulatorAttribute.ForceAvailQtyValidation((PXGraph)this.Base, false);
            SiteLotSerialAccumulatorAttribute.ForceAvailQtyValidation((PXGraph)this.Base, false);
        }

        private bool CreateShipmentFromSchedules2(
          PXResult<SOShipmentPlan, SOLineSplit, SOLine, PX.Objects.IN.InventoryItem, INLotSerClass, INSite, SOShipLine> res,
          SOShipLine newline,
          SOOrderType ordertype,
          string operation,
          DocumentList<SOShipment> list,
          System.Collections.Generic.List<SOShipmentPlan> plans)
        {
            bool flag1 = false;
            SOShipmentPlan plan = (SOShipmentPlan)res;
            SOLine soline = (SOLine)res;
            SOLineSplit soSplit = (SOLineSplit)res;
            INSite inSite = (INSite)res;
            plan = plans.Find((Predicate<SOShipmentPlan>)(x =>
           {
               if (x.OrderType == plan.OrderType && x.OrderNbr == plan.OrderNbr && x.LotSerialNbr == plan.LotSerialNbr)
               {
                   Decimal? planQty1 = x.PlanQty;
                   Decimal? planQty2 = plan.PlanQty;
                   if (planQty1.GetValueOrDefault() == planQty2.GetValueOrDefault() & planQty1.HasValue == planQty2.HasValue)
                   {
                       DateTime? planDate1 = x.PlanDate;
                       DateTime? planDate2 = plan.PlanDate;
                       if (planDate1.HasValue != planDate2.HasValue)
                           return false;
                       return !planDate1.HasValue || planDate1.GetValueOrDefault() == planDate2.GetValueOrDefault();
                   }
               }
               return false;
           }));
            if (plan == null)
                return false;
            bool? selected = plan.Selected;
            bool flag2 = true;
            int num1;
            if (!(selected.GetValueOrDefault() == flag2 & selected.HasValue))
            {
                if (list != null)
                {
                    bool? requireAllocation = plan.RequireAllocation;
                    bool flag3 = false;
                    if (!(requireAllocation.GetValueOrDefault() == flag3 & requireAllocation.HasValue))
                    {
                        short? nullable1 = plan.InclQtySOShipping;
                        int? nullable2 = nullable1.HasValue ? new int?((int)nullable1.GetValueOrDefault()) : new int?();
                        int num2 = 0;
                        if (nullable2.GetValueOrDefault() == num2 & nullable2.HasValue)
                        {
                            nullable1 = plan.InclQtySOShipped;
                            int? nullable3 = nullable1.HasValue ? new int?((int)nullable1.GetValueOrDefault()) : new int?();
                            int num3 = 0;
                            if (nullable3.GetValueOrDefault() == num3 & nullable3.HasValue)
                            {
                                num1 = soSplit.LineType == "GN" ? 1 : 0;
                                goto label_11;
                            }
                        }
                    }
                    num1 = 1;
                }
                else
                    num1 = 0;
            }
            else
                num1 = 1;
            label_11:
            if (num1 != 0)
            {
                newline.OrigOrderType = soline.OrderType;
                newline.OrigOrderNbr = soline.OrderNbr;
                newline.OrigLineNbr = soline.LineNbr;
                SOShipLine soShipLine1 = newline;
                bool? poCreate = soSplit.POCreate;
                bool flag3 = true;
                string planType;
                if (!(poCreate.GetValueOrDefault() == flag3 & poCreate.HasValue))
                {
                    bool? isAllocated = soSplit.IsAllocated;
                    bool flag4 = true;
                    if (!(isAllocated.GetValueOrDefault() == flag4 & isAllocated.HasValue))
                    {
                        planType = soSplit.PlanType;
                        goto label_16;
                    }
                }
                planType = plan.PlanType;
            label_16:
                soShipLine1.OrigPlanType = planType;
                newline.InventoryID = soline.InventoryID;
                newline.SubItemID = soline.SubItemID;
                newline.SiteID = soline.SiteID;
                newline.TranDesc = soline.TranDesc;
                newline.CustomerID = soline.CustomerID;
                newline.InvtMult = soline.InvtMult;
                newline.Operation = soline.Operation;
                newline.LineType = soline.LineType;
                newline.ReasonCode = soline.ReasonCode;
                newline.ProjectID = soline.ProjectID;
                newline.TaskID = soline.TaskID;
                newline.CostCodeID = soline.CostCodeID;
                newline.UOM = soline.UOM;
                newline.IsFree = soline.IsFree;
                newline.ManualDisc = soline.ManualDisc;
                newline.DiscountID = soline.DiscountID;
                newline.DiscountSequenceID = soline.DiscountSequenceID;
                newline.AlternateID = soline.AlternateID;
                this.Base.UpdateOrigValues(ref newline, soline, (INItemPlan)null, plan.PlanQty);
                INLotSerClass inLotSerClass = (INLotSerClass)res;
                Decimal? nullable1;
                bool? nullable2;
                if (inLotSerClass.LotSerTrack == null)
                {
                    newline.ShippedQty = new Decimal?(INUnitAttribute.ConvertFromBase<SOShipLine.inventoryID>(this.Base.Transactions.Cache, (object)newline, newline.UOM, plan.PlanQty.Value, INPrecision.QUANTITY));
                    newline = this.Base.lsselect.InsertMasterWithoutSplits(newline);
                    try
                    {
                        this.Base.ShipAvailable(plan, newline, new PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>((PX.Objects.IN.InventoryItem)res, (INLotSerClass)res));
                    }
                    catch (PXException ex)
                    {
                        this.Base.lsselect.Delete(newline);
                        throw ex;
                    }
                }
                else if (operation == "R")
                {
                    SOShipLine soShipLine2 = newline;
                    PXCache cache = this.Base.Transactions.Cache;
                    int? inventoryId = newline.InventoryID;
                    string uom = newline.UOM;
                    nullable1 = plan.PlanQty;
                    Decimal num2 = nullable1.Value;
                    Decimal? nullable3 = new Decimal?(INUnitAttribute.ConvertFromBase(cache, inventoryId, uom, num2, INPrecision.QUANTITY));
                    soShipLine2.ShippedQty = nullable3;
                    newline.LocationID = inSite.ReturnLocationID;
                    if (!newline.LocationID.HasValue && list != null)
                        throw new PXException("RMA Location is not configured for warehouse {0}", new object[1]
                        {
              (object) inSite.SiteCD
                        });
                    newline = this.Base.Transactions.Insert(newline);
                    this.Base.ReceiveLotSerial(plan, newline, soSplit, new PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>((PX.Objects.IN.InventoryItem)res, (INLotSerClass)res));
                }
                else
                {
                    SOShipLine soShipLine2 = (SOShipLine)this.Base.Transactions.Cache.Locate((object)newline);
                    if (soShipLine2 == null || this.Base.Transactions.Cache.GetStatus((object)soShipLine2) == PXEntryStatus.Deleted || this.Base.Transactions.Cache.GetStatus((object)soShipLine2) == PXEntryStatus.InsertedDeleted)
                    {
                        newline.ShippedQty = new Decimal?(0M);
                        newline = this.Base.lsselect.InsertMasterWithoutSplits(newline);
                    }
                    SOShipLine soShipLine3 = newline;
                    bool? manualAssignRequired = inLotSerClass.IsManualAssignRequired;
                    bool flag4 = true;
                    int num2;
                    if (manualAssignRequired.GetValueOrDefault() == flag4 & manualAssignRequired.HasValue)
                    {
                        Decimal? planQty = plan.PlanQty;
                        Decimal num3 = 0M;
                        if (planQty.GetValueOrDefault() > num3 & planQty.HasValue && string.IsNullOrEmpty(plan.LotSerialNbr))
                        {
                            num2 = inLotSerClass.LotSerAssign != "U" ? 1 : (newline.ShipmentType != "T" ? 1 : 0);
                            goto label_30;
                        }
                    }
                    num2 = 0;
                label_30:
                    bool? nullable3 = new bool?(num2 != 0);
                    soShipLine3.IsUnassigned = nullable3;
                    Decimal? nullable4 = this.Base.ShipAvailable(plan, newline, new PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>((PX.Objects.IN.InventoryItem)res, (INLotSerClass)res));
                    nullable2 = newline.IsUnassigned;
                    bool flag5 = true;
                    if (nullable2.GetValueOrDefault() == flag5 & nullable2.HasValue)
                    {
                        SOShipLine copy = (SOShipLine)this.Base.Transactions.Cache.CreateCopy((object)newline);
                        SOShipLine soShipLine4 = newline;
                        Decimal? planQty = plan.PlanQty;
                        Decimal? nullable5 = nullable4;
                        Decimal? nullable6 = planQty.HasValue & nullable5.HasValue ? new Decimal?(planQty.GetValueOrDefault() - nullable5.GetValueOrDefault()) : new Decimal?();
                        soShipLine4.UnassignedQty = nullable6;
                        SOShipLine soShipLine5 = newline;
                        nullable5 = plan.PlanQty;
                        Decimal? nullable7 = nullable4;
                        Decimal? nullable8 = nullable5.HasValue & nullable7.HasValue ? new Decimal?(nullable5.GetValueOrDefault() - nullable7.GetValueOrDefault()) : new Decimal?();
                        soShipLine5.BaseShippedQty = nullable8;
                        SOShipLine soShipLine6 = newline;
                        PXCache cache = this.Base.unassignedSplits.Cache;
                        int? inventoryId = newline.InventoryID;
                        string uom = newline.UOM;
                        nullable7 = newline.BaseShippedQty;
                        Decimal num3 = nullable7.Value;
                        Decimal? nullable9 = new Decimal?(INUnitAttribute.ConvertFromBase(cache, inventoryId, uom, num3, INPrecision.QUANTITY));
                        soShipLine6.ShippedQty = nullable9;
                        this.Base.lsselect.SuppressedMode = true;
                        try
                        {
                            this.Base.Transactions.Cache.RaiseFieldUpdated<SOShipLine.shippedQty>((object)newline, (object)copy.ShippedQty);
                            this.Base.Transactions.Cache.RaiseRowUpdated((object)newline, (object)copy);
                        }
                        finally
                        {
                            this.Base.lsselect.SuppressedMode = false;
                        }
                    }
                }
                nullable1 = newline.BaseShippedQty;
                Decimal? nullable10 = plan.PlanQty;
                if (nullable1.GetValueOrDefault() < nullable10.GetValueOrDefault() & (nullable1.HasValue & nullable10.HasValue) && string.IsNullOrEmpty(plan.LotSerialNbr))
                    this.Base.PromptReplenishment(this.Base.Transactions.Cache, newline, (PX.Objects.IN.InventoryItem)res, plan);
                PXNoteAttribute.CopyNoteAndFiles(this.Base.Caches[typeof(SOLine)], (object)soline, this.Base.Caches[typeof(SOShipLine)], (object)newline, ordertype.CopyLineNotesToShipment, ordertype.CopyLineFilesToShipment);
                nullable10 = newline.ShippedQty;
                Decimal num4 = 0M;
                if (nullable10.GetValueOrDefault() == num4 & nullable10.HasValue)
                {
                    SOShipmentEntry soShipmentEntry = this.Base;
                    SOShipLine shipline = newline;
                    int num2;
                    if (list != null)
                    {
                        nullable2 = this.Base.sosetup.Current.AddAllToShipment;
                        bool flag4 = false;
                        num2 = nullable2.GetValueOrDefault() == flag4 & nullable2.HasValue ? 1 : 0;
                    }
                    else
                        num2 = 0;
                    flag1 = soShipmentEntry.RemoveLineFromShipment(shipline, num2 != 0);
                }
                nullable10 = newline.BaseShippedQty;
                Decimal? planQty1 = plan.PlanQty;
                Decimal? completeQtyMin = soline.CompleteQtyMin;
                nullable1 = planQty1.HasValue & completeQtyMin.HasValue ? new Decimal?(planQty1.GetValueOrDefault() * completeQtyMin.GetValueOrDefault() / 100M) : new Decimal?();
                if (nullable10.GetValueOrDefault() < nullable1.GetValueOrDefault() & (nullable10.HasValue & nullable1.HasValue) && soline.ShipComplete == "C")
                    flag1 = this.Base.RemoveLineFromShipment(newline, list != null);
                int num5;
                if (!flag1 && plan.PlanType != soSplit.PlanType)
                {
                    nullable2 = soSplit.POCreate;
                    bool flag4 = true;
                    if (!(nullable2.GetValueOrDefault() == flag4 & nullable2.HasValue))
                    {
                        nullable2 = soSplit.IsAllocated;
                        bool flag5 = true;
                        num5 = !(nullable2.GetValueOrDefault() == flag5 & nullable2.HasValue) ? 1 : 0;
                        goto label_48;
                    }
                }
                num5 = 0;
            label_48:
                if (num5 != 0)
                {
                    INItemPlan inItemPlan = (INItemPlan)PXSelectBase<INItemPlan, PXSelect<INItemPlan, Where<INItemPlan.planID, Equal<Required<INItemPlan.planID>>>>.Config>.Select((PXGraph)this.Base, (object)plan.PlanID);
                    if (inItemPlan != null)
                    {
                        inItemPlan.PlanType = soSplit.PlanType;
                        this.Base.Caches[typeof(INItemPlan)].Update((object)inItemPlan);
                    }
                }
            }
            return flag1;
        }

        public static int TotalPackageCount(string shipmentNbr)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();

            foreach (PXResult<SOPackageDetail> pxResult in SelectFrom<SOPackageDetail>.Where<SOPackageDetail.shipmentNbr.IsEqual<P.AsString>>.View.Select((PXGraph)PXGraph.CreateInstance<SOShipmentEntry>(), (object)shipmentNbr))
            {
                SOPackageDetail soPackageDetail = (SOPackageDetail)pxResult;
                SOPackageDetailExt extension = soPackageDetail.GetExtension<SOPackageDetailExt>();
                if (extension.UsrCarton != null && !dictionary.ContainsKey(extension.UsrCarton))
                    dictionary.Add(extension.UsrCarton, (object)soPackageDetail);
            }
            return dictionary.Count;
        }

        public static Decimal? GetCurrencyRate(string curyID, DateTime? docDate) => (Decimal?)((CurrencyRate2)PXSelectBase<CurrencyRate2, PXSelect<CurrencyRate2, Where<CurrencyRate2.toCuryID, Equal<Required<CuryRateFilter.toCurrency>>, And<CurrencyRate2.fromCuryID, Equal<Required<CurrencyRate2.fromCuryID>>, And<CurrencyRate2.curyEffDate, LessEqual<Required<CuryRateFilter.effDate>>>>>, OrderBy<Desc<CurrencyRate2.curyEffDate>>>.Config>.Select((PXGraph)PXGraph.CreateInstance<SOShipmentEntry>(), (object)curyID, (object)"SGD", (object)docDate))?.CuryRate;

        public delegate void DelConfirmShipment(SOOrderEntry docgraph, SOShipment shiporder);

        public delegate void DelInvoiceShipment(
          SOInvoiceEntry docgraph,
          SOShipment shiporder,
          DateTime invoiceDate,
          InvoiceList list,
          PXQuickProcess.ActionFlow quickProcessFlow);

        private class LineShipment : IEnumerable<SOShipLine>, IEnumerable, ICollection<SOShipLine>
        {
            private System.Collections.Generic.List<SOShipLine> _List = new System.Collections.Generic.List<SOShipLine>();
            public bool AnyDeleted = false;

            public int Count => this._List.Count;

            public bool IsReadOnly => ((ICollection<SOShipLine>)this._List).IsReadOnly;

            public IEnumerator<SOShipLine> GetEnumerator() => ((IEnumerable<SOShipLine>)this._List).GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => (IEnumerator)((IEnumerable<SOShipLine>)this._List).GetEnumerator();

            public void Clear() => this._List.Clear();

            public bool Contains(SOShipLine item) => this._List.Contains(item);

            public void CopyTo(SOShipLine[] array, int arrayIndex) => this._List.CopyTo(array, arrayIndex);

            public bool Remove(SOShipLine item) => this._List.Remove(item);

            public void Add(SOShipLine item) => this._List.Add(item);
        }

        private class ShipmentSchedule : IComparable<SOShipmentEntry_Extension.ShipmentSchedule>
        {
            private int sortOrder;
            public SOShipLine ShipLine;

            public ShipmentSchedule(
              PXResult<SOShipmentPlan, SOLineSplit, SOLine, PX.Objects.IN.InventoryItem, INLotSerClass, INSite, SOShipLine> result,
              SOShipLine shipLine)
            {
                this.sortOrder = ((SOLine)result).SortOrder.GetValueOrDefault(1000);
                this.Result = result;
                this.ShipLine = shipLine;
            }

            public PXResult<SOShipmentPlan, SOLineSplit, SOLine, PX.Objects.IN.InventoryItem, INLotSerClass, INSite, SOShipLine> Result { get; private set; }

            public int CompareTo(SOShipmentEntry_Extension.ShipmentSchedule other) => this.sortOrder.CompareTo(other.sortOrder);
        }

        private struct DiscKey
        {
            private string discID;
            private string discSeqID;
            private int freeItemID;

            public string DiscID => this.discID;

            public string DiscSeqID => this.discSeqID;

            public int FreeItemID => this.freeItemID;

            public DiscKey(string discID, string discSeqID, int freeItemID)
            {
                this.discID = discID;
                this.discSeqID = discSeqID;
                this.freeItemID = freeItemID;
            }
        }
    }
}
