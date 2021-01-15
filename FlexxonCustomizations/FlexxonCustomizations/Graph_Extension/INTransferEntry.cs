// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INTransferEntry_Extension
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using PX.Data;

namespace PX.Objects.IN
{
  public class INTransferEntry_Extension : PXGraphExtension<INTransferEntry>
  {
    protected void _(Events.RowSelected<INRegister> e, PXRowSelected baseHandler)
    {
      if (baseHandler != null)
        baseHandler(e.Cache, e.Args);
      PXUIFieldAttribute.SetEnabled<INRegister.transferType>(e.Cache, (object) e.Row, false);
    }

    protected void _(Events.FieldDefaulting<INRegister.transferType> e) => e.NewValue = (object) "2";
  }
}
