<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="FX201000.aspx.cs" Inherits="Page_FX201000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%"
        TypeName="FlexxonCustomizations.Graph.FLXShipmentTrkMaint" PrimaryView="Shipment">
		<CallbackCommands>

		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" Runat="Server">
	<px:PXGrid FastFilterFields="ShipmentNbr,CustomerID,SiteID,ShipDate" SyncPosition="True" AllowPaging="True" AdjustPageSize="Auto" AllowSearch="True" ID="grid" runat="server" DataSourceID="ds" Width="100%" Height="" SkinID="Primary" AllowAutoHide="false">
		<Levels>
			<px:PXGridLevel DataMember="Shipment">
			    <Columns>
				<px:PXGridColumn DataField="ShipmentType" Width="70" ></px:PXGridColumn>
				<px:PXGridColumn DataField="ShipmentNbr" Width="140" ></px:PXGridColumn>
				<px:PXGridColumn DataField="Status" Width="70" ></px:PXGridColumn>
				<px:PXGridColumn DataField="CustomerID" Width="140" ></px:PXGridColumn>
				<px:PXGridColumn DataField="ShipmentQty" Width="100" ></px:PXGridColumn>
				<px:PXGridColumn DataField="SiteID" Width="140" ></px:PXGridColumn>
				<px:PXGridColumn DataField="ShipDate" Width="90" ></px:PXGridColumn>
				<px:PXGridColumn DataField="ShipVia" Width="140" ></px:PXGridColumn>
				<px:PXGridColumn DataField="ShipTermsID" Width="120" ></px:PXGridColumn>
				<px:PXGridColumn DataField="ShipZoneID" Width="140" ></px:PXGridColumn>
				<px:PXGridColumn DataField="UsrTrackNbr" Width="140" ></px:PXGridColumn></Columns>
			
				<RowTemplate>
					<px:PXSegmentMask AllowEdit="True" runat="server" ID="CstPXSegmentMask1" DataField="CustomerID" ></px:PXSegmentMask>
					<px:PXSelector AllowEdit="True" runat="server" ID="CstPXSelector2" DataField="ShipmentNbr" ></px:PXSelector>
					<px:PXSelector AllowEdit="True" runat="server" ID="CstPXSelector3" DataField="ShipTermsID" ></px:PXSelector>
					<px:PXSelector AllowEdit="True" runat="server" ID="CstPXSelector4" DataField="ShipVia" ></px:PXSelector>
					<px:PXSelector runat="server" ID="CstPXSelector5" DataField="ShipZoneID" AllowEdit="True" ></px:PXSelector></RowTemplate></px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="150" ></AutoSize>
		<ActionBar >
		</ActionBar>
	
		<Mode AllowAddNew="False" AllowDelete="False" ></Mode></px:PXGrid>
</asp:Content>