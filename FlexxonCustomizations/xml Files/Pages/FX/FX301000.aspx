<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormTab.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="FX301000.aspx.cs" Inherits="Page_FX301000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormTab.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%"
        TypeName="FlexxonCustomizations.Graph.FLXProjectEntry" PrimaryView="Document">
		<CallbackCommands>
			<px:PXDSCallbackCommand CommitChanges="True" Name="NewTask" Visible="False" />
			<px:PXDSCallbackCommand CommitChanges="True" Name="NewEvent" Visible="False" />
			<px:PXDSCallbackCommand CommitChanges="True" Name="NewMailActivity" Visible="False" />
			<px:PXDSCallbackCommand CommitChanges="True" Name="NewActivity" Visible="False" /></CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" Runat="Server">
	<px:PXFormView NotifyIndicator="True" LinkIndicator="" ActivityIndicator="True" ID="form" runat="server" DataSourceID="ds" DataMember="Document" Width="100%" Height="280px" AllowAutoHide="">
		<Template>
			<px:PXLayoutRule ID="PXLayoutRule1" runat="server" StartRow="True"></px:PXLayoutRule>
			<px:PXLayoutRule runat="server" ID="CstPXLayoutRule1" StartColumn="True" ></px:PXLayoutRule>
			<px:PXSelector AutoRefresh="True" runat="server" ID="CstPXSelector45" DataField="ProjectNbr" ></px:PXSelector>
			<px:PXCheckBox CommitChanges="True" runat="server" ID="CstPXCheckBox43" DataField="Hold" ></px:PXCheckBox>
			<px:PXDropDown Size="S" runat="server" ID="CstPXDropDown5" DataField="Status" ></px:PXDropDown>
			<px:PXDateTimeEdit runat="server" ID="CstPXDateTimeEdit6" DataField="StartDate" ></px:PXDateTimeEdit>
			<px:PXDateTimeEdit runat="server" ID="CstPXDateTimeEdit7" DataField="FinishDate" ></px:PXDateTimeEdit>
			<px:PXDateTimeEdit runat="server" ID="CstPXDateTimeEdit8" DataField="MPDate" ></px:PXDateTimeEdit>
			<px:PXSelector runat="server" ID="CstPXSelector31" DataField="SalesRepID" ></px:PXSelector>
			<px:PXSegmentMask runat="server" ID="CstPXSegmentMask32" DataField="SalespersonID" ></px:PXSegmentMask>
			<px:PXLayoutRule runat="server" ID="CstLayoutRule17" ColumnSpan="2" ></px:PXLayoutRule>
			<px:PXTextEdit runat="server" ID="CstPXTextEdit18" DataField="Descr" ></px:PXTextEdit>
			<px:PXLayoutRule ControlSize="L" runat="server" ID="CstPXLayoutRule2" StartColumn="True" ></px:PXLayoutRule>
			<px:PXSegmentMask runat="server" ID="CstPXSegmentMask9" DataField="CustomerID" ></px:PXSegmentMask>
			<px:PXSelector AllowEdit="True" CommitChanges="True" runat="server" ID="CstPXSelector10" DataField="EndCustomerID" ></px:PXSelector>
			<px:PXSegmentMask LabelText="Test" runat="server" ID="CstPXSegmentMask12" DataField="NonStockItem"></px:PXSegmentMask>
			<px:PXSegmentMask CommitChanges="True" AllowEdit="True" runat="server" ID="CstPXSegmentMask13" DataField="StockItem" ></px:PXSegmentMask>
			<px:PXSegmentMask runat="server" ID="CstPXSegmentMask11" DataField="VendorID" ></px:PXSegmentMask>
			<px:PXSelector AllowEdit="True" runat="server" ID="CstPXSelector40" DataField="OpportunityID" ></px:PXSelector>
			<px:PXNumberEdit runat="server" ID="CstPXNumberEdit34" DataField="OpporLineNbr" ></px:PXNumberEdit>
			<px:PXSelector runat="server" ID="CstPXSelector44" DataField="OwnerID" ></px:PXSelector>
			<px:PXLayoutRule LabelsWidth="SM" runat="server" ID="CstPXLayoutRule3" StartColumn="True" ></px:PXLayoutRule>
			<px:PXSelector runat="server" ID="CstPXSelector35" DataField="Industry" ></px:PXSelector>
			<px:PXTextEdit runat="server" ID="CstPXTextEdit36" DataField="Application" ></px:PXTextEdit>
			<px:PXTextEdit runat="server" ID="CstPXTextEdit14" DataField="FactoryPN" ></px:PXTextEdit>
			<px:PXTextEdit runat="server" ID="CstPXTextEdit15" DataField="Cust2Factory" ></px:PXTextEdit>
			<px:PXSelector runat="server" ID="CstPXSelector37" DataField="CountryID" ></px:PXSelector>
			<px:PXTextEdit runat="server" ID="CstPXTextEdit38" DataField="EAU" ></px:PXTextEdit>
			<px:PXSelector runat="server" ID="CstPXSelector41" DataField="CM" ></px:PXSelector>
			<px:PXSelector runat="server" ID="CstPXSelector42" DataField="Distributor" ></px:PXSelector></Template>
