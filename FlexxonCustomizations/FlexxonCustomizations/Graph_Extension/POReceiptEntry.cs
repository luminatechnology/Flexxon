// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POReceiptEntry_Extension
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using PX.Data;
using System;

namespace PX.Objects.PO
{
  public class POReceiptEntry_Extension : PXGraphExtension<POReceiptEntry>
  {
    protected void _(Events.FieldSelecting<POReceiptExt.usrTotalCost> e)
    {
      Decimal num = 0M;
      foreach (PXResult<POReceiptLine> pxResult in this.Base.transactions.Select())
      {
        POReceiptLine poReceiptLine = (POReceiptLine) pxResult;
        num += poReceiptLine.TranCost.GetValueOrDefault();
      }
      e.ReturnValue = (object) num;
    }
  }
}
