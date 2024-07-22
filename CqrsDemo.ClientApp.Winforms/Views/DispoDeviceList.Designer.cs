namespace CqrsDemo.ClientApp.Winforms.Views
{
    partial class DispoDeviceList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DispoDeviceList));
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonItemUpdateDeliveryDate = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.gridControlDispos = new DevExpress.XtraGrid.GridControl();
            this.gridViewDispos = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDispos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDispos)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.barButtonItemUpdateDeliveryDate});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 2;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl1.Size = new System.Drawing.Size(768, 143);
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
            // 
            // barButtonItemUpdateDeliveryDate
            // 
            this.barButtonItemUpdateDeliveryDate.Caption = "Change delivery date";
            this.barButtonItemUpdateDeliveryDate.Id = 1;
            this.barButtonItemUpdateDeliveryDate.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItemUpdateDeliveryDate.ImageOptions.Image")));
            this.barButtonItemUpdateDeliveryDate.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItemUpdateDeliveryDate.ImageOptions.LargeImage")));
            this.barButtonItemUpdateDeliveryDate.Name = "barButtonItemUpdateDeliveryDate";
            this.barButtonItemUpdateDeliveryDate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemUpdateDeliveryDate_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Dispos";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItemUpdateDeliveryDate);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Actions";
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 464);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl1;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(768, 31);
            // 
            // ribbonPage2
            // 
            this.ribbonPage2.Name = "ribbonPage2";
            this.ribbonPage2.Text = "ribbonPage2";
            // 
            // gridControlDispos
            // 
            this.gridControlDispos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlDispos.Location = new System.Drawing.Point(0, 143);
            this.gridControlDispos.MainView = this.gridViewDispos;
            this.gridControlDispos.MenuManager = this.ribbonControl1;
            this.gridControlDispos.Name = "gridControlDispos";
            this.gridControlDispos.Size = new System.Drawing.Size(768, 321);
            this.gridControlDispos.TabIndex = 2;
            this.gridControlDispos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDispos});
            // 
            // gridViewDispos
            // 
            this.gridViewDispos.GridControl = this.gridControlDispos;
            this.gridViewDispos.Name = "gridViewDispos";
            this.gridViewDispos.OptionsBehavior.Editable = false;
            // 
            // DispoDeviceList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 495);
            this.Controls.Add(this.gridControlDispos);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "DispoDeviceList";
            this.Ribbon = this.ribbonControl1;
            this.StatusBar = this.ribbonStatusBar1;
            this.Text = "DispoDeviceList";
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDispos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDispos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage2;
        private DevExpress.XtraGrid.GridControl gridControlDispos;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDispos;
        private DevExpress.XtraBars.BarButtonItem barButtonItemUpdateDeliveryDate;
    }
}