</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" Runat="Server">
	<px:PXTab DataMember="CurrentDocument" ID="tab" runat="server" Width="100%" Height="150px" DataSourceID="ds" AllowAutoHide="false">
		<Items>
			<px:PXTabItem Text="Activity">
				<Template>
					<pxa:PXGridWithPreview runat="server" PrimaryViewControlID="form" PreviewPanelStyle="z-index: 100; background-color: Window" SplitterStyle="z-index: 100; border-top: solid 1px Gray;  border-bottom: solid 1px Gray" GridSkinID="Details" PreviewPanelSkinID="Preview" NoteField="NoteText" AllowSearch="True" AllowPaging="true" BlankFilterHeader="All Activities" MatrixMode="true" DataSourceID="ds" DataMember="Activity" ID="gridActivities" BorderWidth="0px" Width="100%">
						<AutoSize Enabled="True" MinHeight="150" ></AutoSize>
						<ActionBar DefaultAction="cmdViewActivity" PagerVisible="False">
							<Actions>
								<AddNew Enabled="False" ></AddNew>
								<Delete Enabled="False" ></Delete></Actions>
							<CustomItems>
								<px:PXToolBarButton Key="cmdAddTask">
									<AutoCallBack Target="ds" Command="NewTask" ></AutoCallBack>
									<PopupCommand Target="ds" Command="Cancel" ></PopupCommand></px:PXToolBarButton>
								<px:PXToolBarButton Key="cmdAddEvent">
									<AutoCallBack Target="ds" Command="NewEvent" ></AutoCallBack>
									<PopupCommand Target="ds" Command="Cancel" ></PopupCommand></px:PXToolBarButton>
								<px:PXToolBarButton Key="cmdAddEmail">
									<AutoCallBack Target="ds" Command="NewMailActivity" ></AutoCallBack>
									<PopupCommand Target="ds" Command="Cancel" ></PopupCommand></px:PXToolBarButton>
								<px:PXToolBarButton Key="cmdAddActivity">
									<AutoCallBack Target="ds" Command="NewActivity" ></AutoCallBack>
									<PopupCommand Target="ds" Command="Cancel" ></PopupCommand></px:PXToolBarButton>
								<px:PXToolBarButton Key="cmdViewActivity" Visible="false">
									<AutoCallBack Target="ds" Command="ViewActivity" ></AutoCallBack>
									<ActionBar MenuVisible="false" ></ActionBar></px:PXToolBarButton></CustomItems></ActionBar>
						<GridMode AllowAddNew="False" AllowUpdate="False" ></GridMode>
						<CallbackCommands>
							<Refresh CommitChanges="True" PostData="Page" ></Refresh></CallbackCommands>
						<Levels>
							<px:PXGridLevel DataMember="Activity">
								<RowTemplate>
									<px:PXTimeSpan runat="server" InputMask="hh:mm" MaxHours="99" DataField="TimeSpent" ID="edTimeSpent" ></px:PXTimeSpan>
									<px:PXTimeSpan runat="server" InputMask="hh:mm" MaxHours="99" DataField="OvertimeSpent" ID="edOvertimeSpent" ></px:PXTimeSpan>
									<px:PXTimeSpan runat="server" InputMask="hh:mm" MaxHours="99" DataField="TimeBillable" ID="edTimeBillable" ></px:PXTimeSpan>
									<px:PXTimeSpan runat="server" InputMask="hh:mm" MaxHours="99" DataField="OvertimeBillable" ID="edOvertimeBillable" ></px:PXTimeSpan></RowTemplate>
								<Columns>
									<px:PXGridColumn DataField="IsCompleteIcon" Width="21px" AllowShowHide="False" ForceExport="True" ></px:PXGridColumn>
									<px:PXGridColumn DataField="PriorityIcon" Width="21px" AllowResize="False" AllowShowHide="False" ForceExport="True" ></px:PXGridColumn>
									<px:PXGridColumn DataField="CRReminder__ReminderIcon" Width="21px" AllowResize="False" AllowShowHide="False" ForceExport="True" ></px:PXGridColumn>
									<px:PXGridColumn DataField="ClassIcon" Width="31px" AllowShowHide="False" ForceExport="True" ></px:PXGridColumn>
									<px:PXGridColumn DataField="ClassInfo" ></px:PXGridColumn>
									<px:PXGridColumn DataField="RefNoteID" Visible="false" AllowShowHide="False" ></px:PXGridColumn>
									<px:PXGridColumn DataField="Subject" LinkCommand="ViewActivity" ></px:PXGridColumn>
									<px:PXGridColumn DataField="ApprovalStatus" ></px:PXGridColumn>
									<px:PXGridColumn DataField="Released" ></px:PXGridColumn>
									<px:PXGridColumn DataField="StartDate" ></px:PXGridColumn>
									<px:PXGridColumn DataField="CategoryID" ></px:PXGridColumn>
									<px:PXGridColumn DataField="IsBillable" Type="CheckBox" TextAlign="Center" ></px:PXGridColumn>
									<px:PXGridColumn DataField="TimeSpent" RenderEditorText="True" ></px:PXGridColumn>
									<px:PXGridColumn DataField="OvertimeSpent" RenderEditorText="True" ></px:PXGridColumn>
									<px:PXGridColumn DataField="TimeBillable" RenderEditorText="True" ></px:PXGridColumn>
									<px:PXGridColumn DataField="OvertimeBillable" RenderEditorText="True" ></px:PXGridColumn>
									<px:PXGridColumn DataField="CreatedByID_Creator_Username" Visible="false" ></px:PXGridColumn>
									<px:PXGridColumn DataField="WorkgroupID" ></px:PXGridColumn>
									<px:PXGridColumn DataField="OwnerID" DisplayMode="Text" LinkCommand="OpenActivityOwner" ></px:PXGridColumn></Columns></px:PXGridLevel></Levels>
						<PreviewPanelTemplate>
							<px:PXHtmlView runat="server" DataField="body" ID="edBody" Height="100px" SkinID="Label" Width="100%">
								<AutoSize Enabled="true" Container="Parent" ></AutoSize></px:PXHtmlView></PreviewPanelTemplate></pxa:PXGridWithPreview></Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Purchase Details">
				<Template>
					<px:PXFormView Height="100%" SkinID="Transparent" runat="server" ID="CstFormView19" DataMember="ProjPurchDtls" DataSourceID="ds" Width="100%" >
						<Template>
							<px:PXLabel Height="2px" runat="server" ID="BlankLine1" ></px:PXLabel>
							<px:PXTextEdit Wrap="" LabelWidth="130px" Height="100px" AlreadyLocalized="" runat="server" ID="CstPXTextEdit21" DataField="PurchDescr" TextMode="MultiLine" Width="80%" ></px:PXTextEdit>
							<px:PXLabel Height="2px" runat="server" ID="BlankLine2" ></px:PXLabel>
							<px:PXTextEdit LabelWidth="130px"  Height="100px" TextMode="MultiLine" Width="80%" runat="server" ID="CstPXTextEdit22" DataField="PurchRemark" ></px:PXTextEdit>
							<px:PXLabel runat="server" ID="BlankLine3" Height="2px" ></px:PXLabel>
							<px:PXTextEdit LabelWidth="130px" Height="100px" TextMode="MultiLine" Width="80%" runat="server" ID="CstPXTextEdit23" DataField="InterRemark" ></px:PXTextEdit></Template></px:PXFormView></Template>
