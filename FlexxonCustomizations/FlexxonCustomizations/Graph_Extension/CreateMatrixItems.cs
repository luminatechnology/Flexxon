// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Matrix.Graphs.CreateMatrixItems_Extension
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using PX.Common;
using PX.Data;
using PX.Objects.IN.Matrix.DAC;
using PX.Objects.IN.Matrix.DAC.Unbound;
using PX.Objects.IN.Matrix.Utility;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PX.Objects.IN.Matrix.Graphs
{
    public class CreateMatrixItems_Extension : PXGraphExtension<CreateMatrixItems.CreateMatrixItemsImpl, CreateMatrixItems>
    {
        public PXSelect<MatrixInventoryItem> MatrixItems4Creation;
        public PXAction<MatrixInventoryItem> InsertFromExcel;

        [PXButton]
        [PXUIField(DisplayName = "Insert")]
        public IEnumerable insertFromExcel(PXAdapter adapter)
        {
            if (this.MatrixItems4Creation.AskExt() == WebDialogResult.OK)
            {
                int num1 = 1;
                FileInfo fileInfo = PXContext.SessionTyped<PXSessionStatePXData>().FileInfo["ImportMatrixFile"];
                //HttpContext.Current.Session.Remove("ImportMatrixFile");
                using (XLSXReader xlsxReader = new XLSXReader(fileInfo.BinData))
                {
                    xlsxReader.Reset();
                    xlsxReader.IndexKeyPairs.ToDictionary<KeyValuePair<int, string>, int, string>((Func<KeyValuePair<int, string>, int>)(p => p.Key), (Func<KeyValuePair<int, string>, string>)(p => p.Value));
                    xlsxReader.MoveNext();
                    while (xlsxReader.MoveNext())
                    {
                        try
                        {
                            InventoryItem template = InventoryItem.PK.Find((PXGraph)this.Base, this.Base1.Header.Current.TemplateItemID);
                            CreateMatrixItemsHelper matrixItemsHelper = new CreateMatrixItemsHelper((PXGraph)this.Base);
                            System.Collections.Generic.List<INMatrixGenerationRule> idGenerationRules;
                            System.Collections.Generic.List<INMatrixGenerationRule> descrGenerationRules;
                            matrixItemsHelper.GetGenerationRules(this.Base1.Header.Current.TemplateItemID, out idGenerationRules, out descrGenerationRules);
                            foreach (EntryMatrix row in this.Base1.Matrix.Cache.Cached.Cast<EntryMatrix>().Where<EntryMatrix>((Func<EntryMatrix, bool>)(entry =>
                           {
                               bool? isPreliminary = entry.IsPreliminary;
                               bool flag = true;
                               return !(isPreliminary.GetValueOrDefault() == flag & isPreliminary.HasValue);
                           })))
                            {
                                for (int attributeNumber = 0; attributeNumber < row.InventoryIDs.Length; ++attributeNumber)
                                {
                                    MatrixInventoryItem newItem = matrixItemsHelper.CreateMatrixItemFromTemplate(row, attributeNumber, template, idGenerationRules, descrGenerationRules);
                                    if (newItem != null)
                                    {
                                        newItem.InventoryCD = xlsxReader.GetValue(2);
                                        newItem.Descr = xlsxReader.GetValue(3);
                                        newItem.InventoryID = new int?(++num1);
                                        newItem.Duplicate = new bool?(this.Base.Caches[typeof(MatrixInventoryItem)].Cached.RowCast<MatrixInventoryItem>().Any<MatrixInventoryItem>((Func<MatrixInventoryItem, bool>)(mi => mi.InventoryCD == newItem.InventoryCD)));
                                        MatrixInventoryItem matrixInventoryItem = newItem;
                                        bool? exists = newItem.Exists;
                                        bool flag1 = true;
                                        int num2;
                                        if (!(exists.GetValueOrDefault() == flag1 & exists.HasValue))
                                        {
                                            bool? duplicate = newItem.Duplicate;
                                            bool flag2 = true;
                                            num2 = !(duplicate.GetValueOrDefault() == flag2 & duplicate.HasValue) ? 1 : 0;
                                        }
                                        else
                                            num2 = 0;
                                        bool? nullable = new bool?(num2 != 0);
                                        matrixInventoryItem.Selected = nullable;
                                        System.Collections.Generic.List<string> stringList = new System.Collections.Generic.List<string>();
                                        for (int index = 15; index <= xlsxReader.IndexKeyPairs.Count; ++index)
                                            stringList.Add(xlsxReader.GetValue(index));
                                        newItem.AttributeValues = stringList.ToArray();
                                        this.Base1.MatrixItemsForCreation.Cache.Hold((object)newItem);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            xlsxReader.Dispose();
                        }
                    }
                }
            }
            return adapter.Get();
        }
    }
}
