// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.AlternateSelectorAttribute
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using PX.Data;
using PX.Objects.IN;

namespace PX.Objects.PO
{
  public class AlternateSelectorAttribute : PXSelectorAttribute
  {
    public AlternateSelectorAttribute(System.Type inventoryID, System.Type bAccountID, System.Type subItemID)
      : base(BqlCommand.Compose(typeof (Search<,>), typeof (INItemXRef.alternateID), typeof (Where<,,>), typeof (INItemXRef.inventoryID), typeof (Equal<>), typeof (Current<>), inventoryID, typeof (And<,>), typeof (INItemXRef.bAccountID), typeof (Equal<>), typeof (Current<>), bAccountID, typeof (And<>), typeof (INItemXRef.subItemID), typeof (Equal<>), typeof (Current<>), subItemID), typeof (INItemXRef.alternateID), typeof (INItemXRef.alternateType), typeof (INItemXRef.bAccountID))
      => this.DescriptionField = typeof (INItemXRef.descr);
  }
}
