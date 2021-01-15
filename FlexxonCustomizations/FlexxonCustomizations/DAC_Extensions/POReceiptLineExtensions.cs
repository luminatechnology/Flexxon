// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POReceiptLineExt
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using FlexxonCustomizations.Descriptor;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;

namespace PX.Objects.PO
{
  public class POReceiptLineExt : PXCacheExtension<POReceiptLine>
  {
    [PXString]
    [PXUIField(DisplayName = "Vendor P/N", Enabled = false)]
    [PXSelector(typeof (Search<INItemXRef.alternateID>))]
    [PXDBScalar(typeof (Search<POLine.alternateID, Where<POLine.orderType, Equal<POReceiptLine.pOType>, And<POLine.orderNbr, Equal<POReceiptLine.pONbr>, And<POLine.lineNbr, Equal<POReceiptLine.pOLineNbr>>>>>))]
    [PXFormula(typeof (Default<POReceiptLine.pOLineNbr>))]
    [PXDefault(typeof (Search<POLine.alternateID, Where<POLine.orderType, Equal<Current<POReceiptLine.pOType>>, And<POLine.orderNbr, Equal<Current<POReceiptLine.pONbr>>, And<POLine.lineNbr, Equal<Current<POReceiptLine.pOLineNbr>>>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
    public virtual string UsrAlternateID { get; set; }

    [PXInt]
    [PXUIField(DisplayName = "End Customer", Enabled = false)]
    [EndCustomerSelector]
    [PXDBScalar(typeof (Search<POLineExt.usrEndCustomerID, Where<POLine.orderType, Equal<POReceiptLine.pOType>, And<POLine.orderNbr, Equal<POReceiptLine.pONbr>, And<POLine.lineNbr, Equal<POReceiptLine.pOLineNbr>>>>>))]
    [PXFormula(typeof (Default<POReceiptLine.pOLineNbr>))]
    [PXDefault(typeof (Search<POLineExt.usrEndCustomerID, Where<POLine.orderType, Equal<Current<POReceiptLine.pOType>>, And<POLine.orderNbr, Equal<Current<POReceiptLine.pONbr>>, And<POLine.lineNbr, Equal<Current<POReceiptLine.pOLineNbr>>>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
    public virtual int? UsrEndCustomerID { get; set; }

    [PXString]
    [PXUIField(DisplayName = "Project Nbr.", Enabled = false)]
    [ProjectNbrSelector]
    [PXDBScalar(typeof (Search<POLineExt.usrProjectNbr, Where<POLine.orderType, Equal<POReceiptLine.pOType>, And<POLine.orderNbr, Equal<POReceiptLine.pONbr>, And<POLine.lineNbr, Equal<POReceiptLine.pOLineNbr>>>>>))]
    [PXFormula(typeof (Default<POReceiptLine.pOLineNbr>))]
    [PXDefault(typeof (Search<POLineExt.usrProjectNbr, Where<POLine.orderType, Equal<Current<POReceiptLine.pOType>>, And<POLine.orderNbr, Equal<Current<POReceiptLine.pONbr>>, And<POLine.lineNbr, Equal<Current<POReceiptLine.pOLineNbr>>>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
    public virtual string UsrProjectNbr { get; set; }

    public abstract class usrAlternateID : BqlType<IBqlString, string>.Field<POReceiptLineExt.usrAlternateID>
    {
    }

    public abstract class usrEndCustomerID : BqlType<IBqlInt, int>.Field<POReceiptLineExt.usrEndCustomerID>
    {
    }

    public abstract class usrProjectNbr : BqlType<IBqlString, string>.Field<POReceiptLineExt.usrProjectNbr>
    {
    }
  }
}
