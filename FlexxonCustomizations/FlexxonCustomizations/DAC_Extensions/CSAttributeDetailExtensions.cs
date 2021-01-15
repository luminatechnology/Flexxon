// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.CS_CSAttributeDetail_ExistingColumn
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using PX.Data;

namespace PX.Objects.CS
{
  [PXNonInstantiatedExtension]
  public class CS_CSAttributeDetail_ExistingColumn : PXCacheExtension<CSAttributeDetail>
  {
    [PXDBLocalizableString(1024, IsUnicode = true)]
    [PXUIField(DisplayName = "Description")]
    public string Description { get; set; }
  }
}
