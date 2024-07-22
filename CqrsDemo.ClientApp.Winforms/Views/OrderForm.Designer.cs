namespace CqrsDemo.ClientApp.Winforms.Views
{
    partial class OrderForm
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
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrderForm));
            ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            barButtonItemSave = new DevExpress.XtraBars.BarButtonItem();
            barButtonItemAddPosition = new DevExpress.XtraBars.BarButtonItem();
            barButtonItemDeletePosition = new DevExpress.XtraBars.BarButtonItem();
            ribbonPage = new DevExpress.XtraBars.Ribbon.RibbonPage();
            ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            textEditProduct = new DevExpress.XtraEditors.TextEdit();
            treeListPositions = new DevExpress.XtraTreeList.TreeList();
            comboBoxEditStatus = new DevExpress.XtraEditors.ComboBoxEdit();
            textEditName = new DevExpress.XtraEditors.TextEdit();
            layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)ribbonControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)textEditProduct.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)treeListPositions).BeginInit();
            ((System.ComponentModel.ISupportInitialize)comboBoxEditStatus.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)textEditName.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).BeginInit();
            SuspendLayout();
            // 
            // ribbonControl1
            // 
            ribbonControl1.EmptyAreaImageOptions.ImagePadding = new Padding(45, 44, 45, 44);
            ribbonControl1.ExpandCollapseItem.Id = 0;
            ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] { ribbonControl1.ExpandCollapseItem, barButtonItemSave, barButtonItemAddPosition, barButtonItemDeletePosition });
            ribbonControl1.Location = new Point(0, 0);
            ribbonControl1.Margin = new Padding(4, 4, 4, 4);
            ribbonControl1.MaxItemId = 4;
            ribbonControl1.Name = "ribbonControl1";
            ribbonControl1.OptionsMenuMinWidth = 495;
            ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] { ribbonPage });
            ribbonControl1.Size = new Size(1044, 219);
            // 
            // barButtonItemSave
            // 
            barButtonItemSave.Caption = "Save";
            barButtonItemSave.Id = 1;
            barButtonItemSave.ImageOptions.Image = (Image)resources.GetObject("barButtonItem1.ImageOptions.Image");
            barButtonItemSave.ImageOptions.LargeImage = (Image)resources.GetObject("barButtonItem1.ImageOptions.LargeImage");
            barButtonItemSave.Name = "barButtonItemSave";
            barButtonItemSave.ItemClick += SaveButtonClicked;
            // 
            // barButtonItemAddPosition
            // 
            barButtonItemAddPosition.Caption = "Add position";
            barButtonItemAddPosition.Id = 2;
            barButtonItemAddPosition.Name = "barButtonItemAddPosition";
            barButtonItemAddPosition.ItemClick += AddPositionButtonClicked;
            // 
            // barButtonItemDeletePosition
            // 
            barButtonItemDeletePosition.Caption = "Delete Position";
            barButtonItemDeletePosition.Id = 3;
            barButtonItemDeletePosition.Name = "barButtonItemDeletePosition";
            barButtonItemDeletePosition.ItemClick += DeletePositionButtonClicked;
            // 
            // ribbonPage
            // 
            ribbonPage.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] { ribbonPageGroup1 });
            ribbonPage.Name = "ribbonPage";
            ribbonPage.Text = "Form";
            // 
            // ribbonPageGroup1
            // 
            ribbonPageGroup1.AllowTextClipping = false;
            ribbonPageGroup1.ItemLinks.Add(barButtonItemSave);
            ribbonPageGroup1.ItemLinks.Add(barButtonItemAddPosition);
            ribbonPageGroup1.ItemLinks.Add(barButtonItemDeletePosition);
            ribbonPageGroup1.Name = "ribbonPageGroup1";
            ribbonPageGroup1.Text = "Record";
            // 
            // layoutControl1
            // 
            layoutControl1.Controls.Add(textEditProduct);
            layoutControl1.Controls.Add(treeListPositions);
            layoutControl1.Controls.Add(comboBoxEditStatus);
            layoutControl1.Controls.Add(textEditName);
            layoutControl1.Dock = DockStyle.Fill;
            layoutControl1.Location = new Point(0, 219);
            layoutControl1.Margin = new Padding(4, 4, 4, 4);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.Root = layoutControlGroup1;
            layoutControl1.Size = new Size(1044, 421);
            layoutControl1.TabIndex = 1;
            layoutControl1.Text = "layoutControl1";
            // 
            // textEditProduct
            // 
            textEditProduct.Location = new Point(99, 50);
            textEditProduct.Margin = new Padding(4, 4, 4, 4);
            textEditProduct.MenuManager = ribbonControl1;
            textEditProduct.Name = "textEditProduct";
            textEditProduct.Size = new Size(927, 26);
            textEditProduct.StyleController = layoutControl1;
            textEditProduct.TabIndex = 7;
            // 
            // treeListPositions
            // 
            treeListPositions.FixedLineWidth = 3;
            treeListPositions.HorzScrollStep = 4;
            treeListPositions.Location = new Point(18, 137);
            treeListPositions.Margin = new Padding(4, 4, 4, 4);
            treeListPositions.MinWidth = 30;
            treeListPositions.Name = "treeListPositions";
            treeListPositions.Size = new Size(1008, 266);
            treeListPositions.TabIndex = 6;
            treeListPositions.TreeLevelWidth = 27;
            treeListPositions.FocusedNodeChanged += TreeListFocusedNodeClicked;
            // 
            // comboBoxEditStatus
            // 
            comboBoxEditStatus.Location = new Point(99, 82);
            comboBoxEditStatus.Margin = new Padding(4, 4, 4, 4);
            comboBoxEditStatus.MenuManager = ribbonControl1;
            comboBoxEditStatus.Name = "comboBoxEditStatus";
            comboBoxEditStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            comboBoxEditStatus.Size = new Size(927, 26);
            comboBoxEditStatus.StyleController = layoutControl1;
            comboBoxEditStatus.TabIndex = 5;
            // 
            // textEditName
            // 
            textEditName.Location = new Point(99, 18);
            textEditName.Margin = new Padding(4, 4, 4, 4);
            textEditName.MenuManager = ribbonControl1;
            textEditName.Name = "textEditName";
            textEditName.Size = new Size(927, 26);
            textEditName.StyleController = layoutControl1;
            textEditName.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            layoutControlGroup1.GroupBordersVisible = false;
            layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1, layoutControlItem2, layoutControlItem3, layoutControlItem4 });
            layoutControlGroup1.Name = "layoutControlGroup1";
            layoutControlGroup1.Size = new Size(1044, 421);
            layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = textEditName;
            layoutControlItem1.Location = new Point(0, 0);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new Size(1014, 32);
            layoutControlItem1.Text = "Name";
            layoutControlItem1.TextSize = new Size(63, 19);
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.Control = comboBoxEditStatus;
            layoutControlItem2.Location = new Point(0, 64);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new Size(1014, 32);
            layoutControlItem2.Text = "Status";
            layoutControlItem2.TextSize = new Size(63, 19);
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.Control = treeListPositions;
            layoutControlItem3.Location = new Point(0, 96);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.Size = new Size(1014, 295);
            layoutControlItem3.Text = "Positions";
            layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem3.TextSize = new Size(63, 19);
            // 
            // layoutControlItem4
            // 
            layoutControlItem4.Control = textEditProduct;
            layoutControlItem4.Location = new Point(0, 32);
            layoutControlItem4.Name = "layoutControlItem4";
            layoutControlItem4.Size = new Size(1014, 32);
            layoutControlItem4.Text = "Product";
            layoutControlItem4.TextSize = new Size(63, 19);
            // 
            // OrderForm
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1044, 640);
            Controls.Add(layoutControl1);
            Controls.Add(ribbonControl1);
            Margin = new Padding(4, 4, 4, 4);
            Name = "OrderForm";
            Text = "OrderForm";
            ((System.ComponentModel.ISupportInitialize)ribbonControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)textEditProduct.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)treeListPositions).EndInit();
            ((System.ComponentModel.ISupportInitialize)comboBoxEditStatus.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)textEditName.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraBars.BarButtonItem barButtonItemSave;
        private DevExpress.XtraEditors.TextEdit textEditName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditStatus;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraTreeList.TreeList treeListPositions;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItemAddPosition;
        private DevExpress.XtraBars.BarButtonItem barButtonItemDeletePosition;
        private DevExpress.XtraEditors.TextEdit textEditProduct;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    }
}