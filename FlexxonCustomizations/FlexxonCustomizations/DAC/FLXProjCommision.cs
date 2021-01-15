// Decompiled with JetBrains decompiler
// Type: FlexxonCustomizations.DAC.FLXProjCommission
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using FlexxonCustomizations.Descriptor;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR;
using System;

namespace FlexxonCustomizations.DAC
{
  [PXCacheName("Project Commission")]
  [Serializable]
  public class FLXProjCommission : IBqlTable
  {
    [PXDBString(15, InputMask = "", IsKey = true, IsUnicode = true)]
    [PXUIField(DisplayName = "Commission ID")]
    [PXDBDefault(typeof (FLXCommissionTable.commissionID))]
    [PXParent(typeof (SelectFromBase<FLXCommissionTable, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FLXCommissionTable.commissionID, IBqlString>.IsEqual<BqlField<FLXProjCommission.commissionID, IBqlString>.FromCurrent>>))]
    public virtual string CommissionID { get; set; }

    [PXDBInt(IsKey = true)]
    [PXUIField(DisplayName = "Line Nbr")]
    [PXLineNbr(typeof (FLXCommissionTable.lineCntr))]
    public virtual int? LineNbr { get; set; }

    [PXDBString(15, InputMask = "", IsUnicode = true)]
    [PXUIField(DisplayName = "Project Nbr")]
    [ProjectNbrSelector]
    public virtual string ProjectNbr { get; set; }

    [PXDBString(2, InputMask = "", IsFixed = true, IsUnicode = true)]
    [PXUIField(DisplayName = "Rep Type")]
    [FlexxonCustomizations.DAC.RepType.List]
    public virtual string RepType { get; set; }

    [PXDBInt]
    [PXUIField(DisplayName = "Sales Rep", Visibility = PXUIVisibility.Visible)]
    [SalesRepSelector(typeof (Search<BAccountR.bAccountID>))]
    public virtual int? SalesRepID { get; set; }

    [PXDBDecimal]
    [PXUIField(DisplayName = "Percentage")]
    public virtual Decimal? Percentage { get; set; }

    [PXDBDate]
    [PXUIField(DisplayName = "Effective Date")]
    public virtual DateTime? EffectiveDate { get; set; }

    [PXDBDate]
    [PXUIField(DisplayName = "Expiry Date")]
    public virtual DateTime? ExpirationDate { get; set; }

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

    [PXNote]
    public virtual Guid? NoteID { get; set; }

    [PXDBTimestamp]
    public virtual byte[] Tstamp { get; set; }

    public abstract class commissionID : BqlType<IBqlString, string>.Field<FLXProjCommission.commissionID>
    {
    }

    public abstract class lineNbr : BqlType<IBqlInt, int>.Field<FLXProjCommission.lineNbr>
    {
    }

    public abstract class projectNbr : BqlType<IBqlString, string>.Field<FLXProjCommission.projectNbr>
    {
    }

    public abstract class repType : BqlType<IBqlString, string>.Field<FLXProjCommission.repType>
    {
    }

    public abstract class salesRepID : BqlType<IBqlInt, int>.Field<FLXProjCommission.salesRepID>
    {
    }

    public abstract class percentage : BqlType<IBqlDecimal, Decimal>.Field<FLXProjCommission.percentage>
    {
    }

    public abstract class effectiveDate : BqlType<IBqlDateTime, DateTime>.Field<FLXProjCommission.effectiveDate>
    {
    }

    public abstract class expirationDate : BqlType<IBqlDateTime, DateTime>.Field<FLXProjCommission.expirationDate>
    {
    }

    public abstract class createdByID : BqlType<IBqlGuid, Guid>.Field<FLXProjCommission.createdByID>
    {
    }

    public abstract class createdByScreenID : BqlType<IBqlString, string>.Field<FLXProjCommission.createdByScreenID>
    {
    }

    public abstract class createdDateTime : BqlType<IBqlDateTime, DateTime>.Field<FLXProjCommission.createdDateTime>
    {
    }

    public abstract class lastModifiedByID : BqlType<IBqlGuid, Guid>.Field<FLXProjCommission.lastModifiedByID>
    {
    }

    public abstract class lastModifiedByScreenID : BqlType<IBqlString, string>.Field<FLXProjCommission.lastModifiedByScreenID>
    {
    }

    public abstract class lastModifiedDateTime : BqlType<IBqlDateTime, DateTime>.Field<FLXProjCommission.lastModifiedDateTime>
    {
    }

    public abstract class noteID : BqlType<IBqlGuid, Guid>.Field<FLXProjCommission.noteID>
    {
    }

    public abstract class tstamp : BqlType<IBqlByteArray, byte[]>.Field<FLXProjCommission.tstamp>
    {
    }
  }
}
