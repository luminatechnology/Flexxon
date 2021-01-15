// Decompiled with JetBrains decompiler
// Type: FlexxonCustomizations.Descriptor.CustomerActive2Attribute
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using FlexxonCustomizations.Graph;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

namespace FlexxonCustomizations.Descriptor
{
  [PXDBInt]
  [PXUIField(DisplayName = "Customer ID", Visibility = PXUIVisibility.Visible)]
  [Serializable]
  public class CustomerActive2Attribute : AcctSubAttribute
  {
    public CustomerActive2Attribute()
    {
      System.Type type1 = BqlCommand.Compose(typeof (Search2<,,>), typeof (BAccountR.bAccountID), typeof (LeftJoin<,,>), typeof (Customer), typeof (On<Customer.bAccountID, Equal<BAccountR.bAccountID>, And<Match<Customer, Current<AccessInfo.userName>>>>), typeof (LeftJoin<,,>), typeof (PX.Objects.CR.Contact), typeof (On<PX.Objects.CR.Contact.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<BAccountR.defContactID>>>), typeof (LeftJoin<,,>), typeof (Address), typeof (On<Address.bAccountID, Equal<BAccountR.bAccountID>, And<Address.addressID, Equal<BAccountR.defAddressID>>>), typeof (LeftJoin<,,>), typeof (PX.Objects.CR.Location), typeof (On<PX.Objects.CR.Location.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Location.locationID, Equal<BAccountR.defLocationID>>>), typeof (InnerJoin<,>), typeof (CSAnswers), typeof (On<CSAnswers.refNoteID, Equal<BAccountR.noteID>, And<CSAnswers.attributeID, Equal<FLXProjectEntry.BranchAtt>, And<CSAnswers.value, Contains<RTrim<Current<CustomerExt.usrBranchCD>>>>>>), typeof (Where<BAccountR.type, In3<BAccountType.branchType, BAccountType.organizationType, BAccountType.customerType, BAccountType.combinedType>>));
      PXAggregateAttribute.AggregatedAttributesCollection attributes = this._Attributes;
      System.Type type2 = type1;
      System.Type substituteKey = typeof (BAccountR.acctCD);
      System.Type[] typeArray = new System.Type[13]
      {
        typeof (BAccountR.acctCD),
        typeof (BAccountR.acctName),
        typeof (Address.addressLine1),
        typeof (Address.addressLine2),
        typeof (Address.postalCode),
        typeof (PX.Objects.CR.Contact.phone1),
        typeof (Address.city),
        typeof (Address.countryID),
        typeof (PX.Objects.CR.Location.taxRegistrationID),
        typeof (Customer.curyID),
        typeof (PX.Objects.CR.Contact.attention),
        typeof (Customer.customerClassID),
        typeof (Customer.status)
      };
      PXDimensionSelectorAttribute selectorAttribute1;
      PXDimensionSelectorAttribute selectorAttribute2 = selectorAttribute1 = new PXDimensionSelectorAttribute("BIZACCT", type2, substituteKey, typeArray);
      attributes.Add((PXEventSubscriberAttribute) selectorAttribute1);
      selectorAttribute2.DescriptionField = typeof (Customer.acctName);
      selectorAttribute2.CacheGlobal = true;
      selectorAttribute2.FilterEntity = typeof (Customer);
      this._SelAttrIndex = this._Attributes.Count - 1;
      this.Filterable = true;
    }
  }
}
