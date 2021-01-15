// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.QuoteMaint_Extension
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using PX.Data;
using PX.Objects.CS;

namespace PX.Objects.CR
{
  public class QuoteMaint_Extension : PXGraphExtension<QuoteMaint>
  {
    [PXMergeAttributes(Method = MergeMethod.Append)]
    [PXFormula(typeof (Add<CRQuote.documentDate, int30>))]
    protected void _(Events.CacheAttached<CRQuote.expirationDate> e)
    {
    }

    [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
    protected void _(Events.CacheAttached<CRQuote.subject> e)
    {
    }
  }
}
