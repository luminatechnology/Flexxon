// Decompiled with JetBrains decompiler
// Type: FlexxonCustomizations.Graph.FLXGenComisionPlan
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using FlexxonCustomizations.DAC;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.SO;
using System;
using System.Collections.Generic;

namespace FlexxonCustomizations.Graph
{
    public class FLXGenComisionPlan : PXGraph<FLXGenComisionPlan>
    {
        public PXProcessingJoin<ARTran, InnerJoin<PX.Objects.AR.ARInvoice, On<PX.Objects.AR.ARInvoice.refNbr, Equal<ARTran.refNbr>, And<PX.Objects.AR.ARInvoice.docType, Equal<ARTran.tranType>>>, InnerJoin<SOLine, On<SOLine.orderNbr, Equal<ARTran.sOOrderNbr>, And<SOLine.orderType, Equal<ARTran.sOOrderType>, And<SOLine.lineNbr, Equal<ARTran.sOOrderLineNbr>>>>, LeftJoin<SOOrder, On<SOOrder.orderType, Equal<SOLine.orderType>, And<SOOrder.orderNbr, Equal<SOLine.orderNbr>>>, CrossJoin<FLXSetup>>>>, Where<PX.Objects.AR.ARInvoice.released, Equal<True>, And<ARTran.marginPercent, Greater<FLXSetup.miniMgnPctComsn>, And<PX.Objects.AR.ARInvoice.curyDocBal, Equal<decimal0>, And<Where<ARTranExt.usrComisionCreated, Equal<False>, Or<ARTranExt.usrComisionCreated, PX.Data.IsNull>>>>>>, OrderBy<Asc<PX.Objects.AR.ARInvoice.docDate>>> ComisionPlan;
        public FbqlSelect<SelectFromBase<FLXCommissionTran, TypeArrayOf<IFbqlJoin>.Empty>, FLXCommissionTran>.View CommisstionTran;

        public FLXGenComisionPlan() => this.ComisionPlan.SetProcessDelegate((PXProcessingBase<ARTran>.ProcessListDelegate)(list => FLXGenComisionPlan.CreateProc(list)));

        public static void CreateProc(List<ARTran> list)
        {
            FLXGenComisionPlan instance = PXGraph.CreateInstance<FLXGenComisionPlan>();
            try
            {
                FLXSetup flxSetup = SelectFrom<FLXSetup>.View.Select((PXGraph)instance);
                string format = "{0} Is Missing.";
                string str = string.Empty;
                Decimal? nullable1 = new Decimal?(0M);
                if (string.IsNullOrEmpty(flxSetup.ComsnTranNumberingID))
                    throw new PXRedirectRequiredException((PXGraph)instance, string.Format(format, (object)"ComsnTranNumberingID"));
                for (int index = 0; index < list.Count; ++index)
                {
                    ARTran arTran = list[index];
                    PXResult<SOLine, SOOrder> pxResult1 = (PXResult<SOLine, SOOrder>)SelectFrom<SOLine>.LeftJoin<SOOrder>.On<SOOrder.orderType.IsEqual<SOLine.orderType>
                                                                                                                             .And<SOOrder.orderNbr.IsEqual<SOLine.orderNbr>>>
                                                                                                       .Where<SOLine.orderType.IsEqual<P.AsString>
                                                                                                              .And<SOLine.orderNbr.IsEqual<P.AsString>
                                                                                                                   .And<SOLine.lineNbr.IsEqual<P.AsInt>>>>.View.Select((PXGraph)instance, (object)arTran.SOOrderType, (object)arTran.SOOrderNbr, (object)arTran.SOOrderLineNbr);
                    SOLine soLine = (SOLine)pxResult1;
                    SOOrder soOrder = (SOOrder)pxResult1;
                    SOLineExt extension = soLine.GetExtension<SOLineExt>();
                    PX.Objects.AR.ARInvoice arInvoice = SelectFrom<PX.Objects.AR.ARInvoice>.Where<PX.Objects.AR.ARInvoice.docType.IsEqual<P.AsString>
                                                                                                  .And<PX.Objects.AR.ARInvoice.refNbr.IsEqual<P.AsString>>>
                                                                                           .OrderBy<Asc<PX.Objects.AR.ARInvoice.docDate>>.View.Select((PXGraph)instance, (object)arTran.TranType, (object)arTran.RefNbr);

                    foreach (PXResult<FLXProjCommission> pxResult2 in SelectFrom<FLXProjCommission>.InnerJoin<FLXCommissionTable>.On<FLXProjCommission.commissionID.IsEqual<FLXCommissionTable.commissionID>>
                                                                                                   .Where<FLXCommissionTable.customerID.IsEqual<P.AsInt>
                                                                                                          .And<FLXCommissionTable.endCustomerID.IsEqual<P.AsInt>
                                                                                                               .And<FLXCommissionTable.nonStock.IsEqual<P.AsInt>
                                                                                                                    .And<FLXProjCommission.effectiveDate.IsLessEqual<P.AsDateTime>
                                                                                                                         .And<FLXProjCommission.expirationDate.IsGreaterEqual<P.AsDateTime>>>>>>.View.ReadOnly.Select((PXGraph)instance, (object)soLine.CustomerID, (object)extension.UsrEndCustomerID, (object)extension.UsrNonStockItem, (object)arInvoice.DocDate, (object)arInvoice.DocDate))
                    {
                        FLXProjCommission flxProjCommission = (FLXProjCommission)pxResult2;
                        FLXCommissionTran comisionTran = instance.CommisstionTran.Cache.CreateInstance() as FLXCommissionTran;
                        str = comisionTran.CommissionTranID = AutoNumberAttribute.GetNextNumber(instance.CommisstionTran.Cache, (object)comisionTran, flxSetup.ComsnTranNumberingID, instance.Accessinfo.BusinessDate);
                        FLXGenComisionPlan.CreateComisionTran(ref comisionTran, (object)arInvoice, (object)arTran, (object)soLine, (object)soOrder, (object)extension, (object)flxProjCommission);
                        comisionTran = instance.CommisstionTran.Insert(comisionTran);
                        Decimal? nullable2 = nullable1;
                        Decimal? commisionAmt = comisionTran.CommisionAmt;
                        nullable1 = nullable2.HasValue & commisionAmt.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + commisionAmt.GetValueOrDefault()) : new Decimal?();
                    }
                    FLXGenComisionPlan.CommissionCreated((PXGraph)instance, (object)str, (object)nullable1, (object)arTran.TranType, (object)arTran.RefNbr, (object)arTran.LineNbr);
                }
                instance.Actions.PressSave();
            }
            catch (Exception ex)
            {
                PXProcessing.SetError(ex);
                throw;
            }
        }

