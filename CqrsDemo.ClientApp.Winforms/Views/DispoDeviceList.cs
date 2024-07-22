using CqrsDemo.ClientApp.App.Controllers;
using CqrsDemo.Core.Queries;
using DevExpress.XtraBars.Ribbon;

namespace CqrsDemo.ClientApp.Winforms.Views
{
    public partial class DispoDeviceList : RibbonForm, IDispoDeviceListView
    {
        public DispoDeviceList()
        {
            InitializeComponent();
            gridViewDispos.FocusedRowChanged += GridViewDispos_FocusedRowChanged;
        }

        private void GridViewDispos_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            GetViewModel().SelectedRecord = gridViewDispos.GetRow(e.FocusedRowHandle) as GetDispoDeviceList.ListModel;
            UpdateRecordButtonCommandStatus();
        }

        private void UpdateRecordButtonCommandStatus()
        {
            barButtonItemUpdateDeliveryDate.Enabled = GetViewModel().UpdateDeliveryDateCommand.CanExecute(null);
        }

        private object dataContext;
        public new object DataContext
        {
            get { return dataContext; }
            set
            {
                dataContext = value;
                gridControlDispos.DataSource = GetViewModel().Items;
                GetViewModel().UpdateDeliveryDateCommand.CanExecuteChanged += (sender, e) => barButtonItemUpdateDeliveryDate.Enabled = GetViewModel().UpdateDeliveryDateCommand.CanExecute(null);
            }
        }

        private DispoDeviceListViewModel GetViewModel() => (DataContext as DispoDeviceListViewModel);

        bool? IView.ShowDialog()
        {
            ShowDialog();
            return true;
        }

        private void barButtonItemUpdateDeliveryDate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) => GetViewModel().UpdateDeliveryDateCommand.Execute(null);
    }
}