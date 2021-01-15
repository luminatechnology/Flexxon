// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APInvoiceEntry_Extension
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
using System;

namespace PX.Objects.AP
{
    public class APInvoiceEntry_Extension : PXGraphExtension<APInvoiceEntry>
    {
        [PXOverride]
        public void InvoicePOReceipt(
          POReceipt receipt,
          DocumentList<APInvoice> list,
          bool saveAndAdd,
          bool usePOParemeters,
          bool keepOrderTaxes,
          bool errorIfUnreleasedAPExists,
          APInvoiceEntry_Extension.InvoicePOReceiptDel baseMetod)
        {
            baseMetod(receipt, list, saveAndAdd, usePOParemeters, keepOrderTaxes, errorIfUnreleasedAPExists);
            bool? autoCreateInvoice = receipt.AutoCreateInvoice;
            bool flag = true;
            if (!(autoCreateInvoice.GetValueOrDefault() == flag & autoCreateInvoice.HasValue))
                return;
            DateTime? dueDate;
            TermsAttribute.CalcTermsDates(SelectFrom<Terms>.Where<Terms.termsID.IsEqual<APInvoice.termsID.FromCurrent>>.View.Select((PXGraph)this.Base), receipt.GetExtension<POReceiptExt>().UsrSuppInvDate, out dueDate, out DateTime? _);
            this.Base.CurrentDocument.SetValueExt<APInvoice.dueDate>(this.Base.CurrentDocument.Current, (object)dueDate);
        }

        protected void _(Events.RowDeleted<APInvoice> e, PXRowDeleted invokeBaseHandler)
        {
            if (invokeBaseHandler != null)
                invokeBaseHandler(e.Cache, e.Args);
            PXUpdate<Set<FLXCommissionTran.aPBillCreated, Required<FLXCommissionTran.aPBillCreated>, Set<FLXCommissionTran.aPBillRefNBr, Required<FLXCommissionTran.aPBillRefNBr>>>, FLXCommissionTran, Where<FLXCommissionTran.aPBillRefNBr, Equal<Required<APInvoice.refNbr>>>>.Update((PXGraph)this.Base, (object)false, null, (object)e.Row.RefNbr);
        }

        public delegate void InvoicePOReceiptDel(
          POReceipt receipt,
          DocumentList<APInvoice> list,
          bool saveAndAdd = false,
          bool usePOParemeters = true,
          bool keepOrderTaxes = false,
          bool errorIfUnreleasedAPExists = true);
    }
}