        public static void CreateComisionTran(
          ref FLXCommissionTran comisionTran,
          params object[] objects)
        {
            PX.Objects.AR.ARInvoice arInvoice = objects[0] as PX.Objects.AR.ARInvoice;
            ARTran arTran = objects[1] as ARTran;
            SOLine soLine = objects[2] as SOLine;
            SOOrder soOrder = objects[3] as SOOrder;
            SOLineExt soLineExt = objects[4] as SOLineExt;
            FLXProjCommission flxProjCommission = objects[5] as FLXProjCommission;
            comisionTran.OrderType = soOrder.OrderType;
            comisionTran.OrderNbr = soOrder.OrderNbr;
            comisionTran.CustomerOrderNbr = soOrder.CustomerOrderNbr;
            comisionTran.SOLineNbr = soLine.LineNbr;
            comisionTran.BranchID = arInvoice.BranchID;
            comisionTran.CustomerID = arInvoice.CustomerID;
            comisionTran.DocDate = arInvoice.DocDate;
            comisionTran.CuryID = arInvoice.CuryID;
            comisionTran.DocType = arInvoice.DocType;
            comisionTran.RefNbr = arInvoice.RefNbr;
            comisionTran.SalesRepID = flxProjCommission.SalesRepID;
            comisionTran.Percentage = flxProjCommission.Percentage;
            comisionTran.ARLineNbr = arTran.LineNbr;
            comisionTran.CuryTranAmt = arTran.CuryTranAmt;
            comisionTran.InventoryID = arTran.InventoryID;
            comisionTran.Qty = arTran.Qty;
            comisionTran.CuryUnitPrice = arTran.CuryUnitPrice;
            comisionTran.EndCustomerID = soLineExt.UsrEndCustomerID;
            comisionTran.NonStockItem = soLineExt.UsrNonStockItem;
            comisionTran.ProjectNbr = soLineExt.UsrProjectNbr;
        }

        public static void CommissionCreated(PXGraph graph, params object[] objects)
        {
            string str1 = (string)objects[0];
            Decimal num1 = (Decimal)objects[1];
            string str2 = (string)objects[2];
            string str3 = (string)objects[3];
            int num2 = (int)objects[4];
            PXUpdate<Set<ARTranExt.usrComisionCreated, Required<ARTranExt.usrComisionCreated>, Set<ARTranExt.usrCommissionID, Required<FLXCommissionTran.commissionTranID>, Set<ARTranExt.usrCommissionAmt, Required<ARTranExt.usrCommissionAmt>>>>, ARTran, Where<ARTran.tranType, Equal<Required<ARTran.tranType>>, And<ARTran.refNbr, Equal<Required<ARTran.refNbr>>, And<ARTran.lineNbr, Equal<Required<ARTran.lineNbr>>>>>>.Update(graph, (object)true, (object)str1, (object)num1, (object)str2, (object)str3, (object)num2);
        }
    }
}
