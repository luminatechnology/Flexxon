// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INKitTranSplitExt
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;

namespace PX.Objects.IN
{
  public class INKitTranSplitExt : PXCacheExtension<INKitTranSplit>
  {
    [PXDBString(2, BqlField = typeof (INTranSplitExt.usrCOO), IsUnicode = true)]
    [PXUIField(DisplayName = "COO")]
    [Country]
    public virtual string UsrCOO { get; set; }

    [PXDBString(6, BqlField = typeof (INTranSplitExt.usrDateCode), IsFixed = true, IsUnicode = true)]
    [PXUIField(DisplayName = "Date Code")]
    public virtual string UsrDateCode { get; set; }

    public abstract class usrCOO : BqlType<IBqlString, string>.Field<INKitTranSplitExt.usrCOO>
    {
    }

    public abstract class usrDateCode : BqlType<IBqlString, string>.Field<INKitTranSplitExt.usrDateCode>
    {
    }
  }
}
