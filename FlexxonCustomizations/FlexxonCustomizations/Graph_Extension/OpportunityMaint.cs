// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.OpportunityMaint_Extension
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using FlexxonCustomizations.DAC;
using FlexxonCustomizations.Descriptor;
using FlexxonCustomizations.Graph;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.Objects.IN;
using System.Collections;

namespace PX.Objects.CR
{
    public class OpportunityMaint_Extension : PXGraphExtension<OpportunityMaint>
    {
        public FbqlSelect<SelectFromBase<SalesPerson, TypeArrayOf<IFbqlJoin>.Empty>, SalesPerson>.View SalesPersonView;
        public PXAction<CROpportunityProducts> CreateProj;
        public PXAction<CROpportunityProducts> CreateComision;

        [PXButton(CommitChanges = true)]
        [PXUIField(DisplayName = "Create Project")]
        protected virtual IEnumerable createProj(PXAdapter adapter)
        {
            this.Base.Save.Press();
            FLXProjectEntry instance = PXGraph.CreateInstance<FLXProjectEntry>();
            FLXProject project = this.CreateProject(this.Base.Products.Current);
            instance.Document.Insert(project);
            throw new PXPopupRedirectException((PXGraph)instance, this.CreateProj.GetCaption(), true);
        }

        [PXButton(CommitChanges = true)]
        [PXUIField(DisplayName = "Create Commission")]
        protected virtual IEnumerable createComision(PXAdapter adapter)
        {
            this.Base.Save.Press();
            FLXCommissionEntry instance = PXGraph.CreateInstance<FLXCommissionEntry>();
            FLXCommissionTable comisionTable = this.CreateComisionTable(this.Base.Products.Current);
            instance.Document.Insert(comisionTable);
            throw new PXPopupRedirectException((PXGraph)instance, this.CreateComision.GetCaption(), true);
        }

        [PXRemoveBaseAttribute(typeof(PXDefaultAttribute))]
        protected void _(Events.CacheAttached<CROpportunity.subject> e)
        {
        }

        [PXRemoveBaseAttribute(typeof(POSiteAvailAttribute))]
        [SiteAvail2(typeof(CROpportunityProducts.inventoryID), typeof(CROpportunityProducts.subItemID), typeof(CROpportunity.branchID))]
        protected void _(
          Events.CacheAttached<CROpportunityProducts.siteID> e)
        {
        }

        protected void _(Events.RowSelected<CROpportunityProducts> e, PXRowSelected invokeBaseHandler)
        {
            if (invokeBaseHandler != null)
                invokeBaseHandler(e.Cache, e.Args);
            if (e.Row == null)
                return;
            CROpportunityProductsExt extension = e.Row.GetExtension<CROpportunityProductsExt>();
            this.CreateProj.SetEnabled(string.IsNullOrEmpty(extension.UsrProjectNbr));
            this.CreateComision.SetEnabled(string.IsNullOrEmpty(extension.UsrCommissionID));
        }

