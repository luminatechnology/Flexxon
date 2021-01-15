// Decompiled with JetBrains decompiler
// Type: FlexxonCustomizations.DAC.FLXProjISOSched
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using FlexxonCustomizations.Graph;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.EP;
using PX.Objects.CS;
using System;

namespace FlexxonCustomizations.DAC
{
  [PXCacheName("Project ISO Schedule")]
  [Serializable]
  public class FLXProjISOSched : IBqlTable
  {
    [PXDBIdentity(IsKey = true)]
    public virtual int? ScheduleID { get; set; }

    [PXDBString(2, InputMask = "", IsFixed = true, IsUnicode = true)]
    [PXUIField(DisplayName = "ISO Schedule ID")]
    [PXSelector(typeof (Search<CSAttributeDetail.valueID, Where<CSAttributeDetail.attributeID, Equal<FLXProjectEntry.ISOSchedAtt>>>), new System.Type[] {typeof (CSAttributeDetail.description)}, DescriptionField = typeof (CSAttributeDetail.description))]
    public virtual string ScheduleCD { get; set; }

    [PXDBDate]
    [PXUIField(DisplayName = "Schedule Date")]
    [PXDefault(typeof (AccessInfo.businessDate))]
    public virtual DateTime? ScheduleDate { get; set; }

    [PXDBString(256, InputMask = "", IsUnicode = true)]
    [PXUIField(DisplayName = "Description")]
    [PXFieldDescription]
    [PXDefault(typeof (Search<CSAttributeDetail.description, Where<CSAttributeDetail.attributeID, Equal<FLXProjectEntry.ISOSchedAtt>, And<CSAttributeDetail.valueID, Equal<Current<FLXProjISOSched.scheduleCD>>>>>))]
    [PXFormula(typeof (Default<FLXProjISOSched.scheduleCD>))]
    public virtual string Descr { get; set; }

    [PXDBString(15, InputMask = "", IsUnicode = true)]
    [PXUIField(DisplayName = "Project Nbr.")]
    [PXDBDefault(typeof (FLXProject.projectNbr))]
    [PXParent(typeof (SelectFromBase<FLXProject, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FLXProject.projectNbr, IBqlString>.IsEqual<BqlField<FLXProjISOSched.projectNbr, IBqlString>.FromCurrent>>))]
    public virtual string ProjectNbr { get; set; }

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

    public abstract class scheduleID : BqlType<IBqlInt, int>.Field<FLXProjISOSched.scheduleID>
    {
    }

    public abstract class scheduleCD : BqlType<IBqlString, string>.Field<FLXProjISOSched.scheduleCD>
    {
    }

    public abstract class scheduleDate : BqlType<IBqlDateTime, DateTime>.Field<FLXProjISOSched.scheduleDate>
    {
    }

    public abstract class descr : BqlType<IBqlString, string>.Field<FLXProjISOSched.descr>
    {
    }

    public abstract class projectNbr : BqlType<IBqlString, string>.Field<FLXProjISOSched.projectNbr>
    {
    }

    public abstract class createdByID : BqlType<IBqlGuid, Guid>.Field<FLXProjISOSched.createdByID>
    {
    }

    public abstract class createdByScreenID : BqlType<IBqlString, string>.Field<FLXProjISOSched.createdByScreenID>
    {
    }

    public abstract class createdDateTime : BqlType<IBqlDateTime, DateTime>.Field<FLXProjISOSched.createdDateTime>
    {
    }

    public abstract class lastModifiedByID : BqlType<IBqlGuid, Guid>.Field<FLXProjISOSched.lastModifiedByID>
    {
    }

    public abstract class lastModifiedByScreenID : BqlType<IBqlString, string>.Field<FLXProjISOSched.lastModifiedByScreenID>
    {
    }

    public abstract class lastModifiedDateTime : BqlType<IBqlDateTime, DateTime>.Field<FLXProjISOSched.lastModifiedDateTime>
    {
    }

    public abstract class noteID : BqlType<IBqlGuid, Guid>.Field<FLXProjISOSched.noteID>
    {
    }

    public abstract class tstamp : BqlType<IBqlByteArray, byte[]>.Field<FLXProjISOSched.tstamp>
    {
    }
  }
}
