// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.FLXProjPurchDetails2
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using FlexxonCustomizations.DAC;
using PX.Data;
using PX.Data.BQL;
using System;

namespace PX.Objects.PO
{
  [PXCacheName("Unbound Project Purchase Details")]
  [PXBreakInheritance]
  [Serializable]
  public class FLXProjPurchDetails2 : FLXProjPurchDetails
  {
    [PXString(15, IsUnicode = true)]
    [PXUIField(DisplayName = "Project Nbr.")]
    [PXSelector(typeof (Search<POLineExt.usrProjectNbr, Where<POLine.orderType, Equal<Current<POOrder.orderType>>, And<POLine.orderNbr, Equal<Current<POOrder.orderNbr>>, And<POLineExt.usrProjectNbr, PX.Data.IsNotNull>>>>), new System.Type[] {typeof (POLineExt.usrProjectNbr), typeof (POLineExt.usrEndCustomerID), typeof (POLine.inventoryID)}, DirtyRead = true)]
    [PXUnboundDefault(typeof (Search<POLineExt.usrProjectNbr, Where<POLine.orderType, Equal<Current<POOrder.orderType>>, And<POLine.orderNbr, Equal<Current<POOrder.orderNbr>>>>>))]
    public override string ProjectNbr { get; set; }

    [PXString(InputMask = "", IsUnicode = true)]
    [PXUIField(DisplayName = "Purchase Description", IsReadOnly = true)]
    [PXUnboundDefault(typeof (Search<FLXProjPurchDetails.purchDescr, Where<FLXProjPurchDetails.projectNbr, Equal<Current<FLXProjPurchDetails2.projectNbr>>>>))]
    [PXFormula(typeof (Default<FLXProjPurchDetails2.projectNbr>))]
    public override string PurchDescr { get; set; }

    [PXString(InputMask = "", IsUnicode = true)]
    [PXUIField(DisplayName = "Purchase Remark", IsReadOnly = true)]
    [PXUnboundDefault(typeof (Search<FLXProjPurchDetails.purchRemark, Where<FLXProjPurchDetails.projectNbr, Equal<Current<FLXProjPurchDetails2.projectNbr>>>>))]
    [PXFormula(typeof (Default<FLXProjPurchDetails2.projectNbr>))]
    public override string PurchRemark { get; set; }

    [PXString(InputMask = "", IsUnicode = true)]
    [PXUIField(DisplayName = "Internal Remark", IsReadOnly = true)]
    [PXUnboundDefault(typeof (Search<FLXProjPurchDetails.interRemark, Where<FLXProjPurchDetails.projectNbr, Equal<Current<FLXProjPurchDetails2.projectNbr>>>>))]
    [PXFormula(typeof (Default<FLXProjPurchDetails2.projectNbr>))]
    public override string InterRemark { get; set; }

    public new abstract class projectNbr : BqlType<IBqlString, string>.Field<FLXProjPurchDetails2.projectNbr>
    {
    }

    public new abstract class purchDescr : BqlType<IBqlString, string>.Field<FLXProjPurchDetails2.purchDescr>
    {
    }

    public new abstract class purchRemark : BqlType<IBqlString, string>.Field<FLXProjPurchDetails2.purchRemark>
    {
    }

    public new abstract class interRemark : BqlType<IBqlString, string>.Field<FLXProjPurchDetails2.interRemark>
    {
    }
  }
}
