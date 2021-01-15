// Decompiled with JetBrains decompiler
// Type: FlexxonCustomizations.BAccount3
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using PX.Data;
using PX.Data.BQL;
using System;

namespace FlexxonCustomizations
{
  [PXCacheName("BAccount For ClassID")]
  [Serializable]
  public sealed class BAccount3 : PX.Objects.CR.BAccount
  {
    [PXDimensionSelector("BIZACCT", typeof (PX.Objects.CR.BAccount.acctCD), typeof (BAccount3.acctCD), new System.Type[] {typeof (PX.Objects.CR.BAccount.acctCD), typeof (PX.Objects.CR.BAccount.acctName), typeof (PX.Objects.CR.BAccount.classID), typeof (PX.Objects.CR.BAccount.type), typeof (PX.Objects.CR.BAccount.acctReferenceNbr), typeof (PX.Objects.CR.BAccount.ownerID)})]
    [PXDBString(30, InputMask = "", IsKey = true, IsUnicode = true)]
    [PXUIField(DisplayName = "Account", Visibility = PXUIVisibility.SelectorVisible)]
    public override string AcctCD { get; set; }

    public new abstract class bAccountID : BqlType<IBqlInt, int>.Field<BAccount3.bAccountID>
    {
    }

    public new abstract class acctCD : BqlType<IBqlString, string>.Field<BAccount3.acctCD>
    {
    }

    public new abstract class acctName : BqlType<IBqlString, string>.Field<BAccount3.acctName>
    {
    }

    public new abstract class acctReferenceNbr : BqlType<IBqlString, string>.Field<BAccount3.acctReferenceNbr>
    {
    }

    public new abstract class parentBAccountID : BqlType<IBqlInt, int>.Field<BAccount3.parentBAccountID>
    {
    }

    public new abstract class ownerID : BqlType<IBqlGuid, Guid>.Field<BAccount3.ownerID>
    {
    }

    public new abstract class type : BqlType<IBqlString, string>.Field<BAccount3.type>
    {
    }

    public new abstract class defContactID : BqlType<IBqlInt, int>.Field<BAccount3.defContactID>
    {
    }

    public new abstract class defLocationID : BqlType<IBqlInt, int>.Field<BAccount3.defLocationID>
    {
    }

    public new abstract class classID : BqlType<IBqlInt, int>.Field<BAccount3.classID>
    {
    }
  }
}
