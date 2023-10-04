<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="FX302000.aspx.cs" Inherits="Page_FX302000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%"
        TypeName="FlexxonCustomizations.Graph.FLXCommissionEntry" PrimaryView="Document">
		<CallbackCommands>

		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" Runat="Server">
	<px:PXFormView ActivityIndicator="True" FilesIndicator="True" NoteIndicator="True" ID="form" runat="server" DataSourceID="ds" DataMember="Document" Width="100%" Height="100px" AllowAutoHide="false">
		<Template>
			<px:PXLayoutRule ID="PXLayoutRule1" runat="server" StartRow="True"></px:PXLayoutRule>
			<px:PXLayoutRule runat="server" ID="CstPXLayoutRule1" StartColumn="True" ></px:PXLayoutRule>
			<px:PXSelector runat="server" ID="CstPXSelector3" DataField="CommissionID" ></px:PXSelector>
			<px:PXTextEdit TextMode="MultiLine" Height="50px" runat="server" ID="CstPXTextEdit4" DataField="Descr" ></px:PXTextEdit>
			<px:PXLayoutRule runat="server" ID="CstPXLayoutRule2" StartColumn="True" ></px:PXLayoutRule>
			<px:PXSegmentMask AllowEdit="True" runat="server" ID="CstPXSegmentMask5" DataField="CustomerID" ></px:PXSegmentMask>
			<px:PXSelector AllowEdit="True" runat="server" ID="CstPXSelector6" DataField="EndCustomerID" ></px:PXSelector>
			<px:PXSegmentMask runat="server" ID="CstPXSegmentMask7" DataField="NonStock" AllowEdit="True" ></px:PXSegmentMask>
			<px:PXLayoutRule runat="server" ID="CstPXLayoutRule9" StartColumn="True" ></px:PXLayoutRule>
			<px:PXSelector AllowEdit="True" runat="server" ID="CstPXSelector11" DataField="OpportunityID" ></px:PXSelector>
			<px:PXNumberEdit runat="server" ID="CstPXNumberEdit10" DataField="OpporLineNbr" ></px:PXNumberEdit></Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" Runat="Server">
	<px:PXGrid AutoAdjustColumns="True" SyncPosition="True" ID="grid" runat="server" DataSourceID="ds" Width="100%" Height="150px" SkinID="Details" AllowAutoHide="false">
		<Levels>
			<px:PXGridLevel DataMember="ProjComision">
			    <Columns>
				<px:PXGridColumn DataField="LineNbr" Width="70" ></px:PXGridColumn>
				<px:PXGridColumn DataField="RepType" Width="70" ></px:PXGridColumn>
				<px:PXGridColumn DataField="SalesRepID" Width="140" ></px:PXGridColumn>
				<px:PXGridColumn DataField="Percentage" Width="100" ></px:PXGridColumn>
				<px:PXGridColumn DataField="EffectiveDate" Width="90" ></px:PXGridColumn>
				<px:PXGridColumn DataField="ExpirationDate" Width="90" ></px:PXGridColumn></Columns>
			
				<RowTemplate ></RowTemplate></px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="150" ></AutoSize>
		<ActionBar >
		</ActionBar>
	</px:PXGrid>
</asp:Content>