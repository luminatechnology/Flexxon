// Decompiled with JetBrains decompiler
// Type: FlexxonCustomizations.Descriptor.FLXMessages
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using PX.Common;

namespace FlexxonCustomizations.Descriptor
{
  [PXLocalizable("Flexxon")]
  public static class FLXMessages
  {
    public const string UniqueKey = "{0} Already Has The Same Customer, End Customer & Non Stock/MPN, It Isn't Unique.";
    public const string SplitQtyExceed = "Split Qty Cannot Exceeded Order Qty";
    public const string PackageQtyEmpty = "The Shipment Package Can’t Be Empty.";
    public const string TotalQtyNoMatch = "The Shipped Quantity Isn't Equal To Total Package Quantity.";
    public const string DiffCustAndLoca = "Please Only Select The Same Customer ID & Location For One Shipment";
    public const string WrongBranch2Inv = "You're In Wrong Branch To Prepare Invoice.";
    public const string DiffSOBillInfo = "Prepare Invoice For This Shipment Will Create Multiple Invoices, Please Go Sales Invoice To Create Directly.";
  }
}
