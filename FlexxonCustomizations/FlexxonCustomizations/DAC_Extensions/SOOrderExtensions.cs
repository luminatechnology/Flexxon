// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrderExt
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using FlexxonCustomizations.Descriptor;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;

namespace PX.Objects.SO
{
  public class SOOrderExt : PXCacheExtension<SOOrder>
  {
    protected int? _CustomerID;

    [PXDefault]
    [CustomerActive2(DescriptionField = typeof (Customer.acctName), Filterable = true, ValidateValue = false, Visibility = PXUIVisibility.SelectorVisible)]
    [PXForeignReference(typeof (Field<SOOrder.customerID>.IsRelatedTo<PX.Objects.CR.BAccount.bAccountID>))]
    public virtual int? CustomerID
    {
      get => this._CustomerID;
      set => this._CustomerID = value;
    }

    public abstract class customerID : BqlType<IBqlInt, int>.Field<SOOrderExt.customerID>
    {
    }
  }
}
