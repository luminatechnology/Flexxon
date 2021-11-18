<%@ Page Language="C#" MasterPageFile="~/MasterPages/ListView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="FX503000.aspx.cs" Inherits="Page_FX503000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/ListView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%"
        TypeName="FlexxonCustomizations.Graph.FLXSupplyDemandProc" PrimaryView="SupplyDemandProc">
		<CallbackCommands>

		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phL" runat="Server">
	<px:PXGrid ID="grid" runat="server" DataSourceID="ds" Width="100%" Height="150px" SkinID="PrimaryInquire" AllowAutoHide="false">
		<Levels>
			<px:PXGridLevel DataMember="SupplyDemandProc">
			    <Columns>
				<px:PXGridColumn Type="CheckBox" AllowCheckAll="True" DataField="Selected" Width="30" ></px:PXGridColumn>
				<px:PXGridColumn DataField="InventoryID" Width="70" ></px:PXGridColumn>
				<px:PXGridColumn DataField="Type" Width="70" ></px:PXGridColumn>
				<px:PXGridColumn DataField="SOOrderType" Width="70" ></px:PXGridColumn>
				<px:PXGridColumn DataField="SOOrderNbr" Width="140" ></px:PXGridColumn>
				<px:PXGridColumn DataField="SOOrderStatus" Width="70" ></px:PXGridColumn>
				<px:PXGridColumn DataField="POOrderType" Width="70" />
				<px:PXGridColumn DataField="POOrderNbr" Width="140" />
				<px:PXGridColumn DataField="POOrderStatus" Width="70" />
				<px:PXGridColumn DataField="EndCustomerID" Width="140" ></px:PXGridColumn>
				<px:PXGridColumn DataField="NonStockMPN" Width="70" ></px:PXGridColumn>
				<px:PXGridColumn DataField="ProjectNbr" Width="140" ></px:PXGridColumn>
				<px:PXGridColumn DataField="OpenQty" Width="100" ></px:PXGridColumn>
				<px:PXGridColumn DataField="OrderDate" Width="90" ></px:PXGridColumn></Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="150" ></AutoSize>
		<ActionBar >
		</ActionBar>
	</px:PXGrid>
</asp:Content>