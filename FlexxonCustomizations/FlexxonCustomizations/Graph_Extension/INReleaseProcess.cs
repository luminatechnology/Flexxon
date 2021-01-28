using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.GL;
using PX.Objects.SO;

namespace PX.Objects.IN
{
    public class INReleaseProcess_Extension : PXGraphExtension<INReleaseProcess>
    {
        #region Delegate Function
        public delegate void ReleaseDocProcDelegate(JournalEntry je, INRegister doc);
        [PXOverride]
        public void ReleaseDocProc(JournalEntry je, INRegister doc, INReleaseProcess_Extension.ReleaseDocProcDelegate baseMethod)
        {
            baseMethod(je, doc);

            foreach (INTranSplit row in this.Base.intransplit.Cache.Cached)
            {
                INTranSplit tranSplit = SelectINTranRcpSplit(Base, row.LotSerialNbr, row.InventoryID, row.SubItemID, row.SiteID, row.LocationID);

                if (tranSplit != null && row.DocType != INDocType.Receipt)
                {
                    INTranSplitExt tranSplitExt = tranSplit.GetExtension<INTranSplitExt>();

                    Base.intransplit.Cache.SetValue<INTranSplitExt.usrCOO>(row, tranSplitExt.UsrCOO);
                    Base.intransplit.Cache.SetValue<INTranSplitExt.usrDateCode>(row, tranSplitExt.UsrDateCode);
                }
                else 
                {
                    SOShipLineSplit lineSplit = SelectSOShipLineSplit(Base, row.LotSerialNbr, row.InventoryID, row.SubItemID, row.SiteID, row.LocationID);

                    if (lineSplit == null) { continue; }

                    SOShipLineSplitExt lineSplitExt = lineSplit.GetExtension<SOShipLineSplitExt>();

                    Base.intransplit.Cache.SetValue<INTranSplitExt.usrCOO>(row, lineSplitExt.UsrCOO);
                    Base.intransplit.Cache.SetValue<INTranSplitExt.usrDateCode>(row, lineSplitExt.UsrDateCode);
                }

                Base.Caches[typeof(INTranSplit)].PersistUpdated(this.Base.Caches[typeof(INTranSplit)].Update(row));

                PXTimeStampScope.PutPersisted(this.Base.Caches[typeof(INTranSplit)], row, PXDatabase.SelectTimeStamp());
            }
        }
        #endregion

        #region Static Method
        public static INTranSplit SelectINTranRcpSplit(PXGraph graph, string lotSerialNbr, int? inentoryID, int? subItemID, int? siteID, int? locationID)
        {
            return SelectFrom<INTranSplit>.Where<INTranSplit.lotSerialNbr.IsEqual<P.AsString>
                                                .And<INTranSplit.inventoryID.IsEqual<P.AsInt>
                                                     .And<INTranSplit.subItemID.IsEqual<P.AsInt>
                                                          .And<INTranSplit.siteID.IsEqual<P.AsInt>
                                                               .And<INTranSplit.locationID.IsEqual<P.AsInt>
                                                                    .And<INTranSplit.docType.IsEqual<INDocType.receipt>>>>>>>
                                          .View.SelectSingleBound(graph, null, lotSerialNbr, inentoryID, subItemID, siteID, locationID);
        }

        public static SOShipLineSplit SelectSOShipLineSplit(PXGraph graph, string lotSerialNbr, int? inentoryID, int? subItemID, int? siteID, int? locationID)
        {
            return SelectFrom<SOShipLineSplit>.Where<SOShipLineSplit.lotSerialNbr.IsEqual<P.AsString>
                                                     .And<SOShipLineSplit.inventoryID.IsEqual<P.AsInt>
                                                          .And<SOShipLineSplit.subItemID.IsEqual<P.AsInt>
                                                               .And<SOShipLineSplit.siteID.IsEqual<P.AsInt>
                                                                    .And<SOShipLineSplit.locationID.IsEqual<P.AsInt>>>>>>
                                              .View.SelectSingleBound(graph, null, lotSerialNbr, inentoryID, subItemID, siteID, locationID);
        }
        #endregion
    }
}
