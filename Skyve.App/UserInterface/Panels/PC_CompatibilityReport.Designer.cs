﻿using Skyve.App.UserInterface.Lists;

namespace Skyve.App.UserInterface.Panels;

partial class PC_CompatibilityReport
{
	/// <summary> 
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary> 
	/// Clean up any resources being used.
	/// </summary>
	/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	protected override void Dispose(bool disposing)
	{
		if (disposing && (components != null))
		{
			_notifier.ContentLoaded -= CompatibilityManager_ReportProcessed;
			_notifier.CompatibilityReportProcessed -= CompatibilityManager_ReportProcessed;
			_compatibilityManager.SnoozeChanged -= CompatibilityManager_ReportProcessed;
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	#region Component Designer generated code

	/// <summary> 
	/// Required method for Designer support - do not modify 
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
			SlickControls.DynamicIcon dynamicIcon1 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon2 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon3 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon4 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon5 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon6 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon7 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon8 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon9 = new SlickControls.DynamicIcon();
			this.TLP_Buttons = new System.Windows.Forms.TableLayoutPanel();
			this.B_ApplyAll = new SlickControls.SlickButton();
			this.B_Manage = new SlickControls.SlickButton();
			this.B_YourPackages = new SlickControls.SlickButton();
			this.B_ManageSingle = new SlickControls.SlickButton();
			this.B_Requests = new SlickControls.SlickButton();
			this.PB_Loader = new SlickControls.SlickPictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.TLP_Main = new System.Windows.Forms.TableLayoutPanel();
			this.FLP_Search = new System.Windows.Forms.FlowLayoutPanel();
			this.TB_Search = new SlickControls.SlickTextBox();
			this.I_Refresh = new SlickControls.SlickIcon();
			this.B_Filters = new SlickControls.SlickLabel();
			this.slickSpacer2 = new SlickControls.SlickSpacer();
			this.slickSpacer1 = new SlickControls.SlickSpacer();
			this.TLP_MiddleBar = new System.Windows.Forms.TableLayoutPanel();
			this.L_FilterCount = new System.Windows.Forms.Label();
			this.P_FiltersContainer = new System.Windows.Forms.Panel();
			this.P_Filters = new SlickControls.RoundedGroupTableLayoutPanel();
			this.I_ClearFilters = new SlickControls.SlickIcon();
			this.DR_SubscribeTime = new SlickControls.SlickDateRange();
			this.DR_ServerTime = new SlickControls.SlickDateRange();
			this.I_SortOrder = new SlickControls.SlickIcon();
			this.ListControl = new Skyve.App.UserInterface.Lists.CompatibilityReportList();
			this.DD_Sorting = new Skyve.App.UserInterface.Dropdowns.SortingDropDown();
			this.OT_ModAsset = new Skyve.App.UserInterface.Generic.ThreeOptionToggle();
			this.OT_Workshop = new Skyve.App.UserInterface.Generic.ThreeOptionToggle();
			this.OT_Enabled = new Skyve.App.UserInterface.Generic.ThreeOptionToggle();
			this.OT_Included = new Skyve.App.UserInterface.Generic.ThreeOptionToggle();
			this.DD_PackageStatus = new Skyve.App.UserInterface.Dropdowns.PackageStatusDropDown();
			this.DD_Tags = new Skyve.App.UserInterface.Dropdowns.TagsDropDown();
			this.DD_Author = new Skyve.App.UserInterface.Dropdowns.AuthorDropDown();
			this.DD_Profile = new Skyve.App.UserInterface.Dropdowns.ProfilesDropDown();
			((System.ComponentModel.ISupportInitialize)(this.PB_Loader)).BeginInit();
			this.TLP_Main.SuspendLayout();
			this.FLP_Search.SuspendLayout();
			this.TLP_MiddleBar.SuspendLayout();
			this.P_FiltersContainer.SuspendLayout();
			this.P_Filters.SuspendLayout();
			this.SuspendLayout();
			// 
			// base_Text
			// 
			this.base_Text.Location = new System.Drawing.Point(-2, 3);
			this.base_Text.Size = new System.Drawing.Size(150, 32);
			// 
			// TLP_Buttons
			// 
			this.TLP_Buttons.AutoSize = true;
			this.TLP_Buttons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Buttons.ColumnCount = 6;
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Buttons.Location = new System.Drawing.Point(124, 427);
			this.TLP_Buttons.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_Buttons.Name = "TLP_Buttons";
			this.TLP_Buttons.RowCount = 1;
			this.TLP_Buttons.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Buttons.Size = new System.Drawing.Size(0, 0);
			this.TLP_Buttons.TabIndex = 0;
			// 
			// B_ApplyAll
			// 
			this.B_ApplyAll.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.B_ApplyAll.AutoSize = true;
			this.B_ApplyAll.ButtonType = SlickControls.ButtonType.Active;
			this.B_ApplyAll.ColorShade = null;
			this.B_ApplyAll.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_ApplyAll.Enabled = false;
			dynamicIcon1.Name = "I_CompatibilityReport";
			this.B_ApplyAll.ImageName = dynamicIcon1;
			this.B_ApplyAll.Location = new System.Drawing.Point(446, 3);
			this.B_ApplyAll.Name = "B_ApplyAll";
			this.B_ApplyAll.Size = new System.Drawing.Size(185, 23);
			this.B_ApplyAll.SpaceTriggersClick = true;
			this.B_ApplyAll.TabIndex = 1;
			this.B_ApplyAll.Text = "ApplyAllActions";
			this.B_ApplyAll.Click += new System.EventHandler(this.B_ApplyAll_Click);
			// 
			// B_Manage
			// 
			this.B_Manage.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.B_Manage.AutoSize = true;
			this.B_Manage.ColorShade = null;
			this.B_Manage.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon2.Name = "I_Cog";
			this.B_Manage.ImageName = dynamicIcon2;
			this.B_Manage.Location = new System.Drawing.Point(1113, 3);
			this.B_Manage.Name = "B_Manage";
			this.B_Manage.Size = new System.Drawing.Size(196, 23);
			this.B_Manage.SpaceTriggersClick = true;
			this.B_Manage.TabIndex = 5;
			this.B_Manage.Text = "ManageCompatibilityData";
			this.B_Manage.Click += new System.EventHandler(this.B_Manage_Click);
			// 
			// B_YourPackages
			// 
			this.B_YourPackages.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.B_YourPackages.AutoSize = true;
			this.B_YourPackages.ColorShade = null;
			this.B_YourPackages.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon3.Name = "I_User";
			this.B_YourPackages.ImageName = dynamicIcon3;
			this.B_YourPackages.Location = new System.Drawing.Point(637, 3);
			this.B_YourPackages.Name = "B_YourPackages";
			this.B_YourPackages.Size = new System.Drawing.Size(167, 23);
			this.B_YourPackages.SpaceTriggersClick = true;
			this.B_YourPackages.TabIndex = 2;
			this.B_YourPackages.Text = "YourPackages";
			this.B_YourPackages.Click += new System.EventHandler(this.B_Manage_Click);
			// 
			// B_ManageSingle
			// 
			this.B_ManageSingle.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.B_ManageSingle.AutoSize = true;
			this.B_ManageSingle.ColorShade = null;
			this.B_ManageSingle.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon4.Name = "I_Edit";
			this.B_ManageSingle.ImageName = dynamicIcon4;
			this.B_ManageSingle.Location = new System.Drawing.Point(810, 3);
			this.B_ManageSingle.Name = "B_ManageSingle";
			this.B_ManageSingle.Size = new System.Drawing.Size(187, 23);
			this.B_ManageSingle.SpaceTriggersClick = true;
			this.B_ManageSingle.TabIndex = 3;
			this.B_ManageSingle.Text = "ManageSinglePackage";
			this.B_ManageSingle.Click += new System.EventHandler(this.B_ManageSingle_Click);
			// 
			// B_Requests
			// 
			this.B_Requests.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.B_Requests.AutoSize = true;
			this.B_Requests.ColorShade = null;
			this.B_Requests.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon5.Name = "I_RequestReview";
			this.B_Requests.ImageName = dynamicIcon5;
			this.B_Requests.Location = new System.Drawing.Point(1003, 3);
			this.B_Requests.Name = "B_Requests";
			this.B_Requests.Size = new System.Drawing.Size(104, 23);
			this.B_Requests.SpaceTriggersClick = true;
			this.B_Requests.TabIndex = 4;
			this.B_Requests.Text = "ViewRequests";
			this.B_Requests.Click += new System.EventHandler(this.B_Requests_Click);
			// 
			// PB_Loader
			// 
			this.PB_Loader.LoaderSpeed = 1D;
			this.PB_Loader.Location = new System.Drawing.Point(640, 392);
			this.PB_Loader.Name = "PB_Loader";
			this.PB_Loader.Size = new System.Drawing.Size(32, 32);
			this.PB_Loader.TabIndex = 102;
			this.PB_Loader.TabStop = false;
			this.PB_Loader.Visible = false;
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(134, 20);
			this.label1.TabIndex = 103;
			this.label1.Text = "No issues detected";
			this.label1.Visible = false;
			// 
			// TLP_Main
			// 
			this.TLP_Main.ColumnCount = 3;
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Main.Controls.Add(this.FLP_Search, 0, 1);
			this.TLP_Main.Controls.Add(this.slickSpacer2, 0, 4);
			this.TLP_Main.Controls.Add(this.ListControl, 0, 7);
			this.TLP_Main.Controls.Add(this.slickSpacer1, 0, 6);
			this.TLP_Main.Controls.Add(this.DD_Sorting, 2, 1);
			this.TLP_Main.Controls.Add(this.TLP_MiddleBar, 0, 5);
			this.TLP_Main.Controls.Add(this.P_FiltersContainer, 0, 3);
			this.TLP_Main.Controls.Add(this.I_SortOrder, 1, 1);
			this.TLP_Main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_Main.Location = new System.Drawing.Point(0, 30);
			this.TLP_Main.Name = "TLP_Main";
			this.TLP_Main.RowCount = 8;
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Main.Size = new System.Drawing.Size(1312, 787);
			this.TLP_Main.TabIndex = 104;
			// 
			// FLP_Search
			// 
			this.FLP_Search.AutoSize = true;
			this.FLP_Search.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.FLP_Search.Controls.Add(this.TB_Search);
			this.FLP_Search.Controls.Add(this.I_Refresh);
			this.FLP_Search.Controls.Add(this.B_Filters);
			this.FLP_Search.Dock = System.Windows.Forms.DockStyle.Top;
			this.FLP_Search.Location = new System.Drawing.Point(0, 0);
			this.FLP_Search.Margin = new System.Windows.Forms.Padding(0);
			this.FLP_Search.Name = "FLP_Search";
			this.TLP_Main.SetRowSpan(this.FLP_Search, 2);
			this.FLP_Search.Size = new System.Drawing.Size(1236, 35);
			this.FLP_Search.TabIndex = 0;
			// 
			// TB_Search
			// 
			dynamicIcon6.Name = "I_Search";
			this.TB_Search.ImageName = dynamicIcon6;
			this.TB_Search.LabelText = "Search";
			this.TB_Search.Location = new System.Drawing.Point(3, 3);
			this.TB_Search.Name = "TB_Search";
			this.TB_Search.Padding = new System.Windows.Forms.Padding(0, 8, 0, 8);
			this.TB_Search.Placeholder = "SearchGenericPackages";
			this.TB_Search.SelectedText = "";
			this.TB_Search.SelectionLength = 0;
			this.TB_Search.SelectionStart = 0;
			this.TB_Search.ShowLabel = false;
			this.TB_Search.Size = new System.Drawing.Size(253, 29);
			this.TB_Search.TabIndex = 0;
			this.TB_Search.TextChanged += new System.EventHandler(this.TB_Search_TextChanged);
			this.TB_Search.IconClicked += new System.EventHandler(this.TB_Search_IconClicked);
			// 
			// I_Refresh
			// 
			this.I_Refresh.ActiveColor = null;
			this.I_Refresh.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon7.Name = "I_Refresh";
			this.I_Refresh.ImageName = dynamicIcon7;
			this.I_Refresh.Location = new System.Drawing.Point(262, 3);
			this.I_Refresh.Name = "I_Refresh";
			this.I_Refresh.Size = new System.Drawing.Size(14, 14);
			this.I_Refresh.SpaceTriggersClick = true;
			this.I_Refresh.TabIndex = 1;
			this.I_Refresh.SizeChanged += new System.EventHandler(this.I_Refresh_SizeChanged);
			this.I_Refresh.Click += new System.EventHandler(this.I_Refresh_Click);
			// 
			// B_Filters
			// 
			this.B_Filters.AutoHideText = false;
			this.B_Filters.AutoSize = true;
			this.B_Filters.AutoSizeIcon = true;
			this.B_Filters.ColorShade = null;
			this.B_Filters.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon8.Name = "I_Filter";
			this.B_Filters.ImageName = dynamicIcon8;
			this.B_Filters.Location = new System.Drawing.Point(282, 3);
			this.B_Filters.Name = "B_Filters";
			this.B_Filters.Selected = false;
			this.B_Filters.Size = new System.Drawing.Size(91, 23);
			this.B_Filters.SpaceTriggersClick = true;
			this.B_Filters.TabIndex = 1;
			this.B_Filters.Text = "ShowFilters";
			this.B_Filters.MouseClick += new System.Windows.Forms.MouseEventHandler(this.B_Filters_Click);
			// 
			// slickSpacer2
			// 
			this.TLP_Main.SetColumnSpan(this.slickSpacer2, 3);
			this.slickSpacer2.Dock = System.Windows.Forms.DockStyle.Top;
			this.slickSpacer2.Location = new System.Drawing.Point(0, 131);
			this.slickSpacer2.Margin = new System.Windows.Forms.Padding(0);
			this.slickSpacer2.Name = "slickSpacer2";
			this.slickSpacer2.Size = new System.Drawing.Size(1312, 2);
			this.slickSpacer2.TabIndex = 8;
			this.slickSpacer2.TabStop = false;
			this.slickSpacer2.Text = "slickSpacer2";
			// 
			// slickSpacer1
			// 
			this.TLP_Main.SetColumnSpan(this.slickSpacer1, 3);
			this.slickSpacer1.Dock = System.Windows.Forms.DockStyle.Top;
			this.slickSpacer1.Location = new System.Drawing.Point(0, 162);
			this.slickSpacer1.Margin = new System.Windows.Forms.Padding(0);
			this.slickSpacer1.Name = "slickSpacer1";
			this.slickSpacer1.Size = new System.Drawing.Size(1312, 2);
			this.slickSpacer1.TabIndex = 7;
			this.slickSpacer1.TabStop = false;
			this.slickSpacer1.Text = "slickSpacer1";
			// 
			// TLP_MiddleBar
			// 
			this.TLP_MiddleBar.AutoSize = true;
			this.TLP_MiddleBar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_MiddleBar.ColumnCount = 9;
			this.TLP_Main.SetColumnSpan(this.TLP_MiddleBar, 3);
			this.TLP_MiddleBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_MiddleBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_MiddleBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_MiddleBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_MiddleBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_MiddleBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_MiddleBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_MiddleBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_MiddleBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_MiddleBar.Controls.Add(this.B_Manage, 8, 0);
			this.TLP_MiddleBar.Controls.Add(this.B_ApplyAll, 4, 0);
			this.TLP_MiddleBar.Controls.Add(this.B_Requests, 7, 0);
			this.TLP_MiddleBar.Controls.Add(this.B_ManageSingle, 6, 0);
			this.TLP_MiddleBar.Controls.Add(this.B_YourPackages, 5, 0);
			this.TLP_MiddleBar.Controls.Add(this.L_FilterCount, 1, 0);
			this.TLP_MiddleBar.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_MiddleBar.Location = new System.Drawing.Point(0, 133);
			this.TLP_MiddleBar.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_MiddleBar.Name = "TLP_MiddleBar";
			this.TLP_MiddleBar.RowCount = 1;
			this.TLP_MiddleBar.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_MiddleBar.Size = new System.Drawing.Size(1312, 29);
			this.TLP_MiddleBar.TabIndex = 6;
			// 
			// L_FilterCount
			// 
			this.L_FilterCount.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.L_FilterCount.AutoSize = true;
			this.L_FilterCount.Location = new System.Drawing.Point(3, 4);
			this.L_FilterCount.Name = "L_FilterCount";
			this.L_FilterCount.Size = new System.Drawing.Size(50, 20);
			this.L_FilterCount.TabIndex = 2;
			this.L_FilterCount.Text = "label1";
			this.L_FilterCount.UseMnemonic = false;
			// 
			// P_FiltersContainer
			// 
			this.TLP_Main.SetColumnSpan(this.P_FiltersContainer, 3);
			this.P_FiltersContainer.Controls.Add(this.P_Filters);
			this.P_FiltersContainer.Dock = System.Windows.Forms.DockStyle.Top;
			this.P_FiltersContainer.Location = new System.Drawing.Point(0, 35);
			this.P_FiltersContainer.Margin = new System.Windows.Forms.Padding(0);
			this.P_FiltersContainer.Name = "P_FiltersContainer";
			this.P_FiltersContainer.Size = new System.Drawing.Size(1312, 96);
			this.P_FiltersContainer.TabIndex = 3;
			this.P_FiltersContainer.Visible = false;
			// 
			// P_Filters
			// 
			this.P_Filters.AddOutline = true;
			this.P_Filters.AutoSize = true;
			this.P_Filters.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_Filters.ColumnCount = 4;
			this.P_Filters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.P_Filters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.P_Filters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.P_Filters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.P_Filters.Controls.Add(this.OT_ModAsset, 0, 4);
			this.P_Filters.Controls.Add(this.OT_Workshop, 0, 3);
			this.P_Filters.Controls.Add(this.OT_Enabled, 0, 2);
			this.P_Filters.Controls.Add(this.OT_Included, 0, 1);
			this.P_Filters.Controls.Add(this.I_ClearFilters, 3, 0);
			this.P_Filters.Controls.Add(this.DR_SubscribeTime, 1, 1);
			this.P_Filters.Controls.Add(this.DR_ServerTime, 1, 2);
			this.P_Filters.Controls.Add(this.DD_PackageStatus, 2, 2);
			this.P_Filters.Controls.Add(this.DD_Tags, 2, 1);
			this.P_Filters.Controls.Add(this.DD_Author, 3, 1);
			this.P_Filters.Controls.Add(this.DD_Profile, 3, 2);
			this.P_Filters.Dock = System.Windows.Forms.DockStyle.Top;
			this.P_Filters.Location = new System.Drawing.Point(0, 0);
			this.P_Filters.Name = "P_Filters";
			this.P_Filters.Padding = new System.Windows.Forms.Padding(6);
			this.P_Filters.RowCount = 5;
			this.P_Filters.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_Filters.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_Filters.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_Filters.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_Filters.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_Filters.Size = new System.Drawing.Size(1312, 143);
			this.P_Filters.TabIndex = 0;
			this.P_Filters.Text = "Filters";
			this.P_Filters.UseFirstRowForPadding = true;
			// 
			// I_ClearFilters
			// 
			this.I_ClearFilters.ActiveColor = null;
			this.I_ClearFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.I_ClearFilters.ColorStyle = Extensions.ColorStyle.Red;
			this.I_ClearFilters.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon9.Name = "I_ClearFilter";
			this.I_ClearFilters.ImageName = dynamicIcon9;
			this.I_ClearFilters.Location = new System.Drawing.Point(1273, 9);
			this.I_ClearFilters.Name = "I_ClearFilters";
			this.I_ClearFilters.Size = new System.Drawing.Size(30, 21);
			this.I_ClearFilters.TabIndex = 1;
			this.I_ClearFilters.Click += new System.EventHandler(this.I_ClearFilters_Click);
			// 
			// DR_SubscribeTime
			// 
			this.DR_SubscribeTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.DR_SubscribeTime.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DR_SubscribeTime.Location = new System.Drawing.Point(334, 36);
			this.DR_SubscribeTime.Name = "DR_SubscribeTime";
			this.DR_SubscribeTime.Size = new System.Drawing.Size(319, 20);
			this.DR_SubscribeTime.TabIndex = 3;
			this.DR_SubscribeTime.RangeChanged += new System.EventHandler(this.FilterChanged);
			// 
			// DR_ServerTime
			// 
			this.DR_ServerTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.DR_ServerTime.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DR_ServerTime.Location = new System.Drawing.Point(334, 62);
			this.DR_ServerTime.Name = "DR_ServerTime";
			this.DR_ServerTime.Size = new System.Drawing.Size(319, 20);
			this.DR_ServerTime.TabIndex = 4;
			this.DR_ServerTime.RangeChanged += new System.EventHandler(this.FilterChanged);
			// 
			// I_SortOrder
			// 
			this.I_SortOrder.ActiveColor = null;
			this.I_SortOrder.Cursor = System.Windows.Forms.Cursors.Hand;
			this.I_SortOrder.Location = new System.Drawing.Point(1239, 3);
			this.I_SortOrder.Name = "I_SortOrder";
			this.I_SortOrder.Size = new System.Drawing.Size(14, 14);
			this.I_SortOrder.TabIndex = 1;
			// 
			// ListControl
			// 
			this.ListControl.AllowDrop = true;
			this.ListControl.AutoInvalidate = false;
			this.ListControl.AutoScroll = true;
			this.TLP_Main.SetColumnSpan(this.ListControl, 3);
			this.ListControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ListControl.DynamicSizing = true;
			this.ListControl.GridView = true;
			this.ListControl.ItemHeight = 75;
			this.ListControl.Location = new System.Drawing.Point(0, 164);
			this.ListControl.Margin = new System.Windows.Forms.Padding(0);
			this.ListControl.Name = "ListControl";
			this.ListControl.Size = new System.Drawing.Size(1312, 623);
			this.ListControl.TabIndex = 1;
			// 
			// DD_Sorting
			// 
			this.DD_Sorting.AccentBackColor = true;
			this.DD_Sorting.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_Sorting.Font = new System.Drawing.Font("Nirmala UI", 15F);
			this.DD_Sorting.HideLabel = true;
			this.DD_Sorting.Location = new System.Drawing.Point(1259, 3);
			this.DD_Sorting.Name = "DD_Sorting";
			this.DD_Sorting.Size = new System.Drawing.Size(50, 0);
			this.DD_Sorting.SkyvePage = Skyve.Domain.Enums.SkyvePage.None;
			this.DD_Sorting.TabIndex = 2;
			this.DD_Sorting.Text = "Sort By";
			// 
			// OT_ModAsset
			// 
			this.OT_ModAsset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.OT_ModAsset.Cursor = System.Windows.Forms.Cursors.Hand;
			this.OT_ModAsset.Image1 = "I_Mods";
			this.OT_ModAsset.Image2 = "I_Assets";
			this.OT_ModAsset.Location = new System.Drawing.Point(9, 114);
			this.OT_ModAsset.Name = "OT_ModAsset";
			this.OT_ModAsset.Option1 = "Mods";
			this.OT_ModAsset.Option2 = "Assets";
			this.OT_ModAsset.OptionStyle1 = Extensions.ColorStyle.Active;
			this.OT_ModAsset.OptionStyle2 = Extensions.ColorStyle.Active;
			this.OT_ModAsset.Size = new System.Drawing.Size(319, 20);
			this.OT_ModAsset.TabIndex = 10;
			this.OT_ModAsset.SelectedValueChanged += new System.EventHandler(this.FilterChanged);
			// 
			// OT_Workshop
			// 
			this.OT_Workshop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.OT_Workshop.Cursor = System.Windows.Forms.Cursors.Hand;
			this.OT_Workshop.Image1 = "I_PC";
			this.OT_Workshop.Image2 = "I_Steam";
			this.OT_Workshop.Location = new System.Drawing.Point(9, 88);
			this.OT_Workshop.Name = "OT_Workshop";
			this.OT_Workshop.Option1 = "Local";
			this.OT_Workshop.Option2 = "Workshop";
			this.OT_Workshop.OptionStyle1 = Extensions.ColorStyle.Active;
			this.OT_Workshop.OptionStyle2 = Extensions.ColorStyle.Active;
			this.OT_Workshop.Size = new System.Drawing.Size(319, 20);
			this.OT_Workshop.TabIndex = 2;
			this.OT_Workshop.SelectedValueChanged += new System.EventHandler(this.FilterChanged);
			// 
			// OT_Enabled
			// 
			this.OT_Enabled.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.OT_Enabled.Cursor = System.Windows.Forms.Cursors.Hand;
			this.OT_Enabled.Image1 = "I_Checked";
			this.OT_Enabled.Image2 = "I_Checked_OFF";
			this.OT_Enabled.Location = new System.Drawing.Point(9, 62);
			this.OT_Enabled.Name = "OT_Enabled";
			this.OT_Enabled.Option1 = "Enabled";
			this.OT_Enabled.Option2 = "Disabled";
			this.OT_Enabled.Size = new System.Drawing.Size(319, 20);
			this.OT_Enabled.TabIndex = 1;
			this.OT_Enabled.SelectedValueChanged += new System.EventHandler(this.FilterChanged);
			// 
			// OT_Included
			// 
			this.OT_Included.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.OT_Included.Cursor = System.Windows.Forms.Cursors.Hand;
			this.OT_Included.Image1 = "I_Ok";
			this.OT_Included.Image2 = "I_Enabled";
			this.OT_Included.Location = new System.Drawing.Point(9, 36);
			this.OT_Included.Name = "OT_Included";
			this.OT_Included.Option1 = "Included";
			this.OT_Included.Option2 = "Excluded";
			this.OT_Included.Size = new System.Drawing.Size(319, 20);
			this.OT_Included.TabIndex = 0;
			this.OT_Included.SelectedValueChanged += new System.EventHandler(this.FilterChanged);
			// 
			// DD_PackageStatus
			// 
			this.DD_PackageStatus.AccentBackColor = true;
			this.DD_PackageStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.DD_PackageStatus.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_PackageStatus.Font = new System.Drawing.Font("Nirmala UI", 15F);
			this.DD_PackageStatus.Location = new System.Drawing.Point(659, 62);
			this.DD_PackageStatus.Name = "DD_PackageStatus";
			this.DD_PackageStatus.Size = new System.Drawing.Size(319, 20);
			this.DD_PackageStatus.TabIndex = 7;
			this.DD_PackageStatus.SelectedItemChanged += new System.EventHandler(this.FilterChanged);
			// 
			// DD_Tags
			// 
			this.DD_Tags.AccentBackColor = true;
			this.DD_Tags.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.DD_Tags.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_Tags.Font = new System.Drawing.Font("Nirmala UI", 15F);
			this.DD_Tags.Location = new System.Drawing.Point(659, 36);
			this.DD_Tags.Name = "DD_Tags";
			this.DD_Tags.Size = new System.Drawing.Size(319, 20);
			this.DD_Tags.TabIndex = 5;
			this.DD_Tags.SelectedItemChanged += new System.EventHandler(this.FilterChanged);
			// 
			// DD_Author
			// 
			this.DD_Author.AccentBackColor = true;
			this.DD_Author.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.DD_Author.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_Author.Location = new System.Drawing.Point(984, 36);
			this.DD_Author.Name = "DD_Author";
			this.DD_Author.Size = new System.Drawing.Size(319, 20);
			this.DD_Author.TabIndex = 6;
			this.DD_Author.SelectedItemChanged += new System.EventHandler(this.FilterChanged);
			// 
			// DD_Profile
			// 
			this.DD_Profile.AccentBackColor = true;
			this.DD_Profile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.DD_Profile.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_Profile.Font = new System.Drawing.Font("Nirmala UI", 15F);
			this.DD_Profile.Location = new System.Drawing.Point(984, 62);
			this.DD_Profile.Name = "DD_Profile";
			this.DD_Profile.Size = new System.Drawing.Size(319, 20);
			this.DD_Profile.TabIndex = 9;
			this.DD_Profile.SelectedItemChanged += new System.EventHandler(this.FilterChanged);
			// 
			// PC_CompatibilityReport
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.label1);
			this.Controls.Add(this.PB_Loader);
			this.Controls.Add(this.TLP_Buttons);
			this.Controls.Add(this.TLP_Main);
			this.LabelBounds = new System.Drawing.Point(-2, 3);
			this.Name = "PC_CompatibilityReport";
			this.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
			this.Size = new System.Drawing.Size(1312, 817);
			this.Controls.SetChildIndex(this.TLP_Main, 0);
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.Controls.SetChildIndex(this.TLP_Buttons, 0);
			this.Controls.SetChildIndex(this.PB_Loader, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			((System.ComponentModel.ISupportInitialize)(this.PB_Loader)).EndInit();
			this.TLP_Main.ResumeLayout(false);
			this.TLP_Main.PerformLayout();
			this.FLP_Search.ResumeLayout(false);
			this.FLP_Search.PerformLayout();
			this.TLP_MiddleBar.ResumeLayout(false);
			this.TLP_MiddleBar.PerformLayout();
			this.P_FiltersContainer.ResumeLayout(false);
			this.P_FiltersContainer.PerformLayout();
			this.P_Filters.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion
	private System.Windows.Forms.TableLayoutPanel TLP_Buttons;
	private SlickControls.SlickButton B_Manage;
	private SlickControls.SlickButton B_YourPackages;
	private SlickControls.SlickButton B_ManageSingle;
	private CompatibilityReportList ListControl;
	private SlickControls.SlickPictureBox PB_Loader;
	private SlickControls.SlickButton B_Requests;
	private SlickButton B_ApplyAll;
	private System.Windows.Forms.Label label1;
	public System.Windows.Forms.TableLayoutPanel TLP_Main;
	public System.Windows.Forms.FlowLayoutPanel FLP_Search;
	public SlickTextBox TB_Search;
	internal SlickIcon I_Refresh;
	internal SlickLabel B_Filters;
	internal Dropdowns.SortingDropDown DD_Sorting;
	public System.Windows.Forms.Panel P_FiltersContainer;
	internal RoundedGroupTableLayoutPanel P_Filters;
	internal Generic.ThreeOptionToggle OT_ModAsset;
	internal Generic.ThreeOptionToggle OT_Workshop;
	internal Generic.ThreeOptionToggle OT_Enabled;
	internal Generic.ThreeOptionToggle OT_Included;
	internal SlickIcon I_ClearFilters;
	internal SlickDateRange DR_SubscribeTime;
	internal SlickDateRange DR_ServerTime;
	internal Dropdowns.PackageStatusDropDown DD_PackageStatus;
	internal Dropdowns.TagsDropDown DD_Tags;
	internal Dropdowns.AuthorDropDown DD_Author;
	internal Dropdowns.ProfilesDropDown DD_Profile;
	internal SlickIcon I_SortOrder;
	internal System.Windows.Forms.TableLayoutPanel TLP_MiddleBar;
	internal System.Windows.Forms.Label L_FilterCount;
	internal SlickSpacer slickSpacer2;
	internal SlickSpacer slickSpacer1;
}
