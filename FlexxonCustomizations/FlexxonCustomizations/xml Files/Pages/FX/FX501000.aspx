<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="FX501000.aspx.cs" Inherits="Page_FX501000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%"
        TypeName="FlexxonCustomizations.Graph.FLXGenComisionPlan" PrimaryView="ComisionPlan">
		<CallbackCommands>

		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" Runat="Server">
	<px:PXGrid SyncPosition="True" ID="grid" runat="server" DataSourceID="ds" Width="100%" Height="150px" SkinID="Inquire" AllowAutoHide="false">
		<Levels>
			<px:PXGridLevel DataMember="ComisionPlan">
			  
				<Columns>
								<px:PXGridColumn DataField="Selected" Width="30" Type="CheckBox" AllowCheckAll="True" TextAlign="Center" ></px:PXGridColumn>
								<px:PXGridColumn DataField="ARInvoice__DocDate" Width="90" ></px:PXGridColumn>
								<px:PXGridColumn DataField="ARInvoice__DocType" Width="70" ></px:PXGridColumn>
								<px:PXGridColumn DataField="ARInvoice__RefNbr" Width="140" ></px:PXGridColumn>
					<px:PXGridColumn DataField="CustomerID" Width="140" ></px:PXGridColumn>
					<px:PXGridColumn DataField="ARTran__LineNbr" Width="70" ></px:PXGridColumn>
								<px:PXGridColumn DataField="ARInvoice__CuryDocBal" Width="100" ></px:PXGridColumn>
					<px:PXGridColumn DataField="CuryID" Width="70" ></px:PXGridColumn>
					<px:PXGridColumn DataField="ARTran__InventoryID" Width="70" ></px:PXGridColumn>
					<px:PXGridColumn DataField="ARTran__Qty" Width="100" ></px:PXGridColumn>
					<px:PXGridColumn DataField="ARTran__CuryTranAmt" Width="100" ></px:PXGridColumn>
					<px:PXGridColumn DataField="SOOrder__OrderType" Width="70" ></px:PXGridColumn>
								<px:PXGridColumn DataField="SOOrder__OrderNbr" Width="140" ></px:PXGridColumn>
					<px:PXGridColumn DataField="SOLine__LineNbr" Width="70" ></px:PXGridColumn>
					<px:PXGridColumn DataField="SOOrder__CustomerOrderNbr" Width="180" ></px:PXGridColumn>
					<px:PXGridColumn DataField="SOLine__UsrProjectNbr" Width="140" ></px:PXGridColumn>
					<px:PXGridColumn DataField="SOOrder__CustomerID" Width="140" ></px:PXGridColumn>
					<px:PXGridColumn DataField="SOLine__UsrEndCustomerID" Width="140" ></px:PXGridColumn>
					<px:PXGridColumn DataField="SOLine__UsrNonStockItem" Width="70" ></px:PXGridColumn></Columns>
				<RowTemplate>
					<px:PXSelector AllowEdit="True" runat="server" ID="CstPXSelector12" DataField="ARInvoice__RefNbr" ></px:PXSelector>
					<px:PXSegmentMask runat="server" ID="CstPXSegmentMask13" DataField="InventoryID" ></px:PXSegmentMask>
					<px:PXSelector AllowEdit="True" runat="server" ID="CstPXSelector14" DataField="SOOrder__OrderNbr" ></px:PXSelector>
					<px:PXSelector AllowEdit="True" runat="server" ID="CstPXSelector15" DataField="SOLine__UsrEndCustomerID" ></px:PXSelector>
					<px:PXSegmentMask AllowEdit="True" runat="server" ID="CstPXSegmentMask16" DataField="SOLine__UsrNonStockItem" ></px:PXSegmentMask>
					<px:PXSelector AllowEdit="True" runat="server" ID="CstPXSelector17" DataField="SOLine__UsrProjectNbr" ></px:PXSelector>
					<px:PXSegmentMask AllowEdit="True" runat="server" ID="CstPXSegmentMask18" DataField="SOOrder__CustomerID" ></px:PXSegmentMask>
					<px:PXSegmentMask runat="server" ID="CstPXSegmentMask20" DataField="CustomerID" AllowEdit="True" ></px:PXSegmentMask></RowTemplate></px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="150" ></AutoSize>
		<ActionBar >
		</ActionBar>
	</px:PXGrid>
</asp:Content>