</px:PXTabItem>
			<px:PXTabItem Text="ISO Schedule" >
				<Template>
					<px:PXGrid AutoAdjustColumns="True" runat="server" ID="CstPXGrid20" Width="100%" SkinID="Details" DataSourceID="ds" SyncPosition="True">
						<Levels>
							<px:PXGridLevel DataMember="ProjISOSched" >
								<Columns>
									<px:PXGridColumn CommitChanges="True" DataField="ScheduleCD" Width="70" ></px:PXGridColumn>
									<px:PXGridColumn DataField="Descr" Width="280" ></px:PXGridColumn>
									<px:PXGridColumn DataField="ScheduleDate" Width="90" ></px:PXGridColumn></Columns></px:PXGridLevel></Levels>
						<AutoSize Enabled="True" ></AutoSize></px:PXGrid></Template></px:PXTabItem>
			<px:PXTabItem Text="Label Content" >
				<Template>
					<px:PXTextEdit Height="400px" runat="server" ID="CstPXTextEdit46" DataField="LabelContent" TextMode="MultiLine" Width="90%" ></px:PXTextEdit></Template>
				<ContentLayout InnerSpacing="True" Orientation="Vertical" ColumnsWidth="100%" ></ContentLayout></px:PXTabItem>
			<px:PXTabItem Text="ISO Design Input" >
<Template>
      <px:PXRichTextEdit runat="server" AllowLoadTemplate="false" 
         AllowAttached="true" AllowSearch="true" AllowMacros="true" 
         AllowSourceMode="true" DataField="ISODetails" ID="edDescription" 
         Style='width:100%;'>
        <AutoSize MinHeight="216" Enabled="True" ></AutoSize>
 <LoadTemplate TypeName="PX.SM.SMNotificationMaint" DataMember="Notifications" ViewName="NotificationTemplate" ValueField="notificationID" TextField="Name" DataSourceID="ds" Size="M"></LoadTemplate>     
</px:PXRichTextEdit>
    </Template>
</px:PXTabItem></Items>
		<AutoSize Container="Window" Enabled="True" MinHeight="150" ></AutoSize>
	</px:PXTab>
</asp:Content>