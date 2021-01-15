// Decompiled with JetBrains decompiler
// Type: FlexxonCustomizations.DAC.FLXProject
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using FlexxonCustomizations.Descriptor;
using FlexxonCustomizations.Graph;
using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.PO;
using PX.TM;
using System;

namespace FlexxonCustomizations.DAC
{
  [PXCacheName("Flexxon Project")]
  [PXPrimaryGraph(new System.Type[] {typeof (FLXProjectEntry)}, new System.Type[] {typeof (Select<FLXProject, Where<FLXProject.projectNbr, Equal<Current<FLXProject.projectNbr>>>>)})]
  [Serializable]
  public class FLXProject : IBqlTable
  {
    [PXDBString(15, InputMask = "CCCCCCCCCCCCCCC", IsKey = true, IsUnicode = true)]
    [PXUIField(DisplayName = "Project Nbr.", Visibility = PXUIVisibility.SelectorVisible)]
    [ProjectNbrSelector]
    [FLXProjAutoNumber(typeof (FLXSetup.projNbrNumberingID), typeof (AccessInfo.businessDate))]
    public virtual string ProjectNbr { get; set; }

    [PXDBString(2, InputMask = "", IsFixed = true, IsUnicode = true)]
    [PXUIField(DisplayName = "Status", Enabled = false)]
    [ProjectStatus.List]
    [PXDefault("HO")]
    public virtual string Status { get; set; }

    [PXDBDate]
    [PXUIField(DisplayName = "Start Date")]
    [PXDefault(typeof (AccessInfo.businessDate))]
    public virtual DateTime? StartDate { get; set; }

    [PXDBDate]
    [PXUIField(DisplayName = "Finish Date")]
    public virtual DateTime? FinishDate { get; set; }

    [PXDBDate]
    [PXUIField(DisplayName = "MP Date")]
    public virtual DateTime? MPDate { get; set; }

    [PXDBString(256, InputMask = "", IsUnicode = true)]
    [PXUIField(DisplayName = "Description")]
    [PXFieldDescription]
    public virtual string Descr { get; set; }

    [CustomerAndProspect(DisplayName = "Customer ID")]
    public virtual int? CustomerID { get; set; }

    [PXDBInt]
    [PXUIField(DisplayName = "End Customer", Required = true, Visibility = PXUIVisibility.Visible)]
    [PXDefault]
    [EndCustomerSelector(Filterable = true)]
    public virtual int? EndCustomerID { get; set; }

