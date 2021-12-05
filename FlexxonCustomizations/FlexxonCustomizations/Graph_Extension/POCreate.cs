// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POCreate_Extension
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using FlexxonCustomizations.DAC;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.SO;
using System;
using System.Collections.Generic;

namespace PX.Objects.PO
{
    public class POCreate_Extension : PXGraphExtension<POCreate>
    {
        #region Delegate Methods
        [PXOverride]
        public virtual IEnumerable<Type> GetFixedDemandFieldScope(Func<IEnumerable<Type>> baseFunc)
        {
            foreach (Type r in baseFunc())
            {
                yield return r;
            }

            yield return typeof(SOLineExt.usrEndCustomerID);
            yield return typeof(SOLineExt.usrProjectNbr);
            yield return typeof(SOLineExt.usrNonStockItem);
            yield return typeof(SOLineExt.usrBrand);
            yield return typeof(SOOrder.orderDate);
            yield return typeof(SOLine.shipDate);
        }
        #endregion

        [PXMergeAttributes(Method = MergeMethod.Merge)]
        [PXUIField(DisplayName = "SO Order Date", Visibility = PXUIVisibility.SelectorVisible)]
        protected void _(Events.CacheAttached<PX.Objects.SO.SOOrder.orderDate> e) { }

        #region Event Handlers
        protected void _(Events.RowSelected<POCreate.POCreateFilter> e)
        {
            POCreate.POCreateFilter filter = this.Base.Filter.Current;
            if (filter == null)
                return;
            this.Base.FixedDemand.SetProcessDelegate((PXProcessingBase<POFixedDemand>.ProcessListDelegate)(list => POCreate_Extension.CreateProc2(list, filter.PurchDate, filter.OrderNbr != null)));
            PXLongRunStatus status = PXLongOperation.GetStatus(this.Base.UID, out TimeSpan _, out Exception _);
            PXUIFieldAttribute.SetVisible<POLine.orderNbr>(this.Base.Caches[typeof(POLine)], (object)null, status == PXLongRunStatus.Completed || status == PXLongRunStatus.Aborted);
            PXUIFieldAttribute.SetVisible<POCreate.POCreateFilter.orderTotal>(e.Cache, (object)null, filter.VendorID.HasValue);
        }

        protected void _(Events.FieldSelecting<POFixedDemandExt.usrOnHand> e)
        {
            if (!(e.Row is POFixedDemand row))
                return;
            e.ReturnValue = (object)POCreate_Extension.GetAllWHQty((PXGraph)this.Base, typeof(Aggregate<Sum<PX.Objects.IN.INSiteStatus.qtyOnHand>>), INQtyType.OnHand, row.InventoryID, row.SubItemID);
        }

        protected void _(Events.FieldSelecting<POFixedDemandExt.usrAvailability> e)
        {
            if (!(e.Row is POFixedDemand row))
                return;
            e.ReturnValue = (object)POCreate_Extension.GetAllWHQty((PXGraph)this.Base, typeof(Aggregate<Sum<PX.Objects.IN.INSiteStatus.qtyAvail>>), INQtyType.Available, row.InventoryID, row.SubItemID);
        }

        protected void _(Events.FieldSelecting<POFixedDemandExt.usrQtyAvailShipping> e)
        {
            if (!(e.Row is POFixedDemand row))
                return;
            e.ReturnValue = (object)POCreate_Extension.GetAllWHQty((PXGraph)this.Base, typeof(Aggregate<Sum<PX.Objects.IN.INSiteStatus.qtyHardAvail>>), INQtyType.AvailShipping, row.InventoryID, row.SubItemID);
        }

        protected void _(Events.FieldSelecting<POFixedDemandExt.usrQtyAvailPlus> e)
        {
            if (!(e.Row is POFixedDemand row))
                return;
            e.ReturnValue = (object)POCreate_Extension.GetAllWHQty((PXGraph)this.Base, typeof(Aggregate<Sum<PX.Objects.IN.INSiteStatus.qtyAvail, Sum<PX.Objects.IN.INSiteStatus.qtyPOFixedOrders, Sum<PX.Objects.IN.INSiteStatus.qtyPOFixedReceipts, Sum<PX.Objects.IN.INSiteStatus.qtySOFixed>>>>>), INQtyType.AvailPlus, row.InventoryID, row.SubItemID);
        }
        #endregion

        #region Static Methods
        public static void CreateProc2(System.Collections.Generic.List<POFixedDemand> list, DateTime? orderDate, bool extSort)
        {
            PXRedirectRequiredException poOrders2 = POCreate_Extension.CreatePOOrders2(list, orderDate, extSort);
            if (poOrders2 != null)
                throw poOrders2;
        }

