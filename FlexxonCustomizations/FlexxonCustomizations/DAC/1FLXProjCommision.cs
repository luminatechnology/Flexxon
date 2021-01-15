// Decompiled with JetBrains decompiler
// Type: FlexxonCustomizations.DAC.RepType
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using PX.Data;

namespace FlexxonCustomizations.DAC
{
  public class RepType
  {
    public const string MasterRep = "MR";
    public const string SalesRep1 = "S1";
    public const string SalesRep2 = "S2";
    public const string SalesRep3 = "S3";
    public static readonly string[] Values = new string[4]
    {
      "MR",
      "S1",
      "S2",
      "S3"
    };
    public static readonly string[] Labels = new string[4]
    {
      "Master Rep",
      "Sales Rep 1",
      "Sales Rep 2",
      "Sales Rep 3"
    };

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(RepType.Values, RepType.Labels)
      {
      }
    }
  }
}
