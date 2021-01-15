// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOShipmentExt
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using PX.Data;
using PX.Data.BQL;

namespace PX.Objects.SO
{
  public class SOShipmentExt : PXCacheExtension<SOShipment>
  {
    [PXDBString(30, IsUnicode = true)]
    [PXUIField(DisplayName = "Tracking Nbr.")]
    public virtual string UsrTrackNbr { get; set; }

    [PXString(InputMask = "", IsUnicode = true)]
    [PXUIField(IsReadOnly = true)]
    public virtual string UsrNote { get; set; }

    [PXInt]
    [PXUIField(DisplayName = "Total Packages", IsReadOnly = true)]
    public virtual int? UsrTotalPackageCount
    {
      [PXDependsOnFields(new System.Type[] {typeof (SOShipment.shipmentNbr)})] get => new int?(SOShipmentEntry_Extension.TotalPackageCount(this.Base.ShipmentNbr));
      set
      {
      }
    }

    public abstract class usrTrackNbr : BqlType<IBqlString, string>.Field<SOShipmentExt.usrTrackNbr>
    {
    }

    public abstract class usrNote : BqlType<IBqlString, string>.Field<SOShipmentExt.usrNote>
    {
    }

    public abstract class usrTotalPackageCount : BqlType<IBqlInt, int>.Field<SOShipmentExt.usrTotalPackageCount>
    {
    }
  }
}
