using CqrsDemo.ClientApp.App.Controllers;
using DevExpress.XtraEditors;

namespace CqrsDemo.ClientApp.Winforms.Views
{
    public partial class OrderForm : XtraForm, IOrderFormView
    {
        public OrderForm()
        {
            InitializeComponent();
        }

        private object dataContext;
        public new object DataContext
        {
            get { return dataContext; }
            set
            {
                dataContext = value;
                var vm = GetViewModel();

                textEditName.Text = vm.Name;
                textEditProduct.Text = vm.Product.Name;
                comboBoxEditStatus.EditValue = vm.Status;
                treeListPositions.DataSource = vm.Positions;
            }
        }

        bool? IView.ShowDialog()
        {
            ShowDialog();
            return true;
        }

        private void SaveButtonClicked(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var vm = GetViewModel();
            vm.Name = textEditName.Text;

            GetViewModel().CreateOrUpdateCommand.Execute(null);
        }

        private OrderFormViewModel GetViewModel() => (DataContext as OrderFormViewModel);

        private void TreeListFocusedNodeClicked(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
        }

        private void AddPositionButtonClicked(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetViewModel().Positions.Add(new OrderFormViewModel.Position() { Name = "" });
        }

        private void DeletePositionButtonClicked(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var record = treeListPositions.GetDataRecordByNode(treeListPositions.Selection[0]) as OrderFormViewModel.Position;
            if(record != null)
            {
                record.Delete = true;
            }
        }
    }
}