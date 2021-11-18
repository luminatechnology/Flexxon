<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="FX504000.aspx.cs" Inherits="Page_FX504000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%"
        TypeName="FlexxonCustomizations.Graph.FLXCreateShipment" PrimaryView="Filter" >
		<CallbackCommands>

		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" Runat="Server">
	<px:PXFormView ID="form" runat="server" DataSourceID="ds" DataMember="Filter" Width="100%" Height="50px" AllowAutoHide="false">
		<Template>
			<px:PXLayoutRule ID="PXLayoutRule1" runat="server" StartRow="True"></px:PXLayoutRule>
			<px:PXLayoutRule runat="server" ID="CstPXLayoutRule1" StartColumn="True" ></px:PXLayoutRule>
			<px:PXDateTimeEdit CommitChanges="True" runat="server" ID="CstPXDateTimeEdit3" DataField="StartDate" ></px:PXDateTimeEdit>
			<px:PXLayoutRule runat="server" ID="CstPXLayoutRule2" StartColumn="True" ></px:PXLayoutRule>
			<px:PXDateTimeEdit runat="server" ID="CstPXDateTimeEdit4" DataField="EndDate" CommitChanges="True" ></px:PXDateTimeEdit>
			<px:PXLayoutRule runat="server" ID="CstPXLayoutRule10" StartColumn="True" ></px:PXLayoutRule>
			<px:PXSegmentMask runat="server" ID="CstPXSegmentMask11" DataField="UsrBranchID" /></Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" Runat="Server">
	<px:PXGrid StatusField=""  ID="grid" runat="server" DataSourceID="ds" Width="100%" Height="150px" SkinID="Details" AllowAutoHide="false">
		<Levels>
			<px:PXGridLevel DataMember="Lines">
			    <Columns>
				<px:PXGridColumn DataField="Selected" Width="60" Type="CheckBox" AllowCheckAll="True" TextAlign="Center" ></px:PXGridColumn>
				<px:PXGridColumn DataField="SOLine__BranchID" Width="140" ></px:PXGridColumn>
				<px:PXGridColumn DataField="OrderType" Width="70" ></px:PXGridColumn>
				<px:PXGridColumn DataField="OrderNbr" Width="140" ></px:PXGridColumn>
				<px:PXGridColumn DataField="SOOrder__CustomerID" Width="140" ></px:PXGridColumn>
				<px:PXGridColumn DataField="SOOrder__CustomerLocationID" Width="70" ></px:PXGridColumn>
				<px:PXGridColumn DataField="SOLine__LineNbr" Width="70" ></px:PXGridColumn>
				<px:PXGridColumn DataField="SOLine__InventoryID" Width="70" ></px:PXGridColumn>
				<px:PXGridColumn DataField="SiteID" Width="140" ></px:PXGridColumn>
				<px:PXGridColumn DataField="SOLine__OrderQty" Width="100" ></px:PXGridColumn>
				<px:PXGridColumn DisplayFormat="N2" DataField="PlanQty" Width="100" ></px:PXGridColumn>
				<px:PXGridColumn DataField="UsrOnHand" Width="100" ></px:PXGridColumn>
				<px:PXGridColumn DataField="UsrAvailability" Width="100" ></px:PXGridColumn>
				<px:PXGridColumn DataField="UsrQtyAvailShipping" Width="100" ></px:PXGridColumn>
				<px:PXGridColumn DataField="SOLine__RequestDate" Width="90" ></px:PXGridColumn>
				<px:PXGridColumn DataField="SOLine__ShipDate" Width="90" ></px:PXGridColumn>
				<px:PXGridColumn DataField="SOLine__UsrEstArrivalDate" Width="90" ></px:PXGridColumn>
				<px:PXGridColumn DataField="SOOrder__ShipTermsID" Width="120" ></px:PXGridColumn>
				<px:PXGridColumn DataField="SOOrder__TermsID" Width="120" ></px:PXGridColumn>
				<px:PXGridColumn DataField="SOLine__UsrEndCustomerID" Width="140" ></px:PXGridColumn>
				<px:PXGridColumn DataField="SOLine__UsrNonStockItem" Width="70" ></px:PXGridColumn>
				<px:PXGridColumn DataField="SOLine__UsrBrand" Width="140" ></px:PXGridColumn>
				<px:PXGridColumn DataField="SOLine__UsrProjectNbr" Width="140" ></px:PXGridColumn>
				<px:PXGridColumn DataField="SOOrder__CreatedByID_Creator_displayName" Width="70" ></px:PXGridColumn></Columns>
			
				<RowTemplate>
					<px:PXSelector AllowEdit="True" runat="server" ID="CstPXSelector5" DataField="OrderNbr" ></px:PXSelector>
								<px:PXSelector runat="server" ID="CstPXSelector7" DataField="SOLine__UsrEndCustomerID" AllowEdit="True" ></px:PXSelector>
								<px:PXSegmentMask AllowEdit="True" runat="server" ID="CstPXSegmentMask8" DataField="SOLine__UsrNonStockItem" ></px:PXSegmentMask>
								<px:PXSegmentMask runat="server" ID="CstPXSegmentMask12" DataField="SOOrder__CustomerID" AllowEdit="True" ></px:PXSegmentMask></RowTemplate></px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="150" ></AutoSize>
		<ActionBar >
		</ActionBar>
	</px:PXGrid>
</asp:Content>