<%@ Page Language="C#" MasterPageFile="~/MasterPages/TabView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="FX101000.aspx.cs" Inherits="Page_FX101000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/TabView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%"
        TypeName="FlexxonCustomizations.Graph.FLXSetupMaint"
        PrimaryView="SetupRecord" >
		<CallbackCommands>

		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" Runat="Server">
	<px:PXTab DataMember="SetupRecord" ID="tab" runat="server" DataSourceID="ds" Height="150px" Style="z-index: 100" Width="100%" AllowAutoHide="false">
		<Items>
			<px:PXTabItem Text="General Settings">
			
				<Template>
					<px:PXLayoutRule LabelsWidth="XM" DataMember="" runat="server" ID="CstPXLayoutRule1" StartGroup="True" GroupCaption="Numbering Settings" ></px:PXLayoutRule>
								<px:PXSelector Width="" Style='width:;' runat="server" ID="CstPXSelector2" DataField="ProjNbrNumberingID" AllowEdit="True" ></px:PXSelector>
								<px:PXSelector runat="server" ID="CstPXSelector3" DataField="ComisionNumberingID" AllowEdit="True" ></px:PXSelector>
								<px:PXSelector runat="server" ID="CstPXSelector6" DataField="ComsnTranNumberingID" AllowEdit="True" ></px:PXSelector>
								<px:PXLayoutRule GroupCaption="Commission Settings" runat="server" ID="CstPXLayoutRule4" StartGroup="True" LabelsWidth="XM" ></px:PXLayoutRule>
								<px:PXSegmentMask runat="server" ID="CstPXSegmentMask5" DataField="CommissionItem" ></px:PXSegmentMask>
								<px:PXNumberEdit runat="server" ID="CstPXNumberEdit7" DataField="MiniMgnPctComsn" /></Template></px:PXTabItem></Items>
		<AutoSize Container="Window" Enabled="True" MinHeight="200" ></AutoSize>
	</px:PXTab>
</asp:Content>