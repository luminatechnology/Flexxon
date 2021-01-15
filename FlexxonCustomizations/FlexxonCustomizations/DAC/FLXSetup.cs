// Decompiled with JetBrains decompiler
// Type: FlexxonCustomizations.DAC.FLXSetup
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using FlexxonCustomizations.Descriptor;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;

namespace FlexxonCustomizations.DAC
{
  [PXCacheName("FLX Setup")]
  [Serializable]
  public class FLXSetup : IBqlTable
  {
    [PXDBString(10, InputMask = "", IsUnicode = true)]
    [PXUIField(DisplayName = "Project Nbr Numbering Sequence")]
    [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
    public virtual string ProjNbrNumberingID { get; set; }

    [PXDBString(10, InputMask = "", IsUnicode = true)]
    [PXUIField(DisplayName = "Commission Numbering Sequence")]
    [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
    public virtual string ComisionNumberingID { get; set; }

    [PXDBString(10, InputMask = "", IsUnicode = true)]
    [PXUIField(DisplayName = "Comsn. Tran Numbering Sequence")]
    [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
    public virtual string ComsnTranNumberingID { get; set; }

    [PXDefault]
    [NonStockExpItem]
    public virtual int? CommissionItem { get; set; }

    [PXDBDecimal(2)]
    [PXUIField(DisplayName = "Minimum Margin % for Commission")]
    [PXDefault(TypeCode.Decimal, "0.00")]
    public virtual Decimal? MiniMgnPctComsn { get; set; }

    [PXDBCreatedByID]
    public virtual Guid? CreatedByID { get; set; }

    [PXDBCreatedByScreenID]
    public virtual string CreatedByScreenID { get; set; }

    [PXDBCreatedDateTime]
    public virtual DateTime? CreatedDateTime { get; set; }

    [PXDBLastModifiedByID]
    public virtual Guid? LastModifiedByID { get; set; }

    [PXDBLastModifiedByScreenID]
    public virtual string LastModifiedByScreenID { get; set; }

    [PXDBLastModifiedDateTime]
    public virtual DateTime? LastModifiedDateTime { get; set; }

    [PXDBTimestamp]
    public virtual byte[] Tstamp { get; set; }

    public abstract class projNbrNumberingID : BqlType<IBqlString, string>.Field<FLXSetup.projNbrNumberingID>
    {
    }

    public abstract class comisionNumberingID : BqlType<IBqlString, string>.Field<FLXSetup.comisionNumberingID>
    {
    }

    public abstract class comsnTranNumberingID : BqlType<IBqlString, string>.Field<FLXSetup.comsnTranNumberingID>
    {
    }

    public abstract class commissionItem : BqlType<IBqlInt, int>.Field<FLXSetup.commissionItem>
    {
    }

    public abstract class miniMgnPctComsn : BqlType<IBqlDecimal, Decimal>.Field<FLXSetup.miniMgnPctComsn>
    {
    }

    public abstract class createdByID : BqlType<IBqlGuid, Guid>.Field<FLXSetup.createdByID>
    {
    }

    public abstract class createdByScreenID : BqlType<IBqlString, string>.Field<FLXSetup.createdByScreenID>
    {
    }

    public abstract class createdDateTime : BqlType<IBqlDateTime, DateTime>.Field<FLXSetup.createdDateTime>
    {
    }

    public abstract class lastModifiedByID : BqlType<IBqlGuid, Guid>.Field<FLXSetup.lastModifiedByID>
    {
    }

    public abstract class lastModifiedByScreenID : BqlType<IBqlString, string>.Field<FLXSetup.lastModifiedByScreenID>
    {
    }

    public abstract class lastModifiedDateTime : BqlType<IBqlDateTime, DateTime>.Field<FLXSetup.lastModifiedDateTime>
    {
    }

    public abstract class tstamp : BqlType<IBqlByteArray, byte[]>.Field<FLXSetup.tstamp>
    {
    }
  }
}
