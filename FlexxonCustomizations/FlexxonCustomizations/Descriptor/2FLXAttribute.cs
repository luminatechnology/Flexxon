// Decompiled with JetBrains decompiler
// Type: FlexxonCustomizations.Descriptor.CRProjNbrByEndCustomerAttribute
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using FlexxonCustomizations.DAC;
using PX.Data;
using System;
using System.Collections;

namespace FlexxonCustomizations.Descriptor
{
    public class CRProjNbrByEndCustomerAttribute : PXCustomSelectorAttribute
    {
        public CRProjNbrByEndCustomerAttribute() : base(typeof(Search<FLXProject.projectNbr>), 
                                                                                           typeof(FLXProject.projectNbr), 
                                                                                           typeof(FLXProject.endCustomerID), 
                                                                                           typeof(FLXProject.nonStockItem), 
                                                                                           typeof(FLXProject.status), 
                                                                                           typeof(FLXProject.customerID))
        {
            this.DescriptionField = typeof(FLXProject.descr);
        }

        protected virtual IEnumerable GetRecords()
        {
            int? bAccountID = new int?();
            int? inventoryID = new int?();
            GetAttributeRecords attributeRecords = new GetAttributeRecords(this._Graph, ref bAccountID, ref inventoryID);
            PXGraph graph = this._Graph;
            object[] objArray = new object[2]
            {
                (object) bAccountID, (object) inventoryID
            };

            foreach (PXResult<FLXProject> pxResult in PXSelect<FLXProject, Where<FLXProject.endCustomerID, Equal<Required<FLXProject.endCustomerID>>, 
                                                                                 And<FLXProject.nonStockItem, Equal<Required<FLXProject.nonStockItem>>>>>.Select(graph, objArray))
            {
                FLXProject row = (FLXProject)pxResult;
                yield return (object)row;
                row = (FLXProject)null;
            }
        }
    }
}
