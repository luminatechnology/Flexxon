﻿using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FlexxonCustomizations.DAC;
using FlexxonCustomizations.Descriptor;

namespace PX.Objects.SO
{
    public class SOOrderEntry_Extension : PXGraphExtension<SOOrderEntry>
    {
        public PXFilter<SOLineSplitFilter> LineSplitFilter;

        #region Action
        public PXAction<SOOrder> splitSOLine;
        [PXLookupButton(CommitChanges = true)]
        [PXUIField(DisplayName = "Split SO Line", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
        protected virtual IEnumerable SplitSOLine(PXAdapter adapter)
        {
            if (this.LineSplitFilter.AskExt() == WebDialogResult.OK)
            {
                this.CopySOLine();
            }

            return adapter.Get();
        }
        #endregion

        #region Cache Attached
        [PXMergeAttributes(Method = MergeMethod.Append)]
        [PXFormula(typeof(Default<SOLineExt.usrProjectNbr>))]
        protected void _(Events.CacheAttached<SOLine.inventoryID> e) { }

        [PXMergeAttributes(Method = MergeMethod.Replace)]
        [SOAllocationLotSerialNbr2(typeof(SOLineSplit.inventoryID), 
                                   typeof(SOLineSplit.subItemID), 
                                   typeof(SOLineSplit.siteID), 
                                   typeof(SOLineSplit.locationID), 
                                   FieldClass = "LotSerial")]
        protected void _(Events.CacheAttached<SOLineSplit.lotSerialNbr> e) { }
        #endregion

        #region Event Handlers
        protected void _(Events.RowSelected<SOLine> e, PXRowSelected invokeBaseHandler)
        {
            if (invokeBaseHandler != null)
                invokeBaseHandler(e.Cache, e.Args);
            this.splitSOLine.SetEnabled(e.Row != null);
            if (this.Base.CurrentDocument.Current == null || e.Row == null)
                return;
            bool isEnabled = this.Base.CurrentDocument.Current.Status.IsIn<string>("H", "N");
            this.Base.Transactions.Cache.AllowUpdate = isEnabled;
            PXUIFieldAttribute.SetEnabled<SOLine.shipDate>(this.Base.Transactions.Cache, (object)e.Row, isEnabled);
        }

        protected void _(Events.RowPersisted<SOLine> e)
        {
            using (PXTransactionScope transactionScope = new PXTransactionScope())
            {
                if ((e.Operation & PXDBOperation.Delete) == PXDBOperation.Update || (e.Operation & PXDBOperation.Delete) == PXDBOperation.Insert)
                {
                    SOLine row = e.Row;
                    PXDatabase.Update<SOLine>((PXDataFieldParam)new PXDataFieldAssign<SOLineExt.usrCombineNbr>((object)string.Format("{0}-{1}", (object)row.OrderNbr, (object)row.LineNbr)), (PXDataFieldParam)new PXDataFieldRestrict<SOLine.orderNbr>(PXDbType.NVarChar, (object)row.OrderNbr), (PXDataFieldParam)new PXDataFieldRestrict<SOLine.lineNbr>(PXDbType.Int, (object)row.LineNbr));
                    this.Base.SelectTimeStamp();
                }
                transactionScope.Complete();
            }
        }

        protected void _(Events.RowInserted<SOLine> e)
        {
            foreach (InvoiceSplits invoiceSplits in this.Base.invoicesplits.Cache.Inserted)
            {
                SOLineExt extension1 = ((SOLine)SelectFrom<SOLine>.Where<SOLine.orderType.IsEqual<P.AsString>
                                                                         .And<SOLine.orderNbr.IsEqual<P.AsString>
                                                                              .And<SOLine.lineNbr.IsEqual<P.AsInt>>>>
                                                                  .View.Select(Base, invoiceSplits.OrderTypeSOLine, invoiceSplits.OrderNbrSOLine, invoiceSplits.LineNbrSOLine)).GetExtension<SOLineExt>();
                SOLineExt extension2 = e.Row.GetExtension<SOLineExt>();

                extension2.UsrNonStockItem  = extension1.UsrNonStockItem;
                extension2.UsrEndCustomerID = extension1.UsrEndCustomerID;
                extension2.UsrProjectNbr    = extension1.UsrProjectNbr;
                extension2.UsrCustLineNbr   = extension1.UsrCustLineNbr;
            }
            foreach (SOLine soLine in this.Base.Transactions.Cache.Updated)
            {
                if (soLine.Operation == SOOperation.Receipt)// && soLine.InvoiceNbr != null)
                {
                    SOLineExt extension1 = soLine.GetExtension<SOLineExt>();
                    SOLineExt extension2 = e.Row.GetExtension<SOLineExt>();
                     
                    extension2.UsrNonStockItem  = extension1.UsrNonStockItem;
                    extension2.UsrEndCustomerID = extension1.UsrEndCustomerID;
                    extension2.UsrProjectNbr    = extension1.UsrProjectNbr;
                    extension2.UsrCustLineNbr   = extension1.UsrCustLineNbr;
                }
            }
        }

        protected void _(Events.FieldUpdated<SOLine.inventoryID> e, PXFieldUpdated baseHandler)
        {
            baseHandler?.Invoke(e.Cache, e.Args);

            e.Cache.SetValueExt<SOLine.tranDesc>(e.Row, InventoryItem.PK.Find(Base, ((SOLine)e.Row).GetExtension<SOLineExt>().UsrNonStockItem)?.Descr);
        }

        protected void _(Events.FieldDefaulting<SOLine.inventoryID> e)
        {
            var row = e.Row as SOLine;

            if (row == null) { return; }

            SOLineExt extension = row.GetExtension<SOLineExt>();

            if (!string.IsNullOrEmpty(extension.UsrProjectNbr))
            {
                e.NewValue = SelectFrom<FLXProject>.Where<FLXProject.projectNbr.IsEqual<P.AsString>>.View.Select(Base, extension.UsrProjectNbr).TopFirst.StockItem;
            }
        }

        protected void _(Events.FieldDefaulting<SOLineExt.usrProjectNbr> e)
        {
            var row = e.Row as SOLine;

            if (row == null) { return; }

            SOLineExt extension = row.GetExtension<SOLineExt>();

            List<FLXProject> list = SelectFrom<FLXProject>.Where<FLXProject.status.IsNotEqual<ProjectStatus.hold>
                                                                .And<FLXProject.customerID.IsEqual<P.AsInt>
                                                                     .And<FLXProject.endCustomerID.IsEqual<P.AsInt>
                                                                          .And<FLXProject.nonStockItem.IsEqual<P.AsInt>>>>>
                                                           .View.Select(Base, row.CustomerID, extension.UsrEndCustomerID, extension.UsrNonStockItem).RowCast<FLXProject>().ToList<FLXProject>();
           
            e.NewValue = list.Count == 1 ? list[0].ProjectNbr : null;
        }

        protected void _(Events.FieldUpdated<SOLineExt.usrNonStockItem> e)
        {
            if (!SelectFrom<INItemClass>.InnerJoin<InventoryItem>.On<INItemClass.itemClassID.IsEqual<InventoryItem.itemClassID>>
                                        .Where<InventoryItem.inventoryID.IsEqual<P.AsInt>>.View
                                        .Select((PXGraph)this.Base, e.NewValue).TopFirst.ItemClassCD.Trim().Equals("MPN"))
            {
                e.Cache.SetValueExt<SOLine.inventoryID>(e.Row, e.NewValue);
            }
        }

        protected void _(Events.FieldVerifying<SOLineSplitFilter.splitQty> e)
        {
            Decimal newValue = (Decimal)e.NewValue;
            Decimal? orderQty = this.Base.Transactions.Current.OrderQty;
            Decimal valueOrDefault = orderQty.GetValueOrDefault();
            if (!(newValue > valueOrDefault & orderQty.HasValue))
                return;
            this.LineSplitFilter.Cache.RaiseExceptionHandling<SOLineSplitFilter.splitQty>(e.Row, e.NewValue, (Exception)new PXSetPropertyException("Split Qty Cannot Exceeded Order Qty"));
        }
        #endregion

        #region Methods
        public virtual void CopySOLine()
        {
            SOLineSplitFilter current1 = this.LineSplitFilter.Current;
            SOLine current2 = this.Base.Transactions.Current;
            SOLine copy = this.Base.Transactions.Cache.CreateCopy((object)current2) as SOLine;
            copy.LineNbr = copy.SortOrder = (int?)PXLineNbrAttribute.NewLineNbr<SOLine.lineNbr>(this.Base.Transactions.Cache, (object)this.Base.Document.Current);
            SOLine soLine1 = copy;
            DateTime? shipDate = current1.ShipDate;
            DateTime? nullable1 = shipDate.HasValue ? shipDate : current2.ShipDate;
            soLine1.ShipDate = nullable1;
            SOLine soLine2 = copy;
            bool? poCreate = current2.POCreate;
            bool flag = true;
            bool? nullable2 = poCreate.GetValueOrDefault() == flag & poCreate.HasValue ? new bool?(!this.SOSplitHasPONbr(current2)) : current2.POCreate;
            soLine2.POCreate = nullable2;
            copy.NoteID = new Guid?(Guid.NewGuid());
            copy.GetExtension<SOLineExt>().UsrOrigQty = current1.SplitQty;
            this.Base.Transactions.Insert(copy);
            copy.OrderQty = current1.SplitQty;
            this.Base.Transactions.Update(copy);
            if (!current2.GetExtension<SOLineExt>().UsrOrigQty.HasValue)
                current2.GetExtension<SOLineExt>().UsrOrigQty = current2.OrderQty;
            SOLine soLine3 = current2;
            Decimal? orderQty = soLine3.OrderQty;
            Decimal? qty = copy.Qty;
            soLine3.OrderQty = orderQty.HasValue & qty.HasValue ? new Decimal?(orderQty.GetValueOrDefault() - qty.GetValueOrDefault()) : new Decimal?();
            this.Base.Transactions.Update(current2);
        }

        public virtual bool SOSplitHasPONbr(SOLine origLine) => SelectFrom<SOLineSplit>.Where<SOLineSplit.orderType.IsEqual<P.AsString>
                                                                                              .And<SOLineSplit.orderNbr.IsEqual<P.AsString>
                                                                                                   .And<SOLineSplit.lineNbr.IsEqual<P.AsInt>
                                                                                                        .And<SOLineSplit.pONbr.IsNotNull>>>>.View
                                                                                       .SelectSingleBound(Base, null, origLine.OrderType, origLine.OrderNbr, origLine.LineNbr).Count > 0;
        #endregion
    }
}