        public static PXRedirectRequiredException CreatePOOrders2(
          System.Collections.Generic.List<POFixedDemand> list,
          DateTime? PurchDate,
          bool extSort)
        {
            POOrderEntry docgraph = PXGraph.CreateInstance<POOrderEntry>();
            docgraph.Views.Caches.Add(typeof(POOrderEntry.SOLineSplit3));
            POSetup current = docgraph.POSetup.Current;
            DocumentList<POOrder> documentList1 = new DocumentList<POOrder>((PXGraph)docgraph);
            Dictionary<string, DocumentList<POLine>> dictionary = new Dictionary<string, DocumentList<POLine>>();
            list.Sort((Comparison<POFixedDemand>)((a, b) =>
           {
               string empty1 = string.Empty;
               string empty2 = string.Empty;
               string str;
               if (a.PlanType == "90")
               {
                   PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph)docgraph, a.InventoryID);
                   str = string.Format("ZZ.{0}", inventoryItem == null ? (object)string.Empty : (object)inventoryItem.InventoryCD);
               }
               else
               {
                   POOrderEntry.SOLineSplit3 soLineSplit3 = (POOrderEntry.SOLineSplit3)PXSelectBase<POOrderEntry.SOLineSplit3, PXSelect<POOrderEntry.SOLineSplit3, Where<POOrderEntry.SOLineSplit3.planID, Equal<Required<POOrderEntry.SOLineSplit3.planID>>>>.Config>.Select((PXGraph)docgraph, (object)a.PlanID);
                   str = soLineSplit3 == null ? string.Empty : string.Format("{0}.{1}.{2:D7}", (object)soLineSplit3.OrderType, (object)soLineSplit3.OrderNbr, (object)soLineSplit3.SortOrder.GetValueOrDefault());
               }
               string strB;
               if (b.PlanType == "90")
               {
                   PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph)docgraph, b.InventoryID);
                   strB = string.Format("ZZ.{0}", inventoryItem == null ? (object)string.Empty : (object)inventoryItem.InventoryCD);
               }
               else
               {
                   POOrderEntry.SOLineSplit3 soLineSplit3 = (POOrderEntry.SOLineSplit3)PXSelectBase<POOrderEntry.SOLineSplit3, PXSelect<POOrderEntry.SOLineSplit3, Where<POOrderEntry.SOLineSplit3.planID, Equal<Required<POOrderEntry.SOLineSplit3.planID>>>>.Config>.Select((PXGraph)docgraph, (object)b.PlanID);
                   strB = soLineSplit3 == null ? string.Empty : string.Format("{0}.{1}.{2:D7}", (object)soLineSplit3.OrderType, (object)soLineSplit3.OrderNbr, (object)soLineSplit3.SortOrder.GetValueOrDefault());
               }
               return str.CompareTo(strB);
           }));
            POOrder poOrder1 = (POOrder)null;
            bool flag1 = false;
            foreach (POFixedDemand demand in list)
            {
                if (!(demand.FixedSource != "P"))
                {
                    string OrderType = demand.PlanType == "6D" ? "DP" : (demand.PlanType == "6E" ? "DP" : "RO");
                    string str1 = (string)null;
                    int? nullable1 = demand.VendorID;
                    int num1;
                    if (nullable1.HasValue)
                    {
                        nullable1 = demand.VendorLocationID;
                        num1 = !nullable1.HasValue ? 1 : 0;
                    }
                    else
                        num1 = 1;
                    if (num1 != 0)
                    {
                        PXProcessing<POFixedDemand>.SetWarning(list.IndexOf(demand), "Vendor and vendor location should be defined.");
                    }
                    else
                    {
                        PXErrorLevel pxErrorLevel = PXErrorLevel.RowInfo;
                        string empty = string.Empty;
                        try
                        {
                            PX.Objects.SO.SOOrder soOrder = (PX.Objects.SO.SOOrder)PXSelectBase<PX.Objects.SO.SOOrder, PXSelect<PX.Objects.SO.SOOrder, Where<PX.Objects.SO.SOOrder.noteID, Equal<Required<PX.Objects.SO.SOOrder.noteID>>>>.Config>.Select((PXGraph)docgraph, (object)demand.RefNoteID);
                            POOrderEntry.SOLineSplit3 soLineSplit3 = (POOrderEntry.SOLineSplit3)PXSelectBase<POOrderEntry.SOLineSplit3, PXSelect<POOrderEntry.SOLineSplit3, Where<POOrderEntry.SOLineSplit3.planID, Equal<Required<POOrderEntry.SOLineSplit3.planID>>>>.Config>.Select((PXGraph)docgraph, (object)demand.PlanID);
                            string str2 = (string)null;
                            string str3 = (string)null;
                            if (demand.PlanType == "6B" || demand.PlanType == "6E")
                            {
                                str2 = soLineSplit3.POType;
                                str3 = soLineSplit3.PONbr;
                            }
                            System.Collections.Generic.List<FieldLookup> fieldLookupList1 = new System.Collections.Generic.List<FieldLookup>()
              {
                (FieldLookup) new FieldLookup<POOrder.orderType>((object) OrderType),
                (FieldLookup) new FieldLookup<POOrder.vendorID>((object) demand.VendorID),
                (FieldLookup) new FieldLookup<POOrder.vendorLocationID>((object) demand.VendorLocationID),
                (FieldLookup) new FieldLookup<POOrder.bLOrderNbr>((object) str3)
              };
                            if (OrderType == "RO")
                            {
                                bool? projectPerDocument = docgraph.apsetup.Current.RequireSingleProjectPerDocument;
                                bool flag2 = true;
                                if (projectPerDocument.GetValueOrDefault() == flag2 & projectPerDocument.HasValue)
                                {
                                    nullable1 = demand.ProjectID;
                                    int? nullable2 = nullable1.HasValue ? nullable1 : ProjectDefaultAttribute.NonProject();
                                    fieldLookupList1.Add((FieldLookup)new FieldLookup<POOrder.projectID>((object)nullable2));
                                }
                                int num2;
                                if (poOrder1 != null && poOrder1.ShipDestType == "L")
                                {
                                    nullable1 = poOrder1.SiteID;
                                    num2 = !nullable1.HasValue ? 1 : 0;
                                }
                                else
                                    num2 = 0;
                                if (num2 == 0)
                                    fieldLookupList1.Add((FieldLookup)new FieldLookup<POOrder.siteID>((object)demand.POSiteID));
                            }
                            else if (OrderType == "DP")
                            {
                                fieldLookupList1.Add((FieldLookup)new FieldLookup<POOrder.sOOrderType>((object)soLineSplit3.OrderType));
                                fieldLookupList1.Add((FieldLookup)new FieldLookup<POOrder.sOOrderNbr>((object)soLineSplit3.OrderNbr));
                            }
                            else
                            {
                                fieldLookupList1.Add((FieldLookup)new FieldLookup<POOrder.shipToBAccountID>((object)soOrder.CustomerID));
                                fieldLookupList1.Add((FieldLookup)new FieldLookup<POOrder.shipToLocationID>((object)soOrder.CustomerLocationID));
                                fieldLookupList1.Add((FieldLookup)new FieldLookup<POOrder.siteID>((object)demand.POSiteID));
                            }
                            poOrder1 = documentList1.Find(fieldLookupList1.ToArray()) ?? new POOrder();
                            if (poOrder1.OrderNbr == null)
                            {
                                docgraph.Clear();
                                poOrder1.OrderType = OrderType;
                                poOrder1 = PXCache<POOrder>.CreateCopy(docgraph.Document.Insert(poOrder1));
                                poOrder1.VendorID = demand.VendorID;
                                poOrder1.VendorLocationID = demand.VendorLocationID;
                                poOrder1.SiteID = demand.POSiteID;
                                nullable1 = demand.ProjectID;
                                if (nullable1.HasValue)
                                    poOrder1.ProjectID = demand.ProjectID;
                                poOrder1.OrderDate = PurchDate;
                                poOrder1.BLType = str2;
                                poOrder1.BLOrderNbr = str3;
                                if (OrderType == "DP" | extSort)
                                {
                                    poOrder1.SOOrderType = soLineSplit3.OrderType;
                                    poOrder1.SOOrderNbr = soLineSplit3.OrderNbr;
                                }
                                if (!string.IsNullOrEmpty(poOrder1.BLOrderNbr))
                                {
                                    POOrder poOrder2 = (POOrder)PXSelectBase<POOrder, PXSelect<POOrder, Where<POOrder.orderType, Equal<Current<POOrder.bLType>>, And<POOrder.orderNbr, Equal<Current<POOrder.bLOrderNbr>>>>>.Config>.SelectSingleBound((PXGraph)docgraph, new object[1]
                                    {
                    (object) poOrder1
                                    });
                                    if (poOrder2 != null)
                                        poOrder1.VendorRefNbr = poOrder2.VendorRefNbr;
                                }
                                if (OrderType == "DP")
                                {
                                    poOrder1.ShipDestType = "C";
                                    poOrder1.ShipToBAccountID = soOrder.CustomerID;
                                    poOrder1.ShipToLocationID = soOrder.CustomerLocationID;
                                }
                                else if (current.ShipDestType == "S")
                                {
                                    poOrder1.ShipDestType = "S";
                                    poOrder1.SiteID = demand.POSiteID;
                                }
                                if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multicurrency>())
                                    docgraph.currencyinfo.Current.CuryID = (string)null;
                                poOrder1 = docgraph.Document.Update(poOrder1);
                                if (OrderType == "DP")
                                {
                                    SOAddress soAddress = (SOAddress)PXSelectBase<SOAddress, PXSelect<SOAddress, Where<SOAddress.addressID, Equal<Required<PX.Objects.SO.SOOrder.shipAddressID>>>>.Config>.Select((PXGraph)docgraph, (object)soOrder.ShipAddressID);
                                    bool? isDefaultAddress = soAddress.IsDefaultAddress;
                                    bool flag2 = false;
                                    if (isDefaultAddress.GetValueOrDefault() == flag2 & isDefaultAddress.HasValue)
                                        SharedRecordAttribute.CopyRecord<POOrder.shipAddressID>(docgraph.Document.Cache, (object)poOrder1, (object)soAddress, true);
                                    SOContact soContact = (SOContact)PXSelectBase<SOContact, PXSelect<SOContact, Where<SOContact.contactID, Equal<Required<PX.Objects.SO.SOOrder.shipContactID>>>>.Config>.Select((PXGraph)docgraph, (object)soOrder.ShipContactID);
                                    bool? isDefaultContact = soContact.IsDefaultContact;
                                    bool flag3 = false;
                                    if (isDefaultContact.GetValueOrDefault() == flag3 & isDefaultContact.HasValue)
                                        SharedRecordAttribute.CopyRecord<POOrder.shipContactID>(docgraph.Document.Cache, (object)poOrder1, (object)soContact, true);
                                    DateTime? expectedDate = poOrder1.ExpectedDate;
                                    DateTime? requestDate = soOrder.RequestDate;
                                    if (expectedDate.HasValue & requestDate.HasValue && expectedDate.GetValueOrDefault() < requestDate.GetValueOrDefault())
                                    {
                                        poOrder1 = PXCache<POOrder>.CreateCopy(poOrder1);
                                        poOrder1.ExpectedDate = soOrder.RequestDate;
                                        poOrder1 = docgraph.Document.Update(poOrder1);
                                    }
                                }
                            }
                            else if (!docgraph.Document.Cache.ObjectsEqual((object)docgraph.Document.Current, (object)poOrder1))
                                docgraph.Document.Current = (POOrder)docgraph.Document.Search<POOrder.orderNbr>((object)poOrder1.OrderNbr, (object)poOrder1.OrderType);
                            poOrder1.UpdateVendorCost = new bool?(false);
                            POLine poLine1 = (POLine)null;
                            DocumentList<POLine> documentList2;
                            if (!dictionary.TryGetValue(demand.PlanType, out documentList2))
                                documentList2 = dictionary[demand.PlanType] = new DocumentList<POLine>((PXGraph)docgraph);
                            if (OrderType == "RO" && demand.PlanType != "6B")
                            {
                                System.Collections.Generic.List<FieldLookup> fieldLookupList2 = new System.Collections.Generic.List<FieldLookup>();
                                fieldLookupList2.Add((FieldLookup)new FieldLookup<POLine.vendorID>((object)demand.VendorID));
                                fieldLookupList2.Add((FieldLookup)new FieldLookup<POLine.vendorLocationID>((object)demand.VendorLocationID));
                                fieldLookupList2.Add((FieldLookup)new FieldLookup<POLine.siteID>((object)demand.POSiteID));
                                fieldLookupList2.Add((FieldLookup)new FieldLookup<POLine.inventoryID>((object)demand.InventoryID));
                                fieldLookupList2.Add((FieldLookup)new FieldLookup<POLine.subItemID>((object)demand.SubItemID));
                                fieldLookupList2.Add((FieldLookup)new FieldLookup<POLine.requestedDate>((object)(DateTime?)soLineSplit3?.ShipDate));
                                int? nullable2;
                                if (soLineSplit3 == null)
                                {
                                    nullable1 = new int?();
                                    nullable2 = nullable1;
                                }
                                else
                                    nullable2 = soLineSplit3.ProjectID;
                                fieldLookupList2.Add((FieldLookup)new FieldLookup<POLine.projectID>((object)nullable2));
                                int? nullable3;
                                if (soLineSplit3 == null)
                                {
                                    nullable1 = new int?();
                                    nullable3 = nullable1;
                                }
                                else
                                    nullable3 = soLineSplit3.TaskID;
                                fieldLookupList2.Add((FieldLookup)new FieldLookup<POLine.taskID>((object)nullable3));
                                int? nullable4;
                                if (soLineSplit3 == null)
                                {
                                    nullable1 = new int?();
                                    nullable4 = nullable1;
                                }
                                else
                                    nullable4 = soLineSplit3.CostCodeID;
                                fieldLookupList2.Add((FieldLookup)new FieldLookup<POLine.costCodeID>((object)nullable4));
                                int? nullable5;
                                if (soLineSplit3 == null)
                                {
                                    nullable1 = new int?();
                                    nullable5 = nullable1;
                                }
                                else
                                    nullable5 = soLineSplit3.LineNbr;
                                fieldLookupList2.Add((FieldLookup)new FieldLookup<POLine.lineNbr>((object)nullable5));
                                System.Collections.Generic.List<FieldLookup> fieldLookupList3 = fieldLookupList2;
                                bool? nullable6 = current.CopyLineDescrSO;
                                bool flag2 = true;
                                if (nullable6.GetValueOrDefault() == flag2 & nullable6.HasValue && soLineSplit3 != null)
                                {
                                    fieldLookupList3.Add((FieldLookup)new FieldLookup<POLine.tranDesc>((object)soLineSplit3.TranDesc));
                                    poLine1 = documentList2.Find(fieldLookupList3.ToArray());
                                    int num2;
                                    if (poLine1 != null)
                                    {
                                        nullable6 = current.CopyLineNoteSO;
                                        bool flag3 = true;
                                        if (nullable6.GetValueOrDefault() == flag3 & nullable6.HasValue)
                                        {
                                            num2 = PXNoteAttribute.GetNote(docgraph.Caches[typeof(POLine)], (object)poLine1) != null ? 1 : (PXNoteAttribute.GetNote(docgraph.Caches[typeof(POOrderEntry.SOLineSplit3)], (object)soLineSplit3) != null ? 1 : 0);
                                            goto label_67;
                                        }
                                    }
                                    num2 = 0;
                                label_67:
                                    if (num2 != 0)
                                        poLine1 = (POLine)null;
                                }
                                else
                                    poLine1 = documentList2.Find(fieldLookupList3.ToArray());
                            }
                            POLine dest = poLine1 ?? new POLine();
                            POLine copy1;
                            if (dest.OrderNbr == null)
                            {
                                docgraph.FillPOLineFromDemand(dest, demand, OrderType, soLineSplit3);
                                POLine line = docgraph.Transactions.Insert(dest);
                                bool? copyLineNoteSo = current.CopyLineNoteSO;
                                bool flag2 = true;
                                if (copyLineNoteSo.GetValueOrDefault() == flag2 & copyLineNoteSo.HasValue && soLineSplit3 != null)
                                    PXNoteAttribute.SetNote(docgraph.Transactions.Cache, (object)line, PXNoteAttribute.GetNote(docgraph.Caches[typeof(POOrderEntry.SOLineSplit3)], (object)soLineSplit3));
                                if (docgraph.onCopyPOLineFields != null)
                                    docgraph.onCopyPOLineFields(demand, line);
                                copy1 = PXCache<POLine>.CreateCopy(line);
                                documentList2.Add(copy1);
                            }
                            else
                            {
                                copy1 = PXCache<POLine>.CreateCopy((POLine)PXSelectBase<POLine, PXSelect<POLine, Where<POLine.orderType, Equal<Current<POOrder.orderType>>, And<POLine.orderNbr, Equal<Current<POOrder.orderNbr>>, And<POLine.lineNbr, Equal<Current<POLine.lineNbr>>>>>>.Config>.SelectSingleBound((PXGraph)docgraph, new object[1]
                                {
                  (object) dest
                                }));
                                POLine poLine2 = copy1;
                                Decimal? orderQty1 = poLine2.OrderQty;
                                Decimal? orderQty2 = demand.OrderQty;
                                poLine2.OrderQty = orderQty1.HasValue & orderQty2.HasValue ? new Decimal?(orderQty1.GetValueOrDefault() + orderQty2.GetValueOrDefault()) : new Decimal?();
                            }
                            if (demand.PlanType == "6B" || demand.PlanType == "6E")
                            {
                                str1 = demand.PlanType == "6B" ? "66" : "6D";
                                demand.FixedSource = "P";
                                copy1.POType = soLineSplit3.POType;
                                copy1.PONbr = soLineSplit3.PONbr;
                                copy1.POLineNbr = soLineSplit3.POLineNbr;
                                POLine poLine2 = (POLine)PXSelectBase<POLine, PXSelect<POLine, Where<POLine.orderType, Equal<Current<POLine.pOType>>, And<POLine.orderNbr, Equal<Current<POLine.pONbr>>, And<POLine.lineNbr, Equal<Current<POLine.pOLineNbr>>>>>>.Config>.SelectSingleBound((PXGraph)docgraph, new object[1]
                                {
                  (object) copy1
                                });
                                if (poLine2 != null)
                                {
                                    Decimal? nullable2 = demand.PlanQty;
                                    Decimal? baseOpenQty = poLine2.BaseOpenQty;
                                    if (nullable2.GetValueOrDefault() > baseOpenQty.GetValueOrDefault() & (nullable2.HasValue & baseOpenQty.HasValue))
                                    {
                                        POLine poLine3 = copy1;
                                        Decimal? orderQty = poLine3.OrderQty;
                                        nullable2 = demand.OrderQty;
                                        poLine3.OrderQty = orderQty.HasValue & nullable2.HasValue ? new Decimal?(orderQty.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
                                        if (string.Equals(copy1.UOM, poLine2.UOM))
                                        {
                                            POLine poLine4 = copy1;
                                            nullable2 = poLine4.OrderQty;
                                            Decimal? openQty = poLine2.OpenQty;
                                            poLine4.OrderQty = nullable2.HasValue & openQty.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + openQty.GetValueOrDefault()) : new Decimal?();
                                        }
                                        else
                                        {
                                            PXDBQuantityAttribute.CalcBaseQty<POLine.orderQty>(docgraph.Transactions.Cache, (object)copy1);
                                            POLine poLine4 = copy1;
                                            Decimal? baseOrderQty = poLine4.BaseOrderQty;
                                            nullable2 = poLine2.BaseOpenQty;
                                            poLine4.BaseOrderQty = baseOrderQty.HasValue & nullable2.HasValue ? new Decimal?(baseOrderQty.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
                                            PXDBQuantityAttribute.CalcTranQty<POLine.orderQty>(docgraph.Transactions.Cache, (object)copy1);
                                        }
                                        pxErrorLevel = PXErrorLevel.RowWarning;
                                        empty += PXMessages.LocalizeFormatNoPrefixNLA("Order Quantity reduced to Blanket Order: '{0}' Open Qty. for this item", (object)copy1.PONbr);
                                    }
                                    copy1.CuryUnitCost = poLine2.CuryUnitCost;
                                    copy1.UnitCost = poLine2.UnitCost;
                                }
                            }
                            copy1.SiteID = POCreate_Extension.GetWasrehouseByBranch((PXGraph)docgraph, copy1.BranchID);
                            short? vleadTime = docgraph.location.Current.VLeadTime;
                            POLine poLine5 = copy1;
                            DateTime dateTime = soLineSplit3.ShipDate.Value;
                            ref DateTime local = ref dateTime;
                            short? nullable7 = vleadTime;
                            nullable1 = nullable7.HasValue ? new int?((int)nullable7.GetValueOrDefault()) : new int?();
                            int num3 = 0;
                            double num4 = nullable1.GetValueOrDefault() > num3 & nullable1.HasValue ? (double)-vleadTime.Value : -14.0;
                            DateTime? nullable8 = new DateTime?(local.AddDays(num4));
                            poLine5.RequestedDate = nullable8;
                            POLine poLine6 = docgraph.Transactions.Update(copy1);
                            PXCache cach = docgraph.Caches[typeof(INItemPlan)];
                            POCreate_Extension.CreateSplitDemand2(cach, demand);
                            cach.SetStatus((object)demand, PXEntryStatus.Updated);
                            demand.SupplyPlanID = poLine6.PlanID;
                            if (str1 != null)
                            {
                                cach.RaiseRowDeleted((object)demand);
                                demand.PlanType = str1;
                                cach.RaiseRowInserted((object)demand);
                            }
                            if (soLineSplit3 != null)
                            {
                                int num2;
                                if (demand.AlternateID != null)
                                {
                                    nullable1 = demand.InventoryID;
                                    num2 = nullable1.HasValue ? 1 : 0;
                                }
                                else
                                    num2 = 0;
                                if (num2 != 0)
                                {
                                    PXSelectBase<INItemXRef> pxSelectBase = (PXSelectBase<INItemXRef>)new PXSelect<INItemXRef, Where<INItemXRef.inventoryID, Equal<Required<INItemXRef.inventoryID>>, And<INItemXRef.alternateID, Equal<Required<INItemXRef.alternateID>>>>>((PXGraph)docgraph);
                                    INItemXRef inItemXref1 = (INItemXRef)pxSelectBase.Select((object)demand.InventoryID, (object)demand.AlternateID);
                                    if (inItemXref1 != null && inItemXref1.AlternateType == "GLBL")
                                    {
                                        int num5;
                                        if (poLine6.AlternateID != null)
                                        {
                                            nullable1 = poLine6.InventoryID;
                                            num5 = nullable1.HasValue ? 1 : 0;
                                        }
                                        else
                                            num5 = 0;
                                        if (num5 != 0)
                                        {
                                            INItemXRef inItemXref2 = (INItemXRef)pxSelectBase.Select((object)poLine6.InventoryID, (object)poLine6.AlternateID);
                                            if (inItemXref2 != null && inItemXref2.AlternateType == "GLBL")
                                                poLine6.AlternateID = demand.AlternateID;
                                        }
                                        else
                                            poLine6.AlternateID = demand.AlternateID;
                                    }
                                }
                                soLineSplit3.POType = poLine6.OrderType;
                                soLineSplit3.PONbr = poLine6.OrderNbr;
                                soLineSplit3.POLineNbr = poLine6.LineNbr;
                                soLineSplit3.RefNoteID = docgraph.Document.Current.NoteID;
                                PX.Objects.SO.SOLine soLine = SelectFrom<PX.Objects.SO.SOLine>.Where<PX.Objects.SO.SOLine.orderType.IsEqual<P.AsString>
                                                                                                     .And<PX.Objects.SO.SOLine.orderNbr.IsEqual<P.AsString>
                                                                                                          .And<PX.Objects.SO.SOLine.lineNbr.IsEqual<P.AsInt>>>>.View.ReadOnly.Select((PXGraph)docgraph, (object)soLineSplit3.OrderType, (object)soLineSplit3.OrderNbr, (object)soLineSplit3.LineNbr);
                                POLineExt extension1 = poLine6.GetExtension<POLineExt>();
                                SOLineExt extension2 = soLine.GetExtension<SOLineExt>();
                                extension1.UsrEndCustomerID = extension2.UsrEndCustomerID;
                                extension1.UsrNonStockItem = extension2.UsrNonStockItem;
                                extension1.UsrProjectNbr = extension2.UsrProjectNbr;
                                if (!string.IsNullOrEmpty(extension2.UsrProjectNbr))
                                {
                                    FLXProject flxProject = SelectFrom<FLXProject>.Where<FLXProject.projectNbr.IsEqual<P.AsString>>.View.ReadOnly.Select((PXGraph)docgraph, (object)extension2.UsrProjectNbr);
                                    extension1.UsrCust2Factory = flxProject.Cust2Factory;
                                    extension1.UsrFactoryPN = flxProject.FactoryPN;
                                }
                                docgraph.GetExtension<POOrderEntry_Extension>().UpdateSOLine(soLineSplit3, docgraph.Document.Current.VendorID, true);
                                docgraph.FixedDemand.Cache.SetStatus((object)soLineSplit3, PXEntryStatus.Updated);
                            }
                            if (docgraph.Transactions.Cache.IsInsertedUpdatedDeleted)
                            {
                                using (PXTransactionScope transactionScope = new PXTransactionScope())
                                {
                                    docgraph.Save.Press();
                                    if (demand.PlanType == "90")
                                    {
                                        docgraph.Replenihment.Current = (INReplenishmentOrder)docgraph.Replenihment.Search<INReplenishmentOrder.noteID>((object)demand.RefNoteID);
                                        if (docgraph.Replenihment.Current != null)
                                        {
                                            INReplenishmentLine copy2 = PXCache<INReplenishmentLine>.CreateCopy(docgraph.ReplenishmentLines.Insert(new INReplenishmentLine()));
                                            copy2.InventoryID = poLine6.InventoryID;
                                            copy2.SubItemID = poLine6.SubItemID;
                                            copy2.UOM = poLine6.UOM;
                                            copy2.VendorID = poLine6.VendorID;
                                            copy2.VendorLocationID = poLine6.VendorLocationID;
                                            copy2.Qty = poLine6.OrderQty;
                                            copy2.POType = poLine6.OrderType;
                                            copy2.PONbr = docgraph.Document.Current.OrderNbr;
                                            copy2.POLineNbr = poLine6.LineNbr;
                                            copy2.SiteID = demand.POSiteID;
                                            copy2.PlanID = demand.PlanID;
                                            docgraph.ReplenishmentLines.Update(copy2);
                                            docgraph.Caches[typeof(INItemPlan)].Delete((object)demand);
                                            docgraph.Save.Press();
                                        }
                                    }
                                    transactionScope.Complete();
                                }
                                if (pxErrorLevel == PXErrorLevel.RowInfo)
                                    PXProcessing<POFixedDemand>.SetInfo(list.IndexOf(demand), PXMessages.LocalizeFormatNoPrefixNLA("Purchase Order '{0}' created.", (object)docgraph.Document.Current.OrderNbr) + "\r\n" + empty);
                                else
                                    PXProcessing<POFixedDemand>.SetWarning(list.IndexOf(demand), PXMessages.LocalizeFormatNoPrefixNLA("Purchase Order '{0}' created.", (object)docgraph.Document.Current.OrderNbr) + "\r\n" + empty);
                                if (documentList1.Find((object)docgraph.Document.Current) == null)
                                    documentList1.Add(docgraph.Document.Current);
                            }
                        }
                        catch (Exception ex)
                        {
                            PXProcessing<POFixedDemand>.SetError(list.IndexOf(demand), ex);
                            PXTrace.WriteError(ex);
                            flag1 = true;
                        }
                    }
                }
            }
            if (flag1 || documentList1.Count != 1)
                return (PXRedirectRequiredException)null;
            using (new PXTimeStampScope((byte[])null))
            {
                docgraph.Clear();
                docgraph.Document.Current = (POOrder)docgraph.Document.Search<POOrder.orderNbr>((object)documentList1[0].OrderNbr, (object)documentList1[0].OrderType);
                return new PXRedirectRequiredException((PXGraph)docgraph, "Purchase Order");
            }
        }

        private static void CreateSplitDemand2(PXCache cache, POFixedDemand demand)
        {
            Decimal? orderQty1 = demand.OrderQty;
            Decimal? planUnitQty = demand.PlanUnitQty;
            if (orderQty1.GetValueOrDefault() == planUnitQty.GetValueOrDefault() & orderQty1.HasValue == planUnitQty.HasValue)
                return;
            INItemPlan inItemPlan1 = (INItemPlan)PXSelectBase<INItemPlan, PXSelectReadonly<INItemPlan, Where<INItemPlan.planID, Equal<Current<INItemPlan.planID>>>>.Config>.SelectSingleBound(cache.Graph, new object[1]
            {
        (object) demand
            });
            INItemPlan copy = PXCache<INItemPlan>.CreateCopy(inItemPlan1);
            copy.PlanID = new long?();
            INItemPlan inItemPlan2 = copy;
            Decimal? nullable1 = demand.PlanUnitQty;
            Decimal? orderQty2 = demand.OrderQty;
            Decimal? nullable2 = nullable1.HasValue & orderQty2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - orderQty2.GetValueOrDefault()) : new Decimal?();
            inItemPlan2.PlanQty = nullable2;
            Decimal? planQty;
            if (demand.UnitMultDiv == "M")
            {
                INItemPlan inItemPlan3 = copy;
                planQty = inItemPlan3.PlanQty;
                nullable1 = demand.UnitRate;
                inItemPlan3.PlanQty = planQty.HasValue & nullable1.HasValue ? new Decimal?(planQty.GetValueOrDefault() * nullable1.GetValueOrDefault()) : new Decimal?();
            }
            else
            {
                INItemPlan inItemPlan3 = copy;
                nullable1 = inItemPlan3.PlanQty;
                Decimal? unitRate = demand.UnitRate;
                inItemPlan3.PlanQty = nullable1.HasValue & unitRate.HasValue ? new Decimal?(nullable1.GetValueOrDefault() / unitRate.GetValueOrDefault()) : new Decimal?();
            }
            cache.Insert((object)copy);
            cache.RaiseRowDeleted((object)demand);
            POFixedDemand poFixedDemand = demand;
            planQty = inItemPlan1.PlanQty;
            nullable1 = copy.PlanQty;
            Decimal? nullable3 = planQty.HasValue & nullable1.HasValue ? new Decimal?(planQty.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
            poFixedDemand.PlanQty = nullable3;
            cache.RaiseRowInserted((object)demand);
        }

        public static int? GetWasrehouseByBranch(PXGraph graph, int? branchID)
        {
            string str = SelectFrom<PX.Objects.GL.Branch>.Where<PX.Objects.GL.Branch.branchID.IsEqual<P.AsInt>>.View.Select(graph, branchID).TopFirst.BranchCD.Trim().Equals("FLX") ? "XW01" : "SW02";

            return PXSelect<INSite, Where<Data.BQL.RTrim<INSite.siteCD>, Equal<Required<INSite.siteCD>>>>.Select(graph, (object)str).TopFirst.SiteID;
        }

        public static Decimal? GetAllWHQty(
          PXGraph graph,
          System.Type aggregateType,
          INQtyType qtyType,
          params int?[] integers)
        {
            PXView pxView = new PXView(graph, true, BqlCommand.CreateInstance(typeof(Select<PX.Objects.IN.INSiteStatus, Where<PX.Objects.IN.INSiteStatus.inventoryID, Equal<Required<PX.Objects.IN.INSiteStatus.inventoryID>>, And<PX.Objects.IN.INSiteStatus.subItemID, Equal<Required<PX.Objects.IN.INSiteStatus.subItemID>>>>>)));
            PX.Objects.IN.INSiteStatus inSiteStatus = (PX.Objects.IN.INSiteStatus)new PXView(graph, true, pxView.BqlSelect.AggregateNew(aggregateType)).SelectSingle((object)integers[0], (object)integers[1]);
            Decimal? nullable1 = new Decimal?(0M);
            if (inSiteStatus != null)
            {
                switch (qtyType)
                {
                    case INQtyType.OnHand:
                        nullable1 = inSiteStatus.QtyOnHand;
                        break;
                    case INQtyType.Available:
                        nullable1 = inSiteStatus.QtyAvail;
                        break;
                    case INQtyType.AvailShipping:
                        nullable1 = inSiteStatus.QtyHardAvail;
                        break;
                    case INQtyType.AvailPlus:
                        Decimal? qtyAvail = inSiteStatus.QtyAvail;
                        Decimal? nullable2 = inSiteStatus.QtyPOFixedOrders;
                        Decimal? nullable3 = qtyAvail.HasValue & nullable2.HasValue ? new Decimal?(qtyAvail.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
                        Decimal? nullable4 = inSiteStatus.QtyPOFixedReceipts;
                        Decimal? nullable5;
                        if (!(nullable3.HasValue & nullable4.HasValue))
                        {
                            nullable2 = new Decimal?();
                            nullable5 = nullable2;
                        }
                        else
                            nullable5 = new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault());
                        Decimal? nullable6 = nullable5;
                        Decimal? qtySoFixed = inSiteStatus.QtySOFixed;
                        Decimal? nullable7;
                        if (!(nullable6.HasValue & qtySoFixed.HasValue))
                        {
                            nullable4 = new Decimal?();
                            nullable7 = nullable4;
                        }
                        else
                            nullable7 = new Decimal?(nullable6.GetValueOrDefault() - qtySoFixed.GetValueOrDefault());
                        nullable1 = nullable7;
                        break;
                }
            }
            return nullable1;
        }
        #endregion
    }
}
