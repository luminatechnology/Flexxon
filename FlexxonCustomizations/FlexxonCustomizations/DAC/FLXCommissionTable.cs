// Decompiled with JetBrains decompiler
// Type: FlexxonCustomizations.DAC.FLXCommissionTable
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using FlexxonCustomizations.Descriptor;
using FlexxonCustomizations.Graph;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

namespace FlexxonCustomizations.DAC
{
  [PXCacheName("Commission Table")]
  [PXPrimaryGraph(typeof (FLXCommissionEntry))]
  [Serializable]
  public class FLXCommissionTable : IBqlTable
  {
    [PXDBString(15, InputMask = ">CCCCCCCCCCCCCC", IsKey = true, IsUnicode = true)]
    [PXUIField(DisplayName = "Commission ID")]
    [CommissionSelector]
    [AutoNumber(typeof (FLXSetup.comisionNumberingID), typeof (AccessInfo.businessDate))]
    public virtual string CommissionID { get; set; }

    [PXDBInt]
    [PXDefault(0)]
    public virtual int? LineCntr { get; set; }

    [PXDBString(256, IsUnicode = true)]
    [PXUIField(DisplayName = "Description")]
    public virtual string Descr { get; set; }

    [CustomerAndProspect(DisplayName = "Customer ID")]
    [PXDefault]
    public virtual int? CustomerID { get; set; }

    [PXDBInt]
    [PXUIField(DisplayName = "End Customer")]
    [PXDefault]
    [EndCustomerSelector(Filterable = true)]
    public virtual int? EndCustomerID { get; set; }

    [Inventory(DisplayName = "Non Stock/MPN")]
    [PXDefault]
    public virtual int? NonStock { get; set; }

    [PXDBString(10, InputMask = ">CCCCCCCCCCCCCCC", IsUnicode = true)]
    [PXUIField(DisplayName = "Oppprtunity ID", IsReadOnly = true)]
    [PXSelector(typeof (Search2<CROpportunity.opportunityID, LeftJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<CROpportunity.bAccountID>>, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.contactID, Equal<CROpportunity.contactID>>>>, Where<BqlOperand<CROpportunity.isActive, IBqlBool>.IsEqual<True>>, OrderBy<Desc<CROpportunity.opportunityID>>>), new System.Type[] {typeof (CROpportunity.opportunityID), typeof (CROpportunity.subject), typeof (CROpportunity.status), typeof (CROpportunity.stageID), typeof (CROpportunity.classID), typeof (PX.Objects.CR.BAccount.acctName), typeof (PX.Objects.CR.Contact.displayName), typeof (CROpportunity.subject), typeof (CROpportunity.externalRef), typeof (CROpportunity.closeDate)}, Filterable = true)]
    public virtual string OpportunityID { get; set; }

    [PXDBInt]
    [PXUIField(DisplayName = "Opppr. Line Nbr.", IsReadOnly = true)]
    public virtual int? OpporLineNbr { get; set; }

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

    public abstract class commissionID : BqlType<IBqlString, string>.Field<FLXCommissionTable.commissionID>
    {
    }

    public abstract class lineCntr : BqlType<IBqlInt, int>.Field<FLXCommissionTable.lineCntr>
    {
    }

    public abstract class descr : BqlType<IBqlString, string>.Field<FLXCommissionTable.descr>
    {
    }

    public abstract class customerID : BqlType<IBqlInt, int>.Field<FLXCommissionTable.customerID>
    {
    }

    public abstract class endCustomerID : BqlType<IBqlInt, int>.Field<FLXCommissionTable.endCustomerID>
    {
    }

    public abstract class nonStock : BqlType<IBqlInt, int>.Field<FLXCommissionTable.nonStock>
    {
    }

    public abstract class opportunityID : BqlType<IBqlString, string>.Field<FLXCommissionTable.opportunityID>
    {
    }

    public abstract class opporLineNbr : BqlType<IBqlInt, int>.Field<FLXCommissionTable.opporLineNbr>
    {
    }

    public abstract class createdByID : BqlType<IBqlGuid, Guid>.Field<FLXCommissionTable.createdByID>
    {
    }

    public abstract class createdByScreenID : BqlType<IBqlString, string>.Field<FLXCommissionTable.createdByScreenID>
    {
    }

    public abstract class createdDateTime : BqlType<IBqlDateTime, DateTime>.Field<FLXCommissionTable.createdDateTime>
    {
    }

    public abstract class lastModifiedByID : BqlType<IBqlGuid, Guid>.Field<FLXCommissionTable.lastModifiedByID>
    {
    }

    public abstract class lastModifiedByScreenID : BqlType<IBqlString, string>.Field<FLXCommissionTable.lastModifiedByScreenID>
    {
    }

    public abstract class lastModifiedDateTime : BqlType<IBqlDateTime, DateTime>.Field<FLXCommissionTable.lastModifiedDateTime>
    {
    }

    public abstract class noteID : BqlType<IBqlGuid, Guid>.Field<FLXCommissionTable.noteID>
    {
    }

    public abstract class tstamp : BqlType<IBqlByteArray, byte[]>.Field<FLXCommissionTable.tstamp>
    {
    }
  }
}
