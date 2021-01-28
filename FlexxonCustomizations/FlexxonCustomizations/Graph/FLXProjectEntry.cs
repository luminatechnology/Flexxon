// Decompiled with JetBrains decompiler
// Type: FlexxonCustomizations.Graph.FLXProjectEntry
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using FlexxonCustomizations.DAC;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;

namespace FlexxonCustomizations.Graph
{
    public class FLXProjectEntry : PXGraph<FLXProjectEntry, FLXProject>
    {
        [PXCopyPasteHiddenFields(new System.Type[] { typeof(FLXProject.customerID), typeof(FLXProject.endCustomerID), typeof(FLXProject.nonStockItem) })]
        public FbqlSelect<SelectFromBase<FLXProject, TypeArrayOf<IFbqlJoin>.Empty>, FLXProject>.View Document;
        public FbqlSelect<SelectFromBase<FLXProject, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FLXProject.projectNbr, IBqlString>.IsEqual<BqlField<FLXProject.projectNbr, IBqlString>.FromCurrent>>, FLXProject>.View CurrentDocument;
        public FbqlSelect<SelectFromBase<FLXProjPurchDetails, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FLXProjPurchDetails.projectNbr, IBqlString>.IsEqual<BqlField<FLXProject.projectNbr, IBqlString>.FromCurrent>>, FLXProjPurchDetails>.View ProjPurchDtls;
        public FbqlSelect<SelectFromBase<FLXProjISOSched, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FLXProjISOSched.projectNbr, IBqlString>.IsEqual<BqlField<FLXProject.projectNbr, IBqlString>.FromCurrent>>, FLXProjISOSched>.View ProjISOSched;
        public CRActivityList<FLXProject> Activity;
        public PXSetup<FLXSetup> Setup;
        public const string Application = "APPLICATIO";
        public const string Branch = "BRANCH";
        public const string Brand = "BRAND";
        public const string CM = "CM";
        public const string Distributor = "DISTRY";
        public const string EAU = "EAU";
        public const string EndCustomer = "ENDCUSTOME";
        public const string Industry = "INDUSTRY";
        public const string ISOSched = "ISOSCHEDUL";
        public const string SalesRep = "SALESREP";
        public const string SalesPer = "SALESPERSO";
        public const string Region = "REGION";
        public const string DesParty = "DESIGNINPA";
        public PXAction<FLXProject> MenuActions;
        public PXAction<FLXProject> CreateSubProj;
        public PXAction<FLXProject> PrintProductApprovalSheetAction;

        public FLXProjectEntry()
        {
            this.MenuActions.AddMenuAction((PXAction)this.CreateSubProj);
            this.MenuActions.AddMenuAction((PXAction)this.PrintProductApprovalSheetAction);
        }

        [PXButton(MenuAutoOpen = true, SpecialType = PXSpecialButtonType.Report)]
        [PXUIField(DisplayName = "Actions")]
        public virtual IEnumerable menuActions(PXAdapter adapter) => adapter.Get();

        [PXButton]
        [PXUIField(DisplayName = "Create Sub Project")]
        public IEnumerable createSubProj(PXAdapter adapter)
        {
            if (this.Document.Current != null)
            {
                this.Save.Press();
                this.Copy(this.Document.Current, this.ProjPurchDtls.Current);
            }
            return adapter.Get();
        }

        #region Material Return Action
        [PXButton]
        [PXUIField(DisplayName = "Print Product Approval Sheet", MapEnableRights = PXCacheRights.Select)]
        protected void printProductApprovalSheetAction()
        {
            var curFLXProjectCache = (FLXProject)this.Caches[typeof(FLXProject)].Current;
            // create the parameter for report
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters["ProjectNbr"] = curFLXProjectCache.ProjectNbr;

            // using Report Required Exception to call the report
            throw new PXReportRequiredException(parameters, "FX606400", "FX606400");
        }
        #endregion

