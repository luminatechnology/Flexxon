// Decompiled with JetBrains decompiler
// Type: FlexxonCustomizations.DAC.FLXProjPurchDetails
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.EP;
using System;

namespace FlexxonCustomizations.DAC
{
  [PXCacheName("Project Purchase Details")]
  [Serializable]
  public class FLXProjPurchDetails : IBqlTable
  {
    [PXDBIdentity(IsKey = true)]
    public virtual int? PurchID { get; set; }

    [PXDBString(15, InputMask = "", IsUnicode = true)]
    [PXDBDefault(typeof (FLXProject.projectNbr))]
    [PXParent(typeof (SelectFromBase<FLXProject, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FLXProject.projectNbr, IBqlString>.IsEqual<BqlField<FLXProjPurchDetails.projectNbr, IBqlString>.FromCurrent>>))]
    public virtual string ProjectNbr { get; set; }

    [PXDBString(InputMask = "", IsUnicode = true)]
    [PXUIField(DisplayName = "Purchase Description")]
    [PXFieldDescription]
    public virtual string PurchDescr { get; set; }

    [PXDBString(InputMask = "", IsUnicode = true)]
    [PXUIField(DisplayName = "Purchase Remark")]
    public virtual string PurchRemark { get; set; }

    [PXDBString(InputMask = "", IsUnicode = true)]
    [PXUIField(DisplayName = "Internal Remark")]
    public virtual string InterRemark { get; set; }

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

    public abstract class purchID : BqlType<IBqlInt, int>.Field<FLXProjPurchDetails.purchID>
    {
    }

    public abstract class projectNbr : BqlType<IBqlString, string>.Field<FLXProjPurchDetails.projectNbr>
    {
    }

    public abstract class purchDescr : BqlType<IBqlString, string>.Field<FLXProjPurchDetails.purchDescr>
    {
    }

    public abstract class purchRemark : BqlType<IBqlString, string>.Field<FLXProjPurchDetails.purchRemark>
    {
    }

    public abstract class interRemark : BqlType<IBqlString, string>.Field<FLXProjPurchDetails.interRemark>
    {
    }

    public abstract class createdByID : BqlType<IBqlGuid, Guid>.Field<FLXProjPurchDetails.createdByID>
    {
    }

    public abstract class createdByScreenID : BqlType<IBqlString, string>.Field<FLXProjPurchDetails.createdByScreenID>
    {
    }

    public abstract class createdDateTime : BqlType<IBqlDateTime, DateTime>.Field<FLXProjPurchDetails.createdDateTime>
    {
    }

    public abstract class lastModifiedByID : BqlType<IBqlGuid, Guid>.Field<FLXProjPurchDetails.lastModifiedByID>
    {
    }

    public abstract class lastModifiedByScreenID : BqlType<IBqlString, string>.Field<FLXProjPurchDetails.lastModifiedByScreenID>
    {
    }

    public abstract class lastModifiedDateTime : BqlType<IBqlDateTime, DateTime>.Field<FLXProjPurchDetails.lastModifiedDateTime>
    {
    }

    public abstract class noteID : BqlType<IBqlGuid, Guid>.Field<FLXProjPurchDetails.noteID>
    {
    }

    public abstract class tstamp : BqlType<IBqlByteArray, byte[]>.Field<FLXProjPurchDetails.tstamp>
    {
    }
  }
}
