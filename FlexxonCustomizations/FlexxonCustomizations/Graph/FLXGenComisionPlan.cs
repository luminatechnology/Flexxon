using FlexxonCustomizations.DAC;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.SO;
using System.Collections.Generic;

namespace FlexxonCustomizations.Graph
{
    public class FLXGenComisionPlan : PXGraph<FLXGenComisionPlan>
    {
        #region Features & Selects
        public PXCancel<ARTran> Cancel;
        public PXProcessingJoin<ARTran, InnerJoin<ARInvoice, On<ARInvoice.refNbr, Equal<ARTran.refNbr>, 
                                                                              And<ARInvoice.docType, Equal<ARTran.tranType>>>, 
                                        InnerJoin<SOLine, On<SOLine.orderNbr, Equal<ARTran.sOOrderNbr>, 
                                                             And<SOLine.orderType, Equal<ARTran.sOOrderType>, 
                                                                 And<SOLine.lineNbr, Equal<ARTran.sOOrderLineNbr>>>>, 
                                        LeftJoin<SOOrder, On<SOOrder.orderType, Equal<SOLine.orderType>, 
                                                             And<SOOrder.orderNbr, Equal<SOLine.orderNbr>>>, 
                                        CrossJoin<FLXSetup>>>>, 
                                        Where<ARInvoice.released, Equal<True>, 
                                              And<ARTran.marginPercent, Greater<FLXSetup.miniMgnPctComsn>, 
                                                  And<ARInvoice.curyDocBal, Equal<decimal0>, 
                                                      And<Where<ARTranExt.usrComisionCreated, Equal<False>, 
                                                                Or<ARTranExt.usrComisionCreated, IsNull>>>>>>, 
                                        OrderBy<Asc<ARInvoice.docDate>>> ComisionPlan;

        public SelectFrom<FLXCommissionTran>.View CommisstionTran;
        #endregion

        #region Ctor
        public FLXGenComisionPlan() => this.ComisionPlan.SetProcessDelegate((PXProcessingBase<ARTran>.ProcessListDelegate)(list => FLXGenComisionPlan.CreateProc(list)));
        #endregion

