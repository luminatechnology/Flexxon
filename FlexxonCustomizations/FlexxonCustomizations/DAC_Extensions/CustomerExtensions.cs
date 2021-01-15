// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CustomerExt
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using PX.Data;
using PX.Data.BQL;

namespace PX.Objects.AR
{
  public class CustomerExt : PXCacheExtension<Customer>
  {
    [PXString(30, InputMask = "", IsUnicode = true)]
    [PXDefault(typeof (Search<PX.Objects.GL.Branch.branchCD, Where<PX.Objects.GL.Branch.branchID, Equal<Current<AccessInfo.branchID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
    public virtual string UsrBranchCD { get; set; }

    public abstract class usrBranchCD : BqlType<IBqlString, string>.Field<CustomerExt.usrBranchCD>
    {
    }
  }
}
