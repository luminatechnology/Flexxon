using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.SO;
using System;

namespace FlexxonCustomizations.Descriptor
{
    public class EndCustomerSelectorAttribute : PXSelectorAttribute, IPXFieldDefaultingSubscriber
    {
        protected Type _inventoryType;

        public EndCustomerSelectorAttribute() : base(typeof(Search<BAccountR.bAccountID, Where<BAccountR.type, NotEqual<BAccountType.vendorType>>>), 
                                                     typeof(BAccountR.acctCD), 
                                                     typeof(BAccountR.status), 
                                                     typeof(BAccountR.type), 
                                                     typeof(PX.Objects.CR.BAccount.classID))
        {
            this.IsDirty = true;
            this.SubstituteKey = typeof(BAccountR.acctCD);
        }

        public EndCustomerSelectorAttribute(Type inventory) : base(typeof(Search<BAccountR.bAccountID, Where<BAccountR.type, NotEqual<BAccountType.vendorType>>>), 
                                                                          typeof(BAccountR.acctCD), 
                                                                          typeof(BAccountR.status), 
                                                                          typeof(BAccountR.type), 
                                                                          typeof(PX.Objects.CR.BAccount.classID))
        {
            this._inventoryType = inventory;
            this.SubstituteKey = typeof(BAccountR.acctCD);
        }

        public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
        {
            if (e.Cancel || e.Row == null || this._inventoryType == (Type)null) { return; }
            
            object obj = sender.GetValue(e.Row, this._inventoryType.Name);

            if (obj == null) { return; }

            string attributeID = "BUSINESSUN";
            string attrValue = "Distribution";

            InventoryItem inventoryItem = InventoryItem.PK.Find(sender.Graph, new int?((int)obj));
            CSAnswers csAnswers = CSAnswers.PK.Find(sender.Graph, inventoryItem.NoteID, attributeID);

            if (csAnswers != null)
            {
                SOLine row = e.Row as SOLine;

                if (SelectFrom<CSAttributeDetail>.Where<CSAttributeDetail.attributeID.IsEqual<P.AsString>
                                                        .And<CSAttributeDetail.valueID.IsEqual<P.AsString>>>.View.SelectSingleBound(sender.Graph, null, csAnswers.AttributeID, csAnswers.Value).TopFirst.Description == attrValue && 
                    row != null)
                {
                    e.NewValue = row.CustomerID;
                }
            }
        }
    }
}
