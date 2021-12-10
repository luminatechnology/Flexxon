using PX.Data;
using PX.Data.WorkflowAPI;

namespace PX.Objects.PO
{
    public class POOrderEntry_WorkflowExt : PXGraphExtension<POOrderEntry_ApprovalWorkflow, POOrderEntry_Workflow, POOrderEntry>
    {
        public override void Configure(PXScreenConfiguration configuration)
        {
            base.Configure(configuration);

            WorkflowContext<POOrderEntry, POOrder> context = configuration.GetScreenConfigurationContext<POOrderEntry, POOrder>();

            context.UpdateScreenConfigurationFor(screen =>
            {
                return screen.WithFlows(flows =>
                {
                    flows.Update<POOrderType.regularOrder>(flow =>
                    {
                        return flow.WithFlowStates(states =>
                        {
                            states.Update<POOrderStatus.open>(state =>
                            {
                                return state.WithFieldStates(fields =>
                                {
                                    fields.AddField<POLineExt.usrVendConfDate>();
                                });
                            });
                            //states.Update<POOrderStatus.rejected>(state =>
                            //{
                            //    return state.WithActions(actions => { }).WithFieldStates(fields =>
                            //    {
                            //        fields.AddField<POOrder.orderDesc>();

                            //        fields.AddField(typeof(POLineExt), "usrVendConfDate");
                            //    });
                            //});
                        });
                    });
                });
            });
        }
    }
}