        public override void Persist()
        {
            base.Persist();
            FLXProject current = this.Document.Current;
            int num1;
            if (current != null)
            {
                int? opporLineNbr = current.OpporLineNbr;
                int num2 = 0;
                if (opporLineNbr.GetValueOrDefault() > num2 & opporLineNbr.HasValue && !string.IsNullOrEmpty(current.OpportunityID))
                {
                    num1 = string.IsNullOrEmpty(current.OrigProjNbr) ? 1 : 0;
                    goto label_4;
                }
            }
            num1 = 0;
        label_4:
            if (num1 == 0)
                return;
            PXUpdateJoin<Set<CROpportunityProductsExt.usrProjectNbr, Required<FLXProject.projectNbr>>, CROpportunityProducts, InnerJoin<CROpportunity, On<CROpportunity.defQuoteID, Equal<CROpportunityProducts.quoteID>>>, Where<CROpportunity.opportunityID, Equal<Required<FLXProject.opportunityID>>, And<CROpportunityProducts.lineNbr, Equal<Required<FLXProject.opporLineNbr>>>>>.Update((PXGraph)this, (object)current.ProjectNbr, (object)current.OpportunityID, (object)current.OpporLineNbr);
        }

        protected virtual void _(Events.RowSelected<FLXProject> e)
        {
            string newNumberSymbol = AutoNumberAttribute.GetNewNumberSymbol<FLXProject.projectNbr>(e.Cache, (object)e.Row);
            this.CreateSubProj.SetEnabled(e.Row.ProjectNbr != newNumberSymbol && string.IsNullOrEmpty(e.Row.OrigProjNbr));
        }

        protected virtual void _(Events.RowPersisting<FLXProject> e)
        {
            FLXProject row = e.Row;
            if (!row.CustomerID.HasValue)
                return;
            FLXProject flxProject = SelectFrom<FLXProject>.Where<FLXProject.customerID.IsEqual<P.AsInt>
                                                                .And<FLXProject.endCustomerID.IsEqual<P.AsInt>
                                                                    .And<FLXProject.nonStockItem.IsEqual<P.AsInt>
                                                                         .And<FLXProject.stockItem.IsEqual<P.AsInt>
                                                                             .And<FLXProject.projectNbr.IsNotEqual<P.AsString>>>>>>.View.Select((PXGraph)this, (object)row.CustomerID, (object)row.EndCustomerID, (object)row.NonStockItem, (object)row.StockItem, (object)row.ProjectNbr);
            if (flxProject != null)
                this.Document.Cache.RaiseExceptionHandling<FLXProject.projectNbr>((object)row, (object)row.ProjectNbr, (Exception)new PXSetPropertyException("{0} Already Has The Same Customer, End Customer & Non Stock/MPN, It Isn't Unique.", new object[2]
                {
          (object) flxProject.ProjectNbr,
          (object) PXErrorLevel.Error
                }));
        }

        protected virtual void _(Events.FieldUpdated<FLXProject.endCustomerID> e)
        {
            if (!(e.Row is FLXProject row) || this.ProjISOSched.Current != null)
                return;
            foreach (PXResult<CSAttributeDetail> pxResult in SelectFrom<CSAttributeDetail>.Where<CSAttributeDetail.disabled.IsEqual<False>
                                                                                                 .And<CSAttributeDetail.attributeID.IsEqual<FLXProjectEntry.ISOSchedAtt>>>.View.ReadOnly.Select((PXGraph)this))
            {
                CSAttributeDetail csAttributeDetail = (CSAttributeDetail)pxResult;
                FLXProjISOSched instance = this.ProjISOSched.Cache.CreateInstance() as FLXProjISOSched;
                instance.ProjectNbr = row.ProjectNbr;
                instance.ScheduleCD = csAttributeDetail.ValueID;
                instance.ScheduleDate = this.Accessinfo.BusinessDate;
                instance.Descr = csAttributeDetail.Description;
                this.ProjISOSched.Insert(instance);
            }
        }

        protected virtual void _(Events.FieldUpdated<FLXProject.hold> e) => this.Document.Cache.SetValueExt<FLXProject.status>((object)(e.Row as FLXProject), (bool)e.NewValue ? (object)"HO" : (object)"AC");

