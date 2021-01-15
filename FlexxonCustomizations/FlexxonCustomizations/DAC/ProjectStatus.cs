// Decompiled with JetBrains decompiler
// Type: FlexxonCustomizations.DAC.ProjectStatus
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using PX.Data;
using PX.Data.BQL;

namespace FlexxonCustomizations.DAC
{
  public class ProjectStatus
  {
    public const string Hold = "HO";
    public const string Activated = "AC";
    public const string Drop = "DP";
    public const string Approved = "AP";
    public const string MP = "MP";
    public static readonly string[] Values = new string[5]
    {
      "HO",
      "AC",
      "DP",
      "AP",
      nameof (MP)
    };
    public static readonly string[] Labels = new string[5]
    {
      "On Hold",
      nameof (Activated),
      nameof (Drop),
      nameof (Approved),
      nameof (MP)
    };

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(ProjectStatus.Values, ProjectStatus.Labels)
      {
      }
    }

    public class hold : BqlType<IBqlString, string>.Constant<ProjectStatus.hold>
    {
      public hold()
        : base("HO")
      {
      }
    }
  }
}
