// Decompiled with JetBrains decompiler
// Type: FlexxonCustomizations.Graph.SmartPanelParm
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using PX.Data;
using PX.Data.BQL;
using System;

namespace FlexxonCustomizations.Graph
{
  [Serializable]
  public class SmartPanelParm : IBqlTable
  {
    [PXDBDate]
    [PXUIField(DisplayName = "AP Date")]
    [PXDefault(typeof (AccessInfo.businessDate))]
    public virtual DateTime? APDate { get; set; }

    public abstract class aPDate : BqlType<IBqlInt, int>.Field<SmartPanelParm.aPDate>
    {
    }
  }
}
