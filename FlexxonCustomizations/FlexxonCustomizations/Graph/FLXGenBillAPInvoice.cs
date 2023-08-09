using FlexxonCustomizations.DAC;
using PX.Common;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CR;
using System.Collections;

namespace FlexxonCustomizations.Graph
{
    public class FLXGenBillAPInvoice : PXGraph<FLXGenBillAPInvoice>
    {
        #region Features
        public PXCancel<FLXCommissionTran> Cancel;
        public PXProcessing<FLXCommissionTran, Where<FLXCommissionTran.aPBillCreated, Equal<False>, 
                                                     Or<FLXCommissionTran.aPBillCreated, IsNull>>, 
                                               OrderBy<Asc<FLXCommissionTran.salesRepID>>> ComisionTranProc;
        public PXFilter<SmartPanelParm> Parm;
        public PXSetup<FLXSetup> Setup;
        #endregion

        #region Ctor
        public FLXGenBillAPInvoice()
        {
            FLXSetup setup = this.Setup.Current;

            ComisionTranProc.SetProcessDelegate((PXProcessingBase<FLXCommissionTran>.ProcessListDelegate)(list => FLXGenBillAPInvoice.CreateAPBill(list, setup)));
        }
        #endregion

        #region Actions
        public PXAction<FLXCommissionTran> enterAPDate;
        [PXUIField(DisplayName = "Enter Bill", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select), PXButton]
        public virtual IEnumerable EnterAPDate(PXAdapter adapter) => adapter.Get();

        public PXAction<FLXCommissionTran> updateTranAPDate;
        [PXUIField(DisplayName = "OK", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select), PXLookupButton]
        public virtual IEnumerable UpdateTranAPDate(PXAdapter adapter)
        {
            SmartPanelParm current = this.Parm.Current;

            foreach (FLXCommissionTran flxCommissionTran in this.ComisionTranProc.Cache.Updated)
            {
                bool? selected = flxCommissionTran.Selected;
                bool flag = true;
                if (selected.GetValueOrDefault() == flag & selected.HasValue)
                {
                    flxCommissionTran.APDate = current.APDate;
                    this.ComisionTranProc.Update(flxCommissionTran);
                }
            }
            return adapter.Get();
        }
        #endregion

        #region Event Handlers
        protected virtual void _(Events.RowSelected<FLXCommissionTran> e)
        {
            bool enabled = e.Row != null && e.Row.APDate.HasValue;

            ComisionTranProc.SetProcessEnabled(enabled);
            ComisionTranProc.SetProcessAllEnabled(enabled);
        }
        #endregion

        #region Static Methods
        public static void CreateAPBill(System.Collections.Generic.List<FLXCommissionTran> list, FLXSetup setup)
        {
            try
            {
                APInvoiceEntry invoiceEntry = PXGraph.CreateInstance<APInvoiceEntry>();

                string str = string.Empty, format = "[{0}] Isn't A Vendor.", acctCD = string.Empty;

                for (int index = 0; index < list.Count; ++index)
                {
                    FLXCommissionTran flxCommissionTran = list[index];

                    if (CheckBAcctType(invoiceEntry, flxCommissionTran.SalesRepID, ref acctCD))
                    {
                        if (invoiceEntry.CurrentDocument.Current == null)
                        {
                            APInvoice invoice = invoiceEntry.Document.Cache.CreateInstance() as APInvoice;
                            invoice.DocType    = APDocType.Invoice;
                            invoice.VendorID   = flxCommissionTran.SalesRepID;
                            invoice.DocDate    = flxCommissionTran.APDate;
                            invoice.InvoiceNbr = string.Format("{0} {1}", flxCommissionTran.CommissionTranID, invoiceEntry.vendor.Select(invoice.VendorID).TopFirst.AcctCD.Trim());

                            invoiceEntry.Document.Insert(invoice);
                        }

                        APTran tran = invoiceEntry.Transactions.Cache.CreateInstance() as APTran;

                        tran.BranchID     = flxCommissionTran.BranchID;
                        tran.InventoryID  = setup.CommissionItem;
                        tran.Qty          = 1m;
                        tran.CuryUnitCost = flxCommissionTran.CommisionAmt;
                        tran.TranDesc     = string.Format("{0},{1},{2},{3},{4}", flxCommissionTran.CommissionTranID, flxCommissionTran.OrderNbr, flxCommissionTran.ProjectNbr,
                                                                                 invoiceEntry.nonStockItem.Select(flxCommissionTran.InventoryID).TopFirst.InventoryCD.Trim(),
                                                                                 invoiceEntry.nonStockItem.Select(flxCommissionTran.NonStockItem).TopFirst.InventoryCD.Trim());
                        tran.SubID = ARTran.PK.Find(invoiceEntry, flxCommissionTran.DocType, flxCommissionTran.RefNbr, flxCommissionTran.ARLineNbr).SubID; //invoiceEntry.nonStockItem.Select(tran.InventoryID).TopFirst.COGSSubID;

                        invoiceEntry.Transactions.Insert(tran);

                        str += flxCommissionTran.CommissionTranID + "/";
                    }
                    else
                    {
                        throw new PXException(format, acctCD.Trim());
                    }
                }

                invoiceEntry.Document.Current.DocDesc = str.Substring(0, (str.Length > PX.Objects.Common.Constants.TranDescLength ? PX.Objects.Common.Constants.TranDescLength : str.Length) - 1);
                invoiceEntry.Document.UpdateCurrent();
                invoiceEntry.Save.Press();

                for (int index = 0; index < list.Count; ++index)
                {
                    PXUpdate<Set<FLXCommissionTran.aPBillCreated, Required<FLXCommissionTran.aPBillCreated>, 
                                 Set<FLXCommissionTran.aPBillRefNBr, Required<APInvoice.refNbr>>>, 
                             FLXCommissionTran, 
                             Where<FLXCommissionTran.commissionTranID, Equal<Required<FLXCommissionTran.commissionTranID>>>>
                            .Update(invoiceEntry, true, invoiceEntry.Document.Current.RefNbr, list[index].CommissionTranID);
                }
            }
            catch (PXException ex)
            {
                PXProcessing.SetError(ex);

                throw;
            }
        }

        public static bool CheckBAcctType(PXGraph graph, int? bAcctID, ref string acctCD)
        {
            var baccountR = BAccount.PK.Find(graph, bAcctID.Value);

            acctCD = baccountR.AcctCD;

            return baccountR.Type.IsIn("VE", "VC");
        }
        #endregion
    }
}
