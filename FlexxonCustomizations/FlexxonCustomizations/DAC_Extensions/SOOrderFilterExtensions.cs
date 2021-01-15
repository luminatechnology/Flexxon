// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrderFilterExt
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using PX.Data;
using PX.Data.BQL;

namespace PX.Objects.SO
{
  public class SOOrderFilterExt : PXCacheExtension<SOOrderFilter>
  {
    [PX.Objects.GL.Branch(typeof (SOOrder.branchID), null, true, true, true, Enabled = false)]
    [PXDefault(typeof (AccessInfo.branchID), PersistingCheck = PXPersistingCheck.Nothing)]
    public virtual int? UsrBranchID { get; set; }

    public abstract class usrBranchID : BqlType<IBqlInt, int>.Field<SOOrderFilterExt.usrBranchID>
    {
    }
  }
}
