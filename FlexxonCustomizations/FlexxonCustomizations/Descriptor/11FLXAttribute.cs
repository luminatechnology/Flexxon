// Decompiled with JetBrains decompiler
// Type: FlexxonCustomizations.Descriptor.SiteAvail2Attribute
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR;
using PX.Objects.IN;

namespace FlexxonCustomizations.Descriptor
{
    [PXDBInt]
    [PXUIField(DisplayName = "Warehouse", FieldClass = "INSITE", Visibility = PXUIVisibility.Visible)]
    public class SiteAvail2Attribute : SiteAttribute, IPXFieldDefaultingSubscriber
    {
        protected System.Type _inventoryType;
        protected System.Type _subItemType;
        protected System.Type _branchType;

        public SiteAvail2Attribute(System.Type InventoryType, System.Type SubItemType, System.Type BranchType)
        {
            this._inventoryType = InventoryType;
            this._subItemType = SubItemType;
            this._branchType = BranchType;
            this._Attributes[this._SelAttrIndex] = (PXEventSubscriberAttribute)SiteAvail2Attribute.CreateSelector(BqlCommand.AppendJoin<LeftJoin<PX.Objects.CR.Address, On<INSite.FK.Address>>>(SiteAvail2Attribute.Search), BqlTemplate.OfJoin<LeftJoin<INSiteStatus, On<INSiteStatus.siteID, Equal<INSite.siteID>, And<INSiteStatus.inventoryID, Equal<Optional<SiteAvail2Attribute.InventoryPh>>, And<INSiteStatus.subItemID, Equal<Optional<SiteAvail2Attribute.SubItemPh>>>>>, LeftJoin<INItemStats, On<INItemStats.inventoryID, Equal<Optional<SiteAvail2Attribute.InventoryPh>>, And<INItemStats.siteID, Equal<INSite.siteID>>>>>>.Replace<SiteAvail2Attribute.InventoryPh>(InventoryType).Replace<SiteAvail2Attribute.SubItemPh>(SubItemType).ToType(), new System.Type[4]
            {
        typeof (INSite.siteCD),
        typeof (INSiteStatus.qtyOnHand),
        typeof (INSiteStatus.active),
        typeof (INSite.descr)
            });
        }

        private static PXDimensionSelectorAttribute CreateSelector(
          System.Type searchType,
          System.Type lookupJoin,
          System.Type[] colsType)
        {
            return new PXDimensionSelectorAttribute("INSITE", searchType, lookupJoin, typeof(INSite.siteCD), true, colsType)
            {
                DescriptionField = typeof(INSite.descr)
            };
        }

        private static System.Type Search => typeof(PX.Data.Search<INSite.siteID, Where<INSite.siteID, NotEqual<SiteAttribute.transitSiteID>, And<Match<Current<AccessInfo.userName>>>>>);

        public override void CacheAttached(PXCache sender)
        {
            base.CacheAttached(sender);
            sender.Graph.FieldUpdated.AddHandler(sender.GetItemType(), this._inventoryType.Name, new PXFieldUpdated(this.InventoryID_FieldUpdated));
        }

        public virtual void InventoryID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
        {
            try
            {
                sender.SetDefaultExt(e.Row, this._FieldName);
            }
            catch (PXUnitConversionException ex)
            {
            }
            catch (PXSetPropertyException ex)
            {
                PXUIFieldAttribute.SetError(sender, e.Row, this._FieldName, (string)null);
                sender.SetValue(e.Row, this._FieldOrdinal, (object)null);
            }
        }

        public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
        {
            if (e.Cancel || e.Row == null)
                return;
            object obj = sender.GetValue(e.Row, this._inventoryType.Name);
            if (obj == null)
                return;
            InventoryItem inventoryItem = InventoryItem.PK.Find(sender.Graph, new int?((int)obj));
            if (!inventoryItem.DfltSiteID.HasValue)
            {
                object branchId = sender.GetValue(e.Row, this._branchType.Name);
                if (branchId == null)
                {
                    CROpportunityProducts current = (CROpportunityProducts)sender.Current;
                    if (current != null)
                        branchId = (object)SelectFrom<CROpportunity>.Where<CROpportunity.quoteNoteID.IsEqual<P.AsGuid>>.View.SelectSingleBound(sender.Graph, null, (object)current.QuoteID).TopFirst.BranchID;
                }
                PX.Objects.CR.Location location = (PX.Objects.CR.Location)SelectFrom<PX.Objects.CR.Location>.InnerJoin<PX.Objects.GL.Branch>.On<PX.Objects.GL.Branch.bAccountID.IsEqual<PX.Objects.CR.Location.bAccountID>>
                                                                                                             .Where<PX.Objects.GL.Branch.branchID.IsEqual<P.AsInt>>.View.ReadOnly.Select(sender.Graph, branchId);
                if (location == null)
                    return;
                e.NewValue = (object)location.CSiteID;
            }
            else
            {
                INSite inSite = (INSite)PXSelectBase<INSite, PXSelectReadonly<INSite, Where<INSite.siteID, Equal<Required<INSite.siteID>>, And<Match<INSite, Current<AccessInfo.userName>>>>>.Config>.Select(sender.Graph, (object)inventoryItem.DfltSiteID);
                if (inSite == null)
                    return;
                e.NewValue = (object)inSite.SiteID;
            }
        }

        [PXHidden]
        private class InventoryPh : BqlPlaceholderBase
        {
        }

        [PXHidden]
        private class SubItemPh : BqlPlaceholderBase
        {
        }
    }
}
