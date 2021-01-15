// Decompiled with JetBrains decompiler
// Type: FlexxonCustomizations.Descriptor.ProjectNbrSelectorAttribute
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using FlexxonCustomizations.DAC;
using PX.Data;

namespace FlexxonCustomizations.Descriptor
{
  public class ProjectNbrSelectorAttribute : PXSelectorAttribute
  {
    public ProjectNbrSelectorAttribute()
      : base(typeof (Search<FLXProject.projectNbr>), typeof (FLXProject.projectNbr), typeof (FLXProject.status), typeof (FLXProject.customerID), typeof (FLXProject.endCustomerID), typeof (FLXProject.nonStockItem), typeof (FLXProject.stockItem))
    {
    }

    public ProjectNbrSelectorAttribute(System.Type searchType)
      : base(searchType, typeof (FLXProject.projectNbr), typeof (FLXProject.status), typeof (FLXProject.customerID), typeof (FLXProject.endCustomerID), typeof (FLXProject.nonStockItem), typeof (FLXProject.stockItem))
    {
    }
  }
}
