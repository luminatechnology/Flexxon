// Decompiled with JetBrains decompiler
// Type: FlexxonCustomizations.Descriptor.FLXProjAutoNumberAttribute
// Assembly: FlexxonCustomizations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A59FE566-6025-4730-961E-B73CC13C04A9
// Assembly location: C:\Program Files\Acumatica ERP\Flexxon\Bin\FlexxonCustomizations.dll

using FlexxonCustomizations.DAC;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.FS;
using System;

namespace FlexxonCustomizations.Descriptor
{
    public class FLXProjAutoNumberAttribute : AlternateAutoNumberAttribute
    {
        public FLXProjAutoNumberAttribute(System.Type setupType, System.Type dateType)
          : base(setupType, dateType)
        {
        }

        protected override string GetInitialRefNbr(string baseRefNbr) => throw new NotImplementedException();

        protected override bool SetRefNbr(PXCache cache, object row)
        {
            bool? userNumbering = this.UserNumbering;
            bool flag = true;
            if (userNumbering.GetValueOrDefault() == flag & userNumbering.HasValue)
                return true;
            FLXProject flxProject = row as FLXProject;
            char ch = '-';
            string origProjNbr = flxProject.OrigProjNbr;
            if (string.IsNullOrEmpty(origProjNbr))
            {
                FLXSetup flxSetup = SelectFrom<FLXSetup>.View.Select(cache.Graph);
                flxProject.ProjectNbr = AutoNumberAttribute.GetNextNumber(cache, row, flxSetup.ProjNbrNumberingID, cache.Graph.Accessinfo.BusinessDate) + ch.ToString() + "0";
            }
            else
            {
                string projectNbr = origProjNbr.Substring(0, origProjNbr.IndexOf(ch));
                string str = string.Format("{0}{1}{2}", (object)projectNbr, (object)ch, (object)FLXProjAutoNumberAttribute.CountRecords(cache.Graph, projectNbr));
                flxProject.ProjectNbr = str;
            }
            return true;
        }

        public static int CountRecords(PXGraph graph, string projectNbr) => SelectFrom<FLXProject>.Where<FLXProject.projectNbr.Contains<P.AsString>>.View.ReadOnly.Select(graph, (object)projectNbr).Count;
    }
}
