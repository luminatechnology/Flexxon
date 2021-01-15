// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CROpportunityProductsExt
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using FlexxonCustomizations.DAC;
using FlexxonCustomizations.Descriptor;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.IN;
using System;

namespace PX.Objects.CR
{
    public class CROpportunityProductsExt : PXCacheExtension<CROpportunityProducts>
    {
        [SalesPerson]
        [PXForeignReference(typeof(Field<CROpportunityProductsExt.usrSalespersonID>.IsRelatedTo<SalesPerson.salesPersonID>))]
        public virtual int? UsrSalespersonID { get; set; }

        [PXDBString(5, IsUnicode = true)]
        [PXUIField(DisplayName = "CW")]
        public virtual string UsrCW { get; set; }

        [PXDBString(10, IsUnicode = true)]
        [PXUIField(DisplayName = "L/T")]
        public virtual string UsrLT { get; set; }

        [PXUIField(DisplayName = "MOQ")]
        [PXDBQuantity]
        public virtual Decimal? UsrMOQ { get; set; }

        [PXDBString(5, IsUnicode = true)]
        [PXUIField(DisplayName = "RW")]
        public virtual string UsrRW { get; set; }

        [PXUIField(DisplayName = "SPQ")]
        [PXDBQuantity]
        public virtual Decimal? UsrSPQ { get; set; }

        [PXDBString(50, IsUnicode = true)]
        [PXUIField(DisplayName = "Datasheet")]
        public virtual string UsrDatasheet { get; set; }

        [PXDBString(1, IsFixed = true, IsUnicode = true)]
        [PXUIField(DisplayName = "Product Status")]
        [PXStringList(new string[] { "P", "R", "S", "A", "M", "C", "D" }, new string[] { "Prospect", "RFQ", "Sample", "Approved", "MP", "Replaced", "Drop" })]
        public virtual string UsrProdStatus { get; set; }

        [PXDBString(15, InputMask = ">CCCCCCCCCCCCCCC", IsUnicode = true)]
        [PXUIField(DisplayName = "Project Nbr.")]
        [CRProjNbrByEndCustomer]
        public virtual string UsrProjectNbr { get; set; }

        [PXDBString(50, IsUnicode = true)]
        [PXUIField(DisplayName = "Remark")]
        public virtual string UsrRemark { get; set; }

        [PXDBString(15, InputMask = ">CCCCCCCCCCCCCC", IsUnicode = true)]
        [CRCommissionIDByEndCustomer]
        public virtual string UsrCommissionID { get; set; }

        [PXString(2, IsUnicode = true)]
        [PXUIField(DisplayName = "Project Status", Enabled = false)]
        [ProjectStatus.List]
        [PXDBScalar(typeof(Search<FLXProject.status, Where<FLXProject.projectNbr, Equal<CROpportunityProductsExt.usrProjectNbr>>>))]
        [PXUnboundDefault(typeof(Search<FLXProject.status, Where<FLXProject.projectNbr, Equal<Current<CROpportunityProductsExt.usrProjectNbr>>>>))]
        [PXFormula(typeof(Default<CROpportunityProductsExt.usrProjectNbr>))]
        public virtual string UsrProjStatus { get; set; }

        public abstract class usrSalespersonID : BqlType<IBqlInt, int>.Field<CROpportunityProductsExt.usrSalespersonID>
        {
        }

        public abstract class usrCW : BqlType<IBqlString, string>.Field<CROpportunityProductsExt.usrCW>
        {
        }

        public abstract class usrLT : BqlType<IBqlString, string>.Field<CROpportunityProductsExt.usrLT>
        {
        }

        public abstract class usrMOQ : BqlType<IBqlDecimal, Decimal>.Field<CROpportunityProductsExt.usrMOQ>
        {
        }

        public abstract class usrRW : BqlType<IBqlString, string>.Field<CROpportunityProductsExt.usrRW>
        {
        }

        public abstract class usrSPQ : BqlType<IBqlDecimal, Decimal>.Field<CROpportunityProductsExt.usrSPQ>
        {
        }

        public abstract class usrDatasheet : BqlType<IBqlString, string>.Field<CROpportunityProductsExt.usrDatasheet>
        {
        }

        public abstract class usrProdStatus : BqlType<IBqlString, string>.Field<CROpportunityProductsExt.usrProdStatus>
        {
        }

        public abstract class usrProjectNbr : BqlType<IBqlString, string>.Field<CROpportunityProductsExt.usrProjectNbr>
        {
        }

        public abstract class usrRemark : BqlType<IBqlString, string>.Field<CROpportunityProductsExt.usrRemark>
        {
        }

        public abstract class usrCommissionID : BqlType<IBqlString, string>.Field<CROpportunityProductsExt.usrCommissionID>
        {
        }

        public abstract class usrProjStatus : BqlType<IBqlString, string>.Field<CROpportunityProductsExt.usrProjStatus>
        {
        }
    }
}
