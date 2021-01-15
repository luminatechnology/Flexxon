// Decompiled with JetBrains decompiler
// Type: FlexxonCustomizations.Descriptor.CRCommissionIDByEndCustomerAttribute
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using FlexxonCustomizations.DAC;
using PX.Data;
using System.Collections;

namespace FlexxonCustomizations.Descriptor
{
    public class CRCommissionIDByEndCustomerAttribute : PXCustomSelectorAttribute
    {
        public CRCommissionIDByEndCustomerAttribute()
          : base(typeof(Search<FLXCommissionTable.commissionID>), typeof(FLXCommissionTable.commissionID), typeof(FLXCommissionTable.customerID), typeof(FLXCommissionTable.endCustomerID), typeof(FLXCommissionTable.nonStock))
          => this.DescriptionField = typeof(FLXCommissionTable.descr);

        protected virtual IEnumerable GetRecords()
        {
            int? bAccountID = new int?();
            int? inventoryID = new int?();
            GetAttributeRecords attributeRecords = new GetAttributeRecords(this._Graph, ref bAccountID, ref inventoryID);
            PXGraph graph = this._Graph;
            object[] objArray = new object[2]
            {
        (object) bAccountID,
        (object) inventoryID
            };
            foreach (PXResult<FLXCommissionTable> pxResult in PXSelectBase<FLXCommissionTable, PXSelect<FLXCommissionTable, Where<FLXCommissionTable.endCustomerID, Equal<Required<FLXCommissionTable.endCustomerID>>, And<FLXCommissionTable.nonStock, Equal<Required<FLXCommissionTable.nonStock>>>>>.Config>.Select(graph, objArray))
            {
                FLXCommissionTable row = (FLXCommissionTable)pxResult;
                yield return (object)row;
                row = (FLXCommissionTable)null;
            }
        }
    }
}
