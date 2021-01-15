// Decompiled with JetBrains decompiler
// Type: FlexxonCustomizations.Descriptor.CommissionSelectorAttribute
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using FlexxonCustomizations.DAC;
using PX.Data;

namespace FlexxonCustomizations.Descriptor
{
  public class CommissionSelectorAttribute : PXSelectorAttribute
  {
    public CommissionSelectorAttribute()
      : base(typeof (Search<FLXCommissionTable.commissionID>), typeof (FLXCommissionTable.customerID), typeof (FLXCommissionTable.endCustomerID), typeof (FLXCommissionTable.nonStock))
    {
      this.IsDirty = true;
      this.DescriptionField = typeof (FLXCommissionTable.descr);
    }
  }
}
