// Decompiled with JetBrains decompiler
// Type: FlexxonCustomizations.Graph.FLXCommissionEntry
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using FlexxonCustomizations.DAC;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR;
using System;

namespace FlexxonCustomizations.Graph
{
    public class FLXCommissionEntry : PXGraph<FLXCommissionEntry, FLXCommissionTable>
    {
        public FbqlSelect<SelectFromBase<FLXCommissionTable, TypeArrayOf<IFbqlJoin>.Empty>, FLXCommissionTable>.View Document;
        public FbqlSelect<SelectFromBase<FLXProjCommission, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FLXProjCommission.commissionID, IBqlString>.IsEqual<BqlField<FLXCommissionTable.commissionID, IBqlString>.FromCurrent>>, FLXProjCommission>.View ProjComision;
        public PXSetup<FLXSetup> Setup;

        public override void Persist()
        {
            base.Persist();
            FLXCommissionTable current = this.Document.Current;
            int num1;
            if (current != null)
            {
                int? opporLineNbr = current.OpporLineNbr;
                int num2 = 0;
                if (opporLineNbr.GetValueOrDefault() > num2 & opporLineNbr.HasValue)
                {
                    num1 = !string.IsNullOrEmpty(current.OpportunityID) ? 1 : 0;
                    goto label_4;
                }
            }
            num1 = 0;
        label_4:
            if (num1 == 0)
                return;
            PXUpdateJoin<Set<CROpportunityProductsExt.usrCommissionID, Required<FLXCommissionTable.commissionID>>, CROpportunityProducts, InnerJoin<CROpportunity, On<CROpportunity.defQuoteID, Equal<CROpportunityProducts.quoteID>>>, Where<CROpportunity.opportunityID, Equal<Required<FLXCommissionTable.opportunityID>>, And<CROpportunityProducts.lineNbr, Equal<Required<FLXCommissionTable.opporLineNbr>>>>>.Update((PXGraph)this, (object)current.CommissionID, (object)current.OpportunityID, (object)current.OpporLineNbr);
        }

        protected virtual void _(Events.RowPersisting<FLXCommissionTable> e)
        {
            FLXCommissionTable row = e.Row;
            if (!row.CustomerID.HasValue)
                return;
            FLXCommissionTable flxCommissionTable = SelectFrom<FLXCommissionTable>.Where<FLXCommissionTable.customerID.IsEqual<P.AsInt>
                                                                                        .And<FLXCommissionTable.endCustomerID.IsEqual<P.AsInt>
                                                                                            .And<FLXCommissionTable.nonStock.IsEqual<P.AsInt>
                                                                                                .And<FLXCommissionTable.commissionID.IsNotEqual<P.AsString>>>>>.View.Select((PXGraph)this, (object)row.CustomerID, (object)row.EndCustomerID, (object)row.NonStock, (object)row.CommissionID);
            if (flxCommissionTable != null)
                this.Document.Cache.RaiseExceptionHandling<FLXCommissionTable.commissionID>((object)row, (object)row.CommissionID, (Exception)new PXSetPropertyException("{0} Already Has The Same Customer, End Customer & Non Stock/MPN, It Isn't Unique.", new object[2]
                {
          (object) flxCommissionTable.CommissionID,
          (object) PXErrorLevel.Error
                }));
        }
    }
}