        public virtual FLXProject CreateProject(CROpportunityProducts opporProd)
        {
            CROpportunity current = this.Base.Opportunity.Current;
            FLXProject flxProject = new FLXProject();
            PXFieldState valueExt1 = this.Base.Opportunity.Cache.GetValueExt((object)current, "AttributeSALESREP") as PXFieldState;
            PXFieldState valueExt2 = this.Base.Opportunity.Cache.GetValueExt((object)current, "AttributeSALESPERSO") as PXFieldState;
            PXFieldState valueExt3 = this.Base.Opportunity.Cache.GetValueExt((object)current, "AttributeINDUSTRY") as PXFieldState;
            PXFieldState valueExt4 = this.Base.Opportunity.Cache.GetValueExt((object)current, "AttributeAPPLICATIO") as PXFieldState;
            PXFieldState valueExt5 = this.Base.Opportunity.Cache.GetValueExt((object)current, "AttributeDESIGNINPA") as PXFieldState;
            PXFieldState valueExt6 = this.Base.Opportunity.Cache.GetValueExt((object)current, "AttributeCM") as PXFieldState;
            PXFieldState valueExt7 = this.Base.Opportunity.Cache.GetValueExt((object)current, "AttributeDISTRY") as PXFieldState;
            PXFieldState valueExt8 = this.Base.Opportunity.Cache.GetValueExt((object)current, "AttributeENDCUSTOME") as PXFieldState;
            if (valueExt1.Value != null)
                flxProject.SalesRepID = new int?((int)PXSelectorAttribute.GetField(this.Base.bAccountBasic.Cache, (object)this.Base.BAccounts, "acctCD", valueExt1.Value, "bAccountID"));
            if (valueExt2.Value != null)
                flxProject.SalespersonID = new int?((int)PXSelectorAttribute.GetField(this.SalesPersonView.Cache, (object)this.SalesPersonView, "salesPersonCD", valueExt2.Value, "SalesPersonID"));
            if (valueExt6.Value != null)
                flxProject.CM = new int?((int)PXSelectorAttribute.GetField(this.Base.bAccountBasic.Cache, (object)this.Base.BAccounts, "acctCD", valueExt6.Value, "bAccountID"));
            if (valueExt7.Value != null)
                flxProject.Distributor = new int?((int)PXSelectorAttribute.GetField(this.Base.bAccountBasic.Cache, (object)this.Base.BAccounts, "acctCD", valueExt7.Value, "bAccountID"));
            flxProject.Industry = valueExt3.Value == null ? (string)null : valueExt3.Value.ToString();
            flxProject.Application = valueExt4.Value == null ? (string)null : valueExt4.Value.ToString();
            flxProject.CountryID = valueExt5.Value == null ? (string)null : OpportunityMaint_Extension.GetAddressCountry((PXGraph)this.Base, (string)valueExt5.Value);
            flxProject.EndCustomerID = new int?((int)PXSelectorAttribute.GetField(this.Base.bAccountBasic.Cache, (object)this.Base.BAccounts, "acctCD", valueExt8.Value, "bAccountID"));
            flxProject.OpportunityID = current.OpportunityID;
            flxProject.CustomerID = current.BAccountID;
            flxProject.OpporLineNbr = opporProd.LineNbr;
            flxProject.EAU = opporProd.Qty;
            flxProject.VendorID = opporProd.VendorID;
            flxProject.Descr = opporProd.Descr;
            switch (OpportunityMaint_Extension.GetItemType((PXGraph)this.Base, opporProd.InventoryID))
            {
                case "F":
                case "M":
                case "A":
                    flxProject.StockItem = opporProd.InventoryID;
                    break;
                default:
                    flxProject.NonStockItem = opporProd.InventoryID;
                    break;
            }
            return flxProject;
        }

        public virtual FLXCommissionTable CreateComisionTable(
          CROpportunityProducts opporProd)
        {
            FLXCommissionTable flxCommissionTable = new FLXCommissionTable();
            CROpportunity current = this.Base.Opportunity.Current;
            PXFieldState valueExt = this.Base.Opportunity.Cache.GetValueExt((object)current, "AttributeENDCUSTOME") as PXFieldState;
            flxCommissionTable.EndCustomerID = new int?((int)PXSelectorAttribute.GetField(this.Base.bAccountBasic.Cache, (object)this.Base.BAccounts, "acctCD", valueExt.Value, "bAccountID"));
            flxCommissionTable.CustomerID = current.BAccountID;
            flxCommissionTable.OpportunityID = current.OpportunityID;
            flxCommissionTable.OpporLineNbr = opporProd.LineNbr;
            flxCommissionTable.NonStock = opporProd.InventoryID;
            return flxCommissionTable;
        }

        public static string GetItemType(PXGraph graph, int? inventoryID) => SelectFrom<InventoryItem>.Where<InventoryItem.inventoryID.IsEqual<P.AsInt>>.View.SelectSingleBound(graph, (object[])null, (object)inventoryID).TopFirst.ItemType;

        public static string GetAddressCountry(PXGraph graph, string attrValue)
        {
            Address address = SelectFrom<Address>.InnerJoin<BAccountR>.On<BAccountR.defAddressID.IsEqual<Address.addressID>
                                                                          .And<BAccountR.bAccountID.IsEqual<Address.bAccountID>>>
                                                 .Where<BAccountR.acctCD.IsEqual<P.AsString>>.View.SelectSingleBound(graph, (object[])null, (object)attrValue);
            return address != null ? address.CountryID : string.Empty;
        }
    }
}
