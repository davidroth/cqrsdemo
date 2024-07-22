using System.Collections.ObjectModel;
using System.Windows.Input;
using CqrsDemo.Core.Queries;

namespace CqrsDemo.ClientApp.App.Controllers
{
    public class MainViewModel : ViewModel
    {
        public MainViewModel()
        {}

        public ICommand? UpdateUsernameCommand { get; set; }

        public ICommand? EditRecordCommand { get; set; }

        public ObservableCollection<GetOrderList.Dto> AvailableOrders { get; } = [];
        
        public GetOrderList.Dto? SelectedRecord { get; set; }

        public ICommand? CreateCommand { get; set; }

        public ICommand? CancelOrderCommand { get; set; }
        
        public ICommand? EditCommand { get; set; }

        public ICommand? ReloadCommand { get; set; }
    }
}