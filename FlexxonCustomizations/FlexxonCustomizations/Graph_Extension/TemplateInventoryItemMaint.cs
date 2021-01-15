// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Matrix.Graphs.TemplateInventoryItemMaint_Extension
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using PX.Data;
using PX.Objects.IN.Matrix.DAC.Unbound;
using PX.Objects.IN.Matrix.GraphExtensions;
using System;
using System.Collections;

namespace PX.Objects.IN.Matrix.Graphs
{
    public class TemplateInventoryItemMaint_Extension : PXGraphExtension<CreateMatrixItemsTabExt, ApplyToMatrixItemsExt, ItemsGridExt, TemplateInventoryItemMaint>
    {
        public PXAction<InventoryItem> CopyMatItem;

        [PXButton(CommitChanges = true)]
        [PXUIField(DisplayName = "Copy Matrix Item")]
        protected IEnumerable copyMatItem(PXAdapter adapter)
        {
            MatrixInventoryItem current1 = (MatrixInventoryItem)this.Base1.MatrixItems.Current;
            AdditionalAttributes current2 = this.Base3.AdditionalAttributes.Current;
            bool? selected = current1.Selected;
            bool flag = true;
            if (selected.GetValueOrDefault() == flag & selected.HasValue)
            {
                for (int index1 = 0; index1 < current2.AttributeIdentifiers.Length; ++index1)
                {
                    int index2 = Array.IndexOf<string>(current1.AttributeIDs, current2.AttributeIdentifiers[index1]);
                    current2.Values[index1] = current1.AttributeValues[index2];
                }
            }
            this.Base3.AdditionalAttributes.Insert(current2);
            return adapter.Get();
        }
    }
}