        public void Copy(FLXProject project, FLXProjPurchDetails purchDetails)
        {
            using (PXTransactionScope transactionScope = new PXTransactionScope())
            {
                FLXProject copy1 = PXCache<FLXProject>.CreateCopy(project);
                copy1.OrigProjNbr = project.ProjectNbr;
                copy1.NoteID = new Guid?();
                this.Document.Insert(copy1);
                string projectNbr = this.Document.Current.ProjectNbr;
                if (purchDetails != null)
                {
                    FLXProjPurchDetails copy2 = PXCache<FLXProjPurchDetails>.CreateCopy(purchDetails);
                    copy2.ProjectNbr = projectNbr;
                    copy2.NoteID = new Guid?();
                    this.ProjPurchDtls.Insert(copy2);
                }
                foreach (FLXProjISOSched flxProjIsoSched1 in this.ProjISOSched.Cache.Inserted)
                {
                    FLXProjISOSched flxProjIsoSched2 = SelectFrom<FLXProjISOSched>.Where<FLXProjISOSched.projectNbr.IsEqual<P.AsString>
                                                                                         .And<FLXProjISOSched.scheduleCD.IsEqual<P.AsString>>>.View.SelectSingleBound((PXGraph)this, (object[])null, (object)copy1.OrigProjNbr, (object)flxProjIsoSched1.ScheduleCD);
                    flxProjIsoSched1.ScheduleDate = flxProjIsoSched2.ScheduleDate;
                    this.ProjISOSched.Update(flxProjIsoSched1);
                }
                transactionScope.Complete();
            }
            PXRedirectHelper.TryRedirect((PXGraph)this, PXRedirectHelper.WindowMode.Same);
        }

        public class ApplicationAtt : BqlType<IBqlString, string>.Constant<FLXProjectEntry.ApplicationAtt>
        {
            public ApplicationAtt()
              : base("APPLICATIO")
            {
            }
        }

        public class BranchAtt : BqlType<IBqlString, string>.Constant<FLXProjectEntry.BranchAtt>
        {
            public BranchAtt()
              : base("BRANCH")
            {
            }
        }

        public class BrandAtt : BqlType<IBqlString, string>.Constant<FLXProjectEntry.BrandAtt>
        {
            public BrandAtt()
              : base("BRAND")
            {
            }
        }

        public class CmAtt : BqlType<IBqlString, string>.Constant<FLXProjectEntry.CmAtt>
        {
            public CmAtt()
              : base("CM")
            {
            }
        }

        public class DistributorAtt : BqlType<IBqlString, string>.Constant<FLXProjectEntry.DistributorAtt>
        {
            public DistributorAtt()
              : base("DISTRY")
            {
            }
        }

        public class EauAtt : BqlType<IBqlString, string>.Constant<FLXProjectEntry.EauAtt>
        {
            public EauAtt()
              : base("EAU")
            {
            }
        }

        public class EndCustomerAtt : BqlType<IBqlString, string>.Constant<FLXProjectEntry.EndCustomerAtt>
        {
            public EndCustomerAtt()
              : base("ENDCUSTOME")
            {
            }
        }

        public class IndustryAtt : BqlType<IBqlString, string>.Constant<FLXProjectEntry.IndustryAtt>
        {
            public IndustryAtt()
              : base("INDUSTRY")
            {
            }
        }

        public class ISOSchedAtt : BqlType<IBqlString, string>.Constant<FLXProjectEntry.ISOSchedAtt>
        {
            public ISOSchedAtt()
              : base("ISOSCHEDUL")
            {
            }
        }

        public class SalesRepAtt : BqlType<IBqlString, string>.Constant<FLXProjectEntry.SalesRepAtt>
        {
            public SalesRepAtt()
              : base("SALESREP")
            {
            }
        }

        public class SalesPerAtt : BqlType<IBqlString, string>.Constant<FLXProjectEntry.SalesPerAtt>
        {
            public SalesPerAtt()
              : base("SALESPERSO")
            {
            }
        }

        public class RegionAtt : BqlType<IBqlString, string>.Constant<FLXProjectEntry.RegionAtt>
        {
            public RegionAtt()
              : base("REGION")
            {
            }
        }

        public class DesPartyAtt : BqlType<IBqlString, string>.Constant<FLXProjectEntry.DesPartyAtt>
        {
            public DesPartyAtt()
              : base("DESIGNINPA")
            {
            }
        }
    }
}
