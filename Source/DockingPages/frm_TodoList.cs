using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

// Uses icons from http://www.famfamfam.com/lab/icons/silk/

namespace Protean.Plugins.TodoList.DockingPages
{
	/// <summary>
	/// Summary description for frm_TodoList.
	/// </summary>
	public class frm_TodoList : Protean.Hub.DockingPage.DockingPageControl
	{
		private System.Windows.Forms.ListView lsv_Tasks;
		private System.Windows.Forms.ColumnHeader Type;
		private System.Windows.Forms.ColumnHeader File;
		private System.Windows.Forms.ColumnHeader Path;
		private System.Windows.Forms.ColumnHeader Description;
		private System.Windows.Forms.ColumnHeader Line;
		private System.Windows.Forms.ImageList imageList1;
		private Protean.Hub.Toolbars.ThemedToolbar themedToolbar1;
		private TD.SandBar.ButtonItem buttonItem1;
		private System.Windows.Forms.ImageList iml_ToolbarIcons;
		private System.ComponentModel.IContainer components;
		private TD.SandBar.ComboBoxItem comboBoxItem1;

		private System.Threading.Thread parserThread;
		private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.StatusBarPanel sbp_TotalItems;
		private System.Windows.Forms.StatusBarPanel sbp_FilterItems;
		private ListView.ListViewItemCollection m_ListItems;

		internal enum ItemType 
		{
			Todo = 0,
			Temp = 1,
			Kludge = 2,
			Hack = 3,
			Note = 4,
			All
		}

		public frm_TodoList()
		{
			SourceParser.ParsingFinished += new Protean.Plugins.TodoList.SourceParser.ParsingFinishedEventHandler(SourceParser_ParsingFinished);
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.comboBoxItem1.ComboBox.SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);
			this.m_ListItems	= new ListView.ListViewItemCollection(new ListView());
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		internal void AddItem(ItemType itemType, int lineNumber, string description, Protean.Hub.Files.File sourceFile)
		{
			ListViewItem item = new ListViewItem();
			item.Tag = sourceFile;
			item.Text = itemType.ToString();

			item.ImageIndex	= this.IconFromType(itemType);


			item.SubItems.Add(new ListViewItem.ListViewSubItem(item, lineNumber.ToString()));
			item.SubItems.Add(new ListViewItem.ListViewSubItem(item, description.Trim()));
			item.SubItems.Add(new ListViewItem.ListViewSubItem(item, sourceFile.FileNameOnly));
			item.SubItems.Add(new ListViewItem.ListViewSubItem(item, sourceFile.FileNameExpanded));

			this.m_ListItems.Add(item);
			this.lsv_Tasks.Items.Add((ListViewItem)item.Clone());

			this.sbp_TotalItems.Text = "Total items: " + this.lsv_Tasks.Items.Count.ToString();
			this.sbp_FilterItems.Text = "Filtered items: " + this.lsv_Tasks.Items.Count.ToString();

		}