        #region Static Methods
        public static void CreateProc(List<ARTran> list)
        {
            FLXGenComisionPlan instance = PXGraph.CreateInstance<FLXGenComisionPlan>();

            try
            {
                FLXSetup flxSetup = SelectFrom<FLXSetup>.View.Select(instance);

                string format = "{0} Is Missing.", commissionTranID = string.Empty;
                decimal commisionAmt = 0m;

                if (string.IsNullOrEmpty(flxSetup.ComsnTranNumberingID))
                {
                    throw new PXRedirectRequiredException(instance, string.Format(format, "Commission Transaction Numbering ID"));
                }

                for (int index = 0; index < list.Count; ++index)
                {
                    ARTran arTran = list[index];
                    PXResult<SOLine, SOOrder> result = (PXResult<SOLine, SOOrder>)SelectFrom<SOLine>.LeftJoin<SOOrder>.On<SOOrder.orderType.IsEqual<SOLine.orderType>
                                                                                                                             .And<SOOrder.orderNbr.IsEqual<SOLine.orderNbr>>>
                                                                                                       .Where<SOLine.orderType.IsEqual<P.AsString>
                                                                                                              .And<SOLine.orderNbr.IsEqual<P.AsString>
                                                                                                                   .And<SOLine.lineNbr.IsEqual<P.AsInt>>>>.View
                                                                                                       .Select(instance, arTran.SOOrderType, arTran.SOOrderNbr, arTran.SOOrderLineNbr);
                    SOLine    soLine    = (SOLine)result;
                    SOOrder   soOrder   = (SOOrder)result;
                    SOLineExt extension = soLine.GetExtension<SOLineExt>();
                    ARInvoice arInvoice = SelectFrom<ARInvoice>.Where<ARInvoice.docType.IsEqual<P.AsString>
                                                                      .And<ARInvoice.refNbr.IsEqual<P.AsString>>>
                                                               .OrderBy<Asc<ARInvoice.docDate>>.View
                                                               .Select(instance, arTran.TranType, arTran.RefNbr);

                    foreach (FLXProjCommission row in SelectFrom<FLXProjCommission>.InnerJoin<FLXCommissionTable>.On<FLXProjCommission.commissionID.IsEqual<FLXCommissionTable.commissionID>>
                                                                                   .Where<FLXCommissionTable.customerID.IsEqual<P.AsInt>
                                                                                          .And<FLXCommissionTable.endCustomerID.IsEqual<P.AsInt>
                                                                                               .And<FLXCommissionTable.nonStock.IsEqual<P.AsInt>
                                                                                                    .And<FLXProjCommission.effectiveDate.IsLessEqual<P.AsDateTime>
                                                                                                         .And<FLXProjCommission.expirationDate.IsGreaterEqual<P.AsDateTime>>>>>>.View.ReadOnly
                                                                                   .Select(instance, soLine.CustomerID, extension.UsrEndCustomerID, extension.UsrNonStockItem, arInvoice.DocDate, arInvoice.DocDate))
                    {
                        //FLXProjCommission flxProjCommission = (FLXProjCommission)pxResult2;
                        FLXCommissionTran comisionTran = instance.CommisstionTran.Cache.CreateInstance() as FLXCommissionTran;

                        commissionTranID = comisionTran.CommissionTranID = AutoNumberAttribute.GetNextNumber(instance.CommisstionTran.Cache, comisionTran, flxSetup.ComsnTranNumberingID, instance.Accessinfo.BusinessDate);
                        instance.CreateComisionTran(ref comisionTran, arInvoice, arTran, soLine, soOrder, extension, row);

                        comisionTran = instance.CommisstionTran.Insert(comisionTran);

                        //nullable1 = nullable2.HasValue & commisionAmt.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + commisionAmt.GetValueOrDefault()) : new Decimal?();
                        commisionAmt += comisionTran.CommisionAmt.Value;
                    }

                    instance.CommissionCreated(instance, commissionTranID, commisionAmt, arTran.TranType, arTran.RefNbr, arTran.LineNbr);
                }
                instance.Actions.PressSave();
            }
            catch (PXRedirectRequiredException ex)
            {
                PXProcessing.SetError(ex);
            }
            catch (PXException ex)
            {
                PXProcessing.SetError(ex);
                throw;
            }
        }
        #endregion

        #region Methods
        public virtual void CreateComisionTran(ref FLXCommissionTran comisionTran, params object[] objects)
        {
            ARInvoice arInvoice = objects[0] as ARInvoice;
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

        public virtual void CommissionCreated(PXGraph graph, params object[] objects)
        {
            string  commissionTranID = (string)objects[0];
            decimal commissionAmt    = (decimal)objects[1];
            string  tranType         = (string)objects[2];
            string  refNbr           = (string)objects[3];
            int     lineNbr          = (int)objects[4];

            PXUpdate<Set<ARTranExt.usrComisionCreated, Required<ARTranExt.usrComisionCreated>, 
                         Set<ARTranExt.usrCommissionID, Required<FLXCommissionTran.commissionTranID>, 
                             Set<ARTranExt.usrCommissionAmt, Required<ARTranExt.usrCommissionAmt>>>>, 
                     ARTran, 
                     Where<ARTran.tranType, Equal<Required<ARTran.tranType>>, 
                           And<ARTran.refNbr, Equal<Required<ARTran.refNbr>>, 
                               And<ARTran.lineNbr, Equal<Required<ARTran.lineNbr>>>>>>
                     .Update(graph, true, commissionTranID, commissionAmt, tranType, refNbr, lineNbr);
        }
        #endregion
    }
}
