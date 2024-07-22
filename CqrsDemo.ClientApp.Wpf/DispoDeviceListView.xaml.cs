using System.Windows;
using CqrsDemo.ClientApp.App.Controllers;

namespace CqrsDemo.ClientApp.Wpf
{
    /// <summary>
    /// Interaction logic for DispoDeviceListView.xaml
    /// </summary>
    public partial class DispoDeviceListView : Window, IDispoDeviceListView
    {
        public DispoDeviceListView()
        {
            InitializeComponent();
        }
    }
}
