// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INTranSplitExt
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;

namespace PX.Objects.IN
{
  public class INTranSplitExt : PXCacheExtension<INTranSplit>
  {
    [PXDBString(2, IsUnicode = true)]
    [PXUIField(DisplayName = "COO")]
    [Country]
    public virtual string UsrCOO { get; set; }

    [PXDBString(6, IsFixed = true, IsUnicode = true)]
    [PXUIField(DisplayName = "Date Code")]
    public virtual string UsrDateCode { get; set; }

    public abstract class usrCOO : BqlType<IBqlString, string>.Field<INTranSplitExt.usrCOO>
    {
    }

    public abstract class usrDateCode : BqlType<IBqlString, string>.Field<INTranSplitExt.usrDateCode>
    {
    }
  }
}
