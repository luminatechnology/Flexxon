// Decompiled with JetBrains decompiler
// Type: FlexxonCustomizations.Graph.FLXGenBillAPInvoice
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using FlexxonCustomizations.DAC;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.CR;
using System;
using System.Collections;

namespace FlexxonCustomizations.Graph
{
    public class FLXGenBillAPInvoice : PXGraph<FLXGenBillAPInvoice>
    {
        public PXProcessing<FLXCommissionTran, Where<FLXCommissionTran.aPBillCreated, Equal<False>, Or<FLXCommissionTran.aPBillCreated, PX.Data.IsNull>>, OrderBy<Asc<FLXCommissionTran.salesRepID>>> ComisionTranProc;
        public PXFilter<SmartPanelParm> Parm;
        public PXSetup<FLXSetup> Setup;
        public PXAction<FLXCommissionTran> enterAPDate;
        public PXAction<FLXCommissionTran> updateTranAPDate;

        public FLXGenBillAPInvoice()
        {
            FLXSetup setup = this.Setup.Current;
            this.ComisionTranProc.SetProcessDelegate((PXProcessingBase<FLXCommissionTran>.ProcessListDelegate)(list => FLXGenBillAPInvoice.CreateAPBill(list, setup)));
        }

        [PXUIField(DisplayName = "Enter Bill", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
        [PXButton]
        public virtual IEnumerable EnterAPDate(PXAdapter adapter) => adapter.Get();

        [PXUIField(DisplayName = "OK", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
        [PXLookupButton]
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

        protected virtual void _(Events.RowSelected<FLXCommissionTran> e)
        {
            bool enabled = e.Row != null && e.Row.APDate.HasValue;
            this.ComisionTranProc.SetProcessEnabled(enabled);
            this.ComisionTranProc.SetProcessAllEnabled(enabled);
        }

        public static void CreateAPBill(System.Collections.Generic.List<FLXCommissionTran> list, FLXSetup setup)
        {
            try
            {
                APInvoiceEntry instance1 = PXGraph.CreateInstance<APInvoiceEntry>();
                string str = string.Empty;
                string format = "[{0}] Isn't A Vendor.";
                for (int index = 0; index < list.Count; ++index)
                {
                    FLXCommissionTran flxCommissionTran = list[index];
                    if (FLXGenBillAPInvoice.CheckBAType((PXGraph)instance1, flxCommissionTran.SalesRepID))
                    {
                        if (instance1.CurrentDocument.Current == null)
                        {
                            APInvoice instance2 = instance1.Document.Cache.CreateInstance() as APInvoice;
                            instance2.DocType = "INV";
                            instance2.VendorID = flxCommissionTran.SalesRepID;
                            instance2.DocDate = flxCommissionTran.APDate;
                            instance2.InvoiceNbr = string.Format("{0} {1}", (object)flxCommissionTran.CommissionTranID, (object)instance1.vendor.Select((object)instance2.VendorID).TopFirst.AcctCD.Trim());
                            instance1.Document.Insert(instance2);
                        }
                        APTran instance3 = instance1.Transactions.Cache.CreateInstance() as APTran;
                        instance3.BranchID = flxCommissionTran.BranchID;
                        instance3.InventoryID = setup.CommissionItem;
                        instance3.Qty = new Decimal?((Decimal)1);
                        instance3.CuryUnitCost = flxCommissionTran.CommisionAmt;
                        instance3.TranDesc = string.Format("{0},{1},{2},{3},{4}", (object)flxCommissionTran.CommissionTranID, (object)flxCommissionTran.OrderNbr, (object)flxCommissionTran.ProjectNbr, (object)instance1.nonStockItem.Select((object)flxCommissionTran.InventoryID).TopFirst.InventoryCD.Trim(), (object)instance1.nonStockItem.Select((object)flxCommissionTran.NonStockItem).TopFirst.InventoryCD.Trim());
                        instance3.SubID = instance1.nonStockItem.Select((object)instance3.InventoryID).TopFirst.COGSSubID;
                        instance1.Transactions.Insert(instance3);
                        str = str + flxCommissionTran.CommissionTranID + "/";
                    }
                    else
                        throw new PXException(format, new object[1]
                        {
              (object) SelectFrom<BAccountR>.Where<BAccountR.bAccountID.IsEqual<P.AsInt>>.View.Select((PXGraph) instance1, (object) flxCommissionTran.SalesRepID).TopFirst.AcctCD.Trim()
                        });
                }
                instance1.Document.Current.DocDesc = str.Substring(0, str.Length - 1);
                instance1.Document.UpdateCurrent();
                instance1.Save.Press();
                for (int index = 0; index < list.Count; ++index)
                {
                    FLXCommissionTran flxCommissionTran = list[index];
                    PXUpdate<Set<FLXCommissionTran.aPBillCreated, Required<FLXCommissionTran.aPBillCreated>, Set<FLXCommissionTran.aPBillRefNBr, Required<APInvoice.refNbr>>>, FLXCommissionTran, Where<FLXCommissionTran.commissionTranID, Equal<Required<FLXCommissionTran.commissionTranID>>>>.Update((PXGraph)instance1, (object)true, (object)instance1.Document.Current.RefNbr, (object)flxCommissionTran.CommissionTranID);
                }
            }
            catch (PXException ex)
            {
                PXProcessing.SetError((Exception)ex);
                throw ex;
            }
        }

        public static bool CheckBAType(PXGraph graph, int? bAcctID)
        {
            BAccountR baccountR = SelectFrom<BAccountR>.Where<BAccountR.bAccountID.IsEqual<P.AsInt>>.View.ReadOnly.SelectSingleBound(graph, (object[])null, (object)bAcctID);
            return baccountR.Type.Equals("VE") || baccountR.Type.Equals("VC");
        }
    }
}
