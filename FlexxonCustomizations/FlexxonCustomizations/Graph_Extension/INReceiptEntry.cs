// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INReceiptEntry_Extension
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.PO;

namespace PX.Objects.IN
{
    public class INReceiptEntry_Extension : PXGraphExtension<INReceiptEntry>
    {
        protected void _(Events.RowSelected<INTranSplit> e)
        {
            INRegister current = this.Base.INRegisterDataMember.Current;
            PXCache cache1 = e.Cache;
            INTranSplit row1 = e.Row;
            bool? hold;
            int num1;
            if (e.Row != null)
            {
                hold = current.Hold;
                bool flag = true;
                num1 = hold.GetValueOrDefault() == flag & hold.HasValue ? 1 : 0;
            }
            else
                num1 = 0;
            PXUIFieldAttribute.SetEnabled<INTranSplitExt.usrCOO>(cache1, (object)row1, num1 != 0);
            PXCache cache2 = e.Cache;
            INTranSplit row2 = e.Row;
            int num2;
            if (e.Row != null)
            {
                hold = current.Hold;
                bool flag = true;
                num2 = hold.GetValueOrDefault() == flag & hold.HasValue ? 1 : 0;
            }
            else
                num2 = 0;
            PXUIFieldAttribute.SetEnabled<INTranSplitExt.usrDateCode>(cache2, (object)row2, num2 != 0);
        }

        protected void _(Events.FieldDefaulting<INTranSplitExt.usrCOO> e)
        {
            if (!(e.Row is INTranSplit row) || !(row.DocType == "R"))
                return;
            e.NewValue = (object)INReceiptEntry_Extension.RetriveFromPOReceipt((PXGraph)this.Base, (object)row.InventoryID, (object)row.LotSerialNbr, (object)row.POLineType, (object)AdditionalInfo.COO);
        }

        protected void _(
          Events.FieldDefaulting<INTranSplitExt.usrDateCode> e)
        {
            if (!(e.Row is INTranSplit row) || !(row.DocType == "R"))
                return;
            e.NewValue = (object)INReceiptEntry_Extension.RetriveFromPOReceipt((PXGraph)this.Base, (object)row.InventoryID, (object)row.LotSerialNbr, (object)row.POLineType, (object)AdditionalInfo.DataCode);
        }

        public static string RetriveFromPOReceipt(PXGraph graph, params object[] iNtranParams)
        {
            int? nullable = new int?((int)iNtranParams[0]);
            string iNtranParam1 = (string)iNtranParams[1];
            string iNtranParam2 = (string)iNtranParams[2];
            string str = string.Empty;
            POReceiptLineSplit receiptLineSplit = SelectFrom<POReceiptLineSplit>.Where<POReceiptLineSplit.inventoryID.IsEqual<P.AsInt>
                                                                                       .And<POReceiptLineSplit.lotSerialNbr.IsEqual<P.AsString>
                                                                                            .And<POReceiptLineSplit.lineType.IsEqual<P.AsString>>>>.View.SelectSingleBound(graph, (object[])null, (object)nullable, (object)iNtranParam1, (object)iNtranParam2);
            if (receiptLineSplit != null && iNtranParams[3] is AdditionalInfo iNtranParam3)
            {
                if (iNtranParam3 != AdditionalInfo.COO)
                {
                    if (iNtranParam3 == AdditionalInfo.DataCode)
                        str = receiptLineSplit.GetExtension<POReceiptLineSplitExt>().UsrDateCode;
                }
                else
                    str = receiptLineSplit.GetExtension<POReceiptLineSplitExt>().UsrCOO;
            }
            return str;
        }
    }
}