    [VendorActive]
    [PXFormula(typeof (Default<FLXProject.stockItem>))]
    [PXDefault(typeof (Search2<POVendorInventory.vendorID, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<POVendorInventory.inventoryID>, And<PX.Objects.IN.InventoryItem.preferredVendorID, Equal<POVendorInventory.vendorID>, And<PX.Objects.IN.InventoryItem.preferredVendorLocationID, Equal<POVendorInventory.vendorLocationID>>>>>, Where<POVendorInventory.inventoryID, Equal<Current<FLXProject.stockItem>>, And<POVendorInventory.active, Equal<True>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
    public virtual int? VendorID { get; set; }

    [PX.Objects.IN.StockItem]
    public virtual int? StockItem { get; set; }

    [Inventory(DisplayName = "Non Stock/MPN")]
    [PXDefault]
    public virtual int? NonStockItem { get; set; }

    [PXDBString(30, InputMask = "", IsUnicode = true)]
    [PXUIField(DisplayName = "Factory P/N")]
    public virtual string FactoryPN { get; set; }

    [PXDBString(30, InputMask = "", IsFixed = true, IsUnicode = true)]
    [PXUIField(DisplayName = "Customer To Factory")]
    public virtual string Cust2Factory { get; set; }

    [Country]
    [PXUIField(DisplayName = "Region")]
    public virtual string CountryID { get; set; }

    [PXDBInt]
    [PXDefault(0)]
    public virtual int? LineCntr { get; set; }

    [PXDBInt]
    [PXUIField(DisplayName = "Sales Rep", Visibility = PXUIVisibility.Visible)]
    [SalesRepSelector]
    public virtual int? SalesRepID { get; set; }

    [SalesPerson]
    public virtual int? SalespersonID { get; set; }

    [PXDBString(2, IsFixed = true, IsUnicode = true)]
    [PXUIField(DisplayName = "Industry")]
    [PXSelector(typeof (Search<CSAttributeDetail.valueID, Where<CSAttributeDetail.attributeID, Equal<FLXProjectEntry.IndustryAtt>>>), new System.Type[] {typeof (CSAttributeDetail.description)}, DescriptionField = typeof (CSAttributeDetail.description))]
    public virtual string Industry { get; set; }

    [PXDBString(50, InputMask = "", IsUnicode = true)]
    public virtual string Application { get; set; }

    [PXDBQuantity]
    [PXUIField(Required = true)]
    [PXDefault(TypeCode.Decimal, "0")]
    public virtual Decimal? EAU { get; set; }

    [PXDBString(10, InputMask = ">CCCCCCCCCCCCCCC", IsUnicode = true)]
    [PXUIField(DisplayName = "Oppprtunity ID", IsReadOnly = true)]
    [PXSelector(typeof (Search2<CROpportunity.opportunityID, LeftJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<CROpportunity.bAccountID>>, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.contactID, Equal<CROpportunity.contactID>>>>, Where<BqlOperand<CROpportunity.isActive, IBqlBool>.IsEqual<True>>, OrderBy<Desc<CROpportunity.opportunityID>>>), new System.Type[] {typeof (CROpportunity.opportunityID), typeof (CROpportunity.subject), typeof (CROpportunity.status), typeof (CROpportunity.stageID), typeof (CROpportunity.classID), typeof (PX.Objects.CR.BAccount.acctName), typeof (PX.Objects.CR.Contact.displayName), typeof (CROpportunity.subject), typeof (CROpportunity.externalRef), typeof (CROpportunity.closeDate)}, Filterable = true)]
    public virtual string OpportunityID { get; set; }

    [PXDBInt]
    [PXUIField(DisplayName = "Opppr. Line Nbr.", IsReadOnly = true)]
    public virtual int? OpporLineNbr { get; set; }

    [PXDBInt]
    [PXUIField(DisplayName = "CM", Visibility = PXUIVisibility.Visible)]
    [CMSelector]
    public virtual int? CM { get; set; }

    [PXDBInt]
    [PXUIField(DisplayName = "Distributor", Visibility = PXUIVisibility.Visible)]
    [DistributorSelector]
    public virtual int? Distributor { get; set; }

    [PXDBString(15, InputMask = "", IsUnicode = true)]
    [PXUIField(DisplayName = "Orig. Project Nbr.", Visible = false)]
    public virtual string OrigProjNbr { get; set; }

    [PXDBBool]
    [PXUIField(DisplayName = "Hold", Visibility = PXUIVisibility.Visible)]
    [PXDefault(true)]
    public virtual bool? Hold { get; set; }

    [PXDBGuid(false)]
    [PXUIField(DisplayName = "Owner")]
    [PXOwnerSelector(typeof (CROpportunity.workgroupID))]
    [PXDefault(typeof (AccessInfo.userID))]
    public virtual Guid? OwnerID { get; set; }

    [PXDBText(IsUnicode = true)]
    [PXUIField(DisplayName = "Design In")]
    [PXDefault("No Special Input")]
    public virtual string ISODetails { get; set; }

    [PXDBText(IsUnicode = true)]
    [PXUIField(DisplayName = "Content")]
    public virtual string LabelContent { get; set; }

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

    public abstract class projectNbr : BqlType<IBqlString, string>.Field<FLXProject.projectNbr>
    {
    }

    public abstract class status : BqlType<IBqlString, string>.Field<FLXProject.status>
    {
    }

    public abstract class startDate : BqlType<IBqlDateTime, DateTime>.Field<FLXProject.startDate>
    {
    }

    public abstract class finishDate : BqlType<IBqlDateTime, DateTime>.Field<FLXProject.finishDate>
    {
    }

    public abstract class mPDate : BqlType<IBqlDateTime, DateTime>.Field<FLXProject.mPDate>
    {
    }

    public abstract class descr : BqlType<IBqlString, string>.Field<FLXProject.descr>
    {
    }

    public abstract class customerID : BqlType<IBqlInt, int>.Field<FLXProject.customerID>
    {
    }

    public abstract class endCustomerID : BqlType<IBqlInt, int>.Field<FLXProject.endCustomerID>
    {
    }

    public abstract class vendorID : BqlType<IBqlInt, int>.Field<FLXProject.vendorID>
    {
    }

    public abstract class stockItem : BqlType<IBqlInt, int>.Field<FLXProject.stockItem>
    {
    }

    public abstract class nonStockItem : BqlType<IBqlInt, int>.Field<FLXProject.nonStockItem>
    {
    }

    public abstract class factoryPN : BqlType<IBqlString, string>.Field<FLXProject.factoryPN>
    {
    }

    public abstract class cust2Factory : BqlType<IBqlString, string>.Field<FLXProject.cust2Factory>
    {
    }

    public abstract class countryID : BqlType<IBqlInt, int>.Field<FLXProject.countryID>
    {
    }

    public abstract class lineCntr : BqlType<IBqlInt, int>.Field<FLXProject.lineCntr>
    {
    }

    public abstract class salesRepID : BqlType<IBqlInt, int>.Field<FLXProject.salesRepID>
    {
    }

    public abstract class salespersonID : BqlType<IBqlInt, int>.Field<FLXProject.salespersonID>
    {
    }

    public abstract class industry : BqlType<IBqlString, string>.Field<FLXProject.industry>
    {
    }

    public abstract class application : BqlType<IBqlString, string>.Field<FLXProject.application>
    {
    }

    public abstract class eAU : BqlType<IBqlString, string>.Field<FLXProject.eAU>
    {
    }

    public abstract class opportunityID : BqlType<IBqlString, string>.Field<FLXProject.opportunityID>
    {
    }

    public abstract class opporLineNbr : BqlType<IBqlInt, int>.Field<FLXProject.opporLineNbr>
    {
    }

    public abstract class cM : BqlType<IBqlInt, int>.Field<FLXProject.cM>
    {
    }

    public abstract class distributor : BqlType<IBqlInt, int>.Field<FLXProject.distributor>
    {
    }

    public abstract class origProjNbr : BqlType<IBqlString, string>.Field<FLXProject.origProjNbr>
    {
    }

    public abstract class hold : BqlType<IBqlBool, bool>.Field<FLXProject.hold>
    {
    }

    public abstract class ownerID : BqlType<IBqlGuid, Guid>.Field<FLXProject.ownerID>
    {
    }

    public abstract class iSODetails : BqlType<IBqlString, string>.Field<FLXProject.iSODetails>
    {
    }

    public abstract class labelContent : BqlType<IBqlString, string>.Field<FLXProject.labelContent>
    {
    }

    public abstract class createdByID : BqlType<IBqlGuid, Guid>.Field<FLXProject.createdByID>
    {
    }

    public abstract class createdByScreenID : BqlType<IBqlString, string>.Field<FLXProject.createdByScreenID>
    {
    }

    public abstract class createdDateTime : BqlType<IBqlDateTime, DateTime>.Field<FLXProject.createdDateTime>
    {
    }

    public abstract class lastModifiedByID : BqlType<IBqlGuid, Guid>.Field<FLXProject.lastModifiedByID>
    {
    }

    public abstract class lastModifiedByScreenID : BqlType<IBqlString, string>.Field<FLXProject.lastModifiedByScreenID>
    {
    }

    public abstract class lastModifiedDateTime : BqlType<IBqlDateTime, DateTime>.Field<FLXProject.lastModifiedDateTime>
    {
    }

    public abstract class noteID : BqlType<IBqlGuid, Guid>.Field<FLXProject.noteID>
    {
    }

    public abstract class tstamp : BqlType<IBqlByteArray, byte[]>.Field<FLXProject.tstamp>
    {
    }
  }
}
