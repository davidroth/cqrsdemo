using CqrsDemo.ClientApp.App.Controllers;
using CqrsDemo.Core.Queries;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;

namespace CqrsDemo.ClientApp.Winforms.Views
{
    public partial class MainForm : RibbonForm, IMainView
    {
        public MainForm()
        {
            InitializeComponent();
            gridView1.FocusedRowChanged += GridView1_FocusedRowChanged;
            gridView1.KeyUp += (sender, e) =>
            {
                if(e.KeyCode == Keys.F5)
                {
                    GetViewModel().ReloadCommand.Execute(null);
                }
            };
        }

        private void GridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            GetViewModel().SelectedRecord = gridView1.GetRow(e.FocusedRowHandle) as GetOrderList.Dto;
            UpdateRecordButtonCommandStatus();
        }

        private object dataContext;
        public new object DataContext
        {
            get { return dataContext; }
            set
            {
                dataContext = value;
                gridControl1.DataSource = GetViewModel().AvailableOrders;

                GetViewModel().EditRecordCommand.CanExecuteChanged += (sender, e) => barButtonItemEdit.Enabled = GetViewModel().EditRecordCommand.CanExecute(null);
                GetViewModel().CancelOrderCommand.CanExecuteChanged += (sender, e) => barButtonItemCloseOrder.Enabled = GetViewModel().CancelOrderCommand.CanExecute(null);
            }
        }

        private void UpdateRecordButtonCommandStatus()
        {
            barButtonItemEdit.Enabled = GetViewModel().EditRecordCommand.CanExecute(null);
            barButtonItemCloseOrder.Enabled = GetViewModel().CancelOrderCommand.CanExecute(null);
        }

        private MainViewModel GetViewModel() => (DataContext as MainViewModel);
        public void ShowMessageBox(string message) => MessageBox.Show(message);

        private void barButtonItemCreate_ItemClick(object sender, ItemClickEventArgs e) 
            => GetViewModel().CreateCommand.Execute(null);

        private void barButtonItemEdit_ItemClick(object sender, ItemClickEventArgs e) 
            => GetViewModel().EditCommand.Execute(null);

        private void barButtonItemCloseOrder_ItemClick(object sender, ItemClickEventArgs e) 
            => GetViewModel().CancelOrderCommand.Execute(null);

        public new bool? ShowDialog()
        {
            base.ShowDialog();
            return true;
        }
    }
}