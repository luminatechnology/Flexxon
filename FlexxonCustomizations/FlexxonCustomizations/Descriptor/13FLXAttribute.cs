// Decompiled with JetBrains decompiler
// Type: FlexxonCustomizations.Descriptor.CRAttributeList2`1
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using PX.Common;
using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;
using System;
using System.Globalization;
using System.Linq;

namespace FlexxonCustomizations.Descriptor
{
    public class CRAttributeList2<T> : CRAttributeList<T>
    {
        public CRAttributeList2(PXGraph graph)
          : base(graph)
        {
        }

        protected override void FieldSelectingHandler(PXCache sender, PXFieldSelectingEventArgs e)
        {
            if (!(e.Row is CSAnswers row))
                return;
            CRAttribute.Attribute attribute = CRAttribute.Attributes[row.AttributeID];
            System.Collections.Generic.List<CRAttribute.AttributeValue> values = attribute?.Values;
            bool? nullable1 = row.IsRequired;
            bool flag = true;
            int num1 = nullable1.GetValueOrDefault() == flag & nullable1.HasValue ? 1 : -1;
            if (values != null && values.Count > 0)
            {
                System.Collections.Generic.List<string> stringList1 = new System.Collections.Generic.List<string>();
                System.Collections.Generic.List<string> stringList2 = new System.Collections.Generic.List<string>();
                foreach (CRAttribute.AttributeValue attributeValue in values)
                {
                    if (!attributeValue.Disabled || !(row.Value != attributeValue.ValueID))
                    {
                        stringList1.Add(attributeValue.ValueID);
                        stringList2.Add(attributeValue.Description);
                    }
                }
                e.ReturnState = (object)PXStringState.CreateInstance(e.ReturnState, new int?(10), new bool?(true), typeof(CSAnswers.value).Name, new bool?(false), new int?(num1), attribute.EntryMask, stringList1.ToArray(), stringList2.ToArray(), new bool?(true), (string)null);
                int? controlType = attribute.ControlType;
                int num2 = 6;
                if (controlType.GetValueOrDefault() == num2 & controlType.HasValue)
                    ((PXStringState)e.ReturnState).MultiSelect = true;
            }
            else if (attribute != null)
            {
                int? nullable2 = attribute.ControlType;
                int num2 = 4;
                if (nullable2.GetValueOrDefault() == num2 & nullable2.HasValue)
                {
                    PXFieldSelectingEventArgs selectingEventArgs = e;
                    object returnState = e.ReturnState;
                    System.Type dataType = typeof(bool);
                    bool? isKey = new bool?(false);
                    bool? nullable3 = new bool?(false);
                    int? required = new int?(num1);
                    nullable2 = new int?();
                    int? precision = nullable2;
                    nullable2 = new int?();
                    int? length = nullable2;
                    // ISSUE: variable of a boxed type
                    bool local = false;
                    string name = typeof(CSAnswers.value).Name;
                    bool? enabled = new bool?(true);
                    bool? visible = new bool?(true);
                    nullable1 = new bool?();
                    bool? readOnly = nullable1;
                    PXFieldState instance = PXFieldState.CreateInstance(returnState, dataType, isKey, nullable3, required, precision, length, (object)local, name, enabled: enabled, visible: visible, readOnly: readOnly, visibility: PXUIVisibility.Visible);
                    selectingEventArgs.ReturnState = (object)instance;
                    int result;
                    if (e.ReturnValue is string && int.TryParse((string)e.ReturnValue, NumberStyles.Integer, (IFormatProvider)CultureInfo.InvariantCulture, out result))
                        e.ReturnValue = (object)Convert.ToBoolean(result);
                }
                else
                {
                    nullable2 = attribute.ControlType;
                    int num3 = 5;
                    if (nullable2.GetValueOrDefault() == num3 & nullable2.HasValue)
                    {
                        e.ReturnState = (object)PXDateState.CreateInstance(e.ReturnState, typeof(CSAnswers.value).Name, new bool?(false), new int?(num1), attribute.EntryMask, attribute.EntryMask, new DateTime?(), new DateTime?());
                    }
                    else
                    {
                        PXStringState stateExt = sender.GetStateExt<CSAnswers.value>((object)null) as PXStringState;
                        PXFieldSelectingEventArgs selectingEventArgs = e;
                        object returnState = e.ReturnState;
                        int? length = new int?(stateExt.With<PXStringState, int>((Func<PXStringState, int>)(_ => _.Length)));
                        nullable1 = new bool?();
                        bool? isUnicode = nullable1;
                        string name = typeof(CSAnswers.value).Name;
                        bool? isKey = new bool?(false);
                        int? required = new int?(num1);
                        string entryMask = attribute.EntryMask;
                        bool? exclusiveValues = new bool?(true);
                        PXFieldState instance = PXStringState.CreateInstance(returnState, length, isUnicode, name, isKey, required, entryMask, (string[])null, (string[])null, exclusiveValues, (string)null);
                        selectingEventArgs.ReturnState = (object)instance;
                    }
                }
            }
            if (!(e.ReturnState is PXFieldState))
                return;
            PXFieldState returnState1 = (PXFieldState)e.ReturnState;
            IPXInterfaceField pxInterfaceField = sender.GetAttributes((object)row, typeof(CSAnswers.value).Name).OfType<IPXInterfaceField>().FirstOrDefault<IPXInterfaceField>();
            if (pxInterfaceField != null && pxInterfaceField.ErrorLevel != PXErrorLevel.Undefined && !string.IsNullOrEmpty(pxInterfaceField.ErrorText))
            {
                returnState1.Error = pxInterfaceField.ErrorText;
                returnState1.ErrorLevel = pxInterfaceField.ErrorLevel;
            }
            returnState1.Enabled = true;
        }
    }
}