		internal int IconFromType(ItemType itemType)
		{
			return (int)itemType + 1;

			switch(itemType)
			{
				case ItemType.Todo: return 1; 
				case ItemType.Temp: return 2; 
				case ItemType.Kludge: return 3; 
				case ItemType.Hack: return 4; 
			}


			return 0;
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frm_TodoList));
			this.lsv_Tasks = new System.Windows.Forms.ListView();
			this.Type = new System.Windows.Forms.ColumnHeader();
			this.Line = new System.Windows.Forms.ColumnHeader();
			this.Description = new System.Windows.Forms.ColumnHeader();
			this.File = new System.Windows.Forms.ColumnHeader();
			this.Path = new System.Windows.Forms.ColumnHeader();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.themedToolbar1 = new Protean.Hub.Toolbars.ThemedToolbar();
			this.iml_ToolbarIcons = new System.Windows.Forms.ImageList(this.components);
			this.buttonItem1 = new TD.SandBar.ButtonItem();
			this.comboBoxItem1 = new TD.SandBar.ComboBoxItem();
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.sbp_TotalItems = new System.Windows.Forms.StatusBarPanel();
			this.sbp_FilterItems = new System.Windows.Forms.StatusBarPanel();
			((System.ComponentModel.ISupportInitialize)(this.sbp_TotalItems)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sbp_FilterItems)).BeginInit();
			this.SuspendLayout();
			// 
			// lsv_Tasks
			// 
			this.lsv_Tasks.AllowColumnReorder = true;
			this.lsv_Tasks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lsv_Tasks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.Type,
																						this.Line,
																						this.Description,
																						this.File,
																						this.Path});
			this.lsv_Tasks.FullRowSelect = true;
			this.lsv_Tasks.GridLines = true;
			this.lsv_Tasks.Location = new System.Drawing.Point(0, 24);
			this.lsv_Tasks.MultiSelect = false;
			this.lsv_Tasks.Name = "lsv_Tasks";
			this.lsv_Tasks.Size = new System.Drawing.Size(880, 232);
			this.lsv_Tasks.SmallImageList = this.imageList1;
			this.lsv_Tasks.Sorting = System.Windows.Forms.SortOrder.Descending;
			this.lsv_Tasks.TabIndex = 0;
			this.lsv_Tasks.View = System.Windows.Forms.View.Details;
			this.lsv_Tasks.DoubleClick += new System.EventHandler(this.lsv_Tasks_DoubleClick);
			this.lsv_Tasks.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lsv_Tasks_ColumnClick);
			this.lsv_Tasks.SelectedIndexChanged += new System.EventHandler(this.lsv_Tasks_SelectedIndexChanged);
			// 
			// Type
			// 
			this.Type.Text = "!";
			this.Type.Width = 20;
			// 
			// Line
			// 
			this.Line.Text = "Line";
			this.Line.Width = 63;
			// 
			// Description
			// 
			this.Description.Text = "Description";
			this.Description.Width = 403;
			// 
			// File
			// 
			this.File.Text = "File";
			this.File.Width = 261;
			// 
			// Path
			// 
			this.Path.Text = "Path";
			this.Path.Width = 177;
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
			// 
			// themedToolbar1
			// 
			this.themedToolbar1.Guid = new System.Guid("1d85a100-43d0-4404-ab5d-07f27ff109e0");
			this.themedToolbar1.ImageList = this.iml_ToolbarIcons;
			this.themedToolbar1.Items.AddRange(new TD.SandBar.ToolbarItemBase[] {
																					this.buttonItem1,
																					this.comboBoxItem1});
			this.themedToolbar1.Location = new System.Drawing.Point(0, 0);
			this.themedToolbar1.Name = "themedToolbar1";
			this.themedToolbar1.Renderer = new TD.SandBar.WhidbeyRenderer();
			this.themedToolbar1.Size = new System.Drawing.Size(880, 24);
			this.themedToolbar1.TabIndex = 1;
			this.themedToolbar1.Text = "";
			// 
			// iml_ToolbarIcons
			// 
			this.iml_ToolbarIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.iml_ToolbarIcons.ImageSize = new System.Drawing.Size(16, 16);
			this.iml_ToolbarIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_ToolbarIcons.ImageStream")));
			this.iml_ToolbarIcons.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// buttonItem1
			// 
			this.buttonItem1.ImageIndex = 0;
			this.buttonItem1.Text = "Refresh";
			this.buttonItem1.Activate += new System.EventHandler(this.buttonItem1_Activate);
			// 
			// comboBoxItem1
			// 
			this.comboBoxItem1.BeginGroup = true;
			this.comboBoxItem1.DefaultText = "Show All";
			this.comboBoxItem1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxItem1.Items.AddRange(new object[] {
															   "Show All",
															   "HACK",
															   "KLUDGE",
															   "TEMP",
															   "TODO",
															   "NOTE"});
			this.comboBoxItem1.MinimumControlWidth = 100;
			this.comboBoxItem1.Padding.Bottom = 0;
			this.comboBoxItem1.Padding.Left = 1;
			this.comboBoxItem1.Padding.Right = 1;
			this.comboBoxItem1.Padding.Top = 0;
			this.comboBoxItem1.Text = "Filter: ";
			// 
			// statusBar1
			// 
			this.statusBar1.Location = new System.Drawing.Point(0, 258);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																						  this.sbp_TotalItems,
																						  this.sbp_FilterItems});
			this.statusBar1.ShowPanels = true;
			this.statusBar1.Size = new System.Drawing.Size(880, 22);
			this.statusBar1.SizingGrip = false;
			this.statusBar1.TabIndex = 2;
			this.statusBar1.Text = "statusBar1";
			this.statusBar1.PanelClick += new System.Windows.Forms.StatusBarPanelClickEventHandler(this.statusBar1_PanelClick);
			// 
			// sbp_TotalItems
			// 
			this.sbp_TotalItems.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
			this.sbp_TotalItems.Text = "Total Items:";
			this.sbp_TotalItems.Width = 73;
			// 
			// sbp_FilterItems
			// 
			this.sbp_FilterItems.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
			this.sbp_FilterItems.Text = "Filtered Items:";
			this.sbp_FilterItems.Width = 86;
			// 
			// frm_TodoList
			// 
			this.Controls.Add(this.statusBar1);
			this.Controls.Add(this.themedToolbar1);
			this.Controls.Add(this.lsv_Tasks);
			this.Image16 = ((System.Drawing.Image)(resources.GetObject("$this.Image16")));
			this.InitialPosition = Protean.Hub.DockingPage.DockingLocation.Bottom;
			this.MenuTitle = "Task List";
			this.Name = "frm_TodoList";
			this.ShowByDefault = false;
			this.Size = new System.Drawing.Size(880, 280);
			this.Title = "Task List";
			((System.ComponentModel.ISupportInitialize)(this.sbp_TotalItems)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.sbp_FilterItems)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void lsv_Tasks_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		private void buttonItem1_Activate(object sender, EventArgs e)
		{
			this.lsv_Tasks.Items.Clear();
			this.m_ListItems.Clear();
			this.comboBoxItem1.ComboBox.SelectedIndex = 0;

			this.buttonItem1.Enabled	= false;

			this.parserThread	= new System.Threading.Thread(new System.Threading.ThreadStart(SourceParser.Parse));
			this.parserThread.Start();


		//	Protean.Plugins.TodoList.SourceParser.s_Timer_Elapsed();
		}

		private void lsv_Tasks_DoubleClick(object sender, System.EventArgs e)
		{
			ListViewItem selectedItem	= this.lsv_Tasks.SelectedItems[0];

			if (selectedItem != null) 
			{
				Protean.Hub.Files.File sourceFile	= (Protean.Hub.Files.File)selectedItem.Tag;
				Protean.Hub.Files.File.Load(sourceFile.FileNameExpanded);
				Protean.Hub.EditingPage.EditingPageControl editingPage	= sourceFile.EditingPageControl;
				if(editingPage != null)
				{
					try
					{
						Protean.Hub.EditingPage.ISyntaxBoxEditor editor = (Protean.Hub.EditingPage.ISyntaxBoxEditor) editingPage;
						editor.EditorControl.GotoLine(Int32.Parse(selectedItem.SubItems[1].Text) - 1);
					}
					catch (Exception exception1)
					{
					}

				}
			}

		}

		private void SourceParser_ParsingFinished(Object sender, EventArgs e)
		{
			this.buttonItem1.Enabled = true;
			this.parserThread.Suspend();
		}

		private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ItemType filter = ItemType.All;

			switch((string)this.comboBoxItem1.ComboBox.SelectedItem)
			{
				case "HACK": filter = ItemType.Hack; break;
				case "TODO": filter = ItemType.Todo; break;
				case "TEMP": filter = ItemType.Temp; break;
				case "KLUDGE": filter = ItemType.Kludge; break;
				case "NOTE": filter = ItemType.Note; break;
			}

			this.lsv_Tasks.Items.Clear();

			foreach(ListViewItem listItem in this.m_ListItems) 
			{
				if(filter != ItemType.All) 
				{
					if(listItem.Text == filter.ToString()) 
					{
						this.lsv_Tasks.Items.Add((ListViewItem)listItem.Clone());
					}
				}
				else
				{
					this.lsv_Tasks.Items.Add((ListViewItem)listItem.Clone());
				}
			}

			this.sbp_FilterItems.Text = "Filtered items: " + this.lsv_Tasks.Items.Count.ToString();
		}

		private void lsv_Tasks_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			switch(this.lsv_Tasks.Sorting) 
			{
				case System.Windows.Forms.SortOrder.Ascending: this.lsv_Tasks.Sorting = System.Windows.Forms.SortOrder.Descending; break;
				case System.Windows.Forms.SortOrder.Descending: this.lsv_Tasks.Sorting = System.Windows.Forms.SortOrder.Ascending; break;
				default: this.lsv_Tasks.Sorting = SortOrder.Descending; break;
			}

			this.lsv_Tasks.ListViewItemSorter = new ListViewItemComparer(e.Column, this.lsv_Tasks.Sorting);
			this.lsv_Tasks.Sort();
		}

		private void statusBar1_PanelClick(object sender, System.Windows.Forms.StatusBarPanelClickEventArgs e)
		{
		
		}
	}

	class ListViewItemComparer : IComparer 
	{
		private SortOrder m_SortOrder;
		private int col;
		
		public ListViewItemComparer(int column, SortOrder sortingOrder) 
		{
			col=column;
			m_SortOrder = sortingOrder;
		}

		public int Compare(object x, object y) 
		{
			int result;
			ListViewItem item1	= (ListViewItem)x;
			ListViewItem item2	= (ListViewItem)y;

			if(this.col == 1) 
			{
				int x1	= int.Parse(item1.SubItems[this.col].Text);
				int y1	= int.Parse(item2.SubItems[this.col].Text);

				result	= 0;
				if (x1 > y1) 
				{ 
					result = 1;
				} 
				else if(x1 < y1) 
				{
					result = -1;
				}


			}
			else
			{
				result	= String.Compare(item1.SubItems[col].Text, item2.SubItems[col].Text);
			}

			if (this.m_SortOrder == SortOrder.Ascending) 
			{
				return result;
			} 
			else 
			{
				return -result;
			}
		}
	}
}
