<%@ Page Language="C#" MasterPageFile="~/MasterPages/ListView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="FX502000.aspx.cs" Inherits="Page_FX502000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/ListView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%"
        TypeName="FlexxonCustomizations.Graph.FLXGenBillAPInvoice" PrimaryView="ComisionTranProc">
		<CallbackCommands>
                  <px:PXDSCallbackCommand Name="enterAPDate" PopupPanel="CstSmartPanel17" Visible="True" ></px:PXDSCallbackCommand>
			<px:PXDSCallbackCommand Visible="False" Name="UpdateTranAPDate" ></px:PXDSCallbackCommand></CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phL" runat="Server">
	<px:PXGrid SyncPosition="True" ID="grid" runat="server" DataSourceID="ds" Width="100%" Height="150px" SkinID="Inquire" AllowAutoHide="false">
		<Levels>
			<px:PXGridLevel DataMember="ComisionTranProc">
			    <Columns>
				<px:PXGridColumn DataField="Selected" Width="30" Type="CheckBox" AllowCheckAll="True" TextAlign="Center" ></px:PXGridColumn>
				<px:PXGridColumn DataField="CommissionTranID" Width="140" ></px:PXGridColumn>
				<px:PXGridColumn DataField="APDate" Width="90" ></px:PXGridColumn>
				<px:PXGridColumn DataField="APBillRefNbr" Width="140" ></px:PXGridColumn>
				<px:PXGridColumn DataField="DocDate" Width="90" ></px:PXGridColumn>
				<px:PXGridColumn DataField="CuryID" Width="70" ></px:PXGridColumn>
				<px:PXGridColumn DataField="CustomerID" Width="140" ></px:PXGridColumn>
				<px:PXGridColumn DataField="BranchID" Width="140" ></px:PXGridColumn>
				<px:PXGridColumn DataField="ARLineNbr" Width="70" ></px:PXGridColumn>
				<px:PXGridColumn DataField="InventoryID" Width="70" ></px:PXGridColumn>
				<px:PXGridColumn DataField="Qty" Width="100" ></px:PXGridColumn>
				<px:PXGridColumn DataField="CuryUnitPrice" Width="100" ></px:PXGridColumn>
				<px:PXGridColumn DataField="CuryTranAmt" Width="100" ></px:PXGridColumn>
				<px:PXGridColumn DataField="Percentage" Width="100" ></px:PXGridColumn>
				<px:PXGridColumn DataField="CommisionAmt" Width="100" ></px:PXGridColumn>
				<px:PXGridColumn DataField="OrderType" Width="70" ></px:PXGridColumn>
				<px:PXGridColumn DataField="OrderNbr" Width="140" ></px:PXGridColumn>
				<px:PXGridColumn DataField="CustomerOrderNbr" Width="180" ></px:PXGridColumn>
				<px:PXGridColumn DataField="ProjectNbr" Width="140" ></px:PXGridColumn>
				<px:PXGridColumn DataField="SOLineNbr" Width="70" ></px:PXGridColumn>
				<px:PXGridColumn DataField="EndCustomerID" Width="140" ></px:PXGridColumn>
				<px:PXGridColumn DataField="SalesRepID" Width="140" ></px:PXGridColumn>
				<px:PXGridColumn DataField="NonStockItem" Width="70" ></px:PXGridColumn></Columns>
			
				<RowTemplate>
					<px:PXSelector runat="server" ID="CstPXSelector1" DataField="APBillRefNbr" AllowEdit="True" ></px:PXSelector>
					<px:PXSegmentMask AllowEdit="True" AllowAddNew="" runat="server" ID="CstPXSegmentMask2" DataField="BranchID" ></px:PXSegmentMask>
					<px:PXSelector AllowEdit="True" runat="server" ID="CstPXSelector3" DataField="CuryID" ></px:PXSelector>
					<px:PXSegmentMask AllowEdit="True" AllowAddNew="" runat="server" ID="CstPXSegmentMask4" DataField="CustomerID" ></px:PXSegmentMask>
					<px:PXSelector AllowEdit="True" runat="server" ID="CstPXSelector5" DataField="EndCustomerID" ></px:PXSelector>
					<px:PXSegmentMask AllowEdit="True" AllowAddNew="" runat="server" ID="CstPXSegmentMask6" DataField="NonStockItem" ></px:PXSegmentMask>
					<px:PXSelector AllowEdit="True" runat="server" ID="CstPXSelector7" DataField="OrderNbr" ></px:PXSelector>
					<px:PXSelector AllowEdit="True" runat="server" ID="CstPXSelector8" DataField="ProjectNbr" ></px:PXSelector>
					<px:PXSelector AllowEdit="True" runat="server" ID="CstPXSelector9" DataField="SalesRepID" ></px:PXSelector></RowTemplate></px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="150" ></AutoSize>
		<ActionBar >
		</ActionBar>
	</px:PXGrid>
	<px:PXSmartPanel CaptionVisible="True" Caption="Specify AP Bill Parameter" Height="75px" ShowAfterLoad="True" Key="Parm" AutoCallBack-Command="Refresh" AutoCallBack-Target="CstFormView18" runat="server" ID="CstSmartPanel17" LoadOnDemand="True" Width="280px" >
		<px:PXFormView runat="server" ID="CstFormView18" DataMember="Parm" SkinID="Transparent" DataSourceID="ds" >
			<Template>
				<px:PXLayoutRule LabelsWidth="S" ControlSize="XM" runat="server" ID="CstPXLayoutRule27" StartColumn="True" ></px:PXLayoutRule>
				<px:PXDateTimeEdit CommitChanges="True" LabelWidth="" runat="server" ID="CstPXDateTimeEdit22" DataField="APDate" ></px:PXDateTimeEdit></Template></px:PXFormView>
		<px:PXPanel SkinID="Buttons" runat="server" ID="CstPanel19">
			<px:PXButton CommandName="UpdateTranAPDate" CommandSourceID="ds" runat="server" ID="CstButton23" DialogResult="OK" Text="OK" >
				<AutoCallBack Target="CstFormView18" Command="" ></AutoCallBack></px:PXButton></px:PXPanel></px:PXSmartPanel></asp:Content>