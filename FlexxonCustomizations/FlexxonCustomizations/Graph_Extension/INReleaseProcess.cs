// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INReleaseProcess_Extension
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.GL;

namespace PX.Objects.IN
{
    public class INReleaseProcess_Extension : PXGraphExtension<INReleaseProcess>
    {
        [PXOverride]
        public void ReleaseDocProc(
          JournalEntry je,
          INRegister doc,
          INReleaseProcess_Extension.ReleaseDocProcDelegate baseMethod)
        {
            baseMethod(je, doc);
            foreach (INTranSplit inTranSplit1 in this.Base.intransplit.Cache.Cached)
            {
                INTranSplit inTranSplit2 = INReleaseProcess_Extension.SelectINTranRcpSplit((PXGraph)this.Base, inTranSplit1.LotSerialNbr, inTranSplit1.InventoryID, inTranSplit1.SubItemID, inTranSplit1.SiteID, inTranSplit1.LocationID);
                if (inTranSplit2 != null && inTranSplit1.DocType != "RCP")
                {
                    INTranSplitExt extension = inTranSplit2.GetExtension<INTranSplitExt>();
                    this.Base.intransplit.Cache.SetValue<INTranSplitExt.usrCOO>((object)inTranSplit1, (object)extension.UsrCOO);
                    this.Base.intransplit.Cache.SetValue<INTranSplitExt.usrDateCode>((object)inTranSplit1, (object)extension.UsrDateCode);
                    this.Base.Caches[typeof(INTranSplit)].PersistUpdated(this.Base.Caches[typeof(INTranSplit)].Update((object)inTranSplit1));
                    PXTimeStampScope.PutPersisted(this.Base.Caches[typeof(INTranSplit)], (object)inTranSplit1, (object)PXDatabase.SelectTimeStamp());
                }
            }
        }

        public static INTranSplit SelectINTranRcpSplit(
          PXGraph graph,
          string lotSerialNbr,
          int? inentoryID,
          int? subItemID,
          int? siteID,
          int? locationID)
        {
            return SelectFrom<INTranSplit>.Where<INTranSplit.lotSerialNbr.IsEqual<P.AsString>
                                                .And<INTranSplit.inventoryID.IsEqual<P.AsInt>
                                                     .And<INTranSplit.subItemID.IsEqual<P.AsInt>
                                                          .And<INTranSplit.siteID.IsEqual<P.AsInt>
                                                               .And<INTranSplit.locationID.IsEqual<P.AsInt>
                                                                    .And<INTranSplit.docType.IsEqual<INDocType.receipt>>>>>>>.View.SelectSingleBound(graph, (object[])null, (object)lotSerialNbr, (object)inentoryID, (object)subItemID, (object)siteID, (object)locationID);
        }

        public delegate void ReleaseDocProcDelegate(JournalEntry je, INRegister doc);
    }
}
