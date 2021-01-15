// Decompiled with JetBrains decompiler
// Type: FlexxonCustomizations.Graph.FLXSetupMaint
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using FlexxonCustomizations.DAC;
using PX.Common;
using PX.Data;
using PX.Data.BQL.Fluent;

namespace FlexxonCustomizations.Graph
{
  public class FLXSetupMaint : PXGraph<FLXSetupMaint>
  {
    public PXSave<FLXSetup> Save;
    public PXCancel<FLXSetup> Cancel;
    public FbqlSelect<SelectFromBase<FLXSetup, TypeArrayOf<IFbqlJoin>.Empty>, FLXSetup>.View SetupRecord;
  }
}
