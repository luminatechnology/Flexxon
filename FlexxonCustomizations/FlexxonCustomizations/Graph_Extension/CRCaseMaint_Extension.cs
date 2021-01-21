using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using PX.Common;
using PX.Data;
using System.Collections;
using PX.Data.EP;
using PX.Objects.AR;
using PX.Objects.CT;
using PX.Objects.CR.Workflows;
using PX.Objects.GL;
using PX.Objects.EP;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.SM;
using PX.TM;
using PX.Objects;
using PX.Objects.CR;

namespace PX.Objects.CR
{
    public class CRCaseMaint_Extension : PXGraphExtension<CRCaseMaint>
    {

        public override void Initialize()
        {
            base.Initialize();
            Base.Action.AddMenuAction(ECNReportAction);
        }


        #region Material Issues Action
        public PXAction<CRCase> ECNReportAction;
        [PXButton]
        [PXUIField(DisplayName = "Print ECN Report", MapEnableRights = PXCacheRights.Select)]
        protected void eCNReportAction()
        {
            if (Base.CaseCurrent.Current != null)
            {
                var curCRCaseCache = (CRCase)Base.CaseCurrent.Current;
                // create the parameter for report
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters["CaseCD"] = curCRCaseCache.CaseCD;

                // using Report Required Exception to call the report
                throw new PXReportRequiredException(parameters, "CR606000", "CR606000");
            }
        }
        #endregion
    }
}